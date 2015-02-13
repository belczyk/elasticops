namespace ElasticOps.Commands

    type Command<'T>(connection : Connection) = 
        new (connection) = Command<'T>(connection)
        member x.Connection = connection
        member x.ClusterUri = match x.Connection with 
                                | null -> null
                                | _ -> x.Connection.ClusterUri

        member x.Version = x.Connection.Version