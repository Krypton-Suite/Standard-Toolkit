#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// A push button with a dedicated split chevron that opens a drop-down menu.
/// </summary>
/// <remarks>
/// The button body raises <see cref="Control.Click"/> (and executes <see cref="KryptonDropButton.KryptonCommand"/>).
/// The chevron raises <see cref="KryptonDropButton.DropDown"/> and shows <see cref="VisualControlBase.KryptonContextMenu"/>
/// or <see cref="Control.ContextMenuStrip"/>. Prefer this type over <see cref="KryptonButton.ShowSplitOption"/> when the
/// control is always a split button. Use <see cref="KryptonDropButton"/> with <see cref="KryptonDropButton.Splitter"/>
/// set to <c>false</c> when the whole button should open the menu.
/// </remarks>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonSplitButton), "ToolboxBitmaps.KryptonSplitButton.bmp")]
[DefaultEvent(nameof(Click))]
[DefaultProperty(nameof(Text))]
[Designer(typeof(KryptonDropButtonDesigner))]
[DesignerCategory(@"code")]
[Description(@"Raises Click for the button body and DropDown for the split chevron.")]
public class KryptonSplitButton : KryptonDropButton
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonSplitButton"/> class.
    /// </summary>
    public KryptonSplitButton()
    {
        // Always a split: body click vs chevron drop-down.
        _drawButton.DropDown = true;
        _drawButton.Splitter = true;
        _buttonController.BecomesFixed = true;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets a value indicating that the button always acts as a splitter.
    /// </summary>
    /// <remarks>
    /// The setter is ignored; a <see cref="KryptonSplitButton"/> cannot become a whole-button drop-down.
    /// Use <see cref="KryptonDropButton"/> when that behaviour is required.
    /// </remarks>
    [Browsable(false)]
    [Localizable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public override bool Splitter
    {
        get => true;
        set
        {
            _drawButton.DropDown = true;
            base.Splitter = true;
        }
    }
    #endregion

    #region Protected Overrides
    /// <inheritdoc />
    protected override AccessibleObject CreateAccessibilityInstance() => new KryptonSplitButtonAccessibleObject(this);

    /// <inheritdoc />
    protected override bool MnemonicPerformsDropDown => false;

    /// <inheritdoc />
    protected override KryptonContextMenuPositionH GetPositionH() => DropDownPosition switch
    {
        // Align the menu with the chevron rather than the caption on a wide split button.
        VisualOrientation.Right => KryptonContextMenuPositionH.Right,
        VisualOrientation.Left => KryptonContextMenuPositionH.Left,
        _ => base.GetPositionH()
    };

    /// <inheritdoc />
    protected override KryptonContextMenuPositionV GetPositionV() => DropDownPosition switch
    {
        VisualOrientation.Top => KryptonContextMenuPositionV.Above,
        VisualOrientation.Bottom => KryptonContextMenuPositionV.Below,
        _ => base.GetPositionV()
    };
    #endregion
}
