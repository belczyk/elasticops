module LexerTests 

open NUnit.Framework
open FsUnit
open Lexer
open Microsoft.FSharp.Text.Lexing
open ElasticOps.Parsing.Processing
open ElasticOps.Parsing.Structures






//[<Test>]
//let ``can tokenzie complete simple one prop JSON``() =
//    let res = "{ \"prop\" : 12 }"  |> parseJson
//    res |> should equal (Assoc([{Name = "prop"; Value = Int(12);}]))
//
//
//[<Test>]
//let ``can tokenzie complete simple two prop JSON``() =
//    let res = "{ \"prop\" : 12, \"prop2\" : \"123\" }"  |> parseJson
//    res |> should equal (Assoc([{Name = "prop"; Value = Int(12);}]))
