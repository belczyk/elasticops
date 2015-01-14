module ClusterInfoTests

open FsUnit
open NUnit.Framework
open System 
open System.Collections.Generic
open Foq
open Caliburn.Micro
open ElasticOps.Com
open ElasticOps.Com.ClusterInfo

let clientMock = new Mock<IRESTClient>()

let exampleIndexMappingsResponse = """{
   "docs": {
      "mappings": {
         "docs": {
            "properties": {
               "pdfdoc": {
                  "type": "attachment",
                  "path": "full",
                  "fields": {

                     "author": {
                        "type": "string"
                     },
                     "title": {
                        "type": "string"
                     }
                  }
               }
            }
         },
         "docs2": {
            "properties": {
               "seb": {
                  "type": "long"
               }
            }
         }
      }
   }
}"""
//let connection = new Connection(new Uri("http://localhost:9200"))
//
//[<Test>]
//let ``ListTypes retrieves all type names``() =
//    let command = new ListTypesCommand(connection,"docs")
//    let commandBus = new CommandBus(new EventAggregator(),clientMock.Setup(fun x -> <@ x.GET(any(),any()) @>).Returns(exampleIndexMappingsResponse).Create())
//
//    let res = commandBus.Execute(command)
//
//    res.Result |> should equal (CList.ofList ["docs";"docs2"])
//

