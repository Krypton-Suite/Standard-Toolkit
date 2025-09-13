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

namespace Krypton.Toolkit;

public class KryptonTaskDialogElementCheckBox : KryptonTaskDialogElementSingleLineControlBase,
    IKryptonTaskDialogElementText
{
    private KryptonCheckBox _checkBox;

    /// <summary>
    /// Default constructor
    /// </summary>
    public KryptonTaskDialogElementCheckBox(KryptonTaskDialogDefaults taskDialogDefaults) 
        : base(taskDialogDefaults, 1)
    {
        Panel.Width = Defaults.ClientWidth;

        _checkBox = new();
        _checkBox.AutoSize = true;
        _checkBox.Padding = new Padding(5, 0, 0, 0);
        _checkBox.Margin = _nullMargin;
        _checkBox.CheckState = CheckState.Unchecked;

        _tlp.Controls.Add(_checkBox, 0, 0);

        LayoutDirty = true;
    }

    #region Private
    #endregion
    private void OnSizeChanged(bool performLayout = false)
    {
        // Updates / changes are deferred if the element is not visible or until PerformLayout is called
        if (LayoutDirty && (Visible || performLayout))
        {
            Panel.Height = _checkBox.Height + Defaults.PanelTop + Defaults.PanelBottom;

            if (!performLayout)
            {
                base.OnSizeChanged();
            }

            LayoutDirty = false;

        }
    }

    #region Protected/Internal
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

    internal override void PerformLayout()
    {
        base.PerformLayout();
        OnSizeChanged(true);
    }
    #endregion

    #region Public
    /// <summary>
    /// The text for the checkbox.
    /// </summary>
    public virtual string Text 
    {
        get => _checkBox.Text;
        set => _checkBox.Text = value;
    }

    /// <inheritdoc/>
    public override Color ForeColor 
    {
        get => _checkBox.StateCommon.ShortText.Color1;
        set => _checkBox.StateCommon.ShortText.Color1 = value;
    }

    /// <summary>
    /// The checked state of the checkbox.<br/>
    /// </summary>
    public bool Checked
    {
        get => _checkBox.Checked;
        set => _checkBox.Checked = value;
    }
    #endregion
}
