module ElasticOps.Parsing.Structures


type token =
  | NULL
  | TRUE
  | FALSE
  | STRING of string
  | INT of int
  | FLOAT of float
  | ID of string
  | LEFT_BRACK
  | RIGHT_BRACK
  | LEFT_BRACE
  | RIGHT_BRACE
  | COMMA
  | COLON
  | EOF

type jsonValue = 
  | Assoc of (string * jsonValue) list
  | Bool of bool
  | Float of float
  | Int of int
  | List of jsonValue list
  | Null
  | String of string
