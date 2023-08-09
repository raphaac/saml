using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrieSSO.Domain;

namespace TRIESSO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrieController : ControllerBase
    {
        private readonly ILogger<TrieController> _logger;

        public TrieController(ILogger<TrieController> logger)
        {
            _logger = logger;
        }
                
        [HttpGet]
        [Route("sso/saml")]
        public virtual async Task<IActionResult> GetSamlAuth()
        {
            var ssoService = new SsoService();
            var requestSaml = ssoService.GetSamlAuthRequest(HttpContext);

            return Ok(requestSaml);
        }

        [HttpPost]
        [Route("sso/saml")]
        public virtual async Task<IActionResult> RegisterSamlResponse([FromForm] string SAMLResponse)
        {
            var ssoService = new SsoService();
            var samlResponse = ssoService.DecriptSamlResponse(SAMLResponse);

            return Ok(samlResponse);
        }
    }
}
