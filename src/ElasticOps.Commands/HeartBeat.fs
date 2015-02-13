namespace ElasticOps.Commands

    [<AllowNullLiteral>]
    type HeartBeat( isAlive : bool , version : string ) =
        member val IsAlive = isAlive with get,set
        member val Version = version with get,set

        new(isAlive : bool) = HeartBeat(isAlive,null)
