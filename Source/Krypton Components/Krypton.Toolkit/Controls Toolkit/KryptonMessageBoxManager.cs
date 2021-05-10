#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Sunday 9th May, 2021 @ 14:00 GMT
 *
 */
#endregion

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    [ToolboxBitmap(typeof(KryptonMessageBox)), Description("Displays a new KryptonMessageBox, configurable through the designer.")]
    public class KryptonMessageBoxManager : Component
    {
        #region Variables
        private bool _useBlur, _showControlCopy;

        private string _caption, _text, _helpFilePath;

        private MessageBoxButtons _buttons;

        private MessageBoxDefaultButton _defaultButton;

        private MessageBoxIcon _icon;

        private MessageBoxOptions _options;

        private HelpNavigator _navigator;

        private object _param;

        private int _blurRadius, _cornerRadius;

        private IWin32Window _owner;

        private KryptonForm _parentWindow;
        #endregion

        #region Properties
        [DefaultValue(false), Description("Use the blur feature on the parent form when the KryptonMessageBox is shown.")]
        public bool UseBlur { get => _useBlur; set => _useBlur = value; }

        [DefaultValue(false), Description("Use the 'control + copy' KryptonMessageBox feature.")]
        public bool ShowControlCopy { get => _showControlCopy; set => _showControlCopy = value; }

        [DefaultValue(""), Description("Sets the caption of the KryptonMessageBox.")]
        public string Caption { get => _caption; set => _caption = value; }

        [DefaultValue(""), Description("Sets the message text of the KryptonMessageBox.")]
        public string Text { get => _text; set => _text = value; }

        [DefaultValue(""), Description("Sets the help file path for the KryptonMessageBox help navigator.")]
        public string HelpFilePath { get => _helpFilePath; set => _helpFilePath = value; }

        [DefaultValue(typeof(MessageBoxButtons), "MessageBoxButtons.OK"), Description("Defines the KryptonMessageBox buttons.")]
        public MessageBoxButtons MessageBoxButtons { get => _buttons; set => _buttons = value; }

        [DefaultValue(typeof(MessageBoxDefaultButton), "MessageBoxDefaultButton.Button1"), Description("Defines the KryptonMessageBox default button.")]
        public MessageBoxDefaultButton MessageBoxDefaultButton { get => _defaultButton; set => _defaultButton = value; }

        [DefaultValue(typeof(MessageBoxIcon), "MessageBoxIcon.None"), Description("Defines the KryptonMessageBox icon.")]
        public MessageBoxIcon MessageBoxIcon { get => _icon; set => _icon = value; }

        [DefaultValue(typeof(MessageBoxOptions), "MessageBoxOptions.DefaultDesktopOnly"), Description("Defines the KryptonMessageBox options.")]
        public MessageBoxOptions MessageBoxOptions { get => _options; set => _options = value; }

        public HelpNavigator HelpNavigator { get => _navigator; set => _navigator = value; }

        public object Parameters { get => _param; set => _param = value; }

        public int BlurRadius { get => _blurRadius; set => _blurRadius = value; }

        public int CornerRadius { get => _cornerRadius; set => _cornerRadius = value; }

        public IWin32Window Owner { get => _owner; set => _owner = value; }

        public KryptonForm ParentWindow { get => _parentWindow; set => _parentWindow = value; }
        #endregion

        #region Constructor
        public KryptonMessageBoxManager()
        {
            _useBlur = false;

            _showControlCopy = false;

            _text = "";

            _caption = "";

            _helpFilePath = "";

            _buttons = MessageBoxButtons.OK;

            _defaultButton = MessageBoxDefaultButton.Button1;

            _icon = MessageBoxIcon.None;

            _options = MessageBoxOptions.DefaultDesktopOnly;

            _navigator = HelpNavigator.Index;

            _param = null;

            _blurRadius = 0;

            _cornerRadius = -1;

            _owner = null;

            _parentWindow = null;
        }
        #endregion

        #region Methods
        public DialogResult DisplayKryptonMessageBox()
        {
            DialogResult result = KryptonMessageBox.Show(_owner, _text, _caption, _buttons, _icon, _defaultButton, _options, _helpFilePath, _navigator, _param,
                                                         _showControlCopy, _cornerRadius, _useBlur, _blurRadius, _parentWindow);

            return result;
        }
        #endregion
    }
}