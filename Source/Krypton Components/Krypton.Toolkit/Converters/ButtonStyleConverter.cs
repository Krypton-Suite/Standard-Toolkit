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
    /// Custom type converter so that ButtonStyle values appear as neat text at design time.
    /// </summary>
    internal class ButtonStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #region Old

        /*private readonly Pair[] _pairs =
        {
            new(ButtonStyle.Standalone, "Standalone"),
            new(ButtonStyle.Alternate, "Alternate"),
            new(ButtonStyle.LowProfile, "Low Profile"),
        new (ButtonStyle.ButtonSpec, nameof(ButtonSpec)),
            new (ButtonStyle.BreadCrumb, "BreadCrumb"),
            new (ButtonStyle.CalendarDay, "Calendar Day"),
            new (ButtonStyle.Cluster, "Cluster"),
            new (ButtonStyle.Gallery, "Gallery"),
            new (ButtonStyle.NavigatorStack, "Navigator Stack"),
            new (ButtonStyle.NavigatorOverflow, "Navigator Overflow"),
            new (ButtonStyle.NavigatorMini, "Navigator Mini"),
            new (ButtonStyle.InputControl, "Input Control"),
            new (ButtonStyle.ListItem, "List Item"),
            new (ButtonStyle.Form, nameof(Form)),
            new (ButtonStyle.FormClose, "Form Close"),
            new (ButtonStyle.Command, "Command"),
            new (ButtonStyle.Custom1, "Custom1"),
            new (ButtonStyle.Custom2, "Custom2"),
            new (ButtonStyle.Custom3, "Custom3")
        };*/

        #endregion

        [Localizable(true)]
        private readonly Pair[] _pairs =
        {
            new(ButtonStyle.Standalone, KryptonLanguageManager.ButtonStyles.Standalone),
            new(ButtonStyle.Alternate, KryptonLanguageManager.ButtonStyles.Alternate),
            new(ButtonStyle.LowProfile, KryptonLanguageManager.ButtonStyles.LowProfile),
            new(ButtonStyle.ButtonSpec, KryptonLanguageManager.ButtonStyles.ButtonSpec),
            new(ButtonStyle.BreadCrumb, KryptonLanguageManager.ButtonStyles.BreadCrumb),
            new(ButtonStyle.CalendarDay, KryptonLanguageManager.ButtonStyles.CalendarDay),
            new(ButtonStyle.Cluster, KryptonLanguageManager.ButtonStyles.Cluster),
            new(ButtonStyle.Gallery, KryptonLanguageManager.ButtonStyles.Gallery),
            new(ButtonStyle.NavigatorStack, KryptonLanguageManager.ButtonStyles.NavigatorStack),
            new(ButtonStyle.NavigatorOverflow, KryptonLanguageManager.ButtonStyles.NavigatorOverflow),
            new(ButtonStyle.NavigatorMini, KryptonLanguageManager.ButtonStyles.NavigatorMini),
            new(ButtonStyle.InputControl, KryptonLanguageManager.ButtonStyles.InputControl),
            new(ButtonStyle.ListItem, KryptonLanguageManager.ButtonStyles.ListItem),
            new(ButtonStyle.Form, KryptonLanguageManager.ButtonStyles.Form),
            new(ButtonStyle.FormClose, KryptonLanguageManager.ButtonStyles.FormClose),
            new(ButtonStyle.Command, KryptonLanguageManager.ButtonStyles.Command),
            new(ButtonStyle.Custom1, KryptonLanguageManager.ButtonStyles.CustomOne),
            new(ButtonStyle.Custom2, KryptonLanguageManager.ButtonStyles.CustomTwo),
            new(ButtonStyle.Custom3, KryptonLanguageManager.ButtonStyles.CustomThree)
        };

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonStyleConverter class.
        /// </summary>
        public ButtonStyleConverter()
            : base(typeof(ButtonStyle))
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
