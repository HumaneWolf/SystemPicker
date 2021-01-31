using System;
using System.Threading.Tasks;
using SystemPicker.Matcher;
using SystemPicker.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SystemPicker.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScanText : ControllerBase
    {
        private readonly ILogger<ScanText> _logger;
        private readonly TextMatcher _matcher;

        public ScanText(ILogger<ScanText> logger, TextMatcher matcher)
        {
            _logger = logger;
            _matcher = matcher;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ScanInput input)
        {
            var scanId = Guid.NewGuid();
            _logger.LogInformation("Starting scan {scanId}, text: {text}", scanId, input.Text);
            
            var result = await _matcher.FindSystemMatches(input.Text);
            _logger.LogInformation("Finished scan {scanId}, results: {results}", scanId, result.Count);
            return Ok(result);
        }
    }
}