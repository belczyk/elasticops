namespace ElasticOps.Com
open System.Collections.Generic
open System
open FSharp.Data
open FSharp.Data.JsonExtensions
open Humanizer
open FSharp.Data.Runtime
open System.Linq
open Serilog
open System.IO
open System.Security.Cryptography
open System.Text

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
    let DELETE' = "DELETE"

    let combineUri ( uri : Uri) endpoint =
        match endpoint with 
        | null | "" -> uri.ToString()
        | _ -> let u = new Uri(uri,new Uri(endpoint,UriKind.Relative))
               u.ToString()
    
    let hash (str : String) =
        let sha = new SHA1CryptoServiceProvider(); 
        let bytes = Encoding.UTF8.GetBytes(str)
        let h = sha.ComputeHash(bytes)
        h  |> Seq.ofArray 
           |> Seq.map (fun x -> x.ToString("x2").ToUpper())
           |> String.Concat
    let endpointToDirName (endpoint : string) = 
        endpoint.Replace("?","_").Replace("\\","/").Replace("/", " ");
        
    let saveResult (verb : string,connection : Connection,endpoint : string,body : String option, result : string) =
        let endpointDir =endpointToDirName endpoint

        let dir = new DirectoryInfo(Path.Combine(connection.SavePath,connection.Version.ToString(), endpointDir, verb))
        if (not dir.Exists) then dir.Create()

        match (verb,body) with 
        | ("DELETE", _)
        | ("GET", _) -> 
            File.WriteAllText(Path.Combine(dir.FullName, "result.json"),result)
        | ("POST",Some body) -> 
            let h = hash body 
            File.WriteAllText(Path.Combine(dir.FullName, "body " + h + ".json"), body)
            File.WriteAllText(Path.Combine(dir.FullName, "result " + h + ".json"), result)
        | ("POST", None) ->
            File.WriteAllText(Path.Combine(dir.FullName, "resul.json"), result)
        | _ -> ignore()

    let OfflineGET (connection : Connection ) endpoint = 
        Path.Combine(connection.ReadPath, connection.DiskVersion.ToString(), (endpointToDirName endpoint),GET',"result.json")
            |> File.ReadAllText

    let OfflineDELETE (connection : Connection ) endpoint = 
        Path.Combine(connection.ReadPath, connection.DiskVersion.ToString(), (endpointToDirName endpoint),DELETE',"result.json")
            |> File.ReadAllText

    let OfflinePOST (connection : Connection ) endpoint body= 
        let h = hash body
        Path.Combine(connection.ReadPath, connection.DiskVersion.ToString(), (endpointToDirName endpoint),POST',"result " + h + ".json")
            |> File.ReadAllText

    let GET (connection : Connection) endpoint =
        if connection.IsOfflineMode then 
            OfflineGET connection endpoint
        else
            let url = combineUri connection.ClusterUri endpoint

            try 
                let result = Http.RequestString  url 

                if (connection.IsTrackEnabled) then
                    Log.Logger.Information("GET   clusterUri: {clusterUri}  and endpoint: {endpoint}.  Result:  {result}",connection.ClusterUri,endpoint,result)
                if (connection.SaveResultToDisk) then
                    saveResult(GET',connection,endpoint,None,result)
                result
            with
            | ex -> 
                Log.Logger.Error("Error when executing GET. ClusterUri: {clusterUri} and endpoint: {endpoint}. Error: {Exception}",connection.ClusterUri,endpoint,ex)
                reraise()

    let DELETE (connection : Connection) endpoint =
        if connection.IsOfflineMode then 
            OfflineDELETE connection endpoint
        else
            let url = combineUri connection.ClusterUri endpoint

            try 
                let result = Http.RequestString(url ,httpMethod= DELETE')

                if (connection.IsTrackEnabled) then
                    Log.Logger.Information("DELETE   clusterUri: {clusterUri}  and endpoint: {endpoint}.  Result:  {result}",connection.ClusterUri,endpoint,result)
                if (connection.SaveResultToDisk) then
                    saveResult(DELETE',connection,endpoint,None,result)
                result
            with
            | ex -> 
                Log.Logger.Error("Error when executing DELETE. ClusterUri: {clusterUri} and endpoint: {endpoint}. Error: {Exception}",connection.ClusterUri,endpoint,ex)
                reraise()

    let POSTJson (connection : Connection) endpoint body =
        if connection.IsOfflineMode then 
            OfflinePOST connection endpoint body
        else
            let url = combineUri connection.ClusterUri endpoint 

            try 
                let result = Http.RequestString ( url, httpMethod = "POST",
                                headers = [ "Accept", "application/json" ],
                                body   = TextRequest body )

                if (connection.IsTrackEnabled) then
                    Log.Logger.Information("POST body: {body} to clusterUri: {clusterUri} and endpoint: {endpoint}. Result: {result}",body,connection.ClusterUri,endpoint,result)
                if (connection.SaveResultToDisk) then
                    saveResult(POST',connection,endpoint,Some body,result)
                result
            with 
            | ex -> 
                Log.Logger.Error("Error when executing POST. Body: {body} to clusterUri: {clusterUri} and endpoint: {endpoint}. Error: {Exception}",body,connection.ClusterUri,endpoint,ex)
                reraise()


    let POST (connection : Connection) endpoint body =
        if connection.IsOfflineMode then 
            OfflinePOST connection endpoint body
        else
            let url = (combineUri connection.ClusterUri endpoint)

            try 
                let result = Http.RequestString ( url , httpMethod = "POST",body   = TextRequest body )

                if (connection.IsTrackEnabled) then
                    Log.Logger.Information("POST body: {body} to clusterUri: {clusterUri} and endpoint: {endpoint}. Result: {result}",body,connection.ClusterUri,endpoint,result)
                if (connection.SaveResultToDisk) then
                    saveResult(POST',connection,endpoint,Some body,result)
                result
            with
            | ex -> 
                Log.Logger.Error("Error when executing POST. Body: {body} to clusterUri: {clusterUri} and endpoint: {endpoint}. Error: {Exception}",body,connection.ClusterUri,endpoint,ex)
                reraise()


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
        


