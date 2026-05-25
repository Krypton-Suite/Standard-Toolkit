#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

using Krypton.Toolkit;

namespace ButtonSpecDpiAssetGenerator;

/// <summary>
/// Generates @2x/@3x ButtonSpec BMP assets and a companion .resx for Issue #978.
/// </summary>
internal static class Program
{
    private const int ExitOk = 0;
    private const int ExitError = 1;

    private static readonly (string FileBase, PaletteButtonSpecStyle Style)[] SilverEntries =
    {
        ("ProfessionalCloseButton", PaletteButtonSpecStyle.Close),
        ("ProfessionalContextButton", PaletteButtonSpecStyle.Context),
        ("ProfessionalNextButton", PaletteButtonSpecStyle.Next),
        ("ProfessionalPreviousButton", PaletteButtonSpecStyle.Previous),
        ("ProfessionalArrowLeftButton", PaletteButtonSpecStyle.ArrowLeft),
        ("ProfessionalArrowRightButton", PaletteButtonSpecStyle.ArrowRight),
        ("ProfessionalArrowUpButton", PaletteButtonSpecStyle.ArrowUp),
        ("ProfessionalArrowDownButton", PaletteButtonSpecStyle.ArrowDown),
        ("ProfessionalDropDownButton", PaletteButtonSpecStyle.DropDown),
        ("ProfessionalRestore", PaletteButtonSpecStyle.WorkspaceRestore),
        ("ProfessionalPinVerticalButton", PaletteButtonSpecStyle.PinVertical),
        ("ProfessionalPinHorizontalButton", PaletteButtonSpecStyle.PinHorizontal),
        ("ProfessionalMaximize", PaletteButtonSpecStyle.WorkspaceMaximize),
        ("Office2010ButtonMDIClose", PaletteButtonSpecStyle.PendantClose),
        ("Office2010ButtonMDIMin", PaletteButtonSpecStyle.PendantMin),
        ("Office2010ButtonMDIRestore", PaletteButtonSpecStyle.PendantRestore),
        ("RibbonUp2010", PaletteButtonSpecStyle.RibbonMinimize),
        ("RibbonDown2010", PaletteButtonSpecStyle.RibbonExpand),
    };

    private static readonly (string FileBase, PaletteButtonSpecStyle Style)[] BlackEntries =
    {
        ("Office2010ButtonMDICloseBlack", PaletteButtonSpecStyle.PendantClose),
        ("Office2010ButtonMDIMinBlack", PaletteButtonSpecStyle.PendantMin),
        ("Office2010ButtonMDIRestoreBlack", PaletteButtonSpecStyle.PendantRestore),
        ("RibbonUp2010Black", PaletteButtonSpecStyle.RibbonMinimize),
        ("RibbonDown2010Black", PaletteButtonSpecStyle.RibbonExpand),
    };

    private static readonly (string FileBase, PaletteButtonSpecStyle Style)[] Office2007MdiEntries =
    {
        ("MdiClose", PaletteButtonSpecStyle.PendantClose),
        ("MdiMin", PaletteButtonSpecStyle.PendantMin),
        ("MdiRestore", PaletteButtonSpecStyle.PendantRestore),
        ("MdiRibbonMinimize", PaletteButtonSpecStyle.RibbonMinimize),
        ("MdiRibbonExpand", PaletteButtonSpecStyle.RibbonExpand),
    };

