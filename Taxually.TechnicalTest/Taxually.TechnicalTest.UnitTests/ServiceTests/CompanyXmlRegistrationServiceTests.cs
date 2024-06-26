namespace Taxually.TechnicalTest.UnitTests.ServiceTests;

public class CompanyXmlRegistrationServiceTests
{
    private readonly Mock<ITaxuallyQueueClient> taxuallyQueueClient = new();
    private readonly CompanyXmlRegistrationService companyXmlRegistrationService;

    public CompanyXmlRegistrationServiceTests()
    {
        companyXmlRegistrationService = new CompanyXmlRegistrationService(taxuallyQueueClient.Object);
    }

    [Fact]
    public async Task RegisterCompany_Should_CallEnqueueAsync()
    {
        var requst = Factory.CreateVatRegistrationRequest();

        await companyXmlRegistrationService.RegisterCompanyAsync(requst);

        taxuallyQueueClient.Verify(x => x.EnqueueAsync(Constants.XmlQueueName, It.IsAny<string>()), Times.Once);
    }
}