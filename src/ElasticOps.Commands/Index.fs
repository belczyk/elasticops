namespace ElasticOps.Com
    open ElasticOps.Com

    [<CommandsHandlers>]
    module Index =

        type CloseCommand(connection : Connection, index : string) =
            inherit Command<string>(connection)
            member val Index = index with get,set

        type OpenCommand(connection : Connection, index : string) =
            inherit Command<string>(connection)
            member val Index = index with get,set


        type FlushCommand(connection : Connection, index : string) =
            inherit Command<string>(connection)
            member val Index = index with get,set


        type RefreshCommand(connection : Connection, index : string) =
            inherit Command<string>(connection)
            member val Index = index with get,set


        type OptimizeCommand(connection : Connection, index : string) =
            inherit Command<string>(connection)
            member val Index = index with get,set


        type DeleteCommand(connection : Connection, index : string) =
            inherit Command<string>(connection)
            member val Index = index with get,set


        type ClearCacheCommand(connection : Connection, index : string) =
            inherit Command<string>(connection)
            member val Index = index with get,set

        let close (command : CloseCommand) = 
            POST command.Connection (command.Index + "/_close") ""
            

        let flush (command : FlushCommand) = 
            POST command.Connection (command.Index + "/_flush") ""
            

        let ``open`` (command : OpenCommand) = 
            POST command.Connection (command.Index + "/_open") ""
            

        let delete (command : DeleteCommand) = 
            DELETE command.Connection command.Index
            

        let clearCache (command : ClearCacheCommand) = 
            POST command.Connection (command.Index + "/_cache/clear") ""
            

        let optimize (command : OptimizeCommand) = 
            POST command.Connection (command.Index + "/_optimize") ""

        let refresh (command : RefreshCommand) = 
            POST command.Connection (command.Index + "/_refresh") ""
            
