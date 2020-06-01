using System.Threading.Tasks;
using Common.Infrastructure.Mongo;
using MongoDB.Driver;

namespace Payment.Api.Data
{
    public class PaymentRepository : Repository, IPaymentRepository
    {
        IMongoCollection<BankAccountDto> Payments => MongoDatabase.GetCollection<BankAccountDto>("Payment");

        public PaymentRepository(MongoConfig config) : base(config)
        {
        }

        public async Task SaveAsync(BankAccountDto bankAccount)
        {
            await Payments.InsertOneAsync(bankAccount, new InsertOneOptions { BypassDocumentValidation = false });
        }
    }
}