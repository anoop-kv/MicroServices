using System;
using System.Threading.Tasks;
using Common.Infrastructure.Mongo;
using MongoDB.Driver;

namespace Policy.Api.Data
{
    public class PolicyRepository : Repository, IPolicyRepository
    {
        IMongoCollection<PolicyDto> Policies => MongoDatabase.GetCollection<PolicyDto>("Policy");

        public PolicyRepository(MongoConfig config) : base(config)
        {
        }

        public async Task<PolicyDto> GetByIdAsync(string id)
        {
            return await Task.FromResult(Policies.Find(c => c.PolicyId == id, new FindOptions { AllowPartialResults = false })
                                    .FirstOrDefault());
        }

        public async Task SaveAsync(PolicyDto policy)
        {
            await Policies.InsertOneAsync(policy, new InsertOneOptions { BypassDocumentValidation = false });
        }
    }
}