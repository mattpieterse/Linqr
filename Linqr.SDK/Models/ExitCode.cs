namespace Linqr.SDK.Models;

public enum ExitCode
{
    Success = 0,
    Failure = 1,

    /// <summary>
    /// Indicates an unexpected <see cref="ExitCode.Failure"/>.
    /// </summary>
    Unknown = 2
}
