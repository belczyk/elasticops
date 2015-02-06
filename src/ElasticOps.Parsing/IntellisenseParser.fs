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
  | UNFINISHED_VALUE of (string * ElasticOps.Parsing.Structures.JsonValueType)
  | COMMA
  | COLON
  | RIGHT_BRACK
  | LEFT_BRACK
  | RIGHT_BRACE
  | LEFT_BRACE
  | NULL
  | FALSE
  | TRUE
  | UNTERMINATED_STRING of (string)
  | STRING of (string)
  | ID of (string)
  | FLOAT of (float)
  | INT of (int)
// This type is used to give symbolic names to token indexes, useful for error messages
type tokenId = 
    | TOKEN_EOF
    | TOKEN_UNFINISHED_VALUE
    | TOKEN_COMMA
    | TOKEN_COLON
    | TOKEN_RIGHT_BRACK
    | TOKEN_LEFT_BRACK
    | TOKEN_RIGHT_BRACE
    | TOKEN_LEFT_BRACE
    | TOKEN_NULL
    | TOKEN_FALSE
    | TOKEN_TRUE
    | TOKEN_UNTERMINATED_STRING
    | TOKEN_STRING
    | TOKEN_ID
    | TOKEN_FLOAT
    | TOKEN_INT
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
    | NONTERM_array_values
    | NONTERM_rev_values

// This function maps tokens to integers indexes
let tagOfToken (t:token) = 
  match t with
  | EOF  -> 0 
  | UNFINISHED_VALUE _ -> 1 
  | COMMA  -> 2 
  | COLON  -> 3 
  | RIGHT_BRACK  -> 4 
  | LEFT_BRACK  -> 5 
  | RIGHT_BRACE  -> 6 
  | LEFT_BRACE  -> 7 
  | NULL  -> 8 
  | FALSE  -> 9 
  | TRUE  -> 10 
  | UNTERMINATED_STRING _ -> 11 
  | STRING _ -> 12 
  | ID _ -> 13 
  | FLOAT _ -> 14 
  | INT _ -> 15 

// This function maps integers indexes to symbolic token ids
let tokenTagToTokenId (tokenIdx:int) = 
  match tokenIdx with
  | 0 -> TOKEN_EOF 
  | 1 -> TOKEN_UNFINISHED_VALUE 
  | 2 -> TOKEN_COMMA 
  | 3 -> TOKEN_COLON 
  | 4 -> TOKEN_RIGHT_BRACK 
  | 5 -> TOKEN_LEFT_BRACK 
  | 6 -> TOKEN_RIGHT_BRACE 
  | 7 -> TOKEN_LEFT_BRACE 
  | 8 -> TOKEN_NULL 
  | 9 -> TOKEN_FALSE 
  | 10 -> TOKEN_TRUE 
  | 11 -> TOKEN_UNTERMINATED_STRING 
  | 12 -> TOKEN_STRING 
  | 13 -> TOKEN_ID 
  | 14 -> TOKEN_FLOAT 
  | 15 -> TOKEN_INT 
  | 18 -> TOKEN_end_of_input
  | 16 -> TOKEN_error
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
    | 6 -> NONTERM_value 
    | 7 -> NONTERM_value 
    | 8 -> NONTERM_value 
    | 9 -> NONTERM_value 
    | 10 -> NONTERM_value 
    | 11 -> NONTERM_value 
    | 12 -> NONTERM_value 
    | 13 -> NONTERM_value 
    | 14 -> NONTERM_value 
    | 15 -> NONTERM_object_fields 
    | 16 -> NONTERM_rev_object_fields 
    | 17 -> NONTERM_rev_object_fields 
    | 18 -> NONTERM_rev_object_fields 
    | 19 -> NONTERM_rev_object_fields 
    | 20 -> NONTERM_rev_object_fields 
    | 21 -> NONTERM_rev_object_fields 
    | 22 -> NONTERM_rev_object_fields 
    | 23 -> NONTERM_rev_object_fields 
    | 24 -> NONTERM_rev_object_fields 
    | 25 -> NONTERM_rev_object_fields 
    | 26 -> NONTERM_array_values 
    | 27 -> NONTERM_array_values 
    | 28 -> NONTERM_array_values 
    | 29 -> NONTERM_rev_values 
    | 30 -> NONTERM_rev_values 
    | _ -> failwith "prodIdxToNonTerminal: bad production index"

