#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 * © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

using System.Windows.Forms;
using System.Xml.Linq;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Krypton.Toolkit;

public class KryptonTaskDialogElementHyperLink : KryptonTaskDialogElementSingleLineControlBase,
    IKryptonTaskDialogElementDescription,
    IKryptonTaskDialogElementUrl
{
    #region Fields
    private KryptonWrapLabel _description;
    private KryptonLinkLabel _linkLabel;
    private bool _disposed;
    #endregion

    #region Events
    /// <summary>
    /// Subscribe to be notifed when the user clicks the hyperlink.
    /// </summary>
    public event Action LinkClicked;
    #endregion

    #region Identity
    public KryptonTaskDialogElementHyperLink(KryptonTaskDialogDefaults taskDialogDefaults) 
        : base(taskDialogDefaults, 2)
    {
        _disposed = false;

        _description = new();
        _linkLabel = new();
        SetupPanel();

        ShowDescription = true;
        // Description will trigger theme/size changes 
        // LinkLabel and OnPalettePaint don't react correct in this case.
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

        // Safegaurd against an "early" call from OnGlobalPaletteChanged()
        if (_description is not null && _linkLabel is not null)
        {
            UpdateLinkColors();
            LayoutDirty = true;
            OnSizeChanged();
        }
    }
    #endregion

    #region Public (override)
    /// <summary>
    /// Hyperlink Url
    /// </summary>
    public string Url 
    {
        get => _linkLabel.Text;

        set
        {
            if (_linkLabel.Text != value)
            {
                _linkLabel.Text = value;
            }
        }
    }

    /// <inheritdoc/>
    public override Color ForeColor
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;
                _description.StateCommon.TextColor = ForeColor;
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
    #endregion

    #region Private
    private void OnSizeChanged(bool performLayout = false)
    {
        // Updates / changes are deferred if the element is not visible or until PerformLayout is called
        if (LayoutDirty && (Visible || performLayout))
        {
            // Get the description size
            int height = ShowDescription
                ? _description.Height + _description.Margin.Bottom
                : 0;

            // Size the panel
            Panel.Height = Defaults.PanelTop + Defaults.PanelBottom + _linkLabel.Height + height;

            // Tell everybody about it when visible.
            if (!performLayout)
            {
                base.OnSizeChanged();
            }

            // Done
            LayoutDirty = false;
        }
    }

    private void SetupPanel()
    {
        // Label holds the description that goes with the hyperlink
        _description.AutoSize = true;
        _description.Padding = _nullPadding;
        _description.Margin = new Padding(0, 0, 0, Defaults.ComponentSpace);

        // The hyperlink
        _linkLabel.AutoSize = true;
        _linkLabel.Padding = _nullPadding;
        _linkLabel.Margin = _nullMargin;
        _linkLabel.LinkClicked += OnLinkClicked;

        // add the controls
        _tlp.Controls.Add(_description, 0, 0);
        _tlp.Controls.Add(_linkLabel, 0, 1);
    }

    private void UpdateLinkColors()
    {
        Color linkColor = _linkLabel.StateCommon.GetContentShortTextColor1(PaletteState.LinkNotVisitedOverride);
        _linkLabel.OverrideFocus.ShortText.Color1      = linkColor;
        _linkLabel.OverrideNotVisited.ShortText.Color1 = linkColor;
        _linkLabel.OverridePressed.ShortText.Color1    = linkColor;
        _linkLabel.OverrideVisited.ShortText.Color1    = linkColor;
    }

    private void OnLinkClicked(object? sender, EventArgs e)
    {
        LinkClicked?.Invoke();
    }
    #endregion

    #region IDispose
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _linkLabel.LinkClicked -= OnLinkClicked;
            _description.SizeChanged -= OnDescriptionSizeChanged;
            _disposed = true;
        }

        base.Dispose(disposing);
    }
    #endregion
}

