[<CommandsHandlers>]
module ElasticOps.Com.ScanAndScrollSearch
open Json.AST.Parse
open Json.AST.Traverse
open ElasticOps.Com.CommonTypes
open ElasticOps.Com.ElasticResponseProcessing
open System.Collections.Generic
open FSharp.Data
open System
open System.Text.RegularExpressions
open FSharp.Data.JsonExtensions
open Newtonsoft.Json

[<AllowNullLiteral>]
type ScrollResult(documents,nextScrollId) = 
    member val documents = documents with get, set
    member val nextScrollId = nextScrollId with get,set

type ScrollSearchCommand(connection,scrollId, index,size) = 
    inherit Command<ScrollResult>(connection) 
    member val ScrollId = scrollId with get, set
    member val Size = size with get, set
    member val Index = index with get, set

let obtainScrollId json =
    json?_scroll_id.AsString()

//let getScrollResult propList =  


let getScrollPage clusterUri scrollId =
   let json = (POST clusterUri "/_search/scroll?scroll=5m" scrollId) 
   let res = parseNextObject (toCharList(json))

   let scrollid = find (fst(res)) "_scroll_id"
   let hits = find (fst(res)) "hits.hits"

   new ScrollResult("","")                 
    
let scrollSearch (command : ScrollSearchCommand) = 
    match command.ScrollId with 
        | null -> (GET command.ClusterUri (command.Index+"/_search?search_type=scan&scroll=5m")) 
                    |> JsonValue.Parse
                    |> obtainScrollId 
                    |> (getScrollPage  command.ClusterUri)
        | id -> (getScrollPage command.ClusterUri id)
