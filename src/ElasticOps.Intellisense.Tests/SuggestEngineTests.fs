module SuggestEngineTests

open NUnit.Framework
open FsUnit
open ElasticOps.Parsing.Processing
open ElasticOps
open ElasticOps.SuggestEngine

[<Test>]
let ``matchRuleWithPath: when path and rule matches return true`` () = 
    let path = @"{ ""pro " |> parse  |> Option.get |> findDSLPath

    let rule = [RuleSign.UnfinishedPropertyName]

    SuggestEngine.matchRuleWithPath rule path |> should be True


[<Test>]
let ``matchRuleWithPath: when path and rule are not match return false`` () = 
    let path = @"{ ""prop"" : 1" |> parse  |> Option.get |> findDSLPath

    let rule = [RuleSign.Property("query");RuleSign.UnfinishedPropertyName]

    SuggestEngine.matchRuleWithPath rule path |> should be False


[<Test>]
let ``matchRuleWithPath: try match long path `` () = 
    let path = @"{ ""prop"" : { ""query"" : { ""filter"" : { ""xxx" |> parse  |> Option.get |> findDSLPath

    let rule = [RuleSign.Property("prop");RuleSign.Property("query");RuleSign.Property("filter");RuleSign.UnfinishedPropertyName]


    SuggestEngine.matchRuleWithPath rule path |> should be True

[<Test>]
let ``matchRuleWithPath: match any property `` () = 
    let path = @"{ ""prop"" : { ""query"" : { ""filter"" : { ""xxx" |> parse  |> Option.get |> findDSLPath

    let rule = [RuleSign.Property("prop");RuleSign.AnyProperty;RuleSign.Property("filter");RuleSign.UnfinishedPropertyName]

    SuggestEngine.matchRuleWithPath rule path |> should be True

[<Test>]
let ``can read rules from file`` () = 
    let rules = readRulesFromJson "IntellisenseRules_search.json"


    rules.Length |> should  be (greaterThan 0)

[<Test>]
let ``matchRuleWithPath: match any path`` () = 
    let path = @"{ ""prop"" : { ""query"" : { ""filter"" : { ""xxx" |> parse  |> Option.get |> findDSLPath

    let rule = [RuleSign.AnyPath;RuleSign.Property("filter");RuleSign.UnfinishedPropertyName]

    SuggestEngine.matchRuleWithPath rule path |> should be True

[<Test>]
let ``matchRuleWithPath: match any path when in array `` () = 
    let path = @"{ ""prop"" : { ""query"" : [{ ""filter"" : { ""xxx" |> parse  |> Option.get |> findDSLPath

    let rule = [RuleSign.AnyPath;RuleSign.Property("filter");RuleSign.UnfinishedPropertyName]

    SuggestEngine.matchRuleWithPath rule path |> should be True
