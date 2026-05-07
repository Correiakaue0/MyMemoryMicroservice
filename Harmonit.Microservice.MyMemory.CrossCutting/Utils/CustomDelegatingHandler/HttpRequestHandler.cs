using Harmonit.Microservice.Base.Library.Generic;
using Harmonit.Microservice.MyMemory.Arguments;
using HarmonitVelox.Core;
using Newtonsoft.Json;

namespace Harmonit.Microservice.MyMemory.CrossCutting.Utils;

public class HttpRequestHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) 
    {
        return base.SendAsync(request!, cancellationToken);
    }

    private static async Task<OutputGetContractConfiguration> GetConfiguration(Guid guidApiDataRequest)
    {
        var responseConfiguration = await CustomHttpClientFactory.For<IContractConfigurationVelox>($"{MicroserviceConfiguration.ManagementApi}").GetByIdentifier(guidApiDataRequest, new Management.Arguments.InputIdentifierContractConfiguration(ApiData.GetLoggedContract(guidApiDataRequest)!.Id));
        var resultJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<dynamic>(responseConfiguration.Content!)?.value.result);
        return JsonConvert.DeserializeObject<OutputGetContractConfiguration>(resultJson);
    }
}