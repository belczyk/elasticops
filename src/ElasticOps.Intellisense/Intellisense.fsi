namespace ElasticOps.Intellisense
  module IntellisenseEngine = begin

    val TrySuggest : text:string -> caretLine:int -> caretColumn:int -> endpoint:string -> (Context * Suggestion list option)
    val Complete : context:Context -> suggestion:Suggestion -> Context

  end