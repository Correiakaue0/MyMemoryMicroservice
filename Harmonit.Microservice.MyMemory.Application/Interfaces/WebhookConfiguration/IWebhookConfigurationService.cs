using Harmonit.Microservice.Base.Library.BaseService;
using Harmonit.Microservice.Management.Arguments;

namespace Harmonit.Microservice.MyMemory.Application.Interfaces;

public interface IWebhookConfigurationService : IBaseService_0
{
    Task<BaseResponseApiContent<string, string>> CreateRoute(string route);
    Task<BaseResponseApiContent<string, string>> UpdateRoute(string route);
    Task<BaseResponseApiContent<OutputWebhookConfiguration, string>> Get();
}