#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using Krypton.Toolkit;

namespace Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that ButtonDisplayLogic values appear as neat text at design time.
    /// </summary>
    public class ButtonDisplayLogicConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonDisplayLogicConverter clas.
        /// </summary>
        public ButtonDisplayLogicConverter()
            : base(typeof(ButtonDisplayLogic))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(ButtonDisplayLogic.None,                  "None"),
            new Pair(ButtonDisplayLogic.Context,               "Context"),
            new Pair(ButtonDisplayLogic.NextPrevious,          "Next/Previous"),
            new Pair(ButtonDisplayLogic.ContextNextPrevious,   "Context & Next/Previous") };

        #endregion
    }
}
