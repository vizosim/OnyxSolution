namespace Onyx.Application.Models;

public enum VerificationStatus
{
    Valid,
    Invalid,
    // Unable to get status (e.g. service unavailable)
    Unavailable
}
