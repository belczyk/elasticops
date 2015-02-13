namespace ElasticOps.Com

    open FSharp.Data
    open FSharp.Data.JsonExtensions
    open System
    
    [<AllowNullLiteral>]
    type Connection(clusterUri : Uri) =
        let mutable uri : Uri = clusterUri
        let mutable isConnected = false
        let mutable uncheckedUri = false
        let mutable version : ElasticOps.Com.Version = null
    
        member val DiskVersion = (null : ElasticOps.Com.Version) with get,set
    
        member x.GetVersion uri = 
            if x.IsOfflineMode then 
                (Some x.DiskVersion)
            else
                try
                    let statusReq = System.Net.WebRequest.Create(uri.ToString());
                    let statusRes = statusReq.GetResponse()
                    let stream = statusRes.GetResponseStream()
                    let reader = new System.IO.StreamReader(stream)
                    let response = JsonValue.Parse (reader.ReadToEnd())
    
                    match response?status.AsString() with
                        | "200" -> Some (ElasticOps.Com.Version.FromString(response?version?number.AsString()))
                        | _ -> None
                with
                | :? System.Net.WebException -> None
                | _ -> None
    
        member x.Init() = 
            match x.GetVersion(uri) with 
                            | Some v -> version <- v
                                        isConnected <- true
                                        uncheckedUri <- false
                                        true
                            | None -> isConnected <- false
                                      uncheckedUri <- false
                                      false
    
        member val IsOfflineMode = false with get,set
        member val IsTrackEnabled = false with get,set
        member val SaveResultToDisk = false with get,set
        member val SavePath = String.Empty with get,set
        member val ReadPath = String.Empty with get,set
    
        member x.Version  with get() = 
                                        if (version = null) then 
                                            x.Init() |> ignore
                                        version
    
        member x.ClusterUri  = uri
    
        member x.SetClusterUri sUri =
            Uri.TryCreate(sUri,UriKind.Absolute,&uri) |> ignore
            uncheckedUri <- true
    
        member x.IsConnected 
            with get () = 
                if not uncheckedUri then
                    isConnected
                else
                    x.Init()
    
        new() = Connection(null)
    