namespace ElasticOps
    open ElasticOps.Parsing.Structures

    module DSLPath = 
        let getDLSPath (parseTree: JsonValue) = 
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

