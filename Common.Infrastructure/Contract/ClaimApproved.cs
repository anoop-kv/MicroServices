namespace Common.Infrastructure.Contract
{
    public class ClaimApproved : IEvent
    {
        public string PolicyId { get; set; }

        public string ClaimId { get; set; }

        public decimal ApprovedAmount { get; set; }
    }
}