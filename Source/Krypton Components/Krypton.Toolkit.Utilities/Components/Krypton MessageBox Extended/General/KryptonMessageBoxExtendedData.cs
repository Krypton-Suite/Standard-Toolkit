#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using ContentAlignment = System.Drawing.ContentAlignment;

namespace Krypton.Toolkit.Utilities;

public struct KryptonMessageBoxExtendedData
{
    #region Public

    #region Base

    /// <summary>Gets or sets the owner window.</summary>
    /// <value>The owner window.</value>
    public IWin32Window? Owner { get; set; }

    /// <summary>Gets or sets the message text.</summary>
    /// <value>The message text.</value>
    public string? MessageText { get; set; }

    /// <summary>Gets or sets the window caption.</summary>
    /// <value>The window caption.</value>
    public string Caption { get; set; }

    /// <summary>Gets or sets the buttons.</summary>
    /// <value>The buttons.</value>
    public ExtendedMessageBoxButtons Buttons { get; set; }

    /// <summary>Gets or sets the icon.</summary>
    /// <value>The icon.</value>
    public ExtendedKryptonMessageBoxIcon Icon { get; set; }

    /// <summary>Gets or sets the default button.</summary>
    /// <value>The default button.</value>
    public KryptonMessageBoxDefaultButton? DefaultButton { get; set; }

    /// <summary>Gets or sets the <see cref="MessageBoxOptions"/>.</summary>
    /// <value>The <see cref="MessageBoxOptions"/>.</value>
    public MessageBoxOptions Options { get; set; }

    /// <summary>Gets or sets the help information.</summary>
    /// <value>The help information.</value>
    public HelpInfo? HelpInfo { get; set; }

    /// <summary>Gets or sets the show control copy.</summary>
    /// <value>The show control copy.</value>
    public bool? ShowCtrlCopy { get; set; }

    /// <summary>Gets or sets a value indicating whether an optional Copy button is shown that copies the message box contents to the clipboard.</summary>
    /// <value>The show copy button.</value>
    public bool? ShowCopyButton { get; set; }

    /// <summary>Gets or sets the show help button.</summary>
    /// <value>The show help button.</value>
    public bool? ShowHelpButton { get; set; }

    /// <summary>Gets or sets the show action button.</summary>
    /// <value>The show action button.</value>
    public bool? ShowActionButton { get; set; }

    /// <summary>Gets or sets the action button text.</summary>
    /// <value>The action button text.</value>
    public string? ActionButtonText { get; set; }

    /// <summary>Gets or sets the action button command.</summary>
    /// <value>The action button command.</value>
    public KryptonCommand? ActionButtonCommand { get; set; }

    /// <summary>Gets or sets the application image.</summary>
    /// <value>The application image.</value>
    [Editor(typeof(KryptonDesignerImageEditor), typeof(UITypeEditor))]
    public Image? ApplicationImage { get; set; }

    /// <summary>Gets or sets the application path.</summary>
    /// <value>The application path.</value>
    public string? ApplicationPath { get; set; }

    /// <summary>
    /// Gets or sets an optional overlay (badge) image drawn on top of the main message icon.
    /// When <see cref="KryptonOverlayImage.Image"/> is null, no overlay is applied.
    /// </summary>
    /// <value>The overlay image settings.</value>
    public KryptonOverlayImage OverlayImage { get; set; }

    /// <summary>Gets or sets the type of the message content area.</summary>
    /// <value>The type of the message content area.</value>
    public ExtendedKryptonMessageBoxMessageContainerType? MessageContentAreaType { get; set; }

    /// <summary>Gets or sets the link label command.</summary>
    /// <value>The link label command.</value>
    public KryptonCommand? LinkLabelCommand { get; set; }

    /// <summary>Gets or sets the link launch argument.</summary>
    /// <value>The link launch argument.</value>
    public ProcessStartInfo? LinkLaunchArgument { get; set; }

    /// <summary>Gets or sets the content link area.</summary>
    /// <value>The content link area.</value>
    public LinkArea? ContentLinkArea { get; set; }

    /// <summary>Gets or sets the message text alignment.</summary>
    /// <value>The message text alignment.</value>
    public ContentAlignment? MessageTextAlignment { get; set; }

    /// <summary>Gets or sets the message text box alignment.</summary>
    /// <value>The message text box alignment.</value>
    public HorizontalAlignment? MessageTextBoxAlignment { get; set; }

