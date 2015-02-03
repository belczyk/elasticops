namespace ElasticOps
    module Intellisense =

        open ElasticOps.Parsing
        open ElasticOps.Parsing.Structures
        open Microsoft.FSharp.Core
        
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
            NewCaretPosition : int * int;
            NewText : string
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
        
            (context, Some options)
        
        let complete (context : Context) = 
            match context.Mode with 
                | PropertyName prop -> completeProperty context prop
                | _ -> (context, Option.None)
        
        let TryComplete text caretLine caretColumn = 
            let codeTillCaret = String.substring text caretLine caretColumn
            let tree = Processing.parse codeTillCaret
        
            let context = {
                            OriginalCode = text; 
                            OriginalCaretPosition = (caretLine,caretColumn); 
                            CodeFromCaret = null; 
                            CodeTillCaret = codeTillCaret; 
                            ParseTree = tree; 
                            Mode = None;
                            NewCaretPosition = (caretLine, caretColumn) ;
                            NewText = null;
                          }
        
            let completitionMode = completitionMode context
            
        
            match completitionMode with
            | None -> (context,Option.None)
            | _ -> complete { context with Mode = completitionMode }

        let Complete (context : Context) (suggestion : Suggestion) = 
            match context.Mode with 
            | PropertyName _ -> 
                let codeFromCaret = context.OriginalCode.Substring(context.CodeTillCaret.Length)
                let lastQuotePos = context.CodeTillCaret.LastIndexOf "\""
                let codeTillQuote = context.CodeTillCaret.Substring(0,lastQuotePos+1)

                let suggestText = suggestion.Text + "\" : "
                let newCaretColumn = (Seq.last (codeTillQuote.Split('\n'))).LastIndexOf("\"")+suggestText.Length

                {context with 
                    CodeFromCaret = codeFromCaret; 
                    NewText = codeTillQuote + suggestText + codeFromCaret; 
                    NewCaretPosition = ((fst context.OriginalCaretPosition), newCaretColumn)
                }
            | _ -> failwith "Not supported"

            

            
