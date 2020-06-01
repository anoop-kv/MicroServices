using System.Threading.Tasks;
using Common.Infrastructure.Mongo;
using MongoDB.Driver;

namespace Claim.Api.Data
{
    public class ClaimRepository : Repository, IClaimRepository
    {
        IMongoCollection<ClaimDto> Claims => MongoDatabase.GetCollection<ClaimDto>("Claim");
        IMongoCollection<CustomerPolicyDto> CustomerPolicies => MongoDatabase.GetCollection<CustomerPolicyDto>("Claim");

        public ClaimRepository(MongoConfig config) : base(config)
        {
        }

        public async Task<CustomerPolicyDto> GetPolicyAsync(string policyId)
        {
            return await CustomerPolicies.Find(cp => cp.PolicyId == policyId).FirstOrDefaultAsync();
        }

        public async Task SaveClaimAsync(ClaimDto claim)
        {
            await Claims.InsertOneAsync(claim, new InsertOneOptions { BypassDocumentValidation = false });
        }

        public async Task SaveCustomerPolicyAsync(CustomerPolicyDto customerPolicy)
        {
            await CustomerPolicies.InsertOneAsync(customerPolicy, new InsertOneOptions { BypassDocumentValidation = false });
        }
    }
}