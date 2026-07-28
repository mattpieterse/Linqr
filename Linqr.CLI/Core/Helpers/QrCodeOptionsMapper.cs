using Linqr.CLI.Core.Models;
using Net.Codecrete.QrCodeGenerator;

namespace Linqr.CLI.Core.Helpers;

internal static class QrCodeOptionsMapper
{
#region Functions

    public static QrCode.Ecc ToQrEcc(
        EccFlags value
    ) {
        return value switch {
            EccFlags.L => QrCode.Ecc.Low,
            EccFlags.M => QrCode.Ecc.Medium,
            EccFlags.Q => QrCode.Ecc.Quartile,
            EccFlags.H => QrCode.Ecc.High,

            // Fallback
            _ => throw new ArgumentOutOfRangeException(
                message: "This value could not be mapped to a QrCode.Ecc Enum",
                paramName: nameof(value),
                actualValue: value
            )
        };
    }

#endregion
}
