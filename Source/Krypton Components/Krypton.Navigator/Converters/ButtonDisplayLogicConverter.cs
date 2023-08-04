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

namespace Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that ButtonDisplayLogic values appear as neat text at design time.
    /// </summary>
    public class ButtonDisplayLogicConverter : StringLookupConverter<ButtonDisplayLogic>
    {
        private static readonly IReadOnlyDictionary<ButtonDisplayLogic, string> _pairs = new Dictionary<ButtonDisplayLogic, string>
            {
                {ButtonDisplayLogic.None, "None"},
                {ButtonDisplayLogic.Context, "Context"},
                {ButtonDisplayLogic.NextPrevious, "Next/Previous"},
                {ButtonDisplayLogic.ContextNextPrevious, "Context & Next/Previous"}
            };

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<ButtonDisplayLogic /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
