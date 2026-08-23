using Linqr.SDK.Models;
using Net.Codecrete.QrCodeGenerator;

namespace Linqr.CLI.Mappers;

/// <summary>
/// Presentation to SDK-layer mapping functions for <see cref="EcclCode"/>.
/// </summary>
internal static class QrCodeEnumMapper
{
#region Functions

    /// <summary>
    /// Converts a local <see cref="EcclCode"/> to a Spectre
    /// <see cref="QrCode.Ecc"/> enumeration for use in the SDK-layer QR Code
    /// generation process.
    /// </summary>
    /// <returns>
    /// <see cref="QrCode.Ecc"/> (Codecrete)
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// When the value of the <paramref name="value"/> parameter is not a
    /// configured <see cref="EcclCode"/>. This will occur during regressions to
    /// the codebase.
    /// </exception>
    public static QrCode.Ecc ToQrEcc(
        EcclCode value
    ) {
        return value switch {
            EcclCode.L => QrCode.Ecc.Low,
            EcclCode.M => QrCode.Ecc.Medium,
            EcclCode.Q => QrCode.Ecc.Quartile,
            EcclCode.H => QrCode.Ecc.High,

            // Fallback
            _ => throw new ArgumentOutOfRangeException(
                message: "This value could not be mapped to a QrCode.Ecc Enumeration.",
                paramName: nameof(value),
                actualValue: value
            )
        };
    }

#endregion
}
