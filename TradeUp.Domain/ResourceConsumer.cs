namespace TradeUp.Domain
{
    public class ResourceConsumer
    {
        public int Id { get; set; }
        public Consumer Consumer { get; set; }
        public Resource Resource { get; set; }
        public Frequency Frequency { get; set; }
        public int MinConsumptionRange { get; set; }
        public int MaxConsumptionRange { get; set; }
    }
}