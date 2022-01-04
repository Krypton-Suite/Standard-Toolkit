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

namespace Krypton.Toolkit
{
    [ToolboxBitmap(typeof(KryptonInputBox), "ToolboxBitmaps.KryptonInputBox.bmp")]
    [DesignerCategory("code")]
    public class KryptonInputBoxManager : Component
    {
        #region Variables
        private bool _usePasswordOption;

        private Color _cueColour;

        private Font _cueTypeface;

        private string _caption, _cueText, _defaultResponse, _prompt;

        private IWin32Window _owner = null;
        #endregion

        #region Properties
        /// <summary>Gets or sets a value indicating whether [use password option].</summary>
        /// <value><c>true</c> if [use password option]; otherwise, <c>false</c>.</value>
        [DefaultValue(false), Description("Triggers the password feature of the response textbox.")]
        public bool UsePasswordOption { get => _usePasswordOption; set => _usePasswordOption = value; }

        /// <summary>Gets or sets the cue colour.</summary>
        /// <value>The cue colour.</value>
        [DefaultValue(typeof(Color), "Color.Gray"), Description("Modifies the cue text colour.")]
        public Color CueColour { get => _cueColour; set => _cueColour = value; }

        /// <summary>Gets or sets the cue typeface.</summary>
        /// <value>The cue typeface.</value>
        [DefaultValue(typeof(Font), "Segoe UI, 9pt"), Description("The cue text typeface.")]
        public Font CueTypeface { get => _cueTypeface; set => _cueTypeface = value; }

        /// <summary>Gets or sets the caption.</summary>
        /// <value>The caption.</value>
        [DefaultValue(""), Description("The krypton input box caption.")]
        public string Caption { get => _caption; set => _caption = value; }

        /// <summary>Gets or sets the cue text.</summary>
        /// <value>The cue text.</value>
        [DefaultValue(""), Description("The krypton input box cue text.")]
        public string CueText { get => _cueText; set => _cueText = value; }

        /// <summary>Gets or sets the default response.</summary>
        /// <value>The default response.</value>
        [DefaultValue(""), Description("The krypton input box default response.")]
        public string DefaultResponse { get => _defaultResponse; set => _defaultResponse = value; }

        /// <summary>Gets or sets the prompt.</summary>
        /// <value>The prompt.</value>
        [DefaultValue(""), Description("The krypton input box prompt text.")]
        public string Prompt { get => _prompt; set => _prompt = value; }
        #endregion

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="KryptonInputBoxManager" /> class.</summary>
        public KryptonInputBoxManager()
        {
            _usePasswordOption = false;

            _cueColour = Color.Gray;

            _cueTypeface = new Font("Segoe UI", 9f);

            _caption = string.Empty;

            _cueText = string.Empty;

            _defaultResponse = string.Empty;

            _prompt = string.Empty;
        }
        #endregion

        #region Setters and Getters
        /// <summary>Sets the Owner to the value of value.</summary>
        /// <param name="value">The desired value of Owner.</param>
        public void SetOwner(IWin32Window value) => _owner = value;

        /// <summary>Returns the value of the Owner.</summary>
        /// <returns>The value of the Owner.</returns>
        public IWin32Window GetOwner() => _owner;
        #endregion

        #region Methods
        /// <summary>Displays the krypton input box.</summary>
        public void DisplayKryptonInputBox()
        {
            if (GetOwner() != null)
            {
                KryptonInputBox.Show(_owner, _prompt, _caption, _defaultResponse, _cueText, _cueColour, _cueTypeface, _usePasswordOption);
            }
            else
            {
                KryptonInputBox.Show(_prompt, _caption, _defaultResponse, _cueText, _cueColour, _cueTypeface, _usePasswordOption);
            }
        }
        #endregion
    }
}