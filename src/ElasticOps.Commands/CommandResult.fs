namespace ElasticOps.Commands

    open System

    type CommandResult<'T when 'T : null> (result : 'T, success : Boolean, errorMessage : String, ex : Exception) =
        member x.Result = result
        member x.Success = success 
        member x.ErrorMessage = errorMessage 
        member x.Exception = ex
        member x.Failed 
            with get() = not x.Success
        
        new (result : 'T) = CommandResult(result,true,null,null)
        new (errorMessage : string ) = CommandResult(null,false,errorMessage,null)
        new (errorMessage : string, ex : Exception) = CommandResult(null,false,errorMessage,ex)
