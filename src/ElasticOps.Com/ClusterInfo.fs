[<CommandsHanlders>]
module ElasticOps.Com.ClusterInfo
    open ElasticOps.Com.Models
    open FSharp.Data
    open FSharp.Data.JsonExtensions
    open System.Collections.Generic
    open System
    open ElasticOps.Com.ElasticResponseProcessing
    open ElasticOps.Com
    open ElasticOps.Com.CommonTypes

    [<ESVersionFrom(1)>]
    let Health (uri) =
        request uri "/_cluster/health"
        |> JsonValue.Parse
        |> asPropertyList
        |> Seq.map humanizeKeys
        |> asKeyValuePairList

    let ClusterCounters uri =
        let stats = request uri "_stats"
   
        let ic = stats
                   |> propCount (fun ps -> ps?indices)
               
        let docCount = stats
                        |> JsonValue.Parse
                        |> fun ps -> ps?_all?total?docs?count.AsInteger()

        let nodesCount = request uri "/_nodes"
                            |> propCount (fun p->p)


        { Indices = ic; Documents = docCount; Nodes = nodesCount }



    let NodesInfo (connection : Connection) =
        request connection.clusterUri "/_nodes"
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
            |> List.ofSeq


    let IndicesInfo uri =
        let state = request uri "/_cluster/state"
                        |> JsonValue.Parse
                        |> fun el -> el?metadata?indices
     
        state 
            |> asPropertyList 
            |> Seq.map fst
            |> Seq.map (fun indexName -> 
                       let mappings = state.[indexName]?mappings 
                                         |> asPropertyList
                                         |> Seq.map (fun map -> 
                                             new KeyValuePair<string,string>((fst map),(snd map).InnerText))
                                         |> List.ofSeq

                       let settings = state.[indexName]?settings?index 
                                         |> asPropertyListOfScalars 
                                         |> Seq.map humanizeKeys
                                         |> asKeyValuePairList 
                                         |> List.ofSeq

                       { Name = indexName; Types = mappings; Settings = settings; State = state.[indexName]?state.AsString()}
            )

    let IsAlive uri = 
        let response = Http.Request (combineUri uri "/_cluster/health")
        response.StatusCode = 200