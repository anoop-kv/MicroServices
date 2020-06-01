using System;
using Common.Infrastructure.Contract;

namespace Claim.Api.Command
{
    public class ValidateClaimCommand : ICommand
    {
        public string ClaimId { get; set; }
        
        public string PolicyId { get; set; }

        public decimal ClaimAmount { get; set; }

        public DateTime IncidentDate { get; set; }
    }
}