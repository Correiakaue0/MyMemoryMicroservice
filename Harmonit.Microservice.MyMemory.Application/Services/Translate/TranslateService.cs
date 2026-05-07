using Harmonit.Microservice.Base.Library.BaseService;
using Harmonit.Microservice.MyMemory.ApiClient.VeloxInterface;
using Harmonit.Microservice.MyMemory.Application.Interfaces;
using Harmonit.Microservice.MyMemory.Arguments;

namespace Harmonit.Microservice.MyMemory.Application.Services;

public class TranslateService(ITranslateVelox velox) : BaseService_1<ITranslateVelox>(velox), ITranslateService
{
    public async Task<BaseResponseApiContent<OutputTranslateTranslate, ApiResponseException>> Translate(string text,string langpair)
    {
        var response = _velox!.Translate(text, langpair);
        return await ReturnResponse<OutputTranslateTranslate, ApiResponseException>(response, false, SendWebhook);
    }
}