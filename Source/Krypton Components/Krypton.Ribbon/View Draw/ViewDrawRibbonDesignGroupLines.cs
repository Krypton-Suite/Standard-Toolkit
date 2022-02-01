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


namespace Krypton.Ribbon
{
    /// <summary>
    /// Draws an design time only for adding a new item to a lines container.
    /// </summary>
    internal class ViewDrawRibbonDesignGroupLines : ViewDrawRibbonDesignBase
    {
        #region Static Fields
        private static readonly ImageList _imageList;
        #endregion

        #region Instance Fields
        private readonly KryptonRibbonGroupLines _ribbonLines;
        private ContextMenuStrip _cms;
        private readonly Padding _padding; // = new(0, 2, 2, 4);
        #endregion

        #region Identity
        static ViewDrawRibbonDesignGroupLines()
        {
            // Use image list to convert background Magenta to transparent
            _imageList = new ImageList
            {
                TransparentColor = Color.Magenta
            };
            _imageList.Images.AddRange(new Image[]{Properties.Resources.KryptonRibbonGroupButton,
                                                   Properties.Resources.KryptonRibbonGroupColorButton,
                                                   Properties.Resources.KryptonRibbonGroupCheckBox,
                                                   Properties.Resources.KryptonRibbonGroupRadioButton,
                                                   Properties.Resources.KryptonRibbonGroupLabel,
                                                   Properties.Resources.KryptonRibbonGroupCustomControl,
                                                   Properties.Resources.KryptonRibbonGroupCluster,
                                                   Properties.Resources.KryptonRibbonGroupTextBox,
                                                   Properties.Resources.KryptonRibbonGroupRichTextBox,
                                                   Properties.Resources.KryptonRibbonGroupComboBox,
                                                   Properties.Resources.KryptonRibbonGroupMaskedTextBox,
                                                   Properties.Resources.KryptonRibbonGroupNumericUpDown,
                                                   Properties.Resources.KryptonRibbonGroupDomainUpDown,
                                                   Properties.Resources.KryptonRibbonGroupDateTimePicker,
                                                   Properties.Resources.KryptonRibbonGroupTrackBar});
        }

        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonDesignGroupLines class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonLines">Associated ribbon group lines.</param>
        /// <param name="currentSize">Size the view should use.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonDesignGroupLines(KryptonRibbon ribbon,
                                              KryptonRibbonGroupLines ribbonLines,
                                              GroupItemSize currentSize,
                                              NeedPaintHandler needPaint)
            : base(ribbon, needPaint)
        {
            Debug.Assert(ribbonLines != null);

            _ribbonLines = ribbonLines;
            CurrentSize = currentSize;
             _padding = new Padding(0, (int)(2 * FactorDpiY), (int)(2 * FactorDpiX), (int)(4 * FactorDpiY));
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString() =>
            // Return the class name and instance identifier
            @"ViewDrawRibbonDesignGroupLines:" + Id;

        #endregion

        #region CurrentSize
        /// <summary>
        /// Gets and sets the size the view should use.
        /// </summary>
        public GroupItemSize CurrentSize { get; set; }

        #endregion

        #region Protected
        /// <summary>
        /// Gets the short text used as the main ribbon title.
        /// </summary>
        /// <returns>Title string.</returns>
        public override string GetShortText() => @"Item";

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
                ToolStripMenuItem menuButton = new("Add Button", null, OnAddButton);
                ToolStripMenuItem menuColorButton = new("Add Color Button", null, OnAddColorButton);
                ToolStripMenuItem menuCheckBox = new("Add CheckBox", null, OnAddCheckBox);
                ToolStripMenuItem menuRadioButton = new("Add RadioButton", null, OnAddRadioButton);
                ToolStripMenuItem menuLabel = new("Add Label", null, OnAddLabel);
                ToolStripMenuItem menuCustomControl = new("Add Custom Control", null, OnAddCustomControl);
                ToolStripMenuItem menuCluster = new("Add Cluster", null, OnAddCluster);
                ToolStripMenuItem menuTextBox = new("Add TextBox", null, OnAddTextBox);
                ToolStripMenuItem menuMaskedTextBox = new("Add MaskedTextBox", null, OnAddMaskedTextBox);
                ToolStripMenuItem menuRichTextBox = new("Add RichTextBox", null, OnAddRichTextBox);
                ToolStripMenuItem menuComboBox = new("Add ComboBox", null, OnAddComboBox);
                ToolStripMenuItem menuNumericUpDown = new("Add NumericUpDown", null, OnAddNumericUpDown);
                ToolStripMenuItem menuDomainUpDown = new("Add DomainUpDown", null, OnAddDomainUpDown);
                ToolStripMenuItem menuDateTimePicker = new("Add DateTimePicker", null, OnAddDateTimePicker);
                ToolStripMenuItem menuTrackBar = new("Add TrackBar", null, OnAddTrackBar);

                // Assign correct images
                menuButton.ImageIndex = 0;
                menuColorButton.ImageIndex = 1;
                menuCheckBox.ImageIndex = 2;
                menuRadioButton.ImageIndex = 3;
                menuLabel.ImageIndex = 4;
                menuCustomControl.ImageIndex = 5;
                menuCluster.ImageIndex = 6;
                menuTextBox.ImageIndex = 7;
                menuRichTextBox.ImageIndex = 8;
                menuComboBox.ImageIndex = 9;
                menuMaskedTextBox.ImageIndex = 10;
                menuNumericUpDown.ImageIndex = 11;
                menuDomainUpDown.ImageIndex = 12;
                menuDateTimePicker.ImageIndex = 13;
                menuTrackBar.ImageIndex = 13;

                // Finally, add all items to the strip
                _cms.Items.AddRange(new ToolStripItem[] { menuButton, menuColorButton, menuCheckBox, menuCluster, menuComboBox, menuCustomControl, menuDateTimePicker, menuDomainUpDown, menuLabel, menuNumericUpDown, menuRadioButton, menuRichTextBox, menuTextBox, menuTrackBar, menuMaskedTextBox });
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
            _ribbonLines.OnDesignTimeAddButton();
        }

