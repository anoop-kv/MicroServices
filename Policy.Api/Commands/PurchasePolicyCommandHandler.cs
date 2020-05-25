using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Infrastructure.Contract;
using Common.Infrastructure.Kafka;
using Policy.Api.Data;

namespace Policy.Api.Commands
{
    public class PurchasePolicyCommandHandler : ICommandHandler<PurchasePolicyCommand>
    {
        private readonly IKafkaProducer kafkaProducer;
        private readonly IPolicyRepository policyRepository; 

        public PurchasePolicyCommandHandler(IKafkaProducer kafkaProducer, IPolicyRepository policyRepository)
        {
            this.kafkaProducer = kafkaProducer;
            this.policyRepository = policyRepository;
        }

        public async Task HandleAsync(PurchasePolicyCommand command)
        {
            await policyRepository.SaveAsync(new Data.Policy());

            await kafkaProducer.SendAsync(new PolicyPurchased
            {
                AccountNumber = "account123",
                CoverAmount = 100,
                InceptionDate = DateTime.Now,
                PolicyHolder = "Anoop Venugopalan",
                PolicyId = "Policy123"
            }, "Policy");
        }
    }
}