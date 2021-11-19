

namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for general ribbon values.
    /// </summary>
    public class PaletteRibbonGeneral : Storage,
                                        IPaletteRibbonGeneral
    {
        #region Instance Fields
        private PaletteRelativeAlign _contextTextAlign;
        private Color _contextTextColor;
        private Font _contextTextFont;
        private IPaletteRibbonGeneral _inherit;
        private PaletteRibbonShape _ribbonShape;
        private Color _dialogDarkColor;
        private Color _dialogLightColor;
        private Color _disabledDarkColor;
        private Color _disabledLightColor;
        private Color _dropArrowDarkColor;
        private Color _dropArrowLightColor;
        private Color _groupSeparatorDark;
        private Color _groupSeparatorLight;
        private Color _minimizeBarDarkColor;
        private Color _minimizeBarLightColor;
        private Color _qatButtonDarkColor;
        private Color _qatButtonLightColor;
        private Color _tabSeparatorColor;
        private Color _tabSeparatorContextColor;
        private Font _textFont;
        private PaletteTextHint _textHint;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRibbonGeneral class.
        /// </summary>
        /// <param name="inherit">Source for inheriting general values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteRibbonGeneral(IPaletteRibbonGeneral inherit,
                                    NeedPaintHandler needPaint)
        {
            Debug.Assert(inherit != null);

            // Remember inheritance
            _inherit = inherit;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Set default values
            _contextTextAlign = PaletteRelativeAlign.Inherit;
            _contextTextColor = Color.Empty;
            _contextTextFont = null;
            _disabledDarkColor = Color.Empty;
            _disabledLightColor = Color.Empty;
            _dialogDarkColor = Color.Empty;
            _dialogLightColor = Color.Empty;
            _dropArrowLightColor = Color.Empty;
            _dropArrowDarkColor = Color.Empty;
            _groupSeparatorDark = Color.Empty;
            _groupSeparatorLight = Color.Empty;
            _minimizeBarDarkColor = Color.Empty;
            _minimizeBarLightColor = Color.Empty;
            _ribbonShape = PaletteRibbonShape.Inherit;
            _tabSeparatorColor = Color.Empty;
            _tabSeparatorContextColor = Color.Empty;
            _textFont = null;
            _textHint = PaletteTextHint.Inherit;
            _qatButtonDarkColor = Color.Empty;
            _qatButtonLightColor = Color.Empty;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (ContextTextAlign == PaletteRelativeAlign.Inherit) &&
                                           (ContextTextColor == Color.Empty) &&
                                           (ContextTextFont == null) &&
                                           (DisabledDark == Color.Empty) &&
                                           (DisabledLight == Color.Empty) &&
                                           (DropArrowLight == Color.Empty) &&
                                           (DropArrowDark == Color.Empty) &&
                                           (GroupDialogDark == Color.Empty) &&
                                           (GroupDialogLight == Color.Empty) &&
                                           (GroupSeparatorDark == Color.Empty) &&
                                           (GroupSeparatorLight == Color.Empty) &&
                                           (MinimizeBarDarkColor == Color.Empty) &&
                                           (MinimizeBarLightColor == Color.Empty) &&
                                           (RibbonShape == PaletteRibbonShape.Inherit) &&
                                           (TextFont == null) &&
                                           (TextHint == PaletteTextHint.Inherit) &&
                                           (TabSeparatorColor == Color.Empty) &&
                                           (TabSeparatorContextColor == Color.Empty) &&
                                           (QATButtonDarkColor == Color.Empty) &&
                                           (QATButtonLightColor == Color.Empty);

        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritance parent.
        /// </summary>
        public void SetInherit(IPaletteRibbonGeneral inherit) => _inherit = inherit;

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            ContextTextAlign = GetRibbonContextTextAlign(PaletteState.Normal);
            ContextTextFont = GetRibbonContextTextFont(PaletteState.Normal);
            ContextTextColor = GetRibbonContextTextColor(PaletteState.Normal);
            DisabledDark = GetRibbonDisabledDark(PaletteState.Normal);
            DisabledLight = GetRibbonDisabledLight(PaletteState.Normal);
            DropArrowDark = GetRibbonDropArrowDark(PaletteState.Normal);
            DropArrowLight = GetRibbonDropArrowLight(PaletteState.Normal);
            GroupDialogDark = GetRibbonGroupDialogDark(PaletteState.Normal);
            GroupDialogLight = GetRibbonGroupDialogLight(PaletteState.Normal);
            GroupSeparatorDark = GetRibbonGroupSeparatorDark(PaletteState.Normal);
            GroupSeparatorLight = GetRibbonGroupSeparatorLight(PaletteState.Normal);
            MinimizeBarDarkColor = GetRibbonMinimizeBarDark(PaletteState.Normal);
            MinimizeBarLightColor = GetRibbonMinimizeBarLight(PaletteState.Normal);
            RibbonShape = GetRibbonShape();
            TabSeparatorColor = GetRibbonTabSeparatorColor(PaletteState.Normal);
            TabSeparatorContextColor = GetRibbonTabSeparatorContextColor(PaletteState.Normal);
            TextFont = GetRibbonTextFont(PaletteState.Normal);
            TextHint = GetRibbonTextHint(PaletteState.Normal);
            QATButtonDarkColor = GetRibbonGroupDialogDark(PaletteState.Normal);
            QATButtonLightColor = GetRibbonGroupDialogLight(PaletteState.Normal);
        }
        #endregion

        #region ContextTextAlign
        /// <summary>
        /// Gets and sets the text alignment for the ribbon context text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Text alignment for the ribbon context text.")]
        [DefaultValue(typeof(PaletteRelativeAlign), "Inherit")]
        [RefreshProperties(RefreshProperties.All)]
        public PaletteRelativeAlign ContextTextAlign
        {
            get => _contextTextAlign;

            set
            {
                if (_contextTextAlign != value)
                {
                    _contextTextAlign = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the ContextTextAlign to the default value.
        /// </summary>
        public void ResetContextTextAlign() => ContextTextAlign = PaletteRelativeAlign.Inherit;

        /// <summary>
        /// Gets the text alignment for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public PaletteRelativeAlign GetRibbonContextTextAlign(PaletteState state) =>
            ContextTextAlign != PaletteRelativeAlign.Inherit
                ? ContextTextAlign
                : _inherit.GetRibbonContextTextAlign(state);

        #endregion

        #region ContextTextFont
        /// <summary>
        /// Gets and sets the font for the ribbon context text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Font for the ribbon context text.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Font ContextTextFont
        {
            get => _contextTextFont;

            set
            {
                if (_contextTextFont != value)
                {
                    _contextTextFont = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the ContextTextFont to the default value.
        /// </summary>
        public void ResetContextTextFont() => ContextTextFont = null;

        /// <summary>
        /// Gets the font for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public Font GetRibbonContextTextFont(PaletteState state) => ContextTextFont ?? _inherit.GetRibbonContextTextFont(state);

        #endregion

        #region ContextTextColor
        /// <summary>
        /// Gets and sets the text color used for ribbon context text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Color used for ribbon context text.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color ContextTextColor
        {
            get => _contextTextColor;

            set
            {
                if (_contextTextColor != value)
                {
                    _contextTextColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the ContextTextColor property to its default value.
        /// </summary>
        public void ResetContextTextColor() => ContextTextColor = Color.Empty;

        /// <summary>
        /// Gets the color of the ribbon caption text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonContextTextColor(PaletteState state) => DisabledDark != Color.Empty
            ? ContextTextColor
            : _inherit.GetRibbonContextTextColor(state);

        #endregion

        #region DisabledDark
        /// <summary>
        /// Gets access to dark disabled color used for ribbon glyphs.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Dark disabled color for ribbon glyphs.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color DisabledDark
        {
            get => _disabledDarkColor;

            set
            {
                if (_disabledDarkColor != value)
                {
                    _disabledDarkColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the DisabledDark property to its default value.
        /// </summary>
        public void ResetDisabledDark() => DisabledDark = Color.Empty;

        /// <summary>
        /// Gets the dark disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonDisabledDark(PaletteState state) =>
            DisabledDark != Color.Empty ? DisabledDark : _inherit.GetRibbonDisabledDark(state);

        #endregion

        #region DisabledLight
        /// <summary>
        /// Gets access to light disabled color used for ribbon glyphs.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Light disabled color for ribbon glyphs.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color DisabledLight
        {
            get => _disabledLightColor;

            set
            {
                if (_disabledLightColor != value)
                {
                    _disabledLightColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the DisabledLight property to its default value.
        /// </summary>
        public void ResetDisabledLight() => DisabledLight = Color.Empty;

        /// <summary>
        /// Gets the light disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonDisabledLight(PaletteState state) =>
            DisabledLight != Color.Empty ? DisabledLight : _inherit.GetRibbonDisabledLight(state);

        #endregion

        #region GroupDialogDark
        /// <summary>
        /// Gets access to ribbon dialog launcher button dark color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Ribbon group dialog launcher button dark color.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color GroupDialogDark
        {
            get => _dialogDarkColor;

            set
            {
                if (_dialogDarkColor != value)
                {
                    _dialogDarkColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the GroupDialogDark property to its default value.
        /// </summary>
        public void ResetGroupDialogDark() => GroupDialogDark = Color.Empty;

        /// <summary>
        /// Gets the color for the dialog launcher dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonGroupDialogDark(PaletteState state) => GroupDialogDark != Color.Empty
            ? GroupDialogDark
            : _inherit.GetRibbonGroupDialogDark(state);

        #endregion

        #region GroupDialogLight
        /// <summary>
        /// Gets access to ribbon group dialog launcher button light color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Ribbon group dialog launcher button light color.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color GroupDialogLight
        {
            get => _dialogLightColor;

            set
            {
                if (_dialogLightColor != value)
                {
                    _dialogLightColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the GroupDialogLight property to its default value.
        /// </summary>
        public void ResetGroupDialogLight() => GroupDialogLight = Color.Empty;

        /// <summary>
        /// Gets the color for the dialog launcher light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonGroupDialogLight(PaletteState state) => GroupDialogLight != Color.Empty
            ? GroupDialogLight
            : _inherit.GetRibbonGroupDialogLight(state);

        #endregion

        #region DropArrowDark
        /// <summary>
        /// Gets access to ribbon drop arrow dark color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Ribbon drop arrow dark color.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color DropArrowDark
        {
            get => _dropArrowDarkColor;

            set
            {
                if (_dropArrowDarkColor != value)
                {
                    _dropArrowDarkColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the DropArrowDark property to its default value.
        /// </summary>
        public void ResetDropArrowDark() => DropArrowDark = Color.Empty;

        /// <summary>
        /// Gets the color for the drop arrow dark color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonDropArrowDark(PaletteState state) =>
            DropArrowDark != Color.Empty ? DropArrowDark : _inherit.GetRibbonDropArrowDark(state);

        #endregion

        #region DropArrowLight
        /// <summary>
        /// Gets access to ribbon drop arrow light color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Ribbon drop arrow light color.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color DropArrowLight
        {
            get => _dropArrowLightColor;

            set
            {
                if (_dropArrowLightColor != value)
                {
                    _dropArrowLightColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the DropArrowLight property to its default value.
        /// </summary>
        public void ResetDropArrowLight() => DropArrowLight = Color.Empty;

        /// <summary>
        /// Gets the color for the drop arrow light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonDropArrowLight(PaletteState state) => DropArrowLight != Color.Empty
            ? DropArrowLight
            : _inherit.GetRibbonDropArrowLight(state);

        #endregion

        #region GroupSeparatorDark
        /// <summary>
        /// Gets access to ribbon group separator dark color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Ribbon group separator dark color.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color GroupSeparatorDark
        {
            get => _groupSeparatorDark;

            set
            {
                if (_groupSeparatorDark != value)
                {
                    _groupSeparatorDark = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the GroupDialogDark property to its default value.
        /// </summary>
        public void ResetGroupSeparatorDark() => GroupSeparatorDark = Color.Empty;

        /// <summary>
        /// Gets the color for the dialog launcher dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonGroupSeparatorDark(PaletteState state) => GroupSeparatorDark != Color.Empty
            ? GroupSeparatorDark
            : _inherit.GetRibbonGroupSeparatorDark(state);

        #endregion

        #region GroupSeparatorLight
        /// <summary>
        /// Gets access to ribbon group separator light color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Ribbon group separator light color.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color GroupSeparatorLight
        {
            get => _groupSeparatorLight;

            set
            {
                if (_groupSeparatorLight != value)
                {
                    _groupSeparatorLight = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the GroupSeparatorLight property to its default value.
        /// </summary>
        public void ResetGroupSeparatorLight() => GroupDialogLight = Color.Empty;

        /// <summary>
        /// Gets the color for the dialog launcher light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonGroupSeparatorLight(PaletteState state) => GroupSeparatorLight != Color.Empty
            ? GroupSeparatorLight
            : _inherit.GetRibbonGroupSeparatorLight(state);

        #endregion

        #region MinimizeBarDarkColor
        /// <summary>
        /// Gets access to ribbon minimize bar dark color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Ribbon minimize bar dark color.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color MinimizeBarDarkColor
        {
            get => _minimizeBarDarkColor;

            set
            {
                if (_minimizeBarDarkColor != value)
                {
                    _minimizeBarDarkColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the MinimizeBarDarkColor property to its default value.
        /// </summary>
        public void ResetMinimizeBarDarkColor() => MinimizeBarDarkColor = Color.Empty;

        /// <summary>
        /// Gets the color for the ribbon minimize bar dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonMinimizeBarDark(PaletteState state) => MinimizeBarDarkColor != Color.Empty
            ? MinimizeBarDarkColor
            : _inherit.GetRibbonMinimizeBarDark(state);

        #endregion

        #region MinimizeBarLightColor
        /// <summary>
        /// Gets access to ribbon minimize bar light color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Ribbon minimize bar light color.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color MinimizeBarLightColor
        {
            get => _minimizeBarLightColor;

            set
            {
                if (_minimizeBarLightColor != value)
                {
                    _minimizeBarLightColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the MinimizeBarLightColor property to its default value.
        /// </summary>
        public void ResetMinimizeBarLightColor() => MinimizeBarLightColor = Color.Empty;

        /// <summary>
        /// Gets the color for the ribbon minimize bar light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonMinimizeBarLight(PaletteState state) => MinimizeBarLightColor != Color.Empty
            ? MinimizeBarLightColor
            : _inherit.GetRibbonMinimizeBarLight(state);

        #endregion

        #region RibbonShape
        /// <summary>
        /// Gets access to ribbon shape.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Ribbon shape.")]
        [DefaultValue(typeof(PaletteRibbonShape), "Inherit")]
        public PaletteRibbonShape RibbonShape
        {
            get => _ribbonShape;

            set
            {
                if (_ribbonShape != value)
                {
                    _ribbonShape = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the RibbonShape property to its default value.
        /// </summary>
        public void ResetRibbonShape() => RibbonShape = PaletteRibbonShape.Inherit;

        /// <summary>
        /// Gets the ribbon shape.
        /// </summary>
        /// <returns>Color value.</returns>
        public PaletteRibbonShape GetRibbonShape() =>
            RibbonShape != PaletteRibbonShape.Inherit ? RibbonShape : _inherit.GetRibbonShape();

        #endregion

        #region TabSeparatorColor
        /// <summary>
        /// Gets access to ribbon tab separator color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Ribbon tab separator color.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color TabSeparatorColor
        {
            get => _tabSeparatorColor;

            set
            {
                if (_tabSeparatorColor != value)
                {
                    _tabSeparatorColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the TabSeparatorColor property to its default value.
        /// </summary>
        public void ResetTabSeparatorColor() => TabSeparatorColor = Color.Empty;

        /// <summary>
        /// Gets the color for the tab separator.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonTabSeparatorColor(PaletteState state) => TabSeparatorColor != Color.Empty
            ? TabSeparatorColor
            : _inherit.GetRibbonTabSeparatorColor(state);

        #endregion

        #region TabSeparatorContextColor
        /// <summary>
        /// Gets access to ribbon context tab separator color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Ribbon tab context separator color.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color TabSeparatorContextColor
        {
            get => _tabSeparatorContextColor;

            set
            {
                if (_tabSeparatorContextColor != value)
                {
                    _tabSeparatorContextColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the TabSeparatorContextColor property to its default value.
        /// </summary>
        public void ResetTabSeparatorContextColor() => TabSeparatorContextColor = Color.Empty;

        /// <summary>
        /// Gets the color for the tab context separator.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonTabSeparatorContextColor(PaletteState state) => 
            TabSeparatorColor != Color.Empty
            ? TabSeparatorContextColor
            : _inherit.GetRibbonTabSeparatorContextColor(state);

        #endregion

        #region TextFont
        /// <summary>
        /// Gets and sets the font for the ribbon text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Font for the ribbon text.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Font TextFont
        {
            get => _textFont;

            set
            {
                if (_textFont != value)
                {
                    _textFont = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the TextFont to the default value.
        /// </summary>
        public void ResetTextFont() => TextFont = null;

        /// <summary>
        /// Gets the font for the ribbon text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public Font GetRibbonTextFont(PaletteState state) => TextFont ?? _inherit.GetRibbonTextFont(state);

        #endregion

        #region TextHint
        /// <summary>
        /// Gets and sets the rendering hint for the text font.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Rendering hint for the text font.")]
        [DefaultValue(typeof(PaletteTextHint), "Inherit")]
        [RefreshProperties(RefreshProperties.All)]
        public PaletteTextHint TextHint
        {
            get => _textHint;

            set
            {
                if (_textHint != value)
                {
                    _textHint = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the TextHint to the default value.
        /// </summary>
        public void ResetTextHint() => TextHint = PaletteTextHint.Inherit;

        /// <summary>
        /// Gets the rendering hint for the ribbon font.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public PaletteTextHint GetRibbonTextHint(PaletteState state) =>
            TextHint != PaletteTextHint.Inherit ? TextHint : _inherit.GetRibbonTextHint(state);

        #endregion

        #region QATButtonDarkColor
        /// <summary>
        /// Gets access to extra QAT extra button dark content color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Quick access toolbar extra button dark color.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color QATButtonDarkColor
        {
            get => _qatButtonDarkColor;

            set
            {
                if (_qatButtonDarkColor != value)
                {
                    _qatButtonDarkColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the QATButtonDarkColor property to its default value.
        /// </summary>
        public void ResetQATButtonDarkColor() => QATButtonDarkColor = Color.Empty;

        /// <summary>
        /// Gets the color for the extra QAT button dark content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonQATButtonDark(PaletteState state) => QATButtonDarkColor != Color.Empty
            ? QATButtonDarkColor
            : _inherit.GetRibbonQATButtonDark(state);

        #endregion

        #region QATButtonLightColor
        /// <summary>
        /// Gets access to extra QAT extra button light content color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Quick access toolbar extra button light color.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color QATButtonLightColor
        {
            get => _qatButtonLightColor;

            set
            {
                if (_qatButtonLightColor != value)
                {
                    _qatButtonLightColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the QATButtonLightColor property to its default value.
        /// </summary>
        public void ResetQATButtonLightColor() => QATButtonLightColor = Color.Empty;

        /// <summary>
        /// Gets the color for the extra QAT button light content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonQATButtonLight(PaletteState state) => QATButtonLightColor != Color.Empty
            ? QATButtonLightColor
            : _inherit.GetRibbonQATButtonLight(state);

        #endregion
    }
}
