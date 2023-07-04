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

namespace Krypton.Navigator
{
    /// <summary>
    /// Implement storage for ribbon tab and content.
    /// </summary>
    public class PaletteRibbonTabContent : Storage
    {
        #region Instance Fields
        private readonly PaletteRibbonDouble? _paletteTabDraw;
        private readonly PaletteNavContent? _paletteContent;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRibbonTabContent class.
        /// </summary>
        /// <param name="paletteBack">Source for inheriting palette ribbon background.</param>
        /// <param name="paletteText">Source for inheriting palette ribbon text.</param>
        /// <param name="paletteContent">Source for inheriting palette content.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteRibbonTabContent(IPaletteRibbonBack paletteBack,
                                                IPaletteRibbonText paletteText,
                                                IPaletteContent paletteContent,
                                                NeedPaintHandler needPaint)
        {
            Debug.Assert(paletteBack != null);
            Debug.Assert(paletteText != null);
            Debug.Assert(paletteContent != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Create storage that maps onto the inherit instances
            if (paletteBack != null && paletteText != null)
            {
                _paletteTabDraw = new PaletteRibbonDouble(paletteBack, paletteText, needPaint);
            }

            if (paletteContent != null)
            {
                _paletteContent = new PaletteNavContent(paletteContent, needPaint);
            }
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => Content != null && TabDraw != null && (TabDraw.IsDefault && Content.IsDefault);

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">The palette state to populate with.</param>
        public void PopulateFromBase(PaletteState state)
        {
            if (_paletteTabDraw != null)
            {
                _paletteTabDraw.PopulateFromBase(state);
            }

            if (_paletteContent != null)
            {
                _paletteContent.PopulateFromBase(state);
            }
        }
        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritance parent.
        /// </summary>
        public void SetInherit(IPaletteRibbonBack? paletteBack,
                               IPaletteRibbonText? paletteText,
                               IPaletteContent? paletteContent)
        {
            if (_paletteTabDraw != null)
            {
                _paletteTabDraw.SetInherit(paletteBack, paletteText);
            }

            if (_paletteContent != null)
            {
                _paletteContent.SetInherit(paletteContent);
            }
        }
        #endregion

        #region TabDraw
        /// <summary>
        /// Gets access to the tab drawing appearance.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining tab drawing appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonDouble? TabDraw => _paletteTabDraw;

        private bool ShouldSerializeTabDraw() => _paletteTabDraw != null && !_paletteTabDraw.IsDefault;

        #endregion

        #region Content
        /// <summary>
        /// Gets access to the tab content appearance.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Overrides for defining tab content appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteNavContent? Content => _paletteContent;

        private bool ShouldSerializeContent() => _paletteContent != null && !_paletteContent.IsDefault;

        #endregion
    }
}
