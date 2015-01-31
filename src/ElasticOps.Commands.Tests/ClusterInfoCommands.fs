module ClsterInfoCommandsTests
open FsUnit
open NUnit.Framework
open ElasticOps.Com

//    new ClusterInfo.HealthCommand(connection) |> bus.Execute |> ignore
//    new ClusterInfo.ClusterCountersCommand(connection) |> bus.Execute |> ignore
//    new ClusterInfo.NodesInfoCommand(connection) |> bus.Execute |> ignore
//    new ClusterInfo.IndicesInfoCommand(connection) |> bus.Execute |> ignore
//    new ClusterInfo.DocumentsInfoCommand(connection) |> bus.Execute |> ignore
//    new ClusterInfo.IsAliveCommand(connection) |> bus.Execute |> ignore
//    new ClusterInfo.ListIndicesCommand(connection) |> bus.Execute |> ignore
//    new ClusterInfo.ListTypesCommand(connection, "products") |> bus.Execute |> ignore
//    new ClusterInfo.GetMappingCommand(connection, "products","book") |> bus.Execute |> ignore


[<Test>]
let ``Can execute HealthCommand without error`` () =
    (fun con -> new ClusterInfo.HealthCommand(con))
        |> executeForAllConnections 

    