namespace  ElasticOps.Com.Infrastructure
open System 
open System.Reflection
open System.Linq
open ElasticOps.Com
open ElasticOps.Com.HandlerFinder
open ElasticOps.Com.CommonTypes
open NLog

type CommandBus() =
    static member private logger = LogManager.GetCurrentClassLogger()

    member private  x.executeMethod<'TResult when 'TResult : null> (m : MethodInfo) (command : Command<'TResult>) =
        try 
            let result = (m.Invoke(null,[|command|]))
            new CommandResult<'TResult> (result :?> 'TResult)
        with 
            | ex ->
                CommandBus.logger.WarnException("Exception while executing command in CommandBus",ex)
                match ex.InnerException with
                | null -> new CommandResult<'TResult>(ex.Message)
                | inner -> new CommandResult<'TResult>(ex.InnerException.Message)

    member this.Execute<'TResult when 'TResult : null> (command : Command<'TResult>)  =
        match command.ClusterUri with
        | null -> new CommandResult<'TResult>("Missing uri")
        | _ -> 
            let requestedType = command.GetType()

            let eligibleMethods =Assembly.GetExecutingAssembly().GetTypes()  
                                |> List.ofArray 
                                |> List.filter  (fun t -> t.IsSealed && t.IsAbstract )
                                |> List.filter  (fun t -> t.GetCustomAttributes().Any(fun a -> a.GetType() = typeof<CommandsHandlers>) )
                                |> List.collect (fun t -> List.ofArray (t.GetMethods()))
                                |> List.filter (fun m -> m.IsStatic)

            match (exactHanlderMatch eligibleMethods requestedType command.Version) with 
            | Some m -> (this.executeMethod<'TResult> m command)
            | None -> new CommandResult<'TResult>("Handler not found")
