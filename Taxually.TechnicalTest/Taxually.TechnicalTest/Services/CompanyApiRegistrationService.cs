namespace Taxually.TechnicalTest.Services;

public class CompanyApiRegistrationService : ICompanyApiRegistrationService
{
    private readonly string UkUrl;

    private readonly ITaxuallyHttpClient taxuallyHttpClient;

    public CompanyApiRegistrationService(
        IConfiguration configuration,
        ITaxuallyHttpClient taxuallyHttpClient)
    {
        this.taxuallyHttpClient = taxuallyHttpClient;

        this.UkUrl = configuration.GetValue<string>("UkUrl");
    }

    public async Task RegisterCompanyAsync(VatRegistrationRequest request)
    {
        await this.taxuallyHttpClient.PostAsync(this.UkUrl, request);
    }
}