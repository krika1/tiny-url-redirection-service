using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Mvc;
using TinyUrl.RedirectionService.Infrastructure.Services;

namespace TinyUrl.RedirectionService.Controllers
{
    [ApiController]
    public class RedirectionController : ControllerBase
    {
        private readonly IUrlMappingService _urlMappingService;

        public RedirectionController(IUrlMappingService urlMappingService)
        {
            _urlMappingService = urlMappingService;
        }

        [HttpGet("{shortUrl}")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RedirectToLongUrlAsync([FromRoute] string shortUrl)
        {
            var longUrl = await _urlMappingService.GetLongUrlAsync(Uri.UnescapeDataString(shortUrl)).ConfigureAwait(false);

            if (longUrl is null) return NotFound();

            return Redirect(longUrl);
        }
    }
}
