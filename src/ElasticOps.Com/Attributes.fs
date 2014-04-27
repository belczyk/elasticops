namespace ElasticOps.Com
open System


[<AttributeUsage(AttributeTargets.Method, AllowMultiple=false)>]
type ESVersionFrom (major : int, minor : int, patch : int, build : int) =
    inherit Attribute()
    new (major : int) = ESVersionFrom(major,0,0,0)
    new (major : int, minor : int) = ESVersionFrom(major,minor,0,0)
    new (major : int, minor : int, patch : int) = ESVersionFrom(major,minor,patch,0)
        
    member x.major = major
    member x.minor = minor
    member x.patch = patch
    member x.build = build


[<AttributeUsage(AttributeTargets.Method, AllowMultiple=false)>]
type ESVersionTo (major : int, ?minor : int, ?patch : int, ?build : int) =
    inherit Attribute()
    new (major : int) = ESVersionTo(major,0,0,0)
    new (major : int, minor : int) = ESVersionTo(major,minor,0,0)
    new (major : int, minor : int, patch : int) = ESVersionTo(major,minor,patch,0)

    member x.major = major
    member x.minor = minor
    member x.patch = patch
    member x.build = build

type CommandsHanlders() =
    inherit Attribute()