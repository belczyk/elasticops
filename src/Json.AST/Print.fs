module Json.AST.Print
open Json.AST.Parse

let listToString obj pf= 
    let rec lts lst acc = 
        match obj with
            | hd::[] -> (sprintf "%s %s" acc (pf hd))
            | hd::tl -> lts tl (sprintf "%s %s," acc (pf hd))
            | [] -> acc
    lts obj ""

let rec toString obj = 
    match obj with 
    | Part.Object(ps) -> sprintf "{%s}" (listToString ps toString)
    | Part.Property(name,value) -> sprintf "\"%s\" = %s" name (toString value)
    | Part.EOF -> ""
    | Part.Array(its) -> sprintf "[%s]" (listToString its toString)
    | Part.Value(v) -> match v with
                        | Value.Boolean(b) -> b.ToString().ToLower()
                        | Value.Number(n) -> n.ToString()
                        | Value.String(s) -> sprintf "\"%s\"" s

