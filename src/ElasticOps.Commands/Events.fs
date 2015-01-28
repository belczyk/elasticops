namespace ElasticOps.Com
open System

type ErrorOccuredEvent(msg) =
    member this.ErrorMessage : String = msg

type RefreashEvent() = class end

type GoToStudioEvent() = class end  

type ThemeChangedEvent(theme : String, isDark) = 
    member this.IsDark : bool = isDark
    member this.Theme : string = theme

type AccentChangedEvent(accent) =
    member this.Accent : String = accent

