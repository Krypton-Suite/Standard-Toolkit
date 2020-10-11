// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System.Windows.Forms;
using System.ComponentModel;
using Krypton.Toolkit;

namespace Krypton.Navigator
{
    /// <summary>
    /// Implement redirected storage for common navigator appearance.
    /// </summary>
    public class PaletteNavigatorRedirect : PaletteDoubleMetricRedirect
    {
        #region Instance Fields

        private readonly PaletteBorderInheritRedirect _paletteBorderEdgeInheritRedirect;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteNavigatorNormabled class.
        /// </summary>
        /// <param name="navigator">Reference to owning navigator.</param>
        /// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteNavigatorRedirect(KryptonNavigator navigator,
                                        PaletteRedirect redirect,
                                        NeedPaintHandler needPaint)
            : this(navigator, redirect, redirect, redirect, 
                              redirect, redirect, redirect,
                              redirect, redirect, redirect,
                              redirect, redirect, redirect,
                              redirect, redirect, redirect,
                              redirect, needPaint)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteNavigatorNormabled class.
        /// </summary>
        /// <param name="navigator">Reference to owning navigator.</param>
        /// <param name="redirectNavigator">Inheritence redirection for navigator level.</param>
        /// <param name="redirectNavigatorPage">Inheritence redirection for page level.</param>
        /// <param name="redirectNavigatorHeaderGroup">Inheritence redirection for header groups level.</param>
        /// <param name="redirectNavigatorHeaderPrimary">Inheritence redirection for primary header.</param>
        /// <param name="redirectNavigatorHeaderSecondary">Inheritence redirection for secondary header.</param>
        /// <param name="redirectNavigatorHeaderBar">Inheritence redirection for bar header.</param>
        /// <param name="redirectNavigatorHeaderOverflow">Inheritence redirection for bar header.</param>
        /// <param name="redirectNavigatorCheckButton">Inheritence redirection for check button.</param>
        /// <param name="redirectNavigatorOverflowButton">Inheritence redirection for overflow button.</param>
        /// <param name="redirectNavigatorMiniButton">Inheritence redirection for check button.</param>
        /// <param name="redirectNavigatorBar">Inheritence redirection for bar.</param>
        /// <param name="redirectNavigatorBorderEdge">Inheritence redirection for border edge.</param>
        /// <param name="redirectNavigatorSeparator">Inheritence redirection for separator.</param>
        /// <param name="redirectNavigatorTab">Inheritence redirection for tab.</param>
        /// <param name="redirectNavigatorRibbonTab">Inheritence redirection for ribbon tab.</param>
        /// <param name="redirectNavigatorRibbonGeneral">Inheritence redirection for ribbon general.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteNavigatorRedirect(KryptonNavigator navigator,
                                        PaletteRedirect redirectNavigator,
                                        PaletteRedirect redirectNavigatorPage,
                                        PaletteRedirect redirectNavigatorHeaderGroup,
                                        PaletteRedirect redirectNavigatorHeaderPrimary,
                                        PaletteRedirect redirectNavigatorHeaderSecondary,
                                        PaletteRedirect redirectNavigatorHeaderBar,
                                        PaletteRedirect redirectNavigatorHeaderOverflow,
                                        PaletteRedirect redirectNavigatorCheckButton,
                                        PaletteRedirect redirectNavigatorOverflowButton,
                                        PaletteRedirect redirectNavigatorMiniButton,
                                        PaletteRedirect redirectNavigatorBar,
                                        PaletteRedirect redirectNavigatorBorderEdge,
                                        PaletteRedirect redirectNavigatorSeparator,
                                        PaletteRedirect redirectNavigatorTab,
                                        PaletteRedirect redirectNavigatorRibbonTab,
                                        PaletteRedirect redirectNavigatorRibbonGeneral,
                                        NeedPaintHandler needPaint)
            : base(redirectNavigator, PaletteBackStyle.PanelClient,
                   PaletteBorderStyle.ControlClient, needPaint)
        {
            // Create the palette storage
            PalettePage = new PalettePageRedirect(redirectNavigatorPage, needPaint);
            HeaderGroup = new PaletteNavigatorHeaderGroupRedirect(redirectNavigatorHeaderGroup, redirectNavigatorHeaderPrimary, redirectNavigatorHeaderSecondary, redirectNavigatorHeaderBar, redirectNavigatorHeaderOverflow, needPaint);
            CheckButton = new PaletteTripleRedirect(redirectNavigatorCheckButton, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, needPaint);
            OverflowButton = new PaletteTripleRedirect(redirectNavigatorOverflowButton, PaletteBackStyle.ButtonNavigatorOverflow, PaletteBorderStyle.ButtonNavigatorOverflow, PaletteContentStyle.ButtonNavigatorOverflow, needPaint);
            MiniButton = new PaletteTripleRedirect(redirectNavigatorMiniButton, PaletteBackStyle.ButtonNavigatorMini, PaletteBorderStyle.ButtonNavigatorMini, PaletteContentStyle.ButtonNavigatorMini, needPaint);
            Bar = new PaletteBarRedirect(redirectNavigatorBar, needPaint);
            _paletteBorderEdgeInheritRedirect = new PaletteBorderInheritRedirect(redirectNavigatorBorderEdge, PaletteBorderStyle.ControlClient);
            BorderEdge = new PaletteBorderEdgeRedirect(_paletteBorderEdgeInheritRedirect, needPaint);
            Separator = new PaletteSeparatorPaddingRedirect(redirectNavigatorSeparator, PaletteBackStyle.SeparatorHighInternalProfile, PaletteBorderStyle.SeparatorHighInternalProfile, needPaint);
            Tab = new PaletteTabTripleRedirect(redirectNavigatorTab, PaletteBackStyle.TabHighProfile, PaletteBorderStyle.TabHighProfile, PaletteContentStyle.TabHighProfile, needPaint);
            RibbonTab = new PaletteRibbonTabContentRedirect(redirectNavigatorRibbonTab, needPaint);
            RibbonGeneral = new PaletteRibbonGeneralNavRedirect(redirectNavigatorRibbonGeneral, needPaint);
            Metrics = new PaletteMetrics(navigator, needPaint);
        }
        #endregion

        #region RedirectBorderEdge
        /// <summary>
        /// Update the redirector for the border edge.
        /// </summary>
        public PaletteRedirect RedirectBorderEdge
        {
            set => _paletteBorderEdgeInheritRedirect.SetRedirector(value);
        }
        #endregion

        #region RedirectRibbonGeneral
        /// <summary>
        /// Update the redirector for the ribbon general.
        /// </summary>
        public PaletteRedirect RedirectRibbonGeneral
        {
            set => RibbonGeneral.SetRedirector(value);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (base.IsDefault &&
                                           PalettePage.IsDefault &&
                                           HeaderGroup.IsDefault &&
                                           CheckButton.IsDefault &&
                                           OverflowButton.IsDefault &&
                                           MiniButton.IsDefault &&
                                           Bar.IsDefault &&
                                           BorderEdge.IsDefault &&
                                           Separator.IsDefault &&
                                           Tab.IsDefault &&
                                           RibbonTab.IsDefault &&
                                           RibbonGeneral.IsDefault &&
                                           Metrics.IsDefault);

        #endregion

        #region Bar
        /// <summary>
        /// Gets access to the bar appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining bar appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBarRedirect Bar { get; }

        private bool ShouldSerializeBar()
        {
            return !Bar.IsDefault;
        }
        #endregion

        #region Back
        /// <summary>
        /// Gets access to the background palette details.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override PaletteBack Back => base.Back;


        /// <summary>
        /// Gets the background palette.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override IPaletteBack PaletteBack => base.PaletteBack;

        #endregion

        #region Border
        /// <summary>
        /// Gets access to the border palette details.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override PaletteBorder Border => base.Border;

        /// <summary>
        /// Gets the border palette.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override IPaletteBorder PaletteBorder => base.PaletteBorder;

        #endregion

        #region Panel
        /// <summary>
        /// Gets access to the panel palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining panel appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBack Panel => base.Back;

        private bool ShouldSerializePanel()
        {
            return !base.Back.IsDefault;
        }
        #endregion

        #region CheckButton
        /// <summary>
        /// Gets access to the check button appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining check button appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect CheckButton { get; }

        private bool ShouldSerializeCheckButton()
        {
            return !CheckButton.IsDefault;
        }
        #endregion

        #region OverflowButton
        /// <summary>
        /// Gets access to the outlook overflow button appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining outlook overflow button appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect OverflowButton { get; }

        private bool ShouldSerializeOverflowButton()
        {
            return !OverflowButton.IsDefault;
        }
        #endregion

        #region MiniButton
        /// <summary>
        /// Gets access to the outlook mini button appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining outlook mini button appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect MiniButton { get; }

        private bool ShouldSerializeMiniButton()
        {
            return !MiniButton.IsDefault;
        }
        #endregion

        #region HeaderGroup
        /// <summary>
        /// Gets access to the header group appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining header group appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteNavigatorHeaderGroupRedirect HeaderGroup { get; }

        private bool ShouldSerializeHeaderGroup()
        {
            return !HeaderGroup.IsDefault;
        }
        #endregion

        #region Page
        /// <summary>
        /// Gets access to the page appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining page appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBack Page => PalettePage.Back;

        private bool ShouldSerializePage()
        {
            return !PalettePage.Back.IsDefault;
        }
        #endregion

        #region BorderEdge
        /// <summary>
        /// Gets access to the border edge appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining border edge appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBorderEdgeRedirect BorderEdge { get; }

        private bool ShouldSerializeBorderEdge()
        {
            return !BorderEdge.IsDefault;
        }
        #endregion

        #region Metrics
        /// <summary>
        /// Gets access to the metrics entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining metric entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteMetrics Metrics { get; }

        private bool ShouldSerializeMetrics()
        {
            return !Metrics.IsDefault;
        }
        #endregion

        #region Separator
        /// <summary>
        /// Get access to the overrides for defining separator appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining separator appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteSeparatorPaddingRedirect Separator { get; }

        private bool ShouldSerializeSeparator()
        {
            return !Separator.IsDefault;
        }
        #endregion

        #region Tab
        /// <summary>
        /// Gets access to the tab appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining tab appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTabTripleRedirect Tab { get; }

        private bool ShouldSerializeTab()
        {
            return !Tab.IsDefault;
        }
        #endregion

        #region RibbonTab
        /// <summary>
        /// Gets access to the ribbon tab appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon tab appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonTabContentRedirect RibbonTab { get; }

        private bool ShouldSerializeRibbonTab()
        {
            return !RibbonTab.IsDefault;
        }
        #endregion

        #region RibbonGeneral
        /// <summary>
        /// Gets access to the ribbon general appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon general appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonGeneralNavRedirect RibbonGeneral { get; }

        private bool ShouldSerializeRibbonGeneral()
        {
            return !RibbonGeneral.IsDefault;
        }
        #endregion

        #region IPaletteMetric
        /// <summary>
        /// Gets an integer metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Integer value.</returns>
        public override int GetMetricInt(PaletteState state, PaletteMetricInt metric)
        {
            switch (metric)
            {
                case PaletteMetricInt.PageButtonInset:
                    if (Metrics.PageButtonSpecInset != -1)
                    {
                        return Metrics.PageButtonSpecInset;
                    }
                    break;
            }

            // Pass onto the inheritance
            return base.GetMetricInt(state, metric);
        }

        /// <summary>
        /// Gets a padding metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Padding value.</returns>
        public override Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric)
        {
            switch (metric)
            {
                case PaletteMetricPadding.PageButtonPadding:
                    if (!Metrics.PageButtonSpecPadding.Equals(CommonHelper.InheritPadding))
                    {
                        return Metrics.PageButtonSpecPadding;
                    }
                    break;
            }

            // Pass onto the inheritance
            return base.GetMetricPadding(state, metric);
        }
        #endregion

        #region Internal
        internal PalettePageRedirect PalettePage { get; }

        internal PaletteBorderStyle BorderEdgeStyle
        {
            set => _paletteBorderEdgeInheritRedirect.Style = value;
        }
        #endregion    
    }
}
