namespace TradeUp.Core.Requests
{
    public class AddResourceRequest
    {
        public string Name { get; set; }
        public int AvailableAmount { get; set; }
        public double Price { get; set; }
    }
}