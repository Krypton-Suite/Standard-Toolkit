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
    /// Implement storage for a KryptonDataGridView normal state.
    /// </summary>
    public class PaletteDataGridViewAll : PaletteDataGridViewCells
    {
        #region Instance Fields
        private readonly PaletteDouble _background;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteDataGridViewAll class.
        /// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteDataGridViewAll(PaletteDataGridViewRedirect inherit,
                                      NeedPaintHandler needPaint)
            : base(inherit, needPaint)
        {
            Debug.Assert(inherit != null);

            // Create storage that maps onto the inherit instances
            _background = new PaletteDouble(inherit.BackgroundDouble, needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => Background.IsDefault && base.IsDefault;

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">The palette state to populate with.</param>
        /// <param name="common">Reference to common settings.</param>
        /// <param name="gridStyle">Grid style to use for populating.</param>
        public override void PopulateFromBase(KryptonPaletteCommon common,
                                              PaletteState state,
                                              GridStyle gridStyle)
        {
            base.PopulateFromBase(common, state, gridStyle);

            common.StateCommon.BackStyle = gridStyle == GridStyle.List
                ? PaletteBackStyle.GridBackgroundList
                : PaletteBackStyle.GridBackgroundSheet;

            _background.PopulateFromBase(state);
        }
        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritance parent.
        /// </summary>
        public override void SetInherit(PaletteDataGridViewRedirect inherit)
        {
            base.SetInherit(inherit);
            _background.SetInherit(inherit.BackgroundDouble);
        }
        #endregion

        #region Background
        /// <summary>
        /// Gets access to the data grid view background palette details.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining data grid view background appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteBack Background => _background.Back;

        private bool ShouldSerializeBackground() => !_background.IsDefault;

        #endregion
    }
}