let _fsyacc_endOfInputTag = 18 
let _fsyacc_tagOfErrorTerminal = 16

// This function gets the name of a token as a string
let token_to_string (t:token) = 
  match t with 
  | EOF  -> "EOF" 
  | UNFINISHED_VALUE _ -> "UNFINISHED_VALUE" 
  | COMMA  -> "COMMA" 
  | COLON  -> "COLON" 
  | RIGHT_BRACK  -> "RIGHT_BRACK" 
  | LEFT_BRACK  -> "LEFT_BRACK" 
  | RIGHT_BRACE  -> "RIGHT_BRACE" 
  | LEFT_BRACE  -> "LEFT_BRACE" 
  | NULL  -> "NULL" 
  | FALSE  -> "FALSE" 
  | TRUE  -> "TRUE" 
  | UNTERMINATED_STRING _ -> "UNTERMINATED_STRING" 
  | STRING _ -> "STRING" 
  | ID _ -> "ID" 
  | FLOAT _ -> "FLOAT" 
  | INT _ -> "INT" 

// This function gets the data carried by a token as an object
let _fsyacc_dataOfToken (t:token) = 
  match t with 
  | EOF  -> (null : System.Object) 
  | UNFINISHED_VALUE _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | COMMA  -> (null : System.Object) 
  | COLON  -> (null : System.Object) 
  | RIGHT_BRACK  -> (null : System.Object) 
  | LEFT_BRACK  -> (null : System.Object) 
  | RIGHT_BRACE  -> (null : System.Object) 
  | LEFT_BRACE  -> (null : System.Object) 
  | NULL  -> (null : System.Object) 
  | FALSE  -> (null : System.Object) 
  | TRUE  -> (null : System.Object) 
  | UNTERMINATED_STRING _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | STRING _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | ID _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | FLOAT _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | INT _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
