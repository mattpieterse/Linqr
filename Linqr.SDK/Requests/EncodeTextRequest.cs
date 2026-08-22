using Linqr.SDK.IO.InputSources;
using Linqr.SDK.Requests.Payloads;

namespace Linqr.SDK.Requests;

public sealed record EncodeTextRequest(
    IInputSource Input,
    QrCodeSpecification QrCode,
    OutputSpecification Output
);
