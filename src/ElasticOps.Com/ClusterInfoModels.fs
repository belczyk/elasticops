namespace ElasticOps.Com.Models

open FSharp.Data
open FSharp.Data.JsonExtensions
open System.Collections.Generic
open System
open ElasticOps.Com

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
                   Types: list<KeyValuePair<string, string>>; 
                   Settings: list<KeyValuePair<string, string>>}

