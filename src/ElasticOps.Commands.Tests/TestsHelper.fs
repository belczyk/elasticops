[<AutoOpen>]
module TestHelpers
open Config
open ElasticOps.Com
open System.IO
open System

let config = new CommandsTestsConfig()
config.Load("Config.yaml")

let getConnectionsFromDisk(set : string option) =
    let readPath = match set with
                    | None -> config.ReadPath+config.DefaultSetName
                    | Some s -> config.ReadPath+s

    let dir = new DirectoryInfo(readPath)


    dir.GetDirectories() 
        |> List.ofArray 
        |> List.map (fun x -> Version.FromString x.Name)
        |> List.map (fun ver -> 
                            let connection = new Connection()
                            connection.IsOfflineMode <- true
                            connection.DiskVersion <- ver
                            connection.ReadPath <- readPath
                            connection )

let getConnectionsFromConfig(set : string option) = 
    let savePath = match set with
                    | None -> config.SavePath+config.DefaultSetName
                    | Some s -> config.SavePath+s

    config.ElasticSearchNodesEndpoints 
        |> List.ofSeq
        |> List.map (fun x -> 
                            let connection = new Connection(x)
                            connection.SavePath <- savePath
                            connection.IsOfflineMode <- false
                            connection.SaveResultToDisk <- true
                            connection
                            )
    
let connections(set : string option) = 
    if config.IsOfflineMode then
        getConnectionsFromDisk(set)
    else
        getConnectionsFromConfig(set)

let executeForAllConnectionsOverSet' (createCommand : Connection -> 'T when 'T :> Command<'R>) (set : string option) = 
    let cons = connections(set)
    let bus = new CommandBus()

    let results = cons |> List.map createCommand  |> List.map  bus.Execute

    results


let executeForAllConnections (createCommand : Connection -> 'T when 'T :> Command<'R>) = 
    executeForAllConnectionsOverSet' createCommand None
    
let executeForAllConnectionsOverSet (set : string) (createCommand : Connection -> 'T when 'T :> Command<'R>)  = 
    executeForAllConnectionsOverSet' createCommand (Some set)

