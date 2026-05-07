using Harmonit.StructureApi.Shared.External.Attribute;
using Newtonsoft.Json;

namespace Harmonit.Microservice.MyMemory.Arguments;

[TypeModuleEntityClass("Output", "ResponseDataTranslate")]
public class OutputResponseDataTranslateTranslate
{
    [JsonProperty("translatedText")] public string? TranslatedText { get; set; }
    [JsonProperty("match")] public decimal? Match { get; set; }
}
