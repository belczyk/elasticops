module ElasticOps.Parsing.Structures


type JsonValue = 
  | Assoc of JsonProperty list
  | Bool of bool
  | Float of float
  | Int of int
  | Array of JsonValue list
  | Null
  | String of string
  | UnfinishedValue of string * JsonValueType
  | Property of JsonProperty
  //below function is not important, it simply prints values 
  override x.ToString() = 
            match x with
            | Bool b -> sprintf "Bool(%b)" b
            | Float f -> sprintf "Float(%f)" f
            | Int d -> sprintf "Int(%d)" d
            | String s -> sprintf "String(%s)" s
            | Null ->  "Null()"
            | Property prop -> sprintf "%s" (prop.ToString())
            | Assoc props ->  props 
                               |> List.map (fun p -> sprintf "%s" (p.ToString())) 
                               |> String.concat ","
                               |> sprintf "Assoc(%s)"
            | Array values ->  values
                               |> List.map (fun value -> value.ToString()) 
                               |> String.concat ","
                               |> sprintf "List(%s)"
            | UnfinishedValue (s,t) -> sprintf "Unfinished_%s(%s)" (t.ToString()) s
and JsonProperty =
  | PropertyNameWithColon of string
  | PropertyWithValue of string * JsonValue
  | PropertyName of string
  | UnfinishedPropertyName of string
  override x.ToString() = 
            match x with
            | PropertyName name -> sprintf @"""%s""" name
            | PropertyNameWithColon name -> sprintf @"""%s"" : " name
            | UnfinishedPropertyName name -> sprintf @"""%s" name
            | PropertyWithValue (name, value) -> sprintf @"""%s"" : %s" name (value.ToString())
  member x.getName () = 
      match x with 
      | JsonProperty.PropertyName name -> name
      | JsonProperty.PropertyNameWithColon name -> name
      | JsonProperty.PropertyWithValue(name,_) -> name
      | JsonProperty.UnfinishedPropertyName name -> name

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

