using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for input control palette background details.
    /// </summary>
    public class PaletteInputControlBackStates : Storage,
                                                 IPaletteBack
    {
        #region Instance Fields

        private Color _color1;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteInputControlBackStates class.
        /// </summary>
        /// <param name="inherit">Source for inheriting defaulted values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteInputControlBackStates(IPaletteBack inherit,
                                             NeedPaintHandler needPaint)
        {
            Debug.Assert(inherit != null);

            // Remember inheritance
            Inherit = inherit;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Default the initial values
            _color1 = Color.Empty;
           }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (Color1 == Color.Empty);

        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public void SetInherit(IPaletteBack inherit)
        {
            Inherit = inherit;
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Palette state to use when populating.</param>
        public virtual void PopulateFromBase(PaletteState state)
        {
            // Get the values and set into storage
            Color1 = GetBackColor1(state);
        }
        #endregion

        #region Draw
        /// <summary>
        /// Gets the actual background draw value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetBackDraw(PaletteState state)
        {
            return Inherit.GetBackDraw(state);
        }
        #endregion

        #region GraphicsHint
        /// <summary>
        /// Gets the actual background graphics hint value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public PaletteGraphicsHint GetBackGraphicsHint(PaletteState state)
        {
            return Inherit.GetBackGraphicsHint(state);
        }
        #endregion

        #region Color1
        /// <summary>
        /// Gets and sets the first background color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Main background color.")]
        [KryptonDefaultColorAttribute()]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color Color1
        {
            get => _color1;

            set
            {
                if (value != _color1)
                {
                    _color1 = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Gets the first background color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBackColor1(PaletteState state)
        {
            if (Color1 != Color.Empty)
            {
                return Color1;
            }
            else
            {
                return Inherit.GetBackColor1(state);
            }
        }
        #endregion

        #region Color2
        /// <summary>
        /// Gets the second back color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBackColor2(PaletteState state)
        {
            return Inherit.GetBackColor2(state);
        }
        #endregion

        #region ColorStyle
        /// <summary>
        /// Gets the color drawing style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public PaletteColorStyle GetBackColorStyle(PaletteState state)
        {
            return Inherit.GetBackColorStyle(state);
        }
        #endregion

        #region ColorAlign
        /// <summary>
        /// Gets the color alignment style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public PaletteRectangleAlign GetBackColorAlign(PaletteState state)
        {
            return Inherit.GetBackColorAlign(state);
        }
        #endregion

        #region ColorAngle
        /// <summary>
        /// Gets the color background angle.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public float GetBackColorAngle(PaletteState state)
        {
            return Inherit.GetBackColorAngle(state);
        }
        #endregion

        #region Image
        /// <summary>
        /// Gets a background image.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public Image GetBackImage(PaletteState state)
        {
            return Inherit.GetBackImage(state);
        }
        #endregion

        #region ImageStyle
        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public PaletteImageStyle GetBackImageStyle(PaletteState state)
        {
            return Inherit.GetBackImageStyle(state);
        }
        #endregion

        #region ImageAlign
        /// <summary>
        /// Gets the image alignment style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public PaletteRectangleAlign GetBackImageAlign(PaletteState state)
        {
            return Inherit.GetBackImageAlign(state);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the inheritence parent.
        /// </summary>
        protected IPaletteBack Inherit { get; private set; }

        #endregion
    }
}
