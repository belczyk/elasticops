[<CommandsHandlers>]
module ElasticOps.Com.ClusterInfo
    open ElasticOps.Com.Models
    open FSharp.Data
    open FSharp.Data.JsonExtensions
    open System.Collections.Generic
    open System
    open ElasticOps.Com.ElasticResponseProcessing
    open ElasticOps.Com
    open ElasticOps.Com.CommonTypes
    open System.Linq


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
   
        let ic = stats
                   |> propCount (fun ps -> ps?indices)
               
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
                            CPU = el?os?cpu |> asPropertyList |> asKeyValuePairList;
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
                                             let prop = sec?properties;
                                             let text = sec?properties.ToString();
                                             let t = text.GetType();
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
        inherit Command<List<string>>(connection)

    let ListIndices (command: ListIndicesCommand) =
        IndicesInfo(new IndicesInfoCommand(command.Connection))
        |> Seq.map (fun i -> i.Name)
        |> CList.ofSeq

    type ListTypesCommand(connection) = 
        inherit Command<List<string>>(connection)
        member val IndexName = "" with get,set

    let ListTypes (command: ListTypesCommand) = 
        GET command.ClusterUri (command.IndexName+"/_mapping")
                        |> JsonValue.Parse
                        |> fun el -> el.[command.IndexName]?mappings
                        |> asPropertyList
                        |> Seq.map (fun el -> fst el)
                        |> CList.ofSeq


