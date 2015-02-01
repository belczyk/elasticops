module ClsterInfoCommandsTests
open FsUnit
open NUnit.Framework
open ElasticOps.Com

    module OverDefaultSet =
        [<Test>]
        let ``HealthCommand can be executed without errors`` () =
            (fun con -> new ClusterInfo.HealthCommand(con))
                |> executeForAllConnections 
                |> List.map (fun x -> Assert.IsTrue(x.Success,x.ErrorMessage))
                |> ignore

        [<Test>]
        let ``ClusterCountersCommand can be executed without errors`` () =
            (fun con -> new ClusterInfo.ClusterCountersCommand(con))
                |> executeForAllConnections 
                |> List.map (fun x -> 
                                Assert.IsTrue(x.Success,x.ErrorMessage)
                                Assert.AreEqual(1,x.Result.Nodes)
                                Assert.AreEqual(110000,x.Result.Documents)
                                Assert.AreEqual(1,x.Result.Indices)
                                )
                |> ignore

        [<Test>]
        let ``NodesInfoCommand can be executed without errors`` () =
            (fun con -> new ClusterInfo.NodesInfoCommand(con))
                |> executeForAllConnections 
                |> List.map (fun x -> Assert.IsTrue(x.Success,x.ErrorMessage))
                |> ignore

        [<Test>]
        let ``IndicesInfoCommand can be executed without errors`` () =
            (fun con -> new ClusterInfo.IndicesInfoCommand(con))
                |> executeForAllConnections 
                |> List.map (fun x -> Assert.IsTrue(x.Success,x.ErrorMessage))
                |> ignore

        [<Test>]
        let ``DocumentsInfoCommand can be executed without errors`` () =
            (fun con -> new ClusterInfo.DocumentsInfoCommand(con))
                |> executeForAllConnections 
                |> List.map (fun x -> Assert.IsTrue(x.Success,x.ErrorMessage))
                |> ignore


        [<Test>]
        let ``ListIndicesCommand can be executed without errors`` () =
            (fun con -> new ClusterInfo.ListIndicesCommand(con))
                |> executeForAllConnections 
                |> List.map (fun x -> Assert.IsTrue(x.Success,x.ErrorMessage))
                |> ignore

        [<Test>]
        let ``ListTypesCommand can be executed without errors`` () =
            (fun con -> new ClusterInfo.ListTypesCommand(con,"products"))
                |> executeForAllConnections 
                |> List.map (fun x -> Assert.IsTrue(x.Success,x.ErrorMessage))
                |> ignore

        [<Test>]
        let ``GetMappingCommand can be executed without errors`` () =
            (fun con -> new ClusterInfo.GetMappingCommand(con,"products","book"))
                |> executeForAllConnections 
                |> List.map (fun x -> Assert.IsTrue(x.Success,x.ErrorMessage))
                |> ignore

    module OverEmptySet =
        [<Test>]
        let ``HealthCommand can be executed without errors over empty ElasticSearch node (no indices)`` () =
            (fun con -> new ClusterInfo.HealthCommand(con))
                |> executeForAllConnectionsOverSet "empty" 
                |> List.map (fun x -> Assert.IsTrue(x.Success,x.ErrorMessage))
                |> ignore

        [<Test>]
        let ``ClusterCountersCommand can be executed without errors over empty ElasticSearch node (no indices)`` () =
            (fun con -> new ClusterInfo.ClusterCountersCommand(con))
                |> executeForAllConnectionsOverSet "empty" 
                |> List.map (fun x -> Assert.IsTrue(x.Success,x.ErrorMessage))
                |> ignore

        [<Test>]
        let ``NodesInfoCommand can be executed without errors over empty ElasticSearch node (no indices)`` () =
            (fun con -> new ClusterInfo.NodesInfoCommand(con))
                |> executeForAllConnectionsOverSet "empty" 
                |> List.map (fun x -> Assert.IsTrue(x.Success,x.ErrorMessage))
                |> ignore

        [<Test>]
        let ``IndicesInfoCommand can be executed without errors over empty ElasticSearch node (no indices)`` () =
            (fun con -> new ClusterInfo.IndicesInfoCommand(con))
                |> executeForAllConnectionsOverSet "empty"
                |> List.map (fun x -> Assert.IsTrue(x.Success,x.ErrorMessage))
                |> ignore

