using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Draws an design time only for adding a new item to a triple container.
    /// </summary>
    internal class ViewDrawRibbonDesignGroupTriple : ViewDrawRibbonDesignBase
    {
        #region Static Fields
        private static readonly Padding _preferredPaddingL = new Padding(1, 3, 1, 3);
        private static readonly Padding _layoutPaddingL = new Padding(1);
        private static readonly Padding _outerPaddingL = new Padding(0, 2, 0, 2);
        private static readonly Padding _paddingMS = new Padding(0, 2, 0, 2);
        private static readonly ImageList _imageList;
        #endregion

        #region Instance Fields
        private readonly KryptonRibbonGroupTriple _ribbonTriple;
        private ContextMenuStrip _cms;

        #endregion

        #region Identity
        static ViewDrawRibbonDesignGroupTriple()
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
        /// Initialize a new instance of the ViewDrawRibbonDesignGroupTriple class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonTriple">Associated ribbon group triple.</param>
        /// <param name="currentSize">Size the view should use.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonDesignGroupTriple(KryptonRibbon ribbon,
                                               KryptonRibbonGroupTriple ribbonTriple,
                                               GroupItemSize currentSize,
                                               NeedPaintHandler needPaint)
            : base(ribbon, needPaint)
        {
            Debug.Assert(ribbonTriple != null);

            _ribbonTriple = ribbonTriple;
            CurrentSize = currentSize;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawRibbonDesignGroupTriple:" + Id;
        }
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
        public override string GetShortText()
        {
            return "Item";
        }

        /// <summary>
        /// Gets the padding to use when calculating the preferred size.
        /// </summary>
        protected override Padding PreferredPadding => (CurrentSize == GroupItemSize.Large ? _preferredPaddingL : _paddingMS);

        /// <summary>
        /// Gets the padding to use when laying out the view.
        /// </summary>
        protected override Padding LayoutPadding => (CurrentSize == GroupItemSize.Large ? _layoutPaddingL : Padding.Empty);

        /// <summary>
        /// Gets the padding to shrink the client area by when laying out.
        /// </summary>
        protected override Padding OuterPadding => (CurrentSize == GroupItemSize.Large ? _outerPaddingL : _paddingMS);

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
                ToolStripMenuItem menuButton = new ToolStripMenuItem("Add Button", null, OnAddButton);
                ToolStripMenuItem menuColorButton = new ToolStripMenuItem("Add Color Button", null, OnAddColorButton);
                ToolStripMenuItem menuCheckBox = new ToolStripMenuItem("Add CheckBox", null, OnAddCheckBox);
                ToolStripMenuItem menuCustomControl = new ToolStripMenuItem("Add Custom Control", null, OnAddCustomControl);
                ToolStripMenuItem menuLabel = new ToolStripMenuItem("Add Label", null, OnAddLabel);
                ToolStripMenuItem menuRadioButton = new ToolStripMenuItem("Add RadioButton", null, OnAddRadioButton);
                ToolStripMenuItem menuTextBox = new ToolStripMenuItem("Add TextBox", null, OnAddTextBox);
                ToolStripMenuItem menuMaskedTextBox = new ToolStripMenuItem("Add MaskedTextBox", null, OnAddMaskedTextBox);
                ToolStripMenuItem menuRichTextBox = new ToolStripMenuItem("Add RichTextBox", null, OnAddRichTextBox);
                ToolStripMenuItem menuComboBox = new ToolStripMenuItem("Add ComboBox", null, OnAddComboBox);
                ToolStripMenuItem menuNumericUpDown = new ToolStripMenuItem("Add NumericUpDown", null, OnAddNumericUpDown);
                ToolStripMenuItem menuDomainUpDown = new ToolStripMenuItem("Add DomainUpDown", null, OnAddDomainUpDown);
                ToolStripMenuItem menuDateTimePicker = new ToolStripMenuItem("Add DateTimePicker", null, OnAddDateTimePicker);
                ToolStripMenuItem menuTrackBar = new ToolStripMenuItem("Add TrackBar", null, OnAddTrackBar);

                // Assign correct images
                menuButton.ImageIndex = 0;
                menuColorButton.ImageIndex = 1;
                menuCheckBox.ImageIndex = 2;
                menuRadioButton.ImageIndex = 3;
                menuLabel.ImageIndex = 4;
                menuCustomControl.ImageIndex = 5;
                menuTextBox.ImageIndex = 6;
                menuRichTextBox.ImageIndex = 7;
                menuComboBox.ImageIndex = 8;
                menuMaskedTextBox.ImageIndex = 9;
                menuNumericUpDown.ImageIndex = 10;
                menuDomainUpDown.ImageIndex = 11;
                menuDateTimePicker.ImageIndex = 12;
                menuTrackBar.ImageIndex = 13;

                // Finally, add all items to the strip
                _cms.Items.AddRange(new ToolStripItem[] { menuButton, menuColorButton, menuCheckBox, menuComboBox, menuCustomControl, menuDateTimePicker, menuDomainUpDown, menuLabel, menuNumericUpDown, menuRadioButton, menuRichTextBox, menuTextBox, menuTrackBar, menuMaskedTextBox });
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
            _ribbonTriple.OnDesignTimeAddButton();
        }

        private void OnAddColorButton(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddColorButton();
        }

        private void OnAddCheckBox(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddCheckBox();
        }

        private void OnAddRadioButton(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddRadioButton();
        }

        private void OnAddLabel(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddLabel();
        }

        private void OnAddCustomControl(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddCustomControl();
        }

        private void OnAddTextBox(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddTextBox();
        }

        private void OnAddMaskedTextBox(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddMaskedTextBox();
        }

        private void OnAddRichTextBox(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddRichTextBox();
        }

        private void OnAddComboBox(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddComboBox();
        }

        private void OnAddNumericUpDown(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddNumericUpDown();
        }

        private void OnAddDomainUpDown(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddDomainUpDown();
        }

        private void OnAddDateTimePicker(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddDateTimePicker();
        }

        private void OnAddTrackBar(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddTrackBar();
        }
        #endregion
    }
}
