namespace ElasticOps.Com
open System.Collections.Generic
open System
open FSharp.Data
open FSharp.Data.JsonExtensions
open Humanizer
open FSharp.Data.Runtime
open System.Linq
open Serilog

[<AutoOpen>]
module CList =
    let ofSeq (seq : seq<_>)=
        seq.ToList()

    let ofList (list : _ list ) =
        list.ToList()

[<AutoOpen>]
module REST =

    let GET' = "GET"
    let POST' = "POST"

    let combineUri ( uri : Uri) endpoint =
        match endpoint with 
        | null | "" -> uri.ToString()
        | _ -> let u = new Uri(uri,new Uri(endpoint,UriKind.Relative))
               u.ToString()

    let GET (connection : Connection) endpoint =
        let url = combineUri connection.ClusterUri endpoint

        try 
            let result = Http.RequestString  url 

            if (connection.IsTrackEnabled) then
                Log.Logger.Information("GET   clusterUri: {clusterUri}  and endpoint: {endpoint}.  Result:  {result}",connection.ClusterUri,endpoint,result)
                
            result
        with
        | ex -> 
            Log.Logger.Error("Error when executing GET. ClusterUri: {clusterUri} and endpoint: {endpoint}. Error: {Exception}",connection.ClusterUri,endpoint,ex)
            raise ex

    let POSTJson (connection : Connection) endpoint body =
        let url = combineUri connection.ClusterUri endpoint 

        try 
            let result = Http.RequestString ( url, httpMethod = "POST",
                            headers = [ "Accept", "application/json" ],
                            body   = TextRequest body )

            if (connection.IsTrackEnabled) then
                Log.Logger.Information("POST body: {body} to clusterUri: {clusterUri} and endpoint: {endpoint}. Result: {result}",body,connection.ClusterUri,endpoint,result)

            result
        with 
        | ex -> 
            Log.Logger.Error("Error when executing POST. Body: {body} to clusterUri: {clusterUri} and endpoint: {endpoint}. Error: {Exception}",body,connection.ClusterUri,endpoint,ex)
            raise ex


    let POST (connection : Connection) endpoint body =
        let url = (combineUri connection.ClusterUri endpoint)

        try 
            let result = Http.RequestString ( url , httpMethod = "POST",body   = TextRequest body )

            if (connection.IsTrackEnabled) then
                Log.Logger.Information("POST body: {body} to clusterUri: {clusterUri} and endpoint: {endpoint}. Result: {result}",body,connection.ClusterUri,endpoint,result)

            result
        with
        | ex -> 
            Log.Logger.Error("Error when executing POST. Body: {body} to clusterUri: {clusterUri} and endpoint: {endpoint}. Error: {Exception}",body,connection.ClusterUri,endpoint,ex)
            raise ex


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

