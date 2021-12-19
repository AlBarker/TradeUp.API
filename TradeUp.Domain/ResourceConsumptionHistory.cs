namespace TradeUp.Domain
{
    public class ResourceConsumptionHistory
    {
        public int Id { get; set; }
        public ResourceConsumer ResourceConsumer  { get; set; }
        public long ResourceCountAfterConsumption { get; set; }
        public long ResourcesConsumed { get; set; }
        public double ResourcePriceAtTimeOfConsumption    { get; set; }
        public int Day { get; set; }
    }
}