namespace PsychometricKernel.Base

open System
open Microsoft.FSharp.Core

[<Class>]
[<AttributeUsage(AttributeTargets.Class)>]
type PmkVersionAttribute(major : int32, minor : int32) = inherit Attribute() with
    let mutable minorVersion : int32 = minor
    let mutable majorVersion : int32 = major
    /// <summary>
    /// empty constructor uses for tested plugins
    /// and gives specific versioning  
    /// </summary>
    new() =
        PmkVersionAttribute(-1, 0)
    /// <summary>
    /// Holds on major contract version for extensions/plugins
    /// if differs with loader -> plugin will be unloaded
    /// </summary>
    member val MajorVersion = majorVersion with get, set
    /// <summary>
    /// Holds on minor version for plugins
    /// if it differs -> plugins will be loaded but thrown a warning.
    /// </summary>
    member val MinorVersion = minorVersion with get, set