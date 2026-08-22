using Net.Codecrete.QrCodeGenerator;

namespace Linqr.SDK.Requests.Payloads;

public sealed record QrCodeSpecification(
    QrCode.Ecc Ecc
);
