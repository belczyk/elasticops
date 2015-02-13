module LexerAndParserTests 

open NUnit.Framework
open FsUnit
open ElasticOps.Parsing
open ElasticOps.Parsing.Processing  
open System.IO

let countSubstring (where :string) (what : string) =
    match what with
    | "" -> 0 
    | _ -> (where.Length - where.Replace(what, @"").Length) / what.Length

[<Test>]
let ``parse values ``() =
    "true"  |> parse |> should equal (Some(JsonValue.Bool(true)))
    "false"  |> parse |> should equal (Some(JsonValue.Bool(false)))
    "1"  |> parse |> should equal (Some(JsonValue.Int(1)))
    "1.23"  |> parse |> should equal (Some(JsonValue.Float(1.23)))
    "[1]"  |> parse |> should equal (Some(JsonValue.Array([JsonValue.Int(1)])))
    "[1,2,3]"  |> parse |> should equal (Some(JsonValue.Array([JsonValue.Int(1);JsonValue.Int(2);JsonValue.Int(3)])))
    "\"abc\"" |> parse |> should equal (Some(JsonValue.String("abc")))

[<Test>]
[<TestCase("t")>]
[<TestCase("tr")>]
[<TestCase("tru")>]
let ``parse partial true value``(case) = 
    case  |> parse |> should equal (Some(JsonValue.UnfinishedValue(case,JsonValueType.TBool)))

[<Test>]
[<TestCase("f")>]
[<TestCase("fa")>]
[<TestCase("fal")>]
[<TestCase("fals")>]
let ``parse partial false value``(case) = 
    case  |> parse |> should equal (Some(JsonValue.UnfinishedValue(case,JsonValueType.TBool)))

[<Test>]
[<TestCase("n")>]
[<TestCase("nu")>]
[<TestCase("nul")>]
let ``parse partial null value`` (case) = 
    case  |> parse |> should equal (Some(JsonValue.UnfinishedValue(case,JsonValueType.TNull)))

[<Test>]
[<TestCase("+")>]
[<TestCase("-")>]
let ``parse partial int value`` (case) =
    case  |> parse |> should equal (Some(JsonValue.UnfinishedValue(case,JsonValueType.TInt)))
    
[<Test>]
[<TestCase("0.")>]
[<TestCase("-1.")>]
[<TestCase("+1.")>]
[<TestCase("-12.")>]
[<TestCase("+12.")>]
[<TestCase("+12.1e")>]
[<TestCase("-12.2E")>]
[<TestCase("12.2E")>]
[<TestCase("12.2e")>]
[<TestCase("-12.2E+")>]
[<TestCase("-12.2E-")>]
[<TestCase("-12.1e-")>]
[<TestCase("-12.1e+")>]
[<TestCase("+12.2E+")>]
[<TestCase("+12.2E-")>]
[<TestCase("+12.1e-")>]
[<TestCase("+12.1e+")>]
[<TestCase("12.2E+")>]
[<TestCase("12.2E-")>]
[<TestCase("12.1e-")>]
[<TestCase("12.1e+")>]
let ``parser partial float`` (case) =
    case  |> parse |> should equal (Some(JsonValue.UnfinishedValue(case,JsonValueType.TFloat)))
    
    

[<Test>]
let ``parse simple objects`` () =
    //no peroperties
    "{}" |> parse |> should equal (Some(JsonValue.Assoc([])))
    //one property object
    "{ \"prop\" : 1.25 }" |> parse |> should equal (Some(JsonValue.Assoc([JsonProperty.PropertyWithValue("prop",JsonValue.Float(1.25))])))
    //multiple properties
    "{ \"prop\" : +12, \"prop2\" : \"123\" }" 
        |> parse 
        |> should equal (Some(JsonValue.Assoc([JsonProperty.PropertyWithValue("prop",JsonValue.Int(12));JsonProperty.PropertyWithValue("prop2",JsonValue.String("123"))])))
    //property of type array 
    "{ \"prop\" : [1,23] }" |> parse |> should equal (Some(JsonValue.Assoc([JsonProperty.PropertyWithValue("prop",JsonValue.Array([JsonValue.Int(1); JsonValue.Int(23)]))])))

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
                                                [(JsonProperty.PropertyWithValue("title", JsonValue.String("Cities")));
                                                  JsonProperty.PropertyWithValue("cities", JsonValue.Array([
                                                                                                              JsonValue.Assoc([
                                                                                                                              JsonProperty.PropertyWithValue("name",JsonValue.String("Chicago"));
                                                                                                                              JsonProperty.PropertyWithValue("zips",JsonValue.Array([JsonValue.Int(60601);JsonValue.Int(60600)]))
                                                                                                              ]);
                                                                                                              JsonValue.Assoc([
                                                                                                                              JsonProperty.PropertyWithValue("name",JsonValue.String("New York"));
                                                                                                                              JsonProperty.PropertyWithValue("zips",JsonValue.Array([JsonValue.Int(10001)]))
                                                ])]))])))


[<Test>]
let ``parse huge json in less then half seconds`` () =
    let json = File.ReadAllText "hugeJson.json"

    let stopWatch = System.Diagnostics.Stopwatch.StartNew()
    json |> parse |> ignore
    stopWatch.Stop()

    //System.Console.WriteLine("Parse {0} lines of json {1} ms",(countSubstring json "\n"),stopWatch.ElapsedMilliseconds)

    stopWatch.ElapsedMilliseconds |> should be (lessThan 500)

[<Test>]
let ``parse random complex json without parse error`` () =
    let json = File.ReadAllText "randomComplexTestsJson.json"

    json |> parse |> ignore

