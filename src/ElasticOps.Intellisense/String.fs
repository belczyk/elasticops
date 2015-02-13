[<AutoOpen>]
module Microsoft.FSharp.Core.String

    open System

    let split (splitStr : string) (str : string) = 
        str.Split([|splitStr|],StringSplitOptions.None)

    let substring (str : string) (line : int) col = 
        let allLines = str |> (split "\r\n") |> List.ofArray

        match (allLines,line) with
        | (l::[],_) -> str.Substring(0,col-1)
        | (lines,1) -> str.Substring(0,col-1)
        | (lines,_) -> let leadingLines = lines|> Seq.ofList |> Seq.take (line - 1) |> List.ofSeq
                       let lastLine = (Seq.nth (line - 1) lines).Substring(0,col-1)
                       (leadingLines |> String.concat "\r\n")+"\r\n"+lastLine

