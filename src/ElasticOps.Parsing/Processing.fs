module ElasticOps.Parsing.Processing

open Microsoft.FSharp.Text.Lexing

let lexerReadParserToken lexbuf =
    let t = Lexer.read lexbuf
    match t with
    | Structures.token.COLON -> Parser.token.COLON
    | Structures.token.COMMA -> Parser.token.COMMA
    | Structures.token.EOF -> Parser.token.EOF
    | Structures.token.FALSE -> Parser.token.FALSE
    | Structures.token.FLOAT f -> Parser.token.FLOAT f
    | Structures.token.ID id-> Parser.token.ID id
    | Structures.token.INT i -> Parser.token.INT i
    | Structures.token.LEFT_BRACE -> Parser.token.LEFT_BRACE
    | Structures.token.LEFT_BRACK -> Parser.token.LEFT_BRACK
    | Structures.token.NULL -> Parser.token.NULL
    | Structures.token.RIGHT_BRACE -> Parser.token.RIGHT_BRACE
    | Structures.token.RIGHT_BRACK -> Parser.token.RIGHT_BRACK
    | Structures.token.STRING s -> Parser.token.STRING s
    | Structures.token.TRUE -> Parser.token.TRUE


let tokenizeAll (lexbuf : LexBuffer<char>) = 
    let rec tokenize (buf : LexBuffer<char>) (res : Structures.token list) = 
        match not buf.IsPastEndOfStream with
        | true -> 
            let token = Lexer.read buf
            tokenize buf (token::res)
        | _ -> List.rev res
    tokenize lexbuf []


let parse json = 
    let lexbuf = LexBuffer<char>.FromString json
    let res = Parser.start lexerReadParserToken lexbuf
    res
