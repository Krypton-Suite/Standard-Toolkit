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
    /// Storage for application button related properties.
    /// </summary>
    public class RibbonAppButton : Storage
    {
        #region Static Fields
        private static readonly Image _defaultAppImage = Properties.Resources.AppButtonDefault;
        private const string DEFAULT_APP_TEXT = @"File";
        private static readonly Color _defaultAppBaseColorDark = Color.FromArgb(31, 72, 161);
        private static readonly Color _defaultAppBaseColorLight = Color.FromArgb(84, 158, 243);
        #endregion

        #region Type Definitions
        /// <summary>
        /// Collection for managing ButtonSpecAppMenu instances.
        /// </summary>
        public class AppMenuButtonSpecCollection : ButtonSpecCollection<ButtonSpecAppMenu> 
        { 
            #region Identity
            /// <summary>
            /// Initialize a new instance of the AppMenuButtonSpecCollection class.
            /// </summary>
            /// <param name="owner">Reference to owning object.</param>
            public AppMenuButtonSpecCollection(KryptonRibbon owner)
                : base(owner)
            {
            }
            #endregion
        }
        #endregion

        #region Instance Fields
        private readonly KryptonRibbon _ribbon;
        private Image _appButtonImage;
        private readonly KryptonContextMenuItems _appButtonMenuItems;
        private bool _appButtonVisible;
        private Color _appButtonBaseColorDark;
        private Color _appButtonBaseColorLight;
        private Color _appButtonTextColor;
        private string _appButtonText;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the RibbonAppButton class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon instance.</param>
        public RibbonAppButton(KryptonRibbon ribbon)
        {
            Debug.Assert(ribbon != null);
            _ribbon = ribbon;

            // Default values
            _appButtonMenuItems = new KryptonContextMenuItems
            {
                ImageColumn = false
            };
            _appButtonImage = _defaultAppImage;
            AppButtonSpecs = new AppMenuButtonSpecCollection(ribbon);
            AppButtonRecentDocs = new KryptonRibbonRecentDocCollection();
            AppButtonToolTipTitle = string.Empty;
            AppButtonToolTipBody = string.Empty;
            AppButtonToolTipImageTransparentColor = Color.Empty;
            AppButtonToolTipStyle = LabelStyle.SuperTip;
            AppButtonMinRecentSize = new Size(250, 250);
            AppButtonMaxRecentSize = new Size(350, 350);
            AppButtonShowRecentDocs = true;
            _appButtonVisible = true;
            _appButtonBaseColorDark = _defaultAppBaseColorDark;
            _appButtonBaseColorLight = _defaultAppBaseColorLight;
            _appButtonTextColor = Color.White;
            _appButtonText = DEFAULT_APP_TEXT;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (AppButtonImage == _defaultAppImage) &&
                                           (AppButtonText == DEFAULT_APP_TEXT) &&
                                           (AppButtonBaseColorDark == _defaultAppBaseColorDark) &&
                                           (AppButtonBaseColorLight == _defaultAppBaseColorLight) &&
                                           (AppButtonTextColor == Color.White) &&
                                           (AppButtonMenuItems.Count == 0) &&
                                           (AppButtonRecentDocs.Count == 0) &&
                                           AppButtonMinRecentSize.Equals(new Size(250, 250)) &&
                                           AppButtonMaxRecentSize.Equals(new Size(350, 350)) &&
                                           AppButtonShowRecentDocs &&
                                           (AppButtonSpecs.Count == 0) &&
                                           string.IsNullOrEmpty(AppButtonToolTipBody) &&
                                           string.IsNullOrEmpty(AppButtonToolTipBody) &&
                                           (AppButtonToolTipImage == null) &&
                                           (AppButtonToolTipImageTransparentColor == Color.Empty) &&
                                           (AppButtonToolTipStyle == LabelStyle.SuperTip) &&
                                           AppButtonVisible
                                           && !IgnoreDoubleClickClose;

        #endregion

        #region AppButtonImage
        /// <summary>
        /// Gets and sets the application button image.
        /// </summary>
        [Localizable(true)]
        [Category(@"Values")]
        [Description(@"Application button image.")]
        [RefreshProperties(RefreshProperties.All)]
        public Image AppButtonImage
        {
            get => _appButtonImage;

            set
            {
                if (_appButtonImage != value)
                {
                    _appButtonImage = value;

                    // Caption area is not created when property first set to default value
                    _ribbon.CaptionArea?.AppButtonChanged();
                }
            }
        }

        private bool ShouldSerializeAppButtonImage() => AppButtonImage != _defaultAppImage;

        #endregion

        #region AppButtonBaseColorDark
        /// <summary>
        /// Gets and sets the darker base color used for drawing an Office 2010 style application button.
        /// </summary>
        [Bindable(true)]
        [Category(@"Appearance")]
        [Description(@"Darker base color used for drawing an Office 2010 style application button.")]
        [KryptonDefaultColor()]
        [DefaultValue(typeof(Color), "31, 72, 161")]
        public Color AppButtonBaseColorDark
        {
            get => _appButtonBaseColorDark;

            set 
            {
                if (_appButtonBaseColorDark != Color.Empty)
                {
                    _appButtonBaseColorDark = value;
                    _ribbon.PerformNeedPaint(true);
                }
            }
        }
        #endregion

        #region AppButtonBaseColorLight
        /// <summary>
        /// Gets and sets the lighter base color used for drawing an Office 2010 style application button.
        /// </summary>
        [Bindable(true)]
        [Category(@"Appearance")]
        [Description(@"Lighter base color used for drawing an Office 2010 style application button.")]
        [KryptonDefaultColor()]
        [DefaultValue(typeof(Color), "84, 158, 243")]
        public Color AppButtonBaseColorLight
        {
            get => _appButtonBaseColorLight;

            set 
            {
                if (_appButtonBaseColorLight != Color.Empty)
                {
                    _appButtonBaseColorLight = value;
                    _ribbon.PerformNeedPaint(true);
                }
            }
        }
        #endregion

        #region AppButtonTextColor
        /// <summary>
        /// Gets and sets the text color used for drawing an Office 2010 style application button.
        /// </summary>
        [Bindable(true)]
        [Category(@"Appearance")]
        [Description(@"Text color used for drawing an Office 2010 style application button.")]
        [KryptonDefaultColor()]
        [DefaultValue(typeof(Color), "White")]
        public Color AppButtonTextColor
        {
            get => _appButtonTextColor;

            set 
            {
                if (_appButtonTextColor != Color.Empty)
                {
                    _appButtonTextColor = value;
                    _ribbon.PerformNeedPaint(true);
                }
            }
        }
        #endregion

        #region AppButtonText
        /// <summary>
        /// Gets and sets the text used for drawing an Office 2010 style application button.
        /// </summary>
        [Bindable(true)]
        [Category(@"Appearance")]
        [Description(@"Text used for drawing an Office 2010 style application button.")]
        [KryptonDefaultColor()]
        [DefaultValue("File")]
        [Localizable(true)]
        public string AppButtonText
        {
            get => _appButtonText;

            set 
            {
                if (_appButtonText != null)
                {
                    _appButtonText = value;
                    _ribbon.PerformNeedPaint(true);
                }
            }
        }
        #endregion

        #region AppButtonContextMenu
        /// <summary>
        /// Gets and sets the context menu items for the application button.
        /// </summary>
        [Category(@"Values")]
        [Description(@"Context menu items for the application button.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(@"Krypton.Toolkit.KryptonContextMenuItemCollectionEditor, Krypton.Toolkit", typeof(UITypeEditor))]
        public virtual KryptonContextMenuItemCollection AppButtonMenuItems => _appButtonMenuItems.Items;

        #endregion

        #region AppButtonRecentDocs
        /// <summary>
        /// Gets and sets the recent document entries for the application button.
        /// </summary>
        [Category(@"Values")]
        [Description(@"Recent document entries for the application buttton.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(@"Krypton.Ribbon.KryptonRibbonRecentDocCollectionEditor, Krypton.Ribbon", typeof(UITypeEditor))]
        public virtual KryptonRibbonRecentDocCollection AppButtonRecentDocs { get; }

        #endregion

        #region AppButtonMinRecentSize
        /// <summary>
        /// Gets and sets the minimum size of the recent documents area of the application button.
        /// </summary>
        [Category(@"Values")]
        [Description(@"Minimum size of the recent documents area of the application button.")]
        [DefaultValue(typeof(Size), "250,250")]
        public Size AppButtonMinRecentSize { get; set; }

        #endregion

        #region AppButtonMaxRecentSize
        /// <summary>
        /// Gets and sets the maximum size of the recent documents area of the application button.
        /// </summary>
        [Category(@"Values")]
        [Description(@"Maximum size of the recent documents area of the application button.")]
        [DefaultValue(typeof(Size), "350,350")]
        public Size AppButtonMaxRecentSize { get; set; }

        #endregion

        #region AppButtonSpecs
        /// <summary>
        /// Gets the collection of button specifications.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Collection of button specifications for the app button context menu.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public AppMenuButtonSpecCollection AppButtonSpecs { get; }

        #endregion

        #region AppButtonShowRecentDocs
        /// <summary>
        /// Gets and sets if the recent documents area should be shown in the application button.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Determine if the recent documents area should be shown in the application button.")]
        [DefaultValue(true)]
        public bool AppButtonShowRecentDocs { get; set; }

        #endregion

        #region AppButtonToolTipStyle
        /// <summary>
        /// Gets and sets the tooltip label style for the application button.
        /// </summary>
        [Category(@"Appearance")]
        [Description(@"Tooltip style for the application button.")]
        [DefaultValue(typeof(LabelStyle), "SuperTip")]
        [Localizable(true)]
        public LabelStyle AppButtonToolTipStyle { get; set; }

        #endregion

        #region AppButtonToolTipImage
        /// <summary>
        /// Gets and sets the image for the item ToolTip.
        /// </summary>
        [Bindable(true)]
        [Category(@"Appearance")]
        [Description(@"Display image associated ToolTip.")]
        [DefaultValue(null)]
        [Localizable(true)]
        public Image AppButtonToolTipImage { get; set; }

        #endregion

        #region AppButtonToolTipImageTransparentColor
        /// <summary>
        /// Gets and sets the color to draw as transparent in the ToolTipImage.
        /// </summary>
        [Bindable(true)]
        [Category(@"Appearance")]
        [Description(@"Color to draw as transparent in the ToolTipImage.")]
        [KryptonDefaultColor()]
        [Localizable(true)]
        public Color AppButtonToolTipImageTransparentColor { get; set; }

        #endregion

        #region AppButtonToolTipTitle
        /// <summary>
        /// Gets and sets the title text for the item ToolTip.
        /// </summary>
        [Bindable(true)]
        [Category(@"Appearance")]
        [Description(@"Title text for use in associated ToolTip.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
        [DefaultValue("")]
        [Localizable(true)]
        public string AppButtonToolTipTitle { get; set; }

        #endregion

        #region AppButtonToolTipBody
        /// <summary>
        /// Gets and sets the body text for the item ToolTip.
        /// </summary>
        [Bindable(true)]
        [Category(@"Appearance")]
        [Description(@"Body text for use in associated ToolTip.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
        [DefaultValue("")]
        [Localizable(true)]
        public string AppButtonToolTipBody { get; set; }

        #endregion

        #region AppButtonVisible
        /// <summary>
        /// Gets and sets if the application button is shown.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Determine if the application button is shown.")]
        [DefaultValue(true)]
        public bool AppButtonVisible
        {
            get => _appButtonVisible;

            set
            {
                if (_appButtonVisible != value)
                {
                    _appButtonVisible = value;

                    if (_ribbon.CaptionArea != null)
                    {
                        _ribbon.TabsArea.AppButtonVisibleChanged();
                        _ribbon.CaptionArea.AppButtonVisibleChanged();
                        _ribbon.CaptionArea.PerformFormChromeCheck();
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// Does the application button perform "default theme Close" on double Click
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Does the application button perform 'default theme Close' on double Click.")]
        [DefaultValue(false)]
        public bool IgnoreDoubleClickClose
        {
            get => _ribbon.IgnoreDoubleClickClose;
            set => _ribbon.IgnoreDoubleClickClose = value;
        }
    }
}
