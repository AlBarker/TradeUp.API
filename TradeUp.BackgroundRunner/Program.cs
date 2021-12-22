// See https://aka.ms/new-console-template for more information
using TradeUp.Core.Services;
using TradeUp.Domain;

Console.WriteLine("Hello, World!");

var resourceContributorService = new ResourceContributorService();
var resourceConsumerService = new ResourceConsumerService();
// TODO this needs to come out of the DB in cases where the app is stopped and restarted
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
    var resourceConsumers = await resourceConsumerService.GetResourceConsumers();

    Console.WriteLine($"Found {resourceContributors.Count} resource contributors to process");

    if(processWeekly)
    {
        await ProcessContributorsForFrequency(resourceContributors, Frequency.Weekly, dayCount);
        await ProcessConsumersForFrequency(resourceConsumers, Frequency.Weekly, dayCount);
    }

    if (processMonthly)
    {
        await ProcessContributorsForFrequency(resourceContributors, Frequency.Monthly, dayCount);
        await ProcessConsumersForFrequency(resourceConsumers, Frequency.Monthly, dayCount);
    }

    await ProcessContributorsForFrequency(resourceContributors, Frequency.Daily, dayCount);
    await ProcessConsumersForFrequency(resourceConsumers, Frequency.Daily, dayCount);
        
    Console.WriteLine("Finished processing... sleeping for 5 seconds...");
    Thread.Sleep(5000);
}


async Task ProcessContributorsForFrequency(IList<ResourceContributor> resourceContributors, Frequency frequencyToProcess, int dayCount)
{
    var contributorsToProcess = resourceContributors.Where(x => x.Frequency == frequencyToProcess).ToList();
    Console.WriteLine($"Processing {frequencyToProcess} contributors: {contributorsToProcess.Count} to process...");

    if (contributorsToProcess.Count == 0)
    {
        return;
    }

    foreach (var resourceContributor in contributorsToProcess)
    {
        await resourceContributorService.ProcessContribution(resourceContributor, dayCount);
    }
}

async Task ProcessConsumersForFrequency(IList<ResourceConsumer> resourceConsumers, Frequency frequencyToProcess, int dayCount)
{
    var consumersToProcess = resourceConsumers.Where(x => x.Frequency == frequencyToProcess).ToList();
    Console.WriteLine($"Processing {frequencyToProcess} contributors: {consumersToProcess.Count} to process...");

    if (consumersToProcess.Count == 0)
    {
        return;
    }

    foreach (var resourceConsumer in consumersToProcess)
    {
        await resourceConsumerService.ProcessConsumption(resourceConsumer, dayCount);
    }
}
