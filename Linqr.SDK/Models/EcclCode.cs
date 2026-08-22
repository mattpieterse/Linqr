using Net.Codecrete.QrCodeGenerator;

namespace Linqr.SDK.Models;

/// <summary>
/// Convenience alias for <see cref="QrCode.Ecc"/>
/// </summary>
public enum EcclCode
{
#region Enum

    /// <summary>
    /// Equivalent to <see cref="QrCode.Ecc.Low"/>
    /// </summary>
    L = 1,


    /// <summary>
    /// Equivalent to <see cref="QrCode.Ecc.Medium"/>
    /// </summary>
    M = 2,


    /// <summary>
    /// Equivalent to <see cref="QrCode.Ecc.High"/>
    /// </summary>
    H = 3,


    /// <summary>
    /// Equivalent to <see cref="QrCode.Ecc.Quartile"/>
    /// </summary>
    Q = 4

#endregion
}
