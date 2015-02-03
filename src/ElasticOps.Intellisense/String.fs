[<AutoOpen>]
module Microsoft.FSharp.Core.String

let substring (str : string) (line : int) col = 
    match str.Replace("\r\n","\n").Replace("\r","\n").Split('\n') |> List.ofArray with
    | l::[] -> str.Substring(0,col-1)
    | lines -> let leadingLines = lines|> Seq.ofList |> Seq.take (line - 1) |> List.ofSeq
               let lastLine = (Seq.nth (line - 1) lines)
               (leadingLines |> String.concat "\n")+lastLine

