using Onyx.Application.VAT;

namespace Onyx.Application.Models;

public class VatVerifier : IVatVerifier
{
    private readonly IVarVerificationService _verificationService;

    public VatVerifier(IVarVerificationService verificationService)
    {
        _verificationService = verificationService;
    }

    /// <summary>
    /// Verifies the given VAT ID for the given country using the EU VIES web service.
    /// </summary>
    /// <param name="countryCode"></param>
    /// <param name="vatId"></param>
    /// <returns>Verification status</returns>
    public async Task<VatVerificationResult> VerifyVAT(string countryCode, string vatId)
    {
        try
        {
            var response = await _verificationService.VerifyVAT(new VatVerificationRequest(countryCode, vatId));
            return response;
        }
        catch (Exception)
        {
            return new VatVerificationResult(countryCode, vatId, VerificationStatus.Unavailable);
        }
    }
}

public interface IVatVerifier
{
    Task<VatVerificationResult> VerifyVAT(string countryCode, string vatId);
}