let _fsyacc_gotos = [| 0us; 65535us; 1us; 65535us; 0us; 1us; 1us; 65535us; 0us; 2us; 5us; 65535us; 0us; 4us; 8us; 30us; 20us; 21us; 25us; 26us; 29us; 31us; 1us; 65535us; 5us; 6us; 1us; 65535us; 5us; 18us; 1us; 65535us; 8us; 9us; 1us; 65535us; 8us; 28us; |]
let _fsyacc_sparseGotoTableRowOffsets = [|0us; 1us; 3us; 5us; 11us; 13us; 15us; 17us; |]
let _fsyacc_stateToProdIdxsTableElements = [| 1us; 0us; 1us; 0us; 1us; 1us; 1us; 2us; 1us; 3us; 2us; 4us; 5us; 2us; 4us; 5us; 1us; 4us; 2us; 6us; 7us; 2us; 6us; 7us; 1us; 6us; 1us; 8us; 1us; 9us; 1us; 10us; 1us; 11us; 1us; 12us; 1us; 13us; 1us; 14us; 6us; 15us; 21us; 22us; 23us; 24us; 25us; 3us; 17us; 18us; 19us; 2us; 17us; 18us; 1us; 17us; 1us; 20us; 5us; 21us; 22us; 23us; 24us; 25us; 3us; 21us; 23us; 24us; 2us; 21us; 24us; 1us; 21us; 1us; 25us; 3us; 27us; 28us; 30us; 2us; 28us; 30us; 1us; 29us; 1us; 30us; |]
let _fsyacc_stateToProdIdxsTableRowOffsets = [|0us; 2us; 4us; 6us; 8us; 10us; 13us; 16us; 18us; 21us; 24us; 26us; 28us; 30us; 32us; 34us; 36us; 38us; 40us; 47us; 51us; 54us; 56us; 58us; 64us; 68us; 71us; 73us; 75us; 79us; 82us; 84us; |]
let _fsyacc_action_rows = 32
let _fsyacc_actionTableElements = [|10us; 32768us; 0us; 3us; 1us; 17us; 5us; 8us; 7us; 5us; 8us; 16us; 9us; 15us; 10us; 14us; 12us; 11us; 14us; 13us; 15us; 12us; 0us; 49152us; 0us; 16385us; 0us; 16386us; 0us; 16387us; 2us; 16400us; 11us; 22us; 12us; 19us; 1us; 16389us; 6us; 7us; 0us; 16388us; 9us; 16410us; 1us; 17us; 5us; 8us; 7us; 5us; 8us; 16us; 9us; 15us; 10us; 14us; 12us; 11us; 14us; 13us; 15us; 12us; 1us; 16391us; 4us; 10us; 0us; 16390us; 0us; 16392us; 0us; 16393us; 0us; 16394us; 0us; 16395us; 0us; 16396us; 0us; 16397us; 0us; 16398us; 1us; 16399us; 2us; 23us; 1us; 16403us; 3us; 20us; 9us; 16402us; 1us; 17us; 5us; 8us; 7us; 5us; 8us; 16us; 9us; 15us; 10us; 14us; 12us; 11us; 14us; 13us; 15us; 12us; 0us; 16401us; 0us; 16404us; 2us; 16406us; 11us; 27us; 12us; 24us; 1us; 16407us; 3us; 25us; 9us; 16408us; 1us; 17us; 5us; 8us; 7us; 5us; 8us; 16us; 9us; 15us; 10us; 14us; 12us; 11us; 14us; 13us; 15us; 12us; 0us; 16405us; 0us; 16409us; 1us; 16411us; 2us; 29us; 9us; 16412us; 1us; 17us; 5us; 8us; 7us; 5us; 8us; 16us; 9us; 15us; 10us; 14us; 12us; 11us; 14us; 13us; 15us; 12us; 0us; 16413us; 0us; 16414us; |]
let _fsyacc_actionTableRowOffsets = [|0us; 11us; 12us; 13us; 14us; 15us; 18us; 20us; 21us; 31us; 33us; 34us; 35us; 36us; 37us; 38us; 39us; 40us; 41us; 43us; 45us; 55us; 56us; 57us; 60us; 62us; 72us; 73us; 74us; 76us; 86us; 87us; |]
let _fsyacc_reductionSymbolCounts = [|1us; 1us; 1us; 1us; 3us; 2us; 3us; 2us; 1us; 1us; 1us; 1us; 1us; 1us; 1us; 1us; 0us; 3us; 2us; 1us; 1us; 5us; 2us; 3us; 4us; 3us; 0us; 1us; 2us; 1us; 3us; |]
let _fsyacc_productionToNonTerminalTable = [|0us; 1us; 2us; 2us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 3us; 4us; 5us; 5us; 5us; 5us; 5us; 5us; 5us; 5us; 5us; 5us; 6us; 6us; 6us; 7us; 7us; |]
let _fsyacc_immediateActions = [|65535us; 49152us; 16385us; 16386us; 16387us; 65535us; 65535us; 16388us; 65535us; 65535us; 16390us; 16392us; 16393us; 16394us; 16395us; 16396us; 16397us; 16398us; 65535us; 65535us; 65535us; 16401us; 16404us; 65535us; 65535us; 65535us; 16405us; 16409us; 65535us; 65535us; 16413us; 16414us; |]
let _fsyacc_reductions ()  =    [| 
# 193 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : ElasticOps.Parsing.Structures.JsonValue option)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
                      raise (Microsoft.FSharp.Text.Parsing.Accept(Microsoft.FSharp.Core.Operators.box _1))
                   )
                 : '_startstart));
# 202 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'prog)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 28 "IntellisenseParser.fsy"
                                    _1 
                   )
# 28 "IntellisenseParser.fsy"
                 : ElasticOps.Parsing.Structures.JsonValue option));
# 213 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 31 "IntellisenseParser.fsy"
                                 None 
                   )
# 31 "IntellisenseParser.fsy"
                 : 'prog));
# 223 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'value)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 32 "IntellisenseParser.fsy"
                                 Some _1 
                   )
# 32 "IntellisenseParser.fsy"
                 : 'prog));
# 234 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'object_fields)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 35 "IntellisenseParser.fsy"
                                                                Assoc _2 
                   )
# 35 "IntellisenseParser.fsy"
                 : 'value));
# 245 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'object_fields)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 36 "IntellisenseParser.fsy"
                                                                Assoc _2 
                   )
# 36 "IntellisenseParser.fsy"
                 : 'value));
# 256 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'array_values)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 37 "IntellisenseParser.fsy"
                                                                Array _2 
                   )
# 37 "IntellisenseParser.fsy"
                 : 'value));
# 267 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'array_values)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 38 "IntellisenseParser.fsy"
                                                                Array _2 
                   )
# 38 "IntellisenseParser.fsy"
                 : 'value));
