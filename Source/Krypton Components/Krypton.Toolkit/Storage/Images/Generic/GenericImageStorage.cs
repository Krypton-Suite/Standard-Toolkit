#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GenericImageStorage : Storage
    {
        #region Static Fields

        // ToDo: Use the standard WinForms app icon
        private readonly Image _defaultApplicationImage = GenericImageResources.AppButtonDefault;

        #endregion

        #region Identity

        public GenericImageStorage()
        {
            Reset();
        }

        #endregion

        #region IsDefault

        /// <summary>
        /// Gets a value indicating if all the strings are default values.
        /// </summary>
        /// <returns>True if all values are defaulted; otherwise false.</returns>
        [Browsable(false)]
        public override bool IsDefault => ApplicationIcon.Equals(_defaultApplicationImage);

        #endregion

        #region Public

        /// <summary>
        /// Gets and sets the application button image.
        /// </summary>
        [Localizable(true)]
        [Category(@"Values")]
        [Description(@"Application button image.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(typeof(Image), "Resources.AppButtonDefault.png")]
        public Image ApplicationIcon { get; set; }

        #endregion

        #region Implementation

        public void Reset()
        {
            ApplicationIcon = _defaultApplicationImage;
        }

        #endregion
    }
}
