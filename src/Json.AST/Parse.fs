module Json.AST.Parse

open System
open System.Text.RegularExpressions
open System.Globalization

let toCharList (str : String )= str.ToCharArray() |> List.ofArray

type Value = 
    | String of string
    | Number of double
    | Boolean of bool

let rec skip n xs = 
    match (n, xs) with
    | 0, _ -> xs
    | _, [] -> []
    | n, _ :: xs -> skip (n - 1) xs

type Part = 
    | Object of Part list
    | Array of Part list
    | Property of string * Part
    | Value of Value
    | EOF

let isBlankChar c = c = ' ' || c = '\t' || c = '\r' || c = '\n'

let trim (chars : Char list) = 
    let rec removeBlanks cs = 
        match cs with
        | [] -> chars
        | ch :: tail when isBlankChar ch -> removeBlanks tail
        | _ -> cs
    removeBlanks chars

type StringLookup = 
    | String
    | StringEscape

let nextString (cs : Char list) : String * Char list = 
    let rec findNextString (css : Char list, mode : StringLookup, str : String) = 
        match mode with
        | StringEscape -> 
            match css with
            | [] -> (str, css)
            | c :: tail -> findNextString (tail, String, str + c.ToString())
        | String -> 
            match css with
            | [] -> (str, css)
            | '\\' :: tail -> findNextString (tail, StringEscape, str)
            | '"' :: tail -> (str, tail)
            | c :: tail -> findNextString (tail, String, str + c.ToString())
    if cs = [] then (String.Empty, [])
    else findNextString (cs.Tail, String, "")

let dropChar (c : Char) (chars : Char list) = 
    let rec findChar (cs : Char list) = 
        match cs with
        | [] -> chars
        | ch :: tail when ch = c -> tail
        | ch :: tail when isBlankChar ch -> findChar tail
        | ch :: tail -> chars
    (findChar chars) |> trim

let dropColon = dropChar ':'
let dropComma = dropChar ','

let (|Regex|_|) regex (chars : Char list) = 
    let str = chars |> List.fold (fun acc c -> acc ^ (string c)) ""
    if (Regex.IsMatch(str, regex)) then Some chars
    else None

type NumberLookup = 
    | Number
    | Fraction

let nextNumber (chars : Char list) = 
    let rec findNumber cs mode str = 
        match mode with
        | Number -> 
            match cs with
            | [] -> (str, [])
            | c :: tail when 47 <= (int c) && (int c) <= 57 -> findNumber tail Number (str + c.ToString())
            | c :: tail when c = '.' -> findNumber tail Fraction (str + c.ToString())
            | _ :: tail -> (str, tail)            
        | Fraction -> 
            match cs with
            | [] -> (str, [])
            | c :: tail when 47 <= (int c) && (int c) <= 57 -> findNumber tail Fraction (str + c.ToString())
            | _ :: tail -> (str, tail)

    let res = (findNumber chars NumberLookup.Number String.Empty)
    let numStr = fst res
    let rest = snd res
    let (s, n) = Double.TryParse(numStr, Globalization.NumberStyles.Any, new CultureInfo("en-US"))
    if s then Some(n, snd res)
    else None

let rec propertyValue (chars : Char list) = 
    let cs = chars |> dropColon
    match cs with
    | [] -> (EOF, [])
    | Regex "^\d" _ -> 
        let nn = nextNumber cs
        match nn with
        | None -> (Part.EOF, [])
        | Some(n, rest) -> (Part.Value(Value.Number(n)), rest)
    | Regex "^\".*" _ -> 
        let ns = nextString (cs)
        (Part.Value(Value.String(fst ns)), snd ns)
    | Regex "^true" _ -> (Value(Boolean(true)), skip 4 cs)
    | Regex "^false" _ -> (Value(Boolean(false)), skip 5 cs)
    | Regex "^{" _ -> parseNextObject (cs)
    | Regex "^\[" _ -> parseNextArray (cs.Tail)
    | _ -> failwith "unknown json construct"

and parseNextArray (chars : Char list) = 
    let rec findNextValue (cs : Char list, acc : Part list) = 
        let remaining = 
            (cs
             |> dropComma
             |> trim)
        match remaining with
        | [] -> (acc, [])
        | Regex "^\]" _ -> (acc, remaining.Tail)
        | _ -> 
            let (value, rest) = propertyValue (remaining)
            findNextValue (rest, acc @ [ value ])

    let (res, rest) = findNextValue (chars, [])
    (Array(res), rest)

and objectProperties (chars : Char list) = 
    let rec findProps (cs : Char list, acc) = 
        match cs with
        | [] -> (acc, [])
        | cs when (cs
                   |> dropComma
                   |> trim).Head = '}' -> (acc, (cs |> dropComma).Tail)
        | _ -> 
            let propName = 
                cs
                |> dropComma
                |> nextString
            match propName with
            | (name, rest) when name.Trim() = "" -> (acc, rest)
            | (name, rest) -> 
                let value = rest |> propertyValue
                findProps (snd value, acc @ [ Property(name, fst value) ])
    findProps (chars, [])

and parseNextObject (json : Char list) = 
    let rec findNextPart (cs : Char list) = 
        match cs with
        | [] -> (EOF, [])
        | '{' :: tail -> 
            let (props, rest) = objectProperties (tail)
            (Part.Object(props), rest)
        | _ :: tail -> findNextPart (tail)
    findNextPart (json)



