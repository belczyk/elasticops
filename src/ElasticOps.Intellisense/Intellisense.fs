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
                                            | {Text = text ; Mode = Mode.Property(_)} ->
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

        let postfixFromCompletionMode mode = 
            match mode with
            | "|colon|" -> (" : ","")
            | "|empty_object|" -> (" : {","}")
            | "|empty_array|" -> (" : [","]")
            | "|field|" -> (@" : { ""field"" : ""","\" } ")
            | "|groovy|" -> (" : \"groovy\" ","")
            | "|int_0|" -> (" : 0 ","")
            | "|int_1|" -> (" : 1 ","")
            | "|int_3|" -> (" : 3 ","")
            | "|int_10|" -> (" : 10 ","")
            | "|int_20|" -> (" : 20 ","")
            | "|int_50|" -> (" : 50 ","")
            | "|int_100|" -> (" : 100 ","")
            | "|int_300|" -> (" : 300 ","")
            | "|int_1000|" -> (" : 1000 ","")
            | "|example_lat|" -> (" : 52.376 ","")
            | "|example_lon|" -> (" : 4.894 ","")
            | "|true|" -> (" : true ","")
            | "|astrisk|" -> (" : \"*\" ","")
            | "|_1s|" -> (" : \"_1s\" ","")
            | "|percentiles_percents|" -> (" : [1,5,25,50,75,95,99] ","")
            | "|percentile_ranks_values|" -> (" : [10,15] ","")
            | "|asc|" -> (" : \"asc\" ","")
            | "|depth_first|" -> (" : \"depth_first\" ","")
            | "|ip_mask|" -> (" : \"10.0.0.127/25\" ","")
            | "|empty_string|" -> (" : \"","\" ")
            | "|date_format|" -> (" : \"yyyy-MM-dd\" ","")
            | "|MMyyy|" -> (" : \"MM-yyy\" ","")
            | _ -> failwith "Unknown completion mode."

        let completeProperty context suggestion mode = 
            let codeFromCaret = context.OriginalCode.Substring(context.CodeTillCaret.Length)
            let lastQuotePos = context.CodeTillCaret.LastIndexOf "\""
            let codeTillQuote = context.CodeTillCaret.Substring(0,lastQuotePos+1)

            let postfix = postfixFromCompletionMode(mode)
            let suggestText = suggestion.Text + "\" " 
            let lastLineTillCaret = ((String.split "\r\n" context.CodeTillCaret ) |> Seq.last);
            let indexOfLastQuoteInLastLineTillCaret = lastLineTillCaret.LastIndexOf("\"")

            let newCaretColumn =((fst context.OriginalCaretPosition),indexOfLastQuoteInLastLineTillCaret+suggestText.Length+((fst postfix).Length)+2)

            {context with 
                CodeFromCaret = codeFromCaret; 
                NewText = codeTillQuote + suggestText+ (fst postfix) + (snd postfix) + codeFromCaret; 
                NewCaretPosition = newCaretColumn;
            }

        let Complete (context : Context) (suggestion : Suggestion) = 
            match suggestion.Mode with 
            | Mode.Property mode -> completeProperty context suggestion mode
            | _ -> failwith "Not supported"
        
