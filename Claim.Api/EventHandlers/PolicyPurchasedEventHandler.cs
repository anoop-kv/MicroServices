using System.Threading;
using System.Threading.Tasks;
using Claim.Api.Data;
using Common.Infrastructure.Contract;
using Newtonsoft.Json.Linq;

namespace Claim.Api.EventHandlers
{
    public class PolicyPurchasedEventHandler : IServiceEventHandler
    {
        private readonly IClaimRepository claimRepository;

        public PolicyPurchasedEventHandler(IClaimRepository claimRepository)
        {
            this.claimRepository = claimRepository;
        }

        public async Task HandleAsync(JObject jObject, CancellationToken cancellationToken)
        {
             var policyPurchased = jObject.ToObject<PolicyPurchased>();

            await claimRepository.SaveCustomerPolicyAsync(new CustomerPolicyDto 
            {
                CoverAmount  = policyPurchased.CoverAmount,
                InceptionDate = policyPurchased.InceptionDate,
                PolicyId = policyPurchased.PolicyId
            });
        }
    }
}