#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *
 */
#endregion

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable UnusedMember.Global

namespace Krypton.Toolkit
{
    /// <summary>
    /// Displays a message box that can contain text, buttons, and symbols that inform and instruct the user.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonMessageBox), "ToolboxBitmaps.KryptonMessageBox.bmp")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonMessageBox : KryptonForm
    {
        #region Types
        internal class HelpInfo
        {
            #region Instance Fields

            #endregion

            #region Identity

            /// <summary>
            /// Initialize a new instance of the HelpInfo class.
            /// </summary>
            /// <param name="helpFilePath">Value for HelpFilePath.</param>
            /// <param name="keyword">Value for Keyword</param>
            public HelpInfo(string helpFilePath = null, string keyword = null)
            : this(helpFilePath, keyword, !MissingFrameWorkAPIs.IsNullOrWhiteSpace(keyword) ? HelpNavigator.Topic : HelpNavigator.TableOfContents, null)
            {

            }

            /// <summary>
            /// Initialize a new instance of the HelpInfo class.
            /// </summary>
            /// <param name="helpFilePath">Value for HelpFilePath.</param>
            /// <param name="navigator">Value for Navigator</param>
            /// <param name="param"></param>
            public HelpInfo(string helpFilePath, HelpNavigator navigator, object param = null)
                : this(helpFilePath, null, navigator, param)
            {

            }

            /// <summary>
            /// Initialize a new instance of the HelpInfo class.
            /// </summary>
            /// <param name="helpFilePath">Value for HelpFilePath.</param>
            /// <param name="navigator">Value for Navigator</param>
            /// <param name="keyword">Value for Keyword</param>
            /// <param name="param"></param>
            private HelpInfo(string helpFilePath, string keyword, HelpNavigator navigator, object param)
            {
                HelpFilePath = helpFilePath;
                Keyword = keyword;
                Navigator = navigator;
                Param = param;
            }
            #endregion

            #region Properties
            /// <summary>
            /// Gets the HelpFilePath property.
            /// </summary>
            public string HelpFilePath { get; }

            /// <summary>
            /// Gets the Keyword property.
            /// </summary>
            public string Keyword { get; }

            /// <summary>
            /// Gets the Navigator property.
            /// </summary>
            public HelpNavigator Navigator { get; }

            /// <summary>
            /// Gets the Param property.
            /// </summary>
            public object Param { get; }

            #endregion
        }

        [ToolboxItem(false)]
        internal class MessageButton : KryptonButton
        {
            #region Instance Fields

            #endregion

            #region Identity
            public MessageButton()
            {
                IgnoreAltF4 = false;
                Visible = false;
                Enabled = false;
            }

            /// <summary>
            /// Gets and sets the ignoring of Alt+F4
            /// </summary>
            public bool IgnoreAltF4 { get; set; }

            #endregion

            #region Protected
            /// <summary>
            /// Processes Windows messages.
            /// </summary>
            /// <param name="m">The Windows Message to process. </param>
            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case PI.WM_.KEYDOWN:
                    case PI.WM_.SYSKEYDOWN:
                        if (IgnoreAltF4)
                        {
                            // Extract the keys being pressed
                            Keys keys = ((Keys)((int)m.WParam.ToInt64()));

                            // If the user standard combination ALT + F4
                            if ((keys == Keys.F4) && ((ModifierKeys & Keys.Alt) == Keys.Alt))
                            {
                                // Eat the message, so standard window proc does not close the window
                                return;
                            }
                        }
                        break;
                }

                base.WndProc(ref m);
            }
            #endregion
        }
        #endregion

        #region Static Fields

        private const int GAP = 10;
        private static readonly int _osMajorVersion;
        #endregion

        #region Instance Fields
        private readonly string _text;
        private readonly string _caption;
        private readonly MessageBoxButtons _buttons;
        private readonly KryptonMessageBoxIcon _kryptonMessageBoxIcon;

        #region WinForm Compatibility
        private readonly MessageBoxIcon _messageBoxIcon;
        #endregion

        private readonly MessageBoxDefaultButton _defaultButton;
        private MessageBoxOptions _options; // TODO: What is this used for ? e.g. MessageBoxOptions.RTL
        private KryptonPanel _panelMessage;
        private KryptonPanel _panelMessageText;
        private KryptonWrapLabel _messageText;
        private KryptonPanel _panelMessageIcon;
        private PictureBox _messageIcon;
        private KryptonPanel _panelButtons;
        private MessageButton _button1;
        private MessageButton _button2;
        private MessageButton _button3;
        private MessageButton _button4;
        private KryptonBorderEdge _borderEdge;
        private readonly HelpInfo _helpInfo;
        // If help information provided or we are not a service/default desktop application then grab an owner for showing the message box
        private readonly IWin32Window _showOwner;
        private readonly float _cornerRadius;
        private readonly int _blurRadius;
        private readonly bool _useBlur;
        private readonly KryptonForm _parentWindow;
        #endregion

        #region Identity
        static KryptonMessageBox()
        {
            _osMajorVersion = Environment.OSVersion.Version.Major;
        }

        private KryptonMessageBox(IWin32Window showOwner, string text, string caption,
            MessageBoxButtons buttons, KryptonMessageBoxIcon icon,
            MessageBoxDefaultButton defaultButton, MessageBoxOptions options,
            HelpInfo helpInfo, bool? showCtrlCopy, float? cornerRadius, bool? useBlur,
            int? blurRadius, KryptonForm parentWindow = null)
        {
            // Store incoming values
            _text = text;
            _caption = caption;
            _buttons = buttons;
            _kryptonMessageBoxIcon = icon;
            _defaultButton = defaultButton;
            _options = options;
            _helpInfo = helpInfo;
            _showOwner = showOwner;
            _cornerRadius = cornerRadius ?? GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE;
            _useBlur = useBlur ?? false;
            _blurRadius = blurRadius ?? 0;
            _parentWindow = parentWindow;

            // Create the form contents
            InitializeComponent();

            // Update contents to match requirements
            UpdateText();
            UpdateIcon();
            UpdateButtons();
            UpdateDefault();
            UpdateHelp();
            UpdateTextExtra(showCtrlCopy);

            // Finally calculate and set form sizing
            UpdateSizing(showOwner);

            // Define a corner radius, default is GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE
            StateCommon.Border.Rounding = _cornerRadius;

            // Blur window
            if (_useBlur)
            {
                if (showOwner is KryptonForm)
                {
                    _parentWindow = (KryptonForm)showOwner;

                    _parentWindow.BlurValues.EnableBlur = _useBlur;

                    _parentWindow.BlurValues.BlurWhenFocusLost = _useBlur;

                    _parentWindow.BlurValues.Radius = Convert.ToByte(_blurRadius);
                }
                else if (_parentWindow != null)
                {
                    _parentWindow.BlurValues.EnableBlur = _useBlur;

                    _parentWindow.BlurValues.BlurWhenFocusLost = _useBlur;

                    _parentWindow.BlurValues.Radius = Convert.ToByte(_blurRadius);
                }
            }
        }

        #region WinForm Compatibility
        private KryptonMessageBox(IWin32Window showOwner, string text, string caption,
            MessageBoxButtons buttons, MessageBoxIcon icon,
            MessageBoxDefaultButton defaultButton, MessageBoxOptions options,
            HelpInfo helpInfo, bool? showCtrlCopy, float? cornerRadius, bool? useBlur,
            int? blurRadius, KryptonForm parentWindow = null)
        {
            // Store incoming values
            _text = text;
            _caption = caption;
            _buttons = buttons;
            _messageBoxIcon = icon;
            _defaultButton = defaultButton;
            _options = options;
            _helpInfo = helpInfo;
            _showOwner = showOwner;
            _cornerRadius = cornerRadius ?? GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE;
            _useBlur = useBlur ?? false;
            _blurRadius = blurRadius ?? 0;
            _parentWindow = parentWindow;

            // Create the form contents
            InitializeComponent();

            // Update contents to match requirements
            UpdateText();
            UpdateIcon();
            UpdateButtons();
            UpdateDefault();
            UpdateHelp();
            UpdateTextExtra(showCtrlCopy);

            // Finally calculate and set form sizing
            UpdateSizing(showOwner);

            // Define a corner radius, default is GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE
            StateCommon.Border.Rounding = _cornerRadius;

            // Blur window
            if (_useBlur)
            {
                if (showOwner is KryptonForm)
                {
                    _parentWindow = (KryptonForm)showOwner;

                    _parentWindow.BlurValues.EnableBlur = _useBlur;

                    _parentWindow.BlurValues.BlurWhenFocusLost = _useBlur;

                    _parentWindow.BlurValues.Radius = Convert.ToByte(_blurRadius);
                }
                else if (_parentWindow != null)
                {
                    _parentWindow.BlurValues.EnableBlur = _useBlur;

                    _parentWindow.BlurValues.BlurWhenFocusLost = _useBlur;

                    _parentWindow.BlurValues.Radius = Convert.ToByte(_blurRadius);
                }
            }
        }
        #endregion

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public

        /// <summary>
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, string.Empty, MessageBoxButtons.OK, KryptonMessageBoxIcon.NONE, MessageBoxDefaultButton.Button1, 0, null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified text.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(owner, text, string.Empty, MessageBoxButtons.OK, KryptonMessageBoxIcon.NONE, MessageBoxDefaultButton.Button1, 0, null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with specified text and caption.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, MessageBoxButtons.OK, KryptonMessageBoxIcon.NONE, MessageBoxDefaultButton.Button1, 0, null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified text and caption.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(owner, text, caption, MessageBoxButtons.OK, KryptonMessageBoxIcon.NONE, MessageBoxDefaultButton.Button1, 0, null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with specified text, caption, and buttons.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, KryptonMessageBoxIcon.NONE, MessageBoxDefaultButton.Button1, 0, null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified text, caption, and buttons.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(owner, text, caption, buttons, KryptonMessageBoxIcon.NONE, MessageBoxDefaultButton.Button1, 0, null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, 0, null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(owner, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, 0, null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, and default button.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, MessageBoxDefaultButton defaultButton, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, defaultButton, 0, null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified text, caption, buttons, icon, and default button.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, MessageBoxDefaultButton defaultButton, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(owner, text, caption, buttons, icon, defaultButton, 0, null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, defaultButton, options, null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="displayHelpButton">Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool displayHelpButton, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, defaultButton, options, displayHelpButton ? new HelpInfo() : null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="displayHelpButton">Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool displayHelpButton, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, defaultButton, options, displayHelpButton ? new HelpInfo() : null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath), showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(owner, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath), showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and HelpNavigator.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, navigator), showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and Help keyword.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="keyword">The Help keyword to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, keyword), showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and HelpNavigator.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(owner, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, navigator), showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and Help keyword.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="keyword">The Help keyword to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(owner, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, keyword), showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, HelpNavigator, and Help topic.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, navigator, param), showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, HelpNavigator, and Help topic.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, KryptonMessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(owner, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, navigator, param), showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        #region WinForms Compatible
        /// <summary>
        /// Displays a message box with specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, 0, null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(owner, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, 0, null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, and default button.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, defaultButton, 0, null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified text, caption, buttons, icon, and default button.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(owner, text, caption, buttons, icon, defaultButton, 0, null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, defaultButton, options, null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="displayHelpButton">Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool displayHelpButton, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, defaultButton, options, displayHelpButton ? new HelpInfo() : null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="displayHelpButton">Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool displayHelpButton, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, defaultButton, options, displayHelpButton ? new HelpInfo() : null, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath), showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(owner, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath), showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and HelpNavigator.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, navigator), showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and Help keyword.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="keyword">The Help keyword to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, keyword), showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and HelpNavigator.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(owner, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, navigator), showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and Help keyword.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="keyword">The Help keyword to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(owner, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, keyword), showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, HelpNavigator, and Help topic.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(null, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, navigator, param), showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, HelpNavigator, and Help topic.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param, bool? showCtrlCopy = null, float? cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE, bool? useBlur = false,
                                        int? blurRadius = 0, KryptonForm parentWindow = null) => InternalShow(owner, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, navigator, param), showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow);
        #endregion
        #endregion

        #region Implementation
        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, HelpNavigator, and Help topic.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.KryptonMessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpInfo">Contains the help data of the <see cref="KryptonMessageBox"/>.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        private static DialogResult InternalShow(IWin32Window owner,
                                                 string text, string caption,
                                                 MessageBoxButtons buttons,
                                                 KryptonMessageBoxIcon icon,
                                                 MessageBoxDefaultButton defaultButton,
                                                 MessageBoxOptions options,
                                                 HelpInfo helpInfo, bool? showCtrlCopy,
                                                 float? cornerRadius, bool? useBlur,
                                                 int? blurRadius, KryptonForm parentWindow = null)
        {
            // Check if trying to show a message box from a non-interactive process, this is not possible
            if (!SystemInformation.UserInteractive && ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) == 0))
            {
                throw new InvalidOperationException("Cannot show modal dialog when non-interactive");
            }

            // Check if trying to show a message box from a service and the owner has been specified, this is not possible
            if ((owner != null) && ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) != 0))
            {
                throw new ArgumentException(@"Cannot show message box from a service with an owner specified", nameof(options));
            }

            // Check if trying to show a message box from a service and help information is specified, this is not possible
            if ((helpInfo != null) && ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) != 0))
            {
                throw new ArgumentException(@"Cannot show message box from a service with help specified", nameof(options));
            }

            IWin32Window showOwner = null;
            if ((helpInfo != null) || ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) == 0))
            {
                // If do not have an owner passed in then get the active window and use that instead
                showOwner = owner ?? FromHandle(PI.GetActiveWindow());
            }

            // Show message box window as a modal dialog and then dispose of it afterwards
            using (KryptonMessageBox kmb = new(showOwner, text, caption, buttons, icon, defaultButton, options, helpInfo, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow))
            {
                kmb.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

                return kmb.ShowDialog(showOwner);
            }
        }

        #region WinForm Compatibility
        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, HelpNavigator, and Help topic.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpInfo">Contains the help data of the <see cref="KryptonMessageBox"/>.</param>
        /// <param name="showCtrlCopy">Show extraText in title. If null(default) then only when Warning or Error icon is used.</param>
        /// <param name="cornerRadius">The corner radius of the <see cref="KryptonMessageBox"/>. Default and minimum value possible is 'GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE'.</param>
        /// <param name="useBlur">Uses a blur effect on the 'parentWindow' if it is not null.</param>
        /// <param name="blurRadius">The radius of the blur effect on the 'parentWindow' if it is enabled. By default, it is set to '0'.</param>
        /// <param name="parentWindow">The window to utilise the blurring effect on. If not set as the default value (null), it'll override the 'owner' value.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        private static DialogResult InternalShow(IWin32Window owner,
                                                 string text, string caption,
                                                 MessageBoxButtons buttons,
                                                 MessageBoxIcon icon,
                                                 MessageBoxDefaultButton defaultButton,
                                                 MessageBoxOptions options,
                                                 HelpInfo helpInfo, bool? showCtrlCopy,
                                                 float? cornerRadius, bool? useBlur,
                                                 int? blurRadius, KryptonForm parentWindow = null)
        {
            // Check if trying to show a message box from a non-interactive process, this is not possible
            if (!SystemInformation.UserInteractive && ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) == 0))
            {
                throw new InvalidOperationException("Cannot show modal dialog when non-interactive");
            }

            // Check if trying to show a message box from a service and the owner has been specified, this is not possible
            if ((owner != null) && ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) != 0))
            {
                throw new ArgumentException(@"Cannot show message box from a service with an owner specified", nameof(options));
            }

            // Check if trying to show a message box from a service and help information is specified, this is not possible
            if ((helpInfo != null) && ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) != 0))
            {
                throw new ArgumentException(@"Cannot show message box from a service with help specified", nameof(options));
            }

            IWin32Window showOwner = null;
            if ((helpInfo != null) || ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) == 0))
            {
                // If do not have an owner passed in then get the active window and use that instead
                showOwner = owner ?? FromHandle(PI.GetActiveWindow());
            }

            // Show message box window as a modal dialog and then dispose of it afterwards
            using (KryptonMessageBox kmb = new(showOwner, text, caption, buttons, icon, defaultButton, options, helpInfo, showCtrlCopy, cornerRadius, useBlur, blurRadius, parentWindow))
            {
                kmb.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

                return kmb.ShowDialog(showOwner);
            }
        }
        #endregion

        private void UpdateText()
        {
            Text = (string.IsNullOrEmpty(_caption) ? string.Empty : _caption.Split(Environment.NewLine.ToCharArray())[0]);
            _messageText.Text = _text;
        }

        private void UpdateTextExtra(bool? showCtrlCopy)
        {
            if (!showCtrlCopy.HasValue)
            {
                switch (_kryptonMessageBoxIcon)
                {
                    case KryptonMessageBoxIcon.ERROR:
                    case KryptonMessageBoxIcon.EXCLAMATION:
                        showCtrlCopy = true;
                        break;
                }

                switch (_messageBoxIcon)
                {
                    case MessageBoxIcon.Error:
                    case MessageBoxIcon.Exclamation:
                        showCtrlCopy = true;
                        break;
                }
            }

            if (showCtrlCopy == true)
            {
                TextExtra = @"Ctrl+c to copy";
            }
        }

        private void UpdateIcon()
        {
            switch (_kryptonMessageBoxIcon)
            {
                case KryptonMessageBoxIcon.NONE:
                    _panelMessageIcon.Visible = false;
                    _panelMessageText.Left -= _messageIcon.Right;

                    // Windows XP and before will Beep, Vista and above do not!
                    if (_osMajorVersion < 6)
                    {
                        SystemSounds.Beep.Play();
                    }

                    break;
                case KryptonMessageBoxIcon.QUESTION:
                    _messageIcon.Image = MessageBoxResources.Question;
                    SystemSounds.Question.Play();
                    break;
                case KryptonMessageBoxIcon.INFORMATION:
                    _messageIcon.Image = MessageBoxResources.MBInformation;
                    SystemSounds.Asterisk.Play();
                    break;
                case KryptonMessageBoxIcon.WARNING:
                    _messageIcon.Image = MessageBoxResources.Warning;
                    SystemSounds.Exclamation.Play();
                    break;
                case KryptonMessageBoxIcon.ERROR:
                    _messageIcon.Image = MessageBoxResources.Critical;
                    SystemSounds.Hand.Play();
                    break;
                case KryptonMessageBoxIcon.ASTERISK:
                    _messageIcon.Image = MessageBoxResources.Asterisk;
                    SystemSounds.Asterisk.Play();
                    break;
                case KryptonMessageBoxIcon.HAND:
                    _messageIcon.Image = MessageBoxResources.Hand;
                    SystemSounds.Hand.Play();
                    break;
                case KryptonMessageBoxIcon.STOP:
                    _messageIcon.Image = MessageBoxResources.Stop;
                    SystemSounds.Hand.Play();
                    break;
                case KryptonMessageBoxIcon.SHIELD:
                    _messageIcon.Image = MessageBoxResources.UAC_Shield;
                    break;
                case KryptonMessageBoxIcon.WINDOWSLOGO:
                    if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1)
                    {
                        // Use Windows 7 icon
                        _messageIcon.Image = MessageBoxResources.Windows_7;
                    }
                    else if (Environment.OSVersion.Version.Major >= 6 && Environment.OSVersion.Version.Minor >= 3)
                    {
                        // Use Windows 8/8.1/10 icon
                        _messageIcon.Image = MessageBoxResources.Windows_10;
                    }
                    else
                    {
                        // Use Windows 11 icon

                        // TODO: Windows 11 icon
                    }
                    break;
            }

            switch (_messageBoxIcon)
            {
                case MessageBoxIcon.None:
                    _panelMessageIcon.Visible = false;
                    _panelMessageText.Left -= _messageIcon.Right;

                    // Windows XP and before will Beep, Vista and above do not!
                    if (_osMajorVersion < 6)
                    {
                        SystemSounds.Beep.Play();
                    }
                    break;
                case MessageBoxIcon.Question:
                    _messageIcon.Image = MessageBoxResources.Question;
                    SystemSounds.Question.Play();
                    break;
                case MessageBoxIcon.Error:
                    _messageIcon.Image = MessageBoxResources.Critical;

                    SystemSounds.Asterisk.Play();
                    break;
                case MessageBoxIcon.Warning:
                    _messageIcon.Image = MessageBoxResources.Warning;

                    SystemSounds.Exclamation.Play();
                    break;
                case MessageBoxIcon.Information:
                    _messageIcon.Image = MessageBoxResources.MBInformation;

                    SystemSounds.Asterisk.Play();
                    break;
            }
        }

        private void UpdateButtons()
        {
            switch (_buttons)
            {
                case MessageBoxButtons.OK:
                    _button1.Text = KryptonManager.Strings.OK;
                    _button1.DialogResult = DialogResult.OK;
                    _button1.Visible = true;
                    _button1.Enabled = true;
                    break;
                case MessageBoxButtons.OKCancel:
                    _button1.Text = KryptonManager.Strings.OK;
                    _button2.Text = KryptonManager.Strings.Cancel;
                    _button1.DialogResult = DialogResult.OK;
                    _button2.DialogResult = DialogResult.Cancel;
                    _button1.Visible = true;
                    _button1.Enabled = true;
                    _button2.Visible = true;
                    _button2.Enabled = true;
                    break;
                case MessageBoxButtons.YesNo:
                    _button1.Text = KryptonManager.Strings.Yes;
                    _button2.Text = KryptonManager.Strings.No;
                    _button1.DialogResult = DialogResult.Yes;
                    _button2.DialogResult = DialogResult.No;
                    _button1.Visible = true;
                    _button1.Enabled = true;
                    _button2.Visible = true;
                    _button2.Enabled = true;
                    ControlBox = false;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    _button1.Text = KryptonManager.Strings.Yes;
                    _button2.Text = KryptonManager.Strings.No;
                    _button3.Text = KryptonManager.Strings.Cancel;
                    _button1.DialogResult = DialogResult.Yes;
                    _button2.DialogResult = DialogResult.No;
                    _button3.DialogResult = DialogResult.Cancel;
                    _button1.Visible = true;
                    _button1.Enabled = true;
                    _button2.Visible = true;
                    _button2.Enabled = true;
                    _button3.Visible = true;
                    _button3.Enabled = true;
                    break;
                case MessageBoxButtons.RetryCancel:
                    _button1.Text = KryptonManager.Strings.Retry;
                    _button2.Text = KryptonManager.Strings.Cancel;
                    _button1.DialogResult = DialogResult.Retry;
                    _button2.DialogResult = DialogResult.Cancel;
                    _button1.Visible = true;
                    _button1.Enabled = true;
                    _button2.Visible = true;
                    _button2.Enabled = true;
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    _button1.Text = KryptonManager.Strings.Abort;
                    _button2.Text = KryptonManager.Strings.Retry;
                    _button3.Text = KryptonManager.Strings.Ignore;
                    _button1.DialogResult = DialogResult.Abort;
                    _button2.DialogResult = DialogResult.Retry;
                    _button3.DialogResult = DialogResult.Ignore;
                    _button1.Visible = true;
                    _button1.Enabled = true;
                    _button2.Visible = true;
                    _button2.Enabled = true;
                    _button3.Visible = true;
                    _button3.Enabled = true;
                    ControlBox = false;
                    break;
            }

            // Do we ignore the Alt+F4 on the buttons?
            if (!ControlBox)
            {
                _button1.IgnoreAltF4 = true;
                _button2.IgnoreAltF4 = true;
                _button3.IgnoreAltF4 = true;
                _button4.IgnoreAltF4 = true;
            }
        }

        private void UpdateDefault()
        {
            switch (_defaultButton)
            {
                case MessageBoxDefaultButton.Button2:
                    _button2.Select();
                    break;
                case MessageBoxDefaultButton.Button3:
                    _button3.Select();
                    break;
            }
        }

        private void UpdateHelp()
        {
            if (_helpInfo == null)
            {
                return;
            }

            MessageButton helpButton;
            switch (_buttons)
            {
                case MessageBoxButtons.OK:
                    helpButton = _button2;
                    break;
                case MessageBoxButtons.OKCancel:
                case MessageBoxButtons.YesNo:
                case MessageBoxButtons.RetryCancel:
                    helpButton = _button3;
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                case MessageBoxButtons.YesNoCancel:
                    helpButton = _button4;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (helpButton != null)
            {
                helpButton.Visible = true;
                helpButton.Enabled = true;
                helpButton.Text = KryptonManager.Strings.Help;
                helpButton.KeyPress += (sender, args) => LaunchHelp();
                helpButton.Click += (sender, args) => LaunchHelp();
            }
        }

        /// <summary>
        /// When the user clicks the Help button, the Help file specified in the helpFilePath parameter
        /// is opened and the Help keyword topic identified by the keyword parameter is displayed.
        /// The form that owns the message box (or the active form) also receives the HelpRequested event.
        /// </summary>
        private void LaunchHelp()
        {
            try
            {
                Control control = FromHandle(_showOwner.Handle);

                MethodInfo mInfoMethod = control.GetType().GetMethod(@"OnHelpRequested", BindingFlags.Instance | BindingFlags.NonPublic,
                    Type.DefaultBinder, new[] { typeof(HelpEventArgs) }, null);
                if (mInfoMethod != null)
                {
                    mInfoMethod.Invoke(control, new object[] { new HelpEventArgs(MousePosition) });
                }
                if (MissingFrameWorkAPIs.IsNullOrWhiteSpace(_helpInfo.HelpFilePath))
                {
                    return;
                }

                if (!MissingFrameWorkAPIs.IsNullOrWhiteSpace(_helpInfo.Keyword))
                {
                    Help.ShowHelp(control, _helpInfo.HelpFilePath, _helpInfo.Keyword);
                }
                else
                {
                    Help.ShowHelp(control, _helpInfo.HelpFilePath, _helpInfo.Navigator, _helpInfo.Param);
                }
            }
            catch
            {
                // Do nothing if failure to send to Parent
            }

        }

        private void UpdateSizing(IWin32Window showOwner)
        {
            Size messageSizing = UpdateMessageSizing(showOwner);
            Size buttonsSizing = UpdateButtonsSizing();

            // Size of window is calculated from the client area
            ClientSize = new Size(Math.Max(messageSizing.Width, buttonsSizing.Width),
                                  messageSizing.Height + buttonsSizing.Height);
        }

        private Size UpdateMessageSizing(IWin32Window showOwner)
        {
            // Update size of the message label but with a maximum width
            using (Graphics g = CreateGraphics())
            {
                // Find size of the label, with a max of 2/3 screen width
                Screen screen = showOwner != null ? Screen.FromHandle(showOwner.Handle) : Screen.PrimaryScreen;
                SizeF scaledMonitorSize = screen.Bounds.Size;
                scaledMonitorSize.Width *= (2 / 3.0f);
                scaledMonitorSize.Height *= 0.95f;
                _messageText.UpdateFont();
                SizeF messageSize = g.MeasureString(_text, _messageText.Font, scaledMonitorSize);
                // SKC: Don't forget to add the TextExtra into the calculation
                SizeF captionSize = g.MeasureString($@"{_caption} {TextExtra}", _messageText.Font, scaledMonitorSize);

                float messageXSize = Math.Max(messageSize.Width, captionSize.Width);
                // Work out DPI adjustment factor
                float factorX = g.DpiX > 96 ? ((1.0f * g.DpiX) / 96) : 1.0f;
                float factorY = g.DpiY > 96 ? ((1.0f * g.DpiY) / 96) : 1.0f;
                messageSize.Width = messageXSize * factorX;
                messageSize.Height = messageSize.Height * factorY;

                // Always add on ad extra 5 pixels as sometimes the measure size does not draw the last 
                // character it contains, this ensures there is always definitely enough space for it all
                messageSize.Width += 5;
                _messageText.Size = Size.Ceiling(messageSize);
            }

            // Resize panel containing the message text
            Padding panelMessagePadding = _panelMessageText.Padding;
            _panelMessageText.Width = _messageText.Size.Width + panelMessagePadding.Horizontal;
            _panelMessageText.Height = _messageText.Size.Height + panelMessagePadding.Vertical;

            // Find size of icon area plus the text area added together
            Size panelSize = _panelMessageText.Size;
            if (_messageIcon.Image != null)
            {
                panelSize.Width += _panelMessageIcon.Width;
                panelSize.Height = Math.Max(panelSize.Height, _panelMessageIcon.Height);
            }

            // Enforce a minimum size for the message area
            panelSize = new Size(Math.Max(_panelMessage.Size.Width, panelSize.Width),
                                 Math.Max(_panelMessage.Size.Height, panelSize.Height));

            // Note that the width will be ignored in this update, but that is fine as 
            // it will be adjusted by the UpdateSizing method that is the caller.
            _panelMessage.Size = panelSize;
            return panelSize;
        }

        private Size UpdateButtonsSizing()
        {
            int numButtons = 1;

            // Button1 is always visible
            Size button1Size = _button1.GetPreferredSize(Size.Empty);
            Size maxButtonSize = new(button1Size.Width + GAP, button1Size.Height);

            // If Button2 is visible
            if (_button2.Enabled)
            {
                numButtons++;
                Size button2Size = _button2.GetPreferredSize(Size.Empty);
                maxButtonSize.Width = Math.Max(maxButtonSize.Width, button2Size.Width + GAP);
                maxButtonSize.Height = Math.Max(maxButtonSize.Height, button2Size.Height);
            }

            // If Button3 is visible
            if (_button3.Enabled)
            {
                numButtons++;
                Size button3Size = _button3.GetPreferredSize(Size.Empty);
                maxButtonSize.Width = Math.Max(maxButtonSize.Width, button3Size.Width + GAP);
                maxButtonSize.Height = Math.Max(maxButtonSize.Height, button3Size.Height);
            }
            // If Button4 is visible
            if (_button4.Enabled)
            {
                numButtons++;
                Size button4Size = _button4.GetPreferredSize(Size.Empty);
                maxButtonSize.Width = Math.Max(maxButtonSize.Width, button4Size.Width + GAP);
                maxButtonSize.Height = Math.Max(maxButtonSize.Height, button4Size.Height);
            }

            // Start positioning buttons 10 pixels from right edge
            int right = _panelButtons.Right - GAP;

            // If Button4 is visible
            if (_button4.Enabled)
            {
                _button4.Location = new Point(right - maxButtonSize.Width, GAP);
                _button4.Size = maxButtonSize;
                right -= maxButtonSize.Width + GAP;
            }

            // If Button3 is visible
            if (_button3.Enabled)
            {
                _button3.Location = new Point(right - maxButtonSize.Width, GAP);
                _button3.Size = maxButtonSize;
                right -= maxButtonSize.Width + GAP;
            }

            // If Button2 is visible
            if (_button2.Enabled)
            {
                _button2.Location = new Point(right - maxButtonSize.Width, GAP);
                _button2.Size = maxButtonSize;
                right -= maxButtonSize.Width + GAP;
            }

            // Button1 is always visible
            _button1.Location = new Point(right - maxButtonSize.Width, GAP);
            _button1.Size = maxButtonSize;

            // Size the panel for the buttons
            _panelButtons.Size = new Size((maxButtonSize.Width * numButtons) + (GAP * (numButtons + 1)), maxButtonSize.Height + (GAP * 2));

            // Button area is the number of buttons with gaps between them and 10 pixels around all edges
            return new Size((maxButtonSize.Width * numButtons) + (GAP * (numButtons + 1)), maxButtonSize.Height + (GAP * 2));
        }

        private void button_keyDown(object sender, KeyEventArgs e)
        {
            // Escape key kills the dialog if we allow it to be closed
            if ((e.KeyCode == Keys.Escape) && ControlBox)
            {
                Close();
            }
            else
            {
                // Pressing Ctrl+C should copy message text into the clipboard
                if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.C))
                {
                    StringBuilder sb = new();

                    sb.AppendLine("---------------------------");
                    sb.AppendLine(_caption);
                    sb.AppendLine("---------------------------");
                    sb.AppendLine(_text);
                    sb.AppendLine("---------------------------");
                    sb.Append(_button1.Text);
                    sb.Append("   ");
                    if (_button2.Enabled)
                    {
                        sb.Append(_button2.Text);
                        sb.Append("   ");
                        if (_button3.Enabled)
                        {
                            sb.Append(_button3.Text);
                            sb.Append("   ");
                        }
                        if (_button4.Enabled)
                        {
                            sb.Append(_button4.Text);
                            sb.Append("   ");
                        }
                    }
                    sb.AppendLine("");
                    sb.AppendLine("---------------------------");

                    Clipboard.SetText(sb.ToString(), TextDataFormat.Text);
                    Clipboard.SetText(sb.ToString(), TextDataFormat.UnicodeText);
                }
            }
        }

        private void InitializeComponent()
        {
            _panelMessage = new KryptonPanel();
            _panelMessageText = new KryptonPanel();
            _messageText = new KryptonWrapLabel();
            _panelMessageIcon = new KryptonPanel();
            _messageIcon = new PictureBox();
            _panelButtons = new KryptonPanel();
            _borderEdge = new KryptonBorderEdge();
            _button4 = new MessageButton();
            _button3 = new MessageButton();
            _button1 = new MessageButton();
            _button2 = new MessageButton();
            ((ISupportInitialize)(_panelMessage)).BeginInit();
            _panelMessage.SuspendLayout();
            ((ISupportInitialize)(_panelMessageText)).BeginInit();
            _panelMessageText.SuspendLayout();
            ((ISupportInitialize)(_panelMessageIcon)).BeginInit();
            _panelMessageIcon.SuspendLayout();
            ((ISupportInitialize)(_messageIcon)).BeginInit();
            ((ISupportInitialize)(_panelButtons)).BeginInit();
            _panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // _panelMessage
            // 
            _panelMessage.AutoSize = true;
            _panelMessage.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _panelMessage.Controls.Add(_panelMessageText);
            _panelMessage.Controls.Add(_panelMessageIcon);
            _panelMessage.Dock = DockStyle.Top;
            _panelMessage.Location = new Point(0, 0);
            _panelMessage.Name = "_panelMessage";
            _panelMessage.Size = new Size(156, 52);
            _panelMessage.TabIndex = 0;
            // 
            // _panelMessageText
            // 
            _panelMessageText.AutoSize = true;
            _panelMessageText.Controls.Add(_messageText);
            _panelMessageText.Location = new Point(42, 0);
            _panelMessageText.Margin = new Padding(0);
            _panelMessageText.Name = "_panelMessageText";
            _panelMessageText.Padding = new Padding(5, 17, 5, 17);
            _panelMessageText.Size = new Size(88, 52);
            _panelMessageText.TabIndex = 1;
            // 
            // _messageText
            // 
            _messageText.AutoSize = false;
            _messageText.Font = new Font(@"Segoe UI", 9F);
            _messageText.ForeColor = Color.FromArgb(30, 57, 91);
            _messageText.LabelStyle = LabelStyle.NormalPanel;
            _messageText.Location = new Point(5, 18);
            _messageText.Margin = new Padding(0);
            _messageText.Name = "_messageText";
            _messageText.Size = new Size(78, 15);
            _messageText.Text = @"Message Text";
            // 
            // _panelMessageIcon
            // 
            _panelMessageIcon.AutoSize = true;
            _panelMessageIcon.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _panelMessageIcon.Controls.Add(_messageIcon);
            _panelMessageIcon.Location = new Point(0, 0);
            _panelMessageIcon.Margin = new Padding(0);
            _panelMessageIcon.Name = "_panelMessageIcon";
            _panelMessageIcon.Padding = new Padding(10, 10, 0, 10);
            _panelMessageIcon.Size = new Size(42, 52);
            _panelMessageIcon.TabIndex = 0;
            // 
            // _messageIcon
            // 
            _messageIcon.BackColor = Color.Transparent;
            _messageIcon.Location = new Point(10, 10);
            _messageIcon.Margin = new Padding(0);
            _messageIcon.Name = "_messageIcon";
            _messageIcon.Size = new Size(32, 32);
            _messageIcon.TabIndex = 0;
            _messageIcon.TabStop = false;
            // 
            // _panelButtons
            // 
            _panelButtons.Controls.Add(_borderEdge);
            _panelButtons.Controls.Add(_button4);
            _panelButtons.Controls.Add(_button3);
            _panelButtons.Controls.Add(_button1);
            _panelButtons.Controls.Add(_button2);
            _panelButtons.Dock = DockStyle.Top;
            _panelButtons.Location = new Point(0, 52);
            _panelButtons.Margin = new Padding(0);
            _panelButtons.Name = "_panelButtons";
            _panelButtons.PanelBackStyle = PaletteBackStyle.PanelAlternate;
            _panelButtons.Size = new Size(156, 26);
            _panelButtons.TabIndex = 0;
            // 
            // borderEdge
            // 
            _borderEdge.BorderStyle = PaletteBorderStyle.HeaderPrimary;
            _borderEdge.Dock = DockStyle.Top;
            _borderEdge.Location = new Point(0, 0);
            _borderEdge.Name = "_borderEdge";
            _borderEdge.Size = new Size(156, 1);
            _borderEdge.Text = @"kryptonBorderEdge1";
            // 
            // _button4
            // 
            _button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _button4.AutoSize = true;
            _button4.IgnoreAltF4 = false;
            _button4.Location = new Point(156, 0);
            _button4.Margin = new Padding(0);
            _button4.MinimumSize = new Size(50, 26);
            _button4.Name = "_button4";
            _button4.Size = new Size(50, 26);
            _button4.TabIndex = 2;
            _button4.Values.Text = @"B4";
            _button4.KeyDown += button_keyDown;
            // 
            // _button3
            // 
            _button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _button3.AutoSize = true;
            _button3.IgnoreAltF4 = false;
            _button3.Location = new Point(106, 0);
            _button3.Margin = new Padding(0);
            _button3.MinimumSize = new Size(50, 26);
            _button3.Name = "_button3";
            _button3.Size = new Size(50, 26);
            _button3.TabIndex = 2;
            _button3.Values.Text = @"B3";
            _button3.KeyDown += button_keyDown;
            // 
            // _button1
            // 
            _button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _button1.AutoSize = true;
            _button1.IgnoreAltF4 = false;
            _button1.Location = new Point(6, 0);
            _button1.Margin = new Padding(0);
            _button1.MinimumSize = new Size(50, 26);
            _button1.Name = "_button1";
            _button1.Size = new Size(50, 26);
            _button1.TabIndex = 0;
            _button1.Values.Text = @"B1";
            _button1.KeyDown += button_keyDown;
            // 
            // _button2
            // 
            _button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _button2.AutoSize = true;
            _button2.IgnoreAltF4 = false;
            _button2.Location = new Point(56, 0);
            _button2.Margin = new Padding(0);
            _button2.MinimumSize = new Size(50, 26);
            _button2.Name = "_button2";
            _button2.Size = new Size(50, 26);
            _button2.TabIndex = 1;
            _button2.Values.Text = @"B2";
            _button2.KeyDown += button_keyDown;
            // 
            // KryptonMessageBox
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(156, 78);
            Controls.Add(_panelButtons);
            Controls.Add(_panelMessage);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "KryptonMessageBox";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            TopMost = true;
            ((ISupportInitialize)(_panelMessage)).EndInit();
            _panelMessage.ResumeLayout(false);
            _panelMessage.PerformLayout();
            ((ISupportInitialize)(_panelMessageText)).EndInit();
            _panelMessageText.ResumeLayout(false);
            ((ISupportInitialize)(_panelMessageIcon)).EndInit();
            _panelMessageIcon.ResumeLayout(false);
            ((ISupportInitialize)(_messageIcon)).EndInit();
            ((ISupportInitialize)(_panelButtons)).EndInit();
            _panelButtons.ResumeLayout(false);
            _panelButtons.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }
        #endregion
    }
}
