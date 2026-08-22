using Linqr.CLI.Commands.Encode;
using Linqr.CLI.Rendering.Components.QrCode;
using Linqr.CLI.Rendering.Models;
using Linqr.SDK.Models;
using Linqr.SDK.Requests;
using Net.Codecrete.QrCodeGenerator;
using Spectre.Console;

namespace Linqr.CLI.Rendering;

/// <summary>
/// Delegate terminal writer for encoding commands.
/// </summary>
public static class EncodeTerminalWriter
{
#region Functions

    /// <summary>
    /// Writes to the <see cref="AnsiConsole"/>.
    /// </summary>
    public static void Draw(
        EncodeResponse response,
        EncodeTextCommandSettings settings
    ) {
        if (settings.ClearTerminal) AnsiConsole.Clear();

        var qrAppearance = BuildQrAppearanceSpecification(settings);
        foreach (var qrCode in response.QrCodes) {
            var qrWidget = BuildQrRenderable(settings, qrAppearance, qrCode);
            var spectrePanel = new Panel(qrWidget) {
                Border = BoxBorder.None,
                Padding = new Padding(
                    horizontal: settings.QrCodeVisualOffsetX ?? settings.QrCodeVisualOffset,
                    vertical: settings.QrCodeVisualOffsetY ?? settings.QrCodeVisualOffset
                )
            };

            AnsiConsole.Write(spectrePanel);
        }
    }

#endregion

#region Internals

    private static QrRenderable BuildQrRenderable(
        EncodeCommandSettings settings,
        QrAppearance qrAppearance,
        QrCode qrCode
    ) {
        return (settings.TerminalQrCodeRenderer == VisualizerFlags.Canvas)
            ? new CanvasQrRenderable(qrCode, qrAppearance)
            : new CompatQrRenderable(qrCode, qrAppearance);
    }


    private static QrAppearance BuildQrAppearanceSpecification(
        EncodeCommandSettings settings
    ) {
        return new QrAppearance(
            ForegroundColor: (
                (settings.InvertQrCodeColors)
                    ? settings.QrCodeBackgroundColor
                    : settings.QrCodeForegroundColor
            ),
            BackgroundColor: (
                (settings.InvertQrCodeColors)
                    ? settings.QrCodeForegroundColor
                    : settings.QrCodeBackgroundColor
            ),
            BorderSize: settings.QrCodeVisualBorder
        );
    }

#endregion
}
