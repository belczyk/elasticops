module ElasticOps.Intellisense
open ElasticOps.Parsing
open ElasticOps.Parsing.Structures

module StringProcessing = 
    let stringTillPos (str : string) line col = 
        match str.Split('\n') |> List.ofArray with
        | l::[] -> str.Substring(0,col-1)
        | lines -> let leadingLines = lines|> Seq.ofList |> Seq.take (lines.Length - 1) |> List.ofSeq
                   (leadingLines |> String.concat "\n")+(Seq.last lines).Substring(0,col-1)

type Mode =
    | PropertyName of string
    | Value
    | Snippet
    | None

type Suggestion = {
    Text : string;
    Mode : Mode;
}

type Context = {
    OriginalCode : string;
    CodeTillCaret : string;
    CodeFromCaret : string;
    ParseTree : JsonValue option;
    Mode : Mode;
    OriginalCaretPosition : int * int;
    NewCaretPosition : int * int
    Suggestions : Suggestion list
}


let completitionMode (context : Context) =
    match context.ParseTree with 
    | Some t -> 
        let endsWithPropName = Processing.endsOnPropertyName t

        match endsWithPropName with
        | (true, name) -> if (System.String.IsNullOrEmpty(name) || (not (context.CodeTillCaret.Trim().EndsWith("\"")))) then
                             (PropertyName(name)) 
                          else 
                            None
        | _ -> None
    | _ -> None


let completeProperty context prefix = 
    let suggestions = ["query"; "query_match_all"; "aggregation" ; "filter" ]

    let options = suggestions |> List.filter (fun x -> x.StartsWith(prefix)) |> List.map (fun x-> {Text = x; Mode = Mode.PropertyName x})

    { context with Suggestions = options}

let complete (context : Context) = 
    match context.Mode with 
        | PropertyName prop -> completeProperty context prop
        | _ -> context

let TryComplete text caretLine caretColumn = 
    let codeTillCaret = StringProcessing.stringTillPos text caretLine caretColumn
    let tree = Processing.parse codeTillCaret

    let context = {
                    OriginalCode = text; 
                    OriginalCaretPosition = (caretLine,caretColumn); 
                    CodeFromCaret = null; 
                    CodeTillCaret = codeTillCaret; 
                    ParseTree = tree; 
                    Mode = None;
                    NewCaretPosition = (caretLine, caretColumn) ;
                    Suggestions = []
                  }

    let completitionMode = completitionMode context
    

    match completitionMode with
    | None -> context
    | _ -> complete { context with Mode = completitionMode }


