using System.ComponentModel;
using System.Globalization;
using Spectre.Console;

namespace Linqr.CLI.Converters;

public class HexColorConverter
    : TypeConverter
{
#region Converter

    /// <inheritdoc />
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);


    /// <inheritdoc />
    public override object? ConvertFrom(
        ITypeDescriptorContext? context,
        CultureInfo? culture,
        object value
    ) {
        if (value is not string flagInput) {
            return base.ConvertFrom(context, culture, value);
        }

        var safeInput = GenerateDigitHex(flagInput.Trim());
        if (
            string.IsNullOrWhiteSpace(safeInput) ||
            (safeInput.Length != 6) ||
            !int.TryParse(
                safeInput,
                NumberStyles.HexNumber,
                CultureInfo.InvariantCulture,
                out var hexNumber
            )
        ) {
            throw new FormatException("The color string is malformed.");
        }

        var channels = GenerateTupleRgb(hexNumber);
        return new Color(
            (byte) channels.r,
            (byte) channels.g,
            (byte) channels.b
        );
    }

#endregion

#region Internals

    /// <summary>
    /// Generates a tuple containing the values for the red, green, and blue
    /// channels of the color from its integer representation.
    /// </summary>
    private static (int r, int g, int b) GenerateTupleRgb(
        int colorAsHexadecimal
    ) {
        return (
            r: (colorAsHexadecimal >> 16) & 0xFF,
            g: (colorAsHexadecimal >> 08) & 0xFF,
            b: colorAsHexadecimal & 0xFF
        );
    }


    /// <summary>
    /// Erases the first character of the string to get only the hexadecimal
    /// values if decorators are present.
    /// </summary>
    /// <remarks>
    /// Values should be trimmed before passing.
    /// </remarks>
    private static string GenerateDigitHex(
        string representation
    ) {
        return representation.StartsWith('#')
            ? representation[1..]
            : representation;
    }

#endregion
}
