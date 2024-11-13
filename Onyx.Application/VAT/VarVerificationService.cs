using Onyx.Application.Models;

namespace Onyx.Application.VAT;

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

public interface IVarVerificationService
{
    Task<VatVerificationResult> VerifyVAT(VatVerificationRequest request);
}

public record VatVerificationResult(string CountryCode, string VatNumber, VerificationStatus Status);

public record VatVerificationRequest(string CountryCode, string VatId);