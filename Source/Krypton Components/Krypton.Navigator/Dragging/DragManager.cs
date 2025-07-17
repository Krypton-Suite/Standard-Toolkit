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
/// Specialise the generic collection with type specific rules for item accessor.
/// </summary>
public class DragTargetProviderCollection : TypedCollection<IDragTargetProvider> { }

/// <summary>
/// Manage a dragging operation.
/// </summary>
public class DragManager : IDragPageNotify,
    IDisposable
{
    #region Static Fields
    private static readonly Cursor _validCursor;
    private static readonly Cursor _invalidCursor;
    #endregion

    #region Instance Fields
    private PaletteBase _dragPalette;
    private PaletteBase _localPalette;
    private IRenderer _dragRenderer;
    private PaletteMode _paletteMode;
    private readonly PaletteRedirect _redirector;
    private PageDragEndData? _pageDragEndData;
    private DragFeedback? _dragFeedback;
    private readonly DragTargetList? _dragTargets;
    private DragTarget? _currentTarget;
    private bool _documentCursor;

    #endregion

    #region Identity
    /// <summary>
    /// Initializes a static fields of the TargetManager class.
    /// </summary>
    static DragManager()
    {
        using (var ms = new MemoryStream(CursorResources.DocumentValid))
        {
            _validCursor = new Cursor(ms);
        }

        using (var ms = new MemoryStream(CursorResources.DocumentInvalid))
        {
            _invalidCursor = new Cursor(ms);
        }
    }

    /// <summary>
    /// Initialize a new instance of the DragManager class.
    /// </summary>
    public DragManager(PaletteBase? target = null)
    {
        _redirector = new PaletteRedirect(target);
        StateCommon = new PaletteDragDrop(target!, null);
        _paletteMode = PaletteMode.Global;
        DragTargetProviders = new DragTargetProviderCollection();
        _dragTargets = new DragTargetList();
        _documentCursor = false;
    }

    /// <summary>
    /// Release resources.
    /// </summary>
    ~DragManager()
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
            ClearDragFeedback();
        }

        ClearTargets();

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
    /// Gets access to the common navigator appearance entries.
    /// </summary>
    public PaletteDragDrop StateCommon { get; }

    /// <summary>
    /// Gets or sets the palette to be applied.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _paletteMode;

        set
        {
            if (_paletteMode != value)
            {
                switch (value)
                {
                    case PaletteMode.Custom:
                        // Do nothing, you must assign a palette to the 
                        // 'Palette' property in order to get the custom mode
                        break;
                    default:
                        _paletteMode = value;
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Gets and sets the custom palette implementation.
    /// </summary>
    public PaletteBase Palette
    {
        get => _localPalette;

        set
        {
            if (_localPalette != value)
            {
                _localPalette = value;
                _paletteMode = _localPalette == null ? PaletteMode.Global : PaletteMode.Custom;
            }
        }
    }

    /// <summary>
    /// Gets access to the collection of target providers.
    /// </summary>
    public DragTargetProviderCollection DragTargetProviders { get; }

    /// <summary>
    /// Gets a value indicating if dragging is currently occurring.
    /// </summary>
    public bool IsDragging { get; private set; }

    /// <summary>
    /// Gets and sets a value indicating if document cursors should be used during dragging.
    /// </summary>
    public bool DocumentCursor
    {
        get => _documentCursor;

        set
        {
            if (IsDragging)
            {
                throw new InvalidOperationException("Cannot update DocumentCursor property during dragging operation.");
            }
            else
            {
                _documentCursor = value;
            }
        }
    }

    /// <summary>
    /// Occurs when dragging starts.
    /// </summary>
    /// <param name="screenPt">Mouse screen point at start of drag.</param>
    /// <param name="dragEndData">Data to be dropped at destination.</param>
    /// <returns>True if dragging was started; otherwise false.</returns>
    public virtual bool DragStart(Point screenPt, PageDragEndData? dragEndData)
    {
        if (IsDisposed)
        {
            throw new InvalidOperationException("Cannot DragStart when instance have been disposed.");
        }

        if (IsDragging)
        {
            throw new InvalidOperationException("Cannot DragStart when already performing dragging operation.");
        }

        if (dragEndData == null)
        {
            throw new ArgumentNullException(nameof(dragEndData), @"Cannot provide a null DragEndData.");
        }

        // Generate drag targets from the set of target provides
        ClearTargets();
        foreach (IDragTargetProvider provider in DragTargetProviders)
        {
            _dragTargets?.AddRange(provider, dragEndData);
        }

        // We only drag if we have at least one page and one target
        IsDragging = ((_dragTargets?.Count > 0) && (dragEndData.Pages.Count > 0));

        // Do we really need to start dragging?
        if (IsDragging)
        {
            // We cache the palette/renderer at start of drag and use the same ones for the
            // whole duration of the drag as changing drawing info during drag would be hard!
            ResolvePaletteRenderer();

            // Cache page data for duration of dragging operation
            _pageDragEndData = dragEndData;

            // Create correct drag feedback class and start it up
            ResolveDragFeedback();
            _dragFeedback?.Start(StateCommon, _dragRenderer, _pageDragEndData, _dragTargets!);
        }
        else
        {
            ClearTargets();
        }

        return IsDragging;
    }

    /// <summary>
    /// Occurs on dragging movement.
    /// </summary>
    /// <param name="screenPt">Latest screen point during dragging.</param>
    public virtual void DragMove(Point screenPt)
    {
        if (IsDisposed)
        {
            throw new InvalidOperationException("Cannot DragMove when instance have been disposed.");
        }

        if (!IsDragging)
        {
            throw new InvalidOperationException("Cannot DragMove when DragStart has not been called.");
        }

        // Different feedback objects implement visual feedback differently and so only the feedback
        // instance knows the correct target to use for the given screen point and drag data.
        _currentTarget = _dragFeedback?.Feedback(screenPt, _currentTarget);

        // Check if we need a cursor to indicate the drag state
        UpdateCursor();
    }

    /// <summary>
    /// Occurs when dragging ends because of dropping.
    /// </summary>
    /// <param name="screenPt">Ending screen point when dropping.</param>
    /// <returns>Drop was performed and the source can perform any removal of pages as required.</returns>
    public virtual bool DragEnd(Point screenPt)
    {
        if (IsDisposed)
        {
            throw new InvalidOperationException("Cannot DragEnd when instance have been disposed.");
        }

        if (!IsDragging)
        {
            throw new InvalidOperationException("Cannot DragEnd when DragStart has not been called.");
        }

        // Different feedback objects implement visual feedback differently and so only the feedback
        // instance knows the correct target to use for the given screen point and drag data.
        _currentTarget = _dragFeedback?.Feedback(screenPt, _currentTarget);

        // Remove visual feedback
        _dragFeedback?.Quit();

        // Inform target it needs to perform the drop action
        var ret = false;
        if (_currentTarget != null)
        {
            ret = _currentTarget.PerformDrop(screenPt, _pageDragEndData);
        }

        ClearTargets();
        RestoreCursor();
        EndDragging();

        return ret;
    }

    /// <summary>
    /// Occurs when dragging quits.
    /// </summary>
    public virtual void DragQuit()
    {
        if (IsDisposed)
        {
            throw new InvalidOperationException("Cannot DragQuit when instance have been disposed.");
        }

        if (!IsDragging)
        {
            throw new InvalidOperationException("Cannot DragQuit when DragStart has not been called.");
        }

        // Remove visual feedback
        _dragFeedback?.Quit();

        ClearTargets();
        RestoreCursor();
        EndDragging();
    }

    /// <summary>
    /// Occurs when a page drag is about to begin and allows it to be cancelled.
    /// </summary>
    /// <param name="sender">Source of the page drag; can be null.</param>
    /// <param name="navigator">Navigator instance associated with source; can be null.</param>
    /// <param name="e">Event arguments indicating list of pages being dragged.</param>
    public virtual void PageDragStart(object sender, KryptonNavigator? navigator, PageDragCancelEventArgs e) => e.Cancel = !DragStart(e.ScreenPoint, new PageDragEndData(sender, navigator, e.Pages));

    /// <summary>
    /// Occurs when the mouse moves during the drag operation.
    /// </summary>
    /// <param name="sender">Source of the page drag; can be null.</param>
    /// <param name="e">Event arguments containing the new screen point of the mouse.</param>
    public virtual void PageDragMove(object sender, PointEventArgs e) => DragMove(e.Point);

    /// <summary>
    /// Occurs when drag operation completes with pages being dropped.
    /// </summary>
    /// <param name="sender">Source of the page drag; can be null.</param>
    /// <param name="e">Event arguments containing the new screen point of the mouse.</param>
    /// <returns>Drop was performed and the source can perform any removal of pages as required.</returns>
    public virtual bool PageDragEnd(object sender, PointEventArgs e) => DragEnd(e.Point);

    /// <summary>
    /// Occurs when dragging pages has been cancelled.
    /// </summary>
    /// <param name="sender">Source of the page drag; can be null.</param>
    public virtual void PageDragQuit(object sender) => DragQuit();
    #endregion

    #region Protected
    /// <summary>
    /// Create the actual drop data based on the proposed data provided.
    /// </summary>
    /// <param name="dropData">Proposed drop data.</param>
    /// <returns>Actual drop data</returns>
    protected virtual PageDragEndData CreateDropData(PageDragEndData dropData) => dropData;

    /// <summary>
    /// Update the Displayed cursor to reflect the current dragging state.
    /// </summary>
    protected virtual void UpdateCursor()
    {
        // Should we update cursor to reflect document dragging?
        if (IsDragging && DocumentCursor)
        {
            if (_pageDragEndData?.Navigator != null)
            {
                _pageDragEndData.Navigator.Cursor = _currentTarget == null ? _invalidCursor : _validCursor;
            }
        }
    }

    /// <summary>
    /// Restore the Displayed cursor back to null.
    /// </summary>
    protected virtual void RestoreCursor()
    {
        if (IsDragging)
        {
            if (_pageDragEndData?.Navigator != null)
            {
                _pageDragEndData.Navigator.Cursor = null;
            }
        }
    }
    #endregion

    #region Implementation
    private void ResolvePaletteRenderer()
    {
        // Resolve the correct palette instance to use
        _dragPalette = _paletteMode switch
        {
            PaletteMode.Custom => _localPalette,
            _ => KryptonManager.GetPaletteForMode(_paletteMode)
        };

        // Update redirector to point at the resolved palette
        _redirector.Target = _dragPalette;

        // Inherit the state common values from resolved palette
        StateCommon.SetInherit(_dragPalette);

        // Get the renderer associated with the palette
        _dragRenderer = _dragPalette?.GetRenderer()!;
    }

    private void ResolveDragFeedback()
    {
        ClearDragFeedback();

        // Start with the provided value
        PaletteDragFeedback dragFeedback = StateCommon.GetDragDropFeedback();

        // Should never be 'inherit'
        if (dragFeedback == PaletteDragFeedback.Inherit)
        {
            dragFeedback = PaletteDragFeedback.Rounded;
        }

        // Check if the rounded style is possible
        if (dragFeedback == PaletteDragFeedback.Rounded)
        {
            // Rounded feedback uses a per-pixel alpha blending and so we need to be on a machine that supports
            // more than 256 colors and also allows the layered windows feature. If not then revert to squares
            if ((OSFeature.Feature.GetVersionPresent(OSFeature.LayeredWindows) == null) || (CommonHelper.ColorDepth() <= 8))
            {
                dragFeedback = PaletteDragFeedback.Square;
            }
        }

        _dragFeedback = dragFeedback switch
        {
            PaletteDragFeedback.Rounded or PaletteDragFeedback.Square => new DragFeedbackDocking(dragFeedback),
            _ => new DragFeedbackSolid()
        };
    }

    private void ClearDragFeedback()
    {
        if (_dragFeedback != null)
        {
            _dragFeedback.Dispose();
            _dragFeedback = null;
        }
    }

    private void ClearTargets()
    {
        if (_dragTargets != null)
        {
            // Dispose the targets to ensure references are removed to prevent memory leaks
            foreach (DragTarget target in _dragTargets)
            {
                target.Dispose();
            }

            _dragTargets.Clear();
        }

        _currentTarget = null;
    }

    private void EndDragging()
    {
        _dragPalette = null!;
        _dragRenderer = null!;
        _pageDragEndData = null;
        IsDragging = false;
    }
    #endregion
}