namespace Taxually.TechnicalTest.UnitTests.ServiceTests;

public class CompanyApiRegistrationServiceTests
{
    private const string TestUrl = "testurl.com";

    private readonly Mock<ITaxuallyHttpClient> taxuallyHttpClient = new();
    private readonly CompanyApiRegistrationService companyApiRegistrationService;

    public CompanyApiRegistrationServiceTests()
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            { "UkUrl", TestUrl },
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();

        companyApiRegistrationService = new CompanyApiRegistrationService(
            configuration,
            taxuallyHttpClient.Object);
    }

    [Fact]
    public async Task RegisterCompany_Should_CallEnqueueAsync()
    {
        var request = Factory.CreateVatRegistrationRequest();

        await companyApiRegistrationService.RegisterCompany(request);

        taxuallyHttpClient.Verify(x => x.PostAsync(TestUrl, request), Times.Once);
    }
}