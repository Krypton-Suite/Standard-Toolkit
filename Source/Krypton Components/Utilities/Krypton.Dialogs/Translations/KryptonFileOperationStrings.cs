#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Dialogs;

/// <summary>
/// Localizable text for <see cref="KryptonFileOperation"/> and its dialogs.
/// The strings live on the operation because <c>Krypton.Toolkit</c> does not reference this assembly.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public sealed class KryptonFileOperationStrings
{
    private const string DefaultCopying = "Copying";
    private const string DefaultMoving = "Moving";
    private const string DefaultDeleting = "Deleting";
    private const string DefaultRecycling = "Recycling";
    private const string DefaultRenaming = "Renaming";
    private const string DefaultCopyingAndMoving = "Copying and moving";
    private const string DefaultCalculating = "Calculating...";
    private const string DefaultUndoing = "Undoing...";
    private const string DefaultFromLabel = "From:";
    private const string DefaultToLabel = "To:";
    private const string DefaultPause = "Pause";
    private const string DefaultResume = "Resume";
    private const string DefaultCancel = "Cancel";
    private const string DefaultReplace = "Replace";
    private const string DefaultSkip = "Skip";
    private const string DefaultKeepBoth = "Keep both";
    private const string DefaultApplyToRemaining = "Do this for the remaining conflicts";
    private const string DefaultConflictTitle = "Replace or skip files";
    private const string DefaultConflictIntro = "The destination already has a file or folder with this name.";
    private const string DefaultDeleteTitle = "Delete";
    private const string DefaultDeleteOne = "Are you sure you want to move this item to the Recycle Bin?";
    private const string DefaultDeleteMany = "Are you sure you want to move these {0} items to the Recycle Bin?";
    private const string DefaultPermanentTitle = "Permanent delete";
    private const string DefaultPermanentOne = "Are you sure you want to permanently delete this item?";
    private const string DefaultPermanentMany = "Are you sure you want to permanently delete these {0} items?";
    private const string DefaultYes = "Yes";
    private const string DefaultNo = "No";
    private const string DefaultItemsOf = "{0} of {1} items";
    private const string DefaultBytesOf = "{0} of {1}";
    private const string DefaultSpeedPerSecond = "{0}/s";
    private const string DefaultTimeRemaining = "{0} remaining";
    private const string DefaultMoreDetails = "More details";
    private const string DefaultFewerDetails = "Fewer details";
    private const string DefaultSpeedLabel = "Speed: {0}";

    /// <summary>Gets or sets the copy progress title.</summary>
    [Localizable(true)]
    public string Copying { get; set; } = DefaultCopying;

    /// <summary>Gets or sets the move progress title.</summary>
    [Localizable(true)]
    public string Moving { get; set; } = DefaultMoving;

    /// <summary>Gets or sets the permanent-delete progress title.</summary>
    [Localizable(true)]
    public string Deleting { get; set; } = DefaultDeleting;

    /// <summary>Gets or sets the Recycle Bin progress title.</summary>
    [Localizable(true)]
    public string Recycling { get; set; } = DefaultRecycling;

    /// <summary>Gets or sets the rename progress title.</summary>
    [Localizable(true)]
    public string Renaming { get; set; } = DefaultRenaming;

    /// <summary>Gets or sets the title used when a move crosses volumes.</summary>
    [Localizable(true)]
    public string CopyingAndMoving { get; set; } = DefaultCopyingAndMoving;

    /// <summary>Gets or sets the text shown while the size is still unknown.</summary>
    [Localizable(true)]
    public string Calculating { get; set; } = DefaultCalculating;

    /// <summary>Gets or sets the title used while the shell undoes the operation.</summary>
    [Localizable(true)]
    public string Undoing { get; set; } = DefaultUndoing;

    /// <summary>Gets or sets the source location label.</summary>
    [Localizable(true)]
    public string FromLabel { get; set; } = DefaultFromLabel;

    /// <summary>Gets or sets the destination location label.</summary>
    [Localizable(true)]
    public string ToLabel { get; set; } = DefaultToLabel;

    /// <summary>Gets or sets the pause button text.</summary>
    [Localizable(true)]
    public string Pause { get; set; } = DefaultPause;

    /// <summary>Gets or sets the resume button text.</summary>
    [Localizable(true)]
    public string Resume { get; set; } = DefaultResume;

    /// <summary>Gets or sets the cancel button text.</summary>
    [Localizable(true)]
    public string Cancel { get; set; } = DefaultCancel;

    /// <summary>Gets or sets the replace button text.</summary>
    [Localizable(true)]
    public string Replace { get; set; } = DefaultReplace;

    /// <summary>Gets or sets the skip button text.</summary>
    [Localizable(true)]
    public string Skip { get; set; } = DefaultSkip;

    /// <summary>Gets or sets the keep-both button text.</summary>
    [Localizable(true)]
    public string KeepBoth { get; set; } = DefaultKeepBoth;

    /// <summary>Gets or sets the apply-to-remaining checkbox text.</summary>
    [Localizable(true)]
    public string ApplyToRemaining { get; set; } = DefaultApplyToRemaining;

    /// <summary>Gets or sets the conflict dialog caption.</summary>
    [Localizable(true)]
    public string ConflictTitle { get; set; } = DefaultConflictTitle;

    /// <summary>Gets or sets the conflict dialog introduction.</summary>
    [Localizable(true)]
    public string ConflictIntro { get; set; } = DefaultConflictIntro;

    /// <summary>Gets or sets the Recycle Bin confirmation caption.</summary>
    [Localizable(true)]
    public string DeleteTitle { get; set; } = DefaultDeleteTitle;

    /// <summary>Gets or sets the Recycle Bin confirmation for one item.</summary>
    [Localizable(true)]
    public string DeleteOne { get; set; } = DefaultDeleteOne;

    /// <summary>Gets or sets the Recycle Bin confirmation for several items. <c>{0}</c> is the count.</summary>
    [Localizable(true)]
    public string DeleteMany { get; set; } = DefaultDeleteMany;

    /// <summary>Gets or sets the permanent-delete confirmation caption.</summary>
    [Localizable(true)]
    public string PermanentTitle { get; set; } = DefaultPermanentTitle;

    /// <summary>Gets or sets the permanent-delete confirmation for one item.</summary>
    [Localizable(true)]
    public string PermanentOne { get; set; } = DefaultPermanentOne;

    /// <summary>Gets or sets the permanent-delete confirmation for several items. <c>{0}</c> is the count.</summary>
    [Localizable(true)]
    public string PermanentMany { get; set; } = DefaultPermanentMany;

    /// <summary>Gets or sets the Yes button text.</summary>
    [Localizable(true)]
    public string Yes { get; set; } = DefaultYes;

    /// <summary>Gets or sets the No button text.</summary>
    [Localizable(true)]
    public string No { get; set; } = DefaultNo;

    /// <summary>Gets or sets the item-count line. <c>{0}</c> is current, <c>{1}</c> is total.</summary>
    [Localizable(true)]
    public string ItemsOf { get; set; } = DefaultItemsOf;

    /// <summary>Gets or sets the byte-count line. <c>{0}</c> is current, <c>{1}</c> is total.</summary>
    [Localizable(true)]
    public string BytesOf { get; set; } = DefaultBytesOf;

    /// <summary>Gets or sets the speed line. <c>{0}</c> is the formatted rate.</summary>
    [Localizable(true)]
    public string SpeedPerSecond { get; set; } = DefaultSpeedPerSecond;

    /// <summary>Gets or sets the time-remaining line. <c>{0}</c> is the formatted duration.</summary>
    [Localizable(true)]
    public string TimeRemaining { get; set; } = DefaultTimeRemaining;

    /// <summary>Gets or sets the caption that expands the progress dialog.</summary>
    [Localizable(true)]
    public string MoreDetails { get; set; } = DefaultMoreDetails;

    /// <summary>Gets or sets the caption that collapses the progress dialog.</summary>
    [Localizable(true)]
    public string FewerDetails { get; set; } = DefaultFewerDetails;

    /// <summary>Gets or sets the speed-graph overlay. <c>{0}</c> is the formatted rate.</summary>
    [Localizable(true)]
    public string SpeedLabel { get; set; } = DefaultSpeedLabel;

    /// <summary>Gets whether every string is still the factory text.</summary>
    [Browsable(false)]
    public bool IsDefault =>
        Copying == DefaultCopying &&
        Moving == DefaultMoving &&
        Deleting == DefaultDeleting &&
        Recycling == DefaultRecycling &&
        Renaming == DefaultRenaming &&
        CopyingAndMoving == DefaultCopyingAndMoving &&
        Calculating == DefaultCalculating &&
        Undoing == DefaultUndoing &&
        FromLabel == DefaultFromLabel &&
        ToLabel == DefaultToLabel &&
        Pause == DefaultPause &&
        Resume == DefaultResume &&
        Cancel == DefaultCancel &&
        Replace == DefaultReplace &&
        Skip == DefaultSkip &&
        KeepBoth == DefaultKeepBoth &&
        ApplyToRemaining == DefaultApplyToRemaining &&
        ConflictTitle == DefaultConflictTitle &&
        ConflictIntro == DefaultConflictIntro &&
        DeleteTitle == DefaultDeleteTitle &&
        DeleteOne == DefaultDeleteOne &&
        DeleteMany == DefaultDeleteMany &&
        PermanentTitle == DefaultPermanentTitle &&
        PermanentOne == DefaultPermanentOne &&
        PermanentMany == DefaultPermanentMany &&
        Yes == DefaultYes &&
        No == DefaultNo &&
        ItemsOf == DefaultItemsOf &&
        BytesOf == DefaultBytesOf &&
        SpeedPerSecond == DefaultSpeedPerSecond &&
        TimeRemaining == DefaultTimeRemaining &&
        MoreDetails == DefaultMoreDetails &&
        FewerDetails == DefaultFewerDetails &&
        SpeedLabel == DefaultSpeedLabel;

    /// <summary>Restores every string to the factory text.</summary>
    public void Reset()
    {
        Copying = DefaultCopying;
        Moving = DefaultMoving;
        Deleting = DefaultDeleting;
        Recycling = DefaultRecycling;
        Renaming = DefaultRenaming;
        CopyingAndMoving = DefaultCopyingAndMoving;
        Calculating = DefaultCalculating;
        Undoing = DefaultUndoing;
        FromLabel = DefaultFromLabel;
        ToLabel = DefaultToLabel;
        Pause = DefaultPause;
        Resume = DefaultResume;
        Cancel = DefaultCancel;
        Replace = DefaultReplace;
        Skip = DefaultSkip;
        KeepBoth = DefaultKeepBoth;
        ApplyToRemaining = DefaultApplyToRemaining;
        ConflictTitle = DefaultConflictTitle;
        ConflictIntro = DefaultConflictIntro;
        DeleteTitle = DefaultDeleteTitle;
        DeleteOne = DefaultDeleteOne;
        DeleteMany = DefaultDeleteMany;
        PermanentTitle = DefaultPermanentTitle;
        PermanentOne = DefaultPermanentOne;
        PermanentMany = DefaultPermanentMany;
        Yes = DefaultYes;
        No = DefaultNo;
        ItemsOf = DefaultItemsOf;
        BytesOf = DefaultBytesOf;
        SpeedPerSecond = DefaultSpeedPerSecond;
        TimeRemaining = DefaultTimeRemaining;
        MoreDetails = DefaultMoreDetails;
        FewerDetails = DefaultFewerDetails;
        SpeedLabel = DefaultSpeedLabel;
    }

    /// <summary>Returns an empty string when the values are still the factory text.</summary>
    public override string ToString() => IsDefault ? string.Empty : "Modified";
}
