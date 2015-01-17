module ElasticOps.Parsing.Processing

open Microsoft.FSharp.Text.Lexing
open ElasticOps.Parsing.Structures

let tokenizeAll (lexbuf : LexBuffer<char>) = 
    let rec tokenize (buf : LexBuffer<char>) (res : Tokens list) = 
        match not buf.IsPastEndOfStream with
        | true -> 
            let token = Lexer.tokenize buf
            tokenize buf (token::res)
        | _ -> List.rev res
    tokenize lexbuf []


type Mode = | Object | Property | Array 


let parseValue token =
    match token with
    | Tokens.BOOLEAN(b) -> if b then Values.Bool(true) else Values.Bool(false) 
    | Tokens.DECIMAL(d) -> Values.Decimal(d)
    | Tokens.INT(i) -> Values.Int(i)
    | Tokens.STRING(s) -> Values.String(s)


let parseProperty tokens = 
    match tokens with 
    | Tokens.STRING(s)::Tokens.SEMICOLON::value::tail ->
                (tail,{Name = s; Value = (parseValue(value))})


let parse tokens =
    let rec parseStep  ts tree mode =
        match (ts,mode) with 
        | ([],[]) | (_,[]) | ([],_) -> tree
        | _ ->  match ((List.head ts), (List.head mode)) with
                | (Tokens.STRING(s),Mode.Object) -> let (ts',prop) = parseProperty ts
                                                    parseStep ts' (Assoc([prop])) mode

                | (Tokens.COMMA, Mode.Object) -> let (ts',prop) = parseProperty (List.tail ts)
                                                 let (Assoc props) = tree
                                                 parseStep ts' (Assoc(prop::props)) mode

                | (Tokens.R_C_BRAC, Mode.Object) -> parseStep (List.tail ts) tree (List.tail mode)
                | _ -> failwith "error"

    parseStep (List.tail tokens) (Assoc([])) [Mode.Object]


let parseJson json = 
        json 
        |> LexBuffer<_>.FromString
        |> tokenizeAll
        |> parse
