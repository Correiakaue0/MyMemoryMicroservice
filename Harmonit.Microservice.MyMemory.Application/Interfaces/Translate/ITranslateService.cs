using Harmonit.Microservice.Base.Library.BaseService;
using Harmonit.Microservice.MyMemory.Arguments;

namespace Harmonit.Microservice.MyMemory.Application.Interfaces;

public interface ITranslateService : IBaseService_0
{
    Task<BaseResponseApiContent<OutputTranslateTranslate, ApiResponseException>> Translate(string text,string langpair);
}
