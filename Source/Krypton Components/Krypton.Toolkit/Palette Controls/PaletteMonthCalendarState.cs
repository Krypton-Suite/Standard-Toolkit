// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System.ComponentModel;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for month calendar appearance states.
    /// </summary>
    public class PaletteMonthCalendarState : Storage
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteMonthCalendarState class.
        /// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        public PaletteMonthCalendarState(PaletteMonthCalendarRedirect redirect)
            : this(redirect, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteMonthCalendarState class.
        /// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteMonthCalendarState(PaletteMonthCalendarRedirect redirect,
                                         NeedPaintHandler needPaint) 
        {
            Day = new PaletteTriple(redirect.Day, needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => Day.IsDefault;

        #endregion

        #region Day
        /// <summary>
        /// Gets access to the day appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining day appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple Day { get; }

        private bool ShouldSerializeContent()
        {
            return !Day.IsDefault;
        }
        #endregion
    }
}
