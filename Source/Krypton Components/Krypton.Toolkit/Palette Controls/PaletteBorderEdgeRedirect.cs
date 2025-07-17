#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Implement storage for palette border edge details.
/// </summary>
public class PaletteBorderEdgeRedirect : PaletteBack,
    IPaletteBorder
{
    #region Internal Classes
    internal class BackToBorder : IPaletteBack
    {
        #region Instance Fields
        private readonly IPaletteBorder _parent;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the BackToBorder class.
        /// </summary>
        /// <param name="parent">Parent to get border values from.</param>
        public BackToBorder([DisallowNull] IPaletteBorder parent)
        {
            Debug.Assert(parent != null);
            _parent = parent!;
        }
        #endregion

        #region IPaletteBack
        /// <summary>
        /// Gets the actual background draw value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetBackDraw(PaletteState state) => _parent.GetBorderDraw(state);

        /// <summary>
        /// Gets the actual background graphics hint value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public PaletteGraphicsHint GetBackGraphicsHint(PaletteState state) =>
            // We never want the border edge to use anti aliasing
            PaletteGraphicsHint.None;

        /// <summary>
        /// Gets the first background color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBackColor1(PaletteState state) => _parent.GetBorderColor1(state);

        /// <summary>
        /// Gets the second back color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBackColor2(PaletteState state) => _parent.GetBorderColor2(state);

        /// <summary>
        /// Gets the color drawing style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public PaletteColorStyle GetBackColorStyle(PaletteState state) => _parent.GetBorderColorStyle(state);

        /// <summary>
        /// Gets the color alignment style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public PaletteRectangleAlign GetBackColorAlign(PaletteState state) => _parent.GetBorderColorAlign(state);

        /// <summary>
        /// Gets the color background angle.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public float GetBackColorAngle(PaletteState state) => _parent.GetBorderColorAngle(state);

        /// <summary>
        /// Gets a background image.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public Image? GetBackImage(PaletteState state) => _parent.GetBorderImage(state);

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public PaletteImageStyle GetBackImageStyle(PaletteState state) => _parent.GetBorderImageStyle(state);

        /// <summary>
        /// Gets the image alignment style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public PaletteRectangleAlign GetBackImageAlign(PaletteState state) => _parent.GetBorderImageAlign(state);

        #endregion
    }
    #endregion

    #region Instance Fields
    private IPaletteBorder _inherit;
    private readonly BackToBorder _translate;
    private int _borderWidth;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteBorderEdge class.
    /// </summary>
    /// <param name="inherit">Source for inheriting defaulted values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteBorderEdgeRedirect(IPaletteBorder inherit,
        NeedPaintHandler? needPaint)
        : base(null, needPaint)
    {
        // Remember inheritance
        _inherit = inherit;

        // Default properties
        _borderWidth = -1;

        // Create the helper object that passes background
        // requests from the base class and converts them into
        // border requests on ourself.
        _translate = new BackToBorder(this);
        SetInherit(_translate);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (_borderWidth == -1) && base.IsDefault;

    #endregion

    #region Width
    /// <summary>
    /// Gets and sets the border width.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Border width.")]
    [DefaultValue(-1)]
    [RefreshProperties(RefreshProperties.All)]
    public int Width
    {
        get => _borderWidth;

        set
        {
            if (value != _borderWidth)
            {
                _borderWidth = value;
                PerformNeedPaint(true);
            }
        }
    }
    #endregion

    #region SetPalettes
    /// <summary>
    /// Update the source palettes for drawing.
    /// </summary>
    /// <param name="paletteBorder">Palette source for the border.</param>
    public virtual void SetPalette(IPaletteBorder paletteBorder) => _inherit = paletteBorder;
    #endregion

    #region IPaletteBorder
    /// <summary>
    /// Gets the actual border draw value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetBorderDraw(PaletteState state) => _inherit.GetBorderDraw(state);

    /// <summary>
    /// Gets the actual borders to draw value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteDrawBorders value.</returns>
    public PaletteDrawBorders GetBorderDrawBorders(PaletteState state) => _inherit.GetBorderDrawBorders(state);

    /// <summary>
    /// Gets the actual border graphics hint value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public PaletteGraphicsHint GetBorderGraphicsHint(PaletteState state) => _inherit.GetBorderGraphicsHint(state);

    /// <summary>
    /// Gets the actual first border color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetBorderColor1(PaletteState state) => _inherit.GetBorderColor1(state);

    /// <summary>
    /// Gets the second border color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetBorderColor2(PaletteState state) => _inherit.GetBorderColor2(state);

    /// <summary>
    /// Gets the color drawing style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public PaletteColorStyle GetBorderColorStyle(PaletteState state) => _inherit.GetBorderColorStyle(state);

    /// <summary>
    /// Gets the color alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public PaletteRectangleAlign GetBorderColorAlign(PaletteState state) => _inherit.GetBorderColorAlign(state);

    /// <summary>
    /// Gets the color border angle.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public float GetBorderColorAngle(PaletteState state) => _inherit.GetBorderColorAngle(state);

    /// <summary>
    /// Gets the border width.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Border width.</returns>
    public int GetBorderWidth(PaletteState state) => Width != -1 ? Width : _inherit.GetBorderWidth(state);

    /// <summary>
    /// Gets the border rounding.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Border rounding.</returns>
    public float GetBorderRounding(PaletteState state) => _inherit.GetBorderRounding(state);

    /// <summary>
    /// Gets a border image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public Image? GetBorderImage(PaletteState state) => _inherit.GetBorderImage(state);

    /// <summary>
    /// Gets the border image style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public PaletteImageStyle GetBorderImageStyle(PaletteState state) => _inherit.GetBorderImageStyle(state);

    /// <summary>
    /// Gets the image alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public PaletteRectangleAlign GetBorderImageAlign(PaletteState state) => _inherit.GetBorderImageAlign(state);

    #endregion
}