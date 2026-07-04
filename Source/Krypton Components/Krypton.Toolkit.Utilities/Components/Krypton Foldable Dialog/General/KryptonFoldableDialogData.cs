#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Describes the content and behaviour of a <see cref="KryptonFoldableDialog"/>.
/// </summary>
/// <remarks>
/// A foldable dialog is a message-box style window with a collapsible ("foldable") details region,
/// modelled on the Visual Studio Just-In-Time debugger dialog. The dialog is rendered by the internal
/// <c>VisualFoldableDialogForm</c> and displayed via <see cref="KryptonFoldableDialog.Show(KryptonFoldableDialogData)"/>.
/// </remarks>
public class KryptonFoldableDialogData
{
    #region Instance Fields

    private readonly string _expandText = KryptonManager.Strings.FoldableDialogStrings.ExpandText;
    private readonly string _collapseText = KryptonManager.Strings.FoldableDialogStrings.CollapseText;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonFoldableDialogData"/> class.</summary>
    public KryptonFoldableDialogData()
    {
        Icon = ExtendedKryptonMessageBoxIcon.None;
        Buttons = KryptonMessageBoxButtons.OK;
        DefaultButton = KryptonMessageBoxDefaultButton.Button1;
        ExpandButtonText = _expandText;
        CollapseButtonText = _collapseText;
        StartPosition = FormStartPosition.CenterScreen;
    }

    #endregion

    #region Public

    /// <summary>Gets or sets the window title (caption) of the dialog.</summary>
    public string? Caption { get; set; }

    /// <summary>Gets or sets the main instruction shown as a bold title above the message text.</summary>
    public string? Heading { get; set; }

    /// <summary>Gets or sets the descriptive message text shown below the heading.</summary>
    public string? Text { get; set; }

    /// <summary>Gets or sets the text shown inside the collapsible ("foldable") details region.</summary>
    /// <remarks>When <see langword="null"/> or empty, the expander control and details region are hidden.</remarks>
    public string? DetailsText { get; set; }

    /// <summary>Gets or sets the icon displayed to the left of the heading.</summary>
    public ExtendedKryptonMessageBoxIcon Icon { get; set; }

    /// <summary>Gets or sets a custom icon image used when <see cref="Icon"/> is <see cref="ExtendedKryptonMessageBoxIcon.Custom"/>.</summary>
    public Image? CustomIcon { get; set; }

    /// <summary>Gets or sets the set of action buttons shown along the bottom of the dialog.</summary>
    public KryptonMessageBoxButtons Buttons { get; set; }

    /// <summary>Gets or sets which action button is the default (focused) button.</summary>
    public KryptonMessageBoxDefaultButton DefaultButton { get; set; }

    /// <summary>Gets or sets a value indicating whether the details region is expanded when the dialog is first shown.</summary>
    public bool Expanded { get; set; }

    /// <summary>Gets or sets the text of the expander control while the details region is collapsed.</summary>
    public string? ExpandButtonText { get; set; }

    /// <summary>Gets or sets the text of the expander control while the details region is expanded.</summary>
    public string? CollapseButtonText { get; set; }

    /// <summary>Gets or sets the window that owns the dialog.</summary>
    public IWin32Window? Owner { get; set; }

    /// <summary>Gets or sets the initial position of the dialog when it is first shown.</summary>
    /// <remarks>
    /// Defaults to <see cref="FormStartPosition.CenterScreen"/>. <see cref="FormStartPosition.CenterParent"/>
    /// centres the dialog on its <see cref="Owner"/> (falling back to the screen when no owner is set), and
    /// <see cref="FormStartPosition.Manual"/> leaves the position unchanged.
    /// </remarks>
    public FormStartPosition StartPosition { get; set; }

    #endregion
}
