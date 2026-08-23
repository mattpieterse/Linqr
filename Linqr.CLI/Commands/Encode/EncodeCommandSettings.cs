using System.ComponentModel;
using JetBrains.Annotations;
using Linqr.CLI.Converters;
using Linqr.SDK.Models;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Linqr.CLI.Commands.Encode;

/// <summary>
/// Abstract command settings for encoding commands.
/// </summary>
/// <remarks>
/// This <see cref="CommandSettings"/> class is used to define common behavior
/// through concrete <see cref="CommandOption"/> implementations. Derivations
/// must implement their own <see cref="CommandArgument"/> properties and then
/// cascade validations to the base class.
/// </remarks>
/// <seealso cref="EncodeTextCommandSettings"/>
/// <seealso cref="EncodeFileCommandSettings"/>
public abstract class EncodeCommandSettings
    : CommandSettings
{
#region Flags

#region FLags > QR Code

    [CommandOption("-e|--ecc")]
    [Description(
        $"""
         Set the error correction level to use. Higher settings increase the size and complexity of the QR code but drastically improve reliability by adding extra blocks to the pattern.
         [DIM]Options: 
             - {nameof(EcclCode.L)} [[1]] (~07%)
             - {nameof(EcclCode.M)} [[2]] (~15%)
             - {nameof(EcclCode.H)} [[3]] (~30%)
             - {nameof(EcclCode.Q)} [[4]] (~25%)[/]
         """
    )]
    [DefaultValue(nameof(EcclCode.H))]
    public EcclCode ErrorCorrection { get; [UsedImplicitly] set; }

#endregion

#region Flags > Render Specification

    [CommandOption("--clear")]
    [Description(
        "Clear the terminal to show just the QR Code widget."
    )]
    [DefaultValue(false)]
    public bool ClearTerminal { get; [UsedImplicitly] set; }


    [CommandOption("--renderer")]
    [Description(
        $"""
         Select the rendering engine to use to draw your QR Code.
         [DIM]Options: 
             - {nameof(VisualizerFlags.Compat)} [[1]]
             - {nameof(VisualizerFlags.Canvas)} [[2]][/]
         """
    )]
    [DefaultValue(nameof(VisualizerFlags.Compat))]
    public VisualizerFlags TerminalQrCodeRenderer { get; [UsedImplicitly] set; }


    [CommandOption("--foreground-color|--fg")]
    [Description(
        "Set the foreground color of the QR code widget in HEX or RGB format (no alpha)."
    )]
    [TypeConverter(typeof(HexColorConverter))]
    [DefaultValue("#000000")]
    public Color QrCodeForegroundColor { get; [UsedImplicitly] set; }


    [CommandOption("--background-color|--bg")]
    [Description(
        "Set the background color of the QR code widget in HEX or RGB format (no alpha)."
    )]
    [TypeConverter(typeof(HexColorConverter))]
    [DefaultValue("#FFFFFF")]
    public Color QrCodeBackgroundColor { get; [UsedImplicitly] set; }


    [CommandOption("--invert")]
    [Description(
        "Convenience flag to swap the background and foreground colours."
    )]
    [DefaultValue(false)]
    public bool InvertQrCodeColors { get; [UsedImplicitly] set; }


    [CommandOption("--offset-x")]
    [Description(
        "Visual offset from the edge of the window on the x-axis."
    )]
    public int? QrCodeVisualOffsetX { get; [UsedImplicitly] set; }


    [CommandOption("--offset-y")]
    [Description(
        "Visual offset from the edge of the window on the y-axis."
    )]
    public int? QrCodeVisualOffsetY { get; [UsedImplicitly] set; }


    [CommandOption("--offset")]
    [Description(
        "Visual offset from the edge of the window."
    )]
    [DefaultValue(0)]
    public int QrCodeVisualOffset { get; [UsedImplicitly] set; }


    [CommandOption("--border")]
    [Description(
        "Size of the quet-zone whitespace around the QR Code."
    )]
    [DefaultValue(1)]
    public int QrCodeVisualBorder { get; [UsedImplicitly] set; }

#endregion

#region FLags > Output Specification

    [CommandOption("--export")]
    public string ExportFormat { get; [UsedImplicitly] set; } = string.Empty;


    [CommandOption("--to")]
    public string ExportDestinationPath { get; [UsedImplicitly] set; } = string.Empty;


    [CommandOption("--as")]
    public string ExportDestinationSeed { get; [UsedImplicitly] set; } = string.Empty;


    [CommandOption("--no-draw")]
    public bool SuppressTerminalDrawing { get; [UsedImplicitly] set; }


    [CommandOption("--no-logs")]
    public bool SuppressTerminalWriting { get; [UsedImplicitly] set; }


    [CommandOption("--size")]
    public int SquareExportDimensions { get; [UsedImplicitly] set; }


    [CommandOption("--open")]
    public bool OpenInSystemExplorer { get; [UsedImplicitly] set; }

#endregion

#endregion

#region Validator

    /// <inheritdoc />
    /// <remarks>
    /// You must call <c>base.Validate()</c> in derived and base classes to
    /// ensure accurate and consistent behavior. Abstract base command classes
    /// must still cascade the validation of <see cref="CommandSettings"/>.
    /// </remarks>
    public override ValidationResult Validate() {
        var argsValidations = EncodeCommandValidator.Validate(this);
        return (!argsValidations.Successful)
            ? argsValidations
            : base.Validate();
    }

#endregion
}
