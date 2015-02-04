namespace ElasticOps
  module Intellisense = begin
    type Mode =
      | PropertyName of string
      | Value
      | Snippet

    type Suggestion = {
       Text: string;
       Mode: Mode;
    }


    type Context = {
       OriginalCode: string;
       CodeTillCaret: string;
       CodeFromCaret: string;
       ParseTree: Parsing.Structures.JsonValue option;
       OriginalCaretPosition: int * int;
       NewCaretPosition: int * int;
       NewText: string;
    }

    val TrySuggest : text:string -> caretLine:int -> caretColumn:int -> (Context * Suggestion list option)
    val Complete : context:Context -> suggestion:Suggestion -> Context

  end

