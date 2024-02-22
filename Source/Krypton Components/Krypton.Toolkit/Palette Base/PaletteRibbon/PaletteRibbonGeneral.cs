﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2024. All rights reserved. 
 *  
 */
#endregion



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
        private Font? _contextTextFont;
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
        private Color _tabRowBackgroundSolidColor;
        private Color _tabBackgroundGradientRaftingDarkColor;
        private Color _tabBackgroundGradientRaftingLightColor;
        private Color _tabRowBackgroundGradientFirstColor;
        private Color _qatButtonDarkColor;
        private Color _qatButtonLightColor;
        private Color _tabSeparatorColor;
        private Color _tabSeparatorContextColor;
        private Color _ribbonAppButtonDarkColor;
        private Color _ribbonAppButtonLightColor;
        private Color _ribbonAppButtonTextColor;
        private Font? _textFont;
        private float _ribbonTabRowGradientRaftingAngle;
        private PaletteTextHint _textHint;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRibbonGeneral class.
        /// </summary>
        /// <param name="inherit">Source for inheriting general values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteRibbonGeneral([DisallowNull] IPaletteRibbonGeneral inherit,
                                    NeedPaintHandler needPaint)
        {
            Debug.Assert(inherit != null);

            // Remember inheritance
            _inherit = inherit!;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Set default values
            _contextTextAlign = PaletteRelativeAlign.Inherit;
            _contextTextColor = GlobalStaticValues.EMPTY_COLOR;
            _contextTextFont = null;
            _disabledDarkColor = GlobalStaticValues.EMPTY_COLOR;
            _disabledLightColor = GlobalStaticValues.EMPTY_COLOR;
            _dialogDarkColor = GlobalStaticValues.EMPTY_COLOR;
            _dialogLightColor = GlobalStaticValues.EMPTY_COLOR;
            _dropArrowLightColor = GlobalStaticValues.EMPTY_COLOR;
            _dropArrowDarkColor = GlobalStaticValues.EMPTY_COLOR;
            _groupSeparatorDark = GlobalStaticValues.EMPTY_COLOR;
            _groupSeparatorLight = GlobalStaticValues.EMPTY_COLOR;
            _minimizeBarDarkColor = GlobalStaticValues.EMPTY_COLOR;
            _minimizeBarLightColor = GlobalStaticValues.EMPTY_COLOR;
            _tabBackgroundGradientRaftingDarkColor = GlobalStaticValues.EMPTY_COLOR;
            _tabBackgroundGradientRaftingLightColor = GlobalStaticValues.EMPTY_COLOR;
            _tabRowBackgroundSolidColor = GlobalStaticValues.EMPTY_COLOR;
            _tabRowBackgroundGradientFirstColor = GlobalStaticValues.TAB_ROW_GRADIENT_FIRST_COLOR;
            _ribbonTabRowGradientRaftingAngle = GlobalStaticValues.DEFAULT_RAFTING_RIBBON_TAB_BACKGROUND_GRADIENT;
            _ribbonShape = PaletteRibbonShape.Inherit;
            _tabSeparatorColor = GlobalStaticValues.EMPTY_COLOR;
            _tabSeparatorContextColor = GlobalStaticValues.EMPTY_COLOR;
            _textFont = null;
            _textHint = PaletteTextHint.Inherit;
            _qatButtonDarkColor = GlobalStaticValues.EMPTY_COLOR;
            _qatButtonLightColor = GlobalStaticValues.EMPTY_COLOR;
            _ribbonAppButtonDarkColor = GlobalStaticValues.EMPTY_COLOR;
            _ribbonAppButtonLightColor = GlobalStaticValues.EMPTY_COLOR;
            _ribbonAppButtonTextColor = GlobalStaticValues.EMPTY_COLOR;
        }
        #endregion

        #region IsDefault

        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (ContextTextAlign == PaletteRelativeAlign.Inherit) &&
                                          (ContextTextColor == GlobalStaticValues.EMPTY_COLOR) &&
                                          (ContextTextFont == null) &&
                                          (DisabledDark == GlobalStaticValues.EMPTY_COLOR) &&
                                          (DisabledLight == GlobalStaticValues.EMPTY_COLOR) &&
                                          (DropArrowLight == GlobalStaticValues.EMPTY_COLOR) &&
                                          (DropArrowDark == GlobalStaticValues.EMPTY_COLOR) &&
                                          (GroupDialogDark == GlobalStaticValues.EMPTY_COLOR) &&
                                          (GroupDialogLight == GlobalStaticValues.EMPTY_COLOR) &&
                                          (GroupSeparatorDark == GlobalStaticValues.EMPTY_COLOR) &&
                                          (GroupSeparatorLight == GlobalStaticValues.EMPTY_COLOR) &&
                                          (MinimizeBarDarkColor == GlobalStaticValues.EMPTY_COLOR) &&
                                          (MinimizeBarLightColor == GlobalStaticValues.EMPTY_COLOR) &&
                                          (TabRowBackgroundGradientRaftingDarkColor == GlobalStaticValues.EMPTY_COLOR) &&
                                          (TabRowBackgroundGradientRaftingLightColor == GlobalStaticValues.EMPTY_COLOR) &&
                                          (TabRowBackgroundSolidColor == GlobalStaticValues.EMPTY_COLOR) &&
                                          (TabRowBackgroundGradientFirstColor == GlobalStaticValues.TAB_ROW_GRADIENT_FIRST_COLOR) &&
                                          (RibbonAppButtonDarkColor == GlobalStaticValues.DEFAULT_RIBBON_APP_BUTTON_DARK_COLOR) &&
                                          (RibbonAppButtonLightColor == GlobalStaticValues.DEFAULT_RIBBON_APP_BUTTON_LIGHT_COLOR) &&
                                          (RibbonAppButtonTextColor == GlobalStaticValues.DEFAULT_RIBBON_APP_BUTTON_TEXT_COLOR) &&
                                          (RibbonTabRowGradientRaftingAngle == GlobalStaticValues.DEFAULT_RAFTING_RIBBON_TAB_BACKGROUND_GRADIENT) &&
                                          (RibbonShape == PaletteRibbonShape.Inherit) &&
                                          (TextFont == null) &&
                                          (TextHint == PaletteTextHint.Inherit) &&
                                          (TabSeparatorColor == GlobalStaticValues.EMPTY_COLOR) &&
                                          (TabSeparatorContextColor == GlobalStaticValues.EMPTY_COLOR) &&
                                          (QATButtonDarkColor == GlobalStaticValues.EMPTY_COLOR) &&
                                          (QATButtonLightColor == GlobalStaticValues.EMPTY_COLOR);

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
            TabRowBackgroundSolidColor = GetRibbonTabRowBackgroundSolidColor(PaletteState.Normal);
            TabRowBackgroundGradientRaftingDarkColor = GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState.Normal);
            TabRowBackgroundGradientRaftingLightColor = GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState.Normal);
            TabRowBackgroundGradientFirstColor = GetRibbonTabRowGradientColor1(PaletteState.Normal);
            RibbonTabRowGradientRaftingAngle = GetRibbonTabRowGradientRaftingAngle(PaletteState.Normal);
            RibbonAppButtonDarkColor = GetRibbonAppButtonDarkColor(PaletteState.Normal);
            RibbonAppButtonLightColor = GetRibbonAppButtonLightColor(PaletteState.Normal);
            RibbonAppButtonTextColor = GetRibbonAppButtonTextColor(PaletteState.Normal);
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
        [Category(@"Visuals")]
        [Description(@"Text alignment for the ribbon context text.")]
        [DefaultValue(PaletteRelativeAlign.Inherit)]
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
        [Category(@"Visuals")]
        [Description(@"Font for the ribbon context text.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Font? ContextTextFont
        {
            get => _contextTextFont;

            set
            {
                if (!Equals(_contextTextFont, value))
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
        [Category(@"Visuals")]
        [Description(@"Color used for ribbon context text.")]
        [DefaultValue(typeof(Color), "Empty")]
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
        public void ResetContextTextColor() => ContextTextColor = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the color of the ribbon caption text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonContextTextColor(PaletteState state) => DisabledDark != GlobalStaticValues.EMPTY_COLOR
            ? ContextTextColor
            : _inherit.GetRibbonContextTextColor(state);

        #endregion

        #region DisabledDark
        /// <summary>
        /// Gets access to dark disabled color used for ribbon glyphs.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Dark disabled color for ribbon glyphs.")]
        [DefaultValue(typeof(Color), "Empty")]
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
        public void ResetDisabledDark() => DisabledDark = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the dark disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonDisabledDark(PaletteState state) =>
            DisabledDark != GlobalStaticValues.EMPTY_COLOR ? DisabledDark : _inherit.GetRibbonDisabledDark(state);

        #endregion

        #region DisabledLight
        /// <summary>
        /// Gets access to light disabled color used for ribbon glyphs.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Light disabled color for ribbon glyphs.")]
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
        public void ResetDisabledLight() => DisabledLight = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the light disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonDisabledLight(PaletteState state) =>
            DisabledLight != GlobalStaticValues.EMPTY_COLOR ? DisabledLight : _inherit.GetRibbonDisabledLight(state);

        #endregion

        #region GroupDialogDark
        /// <summary>
        /// Gets access to ribbon dialog launcher button dark color.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon group dialog launcher button dark color.")]
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
        public void ResetGroupDialogDark() => GroupDialogDark = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the color for the dialog launcher dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonGroupDialogDark(PaletteState state) => GroupDialogDark != GlobalStaticValues.EMPTY_COLOR
            ? GroupDialogDark
            : _inherit.GetRibbonGroupDialogDark(state);

        #endregion

        #region GroupDialogLight
        /// <summary>
        /// Gets access to ribbon group dialog launcher button light color.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon group dialog launcher button light color.")]
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
        public void ResetGroupDialogLight() => GroupDialogLight = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the color for the dialog launcher light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonGroupDialogLight(PaletteState state) => GroupDialogLight != GlobalStaticValues.EMPTY_COLOR
            ? GroupDialogLight
            : _inherit.GetRibbonGroupDialogLight(state);

        #endregion

        #region DropArrowDark
        /// <summary>
        /// Gets access to ribbon drop arrow dark color.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon drop arrow dark color.")]
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
        public void ResetDropArrowDark() => DropArrowDark = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the color for the drop arrow dark color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonDropArrowDark(PaletteState state) =>
            DropArrowDark != GlobalStaticValues.EMPTY_COLOR ? DropArrowDark : _inherit.GetRibbonDropArrowDark(state);

        #endregion

        #region DropArrowLight
        /// <summary>
        /// Gets access to ribbon drop arrow light color.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon drop arrow light color.")]
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
        public void ResetDropArrowLight() => DropArrowLight = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the color for the drop arrow light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonDropArrowLight(PaletteState state) => DropArrowLight != GlobalStaticValues.EMPTY_COLOR
            ? DropArrowLight
            : _inherit.GetRibbonDropArrowLight(state);

        #endregion

        #region GroupSeparatorDark
        /// <summary>
        /// Gets access to ribbon group separator dark color.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon group separator dark color.")]
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
        public void ResetGroupSeparatorDark() => GroupSeparatorDark = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the color for the dialog launcher dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonGroupSeparatorDark(PaletteState state) => GroupSeparatorDark != GlobalStaticValues.EMPTY_COLOR
            ? GroupSeparatorDark
            : _inherit.GetRibbonGroupSeparatorDark(state);

        #endregion

        #region GroupSeparatorLight
        /// <summary>
        /// Gets access to ribbon group separator light color.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon group separator light color.")]
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
        public void ResetGroupSeparatorLight() => GroupDialogLight = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the color for the dialog launcher light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonGroupSeparatorLight(PaletteState state) => GroupSeparatorLight != GlobalStaticValues.EMPTY_COLOR
            ? GroupSeparatorLight
            : _inherit.GetRibbonGroupSeparatorLight(state);

        #endregion

        #region MinimizeBarDarkColor
        /// <summary>
        /// Gets access to ribbon minimize bar dark color.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon minimize bar dark color.")]
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
        public void ResetMinimizeBarDarkColor() => MinimizeBarDarkColor = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the color for the ribbon minimize bar dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonMinimizeBarDark(PaletteState state) => MinimizeBarDarkColor != GlobalStaticValues.EMPTY_COLOR
            ? MinimizeBarDarkColor
            : _inherit.GetRibbonMinimizeBarDark(state);

        #endregion

        #region MinimizeBarLightColor
        /// <summary>
        /// Gets access to ribbon minimize bar light color.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon minimize bar light color.")]
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
        public void ResetMinimizeBarLightColor() => MinimizeBarLightColor = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the color for the ribbon minimize bar light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonMinimizeBarLight(PaletteState state) => MinimizeBarLightColor != GlobalStaticValues.EMPTY_COLOR
            ? MinimizeBarLightColor
            : _inherit.GetRibbonMinimizeBarLight(state);

        #endregion

        #region TabRowBackgroundSolidColor

        /// <summary>
        /// Gets access to ribbon tab row solid color.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon tab row background solid color.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color TabRowBackgroundSolidColor
        {
            get => _tabRowBackgroundSolidColor;

            set
            {
                if (_tabRowBackgroundSolidColor != value)
                {
                    _tabRowBackgroundSolidColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the TabRowBackgroundSolidColor property to its default value.
        /// </summary>
        public void ResetTabRowBackgroundSolidColor() => TabRowBackgroundSolidColor = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the solid color for the ribbon tab row.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => TabRowBackgroundSolidColor != GlobalStaticValues.EMPTY_COLOR
            ? TabRowBackgroundSolidColor
            : _inherit.GetRibbonTabRowBackgroundSolidColor(state);

        #endregion

        #region TabRowBackgroundGradientRaftingDarkColor

        /// <summary>
        /// Gets access to ribbon tab row gradient dark rafting color.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon tab row background gradient dark rafting color.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color TabRowBackgroundGradientRaftingDarkColor
        {
            get => _tabBackgroundGradientRaftingDarkColor;

            set
            {
                if (_tabBackgroundGradientRaftingDarkColor != value)
                {
                    _tabBackgroundGradientRaftingDarkColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the TabRowBackgroundGradientRaftingDarkColor property to its default value.
        /// </summary>
        public void ResetTabRowBackgroundGradientRaftingDarkColor() => TabRowBackgroundGradientRaftingDarkColor = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the dark Gradient rafting color for the ribbon tab row.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) => TabRowBackgroundGradientRaftingDarkColor != GlobalStaticValues.EMPTY_COLOR
            ? TabRowBackgroundGradientRaftingDarkColor
            : _inherit.GetRibbonTabRowBackgroundGradientRaftingDark(state);

        #endregion

        #region TabRowBackgroundGradientRaftingLightColor

        /// <summary>
        /// Gets access to ribbon tab row gradient light rafting color.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon tab row background gradient light rafting color.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        public Color TabRowBackgroundGradientRaftingLightColor
        {
            get => _tabBackgroundGradientRaftingLightColor;

            set
            {
                if (_tabBackgroundGradientRaftingLightColor != value)
                {
                    _tabBackgroundGradientRaftingLightColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the TabRowBackgroundGradientRaftingLightColor property to its default value.
        /// </summary>
        public void ResetTabRowBackgroundGradientRaftingLightColor() => TabRowBackgroundGradientRaftingLightColor = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the light rafting color for the ribbon tab row.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) => TabRowBackgroundGradientRaftingLightColor != GlobalStaticValues.EMPTY_COLOR
            ? TabRowBackgroundGradientRaftingLightColor
            : _inherit.GetRibbonTabRowBackgroundGradientRaftingLight(state);

        #endregion

        #region TabRowBackgroundGradientFirstColor

        /// <summary>
        /// Gets access to ribbon tab row gradient first color.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon tab row background gradient first color.")]
        [DefaultValue(typeof(Color), "Color.Transparent")]
        [RefreshProperties(RefreshProperties.All)]
        public Color TabRowBackgroundGradientFirstColor
        {
            get => _tabRowBackgroundGradientFirstColor;

            set
            {
                if (_tabRowBackgroundGradientFirstColor != value)
                {
                    _tabRowBackgroundGradientFirstColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the TabRowBackgroundGradientFirstColor property to its default value.
        /// </summary>
        public void ResetTabRowBackgroundGradientFirstColor() => TabRowBackgroundGradientFirstColor = GlobalStaticValues.TAB_ROW_GRADIENT_FIRST_COLOR;

        /// <inheritdoc />
        public Color GetRibbonTabRowGradientColor1(PaletteState state) => TabRowBackgroundGradientFirstColor != GlobalStaticValues.TAB_ROW_GRADIENT_FIRST_COLOR
            ? TabRowBackgroundGradientFirstColor
            : _inherit.GetRibbonTabRowGradientColor1(state);

        #endregion

        #region RibbonAppButtonDarkColor

        /// <summary>
        /// Gets access to ribbon app button dark color.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon app button dark color.")]
        [DefaultValue(typeof(Color), "Color.Empty")]
        [RefreshProperties(RefreshProperties.All)]
        public Color RibbonAppButtonDarkColor
        {
            get => _ribbonAppButtonDarkColor;

            set
            {
                if (_ribbonAppButtonDarkColor != value)
                {
                    _ribbonAppButtonDarkColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the RibbonAppButtonDarkColor property to its default value.
        /// </summary>
        public void ResetRibbonAppButtonDarkColor() => RibbonAppButtonDarkColor = GlobalStaticValues.DEFAULT_RIBBON_APP_BUTTON_DARK_COLOR;

        /// <inheritdoc />
        public Color GetRibbonAppButtonDarkColor(PaletteState state) => RibbonAppButtonDarkColor != GlobalStaticValues.DEFAULT_RIBBON_APP_BUTTON_DARK_COLOR
            ? RibbonAppButtonDarkColor
            : _inherit.GetRibbonAppButtonDarkColor(state);

        #endregion

        #region RibbonAppButtonLightColor

        /// <summary>
        /// Gets access to ribbon app button light color.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon app button light color.")]
        [DefaultValue(typeof(Color), "Color.Empty")]
        [RefreshProperties(RefreshProperties.All)]
        public Color RibbonAppButtonLightColor
        {
            get => _ribbonAppButtonLightColor;

            set
            {
                if (_ribbonAppButtonLightColor != value)
                {
                    _ribbonAppButtonLightColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the RibbonAppButtonLightColor property to its default value.
        /// </summary>
        public void ResetRibbonAppButtonLightColor() => RibbonAppButtonLightColor = GlobalStaticValues.DEFAULT_RIBBON_APP_BUTTON_LIGHT_COLOR;

        /// <inheritdoc />
        public Color GetRibbonAppButtonLightColor(PaletteState state) => RibbonAppButtonLightColor != GlobalStaticValues.DEFAULT_RIBBON_APP_BUTTON_LIGHT_COLOR
            ? RibbonAppButtonLightColor
            : _inherit.GetRibbonAppButtonLightColor(state);

        #endregion

        #region RibbonAppButtonTextColor

        /// <summary>
        /// Gets access to ribbon app button text color.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon app button text color.")]
        [DefaultValue(typeof(Color), "Color.Empty")]
        [RefreshProperties(RefreshProperties.All)]
        public Color RibbonAppButtonTextColor
        {
            get => _ribbonAppButtonTextColor;

            set
            {
                if (_ribbonAppButtonTextColor != value)
                {
                    _ribbonAppButtonTextColor = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the RibbonAppButtonTextColor property to its default value.
        /// </summary>
        public void ResetRibbonAppButtonTextColor() => RibbonAppButtonTextColor = GlobalStaticValues.DEFAULT_RIBBON_APP_BUTTON_TEXT_COLOR;

        /// <inheritdoc />
        public Color GetRibbonAppButtonTextColor(PaletteState state) => RibbonAppButtonTextColor != GlobalStaticValues.DEFAULT_RIBBON_APP_BUTTON_TEXT_COLOR
            ? RibbonAppButtonTextColor
            : _inherit.GetRibbonAppButtonTextColor(state);

        #endregion

        #region RibbonTabRowGradientRaftingAngle

        /// <summary>
        /// Gets access to ribbon tab row gradient rafting angle.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon tab row background gradient rafting angle.")]
        [DefaultValue(GlobalStaticValues.DEFAULT_RAFTING_RIBBON_TAB_BACKGROUND_GRADIENT)]
        [RefreshProperties(RefreshProperties.All)]
        public float RibbonTabRowGradientRaftingAngle
        {
            get => _ribbonTabRowGradientRaftingAngle;

            set
            {
                if (!_ribbonTabRowGradientRaftingAngle.Equals(value))
                {
                    _ribbonTabRowGradientRaftingAngle = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the RibbonTabRowGradientRaftingAngle property to its default value.
        /// </summary>
        public void ResetRibbonTabRowGradientRaftingAngle() => RibbonTabRowGradientRaftingAngle =
            GlobalStaticValues.DEFAULT_RAFTING_RIBBON_TAB_BACKGROUND_GRADIENT;

        /// <summary>
        /// Gets the rafting angle for the ribbon tab row.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Rafting angle value.</returns>
        public float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => RibbonTabRowGradientRaftingAngle !=
            GlobalStaticValues.DEFAULT_RAFTING_RIBBON_TAB_BACKGROUND_GRADIENT
            ? RibbonTabRowGradientRaftingAngle
            : _inherit.GetRibbonTabRowGradientRaftingAngle(state);

        #endregion

        #region RibbonShape
        /// <summary>
        /// Gets access to ribbon shape.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon shape.")]
        [DefaultValue(PaletteRibbonShape.Inherit)]
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
        [Category(@"Visuals")]
        [Description(@"Ribbon tab separator color.")]
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
        public void ResetTabSeparatorColor() => TabSeparatorColor = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the color for the tab separator.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonTabSeparatorColor(PaletteState state) => TabSeparatorColor != GlobalStaticValues.EMPTY_COLOR
            ? TabSeparatorColor
            : _inherit.GetRibbonTabSeparatorColor(state);

        #endregion

        #region TabSeparatorContextColor
        /// <summary>
        /// Gets access to ribbon context tab separator color.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Ribbon tab context separator color.")]
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
        public void ResetTabSeparatorContextColor() => TabSeparatorContextColor = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the color for the tab context separator.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonTabSeparatorContextColor(PaletteState state) => 
            TabSeparatorColor != GlobalStaticValues.EMPTY_COLOR
            ? TabSeparatorContextColor
            : _inherit.GetRibbonTabSeparatorContextColor(state);

        #endregion

        #region TextFont
        /// <summary>
        /// Gets and sets the font for the ribbon text.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Font for the ribbon text.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Font? TextFont
        {
            get => _textFont;

            set
            {
                if (!Equals(_textFont, value))
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
        [Category(@"Visuals")]
        [Description(@"Rendering hint for the text font.")]
        [DefaultValue(PaletteTextHint.Inherit)]
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
        [Category(@"Visuals")]
        [Description(@"Quick access toolbar extra button dark color.")]
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
        public void ResetQATButtonDarkColor() => QATButtonDarkColor = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the color for the extra QAT button dark content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonQATButtonDark(PaletteState state) => QATButtonDarkColor != GlobalStaticValues.EMPTY_COLOR
            ? QATButtonDarkColor
            : _inherit.GetRibbonQATButtonDark(state);

        #endregion

        #region QATButtonLightColor
        /// <summary>
        /// Gets access to extra QAT extra button light content color.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"Quick access toolbar extra button light color.")]
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
        public void ResetQATButtonLightColor() => QATButtonLightColor = GlobalStaticValues.EMPTY_COLOR;

        /// <summary>
        /// Gets the color for the extra QAT button light content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonQATButtonLight(PaletteState state) => QATButtonLightColor != GlobalStaticValues.EMPTY_COLOR
            ? QATButtonLightColor
            : _inherit.GetRibbonQATButtonLight(state);

        #endregion
    }
}
