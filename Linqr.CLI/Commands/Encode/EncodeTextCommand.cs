using JetBrains.Annotations;
using Linqr.CLI.Mappers;
using Linqr.CLI.Rendering;
using Linqr.SDK.Models;
using Linqr.SDK.Services;
using Spectre.Console.Cli;

// ReSharper disable ReplaceWithPrimaryConstructorParameter

namespace Linqr.CLI.Commands.Encode;

[UsedImplicitly]
public sealed class EncodeTextCommand(
    IEncodeService encodeService
) : Command<EncodeTextCommandSettings>
{
#region Construct

    private readonly IEncodeService _encodeService = encodeService;

#endregion

#region Inherited

    /// <inheritdoc/>
    protected override int Execute(
        CommandContext context,
        EncodeTextCommandSettings settings,
        CancellationToken cancellationToken
    ) {
        var request = EncodeSettingsMapper.ToValueObject(settings);
        var response = _encodeService.Encode(request);
        EncodeTerminalWriter.Draw(response, settings);

        return (int) ExitCode.Success;
    }

#endregion
}
