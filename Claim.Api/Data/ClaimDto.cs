using System;

namespace Claim.Api.Data
{
    public class ClaimDto
    {
        public string PolicyId { get; set; }

        public DateTime IncidentDate { get; set; }

        public decimal ClaimAmount { get; set; }
    }
}