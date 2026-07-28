#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Caption-tab drag implementation that reuses <see cref="DragManager"/> for cross-navigator
/// transfers and creates a new host window when dropped outside any registered target.
/// </summary>
internal sealed class NavigatorCaptionDragPageNotify : IDragPageNotify, IDisposable
{
    #region Instance Fields

    private readonly KryptonNavigatorFormIntegrator _owner;
    private readonly DragManager _dragManager;
    private KryptonPageCollection? _draggingPages;
    private KryptonNavigator? _sourceNavigator;
    private KryptonForm? _sourceForm;
    private TearOutFeedbackWindow? _tearOutFeedback;
    private bool _disposed;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="NavigatorCaptionDragPageNotify" /> class.</summary>
    /// <param name="owner">The owner of the drag and drop operation.</param>
    public NavigatorCaptionDragPageNotify(KryptonNavigatorFormIntegrator owner)
    {
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));
        _dragManager = new DragManager
        {
            DocumentCursor = true
        };
    }

    #endregion

    #region Dispose

    /// <summary>Releases resources used by the <see cref="NavigatorCaptionDragPageNotify" />.</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _dragManager.Dispose();
        _tearOutFeedback?.Dispose();
        _tearOutFeedback = null;
        _disposed = true;
    }

    #endregion

    #region IDragPageNotify Members

    public void PageDragStart(object sender, KryptonNavigator? navigator, PageDragCancelEventArgs e)
    {
        if (_disposed)
        {
            e.Cancel = true;
            return;
        }

        _sourceNavigator = navigator;
        _draggingPages = e.Pages;
        _sourceForm = navigator?.FindForm() as KryptonForm;

        _tearOutFeedback ??= new TearOutFeedbackWindow();

        _dragManager.DragTargetProviders.Clear();
        foreach (KryptonNavigatorFormIntegrator target in _owner.GetRegisteredIntegrators())
        {
            _dragManager.DragTargetProviders.Add(target);
        }

        if (_owner.Workspace is { IsDisposed: false } workspace)
        {
            _dragManager.DragTargetProviders.Add(workspace);
        }

        _dragManager.PageDragStart(sender, navigator, e);
    }

    public void PageDragMove(object sender, PointEventArgs e)
    {
        if (_disposed || !_dragManager.IsDragging)
        {
            return;
        }

        _dragManager.PageDragMove(sender, e);

        // Provide tear-in/out feedback when the drag leaves the source window and is
        // not over any registered navigator client area.
        bool outsideSourceForm = _sourceForm != null && !_sourceForm.Bounds.Contains(e.Point);
        bool overAnyNavigatorClient = false;

        foreach (KryptonNavigatorFormIntegrator target in _owner.GetRegisteredIntegrators())
        {
            if (target.ContainsDropTarget(e.Point))
            {
                overAnyNavigatorClient = true;
                break;
            }
        }

        if (!overAnyNavigatorClient && _owner.Workspace is { IsDisposed: false } workspace)
        {
            Rectangle workspaceScreen = workspace.RectangleToScreen(workspace.ClientRectangle);
            overAnyNavigatorClient = workspaceScreen.Contains(e.Point);
        }

        bool showTearOut = _owner.AllowTearOut && outsideSourceForm && !overAnyNavigatorClient;

        if (showTearOut)
        {
            _tearOutFeedback?.ShowAtScreenPoint(e.Point);
        }
        else
        {
            _tearOutFeedback?.HideFeedback();
        }
    }

    public bool PageDragEnd(object sender, PointEventArgs e)
    {
        if (_disposed)
        {
            return false;
        }

        // Always hide tear-out feedback before completing the drop.
        _tearOutFeedback?.HideFeedback();

        var dropped = _dragManager.IsDragging && _dragManager.PageDragEnd(sender, e);
        if (!dropped && _owner.AllowTearOut && _sourceNavigator != null && _draggingPages != null)
        {
            dropped = _owner.TryTearOutPages(_sourceNavigator, _draggingPages, e.Point);
        }

        // Browser-like behavior: close the source form after moving the last tab away.
        // Remerge uses Pages.Remove (not Clear), so listen for empty count rather than Cleared only.
        if (dropped
            && _owner.CloseEmptySourceWindowAfterLastTabMoved
            && _sourceNavigator != null)
        {
            KryptonNavigator capturedSourceNavigator = _sourceNavigator;
            KryptonForm? capturedForm = _sourceForm;

            void TryCloseEmptySource()
            {
                if (capturedForm is { IsDisposed: false }
                    && capturedSourceNavigator is { IsDisposed: false }
                    && capturedSourceNavigator.Pages.Count == 0
                    && capturedForm.Visible)
                {
                    capturedForm.Close();
                }
            }

            TypedHandler<KryptonPage>? removedHandler = null;
            EventHandler? clearedHandler = null;

            removedHandler = (_, __) =>
            {
                if (capturedSourceNavigator.Pages.Count == 0)
                {
                    capturedSourceNavigator.Pages.Removed -= removedHandler!;
                    capturedSourceNavigator.Pages.Cleared -= clearedHandler!;
                    TryCloseEmptySource();
                }
            };

            clearedHandler = (_, __) =>
            {
                capturedSourceNavigator.Pages.Removed -= removedHandler!;
                capturedSourceNavigator.Pages.Cleared -= clearedHandler!;
                TryCloseEmptySource();
            };

            capturedSourceNavigator.Pages.Removed += removedHandler;
            capturedSourceNavigator.Pages.Cleared += clearedHandler;

            // Page may already have been transferred by PerformDrop before we return;
            // also check after the calling code finishes removing leftovers.
            if (capturedForm is { IsHandleCreated: true })
            {
                capturedForm.BeginInvoke((Action)TryCloseEmptySource);
            }
            else
            {
                TryCloseEmptySource();
            }
        }

        _draggingPages = null;
        _sourceNavigator = null;
        _sourceForm = null;

        return dropped;
    }

    public void PageDragQuit(object sender)
    {
        if (!_disposed && _dragManager.IsDragging)
        {
            _dragManager.PageDragQuit(sender);
        }

        _tearOutFeedback?.HideFeedback();
        _draggingPages = null;
        _sourceNavigator = null;
        _sourceForm = null;
    }

    #endregion
}
