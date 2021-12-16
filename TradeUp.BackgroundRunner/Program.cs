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
        await ProcessContributorsForFrequency(resourceContributors, Frequency.Weekly);
    }

    if (processMonthly)
    {
        await ProcessContributorsForFrequency(resourceContributors, Frequency.Monthly);
    }

    await ProcessContributorsForFrequency(resourceContributors, Frequency.Daily);

    Console.WriteLine("Finished processing... sleeping for 5 seconds...");
    Thread.Sleep(5000);
}


async Task ProcessContributorsForFrequency(IList<ResourceContributor> resourceContributors, Frequency frequencyToProcess)
{
    var contributorsToProcess = resourceContributors.Where(x => x.Frequency == frequencyToProcess).ToList();
    Console.WriteLine($"Processing {frequencyToProcess.ToString()} contributors: {contributorsToProcess.Count} to process...");

    if (contributorsToProcess.Count == 0)
    {
        return;
    }

    foreach (var resourceContributor in contributorsToProcess)
    {
        await resourceContributorService.ProcessContribution(resourceContributor);
    }
}
