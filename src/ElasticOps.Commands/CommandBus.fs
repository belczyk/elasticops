namespace  ElasticOps.Com
open System 
open System.Reflection
open System.Linq
open ElasticOps.Com
open Caliburn.Micro
open Logary
open System.Collections.Generic

type CommandBus(eventAggregator : IEventAggregator) =
    let eventAggregator = eventAggregator

    let logger = Logging.getCurrentLogger()

    member private  this.executeMethod<'TResult when 'TResult : null> (m : MethodInfo) (command : Command<'TResult>) =
        try 
            let args = [command :> System.Object]
            let result = (m.Invoke(null,Array.ofList args))
            new CommandResult<'TResult> (result :?> 'TResult)
        with 
            | ex ->
                logger.Log <| LogLine.create''' "Error occured while executing command." (new Dictionary<string,Object>()) LogLevel.Error [||] "" ex NodaTime.SystemClock.Instance.Now
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
