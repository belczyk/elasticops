module ClusterInfoTests


open Json.AST.Parse
open FsUnit
open NUnit.Framework
open System 
open ElasticOps.Com.CommonTypes
open ElasticOps.Com.ClusterInfo
open System.Collections.Generic


[<Test>]
let ``traverse find finds objects in a tree``() =
    let command = new ListTypesCommand(new Connection(new Uri("http://localhost:9200")))
    command.IndexName <- "docs"
    let res = ListTypes(command)
    res|> should equal (new List<string>())

