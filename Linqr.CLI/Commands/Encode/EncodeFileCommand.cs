using JetBrains.Annotations;
using Linqr.SDK.Models;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Linqr.CLI.Commands.Encode;

[UsedImplicitly]
public sealed class EncodeFileCommand
    : Command<EncodeFileCommandSettings>
{
#region Inherited

    /// <inheritdoc/>
    protected override int Execute(
        CommandContext context,
        EncodeFileCommandSettings settings,
        CancellationToken cancellationToken
    ) {
        AnsiConsole.Write("Feature is still in development.");
        return (int) ExitCode.Success;
    }

#endregion
}
