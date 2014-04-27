module ElasticOps.Com.HandlerFinder
open System.Reflection
open System
open System.Linq
exception AmbiguousHanlderResolution of string

let private foldMethodNames (methods : MethodInfo list) =
    methods 
    |> List.map (fun m -> m.Name)
    |> List.fold (fun mn r -> mn + ", " + r) String.Empty

let private raiseAmbiguousHanlderResolution matchingType (reqType : Type) eligableMethods  = 
      raise (AmbiguousHanlderResolution (sprintf "found multiple \"%s\" handlers for requested type %s: %s" matchingType  reqType.Name  (foldMethodNames eligableMethods) ))


let private matchMethods filter (methods :  MethodInfo list) reqType matchingType= 
     let eligableMethods = methods |> 
                            List.filter filter

     match eligableMethods with
     | [] -> None 
     | m::[]  -> Some m
     | _ -> raiseAmbiguousHanlderResolution matchingType reqType eligableMethods

let exactHanlderMatch (methods :  MethodInfo list)  reqType = 
     matchMethods (fun m -> m.ReturnType = reqType) methods reqType "returns exacly requested type"

let collectionHandlerMatch  (methods :  MethodInfo list)  reqType = 
     matchMethods (fun m -> m.ReturnType.GetGenericArguments().Any() && m.ReturnType.GetGenericArguments().First() = reqType) 
                 methods 
                 reqType 
                 "returns collection of requtested type"
