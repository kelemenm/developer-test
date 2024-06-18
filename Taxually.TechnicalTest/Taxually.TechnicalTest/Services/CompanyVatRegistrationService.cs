namespace Taxually.TechnicalTest.Services;

public class CompanyVatRegistrationService : ICompanyVatRegistrationService
{
    private readonly ICompanyApiRegistrationService apiRegistrationService;
    private readonly ICompanyCsvRegistrationService csvRegistration;
    private readonly ICompanyXmlRegistrationService xmlRegistrationService;

    public CompanyVatRegistrationService(
        ICompanyApiRegistrationService apiRegistrationService, 
        ICompanyCsvRegistrationService csvRegistration, 
        ICompanyXmlRegistrationService xmlRegistrationService)
    {
        this.apiRegistrationService = apiRegistrationService;
        this.csvRegistration = csvRegistration;
        this.xmlRegistrationService = xmlRegistrationService;
    }

    public async Task RegisterCompanyVatNumber(VatRegistrationRequest request)
    {
        switch (request.Country)
        {
            case "GB":
                // UK has an API to register for a VAT number
                await this.apiRegistrationService.RegisterCompany(request);
                break;
            case "FR":
                await this.csvRegistration.RegisterCompany(request);
                break;
            case "DE":
                await this.xmlRegistrationService.RegisterCompany(request);
                break;
            default:
                throw new Exception("Country not supported");

        }
    }
}