namespace TradeUp.Domain
{
    public class ResourceContributor
    {
        public int Id { get; set; }
        public Contributor Contributor { get; set; }
        public Resource Resource { get; set; }
        public Frequency Frequency { get; set; }
        public int MinContributionRange { get; set; }
        public int MaxContributionRange { get; set; }
    }
}