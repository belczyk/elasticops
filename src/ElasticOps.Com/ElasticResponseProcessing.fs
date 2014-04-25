module ElasticOps.Com.ElasticResponseProcessing
open System.Collections.Generic
open System
open FSharp.Data
open FSharp.Data.JsonExtensions
open Humanizer
open FSharp.Data.Runtime

let asPropertyList (jsonValue : JsonValue) = 
    jsonValue.Properties 


let asPropertyListOfScalars (jsonValue : JsonValue) = 
    jsonValue.Properties 
    |> Seq.filter (fun p -> match JsonConversions.AsString false null (snd p) with 
                                | Some _ -> true
                                | _ -> false)

let humanizeKeys (pair : (string * JsonValue)) = 
    ((fst pair).Humanize(LetterCasing.Sentence), (snd pair))


let asKeyValuePairList (propSeq :  (string * JsonValue) seq) = 
    propSeq |> Seq.map humanizeKeys 
    |> Seq.map (fun pair -> new KeyValuePair<string,string>((fst pair),(snd pair).AsString()))
    |> List.ofSeq
    
let combineUri uri endpoint =
    let u = new Uri(uri,new Uri(endpoint,UriKind.Relative))
    u.ToString()


let request uri endpoint =
    combineUri uri endpoint |> Http.RequestString 

let propCount selector json =
    json 
        |> JsonValue.Parse
        |> selector
        |> asPropertyList
        |> Seq.length


