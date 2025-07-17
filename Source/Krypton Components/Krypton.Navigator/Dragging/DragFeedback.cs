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
/// Base class for drag feedback implementations.
/// </summary>
public abstract class DragFeedback : IDisposable
{
    #region Identity
    /// <summary>
    /// Release resources.
    /// </summary>
    ~DragFeedback()
    {
        // Only dispose of resources once
        if (!IsDisposed)
        {
            // Only dispose of managed resources
            Dispose(false);
        }
    }

    /// <summary>
    /// Release managed and unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        // Only dispose of resources once
        if (!IsDisposed)
        {
            // Dispose of managed and unmanaged resources
            Dispose(true);
        }
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Release unmanaged and optionally managed resources.
    /// </summary>
    /// <param name="disposing">Called from Dispose method.</param>
    protected virtual void Dispose(bool disposing)
    {
        // If called from explicit call to Dispose
        if (disposing)
        {
            PageDragEndData = null;
            DragTargets = null;
        }

        // Mark as disposed
        IsDisposed = true;
    }

    /// <summary>
    /// Gets a value indicating if the view has been disposed.
    /// </summary>
    public bool IsDisposed { get; private set; }

    #endregion

    #region Public
    /// <summary>
    /// Called to initialize the implementation when dragging starts.
    /// </summary>
    /// <param name="paletteDragDrop">Drawing palette.</param>
    /// <param name="renderer">Drawing renderer.</param>
    /// <param name="pageDragEndData">Drag data associated with drag operation.</param>
    /// <param name="dragTargets">List of all drag targets.</param>
    public virtual void Start([DisallowNull] IPaletteDragDrop? paletteDragDrop,
        [DisallowNull] IRenderer? renderer,
        [DisallowNull] PageDragEndData? pageDragEndData,
        [DisallowNull] DragTargetList? dragTargets)
    {
        Debug.Assert(paletteDragDrop is not null);
        Debug.Assert(renderer is not null);
        Debug.Assert(pageDragEndData is not null);
        Debug.Assert(dragTargets is not null);

        PaletteDragDrop = paletteDragDrop ?? throw new ArgumentNullException(nameof(paletteDragDrop));
        Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        PageDragEndData = pageDragEndData ?? throw new ArgumentNullException(nameof(pageDragEndData));
        DragTargets = dragTargets ?? throw new ArgumentNullException(nameof(dragTargets));
    }

    /// <summary>
    /// Called to request feedback be shown for the specified target.
    /// </summary>
    /// <param name="screenPt">Current screen point of mouse.</param>
    /// <param name="target">Target that needs feedback.</param>
    /// <returns>Updated drag target.</returns>
    public abstract DragTarget? Feedback(Point screenPt, DragTarget? target);

    /// <summary>
    /// Called to cleanup when dragging has finished.
    /// </summary>
    public virtual void Quit()
    {
        PageDragEndData = null;
        DragTargets = null;
    }
    #endregion

    #region Protected
    /// <summary>
    /// Gets access to the cached drawing palette.
    /// </summary>
    protected IPaletteDragDrop PaletteDragDrop { get; private set; }

    /// <summary>
    /// Gets access to the cached drawing renderer.
    /// </summary>
    protected IRenderer Renderer { get; private set; }

    /// <summary>
    /// Gets access to the cached drag data.
    /// </summary>
    protected PageDragEndData? PageDragEndData { get; private set; }

    /// <summary>
    /// Gets access to the cached drag target list.
    /// </summary>
    protected DragTargetList? DragTargets { get; private set; }

    #endregion
}