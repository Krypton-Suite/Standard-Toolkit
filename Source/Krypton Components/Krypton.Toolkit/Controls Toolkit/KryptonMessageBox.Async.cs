#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Asynchronous dialog helpers for <see cref="KryptonMessageBox"/>.
/// </summary>
public static partial class KryptonMessageBox
{
    #region Public

    /// <summary>
    /// Displays a message box asynchronously with the specified text.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard. If null (default), the button is not shown.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(string text,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null) =>
        ShowCoreAsync(null, text, string.Empty,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton);

    /// <summary>
    /// Displays a message box asynchronously with the specified text and caption.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard. If null (default), the button is not shown.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(string text, string caption, bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null) =>
        ShowCoreAsync(null, text, caption,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton);

    /// <summary>
    /// Displays a message box asynchronously with the specified text, caption and buttons.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="buttons">Which buttons to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard. If null (default), the button is not shown.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(string text, string caption, KryptonMessageBoxButtons buttons,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null) =>
        ShowCoreAsync(null, text, caption, buttons,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton);

    /// <summary>
    /// Displays a message box asynchronously with the specified text, caption, buttons and icon.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="buttons">Which buttons to display in the message box.</param>
    /// <param name="icon">Which icon to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard. If null (default), the button is not shown.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(string text, string caption, KryptonMessageBoxButtons buttons, KryptonMessageBoxIcon icon,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null) =>
        ShowCoreAsync(null, text, caption, buttons, icon,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton);

