namespace  ElasticOps.Com
open System 
open System.Reflection
open System.Linq
open ElasticOps.Com
open NLog
open Caliburn.Micro

type CommandBus(eventAggregator : IEventAggregator, client : IRESTClient ) =
    let eventAggregator = eventAggregator
    let client = client

    static member private logger = LogManager.GetCurrentClassLogger()

    member private this.buildArgs(argsTypes : ParameterInfo list, args : Object list) = 
        match argsTypes with 
            | hd::tail -> match hd.ParameterType.Name with
                | "IRESTClient" -> this.buildArgs(tail,(client :> Object)::args)
                | _ -> invalidOp "Unknown type. Can't find value for handlers argument of type"
            | [] -> args
        
    member private  this.executeMethod<'TResult when 'TResult : null> (m : MethodInfo) (command : Command<'TResult>) =
        try 
            let argsTypes = m.GetParameters() |> List.ofArray |> List.tail
            let args = this.buildArgs (argsTypes,[command]) |> List.rev
            let result = (m.Invoke(null,Array.ofList args))
            new CommandResult<'TResult> (result :?> 'TResult)
        with 
            | ex ->
                CommandBus.logger.WarnException("Exception while executing command in CommandBus",ex)
                match ex.InnerException with
                | null -> 
                    eventAggregator.Publish (new ErrorOccuredEvent(ex.Message))
                    new CommandResult<'TResult>(ex.Message)
                | inner -> 
                    eventAggregator.Publish (new ErrorOccuredEvent(ex.InnerException.Message))
                    new CommandResult<'TResult>(ex.InnerException.Message)

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

            match (HandlerFinder.exactHanlderMatch eligibleMethods requestedType command.Version) with 
            | Some m -> (this.executeMethod<'TResult> m command)
            | None -> 
                let msg = String.Format("Handler for {0} not found.",requestedType.Name)
                eventAggregator.Publish(new ErrorOccuredEvent(msg))
                new CommandResult<'TResult>(msg)
