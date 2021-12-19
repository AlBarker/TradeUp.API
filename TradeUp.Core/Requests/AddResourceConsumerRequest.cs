using TradeUp.Domain;

namespace TradeUp.Core.Requests
{
    public class AddResourceConsumerRequest
    {
        public int ResourceId { get; set; }
        public int ConsumerId { get; set; }
        public Frequency Frequency { get; set; }
        public int MinConsumptionRange { get; set; }
        public int MaxConsumptionRange { get; set; }
    }
}