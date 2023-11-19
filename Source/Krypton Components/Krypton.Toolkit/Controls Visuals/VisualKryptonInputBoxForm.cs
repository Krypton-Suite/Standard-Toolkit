#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// 
    /// </summary>
    public partial class VisualKryptonInputBoxForm : KryptonForm
    {
        #region Instance Fields
        private bool _usePasswordOption;
        private Color _cueColour;
        private string _prompt;
        private string _caption;
        private string _defaultResponse;
        private string _cueText;
        private Font? _cueTypeface;
        #endregion

        #region Identity

        /// <summary>
        /// 
        /// </summary>
        public VisualKryptonInputBoxForm()
        {
            InitializeComponent();
        }

        /// <summary>Initializes a new instance of the <see cref="VisualKryptonInputBoxForm" /> class.</summary>
        /// <param name="prompt">The prompt.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="defaultResponse">The default response.</param>
        /// <param name="cueText">The cue text.</param>
        /// <param name="cueColour">The cue colour.</param>
        /// <param name="cueTypeface">The cue typeface.</param>
        /// <param name="usePasswordOption">if set to <c>true</c> [use password option].</param>
        public VisualKryptonInputBoxForm(string prompt,
                                   string caption,
                                   string defaultResponse,
                                   string cueText,
                                   Color cueColour,
                                   Font? cueTypeface,
                                   bool usePasswordOption)
        {
            InitializeComponent();

            StoreValues(prompt, caption, defaultResponse, cueText, cueColour, cueTypeface, usePasswordOption);

            // Update contents to match requirements
            UpdateText();

            UpdateCue();

            UpdateButtons();
        }

        #endregion

        #region Implementation

        private void StoreValues(string prompt, string caption, string defaultResponse, string cueText, Color cueColour,
                                 Font? cueTypeface, bool usePasswordOption)
        {
            _prompt = prompt;

            _caption = caption;

            _defaultResponse = defaultResponse;

            _cueText = cueText;

            _cueColour = cueColour;

            _cueTypeface = cueTypeface;

            _usePasswordOption = usePasswordOption;
        }

        internal static string InternalShow(IWin32Window? owner,
            string prompt,
            string caption,
            string defaultResponse,
            string cueText,
            Color cueColour,
            Font? cueTypeface,
            bool usePasswordOption)
        {
            // If do not have an owner passed in then get the active window and use that instead
            IWin32Window? showOwner = owner ?? FromHandle(PI.GetActiveWindow());

            // Show input box window as a modal dialog and then dispose of it afterwards
            using var ib = new VisualKryptonInputBoxForm(prompt, caption, defaultResponse, cueText, cueColour,
                cueTypeface, usePasswordOption);
            ib.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

            return ib.ShowDialog(showOwner) == DialogResult.OK
                ? ib.InputResponse
                : string.Empty;
        }

        internal string InputResponse => _textBoxResponse.Text;

        private void UpdateText()
        {
            Text = _caption;
            _labelPrompt.Text = _prompt;
            _textBoxResponse.Text = _defaultResponse;
            _textBoxResponse.UseSystemPasswordChar = _usePasswordOption;
        }

        private void UpdateCue()
        {
            _textBoxResponse.CueHint.CueHintText = _cueText;

            if (!_cueColour.IsEmpty)
            {
                _textBoxResponse.CueHint.Color1 = _cueColour;
            }

            if (_cueTypeface != null)
            {
                _textBoxResponse.CueHint.Font = _cueTypeface;
            }
        }

        private void UpdateButtons()
        {
            _buttonOk.Text = KryptonManager.Strings.GeneralStrings.OK;
            _buttonCancel.Text = KryptonManager.Strings.GeneralStrings.Cancel;
        }

        private void Response_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    _buttonOk.PerformClick();
                    break;
                case Keys.Escape:
                    _buttonCancel.PerformClick();
                    break;
            }
        }

        #endregion
    }
}
