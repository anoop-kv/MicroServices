using System.Threading;
using System.Threading.Tasks;
using Common.Infrastructure.Contract;
using Newtonsoft.Json.Linq;
using Payment.Api.Data;

namespace Payment.Api.EventHandlers
{
    public class PolicyPurchasedEventHandler : IServiceEventHandler
    {
        private readonly IPaymentRepository paymentRepository;

        public PolicyPurchasedEventHandler(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public async Task HandleAsync(JObject jObject, CancellationToken cancellationToken)
        {
            var policyPurchased = jObject.ToObject<PolicyPurchased>();

            await paymentRepository.SaveAsync(new BankAccountDto
            {
                AccountHolder = policyPurchased.PolicyHolder,
                AccountNumber = policyPurchased.AccountNumber
            });
        }
    }
}