#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Designer for a collection of context menu items.
/// </summary>
public partial class KryptonContextMenuCollectionEditor : KryptonDesignerCollectionEditor
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuCollectionEditor class.
    /// </summary>
    public KryptonContextMenuCollectionEditor()
        : base(typeof(KryptonContextMenuCollection))
    {
    }
    #endregion

    #region Internal
    /// <summary>
    /// Creates the shared Krypton context-menu collection editor form.
    /// </summary>
    /// <param name="editor">Owning collection editor.</param>
    /// <returns>Editor form instance.</returns>
    internal static VisualDesignerCollectionForm CreateCollectionForm(KryptonDesignerCollectionEditor editor) =>
        new VisualContextMenuCollectionForm(editor);
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Creates the Krypton-themed collection editor form.
    /// </summary>
    /// <returns>Editor form instance.</returns>
    protected override VisualDesignerCollectionForm CreateKryptonDesignerCollectionForm() =>
        CreateCollectionForm(this);

    /// <summary>
    /// Gets the data types that this collection editor can contain. 
    /// </summary>
    /// <returns>An array of data types that this collection can contain.</returns>
    protected override Type[] CreateNewItemTypes() =>
    [
        typeof(KryptonContextMenuItems),
        typeof(KryptonContextMenuItem),
        typeof(KryptonContextMenuSeparator),
        typeof(KryptonContextMenuHeading),
        typeof(KryptonContextMenuLinkLabel),
        typeof(KryptonContextMenuCheckBox),
        typeof(KryptonContextMenuCheckButton),
        typeof(KryptonContextMenuRadioButton),
        typeof(KryptonContextMenuColorColumns),
        typeof(KryptonContextMenuMonthCalendar),
        typeof(KryptonContextMenuImageSelect),
        typeof(KryptonContextMenuTextBox),
        typeof(KryptonContextMenuComboBox),
        typeof(KryptonContextMenuProgressBar)
    ];
    #endregion
}