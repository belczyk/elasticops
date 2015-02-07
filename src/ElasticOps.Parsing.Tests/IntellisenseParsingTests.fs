module IntellisenseParsingTests

open NUnit.Framework
open FsUnit
open ElasticOps.Parsing.Structures
open ElasticOps.Parsing.Processing  

[<Test>]
let ``parse object with property with default completion mode``() =
     @"{ ""prop"" : {}}" |> parseIntellisense |> should equal (Some(IntellisenseValue.Assoc([IntellisenseProperty.Property("prop","|empty_object|", IntellisenseValue.Assoc([]))])))

[<Test>]
let ``parse anyproperty token with default completion mode``() =
         @"{ ""prop"" : { AnyProperty : {} }}" |> parseIntellisense |> should equal (Some(IntellisenseValue.Assoc([IntellisenseProperty.Property("prop","|empty_object|", IntellisenseValue.Assoc([AnyProperty(IntellisenseValue.Assoc([]))]))])))

[<Test>]
let ``parse anypath token with default completion mode``() =
         @"{ ""prop"" : { AnyPath : {} }}" |> parseIntellisense |> should equal (Some(IntellisenseValue.Assoc([IntellisenseProperty.Property("prop","|empty_object|", IntellisenseValue.Assoc([AnyPath(IntellisenseValue.Assoc([]))]))])))

[<Test>]
let ``parse completion mode``() =
     @"{ ""prop"" |colon| : {}}" |> parseIntellisense |> should equal (Some(IntellisenseValue.Assoc([IntellisenseProperty.Property("prop","|colon|", IntellisenseValue.Assoc([]))])))


[<Test>]
let ``parse completion mode 2``() =
     @"{ ""prop"" |empty_object| : {}, ""prop2"" |empty_array| : {} }" |> parseIntellisense |> should equal (Some(IntellisenseValue.Assoc([IntellisenseProperty.Property("prop","|empty_object|", IntellisenseValue.Assoc([]));IntellisenseProperty.Property("prop2","|empty_array|", IntellisenseValue.Assoc([]))])))
