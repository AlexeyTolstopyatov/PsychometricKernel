namespace PsychometricKernel.Base

open System

/// <summary>
/// Tells loader use another schema for instanticating
/// the implemented PMK extension.
/// </summary>
[<Class>]
type PmkBinaryDataAttribute() = inherit Attribute()
