module LexerTests 

open NUnit.Framework
open FsUnit
open Microsoft.FSharp.Text.Lexing
open ElasticOps.Parsing.Structures
open ElasticOps.Parsing.Processing  





[<Test>]
let ``parse values ``() =
    "true"  |> parse |> should equal (Some(jsonValue.Bool(true)))
    "false"  |> parse |> should equal (Some(jsonValue.Bool(false)))
    "1"  |> parse |> should equal (Some(jsonValue.Int(1)))
    "1.23"  |> parse |> should equal (Some(jsonValue.Float(1.23)))
    "[1,2,3]"  |> parse |> should equal (Some(jsonValue.List([jsonValue.Int(1);jsonValue.Int(2);jsonValue.Int(3)])))
    "{}" |> parse |> should equal (Some(jsonValue.Assoc([])))
    let res = "{\"a\" : 1,}" |> parse //|> should equal (Some(jsonValue.Assoc([])))
    "\"abc\"" |> parse |> should equal (Some(jsonValue.String("abc")))
    1




//[<Test>]
//let ``can tokenzie complete simple one prop JSON``() =
//    let res = "{ ""prop\" : 12 }"  |> parseJson
//    res |> should equal (Assoc([{Name = "prop"; Value = Int(12);}]))
//
//
//[<Test>]
//let ``can tokenzie complete simple two prop JSON``() =
//    let res = "{ \"prop\" : 12, \"prop2\" : \"123\" }"  |> parseJson
//    res |> should equal (Assoc([{Name = "prop"; Value = Int(12);}]))
