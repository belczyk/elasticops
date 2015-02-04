namespace ElasticOps
  module Intellisense = begin

    val TrySuggest : text:string -> caretLine:int -> caretColumn:int -> (Context * Suggestion list option)
    val Complete : context:Context -> suggestion:Suggestion -> Context

  end

