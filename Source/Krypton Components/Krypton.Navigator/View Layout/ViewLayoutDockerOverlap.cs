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

namespace Krypton.Navigator;

/// <summary>
/// Override to draw tab items overlapping a group border and draw the selected tab item last.
/// </summary>
internal class ViewLayoutDockerOverlap : ViewLayoutDocker
{
    #region Instance Fields
    private readonly ViewDrawCanvas _drawCanvas;
    private readonly ViewLayoutInsetOverlap _layoutOverlap;
    private readonly ViewLayoutBarForTabs _layoutTabs;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutDockerOverlap class.
    /// </summary>
    /// <param name="drawCanvas">Canvas used to recover border width/rounding for overlapping.</param>
    /// <param name="layoutOverlap">Overlapping element.</param>
    /// <param name="layoutTabs">Tab item container element.</param>
    public ViewLayoutDockerOverlap([DisallowNull] ViewDrawCanvas drawCanvas,
        [DisallowNull] ViewLayoutInsetOverlap layoutOverlap,
        [DisallowNull] ViewLayoutBarForTabs layoutTabs)
    {
        Debug.Assert(drawCanvas is not null);
        Debug.Assert(layoutOverlap is not null);
        Debug.Assert(layoutTabs is not null);

        // Remember provided references
        _drawCanvas = drawCanvas ?? throw new ArgumentNullException(nameof(_drawCanvas));
        _layoutOverlap = layoutOverlap ?? throw new ArgumentNullException(nameof(_layoutOverlap));
        _layoutTabs = layoutTabs ?? throw new ArgumentNullException(nameof(_layoutTabs));
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutDockerOverlap:{Id}";

    #endregion

    #region BorderWidth
    /// <summary>
    /// Gets the rounding value to apply on the edges.
    /// </summary>
    public int BorderWidth => _drawCanvas.PaletteBorder!.GetBorderWidth(_drawCanvas.State);

    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering after child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderAfter(RenderContext context)
    {
        // Ask for another draw of the child but this time only drawing the selected tab
        _layoutTabs.DrawChecked = true;

        // Only render visible children that are inside the clipping rectangle
        if (_layoutOverlap.Visible && _layoutOverlap.ClientRectangle.IntersectsWith(context.ClipRect))
        {
            _layoutOverlap.Render(context);
        }

        _layoutTabs.DrawChecked = false;
    }
    #endregion

    #region Protected Virtual
    /// <summary>
    /// Allow the preferred size calculated by GetPreferredSize to be modified before use.
    /// </summary>
    /// <param name="preferredSize">Original preferred size value.</param>
    /// <returns>Modified size.</returns>
    protected override Size UpdatePreferredSize(Size preferredSize)
    {
        // Docking edge determines how to apply the overlapping
        switch (GetDock(_layoutOverlap))
        {
            case ViewDockStyle.Top:
            case ViewDockStyle.Bottom:
                preferredSize.Height += BorderWidth;
                break;
            case ViewDockStyle.Left:
            case ViewDockStyle.Right:
                preferredSize.Width += BorderWidth;
                break;
        }

        return preferredSize;
    }

    /// <summary>
    /// Allow the filler rectangle calculated by Layout to be modified before use.
    /// </summary>
    /// <param name="fillerRect">Original filler rectangle.</param>
    /// <param name="control">Owning control instance.</param>
    /// <returns>Modified rectangle.</returns>
    protected override Rectangle UpdateFillerRect(Rectangle fillerRect,
        Control control)
    {
        var borderWidth = BorderWidth;

        // Docking edge determines how to apply the overlapping
        switch (CalculateDock(GetDock(_layoutOverlap), control))
        {
            case ViewDockStyle.Top:
                fillerRect.Height += borderWidth;
                fillerRect.Y -= borderWidth;
                break;
            case ViewDockStyle.Bottom:
                fillerRect.Height += borderWidth;
                break;
            case ViewDockStyle.Left:
                fillerRect.Width += borderWidth;
                fillerRect.X -= borderWidth;
                break;
            case ViewDockStyle.Right:
                fillerRect.Width += borderWidth;
                break;
        }

        return fillerRect;
    }
    #endregion
}