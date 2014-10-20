Elastic Ops
==========



<img src="https://ci.appveyor.com/api/projects/status/uvu9ymptbd1lnfjw?svg=true" height="25px" />

The project started from real need for a tool that I can copy on server an use to quickly asses cluster state. Most of the managment tools for ElasticSearch are Chrom plugins or online sites. It's offen not possible to reach internet from our client servers or to install Chrome but on the other hand I can copy whaterver I want ... (bloody corporate rules).

It became my pet project (also [Weronika Łabaj](https://github.com/weralabaj) helped me a lot) and test field for ideas. Front end is build in `C#, WPF, MVVM` and back end is written in `F#, FSharp.Data`. Initailly backend was also in C# but we switched to F# as it's more sutiable for JSON processing.

Ideas explored in the project:
* F#, C# integration (success) 
* Working with multi version REST API (success, read more about it on [my blog post](http://belczyk.com/2014/06/working-effectively-with-multi-version-apis/)
* intellisense system for ElasticSearch DSL writen in F#; Parsing partial and invalid JSON to support intellisense. (looks promising, huge topic though, in progress)

I'm hoping to add below features:
* Viewing cluster, nodes and indices status and health information (DONE)
* Indices managment 
* Quering (IN PROGRESS)
* Running any REST call to ElasticSearch
* Reviewing _analyze endpoint results 
* Backup and restore 
* Indexing SQL tables 
* Reviewing mappings

![ElsticOps main screen](/docs/images/elastic.png)
