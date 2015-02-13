[<AutoOpen>]
module ElasticOps.Commands.Utils
    open FSharp.Data
    open FSharp.Data.JsonExtensions
    open FSharp.Data.Runtime
    open Humanizer
    open System.Collections.Generic 

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