namespace ElasticOps
    open ElasticOps.Parsing.Structures
    open ElasticOps

    module DSLPath = 
        type DSLPathNode  =
            | PropertyName of string
            | PropertyNameWithColon of string
            | UnfinishedPropertyName of string
            | PropertyWithValue of string
            | Array
            | Value of JsonValue

        let find (parseTree: JsonValue) = 
            let rec findPath tree acc =
                match tree with
                | JsonValue.Assoc props -> 
                                            match props with
                                            | [] -> acc
                                            | _ -> 
                                                    let lastProp = Seq.last props 
                                                    match lastProp with 
                                                    | JsonProperty.PropertyName name -> (DSLPathNode.PropertyName name )::acc
                                                    | JsonProperty.PropertyNameWithColon  name -> (DSLPathNode.PropertyNameWithColon name )::acc
                                                    | JsonProperty.UnfinishedPropertyName  name -> (DSLPathNode.UnfinishedPropertyName name )::acc
                                                    | JsonProperty.PropertyWithValue(name,value) -> findPath value (DSLPathNode.PropertyWithValue(name)::acc)
                | JsonValue.Array elements -> match elements with
                                                | [] -> Array::acc
                                                | _ -> 
                                                    let lastElem = Seq.last elements 
                                                    findPath lastElem (Array::acc)
                | JsonValue.Bool _ 
                | JsonValue.Int _
                | JsonValue.Float _
                | JsonValue.Int _
                | JsonValue.Null 
                | JsonValue.String _
                | JsonValue.UnfinishedValue _ -> Value(tree)::acc


            findPath parseTree [] |> List.rev



    module SuggestEngine = 
        open DSLPath
        open ElasticOps

        let rec matchRuleWithPath rule path = 
                match (rule,path) with 
                | ([],[]) -> true
                | (_::_,[]) | ([],_::_) -> false
                | ( rH::[],pH::[]) ->  
                                   match (rH,pH) with 
                                   | (RuleSign.Property rName, DSLPathNode.PropertyName pName) -> rName = pName
                                   | (RuleSign.Property _ , _ ) -> false
                                   | (RuleSign.UnfinishedPropertyName, DSLPathNode.UnfinishedPropertyName _) -> true
                                   | (RuleSign.UnfinishedPropertyName, _ ) -> false
                | (rH::rT,pH::pT) -> 
                                   match (rH,pH) with
                                   | (RuleSign.Property rName, DSLPathNode.PropertyWithValue pName) when rName = pName -> matchRuleWithPath rT pT 
                                   | (RuleSign.Property _ , _ ) -> false
                                   | (RuleSign.UnfinishedPropertyName, DSLPathNode.UnfinishedPropertyName _) -> matchRuleWithPath rT pT
                                   | (RuleSign.UnfinishedPropertyName, _ ) -> false
        let matchSuggestions (parseTree : JsonValue) = 
            let path = parseTree |> DSLPath.find

            let suggestions = Rules.propertySuggestRules 
                                |> List.filter (fun rule -> matchRuleWithPath rule.Sign path) 
                                |> List.collect (fun rule -> rule.Suggestions )
            suggestions