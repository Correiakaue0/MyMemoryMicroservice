using Harmonit.Microservice.Base.Library.BaseService;
using Harmonit.Microservice.Base.Library.Generic;
using Harmonit.Microservice.MyMemory.Arguments;
using Harmonit.Microservice.MyMemory.Application.Interfaces;

namespace Harmonit.Microservice.MyMemory.Application.Services;

public class ContractConfigurationService(IContractConfigurationVelox velox) : BaseService_1<IContractConfigurationVelox>(velox), IContractConfigurationService
{
    public async Task<BaseResponseApiContent<string, string>> Create(InputCreateContractConfiguration inputCreateContractConfiguration) => await Task.Run(() =>
    {
        var response = _velox!.Create(_guidApiDataRequest, new Management.Arguments.InputCreateContractConfiguration(_contractIdApiDataRequest!, null, inputCreateContractConfiguration));
        return ReturnResponse<string, string>(response, true, SendWebhook, true);
    });

    public async Task<BaseResponseApiContent<OutputGetContractConfiguration, string>> Get() => await Task.Run(() =>
    {
        var response = _velox!.GetByIdentifier(_guidApiDataRequest, new Management.Arguments.InputIdentifierContractConfiguration(_contractIdApiDataRequest!));
        return ReturnResponse<OutputGetContractConfiguration, string>(response, true, SendWebhook, true);
    });

    public async Task<BaseResponseApiContent<string, string>> Update(InputUpdateContractConfiguration inputUpdateContractConfiguration) => await Task.Run(() =>
    {
        var response = _velox!.Update(_guidApiDataRequest, new Management.Arguments.InputUpdateContractConfiguration(_contractIdApiDataRequest!, null, inputUpdateContractConfiguration));
        return ReturnResponse<string, string>(response, true, SendWebhook, true);
    });
}