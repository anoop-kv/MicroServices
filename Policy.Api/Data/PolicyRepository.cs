using System;
using System.Threading.Tasks;
using Common.Infrastructure.Mongo;
using MongoDB.Driver;

namespace Policy.Api.Data
{
    public class PolicyRepository : Repository, IPolicyRepository
    {
        IMongoCollection<Policy> Policies => MongoDatabase.GetCollection<Policy>("Policy");

        public PolicyRepository(MongoConfig config) : base(config)
        {
        }

        public async Task<Policy> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(Policies.Find(c => c.Id == id, new FindOptions { AllowPartialResults = false })
                                    .FirstOrDefault());
        }

        public async Task SaveAsync(Policy policy)
        {
            await Policies.InsertOneAsync(policy, new InsertOneOptions { BypassDocumentValidation = false });
        }
    }
}