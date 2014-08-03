module ElasticOps.Com.ElasticResponseProcessing
open System.Collections.Generic
open System
open FSharp.Data
open FSharp.Data.JsonExtensions
open Humanizer
open FSharp.Data.Runtime
open System.Linq

let asPropertyList (jsonValue : JsonValue) = 
    jsonValue.Properties 


let asPropertyListOfScalars (jsonValue : JsonValue) = 
    jsonValue.Properties 
    |> Seq.filter (fun p -> match JsonConversions.AsString false null (snd p) with 
                                | Some _ -> true
                                | _ -> false)

let humanizeKeys (pair : (string * JsonValue)) = 
    ((fst pair).Humanize(LetterCasing.Sentence), (snd pair))

let extractStringsFromValues (pair : (string * JsonValue)) =
    ((fst pair),(snd pair).AsString())

let asKeyValuePairList (propSeq :  (string * JsonValue) seq) = 
    propSeq |> Seq.map humanizeKeys 
    |> Seq.map (fun pair -> new KeyValuePair<string,string>((fst pair),(snd pair).AsString()))
    |> List.ofSeq
    
let combineUri ( uri : Uri) endpoint =
    match endpoint with 
    | null | "" -> uri.ToString()
    | _ -> let u = new Uri(uri,new Uri(endpoint,UriKind.Relative))
           u.ToString()


let GET uri endpoint =
    combineUri uri endpoint |> Http.RequestString 

let POSTJson uri endpoint body =
    Http.RequestString ( (combineUri uri endpoint), httpMethod = "POST",
                        headers = [ "Accept", "application/json" ],
                        body   = TextRequest body
                        ) 

let POST uri endpoint body =
    Http.RequestString ( (combineUri uri endpoint), httpMethod = "POST",
                        body   = TextRequest body
                        ) 

let propCount selector json =
    json 
        |> JsonValue.Parse
        |> selector
        |> asPropertyList
        |> Seq.length
    
module CList =
    let ofSeq (seq : seq<_>)=
        seq.ToList()

    let ofList (list : _ list ) =
        list.ToList()
