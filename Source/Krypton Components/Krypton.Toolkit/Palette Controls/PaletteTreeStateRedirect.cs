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

using System.ComponentModel;
using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for back, border and tree node triple.
    /// </summary>
    public class PaletteTreeStateRedirect : PaletteDoubleRedirect
                                            
    {
        #region Instance Fields
        private PaletteRedirect _redirect;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteTreeStateRedirect class.
        /// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="back">Storage for back values.</param>
        /// <param name="backInherit">Inheritence for back values.</param>
        /// <param name="border">Storage for border values.</param>
        /// <param name="borderInherit">Inheritence for border values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteTreeStateRedirect(PaletteRedirect redirect,
                                        PaletteBack back,
                                        PaletteBackInheritRedirect backInherit,
                                        PaletteBorder border,
                                        PaletteBorderInheritRedirect borderInherit,
                                        NeedPaintHandler needPaint)
            : base(redirect, back, backInherit, border, borderInherit, needPaint)
        {
            Debug.Assert(redirect != null);

            // Remember the redirect reference
            _redirect = redirect;

            // Create the item redirector
            Node = new PaletteTripleRedirect(redirect,
                                                      PaletteBackStyle.ButtonListItem,
                                                      PaletteBorderStyle.ButtonListItem,
                                                      PaletteContentStyle.ButtonListItem,
                                                      needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (base.IsDefault && Node.IsDefault);

        #endregion

        #region Node
        /// <summary>
        /// Gets the node appearance overrides.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining node appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect Node { get; }

        private bool ShouldSerializeItem()
        {
            return !Node.IsDefault;
        }
        #endregion
    }
}
