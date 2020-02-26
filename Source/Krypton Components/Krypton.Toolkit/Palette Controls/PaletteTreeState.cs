// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.500.0.0  
// *****************************************************************************

using System.ComponentModel;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for background, border and node triple.
    /// </summary>
    public class PaletteTreeState : PaletteDouble
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteTreeState class.
        /// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        /// <param name="back">Reference to back storage.</param>
        /// <param name="border">Reference to border storage.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteTreeState(PaletteTreeStateRedirect inherit,
                                PaletteBack back,
                                PaletteBorder border,
                                NeedPaintHandler needPaint)
            : base(inherit, back, border, needPaint)
        {
            Node = new PaletteTriple(inherit.Node, needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (base.IsDefault && Node.IsDefault);

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Which state to populate from.</param>
        public override void PopulateFromBase(PaletteState state)
        {
            base.PopulateFromBase(state);
            Node.PopulateFromBase(state);
        }
        #endregion

        #region Node
        /// <summary>
        /// Gets the node appearance overrides.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining node appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple Node { get; }

        private bool ShouldSerializeItem()
        {
            return !Node.IsDefault;
        }
        #endregion
    }
}
