using System.ComponentModel;
using JetBrains.Annotations;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Linqr.CLI.Commands.Encode;

/// <summary>
/// Command settings for <see cref="EncodeTextCommand"/>.
/// </summary>
/// <remarks>
/// Specialized wrapper for <see cref="EncodeCommandSettings"/> to determine the
/// unique <see cref="CommandArgument"/> for <see cref="EncodeTextCommand"/> and
/// to validate specific requirements before performing base validations. Also
/// allows for appending additional <see cref="CommandOption"/>.
/// </remarks>
/// <seealso cref="EncodeCommandSettings"/>
public sealed class EncodeTextCommandSettings
    : EncodeCommandSettings
{
#region Arguments

    [CommandArgument(0, "<TEXT-ARRAY>")]
    [Description(
        "The text you wish to encode and display as a QR Code."
    )]
    public string[] Inputs { get; [UsedImplicitly] set; } = [];

#endregion

#region Validator

    /// <inheritdoc />
    public override ValidationResult Validate() {
        var argsValidations = EncodeTextCommandValidator.Validate(this);
        return (!argsValidations.Successful)
            ? argsValidations
            : base.Validate();
    }

#endregion
}