[<Test>]
let ``can parse json terminated before ending }`` () = 
    let json = "{ \"prop\" : 1"
    let res = json |> parse

    res |> should equal (Some(JsonValue.Assoc([JsonProperty.PropertyWithValue("prop",JsonValue.Int(1))])))

[<Test>]
let ``can parse json ending with colon`` () = 
    let json = "{ \"prop\" : "
    let res = json |> parse

    res |> should equal (Some(JsonValue.Assoc([JsonProperty.PropertyNameWithColon("prop")])))
    
[<Test>]
let ``can parse json after property name`` () = 
    let json = "{ \"prop\" "
    let res = json |> parse

    res |> should equal (Some(JsonValue.Assoc([JsonProperty.PropertyName("prop")])))

[<Test>]
let ``can parse json terminated in the middle of array`` () = 
    let json = "{ \"prop\" : [ 1,2 "
    let res = json |> parse

    res |> should equal (Some(JsonValue.Assoc([JsonProperty.PropertyWithValue("prop",JsonValue.Array([JsonValue.Int(1);JsonValue.Int(2)]))])))

[<Test>]
let ``can parse json terminated in the middle of array with colon`` () = 
    let json = "{ \"prop\" : [ 1,2, "
    let res = json |> parse

    res |> should equal (Some(JsonValue.Assoc([JsonProperty.PropertyWithValue("prop",JsonValue.Array([JsonValue.Int(1);JsonValue.Int(2)]))])))

[<Test>]
let ``can parse json terminated in the middle with incomplete object`` () = 
    let json = "{ \"prop\" : [ { \"x\"  "
    let res = json |> parse

    res |> should equal (Some(JsonValue.Assoc([JsonProperty.PropertyWithValue("prop",JsonValue.Array([JsonValue.Assoc([JsonProperty.PropertyName("x")])]))])))

[<Test>]
let ``parser json ending with object terminated with comma after full property`` () = 
    let json = "[{\"id\": 1,\"name\": \"Kathie Steele\"},{\"id\": 2,"
    let res = json |> parse

    res |> should equal (Some(JsonValue.Array([
                                                 JsonValue.Assoc([
                                                                 JsonProperty.PropertyWithValue("id",JsonValue.Int(1));
                                                                 JsonProperty.PropertyWithValue("name", JsonValue.String("Kathie Steele"))
                                                 ]);
                                                 JsonValue.Assoc([
                                                                 JsonProperty.PropertyWithValue("id",JsonValue.Int(2));
                                                 ])
    ])))

[<Test>]
let ``can parse json terminated in the middle of property name`` () = 
    let json = "{ \"pro"
    let res = json |> parse

    res |> should equal (Some(JsonValue.Assoc([JsonProperty.UnfinishedPropertyName("pro")])))

[<Test>]
let ``can parse json terminated after colon`` () = 
    let json = "{ \"pro\" :"
    let json2 = "{ \"pro\" : "
    let json3 = "{ \"pro\":"
    let json4 = "{ \"p\" : 1, \"pro\":"
    let res = json |> parse
    let res2 = json2 |> parse
    let res3 = json3 |> parse
    let res4 = json4 |> parse

    res |> should equal (Some(JsonValue.Assoc([JsonProperty.PropertyNameWithColon("pro")])))
    res2 |> should equal (Some(JsonValue.Assoc([JsonProperty.PropertyNameWithColon("pro")])))
    res3 |> should equal (Some(JsonValue.Assoc([JsonProperty.PropertyNameWithColon("pro")])))
    res4 |> should equal (Some(JsonValue.Assoc([JsonProperty.PropertyWithValue("p",JsonValue.Int(1));JsonProperty.PropertyNameWithColon("pro")])))


[<Test>]
let ``can parse json terminated in a random place`` () = 
    let rnd = System.Random(System.DateTime.Now.Millisecond)
    let json = File.ReadAllText "randomComplexTestsJson.json"
    let lines = json.Split('\n') 
    let lineCount = lines |> Seq.length
    let watch = System.Diagnostics.Stopwatch.StartNew()

    let testAsync (i) = 
        async {
                let lineNum = rnd.Next(2,lineCount)
                let line = Seq.nth lineNum lines
                let lineLen = String.length line
                let columnNum = rnd.Next(0,lineLen)
                let fullLines = lines |> Seq.take (lineNum-1)
                let json = (String.concat "\n" fullLines) + line.Substring(0,columnNum)

                try 
                    json |> parse  |> ignore
                with 
                | _ -> Assert.Fail ("Parser error for JSON: " + json)
        }

    [1..1000] |> Seq.map testAsync |> Async.Parallel |> Async.RunSynchronously |> ignore


    watch.Stop()
    //printfn "total time [ms]: %d" watch.ElapsedMilliseconds

    //fool test runner to not ignore this test
    Assert.True true

[<Test>]
let ``can parse json terminated at any point`` () =
    let json = @"{""b1"": true,""b2"": false,""abs"": [ true, false ],""nu"": null,""ans"": [ null, null ],""in"": -12,""ais"": [ -12, 1 ],""fl"": -123.23E-12,""afs"": [ -123.23E-12, +123.23E-12],""st"": ""st fa\"" f\"" f"",""ass"": [ ""str"", ""fas"", ""\"" f"" ]}"

    let watch = System.Diagnostics.Stopwatch.StartNew()

    let testAsync i = 
        async {
            let subJson = json.Substring(0,i);
            try
                subJson |> parse |> ignore
            with
            | _ -> Assert.Fail ("Parser error for JSON: " + json)
        }

    [1..(json.Length-1)] |> Seq.map testAsync |> Async.Parallel |> Async.RunSynchronously |> ignore

    watch.Stop()

    //printfn "total time [ms]: %d" watch.ElapsedMilliseconds
    //fool test runner to not ignore this test
    Assert.True true
        

        
