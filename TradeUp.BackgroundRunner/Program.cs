// See https://aka.ms/new-console-template for more information
using TradeUp.Core.Services;
using TradeUp.Domain;

Console.WriteLine("Hello, World!");

var resourceContributorService = new ResourceContributorService();
var dayCount = 0;

const int DAYS_IN_MONTH = 30;
const int DAYS_IN_WEEK = 7;

while (true)
{
    bool processWeekly = false, processMonthly = false;

    dayCount++;

    if (dayCount % DAYS_IN_WEEK == 0)
    {
        processWeekly = true;
    }

    if (dayCount % DAYS_IN_MONTH == 0)
    {
        processMonthly = true;
    }


    var resourceContributors = await resourceContributorService.GetResourceContributors();

    Console.WriteLine($"Found {resourceContributors.Count} resource contributors to process");

    if(processWeekly)
    {
        ProcessWeekly(resourceContributors.Where(x => x.Frequency == Frequency.Weekly).ToList());
    }

    if (processMonthly)
    {
        ProcessMonthly(resourceContributors.Where(x => x.Frequency == Frequency.Monthly).ToList());
    }

    ProcessDaily(resourceContributors.Where(x => x.Frequency == Frequency.Daily).ToList());

    Console.WriteLine("Finished processing... sleeping for 5 seoncds now...");
    Thread.Sleep(5000);
}

void ProcessWeekly(IList<ResourceContributor> resourceContributors)
{
    Console.WriteLine($"Processing weekly contributors: {resourceContributors.Count} to process...");
}

void ProcessMonthly(IList<ResourceContributor> resourceContributors)
{
    Console.WriteLine($"Processing monthly contributors: {resourceContributors.Count} to process...");
}

void ProcessDaily(IList<ResourceContributor> resourceContributors)
{
    Console.WriteLine($"Processing daily contributors: {resourceContributors.Count} to process...");
}
