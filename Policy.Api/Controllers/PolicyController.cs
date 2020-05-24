using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Infrastructure.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Policy.Api.Commands;

namespace Payment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolicyController : ControllerBase
    {
        private readonly ICommandHandler<PurchasePolicyCommand> commandHandler;

        public PolicyController(ICommandHandler<PurchasePolicyCommand> commandHandler)
        {
            this.commandHandler = commandHandler;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            await commandHandler.HandleAsync(new PurchasePolicyCommand
            {
                
            }, CancellationToken.None);
            return "OK";
        }
    }
}
