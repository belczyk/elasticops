namespace  ElasticOps.Com
open System 
open System.Reflection
open System.Linq
open ElasticOps.Com
open Caliburn.Micro
open Serilog

type CommandBus(eventAggregator : Caliburn.Micro.IEventAggregator) =
    let eventAggregator = eventAggregator


    member private  this.executeMethod<'TResult when 'TResult : null> (m : MethodInfo) (command : Command<'TResult>) =
        try 
            let args = [command :> System.Object]
            let result = (m.Invoke(null,Array.ofList args))
            new CommandResult<'TResult> (result :?> 'TResult)
        with 
            | ex ->
                match ex.InnerException with
                | null -> 
                    eventAggregator.PublishOnUIThread (new ErrorOccuredEvent(ex.Message))
                    Log.Logger.Warning(ex, "Error when executing command {@Command}. Exception: {@ExceptionMessage}",command.GetType().Name,ex.Message)
                    new CommandResult<'TResult>(String.Format("ES Version {0}", if command.Connection.Version = null then command.Connection.DiskVersion.ToString() else command.Connection.Version.ToString())+ex.Message,ex)
                | inner -> 
                    eventAggregator.PublishOnUIThread (new ErrorOccuredEvent(ex.InnerException.Message))
                    Log.Logger.Warning(ex, "Error when executing command {@Command}. Inner exception: {@ExceptionMessage}",command.GetType().Name,inner.Message)
                    new CommandResult<'TResult>(String.Format("ES Version {0}", if command.Connection.Version = null then command.Connection.DiskVersion.ToString() else command.Connection.Version.ToString())+ex.InnerException.Message,ex)

    member this.Execute<'TResult when 'TResult : null> (command : Command<'TResult>)  =
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
            eventAggregator.PublishOnUIThread(new ErrorOccuredEvent(msg))
            new CommandResult<'TResult>(msg)
