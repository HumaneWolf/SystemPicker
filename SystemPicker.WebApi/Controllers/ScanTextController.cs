﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemPicker.Matcher;
using SystemPicker.Matcher.Models;
using SystemPicker.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace SystemPicker.WebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
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
        [ProducesResponseType(typeof(List<SystemMatch>), 200)]
        [ProducesResponseType(typeof(List<SystemMatch>), 400)]
        [SwaggerOperation(
            Summary = "Find mentioned system names in arbitrary text.",
            Description = "This will match your text against name patterns, as well as known sector and system names to find known system names, ids, and links to useful resources."
        )]
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