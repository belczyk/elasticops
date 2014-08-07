module Json.AST.Tests.TraverseTests


open Json.AST.Parse
open FsUnit
open NUnit.Framework
open System 
open Json.AST.Traverse

[<Test>]
let ``traverse find finds objects in a tree``() =
    let (res,rest) = (parseNextObject ("
    {
   \"query\": {
      \"match_all\": {}
   },
   \"facets\": {
      \"isactive\": {
         \"terms\": {
            \"field\": \"isActive\"
         }
      }
   }
}"|> toCharList))
    let foundObj = find res "facets.isactive"

    foundObj |>
        should equal (Part.Object([Part.Property("terms",
                                            Part.Object([Part.Property("field",Value(Value.String("isActive")))]))]))

[<Test>]
let ``traverse find finds objects in a tree 2``() =
    let (res,rest) = (parseNextObject ("
    {
   \"query\": {
      \"match_all\": {}
   },
   \"facets\": {
      \"isactive\": {
         \"terms\": {
            \"field\": \"isActive\"
         }
      }
   }
}"|> toCharList))
    let foundObj = find res "query.match_all"

    foundObj |>
        should equal (Part.Object([]))


[<Test>]
let ``traverse find finds values in a tree``() =
    let (res,rest) = (parseNextObject ("
    {
   \"query\": {
      \"match_all\": {}
   },
   \"facets\": {
      \"isactive\": {
         \"terms\": {
            \"field\": \"isActive\"
         }
      }
   }
}"|> toCharList))
    let foundObj = find res "facets.isactive.terms.field"

    foundObj |>
        should equal (Part.Value(Value.String("isActive")))
