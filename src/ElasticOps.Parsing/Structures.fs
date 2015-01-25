module ElasticOps.Parsing.Structures

type JsonValue = 
  | Assoc of (string * JsonValue) list
  | Bool of bool
  | Float of float
  | Int of int
  | List of JsonValue list
  | Null
  | String of string
  | Missing
  | Colon
  | UnfinishedValue of string * JsonValueType

  //below function is not important, it simply prints values 
  override x.ToString() = 
            match x with
            | Bool b -> sprintf "Bool(%b)" b
            | Float f -> sprintf "Float(%f)" f
            | Int d -> sprintf "Int(%d)" d
            | String s -> sprintf "String(%s)" s
            | Null ->  "Null()"
            | Colon -> "':'"
            | Missing -> "/missing/"
            | Assoc props ->  props 
                               |> List.map (fun (name,value) -> sprintf "\"%s\" : %s" name (value.ToString())) 
                               |> String.concat ","
                               |> sprintf "Assoc(%s)"
            | List values ->  values
                               |> List.map (fun value -> value.ToString()) 
                               |> String.concat ","
                               |> sprintf "List(%s)"
            | UnfinishedValue (s,t) -> sprintf "Unfinished_%s(%s)" (t.ToString()) s
and JsonValueType =
    | TBool
    | TFloat
    | TInt
    | TString
    | TNull
    override x.ToString() = 
        match x with 
        | TBool -> "Bool"
        | TFloat -> "Float"
        | TInt -> "Int"
        | TString -> "String"
        | TNull -> "Null"

