using Linqr.CLI.Core.Models;
using Linqr.CLI.View.Models;
using Net.Codecrete.QrCodeGenerator;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Linqr.CLI.View.Components;

/// <summary>
/// Draw the QR Code using <see cref="Canvas"/>
/// </summary>
/// <remarks>
/// In some terminal environments this method may lead to artifacting in general
/// or when zooming the widget in/out beyond the borders of the window.
/// </remarks>
public sealed class CanvasQrRenderable(QrCode qrCode, QrAppearance appearance)
    : QrRenderable
{
#region Inherited

    /// <inheritdoc/>
    /// <remarks>
    /// See compatability notice in <see cref="CanvasQrRenderable"/>
    /// </remarks>
    protected override IEnumerable<Segment> Render(
        RenderOptions options,
        int maxWidth
    ) {
        var matrix = new QrMatrix(qrCode, appearance.BorderSize);
        var canvas = new Canvas(
            matrix.Size,
            matrix.Size
        );

        for (var y = 0; y < matrix.Size; y++) {
            for (var x = 0; x < matrix.Size; x++) {
                canvas.SetPixel(
                    x,
                    y,
                    matrix.IsActiveBlock(x, y)
                        ? appearance.ForegroundColor
                        : appearance.BackgroundColor
                );
            }
        }

        IRenderable widget = canvas;
        return widget.Render(options, maxWidth);
    }

#endregion
}
