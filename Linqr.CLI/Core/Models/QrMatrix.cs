using Net.Codecrete.QrCodeGenerator;

namespace Linqr.CLI.Core.Models;

/// <summary>
/// Logical map of each module within a <see cref="QrCode"/> for custom renders.
/// </summary>
/// <param name="qrCode">
/// The <see cref="QrCode"/> to map the matrix to.
/// </param>
/// <param name="border">
/// How many modules wide the background extends past the scope of the QR Code.
/// ISO standards recommend 4 modules for maximum compatibility, but 1 module is
/// the minimum recommended for size constraints.
/// </param>
public sealed class QrMatrix(QrCode qrCode, int border = 1)
{
#region Functions

    /// <summary>
    /// Computed size of the QR Code including outer paddings for the border.
    /// </summary>
    public int Size => qrCode.Size + (border * 2);


    /// <summary>
    /// See <see cref="QrCode.GetModule(int,int)"/>
    /// </summary>
    /// <remarks>
    /// This override accounts the <see cref="border"/> size in its calculations
    /// to ensure that the extra space is not shifted to the right of the widget.
    /// </remarks>
    public bool IsActiveBlock(
        int x,
        int y
    ) {
        var qrX = x - border;
        var qrY = y - border;

        if (
            qrX < 0 || qrX >= qrCode.Size ||
            qrY < 0 || qrY >= qrCode.Size
        ) {
            return false;
        }

        return qrCode.GetModule(qrX, qrY);
    }

#endregion
}
