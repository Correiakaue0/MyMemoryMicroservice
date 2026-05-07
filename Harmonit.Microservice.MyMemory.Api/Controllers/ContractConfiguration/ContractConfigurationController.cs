using Harmonit.Microservice.Base.Library.BaseController;
using Harmonit.Microservice.Base.Library.Generic;
using Harmonit.Microservice.MyMemory.Arguments;
using Harmonit.Microservice.MyMemory.Application.Interfaces;
using Harmonit.Notification.Library.Arguments;
using Microsoft.AspNetCore.Mvc;

namespace Harmonit.Microservice.MyMemory.Api.Controllers;

[Route("api/contractconfiguration/[controller]")]
public class ContractConfigurationController(IApiDataService apiDataService, IContractConfigurationService service) : BaseController_1<IContractConfigurationService>(apiDataService, service)
{
    [LanguageDescription("pt-br", "Consulta a configuração da empresa")]
    [LanguageDescription("en", "Check company setup")]
    [LanguageDescription("es", "Comprobar la configuración de la empresa")]
    [HttpGet("Get")]
    public async Task<ActionResult<BaseResponseApi<OutputGetContractConfiguration, string>>> Get()
    {
        try
        {
            return await ResponseAsync(PrepareReturn(await _service!.Get()));
        }
        catch (BaseResponseException ex)
        {
            return await BaseResponseExceptionAsync(ex);
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [LanguageDescription("pt-br", "Cria a configuração da empresa")]
    [LanguageDescription("en", "Creates the company configuration")]
    [LanguageDescription("es", "Crea la configuración de la empresa")]
    [HttpPost("Create")]
    public async Task<ActionResult<BaseResponseApi<string, string>>> Create(InputCreateContractConfiguration inputCreateConfiguration)
    {
        try
        {
            return await ResponseAsync(PrepareReturn(await _service!.Create(inputCreateConfiguration)));
        }
        catch (BaseResponseException ex)
        {
            return await BaseResponseExceptionAsync(ex);
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [LanguageDescription("pt-br", "Atualiza a configuração da empresa")]
    [LanguageDescription("en", "Updates company configuration")]
    [LanguageDescription("es", "Actualiza la configuración de la empresa")]
    [HttpPut("Update")]
    public async Task<ActionResult<BaseResponseApi<string, string>>> Update(InputUpdateContractConfiguration inputUpdateConfigurationData)
    {
        try
        {
            return await ResponseAsync(PrepareReturn(await _service!.Update(inputUpdateConfigurationData)));
        }
        catch (BaseResponseException ex)
        {
            return await BaseResponseExceptionAsync(ex);
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }
}