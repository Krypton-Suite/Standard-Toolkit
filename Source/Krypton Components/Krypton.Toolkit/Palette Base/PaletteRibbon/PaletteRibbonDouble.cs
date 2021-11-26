#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for ribbon background and text values.
    /// </summary>
    public class PaletteRibbonDouble : Storage,
                                       IPaletteRibbonBack,
                                       IPaletteRibbonText
    {
        #region Instance Fields
        private IPaletteRibbonBack _inheritBack;
        private IPaletteRibbonText _inheritText;
        private Color _backColor1;
        private Color _backColor2;
        private Color _backColor3;
        private Color _backColor4;
        private Color _backColor5;
        private Color _textColor;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRibbonDouble class.
        /// </summary>
        /// <param name="inheritBack">Source for inheriting background values.</param>
        /// <param name="inheritText">Source for inheriting text values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteRibbonDouble(IPaletteRibbonBack inheritBack,
                                   IPaletteRibbonText inheritText,
                                   NeedPaintHandler needPaint) 
        {
            Debug.Assert(inheritBack != null);
            Debug.Assert(inheritText != null);

            // Remember inheritance
            _inheritBack = inheritBack;
            _inheritText = inheritText;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Define default values
            _backColor1 = Color.Empty;
            _backColor2 = Color.Empty;
            _backColor3 = Color.Empty;
            _backColor4 = Color.Empty;
            _backColor5 = Color.Empty;
            _textColor = Color.Empty;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (BackColor1 == Color.Empty) &&
                                          (BackColor2 == Color.Empty) &&
                                          (BackColor3 == Color.Empty) &&
                                          (BackColor4 == Color.Empty) &&
                                          (BackColor5 == Color.Empty) &&
                                          (TextColor == Color.Empty);

        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritance parent.
        /// </summary>
        public void SetInherit(IPaletteRibbonBack inheritBack,
                               IPaletteRibbonText inheritText)
        {
            _inheritBack = inheritBack;
            _inheritText = inheritText;
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Palette state to use when populating.</param>
        public void PopulateFromBase(PaletteState state)
        {
            BackColor1 = GetRibbonBackColor1(state);
            BackColor2 = GetRibbonBackColor2(state);
            BackColor3 = GetRibbonBackColor3(state);
            BackColor4 = GetRibbonBackColor4(state);
            BackColor5 = GetRibbonBackColor5(state);
            TextColor = GetRibbonTextColor(state);
        }
        #endregion

        #region BackColorStyle
        /// <summary>
        /// Gets the background drawing style for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteState state) => _inheritBack.GetRibbonBackColorStyle(state);

        #endregion

        #region BackColor1
        /// <summary>
        /// Gets and sets the first background color for the ribbon item.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("First background color for the ribbon item.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color BackColor1
        {
            get => _backColor1;

            set
            {
                if (_backColor1 != value)
                {
                    _backColor1 = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeBackColor1() => BackColor1 != Color.Empty;
        private void ResetBackColor1() => BackColor1 = Color.Empty;

        /// <summary>
        /// Gets the first background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor1(PaletteState state) =>
            BackColor1 != Color.Empty ? BackColor1 : _inheritBack.GetRibbonBackColor1(state);

        #endregion

        #region BackColor2
        /// <summary>
        /// Gets and sets the second background color for the ribbon item.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Second background color for the ribbon item.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color BackColor2
        {
            get => _backColor2;

            set
            {
                if (_backColor2 != value)
                {
                    _backColor2 = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeBackColor2() => BackColor2 != Color.Empty;
        private void ResetBackColor2() => BackColor2 = Color.Empty;

        /// <summary>
        /// Gets the second background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor2(PaletteState state) =>
            BackColor2 != Color.Empty ? BackColor2 : _inheritBack.GetRibbonBackColor2(state);

        #endregion

        #region BackColor3
        /// <summary>
        /// Gets and sets the third background color for the ribbon item.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Third background color for the ribbon item.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color BackColor3
        {
            get => _backColor3;

            set
            {
                if (_backColor3 != value)
                {
                    _backColor3 = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeBackColor3() => BackColor3 != Color.Empty;
        private void ResetBackColor3() => BackColor3 = Color.Empty;

        /// <summary>
        /// Gets the third background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor3(PaletteState state) =>
            BackColor3 != Color.Empty ? BackColor3 : _inheritBack.GetRibbonBackColor3(state);

        #endregion

        #region BackColor4
        /// <summary>
        /// Gets and sets the fourth background color for the ribbon item.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Fourth background color for the ribbon item.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color BackColor4
        {
            get => _backColor4;

            set
            {
                if (_backColor4 != value)
                {
                    _backColor4 = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeBackColor4() => BackColor4 != Color.Empty;
        private void ResetBackColor4() => BackColor4 = Color.Empty;

        /// <summary>
        /// Gets the fourth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor4(PaletteState state) =>
            BackColor4 != Color.Empty ? BackColor4 : _inheritBack.GetRibbonBackColor4(state);

        #endregion

        #region BackColor5
        /// <summary>
        /// Gets and sets the fifth background color for the ribbon item.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Fifth background color for the ribbon item.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color BackColor5
        {
            get => _backColor5;

            set
            {
                if (_backColor5 != value)
                {
                    _backColor5 = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeBackColor5() => BackColor5 != Color.Empty;
        private void ResetBackColor5() => BackColor5 = Color.Empty;

        /// <summary>
        /// Gets the fifth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor5(PaletteState state) =>
            BackColor5 != Color.Empty ? BackColor5 : _inheritBack.GetRibbonBackColor5(state);

        #endregion

        #region TextColor
        /// <summary>
        /// Gets and sets the Tab color for the item text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Tab color for the tab text.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color TextColor
        {
            get => _textColor;

            set
            {
                if (_textColor != value)
                {
                    _textColor = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeTextColor() => TextColor != Color.Empty;
        private void ResetTextColor() => TextColor = Color.Empty;

        /// <summary>
        /// Gets the tab color for the item text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonTextColor(PaletteState state) =>
            TextColor != Color.Empty ? TextColor : _inheritText.GetRibbonTextColor(state);

        #endregion
    }
}
