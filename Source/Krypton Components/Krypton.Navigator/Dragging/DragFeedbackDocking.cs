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
/// Provides drag feedback as a set of docking indicators.
/// </summary>
public class DragFeedbackDocking : DragFeedback
{
    #region Classes
    private class DockCluster : IDisposable
    {
        #region Type Definitons
        private class HintToTarget : Dictionary<DragTargetHint, DragTarget> { }
        #endregion

        #region Instance Fields
        private readonly IPaletteDragDrop _paletteDragDrop;
        private readonly IRenderer _renderer;
        private HintToTarget? _hintToTarget;
        private IDropDockingIndicator? _indicators;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DockCluster class.
        /// </summary>
        /// <param name="paletteDragDrop">Drawing palette.</param>
        /// <param name="renderer">Drawing renderer.</param>
        /// <param name="target">Initial target for the cluster.</param>
        public DockCluster(IPaletteDragDrop paletteDragDrop,
            IRenderer renderer,
            DragTarget target)
        {
            _paletteDragDrop = paletteDragDrop;
            _renderer = renderer;
            ScreenRect = target.ScreenRect;
            DrawRect = target.DrawRect;
            _hintToTarget = new HintToTarget
            {
                { target.Hint & DragTargetHint.ExcludeFlags, target }
            };
            ExcludeCluster = (target.Hint & DragTargetHint.ExcludeCluster) == DragTargetHint.ExcludeCluster;
        }

        /// <summary>
        /// Release resources.
        /// </summary>
        public void Dispose()
        {
            _hintToTarget?.Clear();
            _hintToTarget = null;

            if (_indicators != null)
            {
                var dispose = _indicators as IDisposable;
                dispose?.Dispose();
                _indicators = null;
            }
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the screen rectangle this cluster works for.
        /// </summary>
        public Rectangle ScreenRect { get; }

        /// <summary>
        /// Gets the drawing rectangle this cluster works for.
        /// </summary>
        public Rectangle DrawRect { get; private set; }

        /// <summary>
        /// Gets a value indicating if the cluster is exclusive to the current contents.
        /// </summary>
        public bool ExcludeCluster { get; }

        /// <summary>
        /// Add the new target to the cluster.
        /// </summary>
        /// <param name="target">Target to add into cluster.</param>
        public void Add(DragTarget target)
        {
            // Find the hint that excludes extra flags
            DragTargetHint hint = target.Hint & DragTargetHint.ExcludeFlags;

            // Can only add one of each hint value
            if (_hintToTarget != null && !_hintToTarget.ContainsKey(hint))
            {
                _hintToTarget.Add(hint, target);

                // Make sure the drawing rectangle encloses all targets
                DrawRect = Rectangle.Union(DrawRect, target.DrawRect);
            }
        }

        /// <summary>
        /// Update visual feedback based on the current screen position of the mouse.
        /// </summary>
        /// <param name="screenPt">Latest mouse screen position.</param>
        /// <param name="dragFeedback">Type of drag feedback required.</param>
        public DragTarget? Feedback(Point screenPt, PaletteDragFeedback dragFeedback)
        {
            if (ScreenRect.Contains(screenPt))
            {
                // Create the docking indicators the first time needed
                _indicators ??= dragFeedback switch
                {
                    PaletteDragFeedback.Rounded => new DropDockingIndicatorsRounded(_paletteDragDrop, _renderer,
                        _hintToTarget != null && _hintToTarget.ContainsKey(DragTargetHint.EdgeLeft),
                        _hintToTarget != null && _hintToTarget.ContainsKey(DragTargetHint.EdgeRight),
                        _hintToTarget != null && _hintToTarget.ContainsKey(DragTargetHint.EdgeTop),
                        _hintToTarget != null && _hintToTarget.ContainsKey(DragTargetHint.EdgeBottom),
                        _hintToTarget != null && _hintToTarget.ContainsKey(DragTargetHint.Transfer)),
                    _ => new DropDockingIndicatorsSquare(_paletteDragDrop, _renderer,
                        _hintToTarget != null && _hintToTarget.ContainsKey(DragTargetHint.EdgeLeft),
                        _hintToTarget != null && _hintToTarget.ContainsKey(DragTargetHint.EdgeRight),
                        _hintToTarget != null && _hintToTarget.ContainsKey(DragTargetHint.EdgeTop),
                        _hintToTarget != null && _hintToTarget.ContainsKey(DragTargetHint.EdgeBottom),
                        _hintToTarget != null && _hintToTarget.ContainsKey(DragTargetHint.Transfer))
                };

                // Ensure window is Displayed in correct location
                _indicators.ShowRelative(ScreenRect);

                // Hit test against indicators and update display
                return _indicators.ScreenMouseMove(screenPt) switch
                {
                    0x0040 => _hintToTarget![DragTargetHint.EdgeLeft],
                    0x0080 => _hintToTarget![DragTargetHint.EdgeRight],
                    0x0100 => _hintToTarget![DragTargetHint.EdgeTop],
                    0x0200 => _hintToTarget![DragTargetHint.EdgeBottom],
                    0x0400 => _hintToTarget![DragTargetHint.Transfer],
                    _ => null // Mouse is not over any of the targets
                };
            }
            else
            {
                if (_indicators != null)
                {
                    _indicators.MouseReset();
                    _indicators.Hide();
                }

                // Mouse is not over any of the targets
                return null;
            }
        }
        #endregion
    }
    #endregion

