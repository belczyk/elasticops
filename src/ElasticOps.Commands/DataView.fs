[<CommandsHandlers>]
module ElasticOps.Commands.DataView

    open ElasticOps.Commands
    open System.Collections.Generic
    open FSharp.Data
    open FSharp.Data.JsonExtensions

    type Page = { Hits: int; Documents : IEnumerable<JsonValue>} 
    
    type PageCommand(connection : Connection, index : string, typeName : string, pageSize : System.Int64, page : System.Int64 ) =
        inherit Command<Page>(connection)
        member val Index = index with get,set
        member val Type = typeName with get,set
        member val PageSize = pageSize with get,set
        member val Page = page with get,set

    let page (command : PageCommand) = 
        let query =  @"{{
                      ""from"": {0},
                      ""size"": {1},
                      ""query"": {{
                        ""match_all"": {{}}
                      }}
                    }}"
        let res = POST command.Connection (command.Index+"/"+command.Type+"/_search") (System.String.Format(query, (command.PageSize*(command.Page-1L)), command.PageSize))
                    |> JsonValue.Parse

        let hits = res?hits?total.AsInteger()

        let docs = res?hits?hits.AsArray()
                    |> List.ofArray
                    |> List.map (fun x -> x?_source)
                    |> CList.ofList

        {Hits = hits; Documents = docs}