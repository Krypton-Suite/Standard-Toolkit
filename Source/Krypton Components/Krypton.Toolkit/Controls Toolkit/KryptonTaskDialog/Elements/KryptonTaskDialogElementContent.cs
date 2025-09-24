#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class KryptonTaskDialogElementContent : KryptonTaskDialogElementBase,
    IKryptonTaskDialogElementContent,
    IKryptonTaskDialogElementForeColor
{
    #region Fields
    // default text format flags
    private const TextFormatFlags textFormatFlags = TextFormatFlags.WordBreak | TextFormatFlags.NoPadding | TextFormatFlags.ExpandTabs;

    // Content text
    KryptonWrapLabel _textControl;
    // Ttextbox width
    int _textBoxWidth;
    #endregion

    #region Identity
    public KryptonTaskDialogElementContent(KryptonTaskDialogDefaults taskDialogDefaults) 
        : base(taskDialogDefaults)
    {
        Panel.Height = 120;
        _textBoxWidth = Defaults.ClientWidth - Defaults.PanelLeft - Defaults.PanelRight;

        _textControl = new()
        {
            AutoSize = true,
            Width = _textBoxWidth,
            Height = 0,
            Padding = new Padding(0),
            Margin = new Padding(3, 0, 0, 0),
            Location = new Point(10, 10),
            MaximumSize = new Size(_textBoxWidth, 0),
            MinimumSize = new Size(_textBoxWidth, 0),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
        };

        Panel.Controls.Add(_textControl);
    }
    #endregion

    #region Protected/Internal
    /// <inheritdoc/>
    protected override void OnSizeChanged(bool performLayout = false)
    {
        // Updates / changes are deferred if the element is not visible or until PerformLayout is called
        if (LayoutDirty && (Visible || performLayout))
        {
            Font font = _textControl.StateCommon.Font
                ?? Palette.GetContentShortTextFont(PaletteContentStyle.LabelNormalControl, PaletteState.Normal)
                ?? KryptonManager.CurrentGlobalPalette.BaseFont;

            int height = TextRenderer.MeasureText(_textControl.Text, font, new SizeF(_textBoxWidth, float.MaxValue).ToSize(), textFormatFlags).Height;

            // Controls seem to need a little help here to stay within the correct bounds
            Panel.Height = height + Defaults.PanelTop + Defaults.PanelBottom;
            _textControl.Width = _textBoxWidth - Defaults.PanelLeft - Defaults.PanelRight;
            _textControl.Height = height;

            // Tell everybody about it when visible.
            base.OnSizeChanged(performLayout);

            // Done
            LayoutDirty = false;
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

    /// <inheritdoc/>
    internal override void PerformLayout()
    {
        base.PerformLayout();
        OnSizeChanged(true);
    }

    /// <inheritdoc/>
    public override bool Visible 
    { 
        get => base.Visible;

        set
        {
            base.Visible = value;
            OnSizeChanged();
        }
    }
    #endregion

    #region Public
    /// <inheritdoc/>
    public string Text 
    {
        get => _textControl.Text;

        set
        {
            if (_textControl.Text != value)
            {
                _textControl.Text = CommonHelper.NormalizeLineBreaks(value) + Environment.NewLine;
                LayoutDirty = true;
                OnSizeChanged();
            }
        }
    }

    public Color ForeColor 
    {
        get => _textControl.StateCommon.TextColor;
        set => _textControl.StateCommon.TextColor = value;
    }
    #endregion
}