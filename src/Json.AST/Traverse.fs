module Json.AST.Traverse
open System 
open Json.AST.Parse
 let rec findProp lst name = 
    let x = 1
    match lst with
    | Part.Property(n,v)::tl -> 
        match (n = name) with  
        | true -> Some v
        | _ -> findProp tl name
    | [] -> None 
    | _ -> failwith "Can't find property"


let find obj (path : String) =
    let getProps o = 
        match o with
        | Part.Object ps -> ps
        | _ -> failwith "Can't get properties from other part then Part.Object"

        
    let rec traverse o (pp : String list)= 
        match pp with 
        | hd::[] -> 
            (o |> getProps |> findProp) hd
        | hd::tl -> 
            let sub = (o |> getProps |> findProp) hd
            match sub with 
            | Some v -> traverse v tl
            | None -> failwith "Can't find object under requested path"
        | _ -> failwith "Can't find object under requested path"

    let res = traverse obj ((path.Split [|'.'|]) |> Array.toList)
    match res with
    | Some v -> v
    | None -> failwith "Can't find object udner requested path"
             

