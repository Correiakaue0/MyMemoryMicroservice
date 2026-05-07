using Harmonit.Microservice.MyMemory.ApiClient.BaseVeloxInterfaces;
using HarmonitVelox.Attributes;
using HarmonitVelox.Response;

namespace Harmonit.Microservice.MyMemory.ApiClient.VeloxInterface;

public interface ITranslateVelox : IBaseVeloxInterfaceDefaultHandler
{
    [Get("/get")]
    Task<ApiResponse<string>> Translate([AliasAs("q")] string text, [AliasAs("langpair")] string langpair);
}