#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

internal class KryptonBackstagePageDesigner : ScrollableControlDesigner
{
    #region Instance Fields
    private KryptonBackstagePage? _page;
    private IComponentChangeService _changeService;
    #endregion

    #region Public Overrides
    public override void Initialize([DisallowNull] IComponent component)
    {
        base.Initialize(component);

        _page = component as KryptonBackstagePage;
        _changeService = (IComponentChangeService?)GetService(typeof(IComponentChangeService)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_changeService)));
        _changeService.ComponentRemoving += OnComponentRemoving;

        // Lock the component from user size/location change when hosted inside a backstage view
        PropertyDescriptor? descriptor = TypeDescriptor.GetProperties(component)[@"Locked"];
        if (descriptor != null && ParentBackstageView != null)
        {
            descriptor.SetValue(component, true);
        }
    }

    public override bool CanBeParentedTo(IDesigner parentDesigner) => parentDesigner.Component is KryptonBackstageView;

    public override SelectionRules SelectionRules =>
        ParentBackstageView != null
            ? (SelectionRules.None | SelectionRules.Locked)
            : SelectionRules.None;
    #endregion

    #region Protected
    /// <summary>
    /// Releases all resources used by the component. 
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        try
        {
            if (disposing)
            {
                // Unhook from events
                if (_changeService != null)
                {
                    _changeService.ComponentRemoving -= OnComponentRemoving;
                }
            }
        }
        finally
        {
            // Must let base class do standard stuff
            base.Dispose(disposing);
        }
    }
    #endregion

    #region Implementation
    private KryptonBackstageView? ParentBackstageView => _page?.Parent as KryptonBackstageView ?? _page?.Parent?.Parent as KryptonBackstageView;

    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        if (_page == null)
        {
            return;
        }

        // When the page is being removed, unhook to avoid leaks
        if (ReferenceEquals(e.Component, _page))
        {
            _changeService.ComponentRemoving -= OnComponentRemoving;
            _page = null;
        }
    }
    #endregion
}
