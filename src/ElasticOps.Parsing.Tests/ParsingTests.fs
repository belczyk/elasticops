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
    "true"  |> parse |> should equal (Some(JsonValue.Bool(true)))
    "false"  |> parse |> should equal (Some(JsonValue.Bool(false)))
    "1"  |> parse |> should equal (Some(JsonValue.Int(1)))
    "1.23"  |> parse |> should equal (Some(JsonValue.Float(1.23)))
    "[1]"  |> parse |> should equal (Some(JsonValue.List([JsonValue.Int(1)])))
    "[1,2,3]"  |> parse |> should equal (Some(JsonValue.List([JsonValue.Int(1);JsonValue.Int(2);JsonValue.Int(3)])))
    "\"abc\"" |> parse |> should equal (Some(JsonValue.String("abc")))

[<Test>]
let ``parse simple objects`` () =
    //no peroperties
    "{}" |> parse |> should equal (Some(JsonValue.Assoc([])))
    //one property object
    "{ \"prop\" : 1.25 }" |> parse |> should equal (Some(JsonValue.Assoc([("prop",JsonValue.Float(1.25))])))
    //multiple properties
    "{ \"prop\" : 12, \"prop2\" : \"123\" }" |> parse |> should equal (Some(JsonValue.Assoc([("prop",JsonValue.Int(12));("prop2",JsonValue.String("123"))])))
    //property of type array 
    "{ \"prop\" : [1,23] }" |> parse |> should equal (Some(JsonValue.Assoc([("prop",JsonValue.List([JsonValue.Int(1); JsonValue.Int(23)]))])))


[<Test>]
let ``parse complex objects`` () = 
    let res = @"{
              ""title"": ""Cities"",
              ""cities"": [
                { ""name"": ""Chicago"",  ""zips"": [60601,60600] },
                { ""name"": ""New York"", ""zips"": [10001] } 
              ]
            }" |> parse 


    res |> should equal (Some( JsonValue.Assoc(
                        [("title",JsonValue.String("Cities"));
                         ("cities", JsonValue.List([
                                        JsonValue.Assoc([
                                                ("name",JsonValue.String("Chicago"));
                                                ("zips",JsonValue.List([JsonValue.Int(60601);JsonValue.Int(60600)]))
                                        ]);
                                        JsonValue.Assoc([
                                                ("name",JsonValue.String("New York"));
                                                ("zips",JsonValue.List([JsonValue.Int(10001)]))
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

[<Test>]
let ``parse random complex json without parse error`` () =
    let json = System.IO.File.ReadAllText "randomComplexTestsJson.json"

    json |> parse |> ignore


[<Test>]
[<Ignore>]
let ``can parse not complete json`` () = 
    let json = "{ \"prop\" : 1 "
    let res = json |> parse

    res |> should equal (JsonValue.Assoc([("prop",JsonValue.Int(1))]))