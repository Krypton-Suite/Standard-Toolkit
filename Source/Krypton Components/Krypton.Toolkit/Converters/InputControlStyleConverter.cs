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
    /// Custom type converter so that InputControl values appear as neat text at design time.
    /// </summary>
    internal class InputControlStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #region Old

        //private readonly Pair[] _pairs =
        //{
        //    new(InputControlStyle.Standalone, "Standalone"),
        //    new(InputControlStyle.Ribbon, "Ribbon"),
        //    new(InputControlStyle.Custom1, "Custom1"),
        //    new(InputControlStyle.Custom2, "Custom2"),
        //    new(InputControlStyle.Custom3, "Custom3"),
        //    new(InputControlStyle.PanelClient, "Panel Client"),
        //    new(InputControlStyle.PanelAlternate, "Panel Alternate"),
        //    // new(InputControlStyle.Disabled, "Disabled")
        //};

        #endregion

        [Localizable(true)]
        private readonly Pair[] _pairs =
        {
            new Pair(InputControlStyle.Standalone, KryptonLanguageManager.InputControlStyles.Standalone),
            new Pair(InputControlStyle.Ribbon, KryptonLanguageManager.InputControlStyles.Ribbon),
            new Pair(InputControlStyle.Custom1, KryptonLanguageManager.InputControlStyles.CustomOne),
            new Pair(InputControlStyle.Custom2, KryptonLanguageManager.InputControlStyles.CustomTwo),
            new Pair(InputControlStyle.Custom3, KryptonLanguageManager.InputControlStyles.CustomThree),
            new Pair(InputControlStyle.PanelClient, KryptonLanguageManager.InputControlStyles.PanelClient),
            new Pair(InputControlStyle.PanelAlternate, KryptonLanguageManager.InputControlStyles.PanelAlternate),
            // new(InputControlStyle.Disabled, "Disabled")
        };

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
        protected override Pair[] Pairs => _pairs;

        #endregion
    }
}
