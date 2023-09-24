﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for bread crumb appearance states.
    /// </summary>
    public class PaletteBreadCrumbDoubleState : PaletteDouble
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteBreadCrumbDoubleState class.
        /// </summary>
        /// <param name="redirect">inheritance redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteBreadCrumbDoubleState(PaletteBreadCrumbRedirect? redirect,
                                            NeedPaintHandler needPaint) 
            : base(redirect, needPaint) =>
            BreadCrumb = new PaletteTriple(redirect.BreadCrumb, needPaint);

        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => base.IsDefault && BreadCrumb.IsDefault;

        #endregion

        #region BreadCrumb
        /// <summary>
        /// Gets access to the bread crumb appearance entries.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining bread crumb appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple? BreadCrumb { get; }

        private bool ShouldSerializeBreadCrumb() => !BreadCrumb.IsDefault;

        #endregion
    }
}
