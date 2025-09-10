namespace PsychometricKernel.Loader

open System
open System.IO
open System.Reflection
open PsychometricKernel.Base

[<Class>]
[<PmkVersion(1, 0)>]
type PmkExtensionsLoader() =
    let mutable results : List<PmkExtension> = []
    /// <summary>
    /// Loads in memory filtered instances of derivatives
    /// by <see cref="PmkExtension"/> from ".../Plugins"
    /// in app domain
    /// </summary>
    [<CompiledName "LoadAll">]
    member public _.load_all() : unit =
        let dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
        let parentType = typeof<PmkExtension>
        results <- Directory.GetFiles(dllPath, "*.dll")
            |> Seq.collect (fun file ->
                try
                    let assembly = Assembly.LoadFrom(file)
                    assembly.GetTypes()
                with _ -> [||])
            // select only implemented derivatives
            |> Seq.filter (fun t ->
                t.IsClass &&
                not t.IsAbstract &&
                t.IsAssignableTo(parentType))
            // filter by actual Attribute
            |> Seq.choose (fun t ->
                let attr = t.GetCustomAttribute<PmkVersionAttribute>()
                try
                    if attr.MajorVersion = 1 then
                        try
                            // check minor versions
                            Activator.CreateInstance(t)
                                :?> PmkExtension
                                |> Some
                        with _ -> None
                    else None
                with
                | stop ->
                    $"\r\n >> {t.Name} thrown an error {stop} " |> Console.WriteLine
                    None
            )
            // cast to F# list
            |> Seq.toList
        ()
    [<CompiledName "InitAll">]
    member public _.init_all(path : string) : List<int32> =
        results |> List.map (fun p -> p.Init path)
    
    [<CompiledName "TryInitAll">]    
    member public _.try_init_all(path : string) : List<bool> =
        results |> List.map (fun p -> p.TryInit path)
    
    [<CompiledName "TryInit">]
    member public _.try_init(index : int32, path : string) : bool =
        results[index].TryInit path
        
    [<CompiledName "Init">]
    member public _.init(index : int32, path : string) : int32 =
        results[index].Init path