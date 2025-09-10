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
    /// Instantiate all plugins from .NET DLLs
    /// and calls `TryInit()` function of each loaded plugin
    /// </summary>
    member private _.init(full_init : bool) : unit =
        let dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
        let parentType = typeof<PmkExtension>
        let call_try_init (t : PmkExtension) : PmkExtension =
            match full_init with
            | true -> t.Init |> ignore
            | _ -> t.TryInit |> ignore
            t
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
                                |> call_try_init
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