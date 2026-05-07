using Harmonit.StructureApi.Shared.External.Attribute;

namespace Harmonit.Microservice.MyMemory.Arguments;

[TypeModuleEntityClass("Output", "Get")]
public class OutputGetContractConfiguration
{
    public string? Id { get; set; }
    public string? ContractId { get; set; }
    public OutputAuthContractConfiguration? ConfigurationAuthentication { get; set; }
}