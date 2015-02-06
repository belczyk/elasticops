module DSLPathTests

open NUnit.Framework
open FsUnit
open ElasticOps.Parsing.Processing
open ElasticOps.Parsing.Structures
open ElasticOps.SuggestEngine
open System.IO
open System

[<Test>]
let ``path for property in object`` () = 
    let res = @"{ ""prop"" : { ""prop2""" 
                |> parse
                |> Option.get
                |> findDSLPath
    res |> should equal [DSLPathNode.PropertyWithValue("prop");DSLPathNode.PropertyName("prop2")]

[<Test>]
let ``can get path for for huge ElasticSearch query cut in the middle randomly`` () =
    let json = File.ReadAllText "hugeESJsonQuery.json"
    let random = Random((int)DateTime.Now.Ticks)
    let watch = System.Diagnostics.Stopwatch.StartNew()

    let testAsync i = 
        async {
            let subJson = json.Substring(0,random.Next(1,json.Length));
            try
                let res = subJson |> parse |> Option.get |> findDSLPath

                ()
            with
            | ex -> raise (new Exception("Failed to generate path for: "+subJson,ex))
        }

    [1..100] |> Seq.map testAsync |> Async.Parallel |> Async.RunSynchronously |> ignore

    watch.Stop()

    printfn "total time [ms]: %d" watch.ElapsedMilliseconds
    //fool test runner to not ignore this test
    Assert.True true

[<Test>]
let ``can get path for json terminated at any point`` () =
    let json = @"{""b1"": true,""b2"": false,""abs"": [ true, false ],""nu"": null,""ans"": [ null, null ],""in"": -12,""ais"": [ -12, 1 ],""fl"": -123.23E-12,""afs"": [ -123.23E-12, +123.23E-12],""st"": ""st fa\"" f\"" f"",""ass"": [ ""str"", ""fas"", ""\"" f"" ]}"

    let watch = System.Diagnostics.Stopwatch.StartNew()

    let testAsync i = 
        async {
            let subJson = json.Substring(0,i);
            try
                subJson |> parse |> Option.get |> findDSLPath |> ignore
            with
            | ex -> raise ex
        }

    [1..(json.Length-1)] |> Seq.map testAsync |> Async.Parallel |> Async.RunSynchronously |> ignore

    watch.Stop()

    //printfn "total time [ms]: %d" watch.ElapsedMilliseconds
    //fool test runner to not ignore this test
    Assert.True true
