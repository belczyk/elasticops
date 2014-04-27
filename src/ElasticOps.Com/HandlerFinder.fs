module ElasticOps.Com.HandlerFinder
open System.Reflection
open System.Linq
open ElasticOps.Com.CommonTypes
open System

exception AmbiguousHanlderResolution of string
exception IllegalOperationMultipleESVersionFromAttributes of string
let private foldMethodNames (methods : MethodInfo list) =
    methods 
    |> List.map (fun m -> m.Name)
    |> List.fold (fun mn r -> mn + ", " + r) String.Empty

let private raiseAmbiguousHanlderResolution matchingType (reqType : Type) eligableMethods  = 
      raise (AmbiguousHanlderResolution (sprintf "found multiple \"%s\" handlers for requested type %s: %s" matchingType  reqType.Name  (foldMethodNames eligableMethods) ))



let getVersionAttribute<'T when 'T :> ESVersionAttribute > (m : MethodInfo) =
    let attributes = m.GetCustomAttributes().ToList()
                     |> Seq.filter (fun a -> a.GetType() = typeof<ESVersionFrom>)
                     |> List.ofSeq

    match attributes with
    | [] -> None
    | from::[] -> Some ((from :?> ESVersionFrom).ToVersion())
    | _ -> raise (IllegalOperationMultipleESVersionFromAttributes (sprintf "Methods %s have multiple ESVersionFrom attributes" m.Name))


let private byVersion (version : CommonTypes.Version) (m : MethodInfo) =
    let fromVersion = getVersionAttribute<ESVersionFrom> m
    let toVersion = getVersionAttribute<ESVersionTo> m
    match (fromVersion, toVersion) with 
    | (None,None) -> true
    | (Some v,None) -> version  >= v
    | (None , Some v) -> version <= v
    | (Some vFrom, Some vTo) -> version >= vFrom && version<= vTo

let exactHanlderMatch (methods :  MethodInfo list) (reqType : Type) (version : CommonTypes.Version) = 
     let eligableMethods = methods  
                            |> List.filter (fun m -> m.ReturnType = reqType)
                            |> List.filter (byVersion version)

     match eligableMethods with
     | [] -> None 
     | m::[]  -> Some m
     | _ -> raiseAmbiguousHanlderResolution "returns exacly requested type" (reqType : Type) eligableMethods


