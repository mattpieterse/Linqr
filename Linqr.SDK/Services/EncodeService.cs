using Linqr.SDK.Requests;
using Linqr.SDK.Requests.Payloads;
using Net.Codecrete.QrCodeGenerator;

namespace Linqr.SDK.Services;

public class EncodeService : IEncodeService
{
#region Inherited

    /// <summary>
    /// Encodes the given <paramref name="request" /> as an
    /// <see cref="EncodeResponse"/> which contains the encoded QR Codes needed
    /// by presentation-layer components.
    /// </summary>
    /// <returns>
    /// <see cref="EncodeResponse"/>
    /// </returns>
    public EncodeResponse Encode(
        EncodeTextRequest request
    ) {
        var ins = request.Input.Read();
        var qrs = ins
            .Select(input => QrCode.EncodeText(input, request.QrCode.Ecc))
            .ToList();

        HandleOutputIo(request.Output);
        return new EncodeResponse(qrs);
    }

#endregion

#region Internals

    /// <summary>
    /// TODO
    /// </summary>
    private static void HandleOutputIo(
        OutputSpecification outputSpecification
    ) {
        // TODO
    }

#endregion
}
