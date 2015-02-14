module IntellisenseEngineTests

open NUnit.Framework
open FsUnit
open ElasticOps.Parsing.Processing
open ElasticOps.Intellisense 
open ElasticOps.Intellisense.SuggestEngine

[<Test>]
let ``all completion macros are supported`` () = 
    let rulesFiles = System.IO.File.ReadAllText("IntellisenseRules_search.json")
    let macros = System.Text.RegularExpressions.Regex.Matches(rulesFiles,"\|[a-z|A-Z|0-9]*\|")
    for m in macros do
        try
            IntellisenseEngine.PostfixFromCompletionMode m.Value |> ignore
        with
        | _ -> 
                System.Console.WriteLine(m)
                Assert.Fail()

    

