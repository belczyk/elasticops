[<CommandsHandlers>]
module ElasticOps.Com.ScanAndScrollSearch
open ElasticOps.Com.CommonTypes
open System.Collections.Generic
open ElasticOps.Com.ElasticResponseProcessing
open FSharp.Data
open FSharp.Data.JsonExtensions

[<AllowNullLiteral>]
type ScrollResult(documents,nextScrollId) = 
    member val documents = documents with get, set
    member val nextScrollId = nextScrollId with get,set

type ScrollSearchCommand(connection,scrollId, index,size) = 
    inherit Command<ScrollResult>(connection) 
    member val ScrollId = scrollId with get, set
    member val Size = size with get, set
    member val Index = index with get, set


let ObtainScrollId json =
    json?_scroll_id.AsString()

let GetScrollPage clusterUri scrollId =
    let res = (POST clusterUri "/_search/scroll?scroll=5m" scrollId)
                    |> JsonValue.Parse 
                    |> (fun res -> res?hits)

    (new ScrollResult("",""))


let ScrollSearch (command : ScrollSearchCommand) = 
    match command.ScrollId with 
        | null -> (GET command.ClusterUri (command.Index+"/_search?search_type=scan&scroll=5m")) 
                    |> JsonValue.Parse
                    |> ObtainScrollId 
                    |> (GetScrollPage  command.ClusterUri)
        | id -> (GetScrollPage command.ClusterUri id)
