module AnalyzeCommandsTests
open NUnit.Framework
open ElasticOps.Com
open FsUnit 
open System.Linq

[<Test>]
let ``Analyze can be executed without errors`` () =
    (fun con -> new Analyze.AnalyzeCommand(con,"standard","wlazl kotek na plotek i mruuu-czy"))
        |> executeForAllConnections 
        |> List.map (fun x -> 
                        x.Success |> should be True
                        x.Result.Count() |> should be (greaterThan 0))
        |> ignore

[<Test>]
let ``Analyze with index analyzer can be executed without errors`` () =
    (fun con -> new Analyze.AnalyzeWithIndexAnalyzerCommand(con,"products","standard","wlazl kotek na plotek i mruuu-czy"))
        |> executeForAllConnections 
        |> List.map (fun x -> 
                        x.Success |> should be True
                        x.Result.Count() |> should be (greaterThan 0))
        |> ignore

[<Test>]
let ``Analyze with field analyzer can be executed without errors`` () =
    (fun con -> new Analyze.AnalyzeWithFieldAnalyzerCommand(con,"products","book.description","wlazl kotek na plotek i mruuu-czy"))
        |> executeForAllConnections 
        |> List.map (fun x -> 
                        x.Success |> should be True
                        x.Result.Count() |> should be (greaterThan 0))
        |> ignore