namespace  ElasticOps.Com.Infrastructure
open System 
open System.Reflection
open System.Linq
open ElasticOps.Com
open ElasticOps.Com.HandlerFinder
open ElasticOps.Com.CommonTypes
open NLog

type RequestBus() =
    static member private logger = LogManager.GetCurrentClassLogger()

    member private  x.executeMethod<'T when 'T : null> (m : MethodInfo) connection =
        try 
            new RequestResult<'T> ((m.Invoke(null,[|connection|])) :?> 'T)
        with 
            | ex ->
                RequestBus.logger.WarnException("Exception while executing request in RequestBus",ex)
                match ex.InnerException with
                | null -> new RequestResult<'T>(ex.Message)
                | inner -> new RequestResult<'T>(ex.InnerException.Message)

    member this.Execute<'T when 'T : null> (connection : Connection)  =
        let requestedType = typeof<'T>

        let eligibleMethods =Assembly.GetExecutingAssembly().GetTypes()  
                            |> List.ofArray 
                            |> List.filter  (fun t -> t.IsSealed && t.IsAbstract )
                            |> List.filter  (fun t -> t.GetCustomAttributes().Any(fun a -> a.GetType() = typedefof<CommandsHanlders>) )
                            |> List.collect (fun t -> List.ofArray (t.GetMethods()))
                            |> List.filter (fun m -> m.IsStatic)

        match (exactHanlderMatch eligibleMethods requestedType connection.version) with 
        | Some m -> (this.executeMethod<'T> m connection)
        | None -> new RequestResult<'T>("Handler not found")
