#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Read-only runtime state for <see cref="KryptonDropZone"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonDropZoneDataValues
{
    #region Instance Fields

    private readonly KryptonDropZone _owner;

    #endregion

    #region Identity

    internal KryptonDropZoneDataValues(KryptonDropZone owner) => _owner = owner;

    #endregion

    #region Files

    [Category(@"Files")]
    [Description(@"Gets the list of currently dropped files.")]
    [Browsable(true)]
    [ReadOnly(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IReadOnlyList<string> DroppedFiles => _owner.GetDroppedFilesSnapshot();

    [Category(@"Files")]
    [Description(@"Gets the count of dropped files.")]
    [Browsable(true)]
    [ReadOnly(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int FileCount => _owner.GetDroppedFileCount();

    [Category(@"Files")]
    [Description(@"Gets the combined size in bytes of all dropped files.")]
    [Browsable(true)]
    [ReadOnly(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public long TotalDroppedSize => _owner.GetTotalDroppedSize();

    [Category(@"Files")]
    [Description(@"Gets the remaining upload size quota in bytes, or long.MaxValue when no quota is set.")]
    [Browsable(true)]
    [ReadOnly(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public long RemainingUploadSize => _owner.GetRemainingUploadSize();

    #endregion

    #region Selection

    [Category(@"Selection")]
    [Description(@"Gets the full path of the first selected file, or null when nothing is selected.")]
    [Browsable(true)]
    [ReadOnly(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? SelectedFile => _owner.GetSelectedFilePath();

    [Category(@"Selection")]
    [Description(@"Gets the paths of the currently selected files.")]
    [Browsable(true)]
    [ReadOnly(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IReadOnlyList<string> SelectedFiles => _owner.GetSelectedFilePaths();

    #endregion

    #region Undo

    [Category(@"Undo")]
    [Description(@"Whether an undo operation is currently available.")]
    [Browsable(true)]
    [ReadOnly(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CanUndo => _owner.GetCanUndo();

    #endregion

    #region Animation

    [Category(@"Animation")]
    [Description(@"The drop-zone animation scenario currently being displayed.")]
    [Browsable(true)]
    [ReadOnly(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonDropZone.DropZoneAnimationScenario CurrentAnimationScenario => _owner.GetCurrentAnimationScenario();

    #endregion
}
