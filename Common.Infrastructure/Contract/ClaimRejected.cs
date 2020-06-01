namespace Common.Infrastructure.Contract
{
    public class ClaimRejected : IEvent
    {
        public string ClaimId { get; set; }

        public string PolicyId { get; set; }

        public string RejectionReason { get; set; }
    }
}