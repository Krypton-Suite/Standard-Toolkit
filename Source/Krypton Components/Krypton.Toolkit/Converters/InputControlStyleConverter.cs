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
    /// <summary>
    /// Custom type converter so that InputControl values appear as neat text at design time.
    /// </summary>
    internal class InputControlStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the InputControlStyleConverter class.
        /// </summary>
        public InputControlStyleConverter()
            : base(typeof(InputControlStyle))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new(InputControlStyle.Standalone, "Standalone"),
            new(InputControlStyle.Ribbon,     "Ribbon"),
            new(InputControlStyle.Custom1,    "Custom1"),
            new(InputControlStyle.Custom2,    "Custom2"),
            new(InputControlStyle.Custom3,    "Custom3")
        };

        #endregion
    }
}
