namespace Taxually.TechnicalTest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VatRegistrationController : ControllerBase
{
    private readonly ICompanyVatRegistrationService companyVatRegistrationService;

    public VatRegistrationController(ICompanyVatRegistrationService companyVatRegistrationService)
    {
        this.companyVatRegistrationService = companyVatRegistrationService;
    }

    /// <summary>
    /// Registers a company for a VAT number in a given country
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] VatRegistrationRequest request)
    {
        await companyVatRegistrationService.RegisterCompanyVatNumber(request);

        return Ok();
    }
}
