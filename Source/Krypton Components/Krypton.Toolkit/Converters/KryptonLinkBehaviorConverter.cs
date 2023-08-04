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
    /// Custom type converter so that KryptonLinkBehavior values appear as neat text at design time.
    /// </summary>
    internal class KryptonLinkBehaviorConverter : StringLookupConverter<KryptonLinkBehavior>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<KryptonLinkBehavior, string> _pairs = new Dictionary<KryptonLinkBehavior, string>
        {
            {KryptonLinkBehavior.AlwaysUnderline, KryptonLanguageManager.LinkBehaviorStrings.AlwaysUnderline},
            {KryptonLinkBehavior.HoverUnderline, KryptonLanguageManager.LinkBehaviorStrings.HoverUnderline},
            {KryptonLinkBehavior.NeverUnderline, KryptonLanguageManager.LinkBehaviorStrings.NeverUnderline }
        };

        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<KryptonLinkBehavior /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
