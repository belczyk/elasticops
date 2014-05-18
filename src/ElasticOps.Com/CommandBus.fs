namespace  ElasticOps.Com.Infrastructure
open System 
open System.Reflection
open System.Linq
open ElasticOps.Com
open ElasticOps.Com.HandlerFinder
open ElasticOps.Com.CommonTypes
open NLog
open Caliburn.Micro
open ElasticOps.Com.Events

type CommandBus(eventAggregator : IEventAggregator ) =
    let eventAggregator = eventAggregator

    static member private logger = LogManager.GetCurrentClassLogger()

    member private  x.executeMethod<'TResult when 'TResult : null> (m : MethodInfo) (command : Command<'TResult>) =
        try 
            let result = (m.Invoke(null,[|command|]))
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

            match (exactHanlderMatch eligibleMethods requestedType command.Version) with 
            | Some m -> (this.executeMethod<'TResult> m command)
            | None -> 
                let msg = String.Format("Handler for {0} not found.",requestedType.Name)
                eventAggregator.Publish(new ErrorOccuredEvent(msg))
                new CommandResult<'TResult>(msg)
