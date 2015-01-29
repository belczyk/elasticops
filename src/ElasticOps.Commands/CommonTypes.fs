namespace ElasticOps.Com
open System
open FSharp.Data
open FSharp.Data.JsonExtensions
open System.Web

[<AllowNullLiteral>]
type Version(major : int,?minor : int,?patch : int,?build : int) =
    member x.major = major
    member x.minor = minor
    member x.patch = patch
    member x.build = build
    member x.ToTuple() = 
        (major,minor,patch,build)

    new (major: int) = Version(major,0,0,0)
    new (major : int, minor : int) = Version(major,minor,0,0)
    new (major : int, minor : int,patch : int) = Version(major,minor,patch,0)
    
    override x.Equals (o ) =
            let other = o :?> Version
            x.major = other.major && x.minor = other.minor && x.patch = other.patch && x.build = other.build

    override x.GetHashCode() =
        let hash o h = h * 486187739 + o.GetHashCode()

        (hash x.major 17) |> hash x.minor |> hash x.patch |> hash x.build 

    member x.getPart p = 
        match p with
            | Some v -> v
            | None -> 0

    override this.ToString () =
        String.Format("{0}.{1}.{2}.{3}",this.major, this.getPart(this.minor), this.getPart(this.patch), this.getPart(this.build))

    static member FromString(version : string) = 
        match version with 
        | null |"" -> new Version(0)
        | _ -> let parts = version.Split '.'
               match (parts |> Seq.map (fun v -> Int32.Parse(v)) |> List.ofSeq)  with 
               | m::mi::p::b::_ -> new Version(m,mi,p,b)
               | m::mi::p::_ -> new Version(m,mi,p)
               | m::mi::_ -> new Version(m,mi)
               | m::_ -> new Version(m)
               | _ -> new Version(0)

    interface System.IComparable  with
        member this.CompareTo(other ) =
            match (this.ToTuple(),(other :?> Version).ToTuple()) with
            | ((m,mi,p,b),(m2,mi2,p2,b2)) when m=m2 && mi=mi2 && p=p2 && b=b2 -> 0
            | (m,_,_,_),(m2,_,_,_) when m > m2 -> 1
            | (m,_,_,_),(m2,_,_,_) when m < m2 -> -1
            | (_,m,_,_),(_,m2,_,_) when m > m2 -> 1
            | (_,m,_,_),(_,m2,_,_) when m < m2 -> -1
            | (_,_,p,_),(_,_,p2,_) when p > p2 -> 1
            | (_,_,p,_),(_,_,p2,_) when p < p2 -> -1
            | (_,_,_,b),(_,_,_,b2) when b > b2 -> 1
            | (_,_,_,b),(_,_,_,b2) when b < b2 -> -1
            | _ -> raise (Exception "unknown version combination when comparing versions")

type CommandResult<'T when 'T : null> (result : 'T, success : Boolean, errorMessage : String, ex : Exception) =
    member x.Result = result
    member x.Success = success 
    member x.ErrorMessage = errorMessage 
    member x.Exception = ex
    member x.Failed 
        with get() = not x.Success
        
    new (result : 'T) = CommandResult(result,true,null,null)
    new (errorMessage : string ) = CommandResult(null,false,errorMessage,null)
    new (errorMessage : string, ex : Exception) = CommandResult(null,false,errorMessage,ex)


[<AllowNullLiteral>]
type Connection(clusterUri : Uri) =
    let mutable uri : Uri = clusterUri
    let mutable isConnected = false
    let mutable uncheckedUri = false
    let mutable version : Version = null

    let getVersion uri = 
        try
            let statusReq = System.Net.WebRequest.Create(uri.ToString());
            let statusRes = statusReq.GetResponse()
            let stream = statusRes.GetResponseStream()
            let reader = new System.IO.StreamReader(stream)
            let response = JsonValue.Parse (reader.ReadToEnd())

            match response?status.AsString() with
                | "200" -> Some (Version.FromString(response?version?number.AsString()))
                | _ -> None
        with
        | :? System.Net.WebException -> None
        | _ -> None

    member x.Version = version

    member x.ClusterUri  = uri

    member x.SetClusterUri sUri =
        Uri.TryCreate(sUri,UriKind.Absolute,&uri) |> ignore
        uncheckedUri <- true

    member x.IsConnected 
        with get () = 
            if not uncheckedUri then
                isConnected
            else
                match getVersion(uri) with 
                    | Some v -> version <- v
                                isConnected <- true
                                uncheckedUri <- false
                                true
                    | None -> isConnected <- false
                              uncheckedUri <- false
                              false
    new() = Connection(null)

type HeartBeat( isAlive : bool , version : string ) =
    member x.IsAlive = isAlive 
    member x.Version = version

    new(isAlive : bool) = HeartBeat(isAlive,null)


type Command<'T>(connection : Connection) = 
    new (connection) = Command<'T>(connection)
    member x.Connection = connection
    member x.ClusterUri = match x.Connection with 
                            | null -> null
                            | _ -> x.Connection.ClusterUri

    member x.Version = x.Connection.Version

type IRESTClient = 
        abstract GET : string -> string
        abstract GET : Uri * string -> string
        abstract POST : string * string -> string
        abstract POST : Uri * string * string -> string
        abstract POSTJson : string * string -> string
        abstract POSTJson : Uri * string * string -> string

type RESTClient() = 
    member private this.combineUri ( uri : Uri) endpoint =
        match endpoint with 
        | null | "" -> uri.ToString()
        | _ -> (new Uri(uri,new Uri(endpoint,UriKind.Relative))).ToString()

    interface IRESTClient with 
        member this.GET(url : string) = 
            url |> Http.RequestString  

        member this.GET(uri, endpoint) =
            this.combineUri uri endpoint |> Http.RequestString

        member this.POST(url : string, body : string) = 
            Http.RequestString ( url, httpMethod = "POST",
                        body   = TextRequest body
                        )
        member this.POST(uri, endpoint, body : string) = 
            Http.RequestString ( (this.combineUri uri endpoint), httpMethod = "POST",
                        body   = TextRequest body
                        )
        member this.POSTJson(url : string, body : string) = 
            Http.RequestString ( url, httpMethod = "POST",
                        headers = [ "Accept", "application/json" ],
                        body   = TextRequest body
                        ) 
        member this.POSTJson(uri, endpoint, body : string) = 
            Http.RequestString ( (this.combineUri uri endpoint), httpMethod = "POST",
                        headers = [ "Accept", "application/json" ],
                        body   = TextRequest body
                        ) 