namespace Onyx.Application.Models;

public enum VerificationStatus
{
    Valid,
    Invalid,
    // Unable to get status (e.g. service unavailable)
    Unavailable
}

public interface IVatVerifier
{
    Task<VatVerificationResult> VerifyVAT(string countryCode, string vatId);
}

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

public interface IVarVerificationService
{
    Task<VatVerificationResult> VerifyVAT(VatVerificationRequest request);
}

public class VarVerificationService : IVarVerificationService
{
    private readonly checkVatPortTypeClient _client;

    public VarVerificationService()
    {
        _client = new checkVatPortTypeClient();
    }

    public async Task<VatVerificationResult> VerifyVAT(VatVerificationRequest verificationRequest)
    {
        var result = await _client.checkVatAsync(new(verificationRequest.CountryCode, verificationRequest.VatId));

        var status = result.valid ? VerificationStatus.Valid : VerificationStatus.Invalid;
        return new VatVerificationResult(result.countryCode, result.vatNumber, status);
    }
}

public record VatVerificationResult(string CountryCode, string VatNumber, VerificationStatus Status);

public record VatVerificationRequest(string CountryCode, string VatId);