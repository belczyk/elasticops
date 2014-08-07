module Json.AST.Tests.ParseTests

open Json.AST.Parse
open FsUnit
open NUnit.Framework
open System 



[<Test>]
let ``parseNextObject returns object for {}``() =
    (parseNextObject ("{}" |> toCharList)) |> fst |> should equal (Part.Object([]))

[<Test>]
let ``parseNextObject returns object for {"prop" : "propVal" } with one property``() =
    let (res, rest) = (parseNextObject ("{\"prop\" : \"propVal\" }"|> toCharList))
    res |>
        should equal (Part.Object([Property("prop",Part.Value(Value.String("propVal")))]))

[<Test>]
let ``parseNextObject handles string excape properly``() =
    let (res, rest) = (parseNextObject (@"{""prop"" : ""propVal\""sometext in quots\"""" }"|> toCharList))
    res |>
        should equal (Part.Object([Property("prop",Part.Value(Value.String("propVal\"sometext in quots\"")))]))

[<Test>]
let ``parseNextObject returns object for {"prop" : "propVal", "prop2" : "propVal2" } with two properties``() =
    let (res,rest) = (parseNextObject ("{\"prop\" : \"propVal\", \"prop2\" : \"propVal2\" }"|> toCharList))
    res |>
        should equal (Part.Object([
                                Property("prop",Part.Value(Value.String("propVal")));
                                Property("prop2",Part.Value(Value.String("propVal2")))]))
[<Test>]
let ``parseNextObject parses arrays {"prop" : "propVal", "prop2" : "propVal2" } ``() =
    let (res,rest) = (parseNextObject ("{\"prop\" : \"propVal\", \"prop2\" : [\"val1\", \"val2\", \"val3\"] }"|> toCharList))
    res |>
        should equal (Part.Object([
                                Property("prop",Part.Value(Value.String("propVal")));
                                Property("prop2",Part.Array([Value(Value.String("val1"));Value(Value.String("val2"));Value(Value.String("val3"))]))]))



[<Test>]
let ``parseNextObject can parse double properties``() =
    let (res,rest)  = (parseNextObject ("{\"prop\" : 2.2, \"prop2\" : 2 }"|> toCharList))
    res |>
        should equal (Part.Object([
                                Property("prop",Part.Value(Value.Number(2.2)));
                                Property("prop2",Part.Value(Value.Number(2.0)))]))

[<Test>]
let ``parseNextObject can parse boolean properties``() =
    let (res,rest)  = (parseNextObject ("{\"prop\" : true \"prop2\" : false }"|> toCharList))
    res |>
        should equal (Part.Object([
                                Property("prop",Part.Value(Value.Boolean(true)));
                                Property("prop2",Part.Value(Value.Boolean(false)))]))


[<Test>]
let ``parseNextObject can parse multitype properties``() =
    let (res ,rest)= (parseNextObject ("{\"prop\" : true \"prop2\" : 45.45566, \"prop3\" : \"somestring\", \"prop4\" : false }"|> toCharList))
    res |>
        should equal (Part.Object([
                                Property("prop",Part.Value(Value.Boolean(true)));
                                Property("prop2",Part.Value(Value.Number(45.45566)));
                                Property("prop3",Part.Value(Value.String("somestring")));
                                Property("prop4",Part.Value(Value.Boolean(false)))]))

[<Test>]
let ``parseNextObject can parse nested objects``() =
    let (res,rest) = (parseNextObject ("{\"prop\" : true \"prop2\" : 45.45566, \"obj\" : {\"prop3\" :\"somestring\", \"prop4\" : false }, \"prop5\" : 2}"|> toCharList))
    res |>
        should equal (Part.Object([
                                Property("prop",Part.Value(Value.Boolean(true)));
                                Property("prop2",Part.Value(Value.Number(45.45566)));
                                Property("obj",Part.Object([Property("prop3",Part.Value(Value.String("somestring")));
                                        Property("prop4",Part.Value(Value.Boolean(false)))]));
                                Property("prop5",Part.Value(Value.Number(2.0)));
                                ]))

[<Test>]
let ``parseNextObject can parse complex objects``() =
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
    res |>
        should equal (Part.Object([
                                Property("query",
                                    Part.Object([Part.Property("match_all",Part.Object([]))]));
                                Property("facets",
                                    Part.Object([Part.Property("isactive",
                                        Part.Object([Part.Property("terms",
                                            Part.Object([Part.Property("field",Value(Value.String("isActive")))]))]))]))
                                ]))


[<Test>]
let ``parseNextObject can parse complex objects with arrays of objects``() =
    let (res,rest) = (parseNextObject ("
{
  \"must\": [
        {
          \"terms\": {
            \"FIELD\": [
              \"VALUE1\",
              \"VALUE2\"
            ]
          }
        },
        {
          \"terms\": {
            \"FIELD2\": [
              \"VALUE3\",
              \"VALUE4\"
            ]
          }
        }
      ]

}"|> toCharList))
    res |>
        should equal (Part.Object([Part.Property("must",Part.Array(
                                                        [Part.Object([
                                                            Part.Property("terms",
                                                                Part.Object([ Part.Property("FIELD",Part.Array([
                                                                    Part.Value(Value.String("VALUE1"));
                                                                    Part.Value(Value.String("VALUE2"))]))]))]);
                                                        Part.Object([Part.Property("terms",
                                                            Part.Object([Part.Property("FIELD2",
                                                                Part.Array([
                                                                    Part.Value(Value.String("VALUE3"));
                                                                    Part.Value(Value.String("VALUE4"))]))]))])])
                                            
                                )]))

[<Test>]
let ``can parse complex partial objects``() =
    let (res,rest) = (parseNextObject ("
    {
   \"query\": {
      \"match_all\": {}
   },
   \"facets\": {
      \"isactive\": {
         \"terms\": {
 "|> toCharList))
    res |>
        should equal (Part.Object([
                                Property("query",
                                    Part.Object([Part.Property("match_all",Part.Object([]))]));
                                Property("facets",
                                    Part.Object([Part.Property("isactive",
                                        Part.Object([Part.Property("terms",
                                            Part.Object([]))]))]))
                                ]))

[<Test>]
let ``can parse objects with numeric properites``() =
    let (res,rest) = (parseNextObject ("
{ 
  \"failed\" : 0,
      \"successful\" : 5,
      \"total\" : 5
  
}
 "|> toCharList))
    res |>
        should equal (Part.Object([
                        Part.Property("failed",Part.Value(Value.Number(0.0)));
                        Part.Property("successful",Part.Value(Value.Number(5.0)));
                        Part.Property("total",Part.Value(Value.Number(5.0)))]))


[<Test>]
let ``can parse objects with mix of different types of properites``() =
    let (res,rest) = (parseNextObject ("
{\"_scroll_id\":\"1\",
\"took\":1,
\"timed_out\":false,
\"_shards\":{\"total\":5,\"successful\":5,\"failed\":0}}
 "|> toCharList))
    res |>
        should equal (Part.Object([
                        Part.Property("_scroll_id",Part.Value(Value.String("1")));
                        Part.Property("took",Part.Value(Value.Number(1.0)));
                        Part.Property("timed_out",Part.Value(Value.Boolean(false)));
                        Part.Property("_shards",Part.Object([
                                                        Part.Property("total",Part.Value(Value.Number(5.0)));
                                                        Part.Property("successful",Part.Value(Value.Number(5.0)));
                                                        Part.Property("failed",Part.Value(Value.Number(0.0)))])
                                                    )
                        
                        ]))
                                        