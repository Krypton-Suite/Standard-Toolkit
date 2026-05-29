#region BSD License
/*
 *
 *   BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Storage for palette integrated toolbar images.</summary>
public class KryptonPaletteImagesIntegratedToolBar : Storage
{

    #region Instance Fields

    private PaletteRedirect? _redirect;
    private Image? _copy;
    private Image? _cut;
    private Image? _help;
    private Image? _paste;
    private Image? _new;
    private Image? _open;
    private Image? _pageSetup;
    private Image? _printPreview;
    private Image? _print;
    private Image? _quickPrint;
    private Image? _redo;
    private Image? _undo;
    private Image? _saveAll;
    private Image? _saveAs;
    private Image? _save;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonPaletteImagesIntegratedToolBar" /> class.</summary>
    /// <param name="redirect">The redirect.</param>
    /// <param name="needPaint">The need paint.</param>
    public KryptonPaletteImagesIntegratedToolBar(PaletteRedirect? redirect, NeedPaintHandler needPaint)
    {
        _redirect = redirect;

        NeedPaint = needPaint;

        _copy = null;
        _cut = null;
        _help = null;
        _paste = null;
        _new = null;
        _open = null;
        _pageSetup = null;
        _printPreview = null;
        _redo = null;
        _undo = null;
        _saveAll = null;
        _saveAs = null;
        _save = null;
        _print = null;
        _quickPrint = null;
    }

    #endregion

    #region IsDefault

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault { get; }

    #endregion

    #region PopulateFromBase

    /// <summary>Populates values from base palette.</summary>
    public void PopulateFromBase()
    {
        _copy = _redirect?.GetButtonSpecImage(PaletteButtonSpecStyle.Copy, PaletteState.Normal);

        _cut = _redirect?.GetButtonSpecImage(PaletteButtonSpecStyle.Cut, PaletteState.Normal);

        _help = _redirect?.GetButtonSpecImage(PaletteButtonSpecStyle.FormHelp, PaletteState.Normal);

        _print = _redirect?.GetButtonSpecImage(PaletteButtonSpecStyle.Print, PaletteState.Normal);

        _pageSetup = _redirect?.GetButtonSpecImage(PaletteButtonSpecStyle.PageSetup, PaletteState.Normal);

        _paste = _redirect?.GetButtonSpecImage(PaletteButtonSpecStyle.Paste, PaletteState.Normal);

        _printPreview = _redirect?.GetButtonSpecImage(PaletteButtonSpecStyle.PrintPreview, PaletteState.Normal);

        _new = _redirect?.GetButtonSpecImage(PaletteButtonSpecStyle.New, PaletteState.Normal);

        _open = _redirect?.GetButtonSpecImage(PaletteButtonSpecStyle.Open, PaletteState.Normal);

        _redo = _redirect?.GetButtonSpecImage(PaletteButtonSpecStyle.Redo, PaletteState.Normal);

        _undo = _redirect?.GetButtonSpecImage(PaletteButtonSpecStyle.Undo, PaletteState.Normal);

        _save = _redirect?.GetButtonSpecImage(PaletteButtonSpecStyle.Save, PaletteState.Normal);

        _saveAll = _redirect?.GetButtonSpecImage(PaletteButtonSpecStyle.SaveAll, PaletteState.Normal);

        _saveAs = _redirect?.GetButtonSpecImage(PaletteButtonSpecStyle.SaveAs, PaletteState.Normal);

        _quickPrint = _redirect?.GetButtonSpecImage(PaletteButtonSpecStyle.QuickPrint, PaletteState.Normal);
    }

    #endregion

    #region SetRedirector

    /// <summary>Update the redirector with new reference.</summary>
    /// <param name="redirect">Target redirector.</param>
    public void SetRedirector(PaletteRedirect? redirect) =>
        // Update our cached reference
        _redirect = redirect;

    #endregion

    #region New

    /// <summary>Gets and sets new image that the integrated toolbar inherits from.</summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"New image that the integrated toolbar inherits from.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? New
    {
        get => _new;

        set
        {
            if (_new != value)
            {
                _new = value;

                PerformNeedPaint(true);
            }
        }
    }

    #endregion

    #region Open

    /// <summary>Gets and sets open image that the integrated toolbar inherits from.</summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Open image that the integrated toolbar inherits from.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Open
    {
        get => _open;

        set
        {
            if (_open != value)
            {
                _open = value;

                PerformNeedPaint(true);
            }
        }
    }

    #endregion

    #region Save

    /// <summary>Gets and sets save image that the integrated toolbar inherits from.</summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Save image that the integrated toolbar inherits from.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Save
    {
        get => _save;

        set
        {
            if (_save != value)
            {
                _save = value;

                PerformNeedPaint(true);
            }
        }
    }

    #endregion

    #region Save As

    /// <summary>Gets and sets save as image that the integrated toolbar inherits from.</summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Save As image that the integrated toolbar inherits from.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? SaveAs
    {
        get => _saveAs;

        set
        {
            if (_saveAs != value)
            {
                _saveAs = value;

                PerformNeedPaint(true);
            }
        }
    }

    #endregion

    #region Save All

    /// <summary>Gets and sets save all image that the integrated toolbar inherits from.</summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Save All image that the integrated toolbar inherits from.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? SaveAll
    {
        get => _saveAll;

        set
        {
            if (_saveAll != value)
            {
                _saveAll = value;

                PerformNeedPaint(true);
            }
        }
    }

    #endregion

    #region Cut

    /// <summary>Gets and sets cut image that the integrated toolbar inherits from.</summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Cut image that the integrated toolbar inherits from.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Cut
    {
        get => _cut;

        set
        {
            if (_cut != value)
            {
                _cut = value;

                PerformNeedPaint(true);
            }
        }
    }

    #endregion

    #region Copy

    /// <summary>Gets and sets copy image that the integrated toolbar inherits from.</summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Copy image that the integrated toolbar inherits from.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Copy
    {
        get => _copy;

        set
        {
            if (_copy != value)
            {
                _copy = value;

                PerformNeedPaint(true);
            }
        }
    }

    #endregion

    #region Paste

    /// <summary>Gets and sets paste image that the integrated toolbar inherits from.</summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Paste image that the integrated toolbar inherits from.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Paste
    {
        get => _paste;

        set
        {
            if (_paste != value)
            {
                _paste = value;

                PerformNeedPaint(true);
            }
        }
    }

    #endregion

    #region Undo

    /// <summary>Gets and sets undo image that the integrated toolbar inherits from.</summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Undo image that the integrated toolbar inherits from.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Undo
    {
        get => _undo;

        set
        {
            if (_undo != value)
            {
                _undo = value;

                PerformNeedPaint(true);
            }
        }
    }

    #endregion

    #region Redo

    /// <summary>Gets and sets redo image that the integrated toolbar inherits from.</summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Redo image that the integrated toolbar inherits from.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Redo
    {
        get => _redo;

        set
        {
            if (_redo != value)
            {
                _redo = value;

                PerformNeedPaint(true);
            }
        }
    }

    #endregion

    #region Page Setup

    /// <summary>Gets and sets page setup image that the integrated toolbar inherits from.</summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Page Setup image that the integrated toolbar inherits from.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? PageSetup
    {
        get => _pageSetup;

        set
        {
            if (_pageSetup != value)
            {
                _pageSetup = value;

                PerformNeedPaint(true);
            }
        }
    }

    #endregion

    #region Print Preview

    /// <summary>Gets and sets print preview image that the integrated toolbar inherits from.</summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Print Preview image that the integrated toolbar inherits from.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? PrintPreview
    {
        get => _printPreview;

        set
        {
            if (_printPreview != value)
            {
                _printPreview = value;

                PerformNeedPaint(true);
            }
        }
    }

    #endregion

    #region Print

    /// <summary>Gets and sets print image that the integrated toolbar inherits from.</summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Print image that the integrated toolbar inherits from.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Print
    {
        get => _print;

        set
        {
            if (_print != value)
            {
                _print = value;

                PerformNeedPaint(true);
            }
        }
    }

    #endregion

    #region Quick Print

    /// <summary>Gets and sets quick print image that the integrated toolbar inherits from.</summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Quick Print image that the integrated toolbar inherits from.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? QuickPrint
    {
        get => _quickPrint;

        set
        {
            if (_quickPrint != value)
            {
                _quickPrint = value;

                PerformNeedPaint(true);
            }
        }
    }

    #endregion

    #region Help

    /// <summary>Gets and sets help image that the integrated toolbar inherits from.</summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Help image that the integrated toolbar inherits from.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Help
    {
        get => _help;

        set
        {
            if (_help != value)
            {
                _help = value;

                PerformNeedPaint(true);
            }
        }
    }

    #endregion

    #region Public override
    // Overrides ToString() since this normally displays the text "Modified" in the property grid.
    public override string ToString()
    {
        return string.Empty;
    }
    #endregion
}