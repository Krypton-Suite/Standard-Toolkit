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
/// Target the entire navigator client area.
/// </summary>
public class DragTargetNavigatorTransfer : DragTarget
{
    #region Instance Fields
    private KryptonNavigator? _navigator;
    private int _notDraggedPagesFromNavigator;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the DragTargetNavigatorTransfer class.
    /// </summary>
    /// <param name="rect">Rectangle for hot and draw areas.</param>
    /// <param name="navigator">Control instance for drop.</param>
    /// <param name="allowFlags">Only drop pages that have one of these flags defined.</param>
    public DragTargetNavigatorTransfer(Rectangle rect,
        KryptonNavigator? navigator,
        KryptonPageFlags allowFlags)
        : base(rect, rect, rect, DragTargetHint.Transfer, allowFlags)
    {
        _navigator = navigator;
        _notDraggedPagesFromNavigator = -1;
    }

    /// <summary>
    /// Release unmanaged and optionally managed resources.
    /// </summary>
    /// <param name="disposing">Called from Dispose method.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _navigator = null;
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Is this target a match for the provided screen position.
    /// </summary>
    /// <param name="screenPt">Position in screen coordinates.</param>
    /// <param name="dragEndData">Data to be dropped at destination.</param>
    /// <returns>True if a match; otherwise false.</returns>
    public override bool IsMatch(Point screenPt, PageDragEndData? dragEndData)
    {
        // First time around...
        if (_notDraggedPagesFromNavigator == -1)
        {
            // Search for any pages that are not from this navigator
            _notDraggedPagesFromNavigator = 0;
            if (dragEndData != null)
            {
                foreach (KryptonPage page in dragEndData.Pages)
                {
                    if (_navigator != null && !_navigator.Pages.Contains(page))
                    {
                        _notDraggedPagesFromNavigator = 1;
                        break;
                    }
                }
            }
        }

        // If 1 or more pages are not from this navigator then allow transfer into the target
        return _notDraggedPagesFromNavigator > 0 && base.IsMatch(screenPt, dragEndData);
    }

    /// <summary>
    /// Perform the drop action associated with the target.
    /// </summary>
    /// <param name="screenPt">Position in screen coordinates.</param>
    /// <param name="data">Data to pass to the target to process drop.</param>
    /// <returns>Drop was performed and the source can perform any removal of pages as required.</returns>
    public override bool PerformDrop(Point screenPt, PageDragEndData? data)
    {
        // Transfer the dragged pages into our navigator instance
        KryptonPage? page = ProcessDragEndData(_navigator, data);

        // Make the last page transfer the newly selected page of the navigator
        if (page != null)
        {
            // If the navigator is allowed to have a selected page then select it
            if (_navigator is { AllowTabSelect: true })
            {
                _navigator.SelectedPage = page;
            }

            // Need to layout so the new cell has been added as a child control and 
            // therefore can receive the focus we want to give it immediately afterwards
            if (_navigator != null)
            {
                _navigator.PerformLayout();

                if (!_navigator.IsDisposed)
                {
                    // Without this DoEvents() call the dropping of multiple pages in a complex arrangement causes an exception for
                    // a complex reason that is hard to work out (i.e. I'm not entirely sure). Something to do with using select to
                    // change activation is causing the source workspace control to dispose to earlier.
                    Application.DoEvents();
                    _navigator.Select();
                }
            }
        }

        return true;
    }
    #endregion
}