using System.ComponentModel;
using JetBrains.Annotations;
using Linqr.CLI.Core.Helpers;
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
        var qrAppearance = new QrAppearance(
            ForegroundColor: (options.InvertColors) ? options.BackgroundColor : options.ForegroundColor,
            BackgroundColor: (options.InvertColors) ? options.ForegroundColor : options.BackgroundColor,
            BorderSize: options.Border
        );

        if (options.ClearTerminal) AnsiConsole.Clear();

        var request = options.Text
            .Where(text => !string.IsNullOrWhiteSpace(text));

        foreach (var input in request) {
            var qrCode = QrCode.EncodeText(
                input,
                QrCodeOptionsMapper.ToQrEcc(
                    options.ErrorCorrection
                )
            );

            QrRenderable qrWidget = (options.Visualizer == VisualizerFlags.Canvas)
                ? new CanvasQrRenderable(qrCode, qrAppearance)
                : new CompatQrRenderable(qrCode, qrAppearance);

            var panel = new Panel(qrWidget) {
                Border = BoxBorder.None,
                Padding = (options.Margin) switch {
                    > 0 => new Padding(options.Margin),
                    _ => new Padding(
                        horizontal: options.MarginX,
                        vertical: options.MarginY
                    )
                }
            };

            AnsiConsole.Write(panel);
        }

        return (int) ExitCode.Success;
    }

#endregion
}
