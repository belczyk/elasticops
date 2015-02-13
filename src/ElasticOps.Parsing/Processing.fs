module ElasticOps.Parsing.Processing

    open Microsoft.FSharp.Text.Lexing
    open ElasticOps.Parsing.JsonParser
    open ElasticOps.Parsing.IntellisenseParser
    open System
    
    let tokenizeAll (lexbuf : LexBuffer<char>, lexingFun) = 
        let rec tokenize (buf : LexBuffer<char>) (res : token list) = 
            match not buf.IsPastEndOfStream with
            | true -> 
                let token = lexingFun buf
                tokenize buf (token::res)
            | _ -> List.rev res
        tokenize lexbuf []
    
    let parse json = 
        try 
            let lexbuf = LexBuffer<char>.FromString json
            let res = JsonParser.start JsonLexer.read lexbuf
            res
        with 
        | _ -> None
    
    let rec endsOnPropertyName (tree : JsonValue) = 
         match tree with 
            | JsonValue.Assoc props-> match props with 
                                        | [] -> (false,String.Empty)
                                        | _ -> match (Seq.last props) with
                                                | PropertyWithValue(_,Array els) -> endsOnPropertyName (Seq.last els)
                                                | PropertyWithValue(_,JsonValue.Assoc props) -> endsOnPropertyName (JsonValue.Assoc props)
                                                | PropertyNameWithColon name -> (true,name)
                                                | PropertyName name -> (true,name)
                                                | UnfinishedPropertyName name -> (true,name)
                                                | _ -> (false,String.Empty)
            | Array els -> endsOnPropertyName (Seq.last els)
            | _ -> (false,String.Empty)
    
    let parseIntellisense json = 
        try 
            let lexbuf = LexBuffer<char>.FromString json
            let res = IntellisenseParser.start IntellisenseLexer.read lexbuf
            res
        with 
        | _ -> None