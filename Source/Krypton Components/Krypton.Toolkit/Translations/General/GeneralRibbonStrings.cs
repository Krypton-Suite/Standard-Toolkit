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
    public class GeneralRibbonStrings : GlobalId
    {
        #region Static Values

        private const string DEFAULT_APPLICATION_BUTTON_TEXT = @"File";

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="GeneralRibbonStrings" /> class.</summary>
        public GeneralRibbonStrings()
        {
            Reset();
        }

        #endregion

        #region IsDefault

        [Browsable(false)] 
        public bool IsDefault => false;

        #endregion

        #region Public



        #endregion

        #region Implementation

        public void Reset()
        {

        }

        #endregion
    }
}