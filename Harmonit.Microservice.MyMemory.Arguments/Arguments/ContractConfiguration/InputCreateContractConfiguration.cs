using Harmonit.StructureApi.Shared.External.Attribute;

namespace Harmonit.Microservice.MyMemory.Arguments;

[TypeModuleEntityClass("Input", "Create")]
public class InputCreateContractConfiguration(string? login, string? password, string? token)
{
    public string? Login { get; private set; } = login;
    public string? Password { get; private set; } = password;
    public string? Token { get; private set; } = token;
}