    /// <summary>
    /// Displays a message box asynchronously with the specified text, caption, buttons, icon and default button.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="buttons">Which buttons to display in the message box.</param>
    /// <param name="icon">Which icon to display in the message box.</param>
    /// <param name="defaultButton">The default button for the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard. If null (default), the button is not shown.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(string text, string caption, KryptonMessageBoxButtons buttons,
        KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null) =>
        ShowCoreAsync(null, text, caption, buttons, icon, defaultButton,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton);

    /// <summary>
    /// Displays a message box asynchronously with the specified text, caption, buttons, icon, default button and options.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="buttons">Which buttons to display in the message box.</param>
    /// <param name="icon">Which icon to display in the message box.</param>
    /// <param name="defaultButton">The default button for the message box.</param>
    /// <param name="options">Display and association options for the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard. If null (default), the button is not shown.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(string text, string caption, KryptonMessageBoxButtons buttons,
        KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton, MessageBoxOptions options,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null) =>
        ShowCoreAsync(null, text, caption, buttons, icon, defaultButton, options,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton);

    /// <summary>
    /// Displays a message box asynchronously with the specified text, caption, buttons, icon, default button, options and help info.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="buttons">Which buttons to display in the message box.</param>
    /// <param name="icon">Which icon to display in the message box.</param>
    /// <param name="defaultButton">The default button for the message box.</param>
    /// <param name="options">Display and association options for the message box.</param>
    /// <param name="helpInfo">Contains the help data of the <see cref="KryptonMessageBox"/>.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard. If null (default), the button is not shown.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(string text, string caption, KryptonMessageBoxButtons buttons,
        KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton, MessageBoxOptions options, HelpInfo helpInfo,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null) =>
        ShowCoreAsync(null, text, caption, buttons, icon, defaultButton, options, helpInfo,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton);

    /// <summary>
    /// Displays a message box asynchronously with an optional Help button and icon overlay.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="buttons">Which buttons to display in the message box.</param>
    /// <param name="displayHelpButton">Displays a Help button.</param>
    /// <param name="icon">Which icon to display in the message box.</param>
    /// <param name="defaultButton">The default button for the message box.</param>
    /// <param name="options">Display and association options for the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard. If null (default), the button is not shown.</param>
    /// <param name="overlayImage">Optional badge image drawn on top of the message icon.</param>
    /// <param name="overlayImagePosition">Corner placement for <paramref name="overlayImage"/>; defaults to bottom-right.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(string text, string caption, KryptonMessageBoxButtons buttons, bool displayHelpButton,
        KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton, MessageBoxOptions options,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null,
        Image? overlayImage = null,
        OverlayImagePosition overlayImagePosition = OverlayImagePosition.BottomRight) =>
        ShowCoreAsync(null, text, caption, buttons, icon, defaultButton, options,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: displayHelpButton,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton,
            overlayImage: KryptonOverlayImage.FromImage(overlayImage, overlayImagePosition));

    /// <summary>
    /// Displays a message box asynchronously in front of the specified owner with the specified text.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard. If null (default), the button is not shown.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(IWin32Window owner, string text,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null) =>
        ShowCoreAsync(owner, text, string.Empty,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton);

    /// <summary>
    /// Displays a message box asynchronously in front of the specified owner with the specified text and caption.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard. If null (default), the button is not shown.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(IWin32Window owner, string? text, string? caption, bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null) =>
        ShowCoreAsync(owner, text, caption,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton);

    /// <summary>
    /// Displays a message box asynchronously in front of the specified owner with the specified text, caption and buttons.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="buttons">Which buttons to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard. If null (default), the button is not shown.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(IWin32Window owner, string? text, string? caption, KryptonMessageBoxButtons buttons,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null) =>
        ShowCoreAsync(owner, text, caption, buttons,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton);

    /// <summary>
    /// Displays a message box asynchronously in front of the specified owner with the specified text, caption, buttons and icon.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="buttons">Which buttons to display in the message box.</param>
    /// <param name="icon">Which icon to display in the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard. If null (default), the button is not shown.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(IWin32Window owner, string? text, string? caption, KryptonMessageBoxButtons buttons, KryptonMessageBoxIcon icon,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null) =>
        ShowCoreAsync(owner, text, caption, buttons, icon,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton);

    /// <summary>
    /// Displays a message box asynchronously in front of the specified owner with the specified text, caption, buttons, icon and default button.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="buttons">Which buttons to display in the message box.</param>
    /// <param name="icon">Which icon to display in the message box.</param>
    /// <param name="defaultButton">The default button for the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard. If null (default), the button is not shown.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(IWin32Window owner, string? text, string? caption, KryptonMessageBoxButtons buttons,
        KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null) =>
        ShowCoreAsync(owner, text, caption, buttons, icon, defaultButton,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton);

    /// <summary>
    /// Displays a message box asynchronously in front of the specified owner with the specified text, caption, buttons, icon, default button and options.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="buttons">Which buttons to display in the message box.</param>
    /// <param name="icon">Which icon to display in the message box.</param>
    /// <param name="defaultButton">The default button for the message box.</param>
    /// <param name="options">Display and association options for the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard. If null (default), the button is not shown.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(IWin32Window owner, string? text, string? caption, KryptonMessageBoxButtons buttons,
        KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton, MessageBoxOptions options,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null) =>
        ShowCoreAsync(owner, text, caption, buttons, icon, defaultButton, options,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: false,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton);

    /// <summary>
    /// Displays a message box asynchronously in front of the specified owner with the specified text, caption, buttons, icon, default button, options and help info.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="buttons">Which buttons to display in the message box.</param>
    /// <param name="icon">Which icon to display in the message box.</param>
    /// <param name="defaultButton">The default button for the message box.</param>
    /// <param name="options">Display and association options for the message box.</param>
    /// <param name="helpInfo">Contains the help data of the <see cref="KryptonMessageBox"/>.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard. If null (default), the button is not shown.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(IWin32Window owner, string? text, string? caption, KryptonMessageBoxButtons buttons,
        KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton, MessageBoxOptions options, HelpInfo helpInfo,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null) =>
        ShowCoreAsync(owner, text, caption, buttons, icon, defaultButton, options,
            showCtrlCopy: showCtrlCopy,
            helpInfo: helpInfo,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton);

    /// <summary>
    /// Displays a message box asynchronously in front of the specified owner with an optional Help button and icon overlay.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="buttons">Which buttons to display in the message box.</param>
    /// <param name="displayHelpButton">Displays a Help button.</param>
    /// <param name="icon">Which icon to display in the message box.</param>
    /// <param name="defaultButton">The default button for the message box.</param>
    /// <param name="options">Display and association options for the message box.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard. If null (default), the button is not shown.</param>
    /// <param name="overlayImage">Optional badge image drawn on top of the message icon.</param>
    /// <param name="overlayImagePosition">Corner placement for <paramref name="overlayImage"/>; defaults to bottom-right.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(IWin32Window owner, string? text, string? caption, KryptonMessageBoxButtons buttons, bool displayHelpButton,
        KryptonMessageBoxIcon icon, KryptonMessageBoxDefaultButton defaultButton, MessageBoxOptions options,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null,
        Image? overlayImage = null,
        OverlayImagePosition overlayImagePosition = OverlayImagePosition.BottomRight) =>
        ShowCoreAsync(owner, text, caption, buttons, icon, defaultButton, options,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: displayHelpButton,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton,
            overlayImage: KryptonOverlayImage.FromImage(overlayImage, overlayImagePosition));

    /// <summary>
    /// Displays a message box asynchronously with optional semantic button colours.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="buttons">Which buttons to display in the message box.</param>
    /// <param name="icon">Which icon to display in the message box.</param>
    /// <param name="buttonColors">Optional semantic accept/cancel/help button colours; null uses <see cref="KryptonManager.DialogButtonColors"/>.</param>
    /// <param name="defaultButton">The default button for the message box.</param>
    /// <param name="options">Display and association options for the message box.</param>
    /// <param name="displayHelpButton">Displays a Help button.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(string text, string caption, KryptonMessageBoxButtons buttons,
        KryptonMessageBoxIcon icon, KryptonDialogButtonColorOptions? buttonColors,
        KryptonMessageBoxDefaultButton defaultButton = KryptonMessageBoxDefaultButton.Button1,
        MessageBoxOptions options = 0,
        bool displayHelpButton = false,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null) =>
        ShowCoreAsync(null, text, caption, buttons, icon, defaultButton, options,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: displayHelpButton,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton,
            buttonColors: buttonColors);

    /// <summary>
    /// Displays a message box asynchronously in front of the specified owner with optional semantic button colours.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="buttons">Which buttons to display in the message box.</param>
    /// <param name="icon">Which icon to display in the message box.</param>
    /// <param name="buttonColors">Optional semantic accept/cancel/help button colours; null uses <see cref="KryptonManager.DialogButtonColors"/>.</param>
    /// <param name="defaultButton">The default button for the message box.</param>
    /// <param name="options">Display and association options for the message box.</param>
    /// <param name="displayHelpButton">Displays a Help button.</param>
    /// <param name="showCtrlCopy">Show extraText in title. If null (default) then only when Warning or Error icon is used.</param>
    /// <param name="showCloseButton">Displays the close button. If null (default), then the close button will be displayed.</param>
    /// <param name="showCopyButton">Displays a 'Copy' button that copies the message box contents to the clipboard.</param>
    /// <returns>A task that produces one of the <see cref="DialogResult"/> values when the message box is closed.</returns>
    public static Task<DialogResult> ShowAsync(IWin32Window? owner, string? text, string? caption, KryptonMessageBoxButtons buttons,
        KryptonMessageBoxIcon icon, KryptonDialogButtonColorOptions? buttonColors,
        KryptonMessageBoxDefaultButton defaultButton = KryptonMessageBoxDefaultButton.Button1,
        MessageBoxOptions options = 0,
        bool displayHelpButton = false,
        bool? showCtrlCopy = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null) =>
        ShowCoreAsync(owner, text, caption, buttons, icon, defaultButton, options,
            showCtrlCopy: showCtrlCopy,
            showHelpButton: displayHelpButton,
            showCloseButton: showCloseButton,
            showCopyButton: showCopyButton,
            buttonColors: buttonColors);

    #endregion

    #region Implementation

    /// <summary>
    /// Displays a message box asynchronously and disposes the underlying form when the dialog completes.
    /// </summary>
    private static async Task<DialogResult> ShowCoreAsync(IWin32Window? owner,
        string? text, string? caption,
        KryptonMessageBoxButtons buttons = KryptonMessageBoxButtons.OK,
        KryptonMessageBoxIcon icon = KryptonMessageBoxIcon.None,
        KryptonMessageBoxDefaultButton defaultButton = KryptonMessageBoxDefaultButton.Button1,
        MessageBoxOptions options = 0,
        HelpInfo? helpInfo = null,
        bool? showCtrlCopy = null,
        bool? showHelpButton = null,
        bool? showCloseButton = null,
        bool? showCopyButton = null,
        KryptonOverlayImage overlayImage = default,
        KryptonDialogButtonColorOptions? buttonColors = null)
    {
        caption = string.IsNullOrEmpty(caption) ? @" " : caption;

        IWin32Window? showOwner = ValidateOptions(owner, options, helpInfo);

        if (options is MessageBoxOptions.RightAlign or MessageBoxOptions.RtlReading)
        {
            using var kmbRtl = new VisualMessageBoxRtlAwareForm(showOwner, text, caption, buttons, icon,
                defaultButton, helpInfo, showCtrlCopy, showHelpButton, showCloseButton, showCopyButton, overlayImage,
                buttonColors);

            kmbRtl.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

            return await KryptonFormAsync.ShowDialogAsync(kmbRtl, showOwner).ConfigureAwait(false);
        }

        using var kmb = new VisualMessageBoxForm(showOwner, text, caption, buttons, icon,
            defaultButton, helpInfo, showCtrlCopy, showHelpButton, showCloseButton, showCopyButton, overlayImage,
            buttonColors);

        kmb.StartPosition = showOwner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;

        return await KryptonFormAsync.ShowDialogAsync(kmb, showOwner).ConfigureAwait(false);
    }

    #endregion
}
