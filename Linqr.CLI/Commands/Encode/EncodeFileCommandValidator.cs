using Spectre.Console;

namespace Linqr.CLI.Commands.Encode;

/// <summary>
/// Command settings validator for <see cref="EncodeFileCommandSettings"/>.
/// </summary>
public static class EncodeFileCommandValidator
{
#region Validator

    /// <summary>
    /// Validates the <see cref="EncodeFileCommandSettings"/> object.
    /// </summary>
    /// <returns>
    /// <see cref="ValidationResult"/>
    /// </returns>
    public static ValidationResult Validate(
        EncodeFileCommandSettings settings
    ) {
        var result = ValidateArguments(settings);
        return result.Successful
            ? ValidationResult.Success()
            : result;
    }

#region Validator > Helpers

    private static ValidationResult ValidateArguments(
        EncodeFileCommandSettings settings
    ) {
        return ValidationResult.Success();
    }

#endregion

#endregion
}
