using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShortLink.Data;

namespace shortLink.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortLinkController : ControllerBase
    {
        private readonly ILogger<ShortLinkController> logger;
        private readonly ApiContext apiContext;

        public ShortLinkController(ILogger<ShortLinkController> logger, ApiContext apiContext)
        {
            this.logger = logger;
            this.apiContext = apiContext;
        }

        [HttpPost, Route("encode")]
        public ShortLinkPair Encode()
        {
            
        }
    }
}
