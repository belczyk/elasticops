module IntellisenseEngineTests

open NUnit.Framework
open ElasticOps.Intellisense 

let ``all completion macros are supported`` fileName = 
    let rulesFiles = System.IO.File.ReadAllText(fileName)
    let macros = System.Text.RegularExpressions.Regex.Matches(rulesFiles,"\|[a-z|A-Z|0-9|_]*\|")
    let mutable shouldFail =  false
    for m in macros do
        try
            IntellisenseEngine.PostfixFromCompletionMode m.Value |> ignore
        with
        | _ -> 
                System.Console.WriteLine(m)
                shouldFail <- true

    if shouldFail then Assert.Fail() else ()

[<Test>]
let ``all completion macros are supported for _search endpoint`` () = 
    ``all completion macros are supported`` "IntellisenseRules_search.json"


[<Test>]
let ``all completion macros are supported for _mapping endpoint`` () = 
    ``all completion macros are supported`` "IntellisenseRules_mapping.json"

