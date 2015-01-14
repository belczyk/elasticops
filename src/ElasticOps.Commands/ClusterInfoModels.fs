namespace ElasticOps.Com

open System.Collections.Generic

type ClusterCounters = { Indices: int; 
                         Documents: int; 
                         Nodes: int }

type NodeInfo = { Name: string; 
                  Hostname: string; 
                  HttpAddress: string; 
                  OS: list<KeyValuePair<string, string>>; 
                  CPU: list<KeyValuePair<string, string>>; 
                  Settings: list<KeyValuePair<string, string>>}

type IndexInfo = { Name: string; 
                   State: string; 
                   Types: List<KeyValuePair<string, string>>;
                   Settings: List<KeyValuePair<string, string>>}

type DocumentInfo = { Name : string; Count : int}

