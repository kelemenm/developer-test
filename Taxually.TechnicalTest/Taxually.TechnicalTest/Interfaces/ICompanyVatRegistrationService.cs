﻿namespace Taxually.TechnicalTest.Interfaces;

public interface ICompanyVatRegistrationService
{
    Task RegisterCompanyVatNumber(VatRegistrationRequest request);
}