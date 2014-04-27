namespace ElasticOps.Com.CommonTypes
open System

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
    
    override this.Equals (o ) =
            let other = o :?> Version
            this.major = other.major && this.minor = other.minor && this.patch = other.patch && this.build = other.build

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

type RequestResult<'T when 'T : null> (result : 'T, success : Boolean, errorMessage : String) =
    member x.Result = result
    member x.Success = success 
    member x.ErrorMessage = errorMessage 

    new (result : 'T) = RequestResult(result,true,null)
    new (errorMessage : string ) = RequestResult(null,false,errorMessage)

type Connection(clusterUri : Uri, version : Version) =
    member x.clusterUri = clusterUri
    member x.version = version 