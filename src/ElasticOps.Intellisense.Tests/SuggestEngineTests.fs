module SuggestEngineTests

open NUnit.Framework
open FsUnit
open ElasticOps.Parsing.Processing
open ElasticOps.Parsing.Structures
open ElasticOps
open System.IO
open ElasticOps.SuggestEngine

[<Test>]
let ``match simple rule`` () = 
    let path = @"{ ""prop"" : 1" |> parse  |> Option.get |> DSLPath.find

    let rule = ([DSL(Object);AnyProperty;AnyValue], [{Text = "aggs"; Mode = Mode.Property}])

    SuggestEngine.matchRuleWithPath (fst rule) path |> should equal true
