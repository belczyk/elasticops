namespace  ElasticOps.Com

    open System 
    open System.Reflection
    open System.Linq
    open ElasticOps.Com
    open Serilog
    
    type CommandBus() =
    
        member private this.buildErrorMessage (command : Command<'T>) (exception' : Exception) = 
            let version = match (command.Connection.Version) with
                            | null -> match (command.Connection.DiskVersion) with
                                        | null -> "[unknown]"
                                        | v -> v.ToString()
                            | v -> v.ToString()
            let clusterUri = match command.Connection.ClusterUri with
                                | null -> "[null]"
                                | u -> u.ToString()
    
            String.Format("Error occured. Url: {0}. Version {1}. Error: {2}", clusterUri, version, exception'.Message)
    
        member private  this.executeMethod<'TResult when 'TResult : null> (m : MethodInfo) (command : Command<'TResult>) =
            try 
                let args = [command :> System.Object]
                let result = (m.Invoke(null,Array.ofList args))
                new CommandResult<'TResult> (result :?> 'TResult)
            with 
                | ex ->
                    match ex.InnerException with
                    | null -> 
                        Log.Logger.Error(ex, "Error when executing command {@Command}. Exception: {@ExceptionMessage}",command.GetType().Name,ex.Message)
                        new CommandResult<'TResult>((this.buildErrorMessage command ex),ex)
                    | inner -> 
                        Log.Logger.Error(ex, "Error when executing command {@Command}. Inner exception: {@ExceptionMessage}",command.GetType().Name,inner.Message)
                        new CommandResult<'TResult>((this.buildErrorMessage command inner),ex)
    
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
                Log.Logger.Error("Handler for {handlerName} not found.",requestedType.Name)
                new CommandResult<'TResult>(msg)
