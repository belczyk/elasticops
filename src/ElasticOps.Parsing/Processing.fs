module ElasticOps.Parsing.Processing

open Microsoft.FSharp.Text.Lexing
open Parser
open ElasticOps.Parsing.Structures
open System

let tokenizeAll (lexbuf : LexBuffer<char>) = 
    let rec tokenize (buf : LexBuffer<char>) (res : token list) = 
        match not buf.IsPastEndOfStream with
        | true -> 
            let token = Lexer.read buf
            tokenize buf (token::res)
        | _ -> List.rev res
    tokenize lexbuf []

let parse json = 
    try 
        let lexbuf = LexBuffer<char>.FromString json
        let res = Parser.start Lexer.read lexbuf
        res
    with 
    | _ -> None

let rec endsOnPropertyName (tree : JsonValue) = 
     match tree with 
        | Assoc props-> match props with 
                        | [] -> (false,String.Empty)
                        | _ -> match (Seq.last props) with
                                | (name, List els) -> endsOnPropertyName (Seq.last els)
                                | (name, Assoc props) -> endsOnPropertyName (Assoc props)
                                | (name, Missing) -> (true,name)
                                | _ -> (false,String.Empty)
        | List els -> endsOnPropertyName (Seq.last els)
        | _ -> (false,String.Empty)