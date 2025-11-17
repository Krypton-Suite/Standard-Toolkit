#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class KryptonTaskDialogElementProgresBar : KryptonTaskDialogElementSingleLineControlBase,
    IKryptonTaskDialogElementDescription,
    IKryptonTaskDialogElementRoundedCorners
{
    #region Fields
    private KryptonWrapLabel _description;
    private KryptonProgressBar _progressBar;
    private bool _disposed;
    #endregion

    #region Identity
    public KryptonTaskDialogElementProgresBar(KryptonTaskDialogDefaults taskDialogDefaults) 
        : base(taskDialogDefaults, 2)
    {
        _disposed = false;
        _description = new();
        _progressBar = new();

        ProgressBar = new KryptonTaskDialogElementProgresBarProperties(_progressBar, OnSizeChanged, this);

        SetupPanel();

        ShowDescription = true;
        // Description will trigger theme/size changes 
        // ProgressBar reacts to PalettePaint
        _description.SizeChanged += OnDescriptionSizeChanged;

        LayoutDirty = true;
        PerformLayout();
    }
    #endregion

    #region Protected/Internal (override)
    /// <inheritdoc/>
    internal override void PerformLayout()
    {
        base.PerformLayout();
        OnSizeChanged(true);
    }

    /// <inheritdoc/>
    private void OnDescriptionSizeChanged(object? sender, EventArgs e)
    {
        LayoutDirty = true;
        OnSizeChanged();
    }

    /// <inheritdoc/>
    protected override void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        base.OnGlobalPaletteChanged(sender, e);

        // Safeguard against an "early" call from OnGlobalPaletteChanged()
        if (_description is not null && _progressBar is not null)
        {
            LayoutDirty = true;
            OnSizeChanged();
        }
    }

    protected override void OnPalettePaint(object? sender, PaletteLayoutEventArgs e)
    {
        base.OnPalettePaint(sender, e);

        // Flag dirty, and if visible call OnSizeChanged,
        // otherwise leave it deferred for a call from PerformLayout.
        LayoutDirty = true;
        if (Visible)
        {
            OnSizeChanged();
        }
    }
    #endregion

    #region Public (override)
    /// <inheritdoc/>
    public override Color ForeColor
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;
                _description.StateCommon.TextColor = value;
            }
        }
    }

    /// <inheritdoc/>
    public string Description 
    { 
        get => _description.Text;
        set
        { 
            if (_description.Text != value)
            {
                _description.Text = value;
            }
        }
    }

    /// <inheritdoc/>
    public bool ShowDescription 
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                _description.Visible = value;
                LayoutDirty = true;
                OnSizeChanged();
            }
        }
    }

    /// <summary>
    /// Configure the ProgressBar.
    /// </summary>
    [Browsable(true)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementProgresBarProperties ProgressBar { get; }

    /// <summary>
    /// Rounds the progressbar corners.
    /// </summary>
    public bool RoundedCorners 
    {
        get => field;
        
        set
        {
            if (field != value)
            {
                field = value;
                _progressBar.StateCommon.Border.Rounding = Defaults.GetCornerRouding(value);
            }
        }
    }
    #endregion

    #region Private
    protected override void OnSizeChanged(bool performLayout = false)
    {
        // Updates / changes are deferred if the element is not visible or until PerformLayout is called
        if (LayoutDirty && (Visible || performLayout))
        {
            // Get the description size
            int height = ShowDescription
                ? _description.Height + _description.Margin.Bottom
                : 0;

            // Size the panel
            Panel.Height = Defaults.PanelTop + Defaults.PanelBottom + _progressBar.Height + _progressBar.Margin.Bottom + height;

            // Tell everybody about it when visible.
            base.OnSizeChanged(performLayout);

            // Done
            LayoutDirty = false;
        }
    }

    private void SetupControls()
    {
        // Label holds the description that goes with the hyperlink
        _description.AutoSize = true;
        _description.Padding = Defaults.NullPadding;
        _description.Margin = new Padding( 0, 0, 0, Defaults.ComponentSpace );

        // The hyperlink
        _progressBar.Width = Defaults.ClientWidth - Defaults.PanelLeft - Defaults.PanelRight;
        _progressBar.Margin = new Padding( 0, 0, 0, Defaults.ComponentSpace );
        _progressBar.Padding = Defaults.NullPadding;
        _progressBar.Margin = Defaults.NullMargin;
    }

    private void SetupPanel()
    {
        SetupControls();

        // add the controls
        _tlp.Controls.Add(_description, 0, 0);
        _tlp.Controls.Add(_progressBar, 0, 1);
    }
    #endregion

    #region IDispose
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _description.SizeChanged -= OnDescriptionSizeChanged;
            _disposed = true;
        }

        base.Dispose(disposing);
    }
    #endregion
}
