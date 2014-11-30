namespace ElasticOps.Com
open System.Collections.Generic
open System
open FSharp.Data
open FSharp.Data.JsonExtensions
open Humanizer
open FSharp.Data.Runtime
open System.Linq


[<AutoOpen>]
module CList =
    let ofSeq (seq : seq<_>)=
        seq.ToList()

    let ofList (list : _ list ) =
        list.ToList()

[<AutoOpen>]
module REST =
    open Logary 
    let logger = Logging.getCurrentLogger()

    let GET' = "GET"
    let POST' = "POST"

    let combineUri ( uri : Uri) endpoint =
        match endpoint with 
        | null | "" -> uri.ToString()
        | _ -> let u = new Uri(uri,new Uri(endpoint,UriKind.Relative))
               u.ToString()

    let GET uri endpoint =
        let url = combineUri uri endpoint
        Log.info "request" [("Uri", url);("Verb", GET')] |> logger.Log
        Http.RequestString  url 

    let POSTJson uri endpoint body =
        let url = combineUri uri endpoint 
        Log.info "request" [("uri", url);("verb", POST'); ("body",body)]  |> logger.Log
        Http.RequestString ( url, httpMethod = "POST",
                            headers = [ "Accept", "application/json" ],
                            body   = TextRequest body )

    let POST uri endpoint body =
        let url = (combineUri uri endpoint)
        Log.info "request"  [("uri", url);("verb", POST'); ("body",body)]  |> logger.Log
        Http.RequestString ( url , httpMethod = "POST",
                            body   = TextRequest body )

[<AutoOpen>]
module JsonValueProcessing =
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
        
    let propCount selector json =
        json 
            |> JsonValue.Parse
            |> selector
            |> asPropertyList
            |> Seq.length

