using TradeUp.Domain;

namespace TradeUp.Core.Requests
{
    public class AddResourceContributorRequest
    {
        public int ResourceId { get; set; }
        public int ContributorId { get; set; }
        public Frequency Frequency { get; set; }
        public int MinContributionRange { get; set; }
        public int MaxContributionRange { get; set; }
    }
}