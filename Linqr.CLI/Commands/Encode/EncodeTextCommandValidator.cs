using Spectre.Console;

namespace Linqr.CLI.Commands.Encode;

/// <summary>
/// Command settings validator for <see cref="EncodeTextCommandSettings"/>.
/// </summary>
public static class EncodeTextCommandValidator
{
#region Validator

    /// <summary>
    /// Validates the <see cref="EncodeTextCommandSettings"/> object.
    /// </summary>
    /// <returns>
    /// <see cref="ValidationResult"/>
    /// </returns>
    public static ValidationResult Validate(
        EncodeTextCommandSettings settings
    ) {
        var result = ValidateArguments(settings);
        return result.Successful
            ? ValidationResult.Success()
            : result;
    }

#region Validator > Helpers

    private static ValidationResult ValidateArguments(
        EncodeTextCommandSettings settings
    ) {
        const int maxInputCount = 4096;
        var inputs = settings.Inputs
            .Where(text => !string.IsNullOrWhiteSpace(text))
            .ToArray();

        return inputs.Length switch {
            0 => ValidationResult.Error("Text is required and cannot be empty."),
            > maxInputCount => ValidationResult.Error(
                $"A maximum of {maxInputCount} text values can be encoded at once."
            ),
            _ => ValidationResult.Success()
        };
    }

#endregion

#endregion
}