    private static readonly (string FileBase, PaletteButtonSpecStyle Style)[] ToolbarOffice2010Entries =
    {
        ("Office2010ToolbarNewNormal", PaletteButtonSpecStyle.New),
        ("Office2010ToolbarOpenNormal", PaletteButtonSpecStyle.Open),
        ("Office2010ToolbarSaveNormal", PaletteButtonSpecStyle.Save),
        ("Office2010ToolbarSaveAsNormal", PaletteButtonSpecStyle.SaveAs),
        ("Office2010ToolbarSaveAllNormal", PaletteButtonSpecStyle.SaveAll),
        ("Office2010ToolbarCutNormal", PaletteButtonSpecStyle.Cut),
        ("Office2010ToolbarCopyNormal", PaletteButtonSpecStyle.Copy),
        ("Office2010ToolbarPasteNormal", PaletteButtonSpecStyle.Paste),
        ("Office2010ToolbarUndoNormal", PaletteButtonSpecStyle.Undo),
        ("Office2010ToolbarRedoNormal", PaletteButtonSpecStyle.Redo),
        ("Office2010ToolbarPageSetupNormal", PaletteButtonSpecStyle.PageSetup),
        ("Office2010ToolbarPrintPreviewNormal", PaletteButtonSpecStyle.PrintPreview),
        ("Office2010ToolbarPrintNormal", PaletteButtonSpecStyle.Print),
        ("Office2010ToolbarQuickPrintNormal", PaletteButtonSpecStyle.QuickPrint),
    };

    private static readonly (string FileBase, PaletteButtonSpecStyle Style)[] ToolbarOffice2019Entries =
    {
        ("Office2019ToolbarNewNormal", PaletteButtonSpecStyle.New),
        ("Office2019ToolbarOpenNormal", PaletteButtonSpecStyle.Open),
        ("Office2019ToolbarSaveNormal", PaletteButtonSpecStyle.Save),
        ("Office2019ToolbarSaveAsNormal", PaletteButtonSpecStyle.SaveAs),
        ("Office2019ToolbarSaveAllNormal", PaletteButtonSpecStyle.SaveAll),
        ("Office2019ToolbarCutNormal", PaletteButtonSpecStyle.Cut),
        ("Office2019ToolbarCopyNormal", PaletteButtonSpecStyle.Copy),
        ("Office2019ToolbarPasteNormal", PaletteButtonSpecStyle.Paste),
        ("Office2019ToolbarUndoNormal", PaletteButtonSpecStyle.Undo),
        ("Office2019ToolbarRedoNormal", PaletteButtonSpecStyle.Redo),
        ("Office2019ToolbarPageSetupNormal", PaletteButtonSpecStyle.PageSetup),
        ("Office2019ToolbarPrintPreviewNormal", PaletteButtonSpecStyle.PrintPreview),
        ("Office2019ToolbarPrintNormal", PaletteButtonSpecStyle.Print),
        ("Office2019ToolbarQuickPrintNormal", PaletteButtonSpecStyle.QuickPrint),
    };

    private static int Main(string[] args)
    {
        try
        {
            string outputDir = args.Length > 0
                ? args[0]
                : Path.Combine(GetToolkitRoot(), "Resources", "ButtonSpecs");

            Directory.CreateDirectory(outputDir);

            var resxNames = new List<string>();

            using (var palette = new PaletteOffice2010Silver())
            {
                GenerateAssetSets(outputDir, palette, SilverEntries, resxNames);
            }

            using (var palette = new PaletteOffice2010Black())
            {
                GenerateAssetSets(outputDir, palette, BlackEntries, resxNames);
            }

            using (var palette = new PaletteOffice2007Silver())
            {
                GenerateAssetSets(outputDir, palette, Office2007MdiEntries, resxNames);
            }

            using (var palette = new PaletteOffice2010Silver())
            {
                GenerateAssetSets(outputDir, palette, ToolbarOffice2010Entries, resxNames);
            }

            using (var palette = new PaletteMicrosoft365Silver())
            {
                GenerateAssetSets(outputDir, palette, ToolbarOffice2019Entries, resxNames);
            }

            string resxPath = Path.Combine(GetToolkitRoot(), "ResourceFiles", "ButtonSpecs",
                "ButtonSpecDpiImageResources.resx");
            WriteResx(resxPath, resxNames);
            string designerPath = Path.Combine(GetToolkitRoot(), "ResourceFiles", "ButtonSpecs",
                "ButtonSpecDpiImageResources.Designer.cs");
            WriteDesigner(designerPath, resxNames);
            Console.WriteLine(@"Generated {0} ButtonSpec DPI asset sets under:", resxNames.Count);
            Console.WriteLine(outputDir);
            Console.WriteLine(@"Updated {0}", resxPath);
            Console.WriteLine(@"Updated {0}", designerPath);
            return ExitOk;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
            return ExitError;
        }
    }

