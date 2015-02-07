// Implementation file for parser generated by fsyacc
module IntellisenseParser
#nowarn "64";; // turn off warnings that type variables used in production annotations are instantiated to concrete type
open Microsoft.FSharp.Text.Lexing
open Microsoft.FSharp.Text.Parsing.ParseHelpers
# 1 "IntellisenseParser.fsy"

open ElasticOps.Parsing.Structures


# 11 "IntellisenseParser.fs"
// This type is the type of tokens accepted by the parser
type token = 
  | EOF
  | ANY_PROPERTY
  | UNFINISHED_VALUE of (string * ElasticOps.Parsing.Structures.JsonValueType)
  | COMPLETION_MACRO of (string)
  | COMMA
  | COLON
  | RIGHT_BRACK
  | LEFT_BRACK
  | RIGHT_BRACE
  | LEFT_BRACE
  | STRING of (string)
  | ID of (string)
// This type is used to give symbolic names to token indexes, useful for error messages
type tokenId = 
    | TOKEN_EOF
    | TOKEN_ANY_PROPERTY
    | TOKEN_UNFINISHED_VALUE
    | TOKEN_COMPLETION_MACRO
    | TOKEN_COMMA
    | TOKEN_COLON
    | TOKEN_RIGHT_BRACK
    | TOKEN_LEFT_BRACK
    | TOKEN_RIGHT_BRACE
    | TOKEN_LEFT_BRACE
    | TOKEN_STRING
    | TOKEN_ID
    | TOKEN_end_of_input
    | TOKEN_error
// This type is used to give symbolic names to token indexes, useful for error messages
type nonTerminalId = 
    | NONTERM__startstart
    | NONTERM_start
    | NONTERM_prog
    | NONTERM_value
    | NONTERM_object_fields
    | NONTERM_rev_object_fields
    | NONTERM_rev_values

// This function maps tokens to integers indexes
let tagOfToken (t:token) = 
  match t with
  | EOF  -> 0 
  | ANY_PROPERTY  -> 1 
  | UNFINISHED_VALUE _ -> 2 
  | COMPLETION_MACRO _ -> 3 
  | COMMA  -> 4 
  | COLON  -> 5 
  | RIGHT_BRACK  -> 6 
  | LEFT_BRACK  -> 7 
  | RIGHT_BRACE  -> 8 
  | LEFT_BRACE  -> 9 
  | STRING _ -> 10 
  | ID _ -> 11 

// This function maps integers indexes to symbolic token ids
let tokenTagToTokenId (tokenIdx:int) = 
  match tokenIdx with
  | 0 -> TOKEN_EOF 
  | 1 -> TOKEN_ANY_PROPERTY 
  | 2 -> TOKEN_UNFINISHED_VALUE 
  | 3 -> TOKEN_COMPLETION_MACRO 
  | 4 -> TOKEN_COMMA 
  | 5 -> TOKEN_COLON 
  | 6 -> TOKEN_RIGHT_BRACK 
  | 7 -> TOKEN_LEFT_BRACK 
  | 8 -> TOKEN_RIGHT_BRACE 
  | 9 -> TOKEN_LEFT_BRACE 
  | 10 -> TOKEN_STRING 
  | 11 -> TOKEN_ID 
  | 14 -> TOKEN_end_of_input
  | 12 -> TOKEN_error
  | _ -> failwith "tokenTagToTokenId: bad token"

/// This function maps production indexes returned in syntax errors to strings representing the non terminal that would be produced by that production
let prodIdxToNonTerminal (prodIdx:int) = 
  match prodIdx with
    | 0 -> NONTERM__startstart 
    | 1 -> NONTERM_start 
    | 2 -> NONTERM_prog 
    | 3 -> NONTERM_prog 
    | 4 -> NONTERM_value 
    | 5 -> NONTERM_value 
    | 6 -> NONTERM_object_fields 
    | 7 -> NONTERM_rev_object_fields 
    | 8 -> NONTERM_rev_object_fields 
    | 9 -> NONTERM_rev_object_fields 
    | 10 -> NONTERM_rev_object_fields 
    | 11 -> NONTERM_rev_object_fields 
    | 12 -> NONTERM_rev_object_fields 
    | 13 -> NONTERM_rev_object_fields 
    | 14 -> NONTERM_rev_object_fields 
    | 15 -> NONTERM_rev_values 
    | 16 -> NONTERM_rev_values 
    | _ -> failwith "prodIdxToNonTerminal: bad production index"

let _fsyacc_endOfInputTag = 14 
let _fsyacc_tagOfErrorTerminal = 12

// This function gets the name of a token as a string
let token_to_string (t:token) = 
  match t with 
  | EOF  -> "EOF" 
  | ANY_PROPERTY  -> "ANY_PROPERTY" 
  | UNFINISHED_VALUE _ -> "UNFINISHED_VALUE" 
  | COMPLETION_MACRO _ -> "COMPLETION_MACRO" 
  | COMMA  -> "COMMA" 
  | COLON  -> "COLON" 
  | RIGHT_BRACK  -> "RIGHT_BRACK" 
  | LEFT_BRACK  -> "LEFT_BRACK" 
  | RIGHT_BRACE  -> "RIGHT_BRACE" 
  | LEFT_BRACE  -> "LEFT_BRACE" 
  | STRING _ -> "STRING" 
  | ID _ -> "ID" 

