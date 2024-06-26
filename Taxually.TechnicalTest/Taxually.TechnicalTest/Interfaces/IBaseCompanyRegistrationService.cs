namespace Taxually.TechnicalTest.Interfaces;

public interface IBaseCompanyRegistrationService
{
    Task RegisterCompanyAsync(VatRegistrationRequest request);
}
