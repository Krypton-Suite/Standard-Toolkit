#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion
using System;
using System.Collections.Generic;
using Krypton.ThemeGen;
using System.Linq;

internal static class Program
{
    private static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            ShowHelp();
            return 1;
        }
        var cmd = args[0].ToLowerInvariant();
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
                    ShowHelp();
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
        if (!dict.TryGetValue("--file", out var palette) && !dict.TryGetValue("-f", out palette))
        {
            Console.Error.WriteLine("--file <path> is required");
            return 1;
        }
        dict.TryGetValue("--output", out var output);
        dict.TryGetValue("-o", out output);
        var dryRun = dict.ContainsKey("--dry-run");
        SchemeGenerator.Generate(palette, output, embedResx: dict.ContainsKey("--embed-resx"), dryRun: dryRun);
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

    private static void ShowHelp()
    {
        Console.WriteLine("kptheme <command> [options]");
        Console.WriteLine("Commands: genscheme (TODO: list, export, import, fix-images)");
        Console.WriteLine("Example: kptheme genscheme --file PaletteOffice2010Blue.cs --output Generated --dry-run");
    }
}