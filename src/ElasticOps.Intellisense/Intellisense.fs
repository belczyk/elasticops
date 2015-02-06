namespace ElasticOps
    module Intellisense =

        open ElasticOps.Parsing
        open Microsoft.FSharp.Core
        open ElasticOps

        let suggestProperty context = 
            let propertyName =  match context.ParseTree with 
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
                    let suggestions = 
                        SuggestEngine.matchSuggestions (Option.get context.ParseTree)
                        |> List.filter (fun s -> match s.Mode with 
                                                    | Mode.Property when s.Text.ToLower().StartsWith (prefix.ToLower()) -> true 
                                                    | Mode.Property -> false
                                                    | _ -> true)
                    (context, Some suggestions)
        
        let  suggest (context : Context) = 
            (suggestProperty context)
            
        
        let TrySuggest text caretLine caretColumn = 
            let context = Context.create text caretLine caretColumn
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
            | Mode.Property -> completeProperty context suggestion
            | _ -> failwith "Not supported"
