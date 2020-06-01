using System.Threading.Tasks;
using Common.Infrastructure.Mongo;
using MongoDB.Driver;

namespace Customer.Api.Data
{
    public class CustomerRepository : Repository, ICustomerRepository
    {
        IMongoCollection<CustomerDto> Customers => MongoDatabase.GetCollection<CustomerDto>("Customer");

        public CustomerRepository(MongoConfig config) : base(config)
        {
        }

        public async Task SaveAsync(CustomerDto customer)
        {
            await Customers.InsertOneAsync(customer, new InsertOneOptions { BypassDocumentValidation = false });
        }
    }
}