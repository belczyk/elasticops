namespace ElasticOps.Com

[<AutoOpen>]
[<RequireQualifiedAccess>]
module Log =
    open Logary
    open System
    let mapOfPairsList data = 
        data 
            |> List.map (fun (el) -> (fst el, (snd el) :> Object))
            |> Map.ofList

    let info msg data =
        LogLine.create msg (mapOfPairsList data) LogLevel.Info [] "" None

    let error msg data ex =
        LogLine.create msg (mapOfPairsList data) LogLevel.Error [] "" (Some ex)

    let verbose msg data = 
        LogLine.create msg (mapOfPairsList data) LogLevel.Verbose [] "" None 
