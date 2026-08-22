namespace Linqr.SDK.IO.InputSources;

public sealed record FileInputSource(
    IEnumerable<FileInfo> Input
) : IInputSource
{
#region Functions

    public IEnumerable<string> Read()
        => Input.Select(file => File.ReadAllText(file.FullName));

#endregion
}
