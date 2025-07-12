#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion
using System;
using System.Collections.Generic;
using Krypton.ThemeGen;
using System.Linq;
using System.IO;

internal static class Program
{
    private static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            PrintGenSchemeUsage();
            return 1;
        }
        var first = args[0].ToLowerInvariant();
        if (first == "--help" || first == "-h")
        {
            PrintGenSchemeUsage();
            return 0;
        }
        var cmd = first;
        var rest = args.Length > 1 ? args.Skip(1).ToArray() : Array.Empty<string>();
        try
        {
            switch (cmd)
            {
                case "genscheme":
                    return RunGenScheme(rest);
                case "list":
                case "export":
                case "import":
                case "fix-images":
                    Console.Error.WriteLine(cmd + " not implemented yet");
                    return 2;
                default:
                    Console.Error.WriteLine("Unknown command: " + cmd);
                    PrintGenSchemeUsage();
                    return 1;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return 1;
        }
    }

    private static int RunGenScheme(string[] args)
    {
        var dict = ParseArgs(args);
        if (!dict.TryGetValue("--file", out var palette) && !dict.TryGetValue("-f", out palette) &&
            !dict.ContainsKey("--directory") && !dict.ContainsKey("-d"))
        {
            Console.Error.WriteLine("No --file or --directory specified. Nothing to do.");
            PrintGenSchemeUsage();
            return 1;
        }
        string? output = null;
        if (!dict.TryGetValue("--output", out output))
        {
            dict.TryGetValue("-o", out output);
        }
        var dryRun = dict.ContainsKey("--dry-run");
        var overwrite = dict.ContainsKey("--overwrite");
        SchemeGenerator.Generate(palette ?? string.Empty, output ?? string.Empty, embedResx: dict.ContainsKey("--embed-resx"), dryRun: dryRun, overwrite: overwrite);
        return 0;
    }

    private static Dictionary<string, string> ParseArgs(string[] args)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        for (var i = 0; i < args.Length; i++)
        {
            var arg = args[i];
            if (arg.StartsWith("-"))
            {
                if (i + 1 < args.Length && !args[i + 1].StartsWith("-"))
                {
                    dict[arg] = args[++i];
                }
                else
                {
                    dict[arg] = string.Empty;
                }
            }
        }
        return dict;
    }

    private static void PrintGenSchemeUsage()
    {
        Console.WriteLine();
        Console.WriteLine("usage: kptheme genscheme [--dry-run] [-o OUTPUT] (-f FILE | -d DIRECTORY) [-r] [--embed-resx]");
        Console.WriteLine();
        Console.WriteLine("Generate *Scheme.cs classes for Krypton palettes");
        Console.WriteLine();
        Console.WriteLine("options:");
        Console.WriteLine("  --dry-run                Preview actions without writing files");
        Console.WriteLine("  -o OUTPUT, --output OUTPUT");
        Console.WriteLine("                           Directory to place all generated files instead of alongside palette files");
        Console.WriteLine("  -f FILE, --file FILE     Convert one specific palette .cs file");
        Console.WriteLine("  -d DIRECTORY, --directory DIRECTORY");
        Console.WriteLine("                           Convert palette files under the given directory");
        Console.WriteLine("  -r, --recursive          With -d/--directory, also search sub-directories");
        Console.WriteLine("  --embed-resx             Embed generated resources (*.resx) next to scheme");
        Console.WriteLine("  --overwrite              Overwrite existing *Scheme.cs files if present");
        Console.WriteLine();
        Console.WriteLine("Example: kptheme genscheme --file PaletteOffice2010Blue.cs --output Generated --dry-run");
    }
}