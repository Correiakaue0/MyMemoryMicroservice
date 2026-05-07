using Harmonit.Microservice.Base.Library.BaseService;
using Harmonit.Microservice.Base.Library.Generic;
using Harmonit.Microservice.Management.Arguments;
using Harmonit.Microservice.MyMemory.Application.Interfaces;

namespace Harmonit.Microservice.MyMemory.Application.Services;

public class WebhookConfigurationService(IWebhookConfigurationVelox velox) : BaseService_1<IWebhookConfigurationVelox>(velox), IWebhookConfigurationService
{
    public async Task<BaseResponseApiContent<string, string>> CreateRoute(string route) => await Task.Run(() =>
    {
        var response = _velox!.CreateRoute(_contractIdApiDataRequest!, route);
        return ReturnResponse<string, string>(response, true, SendWebhook, true);
    });

    public async Task<BaseResponseApiContent<string, string>> UpdateRoute(string route) => await Task.Run(() =>
    {
        var response = _velox!.UpdateRoute(_contractIdApiDataRequest!, route);
        return ReturnResponse<string, string>(response, true, SendWebhook, true);
    });

    public async Task<BaseResponseApiContent<OutputWebhookConfiguration, string>> Get() => await Task.Run(() =>
    {
        var response = _velox!.GetByIdentifier(new InputIdentifierWebhookConfiguration(_contractIdApiDataRequest!));
        return ReturnResponse<OutputWebhookConfiguration, string>(response, true, SendWebhook, true);
    });
}