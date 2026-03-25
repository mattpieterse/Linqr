using Microsoft.Extensions.Hosting;
using Spectre.Console.Cli;

namespace Linqr.CLI.Core.Injection;

public sealed class SpectreTypeResolver(IHost host)
    : ITypeResolver, IDisposable
{
#region Inherited

    /// <inheritdoc cref="IDisposable.Dispose()"/>
    public void Dispose() => host.Dispose();


    /// <inheritdoc cref="ITypeResolver.Resolve(Type?)"/>
    public object? Resolve(Type? type) =>
        (type is not null)
            ? host.Services.GetService(type)
            : null;

#endregion
}
