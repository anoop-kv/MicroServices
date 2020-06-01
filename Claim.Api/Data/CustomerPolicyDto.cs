using System;

namespace Claim.Api.Data
{
    public class CustomerPolicyDto
    {
        public DateTime InceptionDate { get; set; }

        public decimal CoverAmount { get; set; }

        public string CustomerId { get; set; }

        public string PolicyId { get; set; }
    }
}