    private static void GenerateAssetSets(string outputDir, PaletteBase palette,
        (string FileBase, PaletteButtonSpecStyle Style)[] entries, List<string> resxNames)
    {
        foreach ((string fileBase, PaletteButtonSpecStyle style) in entries)
        {
            using Image? baseline = palette.GetButtonSpecImage(style, PaletteState.Normal);
            if (baseline == null)
            {
                Console.WriteLine(@"Skipping {0} (no baseline image).", fileBase);
                continue;
            }

            string baselinePath = Path.Combine(outputDir, fileBase + ".bmp");
            if (!File.Exists(baselinePath))
            {
                baseline.Save(baselinePath, ImageFormat.Bmp);
            }

            SaveScaled(baseline, 2f, Path.Combine(outputDir, fileBase + "_2x.bmp"));
            SaveScaled(baseline, 3f, Path.Combine(outputDir, fileBase + "_3x.bmp"));
            resxNames.Add(fileBase);
        }
    }

    private static string GetToolkitRoot() =>
        Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
            "..", "..", "..", "..", "..", "..",
            "Krypton Components", "Krypton.Toolkit"));

    private static void SaveScaled(Image baseline, float multiplier, string path)
    {
        float w = baseline.Width * multiplier;
        float h = baseline.Height * multiplier;
        using Bitmap? scaled = CommonHelper.ScaleImageForSizedDisplay(baseline, w, h, avoidPurple: true);
        if (scaled == null)
        {
            throw new InvalidOperationException(@"Failed to scale image for " + path);
        }

        scaled.Save(path, ImageFormat.Bmp);
    }

    private static void WriteResx(string resxPath, List<string> fileBases)
    {
        var sb = new StringBuilder();
        sb.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
        sb.AppendLine(@"<root>");
        sb.AppendLine(@"  <resheader name=""resmimetype"">");
        sb.AppendLine(@"    <value>text/microsoft-resx</value>");
        sb.AppendLine(@"  </resheader>");
        sb.AppendLine(@"  <resheader name=""version"">");
        sb.AppendLine(@"    <value>2.0</value>");
        sb.AppendLine(@"  </resheader>");
        sb.AppendLine(@"  <resheader name=""reader"">");
        sb.AppendLine(@"    <value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>");
        sb.AppendLine(@"  </resheader>");
        sb.AppendLine(@"  <resheader name=""writer"">");
        sb.AppendLine(@"    <value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>");
        sb.AppendLine(@"  </resheader>");
        sb.AppendLine(@"  <assembly alias=""System.Windows.Forms"" name=""System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"" />");

        foreach (string fileBase in fileBases)
        {
            AppendResxEntry(sb, fileBase + "_2x", fileBase + "_2x.bmp");
            AppendResxEntry(sb, fileBase + "_3x", fileBase + "_3x.bmp");
        }

        sb.AppendLine(@"</root>");
        File.WriteAllText(resxPath, sb.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
    }

    private static void AppendResxEntry(StringBuilder sb, string resourceName, string fileName)
    {
        sb.AppendLine($@"  <data name=""{resourceName}"" type=""System.Resources.ResXFileRef, System.Windows.Forms"">");
        sb.AppendLine($@"    <value>..\..\Resources\ButtonSpecs\{fileName};System.Drawing.Bitmap, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</value>");
        sb.AppendLine(@"  </data>");
    }

    private static void WriteDesigner(string designerPath, List<string> fileBases)
    {
        var sb = new StringBuilder();
        sb.AppendLine(@"//------------------------------------------------------------------------------");
        sb.AppendLine(@"// <auto-generated>");
        sb.AppendLine(@"//     This code was generated by ButtonSpecDpiAssetGenerator (Issue #978).");
        sb.AppendLine(@"// </auto-generated>");
        sb.AppendLine(@"//------------------------------------------------------------------------------");
        sb.AppendLine();
        sb.AppendLine(@"namespace Krypton.Toolkit.ResourceFiles.ButtonSpecs {");
        sb.AppendLine(@"    using System;");
        sb.AppendLine();
        sb.AppendLine(@"    [global::System.CodeDom.Compiler.GeneratedCodeAttribute(""ButtonSpecDpiAssetGenerator"", ""1.0.0.0"")]");
        sb.AppendLine(@"    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]");
        sb.AppendLine(@"    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]");
        sb.AppendLine(@"    internal class ButtonSpecDpiImageResources {");
        sb.AppendLine();
        sb.AppendLine(@"        private static global::System.Resources.ResourceManager resourceMan;");
        sb.AppendLine();
        sb.AppendLine(@"        private static global::System.Globalization.CultureInfo resourceCulture;");
        sb.AppendLine();
        sb.AppendLine(@"        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute(""Microsoft.Performance"", ""CA1811:AvoidUncalledPrivateCode"")]");
        sb.AppendLine(@"        internal ButtonSpecDpiImageResources() {");
        sb.AppendLine(@"        }");
        sb.AppendLine();
        sb.AppendLine(@"        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]");
        sb.AppendLine(@"        internal static global::System.Resources.ResourceManager ResourceManager {");
        sb.AppendLine(@"            get {");
        sb.AppendLine(@"                if (object.ReferenceEquals(resourceMan, null)) {");
        sb.AppendLine(@"                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager(""Krypton.Toolkit.ResourceFiles.ButtonSpecs.ButtonSpecDpiImageResources"", typeof(ButtonSpecDpiImageResources).Assembly);");
        sb.AppendLine(@"                    resourceMan = temp;");
        sb.AppendLine(@"                }");
        sb.AppendLine(@"                return resourceMan;");
        sb.AppendLine(@"            }");
        sb.AppendLine(@"        }");
        sb.AppendLine();
        sb.AppendLine(@"        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]");
        sb.AppendLine(@"        internal static global::System.Globalization.CultureInfo Culture {");
        sb.AppendLine(@"            get {");
        sb.AppendLine(@"                return resourceCulture;");
        sb.AppendLine(@"            }");
        sb.AppendLine(@"            set {");
        sb.AppendLine(@"                resourceCulture = value;");
        sb.AppendLine(@"            }");
        sb.AppendLine(@"        }");

        foreach (string fileBase in fileBases)
        {
            AppendDesignerProperty(sb, fileBase + "_2x");
            AppendDesignerProperty(sb, fileBase + "_3x");
        }

        sb.AppendLine(@"    }");
        sb.AppendLine(@"}");
        File.WriteAllText(designerPath, sb.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
    }

    private static void AppendDesignerProperty(StringBuilder sb, string resourceName)
    {
        sb.AppendLine();
        sb.AppendLine(@"        /// <summary>");
        sb.AppendLine(@"        ///   Looks up a localized resource of type System.Drawing.Bitmap.");
        sb.AppendLine(@"        /// </summary>");
        sb.AppendLine($@"        internal static System.Drawing.Bitmap {resourceName} {{");
        sb.AppendLine(@"            get {");
        sb.AppendLine($@"                object obj = ResourceManager.GetObject(""{resourceName}"", resourceCulture);");
        sb.AppendLine(@"                return ((System.Drawing.Bitmap)(obj));");
        sb.AppendLine(@"            }");
        sb.AppendLine(@"        }");
    }
}

