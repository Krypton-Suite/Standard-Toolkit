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
/// Displays a message-box style dialog with a collapsible ("foldable") details region, modelled on the
/// Visual Studio Just-In-Time debugger dialog. This is the public interface to the internal
/// <see cref="VisualFoldableDialogForm"/> class.
/// </summary>
/// <remarks>
/// The dialog shows an icon, a heading, a message and a set of action buttons. When
/// <see cref="KryptonFoldableDialogData.DetailsText"/> is supplied, an expander control is displayed that
/// folds the additional details in and out, resizing the dialog accordingly.
/// </remarks>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonFoldableDialog
{
    #region Public

    /// <summary>Displays a foldable dialog described by the supplied <paramref name="data"/>.</summary>
    /// <param name="data">The data describing the dialog content and behaviour. Cannot be null.</param>
    /// <returns>The <see cref="DialogResult"/> produced by the button the user clicked.</returns>
    public static DialogResult Show(KryptonFoldableDialogData data) => ShowCore(data);

    /// <summary>Displays a foldable dialog with the supplied heading, message and collapsible details.</summary>
    /// <param name="heading">The bold main instruction shown at the top of the dialog.</param>
    /// <param name="text">The descriptive message text shown below the heading.</param>
    /// <param name="detailsText">The text shown inside the collapsible details region. When null or empty, no expander is shown.</param>
    /// <param name="caption">The window title (caption) of the dialog.</param>
    /// <param name="buttons">The set of action buttons to display. Defaults to <see cref="KryptonMessageBoxButtons.OK"/>.</param>
    /// <param name="icon">The icon to display. Defaults to <see cref="ExtendedKryptonMessageBoxIcon.None"/>.</param>
    /// <returns>The <see cref="DialogResult"/> produced by the button the user clicked.</returns>
    public static DialogResult Show(string? heading, string? text, string? detailsText, string? caption,
        KryptonMessageBoxButtons buttons = KryptonMessageBoxButtons.OK,
        ExtendedKryptonMessageBoxIcon icon = ExtendedKryptonMessageBoxIcon.None) =>
        ShowCore(new KryptonFoldableDialogData
        {
            Heading = heading,
            Text = text,
            DetailsText = detailsText,
            Caption = caption,
            Buttons = buttons,
            Icon = icon
        });

    /// <summary>Displays a foldable dialog owned by the supplied window.</summary>
    /// <param name="owner">The window that owns the dialog.</param>
    /// <param name="heading">The bold main instruction shown at the top of the dialog.</param>
    /// <param name="text">The descriptive message text shown below the heading.</param>
    /// <param name="detailsText">The text shown inside the collapsible details region. When null or empty, no expander is shown.</param>
    /// <param name="caption">The window title (caption) of the dialog.</param>
    /// <param name="buttons">The set of action buttons to display. Defaults to <see cref="KryptonMessageBoxButtons.OK"/>.</param>
    /// <param name="icon">The icon to display. Defaults to <see cref="ExtendedKryptonMessageBoxIcon.None"/>.</param>
    /// <returns>The <see cref="DialogResult"/> produced by the button the user clicked.</returns>
    public static DialogResult Show(IWin32Window? owner, string? heading, string? text, string? detailsText, string? caption,
        KryptonMessageBoxButtons buttons = KryptonMessageBoxButtons.OK,
        ExtendedKryptonMessageBoxIcon icon = ExtendedKryptonMessageBoxIcon.None) =>
        ShowCore(new KryptonFoldableDialogData
        {
            Owner = owner,
            Heading = heading,
            Text = text,
            DetailsText = detailsText,
            Caption = caption,
            Buttons = buttons,
            Icon = icon
        });

    #endregion

    #region Implementation

    private static DialogResult ShowCore(KryptonFoldableDialogData data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        return VisualFoldableDialogForm.Show(data);
    }

    #endregion
}
