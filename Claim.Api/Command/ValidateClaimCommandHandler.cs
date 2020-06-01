using System;
using System.Threading.Tasks;
using Claim.Api.Data;
using Common.Infrastructure.Contract;
using Common.Infrastructure.Kafka;

namespace Claim.Api.Command
{
    public class ValidateClaimCommandHandler : ICommandHandler<ValidateClaimCommand>
    {
        private readonly IClaimRepository claimRepository;
        private readonly IKafkaProducer producer;

        public ValidateClaimCommandHandler(IClaimRepository claimRepository, IKafkaProducer producer)
        {
            this.claimRepository = claimRepository;
            this.producer = producer;
        }

        public async Task HandleAsync(ValidateClaimCommand command)
        {
            await claimRepository.SaveClaimAsync(new ClaimDto
            {
                ClaimAmount = command.ClaimAmount,
                IncidentDate = command.IncidentDate,
                PolicyId = command.PolicyId
            });

            var policy = await claimRepository.GetPolicyAsync(command.PolicyId);

            if (IsClaimAmountCovered(policy.CoverAmount, command.ClaimAmount) == false)
            {
                var claimRejected = BuildClaimRejectedEvent(command,"Claim amount not covered!");
                await producer.SendAsync(claimRejected, "Claim");

                return;
            }

            if (IsPolicyValidOnIncidentDate(policy.InceptionDate, command.IncidentDate) == false)
            {
                var claimRejected = BuildClaimRejectedEvent(command,"Policy not active on indident date !");
                await producer.SendAsync(claimRejected, "Claim");

                return;
            }

            await producer.SendAsync(new ClaimApproved
            {
                ApprovedAmount = command.ClaimAmount,
                ClaimId = command.ClaimId,
                PolicyId = command.PolicyId
            }, "Claim");
        }

        private static ClaimRejected BuildClaimRejectedEvent(ValidateClaimCommand command, string rejectionReason)
        {
            return new ClaimRejected
            {
                ClaimId = command.ClaimId,
                PolicyId = command.PolicyId,
                RejectionReason = rejectionReason
            };
        }

        private static bool IsClaimAmountCovered(decimal coverAmount, decimal claimAmount)
        {
            return claimAmount < coverAmount;
        }

        private static bool IsPolicyValidOnIncidentDate(DateTime inceptionDate, DateTime incidentDate)
        {
            return incidentDate > inceptionDate;
        }
    }
}