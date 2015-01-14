module Tokens
open System 

type Tokens = 
    | INT of int
    | DECIMAL of decimal
    | STRING of String
    | BOOLEAN of bool
    | L_S_BRAC 
    | R_S_BRAC
    | L_C_BRAC
    | R_C_BRAC
    | SEMICOLON
    | EOF

    
