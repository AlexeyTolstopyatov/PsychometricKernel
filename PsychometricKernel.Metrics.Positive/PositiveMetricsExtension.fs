namespace PsychometricKernel.Metrics.Positive

open System.IO
open System.Text.Json
open System.Text.Json.Serialization
open PsychometricKernel.Base

//
// ### Testable idea
// Positive metrics accept any format tests and just increment
// values by tags. Statistics of test will be pretty simple
//
// all unique answers from JSON collect to the map
// and processes unsigned result. What unique key has the most positive
// -> this will be a result of test.  
//

type PositiveMetricsBlock = {
    [<JsonPropertyName "question">]
    Question : string
    
    [<JsonPropertyName "suggestions">]
    Suggestions : string[]
    
    [<JsonPropertyName "answers">]
    Answers : string[]
}

[<Class>]
[<PmkVersion(1, 0)>]
type PositiveMetricsExtension = inherit PmkExtension with
    override this.Buffer = PmkExtensionBuffer()
    
    override this.Init(source : string) : int32 =
        try
            
            0
        with
        | _ -> -1
    override this.TryInit(source : string) : bool =
        try
            let test_json = File.ReadAllText source
                            |> JsonDocument.Parse
                            |> JsonSerializer.Deserialize<PositiveMetricsBlock[]>
                            |> Seq.toList
            
            true  // if no problems here: send non-zero signal 
        with
        | _ ->
            false // if having problems: send a zero signal
    /// <summary>
    /// This description could be taken from file too. No limits here
    /// But for example this is a just hardcoded value.
    /// </summary>
    override this.Description = @"
Positive metrics accept any format tests and just increment
values by tags. Statistics of test will be pretty simple
all unique answers from JSON collect to the map
and processes unsigned result. What unique key has the most positive
-> this will be a result of test.  
    "
    /// <summary>
    /// Title of plugin/extension could be taken from file.
    /// But in example, I've used hardcoded UTF-16LE string :D!
    /// </summary>
    override this.Title = "Positive Metric test"
    