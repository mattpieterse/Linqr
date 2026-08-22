using Linqr.SDK.Services;
using Net.Codecrete.QrCodeGenerator;

namespace Linqr.SDK.Requests;

/// <summary>
/// Response payload from the <see cref="IEncodeService"/>.
/// </summary>
public sealed record EncodeResponse(
    IEnumerable<QrCode> QrCodes
);
