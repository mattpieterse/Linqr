using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Linqr.CLI.Core.Models;
using Net.Codecrete.QrCodeGenerator;
using Spectre.Console;
using Spectre.Console.Cli;
using ValidationResult = Spectre.Console.ValidationResult;

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
    public string Text { get; [UsedImplicitly] set; } = string.Empty;


#region Arguments > Flags

    [CommandOption("-c|--use-canvas")]
    [Description(
        "Draw the QR code using the improved canvas (may cause artifacts)."
    )]
    public bool UseCanvasWidget { get; [UsedImplicitly] set; } = false;


    [CommandOption("-a|--use-compat")]
    [Description(
        "Draw the QR code using the ASCII-only compatability renderer (default)."
    )]
    public bool UseCompatWidget { get; [UsedImplicitly] set; } = false;


    [CommandOption("-e|--ecc")]
    [Description(
        $"""
         Set the error correction level to use. Higher settings increase the size and complexity of the QR code but drastically improve reliability by adding extra blocks to the pattern.
         [DIM]Options: 
             - {nameof(EccFlags.L)} (~07%)
             - {nameof(EccFlags.M)} (~15%)
             - {nameof(EccFlags.H)} (~30%)
             - {nameof(EccFlags.Q)} (~25%)[/]
         """
    )]
    [DefaultValue(nameof(EccFlags.H))]
    public EccFlags ErrorCorrection { get; [UsedImplicitly] set; }


    [CommandOption("--background-color")]
    [Description(
        "Set the background color of the QR code widget in RGB format."
    )]
    [TypeConverter(typeof(HexColorConverter))]
    [DefaultValue("#FFFFFF")]
    public Color BackgroundColor { get; [UsedImplicitly] set; }


    [CommandOption("--foreground-color")]
    [Description(
        "Set the foreground color of the QR code widget in RGB format."
    )]
    [TypeConverter(typeof(HexColorConverter))]
    [DefaultValue("#000000")]
    public Color ForegroundColor { get; [UsedImplicitly] set; }


    [CommandOption("--padding-x")]
    [Description(
        "Visual offset from the edge of the window on the x-axis."
    )]
    [DefaultValue(1)]
    public int PaddingX { get; [UsedImplicitly] set; }


    [CommandOption("--padding-y")]
    [Description(
        "Visual offset from the edge of the window on the y-axis."
    )]
    [DefaultValue(1)]
    public int PaddingY { get; [UsedImplicitly] set; }


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
    public override ValidationResult Validate() {
        if (string.IsNullOrWhiteSpace(Text))
            return ValidationResult.Error("Link is a required string and cannot be empty.");

        if (UseCanvasWidget && UseCompatWidget)
            return ValidationResult.Error("Cannot use both renderers for a single operation.");

        if (
            (PaddingX is > 20 or < 1) ||
            (PaddingY is > 20 or < 1) ||
            (Border is > 20 or < 1)
        ) {
            return ValidationResult.Error("Integers must be between 1 and 20.");
        }

        return ValidationResult.Success();
    }

#endregion
}
