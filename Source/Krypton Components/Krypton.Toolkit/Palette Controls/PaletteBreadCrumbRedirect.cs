// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.550.0  
// *****************************************************************************

using System.ComponentModel;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Implement redirected storage for common bread crumb appearance.
    /// </summary>
    public class PaletteBreadCrumbRedirect : PaletteDoubleMetricRedirect
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteBreadCrumbRedirect class.
        /// </summary>
        /// <param name="redirect">Inheritence redirection for bread crumb level.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteBreadCrumbRedirect(PaletteRedirect redirect,
                                         NeedPaintHandler needPaint)
            : base(redirect, PaletteBackStyle.PanelAlternate, PaletteBorderStyle.ControlClient)
        {
            BreadCrumb = new PaletteTripleRedirect(redirect, 
                                                      PaletteBackStyle.ButtonBreadCrumb,
                                                      PaletteBorderStyle.ButtonBreadCrumb,
                                                      PaletteContentStyle.ButtonBreadCrumb, 
                                                      needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (base.IsDefault && BreadCrumb.IsDefault);

        #endregion

        #region BreadCrumb
        /// <summary>
        /// Gets access to the bread crumb appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining bread crumb appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect BreadCrumb { get; }

        private bool ShouldSerializeBreadCrumb()
        {
            return !BreadCrumb.IsDefault;
        }
        #endregion  
    }
}
