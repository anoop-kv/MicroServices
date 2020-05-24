using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Infrastructure.Contract;
using Common.Infrastructure.Kafka;

namespace Policy.Api.Commands
{
    public class PurchasePolicyCommandHandler : ICommandHandler<PurchasePolicyCommand>
    {
        private readonly IKafkaProducer kafkaProducer;

        public PurchasePolicyCommandHandler(IKafkaProducer kafkaProducer)
        {
            this.kafkaProducer = kafkaProducer;
        }

        public async Task HandleAsync(PurchasePolicyCommand command, CancellationToken cancellationToken)
        {
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