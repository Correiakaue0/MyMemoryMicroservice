using Harmonit.Microservice.Base.Library.BaseService;
using Harmonit.Microservice.MyMemory.Arguments;

namespace Harmonit.Microservice.MyMemory.Application.Interfaces;

public interface IContractConfigurationService : IBaseService_0
{
    Task<BaseResponseApiContent<OutputGetContractConfiguration, string>> Get();
    Task<BaseResponseApiContent<string, string>> Create(InputCreateContractConfiguration inputCreateContractConfiguration);
    Task<BaseResponseApiContent<string, string>> Update(InputUpdateContractConfiguration inputUpdateContractConfiguration);
}