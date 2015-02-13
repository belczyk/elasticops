[<AutoOpen>]
module ElasticOps.Commands.CList

    open System.Linq

    let ofSeq (seq : seq<_>)=
        seq.ToList()

    let ofList (list : _ list ) =
        list.ToList()