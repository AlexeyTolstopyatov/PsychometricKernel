namespace PsychometricKernel.Loader

open System

// to consider -> doing smth.
// considering => thinking about
// it doesn't help that == that makes it worse
// plenty of smth => too many things

[<Class>]
type PmkExtensionBuffer() =
    let _results : CorList<Object> = CorList<Object>()
    let exceptions_chain : Exception = null
    /// <summary>
    /// Points to exceptions chain for next diagnostics
    /// </summary>
    [<CompiledName "ExceptionsChain">]
    member val ``exception`` = exceptions_chain with get, set
    /// <summary>
    /// Points to list of results
    /// </summary>
    [<CompiledName "Results">]
    member val results = _results with get, set