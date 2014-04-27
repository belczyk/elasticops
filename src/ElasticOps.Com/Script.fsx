#I @"E:\SourceTree\elasticops\src\ElasticOps.Com\bin\Debug"

open System.Reflection
open System
open System.Linq
open ElasticOps.Com

let types = Assembly.Load("ElasticOps.Com").GetTypes();


types 
|> List.ofArray 
|> List.filter  (fun t -> t.IsSealed && a.IsAbstract )
|> List.filter  (fun t -> t.GetCustomAttributes().Any(fun a -> a.GetType() = typedefof<CommandsHanlders>) )
|> List.map (fun a -> a.Name)
|> List.map Console.WriteLine 


