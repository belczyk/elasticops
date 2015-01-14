namespace ElasticOps.Com
open System

type ErrorOccuredEvent(msg) =
    member this.ErrorMessage : String = msg

type RefreashEvent() = class end

type GoToStudioEvent() = class end