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
    let path = @"{ ""prop"" : 1" |> parse  |> Option.get |> DSLPath.find

    let rule = ([DSL(Object);AnyProperty;AnyValue], [{Text = "aggs"; Mode = Mode.Property}])

    SuggestEngine.matchRuleWithPath (fst rule) path |> should be True


[<Test>]
let ``matchRuleWithPath: when path and rule are not match return false`` () = 
    let path = @"{ ""prop"" : 1" |> parse  |> Option.get |> DSLPath.find

    let rule = ([DSL(Object);DSL(Property("query"));AnyValue], [{Text = "aggs"; Mode = Mode.Property}])

    SuggestEngine.matchRuleWithPath (fst rule) path |> should be False


[<Test>]
let ``matchRuleWithPath: when rule has specific value and in path there is different value return false`` () = 
    let path = @"{ ""prop"" : 1" |> parse  |> Option.get |> DSLPath.find

    let rule = ([DSL(Object);AnyProperty;DSL(Value(Bool(true)))], [{Text = "aggs"; Mode = Mode.Property}])

    SuggestEngine.matchRuleWithPath (fst rule) path |> should be False


[<Test>]
let ``matchRuleWithPath: when rule has specific value matching path return true`` () = 
    let path = @"{ ""prop"" : 123" |> parse  |> Option.get |> DSLPath.find

    let rule = ([DSL(Object);AnyProperty;DSL(Value(Int(123)))], [{Text = "aggs"; Mode = Mode.Property}])

    SuggestEngine.matchRuleWithPath (fst rule) path |> should be True

[<Test>]
let ``matchRuleWithPath: try match long path `` () = 
    let path = @"{ ""prop"" : { ""query"" : { ""filter"" : 1 " |> parse  |> Option.get |> DSLPath.find

    let rule = ([DSL(Object);DSL(Property("prop"));DSL(Object);DSL(Property("query"));DSL(Object);DSL(Property("filter"));AnyValue], [{Text = "aggs"; Mode = Mode.Property}])

    SuggestEngine.matchRuleWithPath (fst rule) path |> should be True