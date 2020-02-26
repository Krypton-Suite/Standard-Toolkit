// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.500.0.0  
// *****************************************************************************

using System.ComponentModel;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for month calendar appearance states.
    /// </summary>
    public class PaletteMonthCalendarDoubleState : PaletteDouble
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteMonthCalendarDoubleState class.
        /// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        public PaletteMonthCalendarDoubleState(PaletteMonthCalendarRedirect redirect)
            : this(redirect, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteMonthCalendarDoubleState class.
        /// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteMonthCalendarDoubleState(PaletteMonthCalendarRedirect redirect,
                                               NeedPaintHandler needPaint) 
            : base(redirect, needPaint)
        {
            Header = new PaletteTriple(redirect.Header, needPaint);
            Day = new PaletteTriple(redirect.Day, needPaint);
            DayOfWeek = new PaletteTriple(redirect.DayOfWeek, needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (base.IsDefault && 
                                           Header.IsDefault &&
                                           Day.IsDefault &&
                                           DayOfWeek.IsDefault);

        #endregion

        #region Header
        /// <summary>
        /// Gets access to the month/year header appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining month/year header appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple Header { get; }

        private bool ShouldSerializeHeader()
        {
            return !Header.IsDefault;
        }
        #endregion

        #region Day
        /// <summary>
        /// Gets access to the day appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining day appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple Day { get; }

        private bool ShouldSerializeDay()
        {
            return !Day.IsDefault;
        }
        #endregion

        #region DayOfWeek
        /// <summary>
        /// Gets access to the day of week appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining day of week appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple DayOfWeek { get; }

        private bool ShouldSerializeDayOfWeek()
        {
            return !DayOfWeek.IsDefault;
        }
        #endregion
    }
}
