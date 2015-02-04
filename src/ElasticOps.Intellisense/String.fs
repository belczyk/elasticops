[<AutoOpen>]
module Microsoft.FSharp.Core.String
open System

let split (splitStr : string) (str : string) = 
    str.Split([|splitStr|],StringSplitOptions.None)

let substring (str : string) (line : int) col = 
    match str |> (split "\r\n") |> List.ofArray with
    | l::[] -> str.Substring(0,col-1)
    | lines -> let leadingLines = lines|> Seq.ofList |> Seq.take (line - 1) |> List.ofSeq
               let lastLine = (Seq.nth (line - 1) lines)
               (leadingLines |> String.concat "\r\n")+lastLine

