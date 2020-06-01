using System;

namespace Policy.Api.Data
{
    public class PolicyDto
    {
        public string PolicyId { get; set; }

        public DateTime InceptionDate { get; set; }

        public decimal CoverAmount { get; set; }
    
        public string PolicyHolder { get; set; }

        public string AccountNumber { get; set; }
    }
}