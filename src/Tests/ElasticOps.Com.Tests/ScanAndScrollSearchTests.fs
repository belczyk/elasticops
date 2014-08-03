module ElasticOps.Com.ScanAndScrollSearchTests

open NUnit.Framework
open ElasticOps.Com.Infrastructure
open Caliburn.Micro
open Foq 
open ElasticOps.Com.ScanAndScrollSearch
open ElasticOps.Com.CommonTypes
open System

let givenBus () = 
    (new CommandBus((new Mock<Caliburn.Micro.IEventAggregator>()).Create()))


[<Test>]
let ``extracts ``() =
    let bus = givenBus()
    let res = bus.Execute ((new ScrollSearchCommand(new Connection(new Uri("http://localhost:9200")),null,"ab.monowai.olympic",1000)))
    1
    //type ScrollSearchCommand(connection,scrollId, index,size) = 
    //new Connection(new Uri("http://localhost:9200"))),null,"ab.monowai.olympic",1000