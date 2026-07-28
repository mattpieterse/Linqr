using System.ComponentModel;
using JetBrains.Annotations;
using Linqr.CLI.Core.Helpers;
using Linqr.CLI.Core.Models;
using Net.Codecrete.QrCodeGenerator;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Linqr.CLI.Core.Commands;

/// <summary>
/// Command settings for <see cref="EncodeCommand"/>
/// </summary>
public sealed class EncodeCommandSettings
    : CommandSettings
{
#region Arguments

    [CommandArgument(0, "<TEXT>")]
    [Description(
        "The text you wish to encode and display as a QR Code."
    )]
    public string[] Text { get; [UsedImplicitly] set; } = [];

#region Arguments > Flags

    [CommandOption("-v|--visualizer")]
    [Description(
        $"""
         Select the rendering engine to use to draw your QR Code.
         [DIM]Options: 
             - {nameof(VisualizerFlags.Compat)} [[1]]
             - {nameof(VisualizerFlags.Canvas)} [[2]][/]
         """
    )]
    [DefaultValue(nameof(VisualizerFlags.Compat))]
    public VisualizerFlags Visualizer { get; [UsedImplicitly] set; }


    [CommandOption("-c|--clear")]
    [Description(
        "Clear the terminal to show just the QR Code widget."
    )]
    [DefaultValue(false)]
    public bool ClearTerminal { get; [UsedImplicitly] set; }


    [CommandOption("-e|--ecc")]
    [Description(
        $"""
         Set the error correction level to use. Higher settings increase the size and complexity of the QR code but drastically improve reliability by adding extra blocks to the pattern.
         [DIM]Options: 
             - {nameof(EccFlags.L)} [[1]] (~07%)
             - {nameof(EccFlags.M)} [[2]] (~15%)
             - {nameof(EccFlags.H)} [[3]] (~30%)
             - {nameof(EccFlags.Q)} [[4]] (~25%)[/]
         """
    )]
    [DefaultValue(nameof(EccFlags.H))]
    public EccFlags ErrorCorrection { get; [UsedImplicitly] set; }


    [CommandOption("-f|--foreground-color")]
    [Description(
        "Set the foreground color of the QR code widget in RGB format."
    )]
    [TypeConverter(typeof(HexColorConverter))]
    [DefaultValue("#000000")]
    public Color ForegroundColor { get; [UsedImplicitly] set; }


    [CommandOption("-b|--background-color")]
    [Description(
        "Set the background color of the QR code widget in RGB format."
    )]
    [TypeConverter(typeof(HexColorConverter))]
    [DefaultValue("#FFFFFF")]
    public Color BackgroundColor { get; [UsedImplicitly] set; }


    [CommandOption("-i|--invert")]
    [Description(
        "Convenience flag to swap the background and foreground colours."
    )]
    [DefaultValue(false)]
    public bool InvertColors { get; [UsedImplicitly] set; }


    [CommandOption("--margin-x")]
    [Description(
        "Visual offset from the edge of the window on the x-axis."
    )]
    public int MarginX { get; [UsedImplicitly] set; }


    [CommandOption("--margin-y")]
    [Description(
        "Visual offset from the edge of the window on the y-axis."
    )]
    public int MarginY { get; [UsedImplicitly] set; }


    [CommandOption("-m|--margin")]
    [Description(
        "Visual offset from the edge of the window."
    )]
    [DefaultValue(0)]
    public int Margin { get; [UsedImplicitly] set; }


    [CommandOption("--border")]
    [Description(
        "Size of the whitespace around the QR Code."
    )]
    [DefaultValue(1)]
    public int Border { get; [UsedImplicitly] set; }

#endregion

#endregion

#region Validator

    /// <inheritdoc />
    public override ValidationResult Validate()
        => EncodeCommandValidator.Validate(this);

#endregion
}
