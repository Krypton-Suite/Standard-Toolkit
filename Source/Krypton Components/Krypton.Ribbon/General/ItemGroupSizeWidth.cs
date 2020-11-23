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

namespace Krypton.Ribbon
{
    internal class ItemSizeWidth
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ItemSizeWidth class.
        /// </summary>
        /// <param name="itemSize">Group item size.</param>
        /// <param name="width">Width associated with the item size.</param>
        public ItemSizeWidth(GroupItemSize itemSize, int width)
            : this(itemSize, width, -1)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ItemSizeWidth class.
        /// </summary>
        /// <param name="itemSize">Group item size.</param>
        /// <param name="width">Width associated with the item size.</param>
        /// <param name="tag">Source specific tag information.</param>
        public ItemSizeWidth(GroupItemSize itemSize, 
                             int width,
                             int tag)
        {
            GroupItemSize = itemSize;
            Width = width;
            Tag = tag;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the item size.
        /// </summary>
        public GroupItemSize GroupItemSize { get; set; }

        /// <summary>
        /// Gets and sets the item width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets and sets the item tag.
        /// </summary>
        public int Tag { get; set; }

        #endregion
    }

    internal class GroupSizeWidth
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the GroupSizeWidth class.
        /// </summary>
        /// <param name="width">Width available for sizing a group.</param>
        /// <param name="sizing">Sizing information for applying to group.</param>
        public GroupSizeWidth(int width, ItemSizeWidth[] sizing)
        {
            Width = width;
            Sizing = sizing;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the item width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets and sets the array of sizing information for group.
        /// </summary>
        public ItemSizeWidth[] Sizing { get; set; }

        #endregion
    }
}
