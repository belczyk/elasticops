module ElasticOps.Parsing.Structures
open System 


type Tokens = 
    | INT of int
    | DECIMAL of decimal
    | STRING of String
    | BOOLEAN of bool
    | COMMA
    | L_S_BRAC 
    | R_S_BRAC
    | L_C_BRAC
    | R_C_BRAC
    | SEMICOLON
    | EOF

    
type  Property = {Name : String; Value : Values; } 
and Values =
    | Decimal of decimal
    | Bool of bool
    | String of string
    | Int of int
    | Assoc of Property list
    | List of Values list


