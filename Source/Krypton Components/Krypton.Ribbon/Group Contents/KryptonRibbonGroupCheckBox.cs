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
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Represents a ribbon group check box.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonGroupCheckBox), "ToolboxBitmaps.KryptonRibbonGroupCheckBox.bmp")]
    [Designer(typeof(Krypton.Ribbon.KryptonRibbonGroupCheckBoxDesigner))]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultEvent("CheckedChanged")]
    [DefaultProperty("Checked")]
    [DefaultBindingProperty("CheckState")]
    public class KryptonRibbonGroupCheckBox : KryptonRibbonGroupItem
    {
        #region Instance Fields
        private bool _enabled;
        private bool _visible;
        private CheckState _checkState;
        private bool _checked;
        private bool _threeState;
        private bool _autoCheck;
        private string _textLine1;
        private string _textLine2;
        private string _keyTip;
        private GroupItemSize _itemSizeMax;
        private GroupItemSize _itemSizeMin;
        private GroupItemSize _itemSizeCurrent;
        private KryptonCommand _command;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the check box is clicked.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when the check box is clicked.")]
        public event EventHandler Click;

        /// <summary>
        /// Occurs when the value of the Checked property has changed.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs whenever the Checked property has changed.")]
        public event EventHandler CheckedChanged;

        /// <summary>
        /// Occurs when the value of the CheckState property has changed.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs whenever the CheckState property has changed.")]
        public event EventHandler CheckStateChanged;

        /// <summary>
        /// Occurs after the value of a property has changed.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs after the value of a property has changed.")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when the design time context menu is requested.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event MouseEventHandler DesignTimeContextMenu;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonRibbonGroupCheckBox class.
        /// </summary>
        public KryptonRibbonGroupCheckBox()
        {
            // Default fields
            _enabled = true;
            _visible = true;
            _checked = false;
            _threeState = false;
            _checkState = CheckState.Unchecked;
            _autoCheck = true;
            ShortcutKeys = Keys.None;
            _textLine1 = "CheckBox";
            _textLine2 = string.Empty;
            _keyTip = "C";
            _itemSizeMax = GroupItemSize.Large;
            _itemSizeMin = GroupItemSize.Small;
            _itemSizeCurrent = GroupItemSize.Large;
            ToolTipImageTransparentColor = Color.Empty;
            ToolTipTitle = string.Empty;
            ToolTipBody = string.Empty;
            ToolTipStyle = LabelStyle.SuperTip;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the display text line 1 for the check box.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Check box display text line 1.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("CheckBox")]
        public string TextLine1
        {
            get => _textLine1;

            set
            {
                // We never allow an empty text value
                if (string.IsNullOrEmpty(value))
                {
                    value = "CheckBox";
                }

                if (value != _textLine1)
                {
                    _textLine1 = value;
                    OnPropertyChanged("TextLine1");
                }
            }
        }

        /// <summary>
        /// Gets and sets the display text line 2 for the check box.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Check box display text line 2.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("")]
        public string TextLine2
        {
            get => _textLine2;

            set
            {
                if (value != _textLine2)
                {
                    _textLine2 = value;
                    OnPropertyChanged("TextLine2");
                }
            }
        }

        /// <summary>
        /// Gets and sets the key tip for the ribbon group check box.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Ribbon group check box key tip.")]
        [DefaultValue("C")]
        public string KeyTip
        {
            get => _keyTip;

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = "C";
                }

                _keyTip = value.ToUpper();
            }
        }

        /// <summary>
        /// Gets and sets the visible state of the check box.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the check box is visible or hidden.")]
        [DefaultValue(true)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override bool Visible
        {
            get => _visible;

            set
            {
                if (value != _visible)
                {
                    _visible = value;
                    OnPropertyChanged("Visible");
                }
            }
        }

        /// <summary>
        /// Make the ribbon group visible.
        /// </summary>
        public void Show()
        {
            Visible = true;
        }

        /// <summary>
        /// Make the ribbon group hidden.
        /// </summary>
        public void Hide()
        {
            Visible = false;
        }

        /// <summary>
        /// Gets and sets the enabled state of the group check box entry.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the group check box is enabled.")]
        [DefaultValue(true)]
        public bool Enabled
        {
            get => _enabled;

            set
            {
                if (value != _enabled)
                {
                    _enabled = value;
                    OnPropertyChanged("Enabled");
                }
            }
        }

        /// <summary>
        /// Gets and sets the checked state of the group entry.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the group check box is checked.")]
        [DefaultValue(typeof(CheckState), "Unchecked")]
        public CheckState CheckState
        {
            get => _checkState;

            set
            {
                if (value != _checkState)
                {
                    _checkState = value;
                    bool newChecked = (_checkState != CheckState.Unchecked);
                    bool checkedChanged = (_checked != newChecked);
                    _checked = newChecked;
                    OnPropertyChanged("CheckState");

                    // Generate events
                    if (checkedChanged)
                    {
                        OnCheckedChanged(EventArgs.Empty);
                    }

                    OnCheckStateChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the check box is in the checked state.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates if the check box is in the checked state.")]
        [DefaultValue(false)]
        [Bindable(true)]
        public bool Checked
        {
            get => _checked;

            set
            {
                if (_checked != value)
                {
                    // Store new values
                    _checked = value;
                    _checkState = (_checked ? CheckState.Checked : CheckState.Unchecked);
                    OnPropertyChanged("Checked");

                    // Generate events
                    OnCheckedChanged(EventArgs.Empty);
                    OnCheckStateChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the check box is automatically changed state when clicked. 
        /// </summary>
        [Category("Behavior")]
        [Description("Causes the check box to automatically change state when clicked.")]
        [DefaultValue(true)]
        public bool AutoCheck
        {
            get => _autoCheck;

            set 
            {
                if (_autoCheck != value)
                {
                    _autoCheck = value;
                    OnPropertyChanged("AutoCheck");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the check box allows three states instead of two.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates if the check bix allows three states instead of two.")]
        [DefaultValue(false)]
        public bool ThreeState
        {
            get => _threeState;

            set
            {
                if (_threeState != value)
                {
                    _threeState = value;
                    OnPropertyChanged("ThreeState");
                }
            }
        }

        /// <summary>
        /// Gets and sets the shortcut key combination.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Shortcut key combination to fire click event of the check box.")]
        public Keys ShortcutKeys { get; set; }

        private bool ShouldSerializeShortcutKeys()
        {
            return (ShortcutKeys != Keys.None);
        }

        /// <summary>
        /// Resets the ShortcutKeys property to its default value.
        /// </summary>
        public void ResetShortcutKeys()
        {
            ShortcutKeys = Keys.None;
        }

        /// <summary>
        /// Gets and sets the tooltip label style for group check box.
        /// </summary>
        [Category("Appearance")]
        [Description("Tooltip style for the group check box.")]
        [DefaultValue(typeof(LabelStyle), "SuperTip")]
        public LabelStyle ToolTipStyle { get; set; }

        /// <summary>
        /// Gets and sets the image for the item ToolTip.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Display image associated ToolTip.")]
        [DefaultValue(null)]
        [Localizable(true)]
        public Image ToolTipImage { get; set; }

        /// <summary>
        /// Gets and sets the color to draw as transparent in the ToolTipImage.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Color to draw as transparent in the ToolTipImage.")]
        [KryptonDefaultColorAttribute()]
        [Localizable(true)]
        public Color ToolTipImageTransparentColor { get; set; }

        /// <summary>
        /// Gets and sets the title text for the item ToolTip.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Title text for use in associated ToolTip.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [Localizable(true)]
        public string ToolTipTitle { get; set; }

        /// <summary>
        /// Gets and sets the body text for the item ToolTip.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Body text for use in associated ToolTip.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [Localizable(true)]
        public string ToolTipBody { get; set; }

        /// <summary>
        /// Gets and sets the associated KryptonCommand.
        /// </summary>
        [Category("Behavior")]
        [Description("Command associated with the check box.")]
        [DefaultValue(null)]
        public KryptonCommand KryptonCommand
        {
            get => _command;

            set
            {
                if (_command != value)
                {
                    if (_command != null)
                    {
                        _command.PropertyChanged -= OnCommandPropertyChanged;
                    }

                    _command = value;
                    OnPropertyChanged("KryptonCommand");

                    if (_command != null)
                    {
                        _command.PropertyChanged += OnCommandPropertyChanged;
                    }
                }
            }
        }

        /// <summary>
        /// Gets and sets the maximum allowed size of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override GroupItemSize ItemSizeMaximum
        {
            get => _itemSizeMax;

            set
            {
                if (_itemSizeMax != value)
                {
                    _itemSizeMax = value;
                    OnPropertyChanged("ItemSizeMaximum");
                }
            }
        }

        /// <summary>
        /// Gets and sets the minimum allowed size of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override GroupItemSize ItemSizeMinimum
        {
            get => _itemSizeMin;

            set
            {
                if (_itemSizeMin != value)
                {
                    _itemSizeMin = value;
                    OnPropertyChanged("ItemSizeMinimum");
                }
            }
        }

        /// <summary>
        /// Gets and sets the current item size.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override GroupItemSize ItemSizeCurrent
        {
            get => _itemSizeCurrent;

            set
            {
                if (_itemSizeCurrent != value)
                {
                    _itemSizeCurrent = value;
                    OnPropertyChanged("ItemSizeCurrent");
                }
            }
        }

        /// <summary>
        /// Creates an appropriate view element for this item.
        /// </summary>
        /// <param name="ribbon">Reference to the owning ribbon control.</param>
        /// <param name="needPaint">Delegate for notifying changes in display.</param>
        /// <returns>ViewBase derived instance.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ViewBase CreateView(KryptonRibbon ribbon, 
                                            NeedPaintHandler needPaint)
        {
            return new ViewDrawRibbonGroupCheckBox(ribbon, this, needPaint);
        }

        /// <summary>
        /// Generates a Click event for a check box.
        /// </summary>
        public void PerformClick()
        {
            PerformClick(null);
        }

        /// <summary>
        /// Generates a Click event for a check box.
        /// </summary>
        /// <param name="finishDelegate">Delegate fired during event processing.</param>
        public void PerformClick(EventHandler finishDelegate)
        {
            OnClick(finishDelegate);
        }

        /// <summary>
        /// Internal design time properties.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public ViewBase CheckBoxView { get; set; }

        #endregion

        #region Protected
        /// <summary>
        /// Handles a change in the property of an attached command.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A PropertyChangedEventArgs that contains the event data.</param>
        protected virtual void OnCommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "TextLine1":
                    OnPropertyChanged("TextLine1");
                    break;
                case "TextLine2":
                    OnPropertyChanged("TextLine2");
                    break;
                case "Enabled":
                    OnPropertyChanged("Enabled");
                    break;
                case "Checked":
                case "CheckState":
                    OnPropertyChanged("CheckState");
                    break;
            }
        }

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="finishDelegate">Delegate fired during event processing.</param>
        protected virtual void OnClick(EventHandler finishDelegate)
        {
            bool fireDelegate = true;

            if (!Ribbon.InDesignMode)
            {
                if (Enabled)
                {
                    if (AutoCheck)
                    {
                        // Find current state
                        CheckState checkState = KryptonCommand?.CheckState ?? CheckState;

                        // Find new state based on the current state
                        switch (checkState)
                        {
                            case CheckState.Unchecked:
                                checkState = CheckState.Checked;
                                break;
                            case CheckState.Checked:
                                checkState = (ThreeState ? CheckState.Indeterminate : CheckState.Unchecked);
                                break;
                            case CheckState.Indeterminate:
                                checkState = CheckState.Unchecked;
                                break;
                        }

                        // Push back the change to the attached command
                        if (KryptonCommand != null)
                        {
                            KryptonCommand.CheckState = checkState;
                        }
                        else
                        {
                            CheckState = checkState;
                        }
                    }

                    // In showing a popup we fire the delegate before the click so that the
                    // minimized popup is removed out of the way before the event is handled
                    // because if the event shows a dialog then it would appear behind the popup
                    if (VisualPopupManager.Singleton.CurrentPopup != null)
                    {
                        // Do we need to fire a delegate stating the click processing has finished?
                        if (fireDelegate)
                        {
                            finishDelegate?.Invoke(this, EventArgs.Empty);
                        }

                        fireDelegate = false;
                    }

                    // Generate actual click event
                    Click?.Invoke(this, EventArgs.Empty);

                    // Clicking the button should execute the associated command
                    KryptonCommand?.PerformExecute();
                }
            }

            // Do we need to fire a delegate stating the click processing has finished?
            if (fireDelegate)
            {
                finishDelegate?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the CheckedChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnCheckedChanged(EventArgs e)
        {
            CheckedChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the CheckStateChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnCheckStateChanged(EventArgs e)
        {
            CheckStateChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of property that has changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Internal
        internal void OnDesignTimeContextMenu(MouseEventArgs e)
        {
            DesignTimeContextMenu?.Invoke(this, e);
        }

        internal override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Only interested in key processing if this check box definition 
            // is enabled and itself and all parents are also visible
            if (Enabled && ChainVisible)
            {
                // Do we have a shortcut definition for ourself?
                if (ShortcutKeys != Keys.None)
                {
                    // Does it match the incoming key combination?
                    if (ShortcutKeys == keyData)
                    {
                        PerformClick();
                        return true;
                    }
                }
            }

            return false;
        }

        internal override LabelStyle InternalToolTipStyle => ToolTipStyle;

        internal override Image InternalToolTipImage => ToolTipImage;

        internal override Color InternalToolTipImageTransparentColor => ToolTipImageTransparentColor;

        internal override string InternalToolTipTitle => ToolTipTitle;

        internal override string InternalToolTipBody => ToolTipBody;

        #endregion
    }
}
