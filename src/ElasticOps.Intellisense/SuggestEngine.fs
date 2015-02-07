namespace ElasticOps
    open ElasticOps.Parsing.Structures
    open ElasticOps.Parsing
    open ElasticOps.Parsing.Processing
    open ElasticOps

    module SuggestEngine = 
        type DSLPathNode  =
            | PropertyName of string
            | PropertyNameWithColon of string
            | UnfinishedPropertyName of string
            | PropertyWithValue of string
            | Value of JsonValue

        let findDSLPath (parseTree: JsonValue) = 
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
                                                | [] -> acc
                                                | _ -> 
                                                    let lastElem = Seq.last elements 
                                                    findPath lastElem acc
                | JsonValue.Bool _ 
                | JsonValue.Int _
                | JsonValue.Float _
                | JsonValue.Int _
                | JsonValue.Null 
                | JsonValue.String _
                | JsonValue.UnfinishedValue _ -> Value(tree)::acc


            findPath parseTree [] |> List.rev

        let rulesCache = new System.Collections.Generic.Dictionary<string,Rule list>()
        let readRulesFromJson filePath = 
            match rulesCache.ContainsKey filePath with
            | true -> rulesCache.[filePath]
            | false -> 
                        let json = System.IO.File.ReadAllText filePath
                        let parseTree = parseIntellisense json
                        let isProperty x =
                            match x with 
                            | IntellisenseProperty.Property _ -> true
                            | _ -> false
                        match parseTree with
                        | None -> []
                        | Some tree -> 
                            let rec discoverRules tree rulePrefix =
                                match tree with 
                                | IntellisenseValue.Assoc props -> 
                                                 match props with 
                                                 | [] -> []
                                                 | _ ->
                                                         let mainRule = {Sign = RuleSign.UnfinishedPropertyName::rulePrefix ; Suggestions = props 
                                                                                                            |> List.filter isProperty 
                                                                                                            |> List.map(fun p -> {Text=p.getPropertyName(); Mode = Mode.Property(p.getCompletionMode())})}
                                                         let subRules =  props
                                                                               |> List.map (fun p -> match p with 
                                                                                                       | IntellisenseProperty.Property(name,_, value) -> discoverRules value (RuleSign.Property(name)::rulePrefix)
                                                                                                       | IntellisenseProperty.AnyProperty(value) -> discoverRules value (RuleSign.AnyProperty::rulePrefix)
                                                                                                       | IntellisenseProperty.AnyPath(value) -> discoverRules value (RuleSign.AnyPath::rulePrefix)
                                                                                                       | _ -> failwith "Unsupported")
                                                         mainRule::(List.collect (fun sr -> sr) subRules)
                                | _ -> failwith "Unsupported "
                                  
                            let rules = discoverRules tree []
                                        |> List.map (fun rule -> {rule with Sign = List.rev rule.Sign})
                            rulesCache.Add(filePath,rules)

                            rules

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
                                   | (RuleSign.AnyProperty, DSLPathNode.PropertyWithValue _) -> true
                                   | (RuleSign.AnyProperty, _ ) -> false
                                   | (RuleSign.AnyPath, _ ) -> true
                | (rH::rT,pH::pT) -> 
                                   match (rH,pH) with
                                   | (RuleSign.Property rName, DSLPathNode.PropertyWithValue pName) when rName = pName -> matchRuleWithPath rT pT 
                                   | (RuleSign.Property _ , _ ) -> false
                                   | (RuleSign.UnfinishedPropertyName, DSLPathNode.UnfinishedPropertyName _) -> matchRuleWithPath rT pT
                                   | (RuleSign.UnfinishedPropertyName, _ ) -> false
                                   | (RuleSign.AnyProperty, DSLPathNode.PropertyWithValue _) -> matchRuleWithPath rT pT
                                   | (RuleSign.AnyProperty, _ ) -> false
                                   | (RuleSign.AnyPath, _ ) -> let nextInRule = (List.head rT)
                                                               match nextInRule with 
                                                               | RuleSign.Property name -> let reversedPath = List.rev path
                                                                                           let rec matchingTillMarker path rem = 
                                                                                                match path with
                                                                                                | h::t -> match h with
                                                                                                            | DSLPathNode.PropertyWithValue pName when pName = name -> (true,h::rem)
                                                                                                            | _ -> matchingTillMarker t (h::rem)
                                                                                                | [] -> (false,rem)
                                                                                           let (matched,rem) = (matchingTillMarker reversedPath [])

                                                                                           if matched then matchRuleWithPath rT rem
                                                                                           else false
                                                               | _ -> false

        let matchSuggestions (parseTree : JsonValue) rulesFile= 
            let path = parseTree |> findDSLPath
            let rules = readRulesFromJson rulesFile 
            let suggestions = rules
                                |> List.filter (fun rule -> matchRuleWithPath rule.Sign path) 
                                |> List.collect (fun rule -> rule.Suggestions )
            suggestions