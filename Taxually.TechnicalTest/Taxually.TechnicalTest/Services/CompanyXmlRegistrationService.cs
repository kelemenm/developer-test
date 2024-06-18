namespace Taxually.TechnicalTest.Services;

public class CompanyXmlRegistrationService : ICompanyXmlRegistrationService
{
    private readonly ITaxuallyQueueClient taxuallyQueueClient;

    public CompanyXmlRegistrationService(ITaxuallyQueueClient taxuallyQueueClient)
    {
        this.taxuallyQueueClient = taxuallyQueueClient;
    }

    public async Task RegisterCompany(VatRegistrationRequest request)
    {
        using var stringwriter = new StringWriter();
        var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
        serializer.Serialize(stringwriter, this);
        var xml = stringwriter.ToString();
        
        await this.taxuallyQueueClient.EnqueueAsync("vat-registration-xml", xml);
    }
}