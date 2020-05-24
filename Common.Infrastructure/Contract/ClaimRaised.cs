using System;

namespace Common.Infrastructure.Contract
{
    public class ClaimRaised : IEvent
    {
        public string ClaimId { get; set; }

        public DateTime IncidentDate { get; set; }

        public decimal ClaimAmount { get; set; }

        public string PolicyId { get; set; }
    }
}