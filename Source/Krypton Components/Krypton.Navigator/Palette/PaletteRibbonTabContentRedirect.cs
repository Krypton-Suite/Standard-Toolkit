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
using System.Diagnostics;
using Krypton.Toolkit;

namespace Krypton.Navigator
{
    /// <summary>
    /// Storage for general ribbon values.
    /// </summary>
    public class PaletteRibbonTabContentRedirect : Storage
    {
        #region Instance Fields
        private readonly PaletteNavContent _content;
        private readonly PaletteRibbonDoubleRedirect _drawRedirect;
        private readonly PaletteContentInheritRedirect _contentInherit;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRibbonTabContentRedirect class.
        /// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteRibbonTabContentRedirect(PaletteRedirect redirect,
                                               NeedPaintHandler needPaint)
        {
            Debug.Assert(redirect != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            _drawRedirect = new PaletteRibbonDoubleRedirect(redirect, 
                                                            PaletteRibbonBackStyle.RibbonTab, 
                                                            PaletteRibbonTextStyle.RibbonTab, 
                                                            needPaint);
            
            _contentInherit = new PaletteContentInheritRedirect(redirect);
            _content = new PaletteNavContent(_contentInherit, needPaint);
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            _drawRedirect.SetRedirector(redirect);
            _contentInherit.SetRedirector(redirect);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (TabDraw.IsDefault && Content.IsDefault);

        #endregion

        #region TabDraw
        /// <summary>
        /// Gets access to the tab drawing appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining tab drawing appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonDoubleRedirect TabDraw => _drawRedirect;

        private bool ShouldSerializeTabDraw()
        {
            return !_drawRedirect.IsDefault;
        }
        #endregion

        #region Content
        /// <summary>
        /// Gets access to the tab content appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining tab content appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteNavContent Content => _content;

        private bool ShouldSerializeContent()
        {
            return !_content.IsDefault;
        }
        #endregion
    }
}
