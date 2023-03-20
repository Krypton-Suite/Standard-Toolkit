namespace Krypton.Toolkit;

/// <summary>
/// Access to drag the drop settings.
/// </summary>
public interface IPaletteDragDrop
{
    /// <summary>
    /// Gets the background color for a solid drag drop area.
    /// </summary>
    /// <returns>Color value.</returns>
    Color GetDragDropSolidBack();

    /// <summary>
    /// Gets the border color for a solid drag drop area.
    /// </summary>
    /// <returns>Color value.</returns>
    Color GetDragDropSolidBorder();

    /// <summary>
    /// Gets the opacity of the solid area.
    /// </summary>
    /// <returns>Opacity ranging from 0 to 1.</returns>
    float GetDragDropSolidOpacity();

    /// <summary>
    /// Gets the background color for the docking indicators area.
    /// </summary>
    /// <returns>Color value.</returns>
    Color GetDragDropDockBack();

    /// <summary>
    /// Gets the border color for the docking indicators area.
    /// </summary>
    /// <returns>Color value.</returns>
    Color GetDragDropDockBorder();

    /// <summary>
    /// Gets the active color for docking indicators.
    /// </summary>
    /// <returns>Color value.</returns>
    Color GetDragDropDockActive();

    /// <summary>
    /// Gets the inactive color for docking indicators.
    /// </summary>
    /// <returns>Color value.</returns>
    Color GetDragDropDockInactive();
}