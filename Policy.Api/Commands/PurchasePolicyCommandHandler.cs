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
            await policyRepository.SaveAsync(MapToDto(command));

            await kafkaProducer.SendAsync(new PolicyPurchased
            {
                AccountNumber = command.AccountNumber,
                CoverAmount = command.CoverAmount,
                InceptionDate = command.InceptionDate,
                PolicyHolder = command.PolicyHolder,
                PolicyId = command.PolicyId
            }, "Policy");
        }

        private static PolicyDto MapToDto(PurchasePolicyCommand command)
        {
            return new PolicyDto
            {
                AccountNumber = command.AccountNumber,
                CoverAmount = command.CoverAmount,
                InceptionDate = command.InceptionDate,
                PolicyHolder = command.PolicyHolder,
                PolicyId = command.PolicyId
            };
        }
    }
}