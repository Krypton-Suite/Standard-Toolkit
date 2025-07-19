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
        var exitCode = MainInternal(args);
        Console.WriteLine("--- Press ENTER to end the program. ---");
        Console.ReadLine();
        return exitCode;
    }

    private static int MainInternal(string[] args)
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
                case "prosys":
                    return RunProSys(rest);
                case "pro2003":
                    return RunProOffice2003(rest);
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
        string? output = null;
        if (!dict.TryGetValue("--output", out output))
        {
            dict.TryGetValue("-o", out output);
        }

        // Determine palette specification (file or directory)
        string paletteSpec;
        if (dict.TryGetValue("--file", out var fileSpec) || dict.TryGetValue("-f", out fileSpec))
        {
            paletteSpec = fileSpec;
        }
        else if (dict.TryGetValue("--directory", out var dirSpec) || dict.TryGetValue("-d", out dirSpec))
        {
            paletteSpec = dirSpec; // pass directory path directly; SchemeGenerator handles recursion
        }
        else
        {
            Console.Error.WriteLine("No --file or --directory specified. Nothing to do.");
            PrintGenSchemeUsage();
            return 1;
        }

        var dryRun = dict.ContainsKey("--dry-run");
        var printMapping = dict.ContainsKey("--print");
        var oneCtor      = dict.ContainsKey("--ctor1");

        if (printMapping)
        {
            // --print implies dry-run behaviour (no writes)
            dryRun = true;
        }
        var migrate = dict.ContainsKey("--migrate");
        if (dryRun)
        {
            Console.WriteLine("Dry-run mode: no files will be written.");
        }
        var overwrite = dict.ContainsKey("--overwrite");

        SchemeGenerator.Generate(paletteSpec,
                                 output ?? string.Empty,
                                 embedResx: dict.ContainsKey("--embed-resx"),
                                 dryRun: dryRun,
                                 overwrite: overwrite,
                                 migrate: migrate,
                                 printMapping: printMapping,
                                 oneCtor: oneCtor);
        return 0;
    }

    private static int RunProSys(string[] args)
    {
        var dict = ParseArgs(args);

        string? output = null;
        if (!dict.TryGetValue("--output", out output))
        {
            dict.TryGetValue("-o", out output);
        }

        var dryRun   = dict.ContainsKey("--dry-run");
        var overwrite = dict.ContainsKey("--overwrite");

        if (dryRun)
        {
            Console.WriteLine("Dry-run mode: no files will be written.");
        }

        Krypton.ThemeGen.SchemeGenerator.GenerateProfessional(
            output ?? string.Empty,
            dryRun: dryRun,
            overwrite: overwrite);

        return 0;
    }

    private static int RunProOffice2003(string[] args)
    {
        var dict = ParseArgs(args);

        string? output = null;
        if (!dict.TryGetValue("--output", out output))
        {
            dict.TryGetValue("-o", out output);
        }

        var dryRun   = dict.ContainsKey("--dry-run");
        var overwrite = dict.ContainsKey("--overwrite");

        if (dryRun)
        {
            Console.WriteLine("Dry-run mode: no files will be written.");
        }

        Krypton.ThemeGen.SchemeGenerator.GenerateProfessionalOffice2003(
            output ?? string.Empty,
            dryRun: dryRun,
            overwrite: overwrite);

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
        Console.WriteLine("usage: kptheme genscheme [--dry-run] [--migrate] [-o OUTPUT] (-f FILE | -d DIRECTORY) [-r]");
        Console.WriteLine();
        Console.WriteLine("Generate *_BaseScheme.cs classes for Krypton palette files, optionally removing color arrays from the source");
        Console.WriteLine("- Generator will extract whichever of the arrays are present in the source file(s).");
        Console.WriteLine("- Existing files will *not* be overwritten, unless --overwrite flag is used.");
        Console.WriteLine("- With --migrate existing arrays will be removed and residual array index accesses will be converted to property-based access in the source file(s)!");
        Console.WriteLine("- Dry-run does *not* create any files, it'll only list *expected* filenames.");
        Console.WriteLine();
        Console.WriteLine("options:");
        Console.WriteLine("  --dry-run                Preview actions without writing files");
        Console.WriteLine("  --print                  Display mapping table to console; implies --dry-run and disables file writes");
        Console.WriteLine("  --migrate                Remove color arrays and convert remaining _ribbonColors/_trackBarColors index usages to BaseColors properties");
        Console.WriteLine("  --ctor1                  During --migrate: replace old array ctor with single scheme-based ctor (no helper overload)");
        Console.WriteLine("  -o OUTPUT, --output OUTPUT");
        Console.WriteLine("                           Directory to place all generated files instead of alongside palette files");
        Console.WriteLine("  -f FILE, --file FILE     Convert one specific palette .cs file");
        Console.WriteLine("  -d DIRECTORY, --directory DIRECTORY");
        Console.WriteLine("                           Convert palette files under the given directory");
        Console.WriteLine("  -r, --recursive          With -d/--directory, also search sub-directories");
        Console.WriteLine("  --overwrite              Overwrite existing *Scheme.cs files if present");
        Console.WriteLine();
        Console.WriteLine("Example: kptheme genscheme --file PaletteMicrosoft365White.cs --output Generated --dry-run");
    }
}