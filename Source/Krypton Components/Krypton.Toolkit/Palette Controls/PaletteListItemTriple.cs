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
using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for a a list item triple.
    /// </summary>
    public class PaletteListItemTriple : Storage
    {
        #region Instance Fields
        private readonly PaletteTriple _paletteItem;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteListItemTriple class.
        /// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteListItemTriple(PaletteTripleRedirect inherit,
                                     NeedPaintHandler needPaint)
        {
            Debug.Assert(inherit != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Create storage that maps onto the inherit instances
            _paletteItem = new PaletteTriple(inherit, needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => _paletteItem.IsDefault;

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">The palette state to populate with.</param>
        public virtual void PopulateFromBase(PaletteState state)
        {
            _paletteItem.PopulateFromBase(state);
        }
        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public virtual void SetInherit(PaletteTripleRedirect inherit)
        {
            _paletteItem.SetInherit(inherit);
        }
        #endregion

        #region Item
        /// <summary>
        /// Gets access to the item palette details.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining item appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteTriple Item => _paletteItem;

        private bool ShouldSerializeItem()
        {
            return !_paletteItem.IsDefault;
        }
        #endregion
    }
}
