#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

// ReSharper disable MemberCanBePrivate.Global

namespace Krypton.Toolkit;

/// <summary>
/// Combines button functionality with the styling features of the Krypton Toolkit.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonButton), "ToolboxBitmaps.KryptonButton.bmp")]
[DefaultEvent(nameof(Click))]
[DefaultProperty(nameof(Text))]
[DesignerCategory(@"code")]
[Description(@"Raises an event when the user clicks it.")]
[Designer(typeof(KryptonButtonDesigner))]
public class KryptonButton : KryptonDropButton
{
    #region Instance Fields

    private readonly InputGlowingBorderViewIntegration _glowingBorder;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonButton class.
    /// </summary>
    public KryptonButton()
    {
        // Create the view button instance
        _drawButton.DropDown = false;
        _drawButton.Splitter = false;

        // Create a button controller to handle button style behaviour
        _buttonController.BecomesFixed = false;

        _glowingBorder = new InputGlowingBorderViewIntegration(this,
            NeedPaintDelegate,
            () => IsButtonActive,
            GetTripleState,
            ViewDrawButton,
            () => ViewDrawButton.State);

        ViewManager = new ViewManager(this, _glowingBorder.ViewRoot);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the optional glowing border values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Optional glowing border drawn around the button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public InputGlowingBorderValues GlowingBorderValues => _glowingBorder.Values;

    private bool ShouldSerializeGlowingBorderValues() => !GlowingBorderValues.IsDefault;

    /// <summary>
    /// Gets and sets the visual orientation of the control
    /// </summary>
    [Browsable(true)]
    [Localizable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(VisualOrientation.Top)]
    public virtual VisualOrientation Orientation
    {
        // Backward compatible fix.
        get => ButtonOrientation;
        set => ButtonOrientation = value;
    }

    /// <summary>Gets or sets a value indicating whether [show split option].</summary>
    /// <value><c>true</c> if [show split option]; otherwise, <c>false</c>.</value>

    [Category(@"Visuals")]
    [DefaultValue(false)]
    [Description(@"Displays the split/dropdown option.")]
    public bool ShowSplitOption
    {
        // Backward compatible fix.
        get => base.Splitter;
        set
        {
            _drawButton.DropDown = value;
            base.Splitter = value;
        }
    }

    [Browsable(false)]
    [Localizable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    public new bool Splitter
    {
        get => base.Splitter;
        set => base.Splitter = value;
    }

    /// <summary>
    /// Request the control repaint itself and children.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    public override void PerformNeedPaint(bool needLayout)
    {
        _glowingBorder.UpdateAnimationState();
        base.PerformNeedPaint(needLayout);
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Release managed and unmanaged resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _glowingBorder.Dispose();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Implementation

    private bool IsButtonActive
    {
        get
        {
            if (DesignMode || ContainsFocus)
            {
                return true;
            }

            return ViewDrawButton.State switch
            {
                PaletteState.Tracking => true,
                PaletteState.Pressed => true,
                PaletteState.CheckedTracking => true,
                PaletteState.CheckedPressed => true,
                _ => false
            };
        }
    }

    private IPaletteTriple GetTripleState() => Enabled ? ViewDrawButton.CurrentPalette : StateDisabled;

    #endregion
}
