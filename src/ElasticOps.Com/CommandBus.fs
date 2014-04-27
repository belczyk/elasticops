namespace  ElasticOps.Com.Infrastructure
open System 
open System.Reflection
open System.Linq
open ElasticOps.Com
open ElasticOps.Com.HandlerFinder
type Version(major : int,?minor : int,?patch : int,?build : int) =
    member x.major = major
    member x.minor = minor
    member x.patch = patch
    member x.build = build

exception HanlderNotFound

type Connection(clusterUri : Uri, version : Version) =
    member x.clusterUri = clusterUri
    member x.version = version 

type CommandBus() =
    member x.Execute<'T> () =
        let requestedType = typedefof<'T>
        Console.WriteLine requestedType.Name
        let eligibleMethods =Assembly.GetExecutingAssembly().GetTypes()  
                            |> List.ofArray 
                            |> List.filter  (fun t -> t.IsSealed && t.IsAbstract )
                            |> List.filter  (fun t -> t.GetCustomAttributes().Any(fun a -> a.GetType() = typedefof<CommandsHanlders>) )
                            |> List.collect (fun t -> List.ofArray (t.GetMethods()))
                            |> List.filter (fun m -> m.IsStatic)

        match (exactHanlderMatch eligibleMethods requestedType) with 
        | Some m -> m
        | None -> raise HanlderNotFound
