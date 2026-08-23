using Linqr.CLI.Rendering.Models;
using Linqr.SDK.Models;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Linqr.CLI.Rendering.Components.QrCode;

/// <summary>
/// Draw the QR Code using optimized ASCII characters.
/// </summary>
public sealed class CompatQrRenderable(
    Net.Codecrete.QrCodeGenerator.QrCode qrCode,
    QrAppearance appearance
) : QrRenderable
{
#region Inherited

    /// <inheritdoc/>
    protected override IEnumerable<Segment> Render(
        RenderOptions options,
        int maxWidth
    ) {
        var matrix = new QrMatrix(qrCode, appearance.BorderSize);

        var foregroundColor = appearance.ForegroundColor;
        var backgroundColor = appearance.BackgroundColor;

        for (var y = 0; y < matrix.Size; y += 2) {
            for (var x = 0; x < matrix.Size; x++) {
                var topActive = matrix.IsActiveBlock(x, y);
                var botActive = matrix.IsActiveBlock(x, y + 1);
                yield return new Segment(
                    "▀",
                    new Style(
                        foreground: topActive ? foregroundColor : backgroundColor,
                        background: botActive ? foregroundColor : backgroundColor
                    )
                );
            }

            yield return Segment.LineBreak;
        }
    }

#endregion
}
