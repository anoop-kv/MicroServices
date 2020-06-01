using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Infrastructure.Contract;
using Customer.Api.Data;
using Newtonsoft.Json.Linq;

namespace Customer.Api.EventHandlers
{
    public class PolicyPurchasedEventHandler : IServiceEventHandler
    {
        private readonly ICustomerRepository customerRepository;

        public PolicyPurchasedEventHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task HandleAsync(JObject jObject, CancellationToken cancellationToken)
        {
            var policyPurchased = jObject.ToObject<PolicyPurchased>();

            await customerRepository.SaveAsync(new CustomerDto
            {
                CustomerId = Guid.NewGuid().ToString(),
                Name = policyPurchased.PolicyHolder
            });
        }
    }
}