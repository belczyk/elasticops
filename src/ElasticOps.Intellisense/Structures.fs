namespace ElasticOps
    open ElasticOps.Parsing.Structures
    open ElasticOps.Parsing.Processing
    open Microsoft.FSharp.Core

    type Mode =
            | PropertyName of string
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
                let tree = parse json
                let codeTillCaret = String.substring json caretLine caretColumn
            
                {
                  OriginalCode = json; 
                  OriginalCaretPosition = (caretLine,caretColumn); 
                  CodeFromCaret = null; 
                  CodeTillCaret = codeTillCaret; 
                  ParseTree = tree; 
                  NewCaretPosition = (caretLine, caretColumn) ;
                  NewText = null;
                }

        type DSLPathNodes =
        | Object
        | Property of string 
        | Array 
        | Value of JsonValue

    

