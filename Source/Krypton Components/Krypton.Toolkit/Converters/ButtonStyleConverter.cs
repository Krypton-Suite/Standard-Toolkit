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
            new(ButtonStyle.ButtonSpec, nameof(ButtonSpec)),
            new(ButtonStyle.BreadCrumb, "BreadCrumb"),
            new(ButtonStyle.CalendarDay, "Calendar Day"),
            new(ButtonStyle.Cluster, "Cluster"),
            new(ButtonStyle.Gallery, "Gallery"),
            new(ButtonStyle.NavigatorStack, "Navigator Stack"),
            new(ButtonStyle.NavigatorOverflow, "Navigator Overflow"),
            new(ButtonStyle.NavigatorMini, "Navigator Mini"),
            new(ButtonStyle.InputControl, "Input Control"),
            new(ButtonStyle.ListItem, "List Item"),
            new(ButtonStyle.Form, nameof(Form)),
            new(ButtonStyle.FormClose, "Form Close"),
            new(ButtonStyle.Command, "Command"),
            new(ButtonStyle.Custom1, "Custom1"),
            new(ButtonStyle.Custom2, "Custom2"),
            new(ButtonStyle.Custom3, "Custom3")
        };*/

        #endregion

        private readonly Pair[] _pairs =
        {
            new(ButtonStyle.Standalone, KryptonLanguageManager.SpecStyleStrings.Standalone),
            new(ButtonStyle.Alternate, KryptonLanguageManager.SpecStyleStrings.Alternate),
            new(ButtonStyle.LowProfile, KryptonLanguageManager.SpecStyleStrings.LowProfile),
            new(ButtonStyle.ButtonSpec, KryptonLanguageManager.SpecStyleStrings.ButtonSpecName),
            new(ButtonStyle.BreadCrumb, KryptonLanguageManager.SpecStyleStrings.BreadCrumb),
            new(ButtonStyle.CalendarDay, KryptonLanguageManager.SpecStyleStrings.CalendarDay),
            new(ButtonStyle.Cluster, KryptonLanguageManager.SpecStyleStrings.Cluster),
            new(ButtonStyle.Gallery, KryptonLanguageManager.SpecStyleStrings.Gallery),
            new(ButtonStyle.NavigatorStack, KryptonLanguageManager.SpecStyleStrings.NavigatorStack),
            new(ButtonStyle.NavigatorOverflow, KryptonLanguageManager.SpecStyleStrings.NavigatorOverflow),
            new(ButtonStyle.NavigatorMini, KryptonLanguageManager.SpecStyleStrings.NavigatorMini),
            new(ButtonStyle.InputControl, KryptonLanguageManager.SpecStyleStrings.InputControl),
            new(ButtonStyle.ListItem, KryptonLanguageManager.SpecStyleStrings.ListItem),
            new(ButtonStyle.Form, KryptonLanguageManager.SpecStyleStrings.FormName),
            new(ButtonStyle.FormClose, KryptonLanguageManager.SpecStyleStrings.FormClose),
            new(ButtonStyle.Command, KryptonLanguageManager.SpecStyleStrings.Command),
            new(ButtonStyle.Custom1, KryptonLanguageManager.SpecStyleStrings.CustomOne),
            new(ButtonStyle.Custom2, KryptonLanguageManager.SpecStyleStrings.CustomTwo),
            new(ButtonStyle.Custom3, KryptonLanguageManager.SpecStyleStrings.CustomThree)
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
