#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.IO;

using Krypton.Toolkit;

namespace MacOSPaletteExporter;

/// <summary>
/// Exports builtin macOS palettes to Documents/Palettes for apps that use KryptonCustomPaletteBase XML.
/// </summary>
internal static class Program
{
    [STAThread]
    private static int Main(string[] args)
    {
        string root = args.Length > 0
            ? args[0]
            : Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", ".."));

        string outDir = Path.Combine(root, "Documents", "Palettes");
        Directory.CreateDirectory(outDir);

        string lightPath = Path.Combine(outDir, "macOS-Light.xml");
        string darkPath = Path.Combine(outDir, "macOS-Dark.xml");

        MacOSCustomPaletteHelper.ExportToFile(PaletteMode.MacOSLight, lightPath);
        MacOSCustomPaletteHelper.ExportToFile(PaletteMode.MacOSDark, darkPath);

        Console.WriteLine(@"Exported:");
        Console.WriteLine(lightPath);
        Console.WriteLine(darkPath);
        return 0;
    }
}
