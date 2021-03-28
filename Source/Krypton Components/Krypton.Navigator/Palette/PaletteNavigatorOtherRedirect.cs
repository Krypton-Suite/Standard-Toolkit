#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  Version 6.0.0  
 *
 */
#endregion

using System.ComponentModel;
using Krypton.Toolkit;

namespace Krypton.Navigator
{
    /// <summary>
    /// Implement redirected storage for other navigator appearance states.
    /// </summary>
    public class PaletteNavigatorOtherRedirect : Storage
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteNavigatorOtherRedirect class.
        /// </summary>
        /// <param name="redirectCheckButton">Inheritence redirection instance for the check button.</param>
        /// <param name="redirectOverflowButton">Inheritence redirection instance for the outlook overflow button.</param>
        /// <param name="redirectMiniButton">Inheritence redirection instance for the outlook mini button.</param>
        /// <param name="redirectTab">Inheritence redirection instance for the tab.</param>
        /// <param name="redirectRibbonTab">Inheritence redirection instance for the ribbon tab.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteNavigatorOtherRedirect(PaletteRedirect redirectCheckButton,
                                             PaletteRedirect redirectOverflowButton,
                                             PaletteRedirect redirectMiniButton,
                                             PaletteRedirect redirectTab,
                                             PaletteRedirect redirectRibbonTab,
                                             NeedPaintHandler needPaint) 
        {
            // Create the palette storage
            CheckButton = new PaletteTripleRedirect(redirectCheckButton, 
                                                            PaletteBackStyle.ButtonStandalone,
                                                            PaletteBorderStyle.ButtonStandalone,
                                                            PaletteContentStyle.ButtonStandalone,
                                                            needPaint);

            OverflowButton = new PaletteTripleRedirect(redirectCheckButton,
                                                               PaletteBackStyle.ButtonNavigatorOverflow,
                                                               PaletteBorderStyle.ButtonNavigatorOverflow,
                                                               PaletteContentStyle.ButtonNavigatorOverflow,
                                                               needPaint);

            MiniButton = new PaletteTripleRedirect(redirectMiniButton,
                                                            PaletteBackStyle.ButtonNavigatorMini,
                                                            PaletteBorderStyle.ButtonNavigatorMini,
                                                            PaletteContentStyle.ButtonNavigatorMini,
                                                            needPaint);

            Tab = new PaletteTabTripleRedirect(redirectTab,
                                                       PaletteBackStyle.TabHighProfile,
                                                       PaletteBorderStyle.TabHighProfile,
                                                       PaletteContentStyle.TabHighProfile,
                                                       needPaint);

            RibbonTab = new PaletteRibbonTabContentRedirect(redirectRibbonTab, needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (CheckButton.IsDefault &&
                                           OverflowButton.IsDefault &&
                                           MiniButton.IsDefault &&
                                           RibbonTab.IsDefault &&
                                           Tab.IsDefault);

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
    }
}
