module ElasticOps.Parsing.Structures

type JsonValue = 
  | Assoc of (string * JsonValue) list
  | Bool of bool
  | Float of float
  | Int of int
  | List of JsonValue list
  | Null
  | String of string
