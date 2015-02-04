namespace ElasticOps
    module Intellisense =

        open ElasticOps.Parsing
        open ElasticOps.Parsing.Structures
        open Microsoft.FSharp.Core
        
        type Mode =
            | PropertyName of string
            | Value
            | Snippet

        type Suggestion = {
            Text : string;
            Mode : Mode;
        }
        
        type Context = {
            OriginalCode : string;
            CodeTillCaret : string;
            CodeFromCaret : string;
            ParseTree : JsonValue option;
            OriginalCaretPosition : int * int;
            NewCaretPosition : int * int;
            NewText : string
        }

        type DSLPathNodes =
        | Object
        | Property of string 
        | Array 
        | Value of JsonValue


        let getDLSPath (context : Context) = 
            let rec findPath tree acc =
                match tree with
                | JsonValue.Assoc props -> 
                                            let lastProp = Seq.last props 
                                            let propName = fst lastProp 
                                            let value = snd lastProp 

                                            findPath value (Object::Property(propName)::acc)
                | JsonValue.List elements -> let lastElem = Seq.last elements 
                                             findPath lastElem (Array::acc)
                | _ -> failwith "Unsupported"

            match context.ParseTree with 
            | None -> None
            | Some t -> findPath t []
                    


        let suggestProperty context = 
            let propertyName = match context.ParseTree with 
                                        | Some t -> 
                                            let endsWithPropName = Processing.endsOnPropertyName t
        
                                            match endsWithPropName with
                                            | (true, name) -> if (System.String.IsNullOrEmpty(name) || (not (context.CodeTillCaret.Trim().EndsWith("\"")))) then
                                                                 (Some name) 
                                                              else 
                                                                None
                                            | _ -> None
                                        | _ -> None

            match propertyName with 
            | None -> (context, None)
            | Some prefix -> 
                    let suggestions = ["query"; "query_match_all"; "aggregation" ; "filter" ]
        
                    let options = suggestions |> List.filter (fun x -> x.StartsWith(prefix)) |> List.map (fun x-> {Text = x; Mode = Mode.PropertyName x})
        
                    (context, Some options)
        
        let  suggest (context : Context) = 
            (suggestProperty context)
            
        
        let TrySuggest text caretLine caretColumn = 
            let codeTillCaret = String.substring text caretLine caretColumn
            let tree = Processing.parse codeTillCaret
            
            let context = {
                            OriginalCode = text; 
                            OriginalCaretPosition = (caretLine,caretColumn); 
                            CodeFromCaret = null; 
                            CodeTillCaret = codeTillCaret; 
                            ParseTree = tree; 
                            NewCaretPosition = (caretLine, caretColumn) ;
                            NewText = null;
                          }
        
            (suggest context)

        let completeProperty context suggestion = 
            let codeFromCaret = context.OriginalCode.Substring(context.CodeTillCaret.Length)
            let lastQuotePos = context.CodeTillCaret.LastIndexOf "\""
            let codeTillQuote = context.CodeTillCaret.Substring(0,lastQuotePos+1)

            let suggestText = suggestion.Text + "\" : "
            let lastLineTillCaret = ((String.split "\r\n" context.CodeTillCaret ) |> Seq.last);
            let indexOfLastQuoteInLastLineTillCaret = lastLineTillCaret.LastIndexOf("\"")

            let newCaretColumn =((fst context.OriginalCaretPosition),indexOfLastQuoteInLastLineTillCaret+suggestText.Length+2) // (Seq.last (String.split codeTillQuote "\r\n")).LastIndexOf("\"")+suggestText.Length

            {context with 
                CodeFromCaret = codeFromCaret; 
                NewText = codeTillQuote + suggestText + codeFromCaret; 
                NewCaretPosition = newCaretColumn;
            }

        let Complete (context : Context) (suggestion : Suggestion) = 
            match suggestion.Mode with 
            | PropertyName _ -> completeProperty context suggestion
            | _ -> failwith "Not supported"