// This function gets the data carried by a token as an object
let _fsyacc_dataOfToken (t:token) = 
  match t with 
  | EOF  -> (null : System.Object) 
  | ANY_PROPERTY  -> (null : System.Object) 
  | UNFINISHED_VALUE _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | COMPLETION_MACRO _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | COMMA  -> (null : System.Object) 
  | COLON  -> (null : System.Object) 
  | RIGHT_BRACK  -> (null : System.Object) 
  | LEFT_BRACK  -> (null : System.Object) 
  | RIGHT_BRACE  -> (null : System.Object) 
  | LEFT_BRACE  -> (null : System.Object) 
  | STRING _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | ID _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
let _fsyacc_gotos = [| 0us; 65535us; 1us; 65535us; 0us; 1us; 1us; 65535us; 0us; 2us; 7us; 65535us; 0us; 4us; 10us; 11us; 14us; 15us; 17us; 18us; 20us; 21us; 23us; 24us; 26us; 27us; 1us; 65535us; 5us; 6us; 1us; 65535us; 5us; 8us; 0us; 65535us; |]
let _fsyacc_sparseGotoTableRowOffsets = [|0us; 1us; 3us; 5us; 13us; 15us; 17us; |]
let _fsyacc_stateToProdIdxsTableElements = [| 1us; 0us; 1us; 0us; 1us; 1us; 1us; 2us; 1us; 3us; 2us; 4us; 5us; 2us; 4us; 5us; 1us; 4us; 5us; 6us; 9us; 11us; 13us; 14us; 2us; 8us; 10us; 1us; 8us; 1us; 8us; 4us; 9us; 11us; 13us; 14us; 2us; 9us; 11us; 1us; 9us; 1us; 9us; 1us; 10us; 1us; 10us; 1us; 10us; 1us; 11us; 1us; 11us; 1us; 11us; 1us; 12us; 1us; 12us; 1us; 12us; 1us; 13us; 1us; 13us; 1us; 13us; |]
let _fsyacc_stateToProdIdxsTableRowOffsets = [|0us; 2us; 4us; 6us; 8us; 10us; 13us; 16us; 18us; 24us; 27us; 29us; 31us; 36us; 39us; 41us; 43us; 45us; 47us; 49us; 51us; 53us; 55us; 57us; 59us; 61us; 63us; 65us; |]
let _fsyacc_action_rows = 28
let _fsyacc_actionTableElements = [|2us; 32768us; 0us; 3us; 9us; 5us; 0us; 49152us; 0us; 16385us; 0us; 16386us; 0us; 16387us; 2us; 16391us; 1us; 22us; 10us; 9us; 1us; 16389us; 8us; 7us; 0us; 16388us; 1us; 16390us; 4us; 12us; 2us; 32768us; 3us; 16us; 5us; 10us; 1us; 32768us; 9us; 5us; 0us; 16392us; 2us; 16398us; 1us; 25us; 10us; 13us; 2us; 32768us; 3us; 19us; 5us; 14us; 1us; 32768us; 9us; 5us; 0us; 16393us; 1us; 32768us; 5us; 17us; 1us; 32768us; 9us; 5us; 0us; 16394us; 1us; 32768us; 5us; 20us; 1us; 32768us; 9us; 5us; 0us; 16395us; 1us; 32768us; 5us; 23us; 1us; 32768us; 9us; 5us; 0us; 16396us; 1us; 32768us; 5us; 26us; 1us; 32768us; 9us; 5us; 0us; 16397us; |]
let _fsyacc_actionTableRowOffsets = [|0us; 3us; 4us; 5us; 6us; 7us; 10us; 12us; 13us; 15us; 18us; 20us; 21us; 24us; 27us; 29us; 30us; 32us; 34us; 35us; 37us; 39us; 40us; 42us; 44us; 45us; 47us; 49us; |]
let _fsyacc_reductionSymbolCounts = [|1us; 1us; 1us; 1us; 3us; 2us; 1us; 0us; 3us; 5us; 4us; 6us; 3us; 5us; 2us; 1us; 3us; |]
let _fsyacc_productionToNonTerminalTable = [|0us; 1us; 2us; 2us; 3us; 3us; 4us; 5us; 5us; 5us; 5us; 5us; 5us; 5us; 5us; 6us; 6us; |]
let _fsyacc_immediateActions = [|65535us; 49152us; 16385us; 16386us; 16387us; 65535us; 65535us; 16388us; 65535us; 65535us; 65535us; 16392us; 65535us; 65535us; 65535us; 16393us; 65535us; 65535us; 16394us; 65535us; 65535us; 16395us; 65535us; 65535us; 16396us; 65535us; 65535us; 16397us; |]
let _fsyacc_reductions ()  =    [| 
# 154 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : ElasticOps.Parsing.Structures.IntellisenseValue option)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
                      raise (Microsoft.FSharp.Text.Parsing.Accept(Microsoft.FSharp.Core.Operators.box _1))
                   )
                 : '_startstart));
