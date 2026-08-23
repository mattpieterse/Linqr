using Spectre.Console;
using Wacton.Unicolour;

namespace Linqr.CLI.Commands.Encode;

/// <summary>
/// Command settings validator for <see cref="EncodeCommandSettings"/>.
/// </summary>
public static class EncodeCommandValidator
{
#region Constants

    /// <summary>
    /// The minimum border and offset values for the QR Code edges.
    /// </summary>
    private const int MinimumQrEdgeSize = 00;


    /// <summary>
    /// The maximum border and offset values for the QR Code edges.
    /// </summary>
    private const int MaximumQrEdgeSize = 10;


    /// <summary>
    /// Determines the minimum delta-E (ΔE) value required for the foreground
    /// and background colors to be considered visible. Using CIEDE2000, a value
    /// of 1-2 is considered barely perceptible but reasonably visible.
    /// </summary>
    private const double MinimumDeltaEValue = 2;

#endregion

#region Validator

    /// <summary>
    /// Validates the <see cref="EncodeCommandSettings"/> object.
    /// </summary>
    /// <returns>
    /// <see cref="ValidationResult"/>
    /// </returns>
    public static ValidationResult Validate(
        EncodeCommandSettings settings
    ) {
        var internalValidators = new[] {
            ValidateAppearanceOfColor(settings),
            ValidateAppearanceOfEdges(settings)
        };

        return internalValidators.FirstOrDefault(validator => !(validator.Successful))
               ?? ValidationResult.Success();
    }

#region Validator > Helpers

    /// <summary>
    /// Validates appearance-based command options.
    /// </summary>
    /// <returns>
    /// <see cref="ValidationResult"/>
    /// </returns>
    /// <seealso cref="EncodeCommandSettings.QrCodeForegroundColor"/>
    /// <seealso cref="EncodeCommandSettings.QrCodeBackgroundColor"/>
    private static ValidationResult ValidateAppearanceOfColor(
        EncodeCommandSettings settings
    ) {
        var unicolorQrForeground = ConvertSpectreColorToUnicolour(settings.QrCodeForegroundColor);
        var unicolorQrBackground = ConvertSpectreColorToUnicolour(settings.QrCodeBackgroundColor);
        if (!AreColorsVisibleCiede2000(unicolorQrForeground, unicolorQrBackground)) {
            return ValidationResult.Error(
                "The foreground and background colors are not contrasted enough to be visible."
            );
        }

        return ValidationResult.Success();
    }


    /// <summary>
    /// Validates appearance-based command options.
    /// </summary>
    /// <returns>
    /// <see cref="ValidationResult"/>
    /// </returns>
    /// <seealso cref="EncodeCommandSettings.QrCodeVisualBorder"/>
    /// <seealso cref="EncodeCommandSettings.QrCodeVisualOffset"/>
    /// <seealso cref="EncodeCommandSettings.QrCodeVisualOffsetX"/>
    /// <seealso cref="EncodeCommandSettings.QrCodeVisualOffsetY"/>
    private static ValidationResult ValidateAppearanceOfEdges(
        EncodeCommandSettings settings
    ) {
        if (
            new[] {
                settings.QrCodeVisualBorder,
                settings.QrCodeVisualOffset,
                settings.QrCodeVisualOffsetX,
                settings.QrCodeVisualOffsetY
            }.Any(edgeSize => edgeSize is < MinimumQrEdgeSize or > MaximumQrEdgeSize)
        ) {
            return ValidationResult.Error(
                $"Border and offset values must be integers in the reasonable range of ({MinimumQrEdgeSize}-{MaximumQrEdgeSize})."
            );
        }

        return ValidationResult.Success();
    }

#endregion

#endregion

#region Internals

    /// <summary>
    /// Determines whether the foreground and background colors are visible to a
    /// reasonably able-sighted person. This is based on the CIEDE2000 algorithm
    /// and is not intended for accessibility but rather to prevent the
    /// generation of objectively unreadable QR Codes.
    /// </summary>
    /// <remarks>
    /// Colors inverted after the contrast check will still pass CIEDE2000.
    /// </remarks>
    /// <returns>
    /// <see langword="bool"/>
    /// </returns>
    private static bool AreColorsVisibleCiede2000(
        Unicolour foreground,
        Unicolour background
    ) {
        var deltaE = foreground.Difference(background, DeltaE.Ciede2000);
        return (deltaE >= MinimumDeltaEValue);
    }


    /// <summary>
    /// Converts a Spectre <see cref="Color"/> to a <see cref="Unicolour"/>.
    /// </summary>
    /// <returns>
    /// <see cref="Unicolour"/>
    /// </returns>
    private static Unicolour ConvertSpectreColorToUnicolour(
        Color spectreColor
    ) {
        return new Unicolour(
            ColourSpace.Rgb,
            (
                (spectreColor.R / 255.0),
                (spectreColor.G / 255.0),
                (spectreColor.B / 255.0)
            )
        );
    }

#endregion
}
