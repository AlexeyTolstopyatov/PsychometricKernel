namespace PsychometricKernel.Metrics.Positive

open System
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
    [<JsonPropertyName "key">]
    Key : string
    [<JsonPropertyName "answer">]
    Answer : int
}
/// <summary>
/// All JSON deserialized blocks will be replaced with
/// processed blocks. ["nervous", +3.67], ["hyperfocus_check", +9,23] // JUST positive values 
/// </summary>
type PositiveMetricsBinaryBlock = {
    [<JsonPropertyName "key">]
    Key : string
    [<JsonPropertyName "value">]
    Value : float
}

[<Class>]
[<PmkVersion(1, 0)>]
type PositiveMetricsExtension = inherit PmkExtension with
    override this.Buffer = PmkExtensionBuffer()
    
    override this.Init(source : string) : int32 =
        try
            let _test_blocks =
                File.ReadAllText source
                |> JsonDocument.Parse
                |> JsonSerializer.Deserialize<PositiveMetricsBlock[]>
                |> Seq.toList
            // match all blocks to dictionary blocks
            // answer of key -> {key: sum_of_all_answers}
            0
        with
        | _ -> -1
    override this.TryInit(source : string) : bool =
        try
            // preprocess data 'ch'd (which had) sent from client
            let test_json =
                File.ReadAllText source
                |> JsonDocument.Parse
                |> JsonSerializer.Deserialize<PositiveMetricsBlock[]>
                |> Array.groupBy (fun x -> x.Key)
                |> Array.map (fun (key, blocks) ->
                    {
                        Key = key
                        Value =  blocks |> Array.sumBy (_.Answer)
                    }
                )
                |>  JsonSerializer.Serialize
            
            // encode all data using unknown algorithm
            
            // send it to the client.
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + FileInfo(source).Name, test_json)
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
    