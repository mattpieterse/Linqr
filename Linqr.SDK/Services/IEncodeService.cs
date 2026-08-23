using Linqr.SDK.Requests;

namespace Linqr.SDK.Services;

public interface IEncodeService
{
#region Contracts

    EncodeResponse Encode(EncodeTextRequest request);

#endregion
}
