using Spectre.Console;

namespace Linqr.CLI.Rendering.Models;

public readonly record struct QrAppearance(
    Color ForegroundColor,
    Color BackgroundColor,
    int BorderSize
);
