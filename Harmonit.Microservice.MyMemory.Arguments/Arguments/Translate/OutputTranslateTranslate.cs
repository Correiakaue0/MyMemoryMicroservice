using Harmonit.StructureApi.Shared.External.Attribute;
using Newtonsoft.Json;

namespace Harmonit.Microservice.MyMemory.Arguments;

[TypeModuleEntityClass("Output", "Translate")]
public class OutputTranslateTranslate
{
    [JsonProperty("responseData")] public OutputResponseDataTranslateTranslate? ResponseData { get; set; }
}
