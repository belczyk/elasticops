module ElasticOps.Com.HandlerFinder 

    open System.Reflection
    open System.Linq
    open System

    exception AmbiguousHanlderResolution of string
    exception IllegalOperationMultipleESVersionFromAttributes of string
    let private foldMethodNames (methods : MethodInfo list) =
        methods 
        |> List.map (fun m -> m.Name)
        |> List.fold (fun mn r -> mn + ", " + r) String.Empty
    
    let getVersionAttribute<'T when 'T :> ESVersionAttribute > (m : MethodInfo) =
        let attributes = m.GetCustomAttributes().ToList()
                         |> Seq.filter (fun a -> a.GetType() = typeof<'T>)
                         |> List.ofSeq
    
        match attributes with
        | [] -> None
        | from::[] -> Some ((from :?> 'T ).ToVersion()) 
        | _ -> raise (IllegalOperationMultipleESVersionFromAttributes (sprintf "Methods %s have multiple ESVersionFrom attributes" m.Name))
    
    let private byVersion (version : ElasticOps.Com.Version) (m : MethodInfo) =
        let fromVersion = getVersionAttribute<ESVersionFrom> m
        let toVersion = getVersionAttribute<ESVersionTo> m
        match (fromVersion, toVersion) with 
        | (None,None) -> true
        | (Some v,None) -> version  >= v
        | (None , Some v) -> version <= v
        | (Some vFrom, Some vTo) -> version >= vFrom && version<= vTo
    
    let private raiseAmbiguousHanlderResolution (commandType : Type) eligableMethods (version :  ElasticOps.Com.Version) = 
          raise (AmbiguousHanlderResolution (sprintf "found multiple handlers for requested command %s: %s. ElasticSearch version: %s" commandType.Name  (foldMethodNames eligableMethods) (version.ToString()) ))
    
    let exactHanlderMatch (methods :  MethodInfo list) (commandType : Type) (version : ElasticOps.Com.Version) = 
         let eligableMethods = methods  
                                |> List.filter (fun m -> m.GetParameters().First().ParameterType = commandType)
                                |> List.filter (byVersion version)
    
         match eligableMethods with
         | [] -> None 
         | m::[]  -> Some m
         | _ -> raiseAmbiguousHanlderResolution commandType eligableMethods version
