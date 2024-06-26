namespace Taxually.TechnicalTest.UnitTests
{
    public class CompanyVatRegistrationServiceTests
    {
        private readonly Mock<ICompanyApiRegistrationService> apiRegistrationService = new();
        private readonly Mock<ICompanyCsvRegistrationService> csvRegistrationService = new();
        private readonly Mock<ICompanyXmlRegistrationService> xmlRegistrationService = new();
        private readonly CompanyVatRegistrationService companyVatRegistrationService;

        public CompanyVatRegistrationServiceTests()
        {
            this.companyVatRegistrationService = new CompanyVatRegistrationService(
                this.apiRegistrationService.Object,
                this.csvRegistrationService.Object,
                this.xmlRegistrationService.Object);
        }

        [Fact]
        public async Task RegisterCompanyVatNumber_Should_CallApiRegistrationService()
        {
            var request = GetVatRegistrationRequest("GB");

            await this.companyVatRegistrationService.RegisterCompanyVatNumber(request);

            ValidateRegistrationServices(Times.Once(), Times.Never(), Times.Never());
        }

        [Fact]
        public async Task RegisterCompanyVatNumber_Should_CallCsvRegistrationService()
        {
            var request = GetVatRegistrationRequest("FR");

            await this.companyVatRegistrationService.RegisterCompanyVatNumber(request);

            ValidateRegistrationServices(Times.Never(), Times.Once(), Times.Never());
        }

        [Fact]
        public async Task RegisterCompanyVatNumber_Should_CallXmlRegistrationService()
        {
            var request = GetVatRegistrationRequest("DE");

            await this.companyVatRegistrationService.RegisterCompanyVatNumber(request);

            ValidateRegistrationServices(Times.Never(), Times.Never(), Times.Once());
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("HU")]
        public async Task RegisterCompanyVatNumber_Should_ThrowException(string country)
        {
            var request = GetVatRegistrationRequest(country);

            var func = async () => await this.companyVatRegistrationService.RegisterCompanyVatNumber(request);

            ValidateRegistrationServices(Times.Never(), Times.Never(), Times.Never());

            await func.Should().ThrowAsync<Exception>().WithMessage("Country not supported");
        }

        private static VatRegistrationRequest GetVatRegistrationRequest(string country)
        {
            return new Fixture()
                .Build<VatRegistrationRequest>()
                .WithAutoProperties()
                .With(x => x.Country, country)
                .Create();
        }

        private void ValidateRegistrationServices(Times apiTimes, Times csvTimes, Times xmlTimes)
        {
            this.apiRegistrationService.Verify(x => x.RegisterCompany(It.IsAny<VatRegistrationRequest>()), apiTimes);
            this.csvRegistrationService.Verify(x => x.RegisterCompany(It.IsAny<VatRegistrationRequest>()), csvTimes);
            this.xmlRegistrationService.Verify(x => x.RegisterCompany(It.IsAny<VatRegistrationRequest>()), xmlTimes);
        }
    }
}