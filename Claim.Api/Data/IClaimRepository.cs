using System.Threading.Tasks;

namespace Claim.Api.Data
{
    public interface IClaimRepository 
    {
        Task<CustomerPolicyDto> GetPolicyAsync(string policyId);

        Task SaveClaimAsync(ClaimDto claim);

        Task SaveCustomerPolicyAsync(CustomerPolicyDto customerPolicy);
    }
}