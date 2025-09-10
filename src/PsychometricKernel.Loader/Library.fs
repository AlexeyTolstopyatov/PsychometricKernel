namespace PsychometricKernel.Loader

/// <summary>
/// Common Runtime type Redefinition for
/// standard .NET List generic (not F# List)
/// </summary>
type CorList<'Any> = System.Collections.Generic.List<'Any>

module Say =
    let tell =
        printfn "here is no EntryPoint"