namespace Linqr.SDK.IO.InputSources;

public interface IInputSource
{
#region Contracts

    IEnumerable<string> Read();

#endregion
}
