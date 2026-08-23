namespace Linqr.SDK.IO.InputSources;

public sealed record TextInputSource(
    IEnumerable<string> Input
) : IInputSource
{
#region Functions

    public IEnumerable<string> Read() => Input;

#endregion
}
