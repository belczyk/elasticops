[<AutoOpen>]
module TestHelpers
open Config
open ElasticOps.Com
open Caliburn.Micro 
open System.IO

let config = new CommandsTestsConfig()
config.Load("Config.yaml")

let getConnectionsFromDisk() =
    let dir = new DirectoryInfo(config.ReadPath)
    dir.GetDirectories() 
        |> List.ofArray 
        |> List.map (fun x -> Version.FromString x.Name)
        |> List.map (fun ver -> 
                            let connection = new Connection()
                            connection.IsOfflineMode <- true
                            connection.DiskVersion <- ver
                            connection.ReadPath <- config.ReadPath
                            connection )

let getConnectionsFromConfig() = 
    config.ElasticSearchNodesEndpoints 
        |> List.ofSeq
        |> List.map (fun x -> 
                            let connection = new Connection(x)
                            connection.SavePath <- config.SavePath
                            connection.IsOfflineMode <- false
                            connection.SaveResultToDisk <- true
                            connection
                            )
    
let connections() = 
    if config.IsOfflineMode then
        getConnectionsFromDisk()
    else
        getConnectionsFromConfig()


let executeForAllConnections (createCommand : Connection -> 'T when 'T :> Command<'R>) = 
    let cons = connections()
    let bus = new CommandBus(new EventAggregator())

    let results = cons |> List.map createCommand  |> List.map  bus.Execute

    
    results
    
        
