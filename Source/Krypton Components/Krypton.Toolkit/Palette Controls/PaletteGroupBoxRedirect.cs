﻿namespace Krypton.Toolkit
{
    /// <summary>
    /// Redirect storage for GroupBox states.
    /// </summary>
    public class PaletteGroupBoxRedirect : PaletteDoubleRedirect
    {
        #region Instance Fields

        private readonly PaletteContentInheritRedirect _contentInherit;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteGroupBoxRedirect class.
        /// </summary>
        /// <param name="redirect">inheritance redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteGroupBoxRedirect(PaletteRedirect redirect,
                                       NeedPaintHandler needPaint)
            : this(redirect, redirect, needPaint)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteGroupBoxRedirect class.
        /// </summary>
        /// <param name="redirectDouble">inheritance redirection for group border/background.</param>
        /// <param name="redirectContent">inheritance redirection for group header.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteGroupBoxRedirect(PaletteRedirect redirectDouble,
                                       PaletteRedirect redirectContent,
                                       NeedPaintHandler needPaint)
            : base(redirectDouble, PaletteBackStyle.ControlGroupBox, PaletteBorderStyle.ControlGroupBox, needPaint)
        {
            Debug.Assert(redirectDouble != null);
            Debug.Assert(redirectContent != null);

            _contentInherit = new PaletteContentInheritRedirect(redirectContent, PaletteContentStyle.LabelGroupBoxCaption);
            Content = new PaletteContent(_contentInherit, needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => base.IsDefault && Content.IsDefault;

        #endregion

        #region Content
        /// <summary>
        /// Gets access to the content palette details.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining content appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent Content { get; }

        private bool ShouldSerializeContent() => !Content.IsDefault;

        /// <summary>
        /// Gets the content palette.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IPaletteContent PaletteContent => Content;

        /// <summary>
        /// Gets and sets the content palette style.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PaletteContentStyle ContentStyle
        {
            get => _contentInherit.Style;
            set => _contentInherit.Style = value;
        }
        #endregion
    }
}
