namespace Taxually.TechnicalTest.Services
{
    public class CompanyApiRegistrationService : ICompanyApiRegistrationService
    {
        private const string UkUrl = "https://api.uktax.gov.uk";

        private readonly ITaxuallyHttpClient taxuallyHttpClient;

        public CompanyApiRegistrationService(ITaxuallyHttpClient taxuallyHttpClient)
        {
            this.taxuallyHttpClient = taxuallyHttpClient;
        }

        public async Task RegisterCompany(VatRegistrationRequest request)
        {
            await this.taxuallyHttpClient.PostAsync(UkUrl, request);
        }
    }
}