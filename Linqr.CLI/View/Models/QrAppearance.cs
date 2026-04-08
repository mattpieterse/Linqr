using Spectre.Console;

namespace Linqr.CLI.View.Models;

public readonly record struct QrAppearance(
    Color ForegroundColor,
    Color BackgroundColor,
    int BorderSize
);
