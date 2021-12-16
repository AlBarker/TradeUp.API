namespace TradeUp.Domain
{
    public class ResourceContributionHistory
    {
        public int Id { get; set; }
        public ResourceContributor ResourceContributor  { get; set; }
        public long ResourceCount { get; set; }
        public double ResourcePriceAtTimeOfContribution    { get; set; }
        public int Day { get; set; }
    }
}