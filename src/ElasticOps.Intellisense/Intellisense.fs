namespace ElasticOps.Intellisense

    open ElasticOps.Parsing
    open Microsoft.FSharp.Core
    open ElasticOps.Configuration


    module IntellisenseEngine =
        
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

        let PostfixFromCompletionMode mode = 
            match mode with
            | "|colon|" -> (" : ","")
            | "|empty_object|" -> (" : {","}")
            | "|empty_array|" -> (" : [","]")
            | "|field|" -> (@" : { ""field"" : ""","\" } ")
            | "|groovy|" -> (" : \"groovy\" ","")
            | "|int_0|" -> (" : 0 ","")
            | "|int_1|" -> (" : 1 ","")
            | "|int_2|" -> (" : 2 ","")
            | "|int_3|" -> (" : 3 ","")
            | "|int_4|" -> (" : 3 ","")
            | "|int_5|" -> (" : 5 ","")
            | "|int_10|" -> (" : 10 ","")
            | "|int_12|" -> (" : 12 ","")
            | "|int_20|" -> (" : 20 ","")
            | "|int_25|" -> (" : 25 ","")
            | "|int_50|" -> (" : 50 ","")
            | "|int_70|" -> (" : 70 ","")
            | "|int_100|" -> (" : 100 ","")
            | "|int_200|" -> (" : 200 ","")
            | "|int_300|" -> (" : 300 ","")
            | "|int_1000|" -> (" : 1000 ","")
            | "|int_314159265359|" -> (" : 314159265359 ","")
            | "|decimal_0_2|" -> (" : 0.2","")
            | "|decimal_0_3|" -> (" : 0.3","")
            | "|decimal_0_5|" -> (" : 0.5","")
            | "|decimal_0_7|" -> (" : 0.7","")
            | "|decimal_1_0|" -> (" : ","")
            | "|decimal_1_2|" -> (" : 1.2","")
            | "|h_1_5|" -> (" : \"1.5h\"","")
            | "|best_fields|" -> (" : \"best_fields\"","")
            | "|percent_20|" -> (" : \"20%\"","")
            | "|_value|" -> (" : \"_value\"","")
            | "|example_lat|" -> (" : 52.376 ","")
            | "|example_lon|" -> (" : 4.894 ","")
            | "|true|" -> (" : true ","")
            | "|false|" -> (" : false ","")
            | "|flags_OR_AND_PREFIX|" -> (" : \"OR|AND|PREFIX\"","")
            | "|field_placeholder|" -> (" : \"{field}\"","")
            | "|index_placeholder|" -> (" : \"{index}\"","")
            | "|type_placeholder|" -> (" : \"{type}\"","")
            | "|none|" -> (" : \"none\"","")
            | "|shape|" -> (" : \"shape","\"")
            | "|within|" -> (" : \"within\"","")
            | "|OR|" -> (" : \"OR\"","")
            | "|ROOT|" -> (" : \"ROOT\"","")
            | "|day|" -> (" : \"day\"","")
            | "|max|" -> (" : \"max\"","")
            | "|count|" -> (" : \"count\"","")
            | "|km|" -> (" : \"km\"","")
            | "|envelope|" -> (" : \"envelope\"","")
            | "|standard|" -> (" : \"standard\"","")
            | "|docs|" -> (" : \"docs\"","")
            | "|analyzed|" -> (" : \"analyzed\"","")
            | "|direct|" -> (" : \"direct\"","")
            | "|default|" -> (" : \"default\"","")
            | "|basic_date|" -> (" : \"basic_date\"","")
            | "|just_name|" -> (" : \"just_name\"","")
            | "|no|" -> (" : \"no\"","")
            | "|plain|" -> (" : \"plain\"","")
            | "|avg|" -> (" : \"avg\"","")
            | "|first|" -> (" : \"first\"","")
            | "|multiply|" -> (" : \"first\"","")
            | "|asterisk|" -> (" : \"*\" ","")
            | "|_1s|" -> (" : \"_1s\" ","")
            | "|percentiles_percents|" -> (" : [1,5,25,50,75,95,99] ","")
            | "|percentile_ranks_values|" -> (" : [10,15] ","")
            | "|asc|" -> (" : \"asc\" ","")
            | "|depth_first|" -> (" : \"depth_first\" ","")
            | "|ip_mask|" -> (" : \"10.0.0.127/25\" ","")
            | "|empty_string|" -> (" : \"","\" ")
            | "|date_format|" -> (" : \"yyyy-MM-dd\" ","")
            | "|MMyyy|" -> (" : \"MM-yyy\" ","")
            | "|MMyyyy|" -> (" : \"MM-yyyy\" ","")
            | _ -> failwith ("Unknown completion mode: "+mode)
    
        let completeProperty context suggestion mode = 
            let codeFromCaret = context.OriginalCode.Substring(context.CodeTillCaret.Length)
            let lastQuotePos = context.CodeTillCaret.LastIndexOf "\""
            let codeTillQuote = context.CodeTillCaret.Substring(0,lastQuotePos+1)
        
            let postfix = PostfixFromCompletionMode(mode)
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
        
    