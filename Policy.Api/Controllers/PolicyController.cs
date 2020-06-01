using System.Threading.Tasks;
using Common.Infrastructure.Contract;
using Microsoft.AspNetCore.Mvc;
using Policy.Api.Commands;
using Policy.Api.Data;

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

        [HttpPost]
        public async Task<string> Purchase([FromBody] PolicyDto policy)
        {
            await commandHandler.HandleAsync(MapToCommand(policy));

            return "OK";
        }

        private static PurchasePolicyCommand MapToCommand(PolicyDto policy)
        {
            return new PurchasePolicyCommand
            {
              AccountNumber = policy.AccountNumber,
              CoverAmount = policy.CoverAmount,
              InceptionDate = policy.InceptionDate,
              PolicyHolder = policy.PolicyHolder,
              PolicyId = policy.PolicyId
            };
        }
    }
}
