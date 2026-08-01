#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Keeps ToolStrip-family control fonts aligned with the active palette ColorTable.
/// </summary>
internal sealed class ToolStripFontSyncHelper : IDisposable
{
    #region Instance Fields
    private readonly ToolStrip _toolStrip;
    private readonly ToolStripFontKind _fontKind;
    private readonly Func<PaletteBase?> _getPalette;
    private PaletteBase? _hookedPalette;
    private PaletteBase? _hookedGlobalPalette;
    private bool _disposed;
    #endregion

    #region Identity
    internal ToolStripFontSyncHelper(ToolStrip toolStrip, ToolStripFontKind fontKind, Func<PaletteBase?>? getPalette = null)
    {
        _toolStrip = toolStrip;
        _fontKind = fontKind;
        _getPalette = getPalette ?? (() => KryptonManager.CurrentGlobalPalette);

        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
        HookGlobalPaletteEvents();
        HookPaletteEvents();
        UpdateFont();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;

        if (_hookedPalette != null)
        {
            _hookedPalette.PalettePaintInternal -= OnPalettePaint;
        }

        if (_hookedGlobalPalette != null)
        {
            _hookedGlobalPalette.PalettePaintInternal -= OnGlobalPalettePaint;
        }

        _disposed = true;
    }
    #endregion

    #region Public
    internal void UpdateFont() => ToolStripFontSync.Apply(_toolStrip, _getPalette(), _fontKind);

    internal void OnPaletteSourceChanged(bool paletteModeIsGlobal)
    {
        HookGlobalPaletteEvents();

        if (paletteModeIsGlobal)
        {
            HookPaletteEvents();
        }

        UpdateFont();
    }
    #endregion

    #region Implementation
    private void HookPaletteEvents()
    {
        var currentPalette = _getPalette();

        if (_hookedPalette != currentPalette)
        {
            if (_hookedPalette != null)
            {
                _hookedPalette.PalettePaintInternal -= OnPalettePaint;
            }

            if (currentPalette != null)
            {
                currentPalette.PalettePaintInternal += OnPalettePaint;
            }

            _hookedPalette = currentPalette;
        }
    }

    private void HookGlobalPaletteEvents()
    {
        var currentGlobalPalette = KryptonManager.CurrentGlobalPalette;

        if (_hookedGlobalPalette != currentGlobalPalette)
        {
            if (_hookedGlobalPalette != null)
            {
                _hookedGlobalPalette.PalettePaintInternal -= OnGlobalPalettePaint;
            }

            if (currentGlobalPalette != null)
            {
                currentGlobalPalette.PalettePaintInternal += OnGlobalPalettePaint;
            }

            _hookedGlobalPalette = currentGlobalPalette;
        }
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        if (_disposed || _toolStrip.IsDisposed)
        {
            return;
        }

        HookGlobalPaletteEvents();
        HookPaletteEvents();
        UpdateFont();
    }

    private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e)
    {
        if (!_disposed && !_toolStrip.IsDisposed)
        {
            UpdateFont();
        }
    }

    private void OnGlobalPalettePaint(object? sender, PaletteLayoutEventArgs e)
    {
        if (!_disposed && !_toolStrip.IsDisposed)
        {
            UpdateFont();
        }
    }
    #endregion
}

/// <summary>
/// Identifies which ColorTable font property applies to a ToolStrip control.
/// </summary>
internal enum ToolStripFontKind
{
    MenuStrip,
    ToolStrip,
    StatusStrip
}

/// <summary>
/// Shared helpers for resolving and applying strip fonts from a palette ColorTable.
/// </summary>
internal static class ToolStripFontSync
{
    internal static void Apply(ToolStrip toolStrip, PaletteBase? palette, ToolStripFontKind fontKind)
    {
        if (toolStrip.IsDisposed)
        {
            return;
        }

        try
        {
            palette ??= KryptonManager.CurrentGlobalPalette;
            if (palette == null)
            {
                return;
            }

            var colorTable = palette.ColorTable;
            var font = ResolveFont(colorTable, fontKind);
            if (font != null && toolStrip.Font != font)
            {
                toolStrip.Font = font;
                RefreshToolStripVisual(toolStrip);
            }
        }
        catch
        {
            // ColorTable may not be initialized yet during designer load.
        }
    }

    internal static void RefreshAllOpenForms()
    {
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
        {
            return;
        }

        try
        {
            var palette = KryptonManager.CurrentGlobalPalette;
            if (palette == null)
            {
                return;
            }

            var colorTable = palette.ColorTable;
            var openForms = Application.OpenForms;
            for (var i = 0; i < openForms.Count; i++)
            {
                if (openForms[i] is Form form)
                {
                    RefreshToolStripsInControl(form, colorTable);
                }
            }
        }
        catch
        {
            // Application may not be initialized yet during startup.
        }
    }

    private static void RefreshToolStripsInControl(Control? control, KryptonColorTable colorTable)
    {
        if (control == null || control.IsDisposed)
        {
            return;
        }

        if (control is ToolStrip toolStrip)
        {
            ApplyFromColorTable(toolStrip, colorTable);
        }

        if (control.ContextMenuStrip != null)
        {
            ApplyFromColorTable(control.ContextMenuStrip, colorTable);
        }

        foreach (Control child in control.Controls)
        {
            RefreshToolStripsInControl(child, colorTable);
        }
    }

    private static void RefreshToolStripVisual(ToolStrip toolStrip)
    {
        if (toolStrip.IsDisposed)
        {
            return;
        }

        toolStrip.SuspendLayout();
        try
        {
            RefreshDropDowns(toolStrip.Items);
            toolStrip.PerformLayout();
        }
        finally
        {
            toolStrip.ResumeLayout(true);
        }

        toolStrip.Invalidate(true);
    }

    private static void RefreshDropDowns(ToolStripItemCollection items)
    {
        foreach (ToolStripItem item in items)
        {
            if (item is ToolStripDropDownItem dropDownItem && dropDownItem.DropDown != null)
            {
                var palette = KryptonManager.CurrentGlobalPalette;
                if (palette != null)
                {
                    ApplyFromColorTable(dropDownItem.DropDown, palette.ColorTable);
                }

                RefreshDropDowns(dropDownItem.DropDown.Items);
            }
        }
    }

    internal static void ApplyFromColorTable(ToolStrip toolStrip, KryptonColorTable colorTable)
    {
        if (toolStrip.IsDisposed)
        {
            return;
        }

        var fontKind = toolStrip switch
        {
            MenuStrip or ContextMenuStrip or ToolStripDropDown => ToolStripFontKind.MenuStrip,
            StatusStrip => ToolStripFontKind.StatusStrip,
            ToolStrip => ToolStripFontKind.ToolStrip,
            _ => (ToolStripFontKind?)null
        };

        if (fontKind == null)
        {
            return;
        }

        var font = ResolveFont(colorTable, fontKind.Value);
        if (font != null && toolStrip.Font != font)
        {
            toolStrip.Font = font;
            RefreshToolStripVisual(toolStrip);
        }
    }

    private static Font? ResolveFont(KryptonColorTable colorTable, ToolStripFontKind fontKind) =>
        fontKind switch
        {
            ToolStripFontKind.MenuStrip => colorTable.MenuStripFont,
            ToolStripFontKind.StatusStrip => colorTable.StatusStripFont,
            _ => colorTable.ToolStripFont
        };
}