        private void OnAddColorButton(object sender, EventArgs e)
        {
            _ribbonLines.OnDesignTimeAddColorButton();
        }

        private void OnAddCheckBox(object sender, EventArgs e)
        {
            _ribbonLines.OnDesignTimeAddCheckBox();
        }

        private void OnAddRadioButton(object sender, EventArgs e)
        {
            _ribbonLines.OnDesignTimeAddRadioButton();
        }

        private void OnAddCluster(object sender, EventArgs e)
        {
            _ribbonLines.OnDesignTimeAddCluster();
        }

        private void OnAddLabel(object sender, EventArgs e)
        {
            _ribbonLines.OnDesignTimeAddLabel();
        }

        private void OnAddCustomControl(object sender, EventArgs e)
        {
            _ribbonLines.OnDesignTimeAddCustomControl();
        }

        private void OnAddTextBox(object sender, EventArgs e)
        {
            _ribbonLines.OnDesignTimeAddTextBox();
        }

        private void OnAddMaskedTextBox(object sender, EventArgs e)
        {
            _ribbonLines.OnDesignTimeAddMaskedTextBox();
        }

        private void OnAddRichTextBox(object sender, EventArgs e)
        {
            _ribbonLines.OnDesignTimeAddRichTextBox();
        }

        private void OnAddComboBox(object sender, EventArgs e)
        {
            _ribbonLines.OnDesignTimeAddComboBox();
        }

        private void OnAddNumericUpDown(object sender, EventArgs e)
        {
            _ribbonLines.OnDesignTimeAddNumericUpDown();
        }

        private void OnAddDomainUpDown(object sender, EventArgs e)
        {
            _ribbonLines.OnDesignTimeAddDomainUpDown();
        }

        private void OnAddDateTimePicker(object sender, EventArgs e)
        {
            _ribbonLines.OnDesignTimeAddDateTimePicker();
        }

        private void OnAddTrackBar(object sender, EventArgs e)
        {
            _ribbonLines.OnDesignTimeAddTrackBar();
        }
        #endregion
    }
}
