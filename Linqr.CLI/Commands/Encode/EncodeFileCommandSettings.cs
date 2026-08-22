using System.ComponentModel;
using JetBrains.Annotations;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Linqr.CLI.Commands.Encode;

/// <summary>
/// Command settings for <see cref="EncodeFileCommand"/>.
/// </summary>
/// <remarks>
/// Specialized wrapper for <see cref="EncodeCommandSettings"/> to determine the
/// unique <see cref="CommandArgument"/> for <see cref="EncodeFileCommand"/> and
/// to validate specific requirements before performing base validations. Also
/// allows for appending additional <see cref="CommandOption"/>.
/// </remarks>
/// <seealso cref="EncodeCommandSettings"/>
public sealed class EncodeFileCommandSettings
    : EncodeCommandSettings
{
#region Arguments

    [CommandArgument(0, "<PATH-ARRAY>")]
    [Description(
        """
        The file you wish to encode and display as a QR Code.
        You can specify one or more filepaths by their absolute locations on your machine to have their contents parsed and encoded.
        """
    )]
    public string[] Inputs { get; [UsedImplicitly] set; } = [];

#endregion

#region Validator

    /// <inheritdoc />
    public override ValidationResult Validate() {
        var argsValidations = EncodeFileCommandValidator.Validate(this);
        return (!argsValidations.Successful)
            ? argsValidations
            : base.Validate();
    }

#endregion
}
