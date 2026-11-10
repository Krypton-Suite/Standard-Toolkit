namespace Krypton.Toolkit
{
    /// <summary>
    /// Opt-in hover preview that does not change the committed colour until the user selects one.
    /// </summary>
    /// <remarks>
    /// Colour controls raise <see cref="ColorPreview"/>. Hosts apply <see cref="KryptonColorLivePreviewEventArgs.Color"/>
    /// while the action is <see cref="KryptonColorLivePreviewAction.Preview"/> and restore
    /// <see cref="KryptonColorLivePreviewEventArgs.CommittedColor"/> on
    /// <see cref="KryptonColorLivePreviewAction.Revert"/> or <see cref="KryptonColorLivePreviewAction.Cancel"/>.
    /// Future colour controls drive the same sequence through <see cref="KryptonColorLivePreviewSession"/>.
    /// </remarks>
    public interface IKryptonColorLivePreview
    {
        /// <summary>
        /// Gets or sets a value indicating whether highlighting a swatch previews that colour before it is committed.
        /// </summary>
        bool LivePreview { get; set; }

        /// <summary>
        /// Gets a value indicating whether a swatch colour is currently shown in place of the committed colour.
        /// </summary>
        bool IsColorPreviewActive { get; }

        /// <summary>
        /// Gets the colour to paint: the highlighted swatch while <see cref="IsColorPreviewActive"/> is true, otherwise the committed colour.
        /// </summary>
        Color DisplayColor { get; }

        /// <summary>
        /// Occurs when the live preview starts, updates, commits, or cancels.
        /// </summary>
        event EventHandler<KryptonColorLivePreviewEventArgs>? ColorPreview;
    }
}