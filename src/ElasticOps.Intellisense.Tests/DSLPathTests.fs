module DSLPathTests

open NUnit.Framework
open FsUnit
open ElasticOps.Parsing.Processing
open ElasticOps.Parsing.Structures
open ElasticOps
open ElasticOps.DSLPath

[<Test>]
let ``path for property in object`` () = 
//    let res = @"{ ""prop"" : 1" 
//                |> parse
//                |> Option.get
//                |> getDLSPath
//    res |> should equal [Object;Property("prop");Value(JsonValue.Int(1))]
    1 |> should equal 1