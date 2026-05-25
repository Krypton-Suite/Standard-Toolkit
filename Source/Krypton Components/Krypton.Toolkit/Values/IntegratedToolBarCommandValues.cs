#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class IntegratedToolBarCommandValues : GlobalId
{
    #region Identity

    /// <summary>Initializes a new instance of the <see cref="IntegratedToolBarCommandValues"/> class.</summary>
    public IntegratedToolBarCommandValues()
    {
        Reset();
    }

    /// <summary>Converts to string.</summary>
    /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticVariables.DEFAULT_EMPTY_STRING;

    #endregion

    #region Public

    /// <summary>Gets a value indicating whether this instance is default.</summary>
    [Browsable(false)]
    public bool IsDefault => NewButtonCommand is null &&
                             OpenButtonCommand is null &&
                             SaveButtonCommand is null &&
                             SaveAllButtonCommand is null &&
                             SaveAsButtonCommand is null &&
                             CutButtonCommand is null &&
                             CopyButtonCommand is null &&
                             PasteButtonCommand is null &&
                             UndoButtonCommand is null &&
                             RedoButtonCommand is null &&
                             PageSetupButtonCommand is null &&
                             PrintPreviewButtonCommand is null &&
                             PrintButtonCommand is null &&
                             QuickPrintButtonCommand is null;

    /// <summary>Resets this instance.</summary>
    public void Reset()
    {
        NewButtonCommand = null;
        OpenButtonCommand = null;
        SaveButtonCommand = null;
        SaveAllButtonCommand = null;
        SaveAsButtonCommand = null;
        CutButtonCommand = null;
        CopyButtonCommand = null;
        PasteButtonCommand = null;
        UndoButtonCommand = null;
        RedoButtonCommand = null;
        PageSetupButtonCommand = null;
        PrintPreviewButtonCommand = null;
        PrintButtonCommand = null;
        QuickPrintButtonCommand = null;
    }

    /// <summary>
    /// Assigns configured commands to the integrated toolbar button array.
    /// </summary>
    /// <param name="buttons">Integrated toolbar buttons (14 items).</param>
    public void ApplyTo(ButtonSpecAny[] buttons) => IntegratedToolBarCommandWiring.ApplyCommands(buttons, this);

    /// <summary>Gets or sets the command for the New toolbar button.</summary>
    [DefaultValue(null)]
    public KryptonCommand? NewButtonCommand { get; set; }

    /// <summary>Gets or sets the command for the Open toolbar button.</summary>
    [DefaultValue(null)]
    public KryptonCommand? OpenButtonCommand { get; set; }

    /// <summary>Gets or sets the command for the Save toolbar button.</summary>
    [DefaultValue(null)]
    public KryptonCommand? SaveButtonCommand { get; set; }

    /// <summary>Gets or sets the command for the Save All toolbar button.</summary>
    [DefaultValue(null)]
    public KryptonCommand? SaveAllButtonCommand { get; set; }

    /// <summary>Gets or sets the command for the Save As toolbar button.</summary>
    [DefaultValue(null)]
    public KryptonCommand? SaveAsButtonCommand { get; set; }

    /// <summary>Gets or sets the command for the Cut toolbar button.</summary>
    [DefaultValue(null)]
    public KryptonCommand? CutButtonCommand { get; set; }

    /// <summary>Gets or sets the command for the Copy toolbar button.</summary>
    [DefaultValue(null)]
    public KryptonCommand? CopyButtonCommand { get; set; }

    /// <summary>Gets or sets the command for the Paste toolbar button.</summary>
    [DefaultValue(null)]
    public KryptonCommand? PasteButtonCommand { get; set; }

    /// <summary>Gets or sets the command for the Undo toolbar button.</summary>
    [DefaultValue(null)]
    public KryptonCommand? UndoButtonCommand { get; set; }

    /// <summary>Gets or sets the command for the Redo toolbar button.</summary>
    [DefaultValue(null)]
    public KryptonCommand? RedoButtonCommand { get; set; }

    /// <summary>Gets or sets the command for the Page Setup toolbar button.</summary>
    [DefaultValue(null)]
    public KryptonCommand? PageSetupButtonCommand { get; set; }

    /// <summary>Gets or sets the command for the Print Preview toolbar button.</summary>
    [DefaultValue(null)]
    public KryptonCommand? PrintPreviewButtonCommand { get; set; }

    /// <summary>Gets or sets the command for the Print toolbar button.</summary>
    [DefaultValue(null)]
    public KryptonCommand? PrintButtonCommand { get; set; }

    /// <summary>Gets or sets the command for the Quick Print toolbar button.</summary>
    [DefaultValue(null)]
    public KryptonCommand? QuickPrintButtonCommand { get; set; }

    #endregion
}
