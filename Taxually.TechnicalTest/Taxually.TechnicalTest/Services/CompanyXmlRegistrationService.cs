namespace Taxually.TechnicalTest.Services
{
    public class CompanyXmlRegistrationService : ICompanyXmlRegistrationService
    {
        public async Task RegisterCompany(VatRegistrationRequest request)
        {
            using var stringwriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
            serializer.Serialize(stringwriter, this);
            var xml = stringwriter.ToString();
            var xmlQueueClient = new TaxuallyQueueClient();
            // Queue xml doc to be processed
            await xmlQueueClient.EnqueueAsync("vat-registration-xml", xml);
        }
    }
}