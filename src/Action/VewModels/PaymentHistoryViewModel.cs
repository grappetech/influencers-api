using System;

namespace Action.VewModels
{
    public class PaymentHistoryViewModel
    {
        public string Id { get; set; }
        public long SecondaryPlanId { get; set; }
        public long PlanId { get; set; }
        public string Status { get; set; }
        public string PaymentType { get; set; }
        public string PaymentIdentifier { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime ExpirationDSaDateTime { get; set; }
    }
}