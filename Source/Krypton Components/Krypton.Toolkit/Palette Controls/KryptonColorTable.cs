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

using System.Drawing;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Extend the ProfessionalColorTable with some Krypton specific properties.
    /// </summary>
    public class KryptonColorTable : ProfessionalColorTable
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Creates a new instance of the KryptonColorTable class.
        /// </summary>
        /// <param name="palette">Reference to associated palette.</param>
        public KryptonColorTable(IPalette palette)
        {
            Palette = palette;
        }
        #endregion

        #region Palette
        /// <summary>
        /// Gets the associated palette instance.
        /// </summary>
        public IPalette Palette { get; }

        #endregion

        #region RoundedEdges
        /// <summary>
        /// Gets a value indicating if rounded egdes are required.
        /// </summary>
        public virtual InheritBool UseRoundedEdges => InheritBool.True;

        #endregion

        #region MenuItemText
        /// <summary>
        /// Gets the text color used on the menu items.
        /// </summary>
        public virtual Color MenuItemText => SystemColors.MenuText;

        #endregion

        #region MenuStripText
        /// <summary>
        /// Gets the text color used on the menu strip.
        /// </summary>
        public virtual Color MenuStripText => SystemColors.MenuText;

        #endregion

        #region ToolStripText
        /// <summary>
        /// Gets the text color used on the tool strip.
        /// </summary>
        public virtual Color ToolStripText => SystemColors.MenuText;

        #endregion

        #region StatusStripText
        /// <summary>
        /// Gets the text color used on the status strip.
        /// </summary>
        public virtual Color StatusStripText => SystemColors.MenuText;

        #endregion

        #region MenuStripFont
        /// <summary>
        /// Gets the font used on the menu strip.
        /// </summary>
        public virtual Font MenuStripFont => SystemInformation.MenuFont;

        #endregion

        #region ToolStripFont
        /// <summary>
        /// Gets the font used on the tool strip.
        /// </summary>
        public virtual Font ToolStripFont => SystemInformation.MenuFont;

        #endregion

        #region StatusStripFont
        /// <summary>
        /// Gets the font used on the status strip.
        /// </summary>
        public virtual Font StatusStripFont => SystemInformation.MenuFont;

        #endregion
    }
}
