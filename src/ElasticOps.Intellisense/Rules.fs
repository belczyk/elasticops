namespace ElasticOps
    open ElasticOps
    open ElasticOps.Parsing.Processing
    open ElasticOps.Parsing.Structures
    
    module Rules = 
        let sign rule = fst rule
        let prod rule = snd rule
        let ruleHd rule = 
            List.head (fst rule)

//        let readRulesFromJson = 
//            let json = System.IO.File.ReadAllText "IntellisenseRules.json"
//            let parseTree = parse json
//
//            match parseTree with
//            | None -> []
//            | Some tree -> 
//                let rec discoverRules tree rulePrefix =
//                    match tree with 
//                    | Assoc props -> let rules = props 
//                                                    |> List.map (fun prop -> DSL(Property((fst prop)))::rulePrefix)
//                                     let lowerLevelRules = props
//                                                           |> List.map (fun prop -> discoverRules (snd prop) (DSL(Property(fst prop))::rulePrefix))
//                                                           |> List.collect (fun x -> x)
//                                 
//                                     rules@lowerLevelRules
//                                  
//                discoverRules tree []

        let propertySuggestRules =  [
            {Sign = [RuleSign.UnfinishedPropertyName]; Suggestions = [{Text = "aggs"; Mode = Mode.Property};
                                                  {Text = "explain"; Mode = Mode.Property};
                                                  {Text = "facets"; Mode = Mode.Property};
                                                  {Text = "fielddata_fields"; Mode = Mode.Property};
                                                  {Text = "fields"; Mode = Mode.Property};
                                                  {Text = "from"; Mode = Mode.Property};
                                                  {Text = "highlight"; Mode = Mode.Property};
                                                  {Text = "partial_fields"; Mode = Mode.Property};
                                                  {Text = "post_filter"; Mode = Mode.Property};
                                                  {Text = "query"; Mode = Mode.Property};
                                                  {Text = "script_fields"; Mode = Mode.Property};
                                                  {Text = "size"; Mode = Mode.Property};
                                                  {Text = "sort"; Mode = Mode.Property};
                                                  {Text = "stats"; Mode = Mode.Property};
                                                  {Text = "timeout"; Mode = Mode.Property};
                                                  {Text = "version"; Mode = Mode.Property}
                                                 ]};
            { Sign = [RuleSign.Property("query");RuleSign.UnfinishedPropertyName]; Suggestions = [
                                                                        {Text = "bool"; Mode = Mode.Property};
                                                                        {Text = "boosting"; Mode = Mode.Property};
                                                                        {Text = "constant_score"; Mode = Mode.Property};
                                                                        {Text = "custom_filters_score"; Mode = Mode.Property};
                                                                        {Text = "dis_max"; Mode = Mode.Property};
                                                                        {Text = "field"; Mode = Mode.Property};
                                                                        {Text = "filtered"; Mode = Mode.Property};
                                                                        {Text = "flt"; Mode = Mode.Property};
                                                                        {Text = "function_score"; Mode = Mode.Property};
                                                                        {Text = "fuzzy"; Mode = Mode.Property};
                                                                        {Text = "fuzzy_like_this"; Mode = Mode.Property};
                                                                        {Text = "geo_shape"; Mode = Mode.Property};
                                                                        {Text = "has_child"; Mode = Mode.Property};
                                                                        {Text = "has_parent"; Mode = Mode.Property};
                                                                        {Text = "ids"; Mode = Mode.Property};
                                                                        {Text = "indices"; Mode = Mode.Property};
                                                                        {Text = "match"; Mode = Mode.Property};
                                                                        {Text = "match_all"; Mode = Mode.Property};
                                                                        {Text = "match_phrase"; Mode = Mode.Property};
                                                                        {Text = "match_phrase_perfix"; Mode = Mode.Property};
                                                                        {Text = "mlt"; Mode = Mode.Property};
                                                                        {Text = "more_like_this"; Mode = Mode.Property};
                                                                        {Text = "more_like_this_field"; Mode = Mode.Property};
                                                                        {Text = "multi_match"; Mode = Mode.Property};
                                                                        {Text = "nested"; Mode = Mode.Property};
                                                                        {Text = "prefix"; Mode = Mode.Property};
                                                                        {Text = "query_string"; Mode = Mode.Property};
                                                                        {Text = "range"; Mode = Mode.Property};
                                                                        {Text = "simple_query_string"; Mode = Mode.Property};
                                                                        {Text = "span_frist"; Mode = Mode.Property};
                                                                        {Text = "span_near"; Mode = Mode.Property};
                                                                        {Text = "span_not"; Mode = Mode.Property};
                                                                        {Text = "span_or"; Mode = Mode.Property};
                                                                        {Text = "term"; Mode = Mode.Property};
                                                                        {Text = "terms"; Mode = Mode.Property};
                                                                        {Text = "top_children"; Mode = Mode.Property};
                                                                        {Text = "wildcard"; Mode = Mode.Property};
                                                 ]};
        ]