    #region Type Definitons
    private class DockClusterList : List<DockCluster> { }
    #endregion

    #region Instance Fields
    private DropSolidWindow? _solid;
    private readonly DockClusterList _clusters;
    private readonly PaletteDragFeedback _dragFeedback;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the DockCluster class.
    /// </summary>
    /// <param name="dragFeedback">Type of drag feedback required.</param>
    public DragFeedbackDocking(PaletteDragFeedback dragFeedback)
    {
        _clusters = new DockClusterList();
        _dragFeedback = dragFeedback;
    }

    /// <summary>
    /// Release unmanaged and optionally managed resources.
    /// </summary>
    /// <param name="disposing">Called from Dispose method.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_solid != null)
            {
                _solid.Dispose();
                _solid = null;
            }

            ClearClusters();
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Called to initialize the implementation when dragging starts.
    /// </summary>
    /// <param name="paletteDragDrop">Drawing palette.</param>
    /// <param name="renderer">Drawing renderer.</param>
    /// <param name="pageDragEndData">Drag data associated with drag operation.</param>
    /// <param name="dragTargets">List of all drag targets.</param>
    public override void Start(IPaletteDragDrop paletteDragDrop,
        IRenderer renderer,
        PageDragEndData? pageDragEndData,
        DragTargetList dragTargets)
    {
        if (pageDragEndData != null)
        {
            base.Start(paletteDragDrop, renderer, pageDragEndData, dragTargets);

            if (_solid == null)
            {
                // Create and show a solid feedback window without it taking focus
                _solid = new DropSolidWindow(PaletteDragDrop, Renderer);
                _solid.SetBounds(0, 0, 1, 1, BoundsSpecified.All);
                _solid.ShowWithoutActivate();
                _solid.Refresh();
            }

            ClearClusters();

            // Create clusters of related drag targets
            foreach (DragTarget target in dragTargets)
            {
                // Check if the target is actually able to drop inside itself
                if (target.IsMatch(target.HotRect.Location, pageDragEndData))
                {
                    // Find the existing cluster for the targets screen rectangle
                    DockCluster? cluster = FindTargetCluster(target);

                    // Is the target allowed to be added to the found cluster (if there is one found)
                    if ((cluster == null) || cluster.ExcludeCluster ||
                        ((target.Hint & DragTargetHint.ExcludeCluster) == DragTargetHint.ExcludeCluster))
                    {
                        _clusters.Add(new DockCluster(PaletteDragDrop, Renderer, target));
                    }
                    else
                    {
                        cluster.Add(target);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Called to request feedback be shown for the specified target.
    /// </summary>
    /// <param name="screenPt">Current screen point of mouse.</param>
    /// <param name="target">Target that needs feedback.</param>
    /// <returns>Updated drag target.</returns>
    public override DragTarget? Feedback(Point screenPt, DragTarget? target)
    {
        DragTarget? matchTarget = null;

        // Update each cluster so it shows/hides docking indicators based on mouse position
        foreach (DragTarget? clusterTarget in _clusters
                     .Select(cluster => cluster.Feedback(screenPt, _dragFeedback))
                     .Where(clusterTarget => (clusterTarget != null) && (matchTarget == null))
                )
        {
            // TODO: Should be a better way to select the last match for this ?!?
            matchTarget = clusterTarget;
        }

        // Update the solid feedback rectangle with area of the specific target
        if (_solid != null)
        {
            _solid.SolidRect = matchTarget?.DrawRect ?? Rectangle.Empty;
        }

        return matchTarget;
    }

    /// <summary>
    /// Called to cleanup when dragging has finished.
    /// </summary>
    public override void Quit()
    {
        if (_solid != null)
        {
            _solid.Dispose();
            _solid = null;
        }

        ClearClusters();

        base.Quit();
    }
    #endregion

    #region Implementation
    private void ClearClusters()
    {
        // Must dispose each cluster as they contain unmanaged resources
        foreach (DockCluster cluster in _clusters)
        {
            cluster.Dispose();
        }

        _clusters.Clear();
    }

    private DockCluster? FindTargetCluster(DragTarget target) => _clusters.FirstOrDefault(cluster => !cluster.ExcludeCluster && cluster != null && cluster.ScreenRect.Equals(target.ScreenRect));

    private DragTarget? FindTarget(Point screenPt, PageDragEndData dragEndData) =>
        // Nothing matches
        null;

    #endregion
}