namespace ElasticOps
    open ElasticOps.Parsing.Structures
    open ElasticOps.Parsing.Processing
    open Microsoft.FSharp.Core

    type Mode =
            | Property of string
            | Value
            | Snippet

        type Suggestion = 
            {
                Text : string;
                Mode : Mode;
            }
        
        type Context = 
            {
                OriginalCode : string;
                CodeTillCaret : string;
                CodeFromCaret : string;
                ParseTree : JsonValue option;
                OriginalCaretPosition : int * int;
                NewCaretPosition : int * int;
                NewText : string
            }
            static member create json caretLine caretColumn=
                let codeTillCaret = String.substring json caretLine caretColumn
                let tree = parse codeTillCaret
            
                {
                  OriginalCode = json; 
                  OriginalCaretPosition = (caretLine,caretColumn); 
                  CodeFromCaret = null; 
                  CodeTillCaret = codeTillCaret; 
                  ParseTree = tree; 
                  NewCaretPosition = (caretLine, caretColumn) ;
                  NewText = null;
                }

        type RuleSign = 
            | UnfinishedPropertyName
            | Property of string 
            | AnyProperty

        type Rule = { Sign : RuleSign list ; Suggestions : Suggestion list}


