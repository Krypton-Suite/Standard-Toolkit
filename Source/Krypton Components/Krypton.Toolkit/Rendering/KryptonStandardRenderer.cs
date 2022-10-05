﻿namespace Krypton.Toolkit
{
    /// <summary>
    /// 
    /// </summary>
    public class KryptonStandardRenderer : KryptonProfessionalRenderer
    {
        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonStandardRenderer class.
        /// </summary>
        /// <param name="kct">Source for text colors.</param>
        public KryptonStandardRenderer(KryptonColorTable kct)
            : base(kct)
        {
        }
        #endregion

        #region OnRenderItemText
        /// <summary>
        /// Raises the RenderItemText event. 
        /// </summary>
        /// <param name="e">A ToolStripItemTextRenderEventArgs that contains the event data.</param>
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            switch (e.ToolStrip)
            {
                case MenuStrip _:
                    e.TextColor = KCT.MenuStripText;
                    break;
                case StatusStrip _:
                    e.TextColor = KCT.StatusStripText;
                    break;
                case ContextMenuStrip _:
                case ToolStripDropDown _:
                    e.TextColor = KCT.MenuItemText;
                    break;
                case ToolStrip _:
                    e.TextColor = KCT.ToolStripText;
                    break;
            }

            base.OnRenderItemText(e);
        }
        #endregion

        #region OnRenderToolStripBackground
        /// <summary>
        /// Raises the RenderToolStripBackground event. 
        /// </summary>
        /// <param name="e">An ToolStripRenderEventArgs containing the event data.</param>
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            // Make sure the font is current
            switch (e.ToolStrip)
            {
                case MenuStrip _:
                case ContextMenuStrip _:
                case ToolStripDropDown _:
                    if (e.ToolStrip.Font != KCT.MenuStripFont)
                    {
                        e.ToolStrip.Font = KCT.MenuStripFont;
                    }
                    break;
                case StatusStrip _:
                    if (e.ToolStrip.Font != KCT.StatusStripFont)
                    {
                        e.ToolStrip.Font = KCT.StatusStripFont;
                    }
                    break;
                case ToolStrip _:
                    if (e.ToolStrip.Font != KCT.ToolStripFont)
                    {
                        e.ToolStrip.Font = KCT.ToolStripFont;
                    }
                    break;
            }

            base.OnRenderToolStripBackground(e);
        }
        #endregion
    }
}
