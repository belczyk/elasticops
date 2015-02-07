[<CommandsHandlers>]
module ElasticOps.Com.ClusterInfo
    open ElasticOps.Com
    open FSharp.Data
    open FSharp.Data.JsonExtensions
    open System.Collections.Generic
    open System

    type HealthCommand(connection) = 
        inherit Command<IDictionary<string,string>>(connection)
    
    let Health (command : HealthCommand) =
        let res =GET command.Connection "/_cluster/health"
                    |> JsonValue.Parse
                    |> asPropertyList
                    |> Seq.map humanizeKeys
                    |> Seq.map extractStringsFromValues
        dict res
        
    type ClusterCountersCommand(connection) = 
        inherit Command<ClusterCounters>(connection)

    [<ESVersionTo(0,90,13,0)>]
    let ClusterCountersToV1 (command : ClusterCountersCommand) =
        let stats = GET command.Connection "_stats"
   
        let ic1 = stats
                    |> JsonValue.Parse
                    |> (fun ps -> ps?indices)
                    |> asPropertyList

        let ic = ic1
                    |> Seq.filter (fun i -> not ((fst i).StartsWith(".")))
                    |> Seq.length
               
        let docCount = stats
                        |> JsonValue.Parse
                        |> fun ps -> match  ps?_all?primaries.TryGetProperty("docs") with
                                        | None -> 0
                                        | Some d -> d?count.AsInteger()
        let nodesJson = GET command.Connection "/_nodes"
        let nodesCount = (JsonValue.Parse nodesJson)?nodes |> asPropertyList |> Seq.length


        new ClusterCounters(ic,docCount,nodesCount)

    [<ESVersionFrom(1)>]
    let ClusterCounters (command : ClusterCountersCommand) =
        let stats = GET command.Connection "_stats"
   
        let ic1 = stats
                    |> JsonValue.Parse
                    |> (fun ps -> ps?indices)
                    |> asPropertyList

        let ic = ic1
                    |> Seq.filter (fun i -> (not ((fst i).StartsWith("."))))
                    |> Seq.length
               
        let docCount = stats
                        |> JsonValue.Parse
                        |> fun ps -> match  ps?_all?total.TryGetProperty("docs") with
                                        | None -> 0
                                        | Some d -> d?count.AsInteger()
        let nodesJson = GET command.Connection "/_nodes"
        let nodesCount = (JsonValue.Parse nodesJson)?nodes |> asPropertyList |> Seq.length

        new ClusterCounters(ic,docCount,nodesCount)

    type NodesInfoCommand(connection) = 
        inherit Command<IEnumerable<NodeInfo>>(connection)

    [<ESVersionTo(0,90,13,0)>]
    let NodesInfoToV1 (request : NodesInfoCommand) =
        GET request.Connection "/_nodes"
                    |> JsonValue.Parse
                    |> fun ps -> ps?nodes
                    |> asPropertyList
                    |> Seq.map snd 
                    |> Seq.map (fun el -> 
                         { 
                            Name = el?name.AsString(); 
                            Hostname = el?hostname.AsString(); 
                            HttpAddress = el?http_address.AsString();
                            CPU = [];
                            Settings = [];
                            OS = [];
                        })
                    |> CList.ofSeq

    [<ESVersionFrom(1,0,0,0)>]
    let NodesInfo (request : NodesInfoCommand) =
        GET request.Connection "/_nodes"
                    |> JsonValue.Parse
                    |> fun ps -> ps?nodes
                    |> asPropertyList
                    |> Seq.map snd 
                    |> Seq.map (fun el -> 
                         { 
                            Name = el?name.AsString(); 
                            Hostname = el?host.AsString(); 
                            HttpAddress = el?http_address.AsString();
                            CPU = el?os |> (fun e -> 
                                match e.TryGetProperty("cpu") with
                                | None -> JsonValue.Parse "{}"
                                | Some cpu -> cpu
                            ) |> asPropertyList |> asKeyValuePairList;
                            Settings = el?settings?path |> asPropertyList |> asKeyValuePairList;
                            OS = el?os |> asPropertyListOfScalars |> Seq.map humanizeKeys |> asKeyValuePairList;
                        })
                    |> CList.ofSeq
 
    type IndicesInfoCommand(connection) = 
        inherit Command<IEnumerable<IndexInfo>>(connection)

    let IndicesInfo (command : IndicesInfoCommand) =
        let state = GET command.Connection "/_cluster/state"
                        |> JsonValue.Parse
                        |> fun el -> el?metadata?indices
        state 
            |> asPropertyList 
            |> Seq.map fst
            |> Seq.map (fun indexName -> 
                       let mappings = state.[indexName]?mappings 
                                         |> asPropertyList
                                         |> Seq.map (fun map -> 
                                             let sec = (snd map);
                                             new KeyValuePair<string,string>((fst map), (snd map)?properties.ToString())
                                             )
                                         |> CList.ofSeq

                       let settings = state.[indexName]?settings?index 
                                         |> asPropertyListOfScalars 
                                         |> Seq.map humanizeKeys
                                         |> asKeyValuePairList 
                                         |> CList.ofSeq

                       { Name = indexName; Types = mappings; Settings = settings; State = state.[indexName]?state.AsString()}
            )

    type DocumentsInfoCommand(connection) = 
        inherit Command<IDictionary<string,string>>(connection)

    let DocumentsInfo (command : DocumentsInfoCommand) =
        let elelemnt = (POSTJson command.Connection "_search" """  {"query": {"match_all": {}}, "facets": { "types": { "terms": { "field": "_type", "size": 100 }}}}  """)
                    |> JsonValue.Parse

        match elelemnt.TryGetProperty("facets") with
                    | None -> new Dictionary<String,String>() :> IDictionary<String,String>
                    | Some el -> el
                                |> fun el -> el?types?terms
                                |> fun el -> el.AsArray()
                                |> Seq.map (fun el -> (el?term.AsString(), el?count.AsString()))
                                |> dict

    type ListIndicesCommand(connection) =
        inherit Command<IEnumerable<String>>(connection)

    let ListIndices (command: ListIndicesCommand) =
        GET command.Connection "/_cluster/state"
                        |> JsonValue.Parse
                        |> fun el -> el?metadata?indices
                        |> asPropertyList
                        |> Seq.map fst
                        |> CList.ofSeq

    type ListTypesCommand(connection, indexName) = 
        inherit Command<List<string>>(connection)
        member val IndexName = indexName with get,set

    [<ESVersionFrom(1)>]
    let ListTypes (command: ListTypesCommand) = 
        GET command.Connection (command.IndexName+"/_mapping")
                        |> JsonValue.Parse
                        |> fun el -> el.[command.IndexName]?mappings
                        |> asPropertyList
                        |> Seq.map (fun el -> fst el)
                        |> CList.ofSeq

    [<ESVersionTo(0,90,13,0)>]
    let ListTypesToV1 (command: ListTypesCommand) = 
        GET command.Connection (command.IndexName+"/_mapping")
                        |> JsonValue.Parse
                        |> fun el -> el.[command.IndexName]
                        |> asPropertyList
                        |> Seq.map (fun el -> fst el)
                        |> CList.ofSeq

    type GetMappingCommand(connection, indexName, typeName) = 
        inherit Command<string>(connection)
        member val IndexName = indexName
        member val TypeName = typeName

    let GetMapping(command: GetMappingCommand) = 
        GET command.Connection ("/"+command.IndexName+"/"+command.TypeName+"/_mapping?pretty")


