using System.ComponentModel;
using JetBrains.Annotations;
using Linqr.CLI.Core.Models;
using Linqr.CLI.View.Components;
using Linqr.CLI.View.Models;
using Net.Codecrete.QrCodeGenerator;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Linqr.CLI.Core.Commands;

/// <summary>
/// Command to render a specified URL as a QR Code.
/// </summary>
[UsedImplicitly]
public sealed class EncodeCommand
    : Command<EncodeCommandSettings>
{
#region Inherited

    /// <inheritdoc />
    protected override int Execute(
        CommandContext env,
        EncodeCommandSettings options,
        CancellationToken ctx
    ) {
        var qrCode = QrCode.EncodeText(
            options.Text,
            options.ErrorCorrection switch {
                EccFlags.L => QrCode.Ecc.Low, EccFlags.M => QrCode.Ecc.Medium,
                EccFlags.H => QrCode.Ecc.High, EccFlags.Q => QrCode.Ecc.Quartile,
                _ => throw new InvalidEnumArgumentException()
            }
        );

        var qrAppearance = new QrAppearance(
            ForegroundColor: options.ForegroundColor,
            BackgroundColor: options.BackgroundColor,
            BorderSize: options.Border
        );

        QrRenderable qrWidget = (options.UseCanvasWidget)
            ? new CanvasQrRenderable(qrCode, qrAppearance)
            : new CompatQrRenderable(qrCode, qrAppearance);

        var panel = new Panel(qrWidget) {
            Border = BoxBorder.None,
            Padding = new Padding(
                horizontal: options.PaddingX,
                vertical: options.PaddingY
            )
        };

        AnsiConsole.Write(panel);

        return (int) ExitCode.Success;
    }

#endregion
}
