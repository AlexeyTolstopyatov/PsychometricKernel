namespace PsychometricKernel.Metrics.Mbti

open PsychometricKernel.Base

//
// This is a just example of metrics usage.
// The most interesting in it that format of
// metrics is free.
//
// Plugins can draw or write statistics by itself
//

[<Class>]
[<PmkVersion(1, 0)>]
type MbtiAxisMetricsExtension = inherit PmkExtension with
    override this.Init(source : string) : int32 = failwith "todo"
    override this.TryInit(source : string) : bool = failwith "todo"
    override this.Description = failwith "todo"
    override this.Title = failwith "todo"
    