namespace PsychometricKernel.Base

/// <summary>
/// All plugins derived must be assigned by [PmkVersion] attribute.
/// If this not happened -> loader throws an error of plugin state and
/// continues work without it.
/// </summary>
[<AbstractClass>]
type PmkExtension =
    /// <summary>
    /// Title of external shared library will be shown in
    /// plugins menu when user tries to get results 
    /// 
    /// Let's see an example:
    /// 
    /// `PsychometricKernel.Big5.dll` @ v1.0 has instantiated
    /// entities
    ///     * PmkBig5Extension -> PmkExtension "BIG-5 test analyser"
    ///     * PmkBig5SchemaExtension -> PmkExtension "BIG-5 test schema serializer"
    ///     * ... -> PmkExtension too
    /// </summary>
    abstract member Title : string with get
    /// <summary>
    /// Description of plugin will show you when you select
    /// plugin in menu before call initialization processes
    /// This is an optional field but would be better if you
    /// set it with fully description of your library.
    /// </summary>
    abstract member Description : string with get
    /// <summary>
    /// 2'nd EntryPoint
    /// Works like main, TMAIN, WinMain, CorDllMain, ... 
    /// Accepts ready (filled) file with answers and starts to 
    /// make results. Returns state of operation and fills 
    /// PmkExtentionBuffer with collection of results. 
    /// </summary>
    abstract member Init : source : string -> int32
    /// <summary>
    /// 1'st EntryPoint
    /// If you not sure, your file will be accepted to plugin
    /// you can call it for your sources.
    /// Returns True if file not corrupted (init process done)
    /// overwise returns False
    /// </summary>
    abstract member TryInit : source : string -> bool

