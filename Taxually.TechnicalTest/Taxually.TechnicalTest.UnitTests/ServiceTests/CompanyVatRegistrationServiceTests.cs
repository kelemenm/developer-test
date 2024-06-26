namespace Taxually.TechnicalTest.UnitTests.ServiceTests;

public class CompanyVatRegistrationServiceTests
{
    private readonly Mock<ICompanyApiRegistrationService> apiRegistrationService = new();
    private readonly Mock<ICompanyCsvRegistrationService> csvRegistrationService = new();
    private readonly Mock<ICompanyXmlRegistrationService> xmlRegistrationService = new();
    private readonly CompanyVatRegistrationService companyVatRegistrationService;

    public CompanyVatRegistrationServiceTests()
    {
        companyVatRegistrationService = new CompanyVatRegistrationService(
            apiRegistrationService.Object,
            csvRegistrationService.Object,
            xmlRegistrationService.Object);
    }

    [Fact]
    public async Task RegisterCompanyVatNumber_Should_CallApiRegistrationService()
    {
        var request = Factory.CreateVatRegistrationRequest("GB");

        await companyVatRegistrationService.RegisterCompanyVatNumber(request);

        ValidateRegistrationServices(Times.Once(), Times.Never(), Times.Never());
    }

    [Fact]
    public async Task RegisterCompanyVatNumber_Should_CallCsvRegistrationService()
    {
        var request = Factory.CreateVatRegistrationRequest("FR");

        await companyVatRegistrationService.RegisterCompanyVatNumber(request);

        ValidateRegistrationServices(Times.Never(), Times.Once(), Times.Never());
    }

    [Fact]
    public async Task RegisterCompanyVatNumber_Should_CallXmlRegistrationService()
    {
        var request = Factory.CreateVatRegistrationRequest("DE");

        await companyVatRegistrationService.RegisterCompanyVatNumber(request);

        ValidateRegistrationServices(Times.Never(), Times.Never(), Times.Once());
    }

    [Theory]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("HU")]
    public async Task RegisterCompanyVatNumber_Should_ThrowException(string country)
    {
        var request = Factory.CreateVatRegistrationRequest(country);

        var func = async () => await companyVatRegistrationService.RegisterCompanyVatNumber(request);

        ValidateRegistrationServices(Times.Never(), Times.Never(), Times.Never());

        await func.Should().ThrowAsync<Exception>().WithMessage("Country not supported");
    }

    private void ValidateRegistrationServices(Times apiTimes, Times csvTimes, Times xmlTimes)
    {
        apiRegistrationService.Verify(x => x.RegisterCompanyAsync(It.IsAny<VatRegistrationRequest>()), apiTimes);
        csvRegistrationService.Verify(x => x.RegisterCompanyAsync(It.IsAny<VatRegistrationRequest>()), csvTimes);
        xmlRegistrationService.Verify(x => x.RegisterCompanyAsync(It.IsAny<VatRegistrationRequest>()), xmlTimes);
    }
}