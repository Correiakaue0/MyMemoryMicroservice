using Harmonit.Microservice.Base.Library.BaseController;
using Harmonit.Microservice.Base.Library.Generic;
using Harmonit.Microservice.MyMemory.Application.Interfaces;
using Harmonit.Notification.Library.Arguments;
using Microsoft.AspNetCore.Mvc;

namespace Harmonit.Microservice.MyMemory.Api.Controllers;

[Route("api/webhookconfiguration/[controller]")]
public class WebhookConfigurationController(IApiDataService apiDataService, IWebhookConfigurationService service) : BaseController_1<IWebhookConfigurationService>(apiDataService, service)
{

    [LanguageDescription("pt-br", "Gravar configuração de webhook")]
    [LanguageDescription("en", "Create webhook configuration")]
    [LanguageDescription("es", "Crear configuración de webhook")]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("CreateRoute")]
    public async Task<ActionResult<BaseResponseApi<string, string>>> CreateRoute([FromQuery] string route)
    {
        try
        {
            return await ResponseAsync(PrepareReturn(await _service!.CreateRoute(route)));
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

    [LanguageDescription("pt-br", "Atualiza configuração de webhook")]
    [LanguageDescription("en", "Update webhook configuration")]
    [LanguageDescription("es", "Actualizar la configuración del webhook")]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("UpdateRoute")]
    public async Task<ActionResult<BaseResponseApi<string, string>>> UpdateRoute([FromQuery] string route)
    {
        try
        {
            return await ResponseAsync(PrepareReturn(await _service!.UpdateRoute(route)));
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

    [LanguageDescription("pt-br", "Consulta configuração de webhook")]
    [LanguageDescription("en", "Query webhook configuration")]
    [LanguageDescription("es", "Consultar la configuración del webhook")]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("Get")]
    public async Task<ActionResult<BaseResponseApi<string, string>>> Get()
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
}