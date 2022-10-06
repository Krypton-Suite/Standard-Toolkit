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
    internal class KryptonMonthCalendarActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonMonthCalendar _monthCalendar;
        private readonly IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonMonthCalendarActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonMonthCalendarActionList(KryptonMonthCalendarDesigner owner)
            : base(owner.Component)
        {
            // Remember the bread crumb control instance
            _monthCalendar = owner.Component as KryptonMonthCalendar;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public
        /// <summary>Gets or sets the Krypton Context Menu.</summary>
        /// <value>The Krypton Context Menu.</value>
        public KryptonContextMenu KryptonContextMenu
        {
            get => _monthCalendar.KryptonContextMenu;

            set
            {
                if (_monthCalendar.KryptonContextMenu != value)
                {
                    _service.OnComponentChanged(_monthCalendar, null, _monthCalendar.KryptonContextMenu, value);

                    _monthCalendar.KryptonContextMenu = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _monthCalendar.PaletteMode;

            set
            {
                if (_monthCalendar.PaletteMode != value)
                {
                    _service.OnComponentChanged(_monthCalendar, null, _monthCalendar.PaletteMode, value);
                    _monthCalendar.PaletteMode = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the maximum selection count.
        /// </summary>
        public int MaxSelectionCount
        {
            get => _monthCalendar.MaxSelectionCount;

            set
            {
                if (_monthCalendar.MaxSelectionCount != value)
                {
                    _service.OnComponentChanged(_monthCalendar, null, _monthCalendar.MaxSelectionCount, value);
                    _monthCalendar.MaxSelectionCount = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the today button.
        /// </summary>
        public bool ShowToday
        {
            get => _monthCalendar.ShowToday;

            set
            {
                if (_monthCalendar.ShowToday != value)
                {
                    _service.OnComponentChanged(_monthCalendar, null, _monthCalendar.ShowToday, value);
                    _monthCalendar.ShowToday = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the today entry circled.
        /// </summary>
        public bool ShowTodayCircle
        {
            get => _monthCalendar.ShowTodayCircle;

            set
            {
                if (_monthCalendar.ShowTodayCircle != value)
                {
                    _service.OnComponentChanged(_monthCalendar, null, _monthCalendar.ShowTodayCircle, value);
                    _monthCalendar.ShowTodayCircle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the display of week numbers.
        /// </summary>
        public bool ShowWeekNumbers
        {
            get => _monthCalendar.ShowWeekNumbers;

            set
            {
                if (_monthCalendar.ShowWeekNumbers != value)
                {
                    _service.OnComponentChanged(_monthCalendar, null, _monthCalendar.ShowWeekNumbers, value);
                    _monthCalendar.ShowWeekNumbers = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font DayStateCommonShortTextFont
        {
            get => _monthCalendar.StateCommon.Day.Content.ShortText.Font;

            set
            {
                if (_monthCalendar.StateCommon.Day.Content.ShortText.Font != value)
                {
                    _service.OnComponentChanged(_monthCalendar, null, _monthCalendar.StateCommon.Day.Content.ShortText.Font, value);

                    _monthCalendar.StateCommon.Day.Content.ShortText.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font DayStateCommonLongTextFont
        {
            get => _monthCalendar.StateCommon.Day.Content.LongText.Font;

            set
            {
                if (_monthCalendar.StateCommon.Day.Content.LongText.Font != value)
                {
                    _service.OnComponentChanged(_monthCalendar, null, _monthCalendar.StateCommon.Day.Content.LongText.Font, value);

                    _monthCalendar.StateCommon.Day.Content.LongText.Font = value;
                }
            }
        }

        /// <summary>Gets or sets the corner radius.</summary>
        /// <value>The corner radius.</value>
        [DefaultValue(GlobalStaticValues.PRIMARY_CORNER_ROUNDING_VALUE)]
        public float StateCommonCornerRoundingRadius
        {
            get => _monthCalendar.StateCommon.Border.Rounding;

            set
            {
                if (_monthCalendar.StateCommon.Border.Rounding != value)
                {
                    _service.OnComponentChanged(_monthCalendar, null, _monthCalendar.StateCommon.Border.Rounding, value);

                    _monthCalendar.StateCommon.Border.Rounding = value;
                }
            }
        }
        #endregion

        #region Public Override
        /// <summary>
        /// Returns the collection of DesignerActionItem objects contained in the list.
        /// </summary>
        /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            // Create a new collection for holding the single item we want to create
            DesignerActionItemCollection actions = new();

            // This can be null when deleting a control instance at design time
            if (_monthCalendar != null)
            {
                // Add the list of bread crumb specific actions
                actions.Add(new DesignerActionHeaderItem(@"Appearance"));
                actions.Add(new DesignerActionPropertyItem(@"KryptonContextMenu", @"Krypton Context Menu", @"Appearance", @"The Krypton Context Menu for the control."));
                actions.Add(new DesignerActionPropertyItem(@"DayStateCommonShortTextFont", @"Day State Common Short Text Font", @"Appearance", @"The State Common Short Text Font."));
                actions.Add(new DesignerActionPropertyItem(@"DayStateCommonLongTextFont", @"Day State Common State Common Long Text Font", @"Appearance", @"The State Common State Common Long Text Font."));
                actions.Add(new DesignerActionPropertyItem(@"StateCommonCornerRoundingRadius", @"State Common Corner Rounding Radius", @"Appearance", @"The corner rounding radius of the control."));
                actions.Add(new DesignerActionHeaderItem(@"Behavior"));
                actions.Add(new DesignerActionPropertyItem(@"MaxSelectionCount", @"MaxSelectionCount", @"Behavior", @"Maximum number of selected days"));
                actions.Add(new DesignerActionPropertyItem(@"ShowToday", @"ShowToday", @"Behavior", @"Show the today button"));
                actions.Add(new DesignerActionPropertyItem(@"ShowTodayCircle", @"ShowTodayCircle", @"Behavior", @"Show a circle around the today entry"));
                actions.Add(new DesignerActionPropertyItem(@"ShowWeekNumbers", @"ShowWeekNumbers", @"Behavior", @"Show the week numbers"));
                actions.Add(new DesignerActionHeaderItem(@"Visuals"));
                actions.Add(new DesignerActionPropertyItem(@"PaletteMode", @"Palette", @"Visuals", @"Palette applied to drawing"));
            }

            return actions;
        }
        #endregion
    }
}
