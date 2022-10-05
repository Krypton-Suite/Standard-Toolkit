﻿namespace Krypton.Toolkit
{
    /// <summary>
    /// Implementation for internal calendar buttons.
    /// </summary>
    public class ButtonSpecCalendar : ButtonSpec
    {
        #region Instance Fields

        private readonly RelativeEdgeAlign _edge;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecCalendar class.
        /// </summary>
        /// <param name="month">Reference to owning view.</param>
        /// <param name="fixedStyle">Fixed style to use.</param>
        /// <param name="edge">Alignment edge.</param>
        public ButtonSpecCalendar(ViewDrawMonth month,
                                  PaletteButtonSpecStyle fixedStyle,
                                  RelativeEdgeAlign edge)
        {
            // Remember back reference to owning navigator.
            _edge = edge;
            Enabled = true;
            Visible = true;

            // Fix the type
            ProtectedType = fixedStyle;
        }      
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the visible state.
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Gets and sets the enabled state.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Can a component be associated with the view.
        /// </summary>
        public override bool AllowComponent => false;

        /// <summary>
        /// Gets the button visible value.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button visibility.</returns>
        public override bool GetVisible(IPalette palette) => Visible;

        /// <summary>
        /// Gets the button enabled state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button enabled state.</returns>
        public override ButtonEnabled GetEnabled(IPalette palette) => Enabled ? ButtonEnabled.Container : ButtonEnabled.False;

        /// <summary>
        /// Gets the button checked state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button checked state.</returns>
        public override ButtonCheckState GetChecked(IPalette palette) => ButtonCheckState.Unchecked;

        /// <summary>
        /// Gets the button edge to position against.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Edge position.</returns>
        public override RelativeEdgeAlign GetEdge(IPalette palette) => _edge;

        #endregion
    }
}
