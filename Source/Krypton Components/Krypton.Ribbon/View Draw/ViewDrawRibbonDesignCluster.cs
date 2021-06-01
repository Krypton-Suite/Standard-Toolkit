﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Draws an design time only for adding a new button to a cluster.
    /// </summary>
    internal class ViewDrawRibbonDesignCluster : ViewDrawRibbonDesignBase
    {
        #region Static Fields
        private static readonly Padding _padding = new Padding(1, 2, 0, 2);
        private static readonly ImageList _imageList;
        #endregion

        #region Instance Fields
        private readonly KryptonRibbonGroupCluster _ribbonCluster;
        private ContextMenuStrip _cms;
        #endregion

        #region Identity
        static ViewDrawRibbonDesignCluster()
        {
            // Use image list to convert background Magenta to transparent
            _imageList = new ImageList
            {
                TransparentColor = Color.Magenta
            };
            _imageList.Images.AddRange(new Image[]{Properties.Resources.KryptonRibbonGroupClusterButton,                                                   
                                                   Properties.Resources.KryptonRibbonGroupClusterColorButton});
        }

        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonDesignCluster class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonCluster">Reference to cluster definition.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonDesignCluster(KryptonRibbon ribbon,
                                           KryptonRibbonGroupCluster ribbonCluster,
                                           NeedPaintHandler needPaint)
            : base(ribbon, needPaint)
        {
            Debug.Assert(ribbonCluster != null);
            _ribbonCluster = ribbonCluster;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawRibbonDesignCluster:" + Id;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the short text used as the main ribbon title.
        /// </summary>
        /// <returns>Title string.</returns>
        public override string GetShortText()
        {
            return "Item";
        }

        /// <summary>
        /// Gets the padding to use when calculating the preferred size.
        /// </summary>
        protected override Padding PreferredPadding => _padding;

        /// <summary>
        /// Gets the padding to use when laying out the view.
        /// </summary>
        protected override Padding LayoutPadding => Padding.Empty;

        /// <summary>
        /// Gets the padding to shrink the client area by when laying out.
        /// </summary>
        protected override Padding OuterPadding => _padding;

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnClick(object sender, EventArgs e)
        {
            // Create the context strip the first time around
            if (_cms == null)
            {
                _cms = new ContextMenuStrip
                {
                    ImageList = _imageList
                };

                // Create child items
                ToolStripMenuItem menuButton = new ToolStripMenuItem("Add Cluster Button", null, OnAddButton);
                ToolStripMenuItem menuColorButton = new ToolStripMenuItem("Add Cluster Color Button", null, OnAddColorButton);

                // Assign correct images
                menuButton.ImageIndex = 0;
                menuColorButton.ImageIndex = 1;
                
                // Finally, add all items to the strip
                _cms.Items.AddRange(new ToolStripItem[] { menuButton, menuColorButton });
            }

            if (CommonHelper.ValidContextMenuStrip(_cms))
            {
                // Find the screen area of this view item
                Rectangle screenRect = Ribbon.ViewRectangleToScreen(this);

                // Make sure the popup is shown in a compatible way with any popups
                VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, new Point(screenRect.X, screenRect.Bottom));
            }
        }
        #endregion

        #region Implementation
        private void OnAddButton(object sender, EventArgs e)
        {
            _ribbonCluster.OnDesignTimeAddButton();
        }

        private void OnAddColorButton(object sender, EventArgs e)
        {
            _ribbonCluster.OnDesignTimeAddColorButton();
        }
        #endregion
    }
}
