#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class KryptonOutlookGridStrings : GlobalId
    {
        #region Static Fields

        #region OutlookGrid Component

        private const string DEFAULT_AFTER_NEXT_MONTH = @"After next month";

        private const string DEFAULT_ALPHABETIC_GROUP_TEXT = @"Alphabetic";

        private const string DEFAULT_BAR = @"Data Bars";

        private const string DEFAULT_BEFORE_PREVIOUS_MONTH = @"Before previous month";

        private const string DEFAULT_BEST_FIT_ALL = @"Best fit (all columns)";

        private const string DEFAULT_BEST_FIT = @"Best fit";

        private const string DEFAULT_CANCEL = @"Cancel";

        private const string DEFAULT_CLEAR_GROUPING = @"Clear grouping";

        private const string DEFAULT_CLEAR_RULES = @"Clear rules...";

        private const string DEFAULT_CLEAR_SORTING = @"Clear sorting";

        private const string DEFAULT_COLLAPSE = @"Collapse";

        private const string DEFAULT_COLUMNS = @"Columns";

        private const string DEFAULT_CONDITIONAL_FORMATTING = @"Conditional formatting";

        private const string DEFAULT_CUSTOM_THREE_DOTS = @"Custom...";

        private const string DEFAULT_DATE_GROUP_TEXT = @"Date";

        private const string DEFAULT_DAY = @"Day";

        private const string DEFAULT_DRAG_COLUMN_TO_GROUP = @"Drag a column header here to group by that column";

        private const string DEFAULT_EARLIER_DURING_THIS_MONTH = @"Earlier during this month";

        private const string DEFAULT_EARLIER_THIS_YEAR = @"Earlier this year";

        private const string DEFAULT_EXPAND = @"Expand";

        private const string DEFAULT_FINISH = @"Finish";

        private const string DEFAULT_FULL_COLLAPSE = @"Full collapse";

        private const string DEFAULT_FULL_EXPAND = @"Full expand";

        private const string DEFAULT_GRADIENT_FILL = @"Gradient Fill";

        private const string DEFAULT_GROUP = @"Group by this column";

        private const string DEFAULT_GROUP_INTERVAL = @"Group interval";

        private const string DEFAULT_HIDE_GROUP_BOX = @"Hide GroupBox";

        private const string DEFAULT_IN_THREE_WEEKS = @"In three weeks";

        private const string DEFAULT_IN_TWO_WEEKS = @"In two weeks";

        private const string DEFAULT_LATER_DURING_THIS_MONTH = @"Later during this month";

        private const string DEFAULT_MONTH = @"Month";

        private const string DEFAULT_NEXT_MONTH = @"Next month";

        private const string DEFAULT_NEXT_WEEK = @"Next week";

        private const string DEFAULT_NO_DATE = @"No date";

        private const string DEFAULT_OLDER = @"Older";

        private const string DEFAULT_ONE_ITEM = @"1 item";

        private const string DEFAULT_OTHER = @"Other";

        private const string DEFAULT_PALETTE_CUSTOM = @"Custom...";

        private const string DEFAULT_PALETTE_CUSTOM_HEADING = @"Custom palettes";

        private const string DEFAULT_PREVIOUS_MONTH = @"Previous month";

        private const string DEFAULT_PREVIOUS_WEEK = @"Previous week";

        private const string DEFAULT_PREVIOUS_YEAR = @"Previous year";

        private const string DEFAULT_QUARTER_ONE = @"Q1";

        private const string DEFAULT_QUARTER_TWO = @"Q2";

        private const string DEFAULT_QUARTER_THREE = @"Q3";

        private const string DEFAULT_QUARTER_FOUR = @"Q4";

        private const string DEFAULT_QUARTER = @"Quarter";

        private const string DEFAULT_SHOW_GROUP_BOX = @"Show GroupBox";

        private const string DEFAULT_SMART = @"Smart";

        private const string DEFAULT_SOLID_FILL = @"Solid Fill";

        private const string DEFAULT_SORT_ASCENDING = @"Sort ascending";

        private const string DEFAULT_SORT_BY_SUMMARY_COUNT = @"Sort by summary count";

        private const string DEFAULT_SORT_DESCENDING = @"Sort descending";

        private const string DEFAULT_THREE_COLORS_RANGE = @"Three Color Scale";

        private const string DEFAULT_THREE_WEEKS_AGO = @"Three weeks ago";

        private const string DEFAULT_TODAY = @"Today";

        private const string DEFAULT_TOMORROW = @"Tomorrow";

        private const string DEFAULT_TWO_COLORS_RANGE = @"Two Color Scale";

        private const string DEFAULT_TWO_WEEKS_AGO = @"Two weeks ago";

        private const string DEFAULT_UNGROUP = @"Ungroup";

        private const string DEFAULT_UNKNOWN = @"Unknown";

        private const string DEFAULT_NUMBER_OF_ITEMS = @" items";

        private const string DEFAULT_YEAR = @"Year";

        private const string DEFAULT_YEAR_GROUP_TEXT = @"Year";

        private const string DEFAULT_YESTERDAY = @"Yesterday";

        private const string DEFAULT_MONDAY = @"Monday";

        private const string DEFAULT_TUESDAY = @"Tuesday";

        private const string DEFAULT_WEDNESDAY = @"Wednesday";

        private const string DEFAULT_THURSDAY = @"Thursday";

        private const string DEFAULT_FRIDAY = @"Friday";

        private const string DEFAULT_SATURDAY = @"Saturday";

        private const string DEFAULT_SUNDAY = @"Sunday";

        private const string DEFAULT_MINIMUM_COLOUR = @"Min Colour";

        private const string DEFAULT_MEDIUM_COLOUR = @"Medium Colour";

        private const string DEFAULT_MAXIMUM_COLOUR = @"Max Colour";

        #endregion

        #region Custom Format Window

        private const string DEFAULT_CUSTOM_FORMAT_WINDOW_TITLE = @"Custom Rule";

        private const string DEFAULT_CUSTOM_FORMAT_FORMAT = @"Format";

        private const string DEFAULT_CUSTOM_FORMAT_PREVIEW = @"Preview";

        private const string DEFAULT_CUSTOM_FORMAT_MINIMUM_COLOR = @"Minimum Color";

        private const string DEFAULT_CUSTOM_FORMAT_INTERMEDIATE_COLOR = @"Intermediate Color";

        private const string DEFAULT_CUSTOM_FORMAT_MAXIMUM_COLOR = @"Maximum Color";

        private const string DEFAULT_CUSTOM_FORMAT_FILL = @"Fill";

        #endregion

        #endregion

        #region Identity

        public KryptonOutlookGridStrings()
        {
            Reset();
        }

        #endregion

        #region IsDefault

        [Browsable(false)]
        public bool IsDefault => // OutlookGrid Component
                                 AfterNextMonth.Equals(DEFAULT_AFTER_NEXT_MONTH) &&
                                 AlphabeticGroupText.Equals(DEFAULT_ALPHABETIC_GROUP_TEXT) &&
                                 Bar.Equals(DEFAULT_BAR) &&
                                 BeforePreviousMonth.Equals(DEFAULT_BEFORE_PREVIOUS_MONTH) &&
                                 BestFitAll.Equals(DEFAULT_BEST_FIT_ALL) &&
                                 BestFit.Equals(DEFAULT_BEST_FIT) &&
                                 Cancel.Equals(DEFAULT_CANCEL) &&
                                 ClearGrouping.Equals(DEFAULT_CLEAR_GROUPING) &&
                                 ClearRules.Equals(DEFAULT_CLEAR_RULES) &&
                                 ClearSorting.Equals(DEFAULT_CLEAR_SORTING) &&
                                 Collapse.Equals(DEFAULT_COLLAPSE) &&
                                 Columns.Equals(DEFAULT_COLUMNS) &&
                                 ConditionalFormatting.Equals(DEFAULT_CONDITIONAL_FORMATTING) &&
                                 CustomThreeDots.Equals(DEFAULT_CUSTOM_THREE_DOTS) &&
                                 DateGroupText.Equals(DEFAULT_DATE_GROUP_TEXT) &&
                                 Day.Equals(DEFAULT_DAY) &&
                                 DragColumnToGroup.Equals(DEFAULT_DRAG_COLUMN_TO_GROUP) &&
                                 EarlierDuringThisMonth.Equals(DEFAULT_EARLIER_DURING_THIS_MONTH) &&
                                 EarlierDuringThisYear.Equals(DEFAULT_EARLIER_THIS_YEAR) &&
                                 Expand.Equals(DEFAULT_EXPAND) &&
                                 Finish.Equals(DEFAULT_FINISH) &&
                                 FullCollapse.Equals(DEFAULT_FULL_COLLAPSE) &&
                                 FullExpand.Equals(DEFAULT_FULL_EXPAND) &&
                                 GradientFill.Equals(DEFAULT_GRADIENT_FILL) &&
                                 Group.Equals(DEFAULT_GROUP) &&
                                 GroupInterval.Equals(DEFAULT_GROUP_INTERVAL) &&
                                 HideGroupBox.Equals(DEFAULT_HIDE_GROUP_BOX) &&
                                 InThreeWeeks.Equals(DEFAULT_IN_THREE_WEEKS) &&
                                 InTwoWeeks.Equals(DEFAULT_IN_TWO_WEEKS) &&
                                 LaterDuringThisMonth.Equals(DEFAULT_LATER_DURING_THIS_MONTH) &&
                                 Month.Equals(DEFAULT_MONTH) &&
                                 NextMonth.Equals(DEFAULT_NEXT_MONTH) &&
                                 NextWeek.Equals(DEFAULT_NEXT_WEEK) &&
                                 NoDate.Equals(DEFAULT_NO_DATE) &&
                                 Older.Equals(DEFAULT_OLDER) &&
                                 OneItem.Equals(DEFAULT_ONE_ITEM) &&
                                 Other.Equals(DEFAULT_OTHER) &&
                                 PaletteCustom.Equals(DEFAULT_PALETTE_CUSTOM) &&
                                 PaletteCustomHeading.Equals(DEFAULT_PALETTE_CUSTOM_HEADING) &&
                                 PreviousMonth.Equals(DEFAULT_PREVIOUS_MONTH) &&
                                 PreviousWeek.Equals(DEFAULT_PREVIOUS_WEEK) &&
                                 PreviousYear.Equals(DEFAULT_PREVIOUS_YEAR) &&
                                 QuarterOne.Equals(DEFAULT_QUARTER_ONE) &&
                                 QuarterTwo.Equals(DEFAULT_QUARTER_TWO) &&
                                 QuarterThree.Equals(DEFAULT_QUARTER_THREE) &&
                                 QuarterFour.Equals(DEFAULT_QUARTER_FOUR) &&
                                 Quarter.Equals(DEFAULT_QUARTER) &&
                                 ShowGroupBox.Equals(DEFAULT_SHOW_GROUP_BOX) &&
                                 Smart.Equals(DEFAULT_SMART) &&
                                 SolidFill.Equals(DEFAULT_SOLID_FILL) &&
                                 SortAscending.Equals(DEFAULT_SORT_ASCENDING) &&
                                 SortBySummaryCount.Equals(DEFAULT_SORT_BY_SUMMARY_COUNT) &&
                                 SortDescending.Equals(DEFAULT_SORT_DESCENDING) &&
                                 ThreeColorsRange.Equals(DEFAULT_THREE_COLORS_RANGE) &&
                                 ThreeWeeksAgo.Equals(DEFAULT_THREE_WEEKS_AGO) &&
                                 Today.Equals(DEFAULT_TODAY) &&
                                 Tomorrow.Equals(DEFAULT_TOMORROW) &&
                                 TwoColorsRange.Equals(DEFAULT_TWO_COLORS_RANGE) &&
                                 TwoWeeksAgo.Equals(DEFAULT_TWO_WEEKS_AGO) &&
                                 UnGroup.Equals(DEFAULT_UNGROUP) &&
                                 Unknown.Equals(DEFAULT_UNKNOWN) &&
                                 NumberOfItems.Equals(DEFAULT_NUMBER_OF_ITEMS) &&
                                 Year.Equals(DEFAULT_YEAR) &&
                                 YearGroupText.Equals(DEFAULT_YEAR_GROUP_TEXT) &&
                                 Yesterday.Equals(DEFAULT_YESTERDAY) &&
                                 Monday.Equals(DEFAULT_MONDAY) &&
                                 Tuesday.Equals(DEFAULT_TUESDAY) &&
                                 Wednesday.Equals(DEFAULT_WEDNESDAY) &&
                                 Thursday.Equals(DEFAULT_THURSDAY) &&
                                 Friday.Equals(DEFAULT_FRIDAY) &&
                                 Saturday.Equals(DEFAULT_SATURDAY) &&
                                 Sunday.Equals(DEFAULT_SUNDAY) &&
                                 MinimumColour.Equals(DEFAULT_MINIMUM_COLOUR) &&
                                 MediumColour.Equals(DEFAULT_MEDIUM_COLOUR) &&
                                 MaximumColour.Equals(DEFAULT_MAXIMUM_COLOUR);

        #endregion

        #region Public

        #region OutlookGrid Component

        /// <summary>Gets or sets the after next month string for the KryptonOutlookGrid.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"AfterNextMonth string used for Krypton Outlook Grid.")]
        [DefaultValue(DEFAULT_AFTER_NEXT_MONTH)]
        [RefreshProperties(RefreshProperties.All)]
        public string? AfterNextMonth { get; set; }

        /// <summary>Gets or sets the alphabetic group string for the KryptonOutlookGrid.</summary>
        public string AlphabeticGroupText { get; set; }

        /// <summary>Gets or sets the bar string for the KryptonOutlookGrid.</summary>
        public string Bar { get; set; }

        /// <summary>Gets or sets the before previous month string for the KryptonOutlookGrid.</summary>
        public string? BeforePreviousMonth { get; set; }

        /// <summary>Gets or sets the best fit all string for the KryptonOutlookGrid.</summary>
        public string BestFitAll { get; set; }

        /// <summary>Gets or sets the best fit string for the KryptonOutlookGrid.</summary>
        public string BestFit { get; set; }

        /// <summary>Gets or sets the cancel string for the KryptonOutlookGrid.</summary>
        public string Cancel { get; set; }

        /// <summary>Gets or sets the clear grouping string for the KryptonOutlookGrid.</summary>
        public string ClearGrouping { get; set; }

        /// <summary>Gets or sets the clear rules string for the KryptonOutlookGrid.</summary>
        public string ClearRules { get; set; }

        /// <summary>Gets or sets the clear sorting string for the KryptonOutlookGrid.</summary>
        public string ClearSorting { get; set; }

        /// <summary>Gets or sets the collapse string for the KryptonOutlookGrid.</summary>
        public string Collapse { get; set; }

        /// <summary>Gets or sets the columns string for the KryptonOutlookGrid.</summary>
        public string Columns { get; set; }

        /// <summary>Gets or sets the conditional formatting string for the KryptonOutlookGrid.</summary>
        public string ConditionalFormatting { get; set; }

        /// <summary>Gets or sets the custom three dots string for the KryptonOutlookGrid.</summary>
        public string CustomThreeDots { get; set; }

        /// <summary>Gets or sets the date group text string for the KryptonOutlookGrid.</summary>
        public string DateGroupText { get; set; }

        /// <summary>Gets or sets the day string for the KryptonOutlookGrid.</summary>
        public string Day { get; set; }

        /// <summary>Gets or sets the drag column to group string for the KryptonOutlookGrid.</summary>
        public string DragColumnToGroup { get; set; }

        /// <summary>Gets or sets the earlier during this month string for the KryptonOutlookGrid.</summary>
        public string? EarlierDuringThisMonth { get; set; }

        /// <summary>Gets or sets the earlier during this year string for the KryptonOutlookGrid.</summary>
        public string? EarlierDuringThisYear { get; set; }

        /// <summary>Gets or sets the expand string for the KryptonOutlookGrid.</summary>
        public string Expand { get; set; }

        /// <summary>Gets or sets the finish string for the KryptonOutlookGrid.</summary>
        public string Finish { get; set; }

        /// <summary>Gets or sets the full collapse string for the KryptonOutlookGrid.</summary>
        public string FullCollapse { get; set; }

        /// <summary>Gets or sets the full expand string for the KryptonOutlookGrid.</summary>
        public string FullExpand { get; set; }

        /// <summary>Gets or sets the gradient fill string for the KryptonOutlookGrid.</summary>
        public string GradientFill { get; set; }

        /// <summary>Gets or sets the group string for the KryptonOutlookGrid.</summary>
        public string Group { get; set; }

        /// <summary>Gets or sets the group interval string for the KryptonOutlookGrid.</summary>
        public string GroupInterval { get; set; }

        /// <summary>Gets or sets the hide group box string for the KryptonOutlookGrid.</summary>
        public string HideGroupBox { get; set; }

        /// <summary>Gets or sets the in three weeks string for the KryptonOutlookGrid.</summary>
        public string? InThreeWeeks { get; set; }

        /// <summary>Gets or sets the in two weeks string for the KryptonOutlookGrid.</summary>
        public string? InTwoWeeks { get; set; }

        /// <summary>Gets or sets the later during this month string for the KryptonOutlookGrid.</summary>
        public string? LaterDuringThisMonth { get; set; }

        /// <summary>Gets or sets the month string for the KryptonOutlookGrid.</summary>
        public string Month { get; set; }

        /// <summary>Gets or sets the next month string for the KryptonOutlookGrid.</summary>
        public string? NextMonth { get; set; }

        /// <summary>Gets or sets the next week string for the KryptonOutlookGrid.</summary>
        public string? NextWeek { get; set; }

        /// <summary>Gets or sets the no date string for the KryptonOutlookGrid.</summary>
        public string? NoDate { get; set; }

        /// <summary>Gets or sets the older string for the KryptonOutlookGrid.</summary>
        public string? Older { get; set; }

        /// <summary>Gets or sets the one item string for the KryptonOutlookGrid.</summary>
        public string OneItem { get; set; }

        /// <summary>Gets or sets the other string for the KryptonOutlookGrid.</summary>
        public string Other { get; set; }

        /// <summary>Gets or sets the palette custom string for the KryptonOutlookGrid.</summary>
        public string PaletteCustom { get; set; }

        /// <summary>Gets or sets the palette custom heading string for the KryptonOutlookGrid.</summary>
        public string PaletteCustomHeading { get; set; }

        /// <summary>Gets or sets the previous month string for the KryptonOutlookGrid.</summary>
        public string? PreviousMonth { get; set; }

        /// <summary>Gets or sets the previous week string for the KryptonOutlookGrid.</summary>
        public string? PreviousWeek { get; set; }

        /// <summary>Gets or sets the previous year string for the KryptonOutlookGrid.</summary>
        public string? PreviousYear { get; set; }

        /// <summary>Gets or sets the quarter one string for the KryptonOutlookGrid.</summary>
        public string QuarterOne { get; set; }

        /// <summary>Gets or sets the quarter two string for the KryptonOutlookGrid.</summary>
        public string QuarterTwo { get; set; }

        /// <summary>Gets or sets the quarter three string for the KryptonOutlookGrid.</summary>
        public string QuarterThree { get; set; }

        /// <summary>Gets or sets the quarter four string for the KryptonOutlookGrid.</summary>
        public string QuarterFour { get; set; }

        /// <summary>Gets or sets the quarter string for the KryptonOutlookGrid.</summary>
        public string Quarter { get; set; }

        /// <summary>Gets or sets the show group box string for the KryptonOutlookGrid.</summary>
        public string ShowGroupBox { get; set; }

        /// <summary>Gets or sets the smart string for the KryptonOutlookGrid.</summary>
        public string Smart { get; set; }

        /// <summary>Gets or sets the solid fill string for the KryptonOutlookGrid.</summary>
        public string SolidFill { get; set; }

        /// <summary>Gets or sets the sort ascending string for the KryptonOutlookGrid.</summary>
        public string SortAscending { get; set; }

        /// <summary>Gets or sets the sort by summary count string for the KryptonOutlookGrid.</summary>
        public string SortBySummaryCount { get; set; }

        /// <summary>Gets or sets the sort descending string for the KryptonOutlookGrid.</summary>
        public string SortDescending { get; set; }

        /// <summary>Gets or sets the three colors range string for the KryptonOutlookGrid.</summary>
        public string ThreeColorsRange { get; set; }

        /// <summary>Gets or sets the three weeks ago string for the KryptonOutlookGrid.</summary>
        public string? ThreeWeeksAgo { get; set; }

        /// <summary>Gets or sets the today string for the KryptonOutlookGrid.</summary>
        public string? Today { get; set; }

        /// <summary>Gets or sets the tomorrow string for the KryptonOutlookGrid.</summary>
        public string? Tomorrow { get; set; }

        /// <summary>Gets or sets the two colors range string for the KryptonOutlookGrid.</summary>
        public string TwoColorsRange { get; set; }

        /// <summary>Gets or sets the two weeks ago string for the KryptonOutlookGrid.</summary>
        public string? TwoWeeksAgo { get; set; }

        /// <summary>Gets or sets the UnGroup string for the KryptonOutlookGrid.</summary>
        public string UnGroup { get; set; }

        /// <summary>Gets or sets the unknown string for the KryptonOutlookGrid.</summary>
        public string Unknown { get; set; }

        /// <summary>Gets or sets the number of items string for the KryptonOutlookGrid.</summary>
        public string? NumberOfItems { get; set; }

        /// <summary>Gets or sets the year string for the KryptonOutlookGrid.</summary>
        public string Year { get; set; }

        /// <summary>Gets or sets the year group text string for the KryptonOutlookGrid.</summary>
        public string YearGroupText { get; set; }

        /// <summary>Gets or sets the yesterday string for the KryptonOutlookGrid.</summary>
        public string? Yesterday { get; set; }

        public string? Monday { get; set; }

        public string? Tuesday { get; set; }

        public string? Wednesday { get; set; }

        public string? Thursday { get; set; }

        public string? Friday { get; set; }

        public string? Saturday { get; set; }

        public string Sunday { get; set; }

        public string MinimumColour { get; set; }

        public string MediumColour { get; set; }

        public string MaximumColour { get; set; }

        #endregion

        #region Custom Format Window

        public string CustomFormatWindowTitle { get; set; }

        public string CustomFormatLabelText { get; set; }

        public string CustomFormatPreviewLabelText { get; set; }

        public string CustomFormatMinimumColorButtonText { get; set; }

        public string CustomFormatIntermediateColorButtonText { get; set; }

        public string CustomFormatMaximumColorButtonText { get; set; }

        public string CustomFormatFillLabelText { get; set; }

        #endregion

        #endregion

        #region Implementation

        public void Reset()
        {
            #region OutlookGrid Component

            AfterNextMonth = DEFAULT_AFTER_NEXT_MONTH;

            AlphabeticGroupText = DEFAULT_ALPHABETIC_GROUP_TEXT;

            Bar = DEFAULT_BAR;

            BeforePreviousMonth = DEFAULT_BEFORE_PREVIOUS_MONTH;

            BestFitAll = DEFAULT_BEST_FIT_ALL;

            BestFit = DEFAULT_BEST_FIT;

            Cancel = DEFAULT_CANCEL;

            ClearGrouping = DEFAULT_CLEAR_GROUPING;

            ClearRules = DEFAULT_CLEAR_RULES;

            ClearSorting = DEFAULT_CLEAR_SORTING;

            Collapse = DEFAULT_COLLAPSE;

            Columns = DEFAULT_COLUMNS;

            ConditionalFormatting = DEFAULT_CONDITIONAL_FORMATTING;

            CustomThreeDots = DEFAULT_CUSTOM_THREE_DOTS;

            DateGroupText = DEFAULT_DATE_GROUP_TEXT;

            Day = DEFAULT_DAY;

            DragColumnToGroup = DEFAULT_DRAG_COLUMN_TO_GROUP;

            EarlierDuringThisMonth = DEFAULT_EARLIER_DURING_THIS_MONTH;

            EarlierDuringThisYear = DEFAULT_EARLIER_THIS_YEAR;

            Expand = DEFAULT_EXPAND;

            Finish = DEFAULT_FINISH;

            FullCollapse = DEFAULT_FULL_COLLAPSE;

            FullExpand = DEFAULT_FULL_EXPAND;

            GradientFill = DEFAULT_GRADIENT_FILL;

            Group = DEFAULT_GROUP;

            GroupInterval = DEFAULT_GROUP_INTERVAL;

            HideGroupBox = DEFAULT_HIDE_GROUP_BOX;

            InThreeWeeks = DEFAULT_IN_THREE_WEEKS;

            InTwoWeeks = DEFAULT_IN_TWO_WEEKS;

            LaterDuringThisMonth = DEFAULT_LATER_DURING_THIS_MONTH;

            Month = DEFAULT_MONTH;

            NextMonth = DEFAULT_NEXT_MONTH;

            NextWeek = DEFAULT_NEXT_WEEK;

            NoDate = DEFAULT_NO_DATE;

            Older = DEFAULT_OLDER;

            OneItem = DEFAULT_ONE_ITEM;

            Other = DEFAULT_OTHER;

            PaletteCustom = DEFAULT_PALETTE_CUSTOM;

            PaletteCustomHeading = DEFAULT_PALETTE_CUSTOM_HEADING;

            PreviousMonth = DEFAULT_PREVIOUS_MONTH;

            PreviousWeek = DEFAULT_PREVIOUS_WEEK;

            PreviousYear = DEFAULT_PREVIOUS_YEAR;

            QuarterOne = DEFAULT_QUARTER_ONE;

            QuarterTwo = DEFAULT_QUARTER_TWO;

            QuarterThree = DEFAULT_QUARTER_THREE;

            QuarterFour = DEFAULT_QUARTER_FOUR;

            Quarter = DEFAULT_QUARTER;

            ShowGroupBox = DEFAULT_SHOW_GROUP_BOX;

            Smart = DEFAULT_SMART;

            SolidFill = DEFAULT_SOLID_FILL;

            SortAscending = DEFAULT_SORT_ASCENDING;

            SortBySummaryCount = DEFAULT_SORT_BY_SUMMARY_COUNT;

            SortDescending = DEFAULT_SORT_DESCENDING;

            ThreeColorsRange = DEFAULT_THREE_COLORS_RANGE;

            ThreeWeeksAgo = DEFAULT_THREE_WEEKS_AGO;

            Today = DEFAULT_TODAY;

            Tomorrow = DEFAULT_TOMORROW;

            TwoColorsRange = DEFAULT_TWO_COLORS_RANGE;

            TwoWeeksAgo = DEFAULT_TWO_WEEKS_AGO;

            UnGroup = DEFAULT_UNGROUP;

            Unknown = DEFAULT_UNKNOWN;

            NumberOfItems = DEFAULT_NUMBER_OF_ITEMS;

            Year = DEFAULT_YEAR;

            YearGroupText = DEFAULT_YEAR_GROUP_TEXT;

            Yesterday = DEFAULT_YESTERDAY;

            Monday = DEFAULT_MONDAY;

            Tuesday = DEFAULT_TUESDAY;

            Wednesday = DEFAULT_WEDNESDAY;

            Thursday = DEFAULT_THURSDAY;

            Friday = DEFAULT_FRIDAY;

            Saturday = DEFAULT_SATURDAY;

            Sunday = DEFAULT_SUNDAY;

            MinimumColour = DEFAULT_MINIMUM_COLOUR;

            MediumColour = DEFAULT_MEDIUM_COLOUR;

            MaximumColour = DEFAULT_MAXIMUM_COLOUR;

            #endregion
        }

        #endregion
    }
}