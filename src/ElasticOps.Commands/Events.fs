namespace ElasticOps.Com
open System
open Serilog.Events

type ErrorOccuredEvent(msg) =
    member this.ErrorMessage : String = msg

type RefreshEvent() = class end

type GoToStudioEvent() = class end  

type ThemeChangedEvent(theme : String, isDark) = 
    member this.IsDark : bool = isDark
    member this.Theme : string = theme

type AccentChangedEvent(accent) =
    member this.Accent : String = accent

type LogEntryCreatedEvent (logEvent : LogEvent) = 
    member this.LogEvent : LogEvent = logEvent

type NewConnectionEvent (url : String) =
    member this.URL = url

type PreviewValueEvent (value : String) = 
    member this.Value = value