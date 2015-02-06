module SuggestEngineTests

open NUnit.Framework
open FsUnit
open ElasticOps.Parsing.Processing
open ElasticOps.Parsing.Structures
open ElasticOps
open System.IO
open ElasticOps.SuggestEngine

[<Test>]
let ``matchRuleWithPath: when path and rule matches return true`` () = 
    let path = @"{ ""pro " |> parse  |> Option.get |> findDSLPath

    let rule = { Sign = [RuleSign.UnfinishedPropertyName]; Suggestions = [{Text = "aggs"; Mode = Mode.Property}]}

    SuggestEngine.matchRuleWithPath rule.Sign path |> should be True


[<Test>]
let ``matchRuleWithPath: when path and rule are not match return false`` () = 
    let path = @"{ ""prop"" : 1" |> parse  |> Option.get |> findDSLPath

    let rule = { Sign = [RuleSign.Property("query");RuleSign.UnfinishedPropertyName]; Suggestions = [{Text = "aggs"; Mode = Mode.Property}]}

    SuggestEngine.matchRuleWithPath rule.Sign path |> should be False


[<Test>]
let ``matchRuleWithPath: try match long path `` () = 
    let path = @"{ ""prop"" : { ""query"" : { ""filter"" : { ""xxx" |> parse  |> Option.get |> findDSLPath

    let rule = { Sign = [RuleSign.Property("prop");RuleSign.Property("query");RuleSign.Property("filter");RuleSign.UnfinishedPropertyName]; Suggestions = [{Text = "aggs"; Mode = Mode.Property}]}


    SuggestEngine.matchRuleWithPath rule.Sign path |> should be True

[<Test>]
let ``can read rules from file`` () = 
    let rules = readRulesFromJson "IntellisenseRules.json"


    rules.Length |> should  be (greaterThan 0)