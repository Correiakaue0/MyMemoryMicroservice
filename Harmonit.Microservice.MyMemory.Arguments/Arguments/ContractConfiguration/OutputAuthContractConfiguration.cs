using Harmonit.StructureApi.Shared.External.Attribute;

namespace Harmonit.Microservice.MyMemory.Arguments;

[TypeModuleEntityClass("Output", "ConfigurationAuthentication")]
public class OutputAuthContractConfiguration
{
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? Token { get; set; }
}