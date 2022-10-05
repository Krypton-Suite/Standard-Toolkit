﻿namespace Krypton.Ribbon
{
    /// <summary>
    /// Implement storage for a gallery palette state. 
    /// </summary>
    public class PaletteGalleryState : Storage
    {
        #region Instance Fields
        private readonly PaletteRibbonBack _ribbonBack;
        private readonly PaletteRibbonBack _ribbonBorder;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteGalleryState class.
        /// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteGalleryState(PaletteGalleryRedirect inherit,
                                   NeedPaintHandler needPaint)
        {
            // Create storage that maps onto the inherit instances
            _ribbonBack= new PaletteRibbonBack(inherit.RibbonGalleryBack, needPaint);
            _ribbonBorder = new PaletteRibbonBack(inherit.RibbonGalleryBorder, needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => RibbonGalleryBack.IsDefault &
                                           RibbonGalleryBorder.IsDefault;

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">The palette state to populate with.</param>
        public virtual void PopulateFromBase(PaletteState state)
        {
            _ribbonBack.PopulateFromBase(state);
            _ribbonBorder.PopulateFromBase(state);
        }
        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritance parent.
        /// </summary>
        public virtual void SetInherit(PaletteGalleryRedirect inherit)
        {
            _ribbonBack.SetInherit(inherit.RibbonGalleryBack);
            _ribbonBorder.SetInherit(inherit.RibbonGalleryBorder);
        }
        #endregion

        #region RibbonGalleryBack
        /// <summary>
        /// Gets access to the gallery background palette details.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining gallery background appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonGalleryBack => _ribbonBack;

        private bool ShouldSerializeRibbonGalleryBack() => !_ribbonBack.IsDefault;

        #endregion

        #region RibbonGalleryBorder
        /// <summary>
        /// Gets access to the gallery border palette details.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining gallery border appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonGalleryBorder => _ribbonBorder;

        private bool ShouldSerializeRibbonGalleryBorder() => !_ribbonBorder.IsDefault;

        #endregion
    }
}
