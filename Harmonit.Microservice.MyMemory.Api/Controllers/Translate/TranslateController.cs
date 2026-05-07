using Harmonit.Microservice.Base.Library.BaseController;
using Harmonit.Microservice.Base.Library.Generic;
using Harmonit.Microservice.MyMemory.Application;
using Harmonit.Microservice.MyMemory.Application.Interfaces;
using Harmonit.Microservice.MyMemory.Arguments;
using Harmonit.Notification.Library.Arguments;
using Microsoft.AspNetCore.Mvc;

namespace Harmonit.Microservice.MyMemory.Api.Controllers;

[Route("api/mymemory/[controller]")]
public class TranslateController(IApiDataService apiDataService, ITranslateService service) : BaseController_1<ITranslateService>(apiDataService, service)
{
    [LanguageDescription("pt-br", "Tradução de palavras")]
    [LanguageDescription("en", "Word translation")]
    [LanguageDescription("es", "Traducción de palabras")]
    [HttpGet("Translate")]
    public async Task<ActionResult<BaseResponseApi<OutputTranslateTranslate, ApiResponseException>>> Translate([FromHeader] string text, [FromHeader] string langpair)
    {
        try
        {
            return await ResponseAsync(PrepareReturn(await _service!.Translate(text, langpair)));
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