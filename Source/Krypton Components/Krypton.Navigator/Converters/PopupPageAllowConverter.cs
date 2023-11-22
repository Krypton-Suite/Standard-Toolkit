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

namespace Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that PopupPageAllow values appear as neat text at design time.
    /// </summary>
    public class PopupPageAllowConverter : StringLookupConverter<PopupPageAllow>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<PopupPageAllow, string> _pairs = new Dictionary<PopupPageAllow, string>
        {
            {PopupPageAllow.Never, @"Never"},
            {PopupPageAllow.OnlyCompatibleModes, @"Only Compatible Modes"},
            {PopupPageAllow.OnlyOutlookMiniMode, @"Only Outlook Mini Mode"}
        };

        #endregion
        protected override IReadOnlyDictionary<PopupPageAllow /*Enum*/, string /*Display*/> Pairs => _pairs;


    }
}
