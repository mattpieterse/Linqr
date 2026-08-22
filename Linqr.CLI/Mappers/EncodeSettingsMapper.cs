using Linqr.CLI.Commands.Encode;
using Linqr.SDK.IO.InputSources;
using Linqr.SDK.Requests;
using Linqr.SDK.Requests.Payloads;

namespace Linqr.CLI.Mappers;

/// <summary>
/// Presentation to SDK-layer mapping functions for
/// <see cref="EncodeCommandSettings"/>.
/// </summary>
/// <seealso cref="EncodeTextCommandSettings"/>
/// <seealso cref="EncodeFileCommandSettings"/>
public static class EncodeSettingsMapper
{
#region Functions

    /// <summary>
    /// Converts a presentation <see cref="EncodeTextCommandSettings"/> to an
    /// SDK <see cref="EncodeTextRequest"/> object for cross-layer communication
    /// between presentation and business logic.
    /// </summary>
    /// <remarks>
    /// Overloaded methods are available.
    /// </remarks>
    /// <returns>
    /// <see cref="EncodeTextRequest"/>
    /// </returns>
    public static EncodeTextRequest ToValueObject(
        EncodeTextCommandSettings settings
    ) => new(
        Input: new TextInputSource(settings.Inputs),
        QrCode: new QrCodeSpecification(
            Ecc: QrCodeEnumMapper.ToQrEcc(settings.ErrorCorrection)
        ),
        Output: new OutputSpecification()
    );


    /// <summary>
    /// Converts a presentation <see cref="EncodeFileCommandSettings"/> to an
    /// SDK <see cref="EncodeTextRequest"/> object for cross-layer communication
    /// between presentation and business logic.
    /// </summary>
    /// <remarks>
    /// Overloaded methods are available.
    /// </remarks>
    /// <returns>
    /// <see cref="EncodeTextRequest"/>
    /// </returns>
    public static EncodeTextRequest ToValueObject(
        EncodeFileCommandSettings settings
    ) => new(
        Input: new TextInputSource(settings.Inputs),
        QrCode: new QrCodeSpecification(
            Ecc: QrCodeEnumMapper.ToQrEcc(settings.ErrorCorrection)
        ),
        Output: new OutputSpecification()
    );

#endregion
}
