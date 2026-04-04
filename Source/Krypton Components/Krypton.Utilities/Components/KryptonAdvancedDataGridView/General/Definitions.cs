#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

#region Enum FilterType

/// <summary>
/// Specifies the category of filtering to apply for a column or value.
/// </summary>
public enum FilterType
{
    /// <summary>
    /// The filter type is unknown or not specified.
    /// </summary>
    Unknown,

    /// <summary>
    /// Filter applies to <see cref="System.DateTime"/> values.
    /// </summary>
    DateTime,

    /// <summary>
    /// Filter applies to <see cref="System.TimeSpan"/> values.
    /// </summary>
    TimeSpan,

    /// <summary>
    /// Filter applies to textual/string values.
    /// </summary>
    String,

    /// <summary>
    /// Filter applies to floating point numeric values.
    /// </summary>
    Float,

    /// <summary>
    /// Filter applies to integer numeric values.
    /// </summary>
    Integer
}

#endregion

#region Enum TranslationKey

/// <summary>
/// Available translation keys for KryptonAdvancedDataGridView UI strings.
/// </summary>
public enum TranslationKey
{
    /// <summary>
    /// Sort by date/time ascending.
    /// </summary>
    KryptonAdvancedDataGridViewSortDateTimeAscending,

    /// <summary>
    /// Sort by date/time descending.
    /// </summary>
    KryptonAdvancedDataGridViewSortDateTimeDescending,

    /// <summary>
    /// Sort boolean values with false then true.
    /// </summary>
    KryptonAdvancedDataGridViewSortBoolAscending,

    /// <summary>
    /// Sort boolean values with true then false.
    /// </summary>
    KryptonAdvancedDataGridViewSortBoolDescending,

    /// <summary>
    /// Sort numeric values ascending.
    /// </summary>
    KryptonAdvancedDataGridViewSortNumAscending,

    /// <summary>
    /// Sort numeric values descending.
    /// </summary>
    KryptonAdvancedDataGridViewSortNumDescending,

    /// <summary>
    /// Sort text values ascending (A-Z).
    /// </summary>
    KryptonAdvancedDataGridViewSortTextAscending,

    /// <summary>
    /// Sort text values descending (Z-A).
    /// </summary>
    KryptonAdvancedDataGridViewSortTextDescending,

    /// <summary>
    /// Add a custom filter.
    /// </summary>
    KryptonAdvancedDataGridViewAddCustomFilter,

    /// <summary>
    /// Label for a custom filter.
    /// </summary>
    KryptonAdvancedDataGridViewCustomFilter,

    /// <summary>
    /// Clear the active filter.
    /// </summary>
    KryptonAdvancedDataGridViewClearFilter,

    /// <summary>
    /// Clear the active sort.
    /// </summary>
    KryptonAdvancedDataGridViewClearSort,

    /// <summary>
    /// Text for the filter button.
    /// </summary>
    KryptonAdvancedDataGridViewButtonFilter,

    /// <summary>
    /// Text for the undo filter button.
    /// </summary>
    KryptonAdvancedDataGridViewButtonUndoFilter,

    /// <summary>
    /// Select all nodes in checklist filter.
    /// </summary>
    KryptonAdvancedDataGridViewNodeSelectAll,

    /// <summary>
    /// Select empty/null nodes in checklist filter.
    /// </summary>
    KryptonAdvancedDataGridViewNodeSelectEmpty,

    /// <summary>
    /// Select nodes with true values in checklist filter.
    /// </summary>
    KryptonAdvancedDataGridViewNodeSelectTrue,

    /// <summary>
    /// Select nodes with false values in checklist filter.
    /// </summary>
    KryptonAdvancedDataGridViewNodeSelectFalse,

    /// <summary>
    /// Disable checklist filter.
    /// </summary>
    KryptonAdvancedDataGridViewFilterChecklistDisable,

    /// <summary>
    /// Equality operator label.
    /// </summary>
    KryptonAdvancedDataGridViewEquals,

    /// <summary>
    /// Inequality operator label.
    /// </summary>
    KryptonAdvancedDataGridViewDoesNotEqual,

    /// <summary>
    /// Earlier than operator label.
    /// </summary>
    KryptonAdvancedDataGridViewEarlierThan,

    /// <summary>
    /// Earlier than or equal to operator label.
    /// </summary>
    KryptonAdvancedDataGridViewEarlierThanOrEqualTo,

    /// <summary>
    /// Later than operator label.
    /// </summary>
    KryptonAdvancedDataGridViewLaterThan,

    /// <summary>
    /// Later than or equal to operator label.
    /// </summary>
    KryptonAdvancedDataGridViewLaterThanOrEqualTo,

    /// <summary>
    /// Between operator label.
    /// </summary>
    KryptonAdvancedDataGridViewBetween,

    /// <summary>
    /// Greater than operator label.
    /// </summary>
    KryptonAdvancedDataGridViewGreaterThan,

    /// <summary>
    /// Greater than or equal to operator label.
    /// </summary>
    KryptonAdvancedDataGridViewGreaterThanOrEqualTo,

    /// <summary>
    /// Less than operator label.
    /// </summary>
    KryptonAdvancedDataGridViewLessThan,

    /// <summary>
    /// Less than or equal to operator label.
    /// </summary>
    KryptonAdvancedDataGridViewLessThanOrEqualTo,

    /// <summary>
    /// Begins with operator label.
    /// </summary>
    KryptonAdvancedDataGridViewBeginsWith,

    /// <summary>
    /// Does not begin with operator label.
    /// </summary>
    KryptonAdvancedDataGridViewDoesNotBeginWith,

    /// <summary>
    /// Ends with operator label.
    /// </summary>
    KryptonAdvancedDataGridViewEndsWith,

    /// <summary>
    /// Does not end with operator label.
    /// </summary>
    KryptonAdvancedDataGridViewDoesNotEndWith,

    /// <summary>
    /// Contains operator label.
    /// </summary>
    KryptonAdvancedDataGridViewContains,

    /// <summary>
    /// Does not contain operator label.
    /// </summary>
    KryptonAdvancedDataGridViewDoesNotContain,

    /// <summary>
    /// Invalid value message.
    /// </summary>
    KryptonAdvancedDataGridViewInvalidValue,

    /// <summary>
    /// Description for string filter UI.
    /// </summary>
    KryptonAdvancedDataGridViewFilterStringDescription,

    /// <summary>
    /// Title for the filter form.
    /// </summary>
    KryptonAdvancedDataGridViewFormTitle,

    /// <summary>
    /// Label for column name text.
    /// </summary>
    KryptonAdvancedDataGridViewLabelColumnNameText,

    /// <summary>
    /// Label for logical AND between conditions.
    /// </summary>
    KryptonAdvancedDataGridViewLabelAnd,

    /// <summary>
    /// Text for the "OK" button in the advanced data grid view filter form.
    /// </summary>
    KryptonAdvancedDataGridViewButtonOk,

    /// <summary>
    /// Text for the "Cancel" button in the advanced data grid view filter form.
    /// </summary>
    KryptonAdvancedDataGridViewButtonCancel,
    ADGVSTBLabelSearch,
    ADGVSTBButtonFromBegin,
    ADGVSTBButtonCaseSensitiveToolTip,
    ADGVSTBButtonSearchToolTip,
    ADGVSTBButtonCloseToolTip,
    ADGVSTBButtonWholeWordToolTip,
    ADGVSTBComboBoxColumnsAll,
    ADGVSTBTextBoxSearchToolTip
}

#endregion