# 278 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 39 "IntellisenseParser.fsy"
                                                                String _1 
                   )
# 39 "IntellisenseParser.fsy"
                 : 'value));
# 289 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : int)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 40 "IntellisenseParser.fsy"
                                                                Int _1 
                   )
# 40 "IntellisenseParser.fsy"
                 : 'value));
# 300 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : float)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 41 "IntellisenseParser.fsy"
                                                                Float _1 
                   )
# 41 "IntellisenseParser.fsy"
                 : 'value));
# 311 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 42 "IntellisenseParser.fsy"
                                                                Bool true 
                   )
# 42 "IntellisenseParser.fsy"
                 : 'value));
# 321 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 43 "IntellisenseParser.fsy"
                                                                Bool false 
                   )
# 43 "IntellisenseParser.fsy"
                 : 'value));
# 331 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 44 "IntellisenseParser.fsy"
                                                                Null 
                   )
# 44 "IntellisenseParser.fsy"
                 : 'value));
# 341 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : string * ElasticOps.Parsing.Structures.JsonValueType)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 45 "IntellisenseParser.fsy"
                                                                UnfinishedValue _1 
                   )
# 45 "IntellisenseParser.fsy"
                 : 'value));
# 352 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'rev_object_fields)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 47 "IntellisenseParser.fsy"
                                                        List.rev _1 
                   )
# 47 "IntellisenseParser.fsy"
                 : 'object_fields));
# 363 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 50 "IntellisenseParser.fsy"
                                                                       [] 
                   )
# 50 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 373 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'value)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 51 "IntellisenseParser.fsy"
                                                                       [PropertyWithValue(_1,_3)] 
                   )
# 51 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 385 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 52 "IntellisenseParser.fsy"
                                                                       [PropertyNameWithColon(_1)] 
                   )
# 52 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 396 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 53 "IntellisenseParser.fsy"
                                                                       [PropertyName(_1)] 
                   )
# 53 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 407 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 54 "IntellisenseParser.fsy"
                                                                       [UnfinishedPropertyName(_1)] 
                   )
# 54 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 418 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'rev_object_fields)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : 'value)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 55 "IntellisenseParser.fsy"
                                                                       PropertyWithValue(_3,_5) :: _1 
                   )
# 55 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 431 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'rev_object_fields)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 56 "IntellisenseParser.fsy"
                                                                       _1 
                   )
# 56 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 442 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'rev_object_fields)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 57 "IntellisenseParser.fsy"
                                                                       PropertyName(_3) :: _1 
                   )
# 57 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 454 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'rev_object_fields)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 58 "IntellisenseParser.fsy"
                                                                       PropertyNameWithColon(_3) :: _1 
                   )
# 58 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 466 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'rev_object_fields)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 59 "IntellisenseParser.fsy"
                                                                       UnfinishedPropertyName(_3) :: _1 
                   )
# 59 "IntellisenseParser.fsy"
                 : 'rev_object_fields));
# 478 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 62 "IntellisenseParser.fsy"
                                            [] 
                   )
# 62 "IntellisenseParser.fsy"
                 : 'array_values));
# 488 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'rev_values)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 63 "IntellisenseParser.fsy"
                                            List.rev _1 
                   )
# 63 "IntellisenseParser.fsy"
                 : 'array_values));
# 499 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'rev_values)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 64 "IntellisenseParser.fsy"
                                            List.rev _1 
                   )
# 64 "IntellisenseParser.fsy"
                 : 'array_values));
# 510 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'value)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 67 "IntellisenseParser.fsy"
                                                  [_1] 
                   )
# 67 "IntellisenseParser.fsy"
                 : 'rev_values));
# 521 "IntellisenseParser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'rev_values)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'value)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 68 "IntellisenseParser.fsy"
                                                  _3 :: _1 
                   )
# 68 "IntellisenseParser.fsy"
                 : 'rev_values));
|]
# 534 "IntellisenseParser.fs"
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
    numTerminals = 19;
    productionToNonTerminalTable = _fsyacc_productionToNonTerminalTable  }
let engine lexer lexbuf startState = (tables ()).Interpret(lexer, lexbuf, startState)
let start lexer lexbuf : ElasticOps.Parsing.Structures.JsonValue option =
    Microsoft.FSharp.Core.Operators.unbox ((tables ()).Interpret(lexer, lexbuf, 0))
