#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Contains string constants for Windows API library names used in Platform Invoke declarations.
/// These constants provide centralized references to Windows system DLLs and components.
/// </summary>
internal static class Libraries
{
    /// <summary>Common Controls library - provides advanced UI controls</summary>
    public const string Comctl32 = "comctl32.dll";
    /// <summary>Common Dialog library - provides standard dialog boxes</summary>
    public const string Comdlg32 = "comdlg32.dll";
    /// <summary>Desktop Window Manager API - provides window composition and effects</summary>
    public const string DWMApi = @"dwmapi.dll";
    /// <summary>Graphics Device Interface - provides drawing and graphics functions</summary>
    public const string Gdi32 = "gdi32.dll";
    /// <summary>GDI+ library - provides advanced 2D graphics capabilities</summary>
    public const string Gdiplus = "gdiplus.dll";
    /// <summary>HTML Help Control - provides help system functionality</summary>
    public const string Hhctrl = "hhctrl.ocx";
    /// <summary>Input Method Manager - provides input method editor support</summary>
    public const string Imm32 = "imm32.dll";
    /// <summary>Image Resource library - provides access to system image resources</summary>
    public const string Imageres = "imageres.dll";
    /// <summary>Kernel library - provides core system functions</summary>
    public const string Kernel32 = "kernel32.dll";
    /// <summary>Native API library - provides low-level system functions</summary>
    public const string NtDll = "ntdll.dll";
    /// <summary>OLE library - provides object linking and embedding support</summary>
    public const string Ole32 = "ole32.dll";
    /// <summary>OLE Accessibility - provides accessibility support</summary>
    public const string Oleacc = "oleacc.dll";
    /// <summary>OLE Automation - provides automation support</summary>
    public const string Oleaut32 = "oleaut32.dll";
    /// <summary>Power Profile library - provides power management functions</summary>
    public const string Powrprof = "Powrprof.dll";
    /// <summary>Property System library - provides property system support</summary>
    public const string Propsys = "Propsys.dll";
    /// <summary>Rich Edit 4.1 control - provides rich text editing capabilities</summary>
    public const string RichEdit41 = "MsftEdit.DLL";
    /// <summary>Shell Core library - provides shell functionality</summary>
    public const string SHCore = "SHCore.dll";
    /// <summary>Shell library - provides shell and file system functions</summary>
    public const string Shell32 = "shell32.dll";
    /// <summary>Shell Lightweight Utility API - provides shell utility functions</summary>
    public const string Shlwapi = "shlwapi.dll";
    /// <summary>UI Automation Core - provides accessibility and automation support</summary>
    public const string UiaCore = "UIAutomationCore.dll";
    /// <summary>User library - provides window management and user interface functions</summary>
    public const string User32 = "user32.dll";
    /// <summary>Visual Styles library - provides theme and visual style support</summary>
    public const string UxTheme = "uxtheme.dll";
}