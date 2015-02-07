namespace ElasticOps.Com
    open ElasticOps.Com
    open FSharp.Data
    open FSharp.Data.JsonExtensions
    open System.Collections.Generic
    open System

    type AnalyzedToken = {Token : string; StartOffset : int; EndOffset : int; Position: int; Type : string}

    [<CommandsHandlers>]
    module Analyze =

        type AnalyzeCommand(connection : Connection, analyzer : string, text : string) =
            inherit Command<IEnumerable<AnalyzedToken>>(connection)
            member val Text = text with get,set
            member val Analyzer = analyzer with get,set

        type AnalyzeWithIndexAnalyzerCommand(connection : Connection, index : string, analyzer : string, text : string) =
            inherit Command<IEnumerable<AnalyzedToken>>(connection)
            member val Text = text with get,set
            member val Analyzer = analyzer with get,set
            member val Index = index with get,set

        type AnalyzeWithFieldAnalyzerCommand(connection : Connection, index : string, field : string, text : string) =
            inherit Command<IEnumerable<AnalyzedToken>>(connection)
            member val Text = text with get,set
            member val Field = field with get,set
            member val Index = index with get,set

        let extractTokens tokens = 
            tokens|> JsonValue.Parse 
                  |> (fun x -> x?tokens.AsArray())
                  |> List.ofArray 
                  |> List.map (fun t -> {Token = t?token.AsString(); 
                                                StartOffset = t?start_offset.AsInteger(); 
                                                EndOffset = t?end_offset.AsInteger(); 
                                                Position = t?position.AsInteger(); 
                                                Type = t?``type``.AsString() })
                        |> CList.ofList

        let Analyze (command : AnalyzeCommand) =
             let res = POST command.Connection ("_analyze?analyzer="+command.Analyzer) command.Text
                            |> extractTokens
             res

        let AnalyzeWithIndexAnalyzer (command : AnalyzeWithIndexAnalyzerCommand) = 
            let res = POST command.Connection (command.Index+"/_analyze?analyzer="+command.Analyzer) command.Text
                        |> extractTokens
            res

        let AnalyzeWithFieldAnalyzer (command : AnalyzeWithFieldAnalyzerCommand) = 
            let res = POST command.Connection (command.Index+"/_analyze?field="+command.Field) command.Text
                        |> extractTokens
            res