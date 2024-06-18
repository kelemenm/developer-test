namespace Taxually.TechnicalTest
{
    public sealed record VatRegistrationRequest
    {
        public string CompanyName { get; set; }
        public string CompanyId { get; set; }
        public string Country { get; set; }
    }
}
