// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Allow some palette values to be overriden.
    /// </summary>
    public class PaletteNodeOverride : GlobalId,
                                       IPaletteTriple
    {
        #region Intance Fields
        private readonly PaletteBackInheritNode _overrideBack;
        private readonly PaletteBorderInheritOverride _overrideBorder;
        private readonly PaletteContentInheritNode _overrideContent;
        #endregion

        #region Identity

        /// <summary>
        /// Initialize a new instance of the PaletteNodeOverride class.
        /// </summary>
        /// <param name="triple">Palette to use.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public PaletteNodeOverride(IPaletteTriple triple)
        {
            Debug.Assert(triple != null);

            // Validate incoming references
            if (triple == null)
            {
                throw new ArgumentNullException(nameof(triple));
            }

            // Create the triple override instances
            _overrideBack = new PaletteBackInheritNode(triple.PaletteBack);
            _overrideBorder = new PaletteBorderInheritOverride(triple.PaletteBorder, triple.PaletteBorder);
            _overrideContent = new PaletteContentInheritNode(triple.PaletteContent);
        }            
        #endregion

        #region TreeNode
        /// <summary>
        /// Set the tree node to use for sourcing values.
        /// </summary>
        public TreeNode TreeNode
        {
            set
            {
                _overrideBack.TreeNode = value;
                _overrideContent.TreeNode = value;
            }
        }
        #endregion

        #region Palette Accessors
        /// <summary>
        /// Gets the background palette.
        /// </summary>
        public IPaletteBack PaletteBack => _overrideBack;

        /// <summary>
        /// Gets the border palette.
        /// </summary>
        public IPaletteBorder PaletteBorder => _overrideBorder;

        /// <summary>
        /// Gets the border palette.
        /// </summary>
        public IPaletteContent PaletteContent => _overrideContent;

        #endregion    
    }
}
