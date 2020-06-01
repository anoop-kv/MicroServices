using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Claim.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Claim.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaimController : ControllerBase
    {
        [HttpPost]
        public string Create([FromBody] ClaimDto claim)
        {
            return "OK";
        }
    }
}
