#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>Exposes the set of <see cref="KryptonToastNotificationIconConverter"/> strings used within Krypton and that are localizable.</summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ToastNotificationIconStrings : GlobalId
    {
        #region Identity

        /// <summary>Initializes a new instance of the <see cref="ToastNotificationIconStrings" /> class.</summary>
        public ToastNotificationIconStrings()
        {
            Reset();
        }

        #endregion

        #region Public

        [Browsable(false)] 
        public bool IsDefault => true;

        public string Application { get; set; }

        public string Asterisk { get; set; }

        public string Custom { get; set; }

        public string Error { get; set; }

        public string Exclamation { get; set; }

        public string Hand { get; set; }

        public string Information { get; set; }

        public string None { get; set; }

        public string Ok { get; set; }

        public string Question { get; set; }

        public string Shield { get; set; }

        public string Stop { get; set; }

        public string SystemApplication { get; set; }

        public string SystemAsterisk { get; set; }

        public string SystemError { get; set; }

        public string SystemExclamation { get; set; }

        public string SystemHand { get; set; }

        public string SystemInformation { get; set; }

        public string SystemQuestion { get; set; }

        public string SystemStop { get; set; }

        public string SystemWarning { get; set; }

        public string Warning { get; set; }

        public string WindowsLogo { get; set; }

        #endregion

        #region Implementation

        public void Reset()
        {

        }

        #endregion
    }
}