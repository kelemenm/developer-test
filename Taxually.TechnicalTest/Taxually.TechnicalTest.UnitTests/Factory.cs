using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxually.TechnicalTest.UnitTests;

internal static class Factory
{
    internal static VatRegistrationRequest CreateVatRegistrationRequest(string country = "")
    {
        return new Fixture()
            .Build<VatRegistrationRequest>()
            .WithAutoProperties()
            .With(x => x.Country, country)
            .Create();
    }
}
