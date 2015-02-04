namespace ElasticOps
    open ElasticOps.Parsing.Structures
    open ElasticOps.Rules

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
        let matchSuggestions (parseTree : JsonValue) = 
            let rec filterRules path rules = 
                match List.length rules with 
                | 1 -> (snd (List.head rules))
                | _ -> 
                        let pathHd = List.head path
                        rules
                            |> List.filter (fun rule -> match ruleHd rule with
                                                        | AnyProperty -> match pathHd with 
                                                                         | Property _ -> true
                                                                         | _ -> false
                                                        | AnyValue -> match pathHd with
                                                                          | Value _ -> true
                                                                          | _ -> false
                                                        | DSL jv -> jv = pathHd
                                           )
                            |> filterRules (List.tail path)
        
            filterRules (DSLPath.find parseTree) Rules.propertySuggestRules 
