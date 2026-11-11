#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Smart-tag action list for <see cref="KryptonNavigatorTaskbarThumbnails"/>.
/// </summary>
internal class KryptonNavigatorTaskbarThumbnailsActionList : DesignerActionList
{
    private readonly KryptonNavigatorTaskbarThumbnails _component;
    private readonly IComponentChangeService? _service;
    private DesignerActionItemCollection? _actions;

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonNavigatorTaskbarThumbnailsActionList"/> class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonNavigatorTaskbarThumbnailsActionList(KryptonNavigatorTaskbarThumbnailsDesigner owner)
        : base(owner.Component)
    {
        _component = (KryptonNavigatorTaskbarThumbnails)owner.Component;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    public KryptonNavigator? Navigator
    {
        get => _component.Navigator;
        set
        {
            if (!ReferenceEquals(_component.Navigator, value))
            {
                _service?.OnComponentChanged(_component, null, _component.Navigator, value);
                _component.Navigator = value;
            }
        }
    }

    public KryptonNavigatorFormIntegrator? FormIntegrator
    {
        get => _component.FormIntegrator;
        set
        {
            if (!ReferenceEquals(_component.FormIntegrator, value))
            {
                _service?.OnComponentChanged(_component, null, _component.FormIntegrator, value);
                _component.FormIntegrator = value;
            }
        }
    }

    public bool Enabled
    {
        get => _component.Enabled;
        set
        {
            if (_component.Enabled != value)
            {
                _service?.OnComponentChanged(_component, null, _component.Enabled, value);
                _component.Enabled = value;
            }
        }
    }

    public bool ShowTabGroupThumbnails
    {
        get => _component.ShowTabGroupThumbnails;
        set
        {
            if (_component.ShowTabGroupThumbnails != value)
            {
                _service?.OnComponentChanged(_component, null, _component.ShowTabGroupThumbnails, value);
                _component.ShowTabGroupThumbnails = value;
            }
        }
    }

    public bool AllowCloseFromThumbnail
    {
        get => _component.AllowCloseFromThumbnail;
        set
        {
            if (_component.AllowCloseFromThumbnail != value)
            {
                _service?.OnComponentChanged(_component, null, _component.AllowCloseFromThumbnail, value);
                _component.AllowCloseFromThumbnail = value;
            }
        }
    }

    public bool IncludeHiddenPages
    {
        get => _component.IncludeHiddenPages;
        set
        {
            if (_component.IncludeHiddenPages != value)
            {
                _service?.OnComponentChanged(_component, null, _component.IncludeHiddenPages, value);
                _component.IncludeHiddenPages = value;
            }
        }
    }

    public int MaxThumbnails
    {
        get => _component.MaxThumbnails;
        set
        {
            if (_component.MaxThumbnails != value)
            {
                _service?.OnComponentChanged(_component, null, _component.MaxThumbnails, value);
                _component.MaxThumbnails = value;
            }
        }
    }

    public bool ActiveTabUsesAppPreview
    {
        get => _component.ActiveTabUsesAppPreview;
        set
        {
            if (_component.ActiveTabUsesAppPreview != value)
            {
                _service?.OnComponentChanged(_component, null, _component.ActiveTabUsesAppPreview, value);
                _component.ActiveTabUsesAppPreview = value;
            }
        }
    }

    /// <inheritdoc />
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        _actions ??= new DesignerActionItemCollection
        {
            new DesignerActionHeaderItem("Behavior"),
            new DesignerActionPropertyItem(nameof(Navigator), "Navigator", "Behavior",
                "Navigator whose pages are registered as taskbar thumbnails."),
            new DesignerActionPropertyItem(nameof(FormIntegrator), "Form Integrator", "Behavior",
                "Form integrator whose TabGroups catalog drives composite Group | … thumbnails."),
            new DesignerActionPropertyItem(nameof(Enabled), "Enabled", "Behavior",
                "Register each eligible page as an individual Windows taskbar thumbnail."),
            new DesignerActionPropertyItem(nameof(ShowTabGroupThumbnails), "Show Tab Group Thumbnails", "Behavior",
                "Insert composite Group | … taskbar thumbnails for FormIntegrator tab groups."),
            new DesignerActionPropertyItem(nameof(AllowCloseFromThumbnail), "Allow Close From Thumbnail", "Behavior",
                "Closing a taskbar thumbnail closes the related navigator page."),
            new DesignerActionPropertyItem(nameof(IncludeHiddenPages), "Include Hidden Pages", "Behavior",
                "Include hidden pages in the taskbar thumbnail flyout."),
            new DesignerActionPropertyItem(nameof(MaxThumbnails), "Max Thumbnails", "Behavior",
                "Maximum registered taskbar tabs (groups + pages). Zero means unlimited."),
            new DesignerActionPropertyItem(nameof(ActiveTabUsesAppPreview), "Active Tab Uses App Preview", "Behavior",
                "When true, the active tab uses the application window for thumbnail and Peek previews.")
        };

        return _actions;
    }
}
