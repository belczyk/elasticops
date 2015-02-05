namespace ElasticOps
    open ElasticOps.Parsing.Structures
    open ElasticOps.Rules
    open ElasticOps

    module DSLPath = 
        let find (parseTree: JsonValue) = 
            let rec findPath tree acc =
                match tree with
                | JsonValue.Assoc props -> 
                                            match props with
                                            | [] -> Object::acc
                                            | _ -> 
                                                    let lastProp = Seq.last props 
                                                    let propName = fst lastProp 
                                                    let value = snd lastProp 

                                                    findPath value (Property(propName)::Object::acc)
                | JsonValue.List elements -> match elements with
                                                | [] -> Array::acc
                                                | _ -> 
                                                    let lastElem = Seq.last elements 
                                                    findPath lastElem (Array::acc)
                | JsonValue.Bool _ 
                | JsonValue.Colon 
                | JsonValue.Int _
                | JsonValue.Float _
                | JsonValue.Int _
                | JsonValue.Missing 
                | JsonValue.Null 
                | JsonValue.String _
                | JsonValue.UnfinishedValue _ -> Value(tree)::acc

            findPath parseTree [] |> List.rev



    module SuggestEngine = 

        let rec matchRuleWithPath rule path = 
                match (rule,path) with 
                | ([],[]) -> true
                | (_::_,[]) | ([],_::_) -> false
                | ( rH::rT,pH::pT) ->  
                                   match (rH,pH) with 
                                   | (AnyProperty, Property _)
                                   | (AnyValue, Value _) -> matchRuleWithPath rT pT
                                   | (DSL node, pNode) when node = pNode -> matchRuleWithPath rT pT
                                   | _ -> false
        let matchSuggestions (parseTree : JsonValue) = 
            let path = parseTree |> DSLPath.find

            let suggestions = Rules.propertySuggestRules 
                                |> List.filter (fun rule -> matchRuleWithPath (sign rule) path) 
                                |> List.collect (fun rule -> (prod rule) )
                                    
            suggestions