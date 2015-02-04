namespace ElasticOps
    open ElasticOps.Parsing.Structures

    module DSLPath = 
        let getDLSPath (parseTree: JsonValue) = 
            let rec findPath tree acc =
                match tree with
                | JsonValue.Assoc props -> 
                                            let lastProp = Seq.last props 
                                            let propName = fst lastProp 
                                            let value = snd lastProp 

                                            findPath value (Object::Property(propName)::acc)
                | JsonValue.List elements -> let lastElem = Seq.last elements 
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

            findPath parseTree []

