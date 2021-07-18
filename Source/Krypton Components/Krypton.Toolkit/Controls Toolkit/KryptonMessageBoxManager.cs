#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 10th May, 2021 @ 10:00 GMT
 *
 */
#endregion


namespace Krypton.Toolkit
{
    /// <summary>Displays a new KryptonMessageBox, configurable through the designer.</summary>
    [ToolboxBitmap(typeof(KryptonMessageBox), "ToolboxBitmaps.KryptonMessageBox.bmp"), Description("Displays a new KryptonMessageBox, configurable through the designer.")]
    public class KryptonMessageBoxManager : Component
    {
        #region Variables
        private bool _useBlur, _useControlCopy;

        private string _caption, _text, _helpFilePath;

        private MessageBoxButtons _buttons;

        private MessageBoxDefaultButton _defaultButton;

        private KryptonMessageBoxIcon _icon;

        private MessageBoxOptions _options;

        private HelpNavigator _navigator;

        private object _param;

        private int _blurRadius;

        private float _cornerRadius;

        private IWin32Window _owner;

        private KryptonForm _parentWindow;
        #endregion

        #region Properties
        /// <summary>Use the blur feature on the parent form when the KryptonMessageBox is shown.</summary>
        /// <value><c>true</c> if [use blur]; otherwise, <c>false</c>.</value>
        [DefaultValue(false), Description("Use the blur feature on the parent form when the KryptonMessageBox is shown.")]
        public bool UseBlur { get => _useBlur; set => _useBlur = value; }

        /// <summary>Use the 'Ctrl + C' KryptonMessageBox feature.</summary>
        /// <value><c>true</c> if [use Ctrl + C]; otherwise, <c>false</c>.</value>
        [DefaultValue(false), Description("Use the 'Ctrl + C' KryptonMessageBox feature.")]
        public bool UseControlCopy { get => _useControlCopy; set => _useControlCopy = value; }

        /// <summary>Gets or sets the caption of the KryptonMessageBox.</summary>
        /// <value>The caption of the KryptonMessageBox.</value>
        [DefaultValue(""), Description("Sets the caption of the KryptonMessageBox.")]
        public string Caption { get => _caption; set => _caption = value; }

        /// <summary>Gets or sets the message text of the KryptonMessageBox.</summary>
        /// <value>The message text of the KryptonMessageBox.</value>
        [DefaultValue(""), Description("Sets the message text of the KryptonMessageBox.")]
        public string Text { get => _text; set => _text = value; }

        /// <summary>Gets or sets the help file path for the KryptonMessageBox help navigator.</summary>
        /// <value>The help file path for the KryptonMessageBox help navigator.</value>
        [DefaultValue(""), Description("Sets the help file path for the KryptonMessageBox help navigator.")]
        public string HelpFilePath { get => _helpFilePath; set => _helpFilePath = value; }

        /// <summary>Gets or sets the message box buttons.</summary>
        /// <value>The message box buttons.</value>
        [DefaultValue(typeof(MessageBoxButtons), "MessageBoxButtons.OK"), Description("Defines the KryptonMessageBox buttons.")]
        public MessageBoxButtons MessageBoxButtons { get => _buttons; set => _buttons = value; }

        /// <summary>Gets or sets the message box default button.</summary>
        /// <value>The message box default button.</value>
        [DefaultValue(typeof(MessageBoxDefaultButton), "MessageBoxDefaultButton.Button1"), Description("Defines the KryptonMessageBox default button.")]
        public MessageBoxDefaultButton MessageBoxDefaultButton { get => _defaultButton; set => _defaultButton = value; }

        /// <summary>Gets or sets the message box icon.</summary>
        /// <value>The message box icon.</value>
        [DefaultValue(typeof(KryptonMessageBoxIcon), "KryptonMessageBoxIcon.NONE"), Description("Defines the KryptonMessageBox icon.")]
        public KryptonMessageBoxIcon MessageBoxIcon { get => _icon; set => _icon = value; }

        /// <summary>Gets or sets the message box options.</summary>
        /// <value>The message box options.</value>
        [DefaultValue(typeof(MessageBoxOptions), "MessageBoxOptions.DefaultDesktopOnly"), Description("Defines the KryptonMessageBox options.")]
        public MessageBoxOptions MessageBoxOptions { get => _options; set => _options = value; }

        /// <summary>Gets or sets the help navigator.</summary>
        /// <value>The help navigator.</value>
        [DefaultValue(typeof(HelpNavigator), "HelpNavigator.Index"), Description("Defines the KryptonMessageBox help navigator type.")]
        public HelpNavigator HelpNavigator { get => _navigator; set => _navigator = value; }

        /// <summary>Gets or sets the parameters of the numeric ID of the Help topic to display when the user clicks the Help button.</summary>
        /// <value>The parameters.</value>
        [DefaultValue(null), Description("Define the numeric ID of the Help topic to display when the user clicks the Help button.")]
        public object Parameters { get => _param; set => _param = value; }

        /// <summary>Gets or sets the blur radius of the parent window (if enabled).</summary>
        /// <value>The blur radius.</value>
        [DefaultValue(0), Description("Defines the blur radius of the parent window (if enabled).")]
        public int BlurRadius { get => _blurRadius; set => _blurRadius = value; }

        /// <summary>Gets or sets the corner radius of the KryptonMessageBox.</summary>
        /// <value>The corner radius.</value>
        [DefaultValue(GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE), Description("Defines the corner radius of the KryptonMessageBox.")]
        public float CornerRadius { get => _cornerRadius; set => _cornerRadius = value; }

        /// <summary>Gets or sets the owner.</summary>
        /// <value>The owner.</value>
        [DefaultValue(null), Description("The owner of the KryptonMessageBox.")]
        public IWin32Window Owner { get => _owner; set => _owner = value; }

        /// <summary>Gets or sets the parent window.</summary>
        /// <value>The parent window.</value>
        [DefaultValue(null), Description("Defines the window to utilise the blurring effect on.")]
        public KryptonForm ParentWindow { get => _parentWindow; set => _parentWindow = value; }
        #endregion

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="KryptonMessageBoxManager" /> class.</summary>
        public KryptonMessageBoxManager()
        {
            _useBlur = false;

            _useControlCopy = false;

            _text = "";

            _caption = "";

            _helpFilePath = "";

            _buttons = MessageBoxButtons.OK;

            _defaultButton = MessageBoxDefaultButton.Button1;

            _icon = KryptonMessageBoxIcon.NONE;

            _options = MessageBoxOptions.DefaultDesktopOnly;

            _navigator = HelpNavigator.Index;

            _param = null;

            _blurRadius = 0;

            _cornerRadius = GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE;

            _owner = null;

            _parentWindow = null;
        }
        #endregion

        #region Methods
        /// <summary>Displays the krypton message box.</summary>
        /// <returns>The result of the users' choice.</returns>
        public DialogResult DisplayKryptonMessageBox()
        {
            DialogResult result = KryptonMessageBox.Show(_owner, _text, _caption, _buttons, _icon, _defaultButton, _options, _helpFilePath, _navigator, _param,
                                                         _useControlCopy, _cornerRadius, _useBlur, _blurRadius, _parentWindow);

            return result;
        }
        #endregion
    }
}