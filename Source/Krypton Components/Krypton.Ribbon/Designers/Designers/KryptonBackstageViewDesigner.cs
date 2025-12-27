#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Designer for the backstage view instance.
/// </summary>
public class KryptonBackstageViewDesigner : ParentControlDesigner
{
    #region Instance Fields
    private DesignerVerbCollection? _verbs;
    private DesignerVerb _verbAddPage;
    private DesignerVerb _verbRemovePage;
    private DesignerVerb _verbClearPages; 
    private IDesignerHost _designerHost;
    private IComponentChangeService _changeService;
    private ISelectionService _selectionService;
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the backstage view component.
    /// </summary>
    public KryptonBackstageView? BackstageView { get; private set; }
    #endregion

    #region Public Overrides
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate the designer with.</param>
    public override void Initialize([DisallowNull] IComponent component)
    {
        // Let base class do standard stuff
        base.Initialize(component);

        Debug.Assert(component != null);

        // The resizing handles around the control need to change depending on the
        // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
        // do not get the resizing handles, otherwise you do.
        AutoResizeHandles = true;

        BackstageView = component as KryptonBackstageView;
        if (BackstageView != null)
        {
            // Must enable the child panel so that copy/paste correctly copies the child pages.
            EnableDesignMode(BackstageView.PageContainer, @"PageContainer");

            // Make sure that all the pages in control can be designed
            foreach (KryptonBackstagePage page in BackstageView.Pages)
            {
                EnableDesignMode(page, page.Name);
            }

            BackstageView.Pages.Inserted += OnPageInserted;
            BackstageView.Pages.Removed += OnPageRemoved;
            BackstageView.Pages.Cleared += OnPagesCleared;
            BackstageView.SelectedPageChanged += OnSelectedPageChanged;
        }

        // Get access to the services
        _designerHost = (IDesignerHost?)GetService(typeof(IDesignerHost)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_designerHost)));
        _changeService = (IComponentChangeService?)GetService(typeof(IComponentChangeService)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_changeService)));
        _selectionService = (ISelectionService?)GetService(typeof(ISelectionService)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_selectionService)));

        // We need to know when we are being removed
        _changeService.ComponentRemoving += OnComponentRemoving;
    }

    /// <summary>
    /// Initializes a newly created component.
    /// </summary>
    /// <param name="defaultValues">A name/value dictionary of default values to apply to properties.</param>
    public override void InitializeNewComponent(IDictionary defaultValues)
    {
        base.InitializeNewComponent(defaultValues);

        // Add a couple of pages
        OnAddPage(this, EventArgs.Empty);
        OnAddPage(this, EventArgs.Empty);

        // Get access to the Pages property
        if (BackstageView != null)
        {
            MemberDescriptor? propertyPages = TypeDescriptor.GetProperties(BackstageView)[nameof(KryptonBackstageView.Pages)];

            // Notify that the pages collection has been updated
            RaiseComponentChanging(propertyPages);
            RaiseComponentChanged(propertyPages, null, null);
        }
    }

    /// <summary>
    /// Gets the design-time verbs supported by the component that is associated with the designer.
    /// </summary>
    public override DesignerVerbCollection Verbs
    {
        get
        {
            if (_verbs == null)
            {
                _verbAddPage = new DesignerVerb(@"Add Page", OnAddPage);
                _verbRemovePage = new DesignerVerb(@"Remove Page", OnRemovePage);
                _verbClearPages = new DesignerVerb(@"Clear Pages", OnClearPages);
                _verbs = new DesignerVerbCollection(new[] { _verbAddPage, _verbRemovePage, _verbClearPages });
                UpdateVerbStatus();
            }

            return _verbs;
        }
    }

    /// <summary>
    /// Gets the collection of components associated with the component managed by the designer.
    /// </summary>
    public override ICollection AssociatedComponents
    {
        get
        {
            var compound = new ArrayList();

            if (BackstageView != null)
            {
                compound.AddRange(BackstageView.Pages);
            }

            return compound;
        }
    }

    /// <summary>
    /// Indicates whether the specified control can be a child of the control managed by a designer.
    /// </summary>
    /// <param name="control">The Control to test.</param>
    /// <returns>true if the specified control can be a child; otherwise, false.</returns>
    public override bool CanParent(Control control)
    {
        if (BackstageView == null)
        {
            return false;
        }

        if (control is KryptonBackstagePage)
        {
            return !BackstageView.PageContainer.Controls.Contains(control);
        }

        return false;
    }
    #endregion

    #region Implementation
    private void OnAddPage(object? sender, EventArgs e)
    {
        if (BackstageView == null)
        {
            return;
        }

        // Create the page and give it a sensible initial name/text
        var page = (KryptonBackstagePage)_designerHost.CreateComponent(typeof(KryptonBackstagePage));
        page.Text = page.Name;

        MemberDescriptor? propertyPages = TypeDescriptor.GetProperties(BackstageView)[nameof(KryptonBackstageView.Pages)];
        RaiseComponentChanging(propertyPages);

        BackstageView.Pages.Add(page);
        BackstageView.SelectedPage = page;

        RaiseComponentChanged(propertyPages, null, null);

        // Ensure design-mode is enabled for the page
        EnableDesignMode(page, page.Name);

        UpdateVerbStatus();
    }

    private void OnRemovePage(object? sender, EventArgs e)
    {
        if (BackstageView == null)
        {
            return;
        }

        KryptonBackstagePage? page = BackstageView.SelectedPage;
        if (page == null)
        {
            return;
        }

        MemberDescriptor? propertyPages = TypeDescriptor.GetProperties(BackstageView)[nameof(KryptonBackstageView.Pages)];
        RaiseComponentChanging(propertyPages);

        BackstageView.Pages.Remove(page);
        _designerHost.DestroyComponent(page);

        RaiseComponentChanged(propertyPages, null, null);

        UpdateVerbStatus();
    }

    private void OnClearPages(object? sender, EventArgs e)
    {
        if (BackstageView == null)
        {
            return;
        }

        if (BackstageView.Pages.Count == 0)
        {
            return;
        }

        MemberDescriptor? propertyPages = TypeDescriptor.GetProperties(BackstageView)[nameof(KryptonBackstageView.Pages)];
        RaiseComponentChanging(propertyPages);

        // Copy to array to avoid collection modification issues
        foreach (KryptonBackstagePage page in BackstageView.Pages.ToArray())
        {
            BackstageView.Pages.Remove(page);
            _designerHost.DestroyComponent(page);
        }

        RaiseComponentChanged(propertyPages, null, null);

        UpdateVerbStatus();
    }

    private void OnPageInserted(object? sender, TypedCollectionEventArgs<KryptonBackstagePage> e)
    {
        if (BackstageView != null && e.Item != null)
        {
            EnableDesignMode(e.Item, e.Item.Name);
            UpdateVerbStatus();
        }
    }

    private void OnPageRemoved(object? sender, TypedCollectionEventArgs<KryptonBackstagePage> e) => UpdateVerbStatus();

    private void OnPagesCleared(object? sender, EventArgs e) => UpdateVerbStatus();

    private void OnSelectedPageChanged(object? sender, EventArgs e)
    {
        if (BackstageView?.SelectedPage != null)
        {
            _selectionService.SetSelectedComponents(new object[] { BackstageView.SelectedPage }, SelectionTypes.Primary);
            UpdateVerbStatus();
        }
    }

    private void UpdateVerbStatus()
    {
        if (BackstageView == null || _verbs == null)
        {
            return;
        }

        _verbRemovePage.Enabled = BackstageView.Pages.Count > 0 && BackstageView.SelectedPage != null;
        _verbClearPages.Enabled = BackstageView.Pages.Count > 0;
    }

    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        if (BackstageView == null)
        {
            return;
        }

        // If removing a page, ensure it is also removed from the Pages collection
        if (e.Component is KryptonBackstagePage page)
        {
            if (BackstageView.Pages.Contains(page))
            {
                BackstageView.Pages.Remove(page);
            }
        }

        // If removing the view itself, unhook
        if (ReferenceEquals(e.Component, BackstageView))
        {
            _changeService.ComponentRemoving -= OnComponentRemoving;
        }
    }
    #endregion
}
