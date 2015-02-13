namespace ElasticOps.Commands

    open System
    open System.Globalization
    
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
            String.Format(CultureInfo.InvariantCulture,"{0}.{1}.{2}.{3}",this.major, this.getPart(this.minor), this.getPart(this.patch), this.getPart(this.build))
    
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
    