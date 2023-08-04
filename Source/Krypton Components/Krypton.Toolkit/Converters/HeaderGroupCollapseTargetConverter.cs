﻿#region BSD License
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
    /// Custom type converter so that HeaderGroupCollapseTarget values appear as neat text at design time.
    /// </summary>
    internal class HeaderGroupCollapsedTargetConverter : StringLookupConverter<HeaderGroupCollapsedTarget>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<HeaderGroupCollapsedTarget, string> _pairs = new Dictionary<HeaderGroupCollapsedTarget, string>
        {
            { HeaderGroupCollapsedTarget.CollapsedToPrimary,
                KryptonLanguageManager.GroupCollapsedTargetStrings.CollapsedToPrimary},
            {HeaderGroupCollapsedTarget.CollapsedToSecondary,
                KryptonLanguageManager.GroupCollapsedTargetStrings.CollapsedToSecondary},
            {HeaderGroupCollapsedTarget.CollapsedToBoth,
                KryptonLanguageManager.GroupCollapsedTargetStrings.CollapsedToBoth}
        };

        #endregion

        #region Protected

        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<HeaderGroupCollapsedTarget /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
