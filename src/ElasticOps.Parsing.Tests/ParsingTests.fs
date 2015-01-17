module LexerTests 

open NUnit.Framework
open FsUnit
open ElasticOps.Parsing.Structures
open ElasticOps.Parsing.Processing  


let countSubstring (where :string) (what : string) =
    match what with
    | "" -> 0 // just a definition; infinity is not an int
    | _ -> (where.Length - where.Replace(what, @"").Length) / what.Length


[<Test>]
let ``parse values ``() =
    "true"  |> parse |> should equal (Some(jsonValue.Bool(true)))
    "false"  |> parse |> should equal (Some(jsonValue.Bool(false)))
    "1"  |> parse |> should equal (Some(jsonValue.Int(1)))
    "1.23"  |> parse |> should equal (Some(jsonValue.Float(1.23)))
    "[1]"  |> parse |> should equal (Some(jsonValue.List([jsonValue.Int(1)])))
    "[1,2,3]"  |> parse |> should equal (Some(jsonValue.List([jsonValue.Int(1);jsonValue.Int(2);jsonValue.Int(3)])))
    "\"abc\"" |> parse |> should equal (Some(jsonValue.String("abc")))

[<Test>]
let ``parse simple objects`` () =
    //no peroperties
    "{}" |> parse |> should equal (Some(jsonValue.Assoc([])))
    //one property object
    "{ \"prop\" : 1.25 }" |> parse |> should equal (Some(jsonValue.Assoc([("prop",jsonValue.Float(1.25))])))
    //multiple properties
    "{ \"prop\" : 12, \"prop2\" : \"123\" }" |> parse |> should equal (Some(jsonValue.Assoc([("prop",jsonValue.Int(12));("prop2",jsonValue.String("123"))])))
    //property of type array 
    "{ \"prop\" : [1,23] }" |> parse |> should equal (Some(jsonValue.Assoc([("prop",jsonValue.List([jsonValue.Int(1); jsonValue.Int(23)]))])))


[<Test>]
let ``parse complex objects`` () = 
    let res = @"{
              ""title"": ""Cities"",
              ""cities"": [
                { ""name"": ""Chicago"",  ""zips"": [60601,60600] },
                { ""name"": ""New York"", ""zips"": [10001] } 
              ]
            }" |> parse 


    res |> should equal (Some( jsonValue.Assoc(
                        [("title",jsonValue.String("Cities"));
                         ("cities", jsonValue.List([
                                        jsonValue.Assoc([
                                                ("name",jsonValue.String("Chicago"));
                                                ("zips",jsonValue.List([jsonValue.Int(60601);jsonValue.Int(60600)]))
                                        ]);
                                        jsonValue.Assoc([
                                                ("name",jsonValue.String("New York"));
                                                ("zips",jsonValue.List([jsonValue.Int(10001)]))
                                        ])
                         ]))]
                      )
    ))

[<Test>]
let ``parse huge json in less then half seconds`` () =
    let json = System.IO.File.ReadAllText "hugeJson.json"

    let stopWatch = System.Diagnostics.Stopwatch.StartNew()
    json |> parse |> ignore
    stopWatch.Stop()

    System.Console.WriteLine("Parse {0} lines of json {1} ms",(countSubstring json "\n"),stopWatch.ElapsedMilliseconds)

    stopWatch.ElapsedMilliseconds |> should be (lessThan 500)