    /// <summary>Gets or sets the force use of operating system icons.</summary>
    /// <value>Forces the use of operating system icons.</value>
    public bool? ForceUseOfOperatingSystemIcons { get; set; }

    /// <summary>Gets or sets the help file path for <see cref="HelpInfo"/>.</summary>
    /// <value>The help file path.</value>
    public string? HelpFilePath { get; set; }

    /// <summary>Gets or sets the help navigator for <see cref="HelpInfo"/>.</summary>
    /// <value>The help navigator.</value>
    public HelpNavigator? HelpNavigator { get; set; }

    /// <summary>Gets or sets the help parameters for <see cref="HelpInfo"/>.</summary>
    /// <value>The help parameters.</value>
    public object? HelpParameters { get; set; }

    /// <summary>Gets or sets the CheckBox text.</summary>
    /// <value>The CheckBox text.</value>
    public string? CheckBoxText { get; set; }

    /// <summary>Gets or sets the CheckBox checked value.</summary>
    /// <value>The CheckBox checked value.</value>
    public bool? IsCheckBoxChecked { get; set; }

    /// <summary>Gets or sets the state of the CheckBox <see cref="CheckState"/>.</summary>
    /// <value>The state of the CheckBox <see cref="CheckState"/>.</value>
    public CheckState? CheckBoxCheckState { get; set; }

    /// <summary>Gets or sets the state of the use CheckBox three.</summary>
    /// <value>The state of the use CheckBox three.</value>
    public bool? UseCheckBoxThreeState { get; set; }

    /// <summary>Gets or sets the show close button.</summary>
    /// <value>The show close button.</value>
    public bool? ShowCloseButton { get; set; }

    #endregion

    #region Extended

    /// <summary>
    /// Gets or sets a value indicating whether the collapsible details expander is shown.
    /// </summary>
    /// <remarks>
    /// A non-empty <see cref="DetailsText"/> or <see cref="MoreDetailsMessageText"/> also shows the expander
    /// (same rule as <see cref="KryptonFoldableDialogData.DetailsText"/>). Use this flag with
    /// <see cref="FooterContentType"/> of <see cref="ExtendedKryptonMessageBoxFooterContentType.CheckBox"/>
    /// when there is no details text.
    /// </remarks>
    public bool ShowMoreDetailsOption { get; set; }

    /// <summary>Gets or sets a value indicating whether the "more details" footer starts expanded.</summary>
    /// <value><c>true</c> to show the details region expanded on open; otherwise, <c>false</c> (collapsed).</value>
    public bool MoreDetailsExpanded { get; set; }

    /// <summary>
    /// Gets or sets whether the details region starts expanded.
    /// </summary>
    /// <remarks>FoldableDialog-style alias for <see cref="MoreDetailsExpanded"/>.</remarks>
    public bool Expanded
    {
        get => MoreDetailsExpanded;
        set => MoreDetailsExpanded = value;
    }

    public PaletteRelativeAlign? RichTextBoxTextAlignment { get; set; }

    /// <summary>
    /// Gets or sets a single toggle caption used for both expanded and collapsed states.
    /// </summary>
    /// <remarks>
    /// Prefer <see cref="ExpandButtonText"/> / <see cref="CollapseButtonText"/> to match
    /// <see cref="KryptonFoldableDialog"/>. When those are empty, this value is used for both states;
    /// otherwise the localizable <see cref="KryptonFoldableDialogStrings"/> defaults apply.
    /// </remarks>
    public string? MoreDetailsButtonText { get; set; }

    /// <summary>
    /// Gets or sets the expander caption while the details region is expanded (typically "Hide Details").
    /// </summary>
    public string? ExpandButtonText { get; set; }

    /// <summary>
    /// Gets or sets the expander caption while the details region is collapsed (typically "Show Details").
    /// </summary>
    public string? CollapseButtonText { get; set; }

    /// <summary>Gets or sets the text shown inside the collapsible details region.</summary>
    public string? MoreDetailsMessageText { get; set; }

    /// <summary>
    /// Gets or sets the text shown inside the collapsible details region.
    /// </summary>
    /// <remarks>
    /// FoldableDialog-style alias for <see cref="MoreDetailsMessageText"/>. Setting a non-empty value
    /// shows the expander without also setting <see cref="ShowMoreDetailsOption"/>.
    /// </remarks>
    public string? DetailsText
    {
        get => MoreDetailsMessageText;
        set
        {
            MoreDetailsMessageText = value;
            if (!string.IsNullOrEmpty(value))
            {
                ShowMoreDetailsOption = true;
            }
        }
    }

