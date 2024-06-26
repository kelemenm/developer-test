namespace Taxually.TechnicalTest.Services;

public class CompanyCsvRegistrationService : ICompanyCsvRegistrationService
{
    private readonly ITaxuallyQueueClient taxuallyQueueClient;

    public CompanyCsvRegistrationService(ITaxuallyQueueClient taxuallyQueueClient)
    {
        this.taxuallyQueueClient = taxuallyQueueClient;
    }

    public async Task RegisterCompanyAsync(VatRegistrationRequest request)
    {
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("CompanyName,CompanyId");
        csvBuilder.AppendLine($"{request.CompanyName}{request.CompanyId}");
        var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());

        await this.taxuallyQueueClient.EnqueueAsync(Constants.CsvQueueName, csv);
    }
}