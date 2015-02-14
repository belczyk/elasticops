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
    
    let parseIntellisense ison = 
        try 
            let lexbuf = LexBuffer<char>.FromString ison
            let res = IntellisenseParser.start IntellisenseLexer.read lexbuf
            res
        with 
        | _ -> None


    let formatTokens text lexerRead leveUpOnToken levelDownOnToken newLineOnToken printToken indent = 
        let tokens = tokenizeAll((LexBuffer<char>.FromString text),lexerRead)

        let rec format remTokens level acc = 
            match remTokens with
            | hd::tl -> if hd=leveUpOnToken then
                            format tl (level+1) (acc+printToken(hd)+"\n"+(String.replicate (level+1) indent))
                        else if hd = levelDownOnToken then
                            format tl (level-1) (acc+"\n"+(String.replicate (level-1) indent)+printToken(hd))
                        else if hd = newLineOnToken then
                            format tl level (acc+printToken(hd)+"\n"+(String.replicate level indent))
                        else
                            format tl level (acc+printToken(hd)+" ")
            | [] -> acc

        format tokens 0 ""
    let printIntellisenseToken t = 
        match t with 
        | IntellisenseParser.ANY_PATH -> "AnyPath"
        | IntellisenseParser.ANY_PROPERTY -> "AnyProperty"
        | IntellisenseParser.COLON -> ":"
        | IntellisenseParser.COMMA -> ","
        | IntellisenseParser.COMPLETION_MACRO macro -> macro
        | IntellisenseParser.EOF  -> ""
        | IntellisenseParser.ID id-> id
        | IntellisenseParser.LEFT_BRACE-> "{"
        | IntellisenseParser.LEFT_BRACK-> "["
        | IntellisenseParser.LEFT_PARENTHESIS-> "("
        | IntellisenseParser.ONE_OF -> "OneOf"
        | IntellisenseParser.RIGHT_BRACE  -> "}"
        | IntellisenseParser.RIGHT_BRACK  -> "]"
        | IntellisenseParser.RIGHT_PARENTHESIS  -> ")"
        | IntellisenseParser.STRING str -> "\""+str+"\""
        | IntellisenseParser.UNFINISHED_VALUE value -> fst value


    let formatIntellisense ison = 
        formatTokens ison IntellisenseLexer.read IntellisenseParser.LEFT_BRACE IntellisenseParser.RIGHT_BRACE IntellisenseParser.COMMA printIntellisenseToken "\t"

