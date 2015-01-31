[<AutoOpen>]
module TestHelpers
open Config
open ElasticOps.Com
open Caliburn.Micro 
open System.IO

let config = new CommandsTestsConfig()
config.Load("Config.yaml")

let getConnectionsFromDisk() =
    let dir = new DirectoryInfo(config.DataPath)
    dir.GetDirectories() 
        |> Seq.ofArray 
        |> Seq.map (fun x -> Version.FromString x.Name)
        |> Seq.map (fun ver -> 
                            let connection = new Connection()
                            connection.IsOfflineMode <- true
                            connection.DiskVersion <- ver
                            connection.ReadPath <- config.DataPath
                            connection )

let getConnectionsFromConfig() = 
    config.ElasticSearchNodesEndpoints 
        |> Seq.map (fun x -> 
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

let execute  (createCommand : Connection -> 'T when 'T :> Command<'R>) (con : Connection) =
    let bus = new CommandBus(new EventAggregator())

    let cmd = con |> createCommand 
    let res = cmd |> bus.Execute 
    res
let executeForAllConnections (createCommand : Connection -> 'T when 'T :> Command<'R>) = 
    let cons = connections()

    for con in cons do execute  createCommand con
        
