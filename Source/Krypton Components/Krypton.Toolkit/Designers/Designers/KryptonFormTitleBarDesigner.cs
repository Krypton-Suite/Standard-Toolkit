#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonFormTitleBarDesigner : ComponentDesigner
{
    #region Instance Fields

    private KryptonFormTitleBar? _titleBar;
    private IComponentChangeService? _changeService;
    private DesignerVerbCollection? _verbs;

    #endregion

    #region Public Overrides

    /// <summary>
    /// Gets the design-time verbs supported by the component.
    /// </summary>
    public override DesignerVerbCollection Verbs
    {
        get
        {
            if (_verbs == null)
            {
                _verbs = new DesignerVerbCollection
                {
                    new DesignerVerb(@"Insert Standard Items", OnInsertStandardItems)
                };
            }

            return _verbs;
        }
    }

    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    public override void Initialize([DisallowNull] IComponent component)
    {
        base.Initialize(component);

        Debug.Assert(component != null);

        _titleBar = component as KryptonFormTitleBar;
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        _changeService!.ComponentRemoving += OnComponentRemoving;
    }

    /// <summary>
    /// Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionLists = new DesignerActionListCollection();
            actionLists.AddRange(base.ActionLists);
            actionLists.Add(new KryptonFormTitleBarActionList(this));
            return actionLists;
        }
    }

    #endregion

    #region Protected

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        try
        {
            if (disposing)
            {
                _changeService!.ComponentRemoving -= OnComponentRemoving;
            }
        }
        finally
        {
            base.Dispose(disposing);
        }
    }

    #endregion

    #region Internal

    /// <summary>
    /// Inserts the standard button specs into <paramref name="titleBar"/> using the supplied
    /// <paramref name="host"/> and notifies <paramref name="changeService"/> of the change.
    /// Both the designer verb and the action-list verb delegate here so the logic lives in one place.
    /// </summary>
    internal static void InsertStandardItems(
        KryptonFormTitleBar titleBar,
        IDesignerHost? host,
        IComponentChangeService? changeService)
    {
        var specs = KryptonFormTitleBar.CreateStandardButtonSpecs();

        foreach (var template in specs)
        {
            var spec = host != null
                ? (ButtonSpecAny)host.CreateComponent(typeof(ButtonSpecAny))
                : new ButtonSpecAny();

            spec.Type = template.Type;
            spec.ToolTipTitle = template.ToolTipTitle;
            spec.Enabled = template.Enabled;
            spec.Visible = template.Visible;
            spec.Edge = template.Edge;
            spec.Text = template.Text;
            spec.AllowInheritText = template.AllowInheritText;
            spec.ShowDrop = template.ShowDrop;
            spec.KryptonContextMenu = template.KryptonContextMenu;
            titleBar.ButtonSpecs.Add(spec);
        }

        changeService?.OnComponentChanged(titleBar, null, null, null);
    }

    #endregion

    #region Implementation

    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        if (_titleBar != null && Equals(e.Component, _titleBar))
        {
            var host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            for (var i = _titleBar.ButtonSpecs.Count - 1; i >= 0; i--)
            {
                var spec = _titleBar.ButtonSpecs[i];
                _titleBar.ButtonSpecs.Remove(spec);
                host?.DestroyComponent(spec);
            }
        }
    }

    private void OnInsertStandardItems(object? sender, EventArgs e)
    {
        if (_titleBar == null)
        {
            return;
        }

        InsertStandardItems(
            _titleBar,
            GetService(typeof(IDesignerHost)) as IDesignerHost,
            _changeService);
    }

    #endregion
}
