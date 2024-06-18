namespace Taxually.TechnicalTest.Services
{
    public class CompanyApiRegistrationService : ICompanyApiRegistrationService
    {
        private const string UkUrl = "https://api.uktax.gov.uk";

        public async Task RegisterCompany(VatRegistrationRequest request)
        {
            var httpClient = new TaxuallyHttpClient();
            await httpClient.PostAsync(UkUrl, request);
        }
    }
}