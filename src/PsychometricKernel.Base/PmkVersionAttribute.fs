namespace PsychometricKernel.Base

open System
open Microsoft.FSharp.Core

[<Class>]
[<AttributeUsage(AttributeTargets.Class)>]
type PmkVersionAttribute(major : Int32, minor : Int32) = inherit Attribute() with
    
    let mutable minorVersion : Int32 = minor
    let mutable majorVersion : Int32 = major
    
    new() =
        PmkVersionAttribute(1, 0)
    
    member val MajorVersion = majorVersion with get, set
    member val MinorVersion = minorVersion with get, set