# 163 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'prog)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 25 "IntellisenseParser.fsy"
                                    _1 
                   )
# 25 "IntellisenseParser.fsy"
                 : ElasticOps.Parsing.Structures.IntellisenseValue option));
# 174 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 28 "IntellisenseParser.fsy"
                                 None 
                   )
# 28 "IntellisenseParser.fsy"
                 : 'prog));
# 184 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'value)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 29 "IntellisenseParser.fsy"
                                 Some _1 
                   )
# 29 "IntellisenseParser.fsy"
                 : 'prog));
# 195 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'object_fields)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 32 "IntellisenseParser.fsy"
                                                                Assoc _2 
                   )
# 32 "IntellisenseParser.fsy"
                 : 'value));
# 206 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'object_fields)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 33 "IntellisenseParser.fsy"
                                                                Assoc _2 
                   )
# 33 "IntellisenseParser.fsy"
                 : 'value));
# 217 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'rev_object_fields)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 35 "IntellisenseParser.fsy"
                                                        List.rev _1 
                   )
# 35 "IntellisenseParser.fsy"
                 : 'object_fields));
# 228 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 38 "IntellisenseParser.fsy"
                                                                             [] 
                   )
# 38 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 238 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'value)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 39 "IntellisenseParser.fsy"
                                                                             [IntellisenseProperty.Property(_1,"|empty_object|",_3)] 
                   )
# 39 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 250 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'rev_object_fields)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : 'value)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 40 "IntellisenseParser.fsy"
                                                                             IntellisenseProperty.Property(_3,"|empty_object|",_5) :: _1 
                   )
# 40 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 263 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _4 = (let data = parseState.GetInput(4) in (Microsoft.FSharp.Core.Operators.unbox data : 'value)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 41 "IntellisenseParser.fsy"
                                                                                              [IntellisenseProperty.Property(_1,_2,_4)] 
                   )
# 41 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 276 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'rev_object_fields)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _4 = (let data = parseState.GetInput(4) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _6 = (let data = parseState.GetInput(6) in (Microsoft.FSharp.Core.Operators.unbox data : 'value)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 42 "IntellisenseParser.fsy"
                                                                                              IntellisenseProperty.Property(_3,_4,_6) :: _1 
                   )
# 42 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 290 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'value)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 43 "IntellisenseParser.fsy"
                                                                             [IntellisenseProperty.AnyProperty(_3)] 
                   )
# 43 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 301 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'rev_object_fields)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : 'value)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 44 "IntellisenseParser.fsy"
                                                                             IntellisenseProperty.AnyProperty(_5) :: _1 
                   )
# 44 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 313 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'rev_object_fields)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 45 "IntellisenseParser.fsy"
                                                                             _1 
                   )
# 45 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 324 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'value)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 48 "IntellisenseParser.fsy"
                                                  [_1] 
                   )
# 48 "IntellisenseParser.fsy"
                 : 'rev_values));
# 335 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'rev_values)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'value)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 49 "IntellisenseParser.fsy"
                                                  _3 :: _1 
                   )
# 49 "IntellisenseParser.fsy"
                 : 'rev_values));
|]
# 348 "IntellisenseParser.fs"
let tables () : Microsoft.FSharp.Text.Parsing.Tables<_> = 
  { reductions= _fsyacc_reductions ();
    endOfInputTag = _fsyacc_endOfInputTag;
    tagOfToken = tagOfToken;
    dataOfToken = _fsyacc_dataOfToken; 
    actionTableElements = _fsyacc_actionTableElements;
    actionTableRowOffsets = _fsyacc_actionTableRowOffsets;
    stateToProdIdxsTableElements = _fsyacc_stateToProdIdxsTableElements;
    stateToProdIdxsTableRowOffsets = _fsyacc_stateToProdIdxsTableRowOffsets;
    reductionSymbolCounts = _fsyacc_reductionSymbolCounts;
    immediateActions = _fsyacc_immediateActions;
    gotos = _fsyacc_gotos;
    sparseGotoTableRowOffsets = _fsyacc_sparseGotoTableRowOffsets;
    tagOfErrorTerminal = _fsyacc_tagOfErrorTerminal;
    parseError = (fun (ctxt:Microsoft.FSharp.Text.Parsing.ParseErrorContext<_>) -> 
                              match parse_error_rich with 
                              | Some f -> f ctxt
                              | None -> parse_error ctxt.Message);
    numTerminals = 15;
    productionToNonTerminalTable = _fsyacc_productionToNonTerminalTable  }
let engine lexer lexbuf startState = (tables ()).Interpret(lexer, lexbuf, startState)
let start lexer lexbuf : ElasticOps.Parsing.Structures.IntellisenseValue option =
    Microsoft.FSharp.Core.Operators.unbox ((tables ()).Interpret(lexer, lexbuf, 0))