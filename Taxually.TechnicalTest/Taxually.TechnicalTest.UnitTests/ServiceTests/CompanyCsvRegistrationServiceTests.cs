namespace Taxually.TechnicalTest.UnitTests.ServiceTests;

public class CompanyCsvRegistrationServiceTests
{
    private readonly Mock<ITaxuallyQueueClient> taxuallyQueueClient = new();
    private readonly CompanyCsvRegistrationService companyCsvRegistrationService;

    public CompanyCsvRegistrationServiceTests()
    {
        companyCsvRegistrationService = new CompanyCsvRegistrationService(taxuallyQueueClient.Object);
    }

    [Fact]
    public async Task RegisterCompany_Should_CallEnqueueAsync()
    {
        var request = Factory.CreateVatRegistrationRequest();

        await companyCsvRegistrationService.RegisterCompanyAsync(request);

        taxuallyQueueClient.Verify(x => x.EnqueueAsync(Constants.CsvQueueName, It.IsAny<byte[]>()), Times.Once);
    }
}