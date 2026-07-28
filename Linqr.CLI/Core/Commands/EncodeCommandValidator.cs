using Linqr.CLI.Core.Helpers;
using Linqr.CLI.Core.Models;
using Net.Codecrete.QrCodeGenerator;
using Spectre.Console;

namespace Linqr.CLI.Core.Commands;

internal static class EncodeCommandValidator
{
#region Validator

    public static ValidationResult Validate(
        EncodeCommandSettings settings
    ) {
        var textValidation = ValidateText(settings);
        if (!textValidation.Successful) {
            return textValidation;
        }

        var enumValidation = ValidateEnums(settings);
        if (!enumValidation.Successful) {
            return enumValidation;
        }

        var layoutValidation = ValidateLayout(settings);
        if (!layoutValidation.Successful) {
            return layoutValidation;
        }

        var colorValidation = ValidateColors(settings);
        if (!colorValidation.Successful) {
            return colorValidation;
        }

        return ValidationResult.Success();
    }


#region Validator > Helpers

    private static ValidationResult ValidateText(
        EncodeCommandSettings settings
    ) {
        const int maxInputCount = 4096;

        var inputs = settings.Text
            .Where(text => !string.IsNullOrWhiteSpace(text))
            .ToArray();

        switch (inputs.Length) {
        case 0:
            return ValidationResult.Error("Text is required and cannot be empty.");
        case > maxInputCount:
            return ValidationResult.Error($"A maximum of {maxInputCount} text values can be encoded at once.");
        }

        foreach (var input in inputs) {
            var qrValidation = ValidateQrPayload(input, settings.ErrorCorrection);
            if (!qrValidation.Successful) {
                return qrValidation;
            }
        }

        return ValidationResult.Success();
    }


    private static ValidationResult ValidateQrPayload(string input, EccFlags errorCorrection) {
        try {
            _ = QrCode.EncodeText(input, QrCodeOptionsMapper.ToQrEcc(errorCorrection));
            return ValidationResult.Success();
        }
        catch (ArgumentException) {
            return ValidationResult.Error(
                "The provided text is too large to encode as a QR code with the selected error correction level."
            );
        }
    }


    private static ValidationResult ValidateEnums(
        EncodeCommandSettings settings
    ) {
        if (!Enum.IsDefined(settings.Visualizer)) {
            return ValidationResult.Error(
                $"--visualizer must be one of: {string.Join(", ", Enum.GetNames<VisualizerFlags>())}."
            );
        }

        if (!Enum.IsDefined(settings.ErrorCorrection)) {
            return ValidationResult.Error(
                $"--ecc must be one of: {string.Join(", ", Enum.GetNames<EccFlags>())}."
            );
        }

        return ValidationResult.Success();
    }


    private static ValidationResult ValidateLayout(
        EncodeCommandSettings settings
    ) {
        const int minBorder = 01;
        const int maxBorder = 20;
        const int minMargin = 00;
        const int maxMargin = 20;

        if (settings.Border is < minBorder or > maxBorder) {
            return ValidationResult.Error($"--border must be between {minBorder} and {maxBorder}.");
        }

        if (settings.Margin is < minMargin or > maxMargin) {
            return ValidationResult.Error($"--margin must be between {minMargin} and {maxMargin}.");
        }

        if (settings.MarginX is < minMargin or > maxMargin) {
            return ValidationResult.Error($"--margin-x must be between {minMargin} and {maxMargin}.");
        }

        if (settings.MarginY is < minMargin or > maxMargin) {
            return ValidationResult.Error($"--margin-y must be between {minMargin} and {maxMargin}.");
        }

        return ValidationResult.Success();
    }


    private static ValidationResult ValidateColors(
        EncodeCommandSettings settings
    ) {
        var foreground = settings.InvertColors
            ? settings.BackgroundColor
            : settings.ForegroundColor;

        var background = settings.InvertColors
            ? settings.ForegroundColor
            : settings.BackgroundColor;

        if (foreground == background) {
            return ValidationResult.Error(
                "Foreground and background colors must be different after applying --invert."
            );
        }

        return ValidationResult.Success();
    }

#endregion

#endregion
}
