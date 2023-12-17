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

        #endregion

        #region Implementation

        public void Reset()
        {

        }

        #endregion
    }
}