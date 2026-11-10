#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

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
        e.TextColor = e.ToolStrip switch
        {
            MenuStrip _ => KCT.MenuStripText,
            StatusStrip _ => KCT.StatusStripText,
            ContextMenuStrip _ or ToolStripDropDown _ => KCT.MenuItemText,
            ToolStrip _ => KCT.ToolStripText,
            _ => e.TextColor
        };

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
        SyncToolStripFont(e.ToolStrip);

        switch (e.ToolStrip)
        {
            case StatusStrip _:
                if (TryRenderStatusStripOverride(e, e.Graphics))
                {
                    return;
                }
                break;
        }

        base.OnRenderToolStripBackground(e);
    }
    #endregion
}