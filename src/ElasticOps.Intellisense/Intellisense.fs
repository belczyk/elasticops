namespace ElasticOps
    module Intellisense =
        open ElasticOps.Parsing
        open Microsoft.FSharp.Core
        open ElasticOps.Configuration
        open ElasticOps

        let config = ConfigLoaders.LoadIntellisenseConfig()
        

        let filterPropertySuggestions context suggestions =
            suggestions 
                |> List.filter (fun s -> match s with 
                                            | {Text = text ; Mode = Mode.Property} ->
                                                                                        match  Processing.endsOnPropertyName (Option.get context.ParseTree) with
                                                                                                | (true, name) ->   text.ToLower().StartsWith(name.ToLower()) 
                                                                                                | _ -> false
                                            | _ -> true)

        let TrySuggest text caretLine caretColumn (endpoint:string) = 
            let context = Context.create text caretLine caretColumn

            match context.ParseTree with
            | None -> (context, None)
            | Some tree -> 
                let file = System.String.Format(config.RulesFileFomrat,endpoint)
                let suggestions = SuggestEngine.matchSuggestions tree file
                                    |> filterPropertySuggestions context
                                    |> fun sgs -> match sgs  with
                                                    | [] -> None
                                                    | _ -> Some sgs
                                                    

                (context, suggestions)

        let completeProperty context suggestion = 
            let codeFromCaret = context.OriginalCode.Substring(context.CodeTillCaret.Length)
            let lastQuotePos = context.CodeTillCaret.LastIndexOf "\""
            let codeTillQuote = context.CodeTillCaret.Substring(0,lastQuotePos+1)

            let suggestText = suggestion.Text + "\" : "
            let lastLineTillCaret = ((String.split "\r\n" context.CodeTillCaret ) |> Seq.last);
            let indexOfLastQuoteInLastLineTillCaret = lastLineTillCaret.LastIndexOf("\"")

            let newCaretColumn =((fst context.OriginalCaretPosition),indexOfLastQuoteInLastLineTillCaret+suggestText.Length+2)

            {context with 
                CodeFromCaret = codeFromCaret; 
                NewText = codeTillQuote + suggestText + codeFromCaret; 
                NewCaretPosition = newCaretColumn;
            }

        let Complete (context : Context) (suggestion : Suggestion) = 
            match suggestion.Mode with 
            | Mode.Property -> completeProperty context suggestion
            | _ -> failwith "Not supported"
        
