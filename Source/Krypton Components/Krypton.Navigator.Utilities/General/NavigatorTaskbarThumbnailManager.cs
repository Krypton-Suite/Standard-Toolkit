#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Per-component adapter that registers with the shared <see cref="NavigatorTaskbarHostCoordinator"/>.
/// </summary>
internal sealed class NavigatorTaskbarThumbnailManager : IDisposable
{
    private readonly KryptonNavigatorTaskbarThumbnails _owner;
    private NavigatorTaskbarHostCoordinator? _coordinator;
    private Form? _boundHost;
    private KryptonPage? _previousSelectedPage;
    private bool _disposed;

    public NavigatorTaskbarThumbnailManager(KryptonNavigatorTaskbarThumbnails owner) =>
        _owner = owner;

    public Form? HostForm => _boundHost;

    public void Sync()
    {
        if (_disposed || CommonHelper.DesignMode())
        {
            return;
        }

        Form? resolved = null;
        if (_owner.Enabled && _owner.Navigator != null && !_owner.Navigator.IsDisposed)
        {
            resolved = NavigatorTaskbarHostCoordinator.ResolveTaskbarHost(_owner.Navigator.FindForm());
        }

        if (!ReferenceEquals(_boundHost, resolved))
        {
            UnregisterFromCoordinator();
            _boundHost = resolved;
            if (_boundHost != null)
            {
                _coordinator = NavigatorTaskbarHostCoordinator.GetOrCreate(_boundHost);
                _coordinator.Register(_owner);
            }
        }
        else if (_boundHost != null && _coordinator == null)
        {
            _coordinator = NavigatorTaskbarHostCoordinator.GetOrCreate(_boundHost);
            _coordinator.Register(_owner);
        }

        if (_coordinator == null)
        {
            return;
        }

        if (!_owner.Enabled || _owner.Navigator == null || _owner.Navigator.IsDisposed || _boundHost == null)
        {
            _coordinator.Unregister(_owner);
            _coordinator = null;
            _boundHost = null;
            return;
        }

        _coordinator.Sync();
    }

    public void UpdateActiveTab()
    {
        KryptonPage? previous = _previousSelectedPage;
        _previousSelectedPage = _owner.Navigator?.SelectedPage;
        _coordinator?.OnSelectedPageChanged(_owner, previous);
    }

    public void InvalidatePage(KryptonPage page) => _coordinator?.InvalidatePage(page);

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        UnregisterFromCoordinator();
    }

    private void UnregisterFromCoordinator()
    {
        if (_coordinator != null)
        {
            _coordinator.Unregister(_owner);
            _coordinator = null;
        }

        _boundHost = null;
    }
}
