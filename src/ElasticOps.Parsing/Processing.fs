module ElasticOps.Parsing.Processing

open Microsoft.FSharp.Text.Lexing
open Parser
open ElasticOps.Parsing.Structures

let tokenizeAll (lexbuf : LexBuffer<char>) = 
    let rec tokenize (buf : LexBuffer<char>) (res : token list) = 
        match not buf.IsPastEndOfStream with
        | true -> 
            let token = Lexer.read buf
            tokenize buf (token::res)
        | _ -> List.rev res
    tokenize lexbuf []

let parse json = 
    let lexbuf = LexBuffer<char>.FromString json
    let res = Parser.start Lexer.read lexbuf
    res

let rec endsOnPropertyName (tree : JsonValue)= 
    match tree with 
    | Assoc props-> match props with 
                    | [] -> false
                    | _ -> match (Seq.last props) with
                            | (name, Missing) -> true
                            | _ -> false
    | List els -> endsOnPropertyName (Seq.last els)
    | _ -> false