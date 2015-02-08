namespace ElasticOps.Com

open System.Collections.Generic
open System

[<AllowNullLiteral>]
type ClusterCounters(i, d, n) =
    member val Indices = i with get,set
    member val Documents = d with get,set
    member val Nodes = n with get,set


type NodeInfo = { Name: string; 
                  Hostname: string; 
                  HttpAddress: string; 
                  OS: list<KeyValuePair<string, string>>; 
                  CPU: list<KeyValuePair<string, string>>; 
                  Settings: list<KeyValuePair<string, string>>}

type IndexInfo = { Name: string; 
                   State: string; 
                   Types: List<KeyValuePair<string, string>>;
                   Settings: List<KeyValuePair<string, string>>;
                   IsOpen : bool }

type DocumentInfo = { Name : string; Count : int}

