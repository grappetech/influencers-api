using System;
using Newtonsoft.Json;

namespace Action.VewModels
{
    public class PaymentHistoryViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("secondaryPlan")]
        public long SecondaryPlanId { get; set; }
        [JsonProperty("planId")]
        public long PlanId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("paymentType")]
        public string PaymentType { get; set; }
        [JsonProperty("paymentIdentifier")]
        public string PaymentIdentifier { get; set; }
        [JsonProperty("paymentDueDuate")]
        public DateTime PaymentDueDate { get; set; }
        [JsonProperty("paymentDate")]
        public DateTime PaymentDate { get; set; }
        [JsonProperty("expirationDSaDateTime")]
        public DateTime ExpirationDSaDateTime { get; set; }
    }
}