    /// <summary>
    /// Gets or sets the details content type. When <see langword="null"/>, a non-empty details string uses
    /// <see cref="ExtendedKryptonMessageBoxFooterContentType.RichTextBox"/> (FoldableDialog-style).
    /// </summary>
    public ExtendedKryptonMessageBoxFooterContentType? FooterContentType { get; set; }

    /// <summary>
    /// Gets or sets the details RichTextBox height. When <see langword="null"/>, the FoldableDialog default (180) is used.
    /// </summary>
    public int? FooterRichTextBoxHeight { get; set; }

    /// <summary>
    /// Gets or sets optional semantic colours for the message-box action buttons.
    /// </summary>
    /// <remarks>
    /// When null, <see cref="KryptonManager.DialogButtonColors"/> is used. When both are null,
    /// buttons keep themed Standalone chrome.
    /// </remarks>
    public KryptonDialogButtonColorOptions? ButtonColors { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the message box fades in on show and fades out on close.
    /// </summary>
    /// <value><c>true</c> to animate opacity; otherwise, <c>false</c>. Default is <c>false</c>.</value>
    public bool UseFade { get; set; }

    /// <summary>
    /// Gets or sets the fade speed preset used when <see cref="UseFade"/> is <c>true</c>.
    /// </summary>
    /// <value>A <see cref="FadeSpeedChoice"/> value. Default is <see cref="FadeSpeedChoice.Normal"/>.</value>
    public FadeSpeedChoice FadeSpeed { get; set; }

    /// <summary>
    /// Gets or sets a custom fade step used when <see cref="FadeSpeed"/> is <see cref="FadeSpeedChoice.Custom"/>.
    /// </summary>
    /// <value>Opacity step scale matching other Krypton fade APIs; ignored for named speed presets.</value>
    public float? CustomFadeSpeed { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether remaining time is shown in the caption.
    /// </summary>
    /// <value><c>true</c> to display a caption countdown; otherwise, <c>false</c>. Default is <c>false</c>.</value>
    /// <remarks>
    /// When <c>true</c> and <see cref="AutoClose"/> is <see langword="null"/>, the dialog also auto-closes at zero.
    /// </remarks>
    public bool UseTimeOut { get; set; }

    /// <summary>
    /// Gets or sets the timeout duration in seconds.
    /// </summary>
    /// <value>Seconds until the countdown completes. Default is 60. Values of 0 or less are treated as 60.</value>
    public int TimeOut { get; set; }

    /// <summary>
    /// Gets or sets the timeout timer interval in milliseconds.
    /// </summary>
    /// <value>Tick interval for the caption countdown. Default is 1000. Values of 0 or less are treated as 1000.</value>
    public int TimeOutInterval { get; set; }

    /// <summary>
    /// Gets or sets whether the message box closes when the timeout reaches zero.
    /// </summary>
    /// <value>
    /// <see langword="null"/> to auto-close when <see cref="UseTimeOut"/> is <c>true</c>;
    /// <c>true</c> to dismiss even without a caption countdown;
    /// <c>false</c> for a display-only countdown.
    /// </value>
    public bool? AutoClose { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DialogResult"/> returned when auto-close uses <see cref="ExtendedMessageBoxTimeoutAction.Close"/>.
    /// </summary>
    /// <value>
    /// The result to return. When <see cref="DialogResult.None"/>, the default button result (typically OK) is used
    /// so a timed-out modal does not return <see cref="DialogResult.None"/>.
    /// </value>
    public DialogResult TimeOutResult { get; set; }

    /// <summary>
    /// Gets or sets the action taken when auto-close fires.
    /// </summary>
    /// <value>
    /// <see cref="ExtendedMessageBoxTimeoutAction.Close"/> (default) closes with <see cref="TimeOutResult"/>;
    /// <see cref="ExtendedMessageBoxTimeoutAction.ButtonOne"/> through <see cref="ExtendedMessageBoxTimeoutAction.ButtonFour"/>
    /// click that button instead.
    /// </value>
    public ExtendedMessageBoxTimeoutAction TimeOutAction { get; set; }

    #endregion

    #endregion

    #region Identity

    public KryptonMessageBoxExtendedData()
    {
        TimeOut = 60;
        TimeOutInterval = 1000;
        FadeSpeed = FadeSpeedChoice.Normal;
        TimeOutAction = ExtendedMessageBoxTimeoutAction.Close;
    }

    #endregion
}
