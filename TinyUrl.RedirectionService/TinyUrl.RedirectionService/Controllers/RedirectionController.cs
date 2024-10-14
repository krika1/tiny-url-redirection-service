using Microsoft.AspNetCore.Mvc;
using TinyUrl.RedirectionService.Infrastructure.Common;
using TinyUrl.RedirectionService.Infrastructure.Contracts.Responses;
using TinyUrl.RedirectionService.Infrastructure.Exceptions;
using TinyUrl.RedirectionService.Infrastructure.Services;

namespace TinyUrl.RedirectionService.Controllers
{
    [ApiController]
    public class RedirectionController : ControllerBase
    {
        private readonly IUrlMappingService _urlMappingService;
        private readonly IRabbitMQService _rabbitMQService;

        public RedirectionController(IUrlMappingService urlMappingService, IRabbitMQService rabbitMQService)
        {
            _urlMappingService = urlMappingService;
            _rabbitMQService=rabbitMQService;
        }

        [HttpGet("{shortUrl}")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status410Gone)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RedirectToLongUrlAsync([FromRoute] string shortUrl)
        {
            try
            {
                var longUrl = await _urlMappingService.GetLongUrlAsync(Uri.UnescapeDataString(shortUrl)).ConfigureAwait(false);

                if (longUrl is null) return NotFound();

                _rabbitMQService.SendToQueue(shortUrl);

                return Redirect(longUrl);
            }
            catch (ShortUrlExpiredException ex)
            {
                var error = new ErrorContract(StatusCodes.Status410Gone, ex.Message, ErrorTitles.RedirectUrlFailedErrorTitle);

                return new ObjectResult(error)
                {
                    StatusCode = StatusCodes.Status410Gone,
                };
            }
        }
    }
}
