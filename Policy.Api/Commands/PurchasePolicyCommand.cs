using System;
using Common.Infrastructure.Contract;

namespace Policy.Api.Commands
{
    public class PurchasePolicyCommand : ICommand
    {
        public string PolicyId { get; set; }

        public DateTime InceptionDate { get; set; }

        public decimal CoverAmount { get; set; }
    
        public string PolicyHolder { get; set; }

        public string AccountNumber { get; set; }
    }
}