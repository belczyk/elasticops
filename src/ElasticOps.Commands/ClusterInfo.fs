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
        let res =GET command.ClusterUri "/_cluster/health"
                    |> JsonValue.Parse
                    |> asPropertyList
                    |> Seq.map humanizeKeys
                    |> Seq.map extractStringsFromValues
        dict res
        
    type ClusterCountersCommand(connection) = 
        inherit Command<ClusterCounters>(connection)

    let ClusterCounters (command : ClusterCountersCommand) =
        let stats = GET command.ClusterUri "_stats"
   
        let ic1 = stats
                    |> JsonValue.Parse
                    |> (fun ps -> ps?indices)
                    |> asPropertyList

        let ic = ic1
                    |> Seq.filter (fun i -> (fst i).StartsWith("."))
                    |> Seq.length
               
        let docCount = stats
                        |> JsonValue.Parse
                        |> fun ps -> ps?_all?total?docs?count.AsInteger()

        let nodesCount = GET command.ClusterUri "/_nodes"
                            |> propCount (fun p->p)

        { Indices = ic; Documents = docCount; Nodes = nodesCount }

    type NodesInfoCommand(connection) = 
        inherit Command<IEnumerable<NodeInfo>>(connection)

    [<ESVersionFrom(1,0,0,0)>]
    let NodesInfo (request : NodesInfoCommand) =
        GET request.ClusterUri "/_nodes"
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
        let state = GET command.ClusterUri "/_cluster/state"
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
        let request = combineUri command.ClusterUri "_search"
        (POSTJson command.ClusterUri "_search" """  {"query": {"match_all": {}}, "facets": { "types": { "terms": { "field": "_type", "size": 100 }}}}  """)
            |> JsonValue.Parse
            |> fun el -> el?facets?types?terms
            |> fun el -> el.AsArray()
            |> Seq.map (fun el -> (el?term.AsString(), el?count.AsString()))
            |> dict

    type IsAliveCommand(connection) = 
        inherit Command<HeartBeat>(connection)

    let IsAlive uri = 
        try 
            let response = JsonValue.Parse (GET uri null) 
            match response?status.AsString() with
            | "200" -> new HeartBeat(true,response?version?number.AsString())
            | _ -> new HeartBeat(false)
        with
        | ex -> new HeartBeat(false)

    type ListIndicesCommand(connection) =
        inherit Command<IEnumerable<String>>(connection)

    let ListIndices (command: ListIndicesCommand) =
        GET command.ClusterUri "/_cluster/state"
                        |> JsonValue.Parse
                        |> fun el -> el?metadata?indices
                        |> asPropertyList
                        |> Seq.map fst
                        |> CList.ofSeq

    type ListTypesCommand(connection, indexName) = 
        inherit Command<List<string>>(connection)
        member val IndexName = indexName with get,set

    let ListTypes (command: ListTypesCommand) = 
        GET command.ClusterUri (command.IndexName+"/_mapping")
                        |> JsonValue.Parse
                        |> fun el -> el.[command.IndexName]?mappings
                        |> asPropertyList
                        |> Seq.map (fun el -> fst el)
                        |> CList.ofSeq

    type GetMappingCommand(connection, indexName, typeName) = 
        inherit Command<string>(connection)
        member val IndexName = indexName
        member val TypeName = typeName

    let GetMapping(command: GetMappingCommand, client : IRESTClient) = 
        client.GET (command.ClusterUri,("/"+command.IndexName+"/"+command.TypeName+"/_mapping?pretty"))


