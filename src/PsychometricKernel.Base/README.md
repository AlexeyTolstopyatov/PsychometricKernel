### PsychometricKernel Base
`PsychometricKernel.Base.dll` is an abstractions shared .NET library which
stores all templates and abstractions for each external and internal detail.

All what this library stores must be public.

`PsychometricKernel.Base.dll` represents following types for your 
backend `.dll`:
 - `PmkExtension` - abstract class, which implements and extends by you as you want
 - `PmkExtensionBuffer` - model of temporary data storage. This data takes by client when application tries to initialize external components and holds instantiated plugins till the themselves lifetimes ends.
 - `PmkVersionAttribute` - critically important attribute, which tells scheme for loader "how to understand you" and instanciate your component for Client.

### `PmkExtension`

Let's lookup this type declaration in code:

```fsharp
[<AbstractClass>]
type PmkExtension =
    abstract member Title : string with get
    abstract member Description : string with get
    abstract member Buffer : PmkExtensionBuffer with get
    abstract member Init : source : string -> int32
    abstract member TryInit : source : string -> bool
```
This is a based bundle of procedures you must to implement.

`TryInit` function checks the `[unknown markup]` file filled by user
and checks it for an errors. Next regions I'll tell "why the version contract so important"
and 


### What can stops you?

One things what library not provides - is a data encoding pre/processor.

> [!TIP]
> I know, fact that DLL based on CLR metadata can be disassembled and decompiled.
> Unfortunately, I'm very lazy for client-server organization. It may be higer I/O bound
> and also redundant at all. 

But this is a one limit which stops you to write own plugins for this
platform.

Dynamically linked CLR object `PsychometricKernel.Crypto.dll` holds all
data decoding and encoding logic. If you remember part of end-user license, you would better
think before decompile hidden parts of code.

Remember one thing. This is a big social experiment what I want to organize.
