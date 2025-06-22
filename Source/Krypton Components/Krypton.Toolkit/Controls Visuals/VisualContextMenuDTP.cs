#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Specialized version of the visual context menu that knows about the KryptonDateTimePicker drop-down button.
/// </summary>
public class VisualContextMenuDTP : VisualContextMenu
{
    #region Instance Fields
    private readonly Rectangle _dropScreenRect;
    #endregion

    #region Identity
    /// <summary>
    ///  Initialize a new instance of the VisualContextMenuDTP class.
    /// </summary>
    /// <param name="contextMenu">Originating context menu instance.</param>
    /// <param name="palette">Local palette setting to use initially.</param>
    /// <param name="paletteMode">Palette mode setting to use initially.</param>
    /// <param name="redirector">Redirector used for obtaining palette values.</param>
    /// <param name="redirectorImages">Redirector used for obtaining images.</param>
    /// <param name="items">Collection of context menu items to be displayed.</param>
    /// <param name="enabled">Enabled state of the context menu.</param>
    /// <param name="keyboardActivated">Was the context menu activate by a keyboard action.</param>
    /// <param name="dropScreenRect">Screen rectangle of the drop-down button.</param>
    public VisualContextMenuDTP(KryptonContextMenu contextMenu,
        PaletteBase? palette,
        PaletteMode paletteMode,
        PaletteRedirect redirector,
        PaletteRedirectContextMenu redirectorImages,
        KryptonContextMenuCollection items,
        bool enabled,
        bool keyboardActivated,
        Rectangle dropScreenRect)
        : base(contextMenu, palette, paletteMode, redirector, redirectorImages,
            items, enabled, keyboardActivated) =>
        _dropScreenRect = dropScreenRect;

    #endregion

    #region Public
    /// <summary>
    /// Should the mouse down be eaten when the tracking has been ended.
    /// </summary>
    /// <param name="m">Original message.</param>
    /// <param name="pt">Screen coordinates point.</param>
    /// <returns>True to eat message; otherwise false.</returns>
    public override bool DoesMouseDownGetEaten(Message m, Point pt) =>
        // If the user dismissed the context menu by clicking down on the drop-down button of
        // the KryptonDateTimePicker then eat the down message to prevent the down press from
        // opening the menu again.
        _dropScreenRect.Contains(new Point(pt.X, pt.Y));

    #endregion
}