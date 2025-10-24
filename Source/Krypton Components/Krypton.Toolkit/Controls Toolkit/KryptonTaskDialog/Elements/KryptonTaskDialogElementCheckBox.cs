#region BSD License
/*
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
        SetupPanel();

        LayoutDirty = true;
    }

    #region Private
    private void SetupControls()
    {
        _checkBox = new();
        _checkBox.AutoSize = true;
        _checkBox.Padding = new Padding( Defaults.ComponentSpace, 0, 0, 0 );
        _checkBox.Margin = Defaults.NullMargin;
        _checkBox.CheckState = CheckState.Unchecked;
    }

    private void SetupPanel()
    {
        Panel.Width = Defaults.ClientWidth;
        SetupControls();

        _tlp.Controls.Add( _checkBox, 0, 0 );
    }
    #endregion
    #region Protected/Internal
    /// <inheritdoc/>
    protected override void OnSizeChanged(bool performLayout = false)
    {
        // Updates / changes are deferred if the element is not visible or until PerformLayout is called
        if (LayoutDirty && (Visible || performLayout))
        {
            Panel.Height = _checkBox.Height + Defaults.PanelTop + Defaults.PanelBottom;

            base.OnSizeChanged(performLayout);

            LayoutDirty = false;
        }
    }

    /// <inheritdoc/>
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
