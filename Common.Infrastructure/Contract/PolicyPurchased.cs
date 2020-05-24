using System;

namespace Common.Infrastructure.Contract
{
    public class PolicyPurchased : IEvent
    {
        public string PolicyId { get; set; }

        public DateTime InceptionDate { get; set; }

        public decimal CoverAmount { get; set; }
    
        public string PolicyHolder { get; set; }

        public string AccountNumber { get; set; }
    }
}