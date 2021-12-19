namespace TradeUp.Core.Requests
{
    public class AddConsumerRequest
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public int ConsumptionFactor { get; set; }
    }
}