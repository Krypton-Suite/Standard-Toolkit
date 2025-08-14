#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, KamaniAR, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides a designer for the KryptonExtraGrid control, ensuring
/// internal controls are designable but not directly removable.
/// </summary>
internal class KryptonOutlookGridAioDesigner : KryptonHeaderGroupDesigner
{
    private KryptonOutlookGridAio? _extraGrid;
    private IComponentChangeService? _changeService;
    private IDesignerHost? _designerHost;
    private bool _disposed;
    private KryptonOutlookGridAioDesignerNativeWindow _aioDesignerNativeWindow;
    #region Public

    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate the designer with.</param>
    public override void Initialize([DisallowNull] IComponent component)
    {
        // Call the base designer's Initialize FIRST.
        // This ensures the Panel is made designable by KryptonHeaderGroupDesigner.
        base.Initialize(component);

        Debug.Assert(component != null);

        _disposed = false;
        _extraGrid = component as KryptonOutlookGridAio;

        // Do NOT enable design mode for the inner OutlookGrid to avoid nested containers.
        if (_extraGrid != null && _extraGrid.OutlookGrid != null)
        {
            _aioDesignerNativeWindow = new();
            _aioDesignerNativeWindow.AssignHandle(_extraGrid.OutlookGrid.Handle);
        }

        _designerHost = GetService(typeof(System.ComponentModel.Design.IDesignerHost)) as System.ComponentModel.Design.IDesignerHost;
        _changeService = GetService(typeof(System.ComponentModel.Design.IComponentChangeService)) as System.ComponentModel.Design.IComponentChangeService;

        if (_changeService != null)
        {
            _changeService.ComponentAdded += OnComponentAdded;
        }

        // Sanitize any existing column component site names created before this designer fix
        SanitizeExistingColumnSites();
    }

    /// <summary>
    /// Gets the collection of components associated with the component managed by the designer.
    /// </summary>
    public override ICollection AssociatedComponents
    {
        get
        {
            ArrayList list = new(base.AssociatedComponents);

            if (_extraGrid != null)
            {
                list.Add(_extraGrid.OutlookGrid);
                if (_extraGrid.OutlookGrid != null)
                {
                    foreach (System.Windows.Forms.DataGridViewColumn column in _extraGrid.OutlookGrid.Columns)
                    {
                        list.Add(column);
                    }
                }

                list.Add(_extraGrid.SearchToolBar);
                list.Add(_extraGrid.GroupBox);
                list.Add(_extraGrid.SummaryGrid);
            }
            return list;
        }
    }

    /// <summary>
    ///  Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            // Create a collection of action lists
            var actionLists = new DesignerActionListCollection
            {
                // Add the header group specific list
                new KryptonOutlookGridAioActionList(this)
            };

            return actionLists;
        }
    }

    #endregion Public

    protected override void Dispose(bool disposing)
    {
        if (!_disposed 
            && disposing 
            && _changeService != null)
        {
            _changeService.ComponentAdded -= OnComponentAdded;
            _aioDesignerNativeWindow?.ReleaseHandle();
            _disposed = true;
        }

        base.Dispose(disposing);
    }

    private void OnComponentAdded(object? sender, System.ComponentModel.Design.ComponentEventArgs e)
    {
        var column = e.Component as System.Windows.Forms.DataGridViewColumn;
        if (column == null
            || _extraGrid == null 
            || _extraGrid.OutlookGrid == null
            || _designerHost == null 
            || column.Site == null)
        {
            return;
        }

        SanitizeAndResiteColumnIfNeeded(column);
    }

    private void SanitizeExistingColumnSites()
    {
        if (_extraGrid == null 
            || _extraGrid.OutlookGrid == null 
            || _designerHost == null)
        {
            return;
        }

        foreach (System.Windows.Forms.DataGridViewColumn column in _extraGrid.OutlookGrid.Columns)
        {
            if (column.Site == null)
            {
                continue;
            }

            SanitizeAndResiteColumnIfNeeded(column);
        }

        // Force the designer to refresh the inner grid surface so new columns appear at design time
        _extraGrid.OutlookGrid.Invalidate();
    }

    private void SanitizeAndResiteColumnIfNeeded(System.Windows.Forms.DataGridViewColumn column)
    {
        if (_designerHost == null 
            || column.Site == null 
            || _extraGrid == null)
        {
            return;
        }

        string currentName = column.Site.Name ?? string.Empty;
        string? ownerName = _extraGrid.Site != null ? _extraGrid.Site.Name : null;
        int dotIndex = currentName.LastIndexOf('.');
        bool hasOwnerPrefix = ownerName != null &&
                              currentName.StartsWith(ownerName + ".", StringComparison.Ordinal);
        if (dotIndex < 0 || !hasOwnerPrefix)
        {
            return;
        }

        string baseName = currentName.Substring(dotIndex + 1);
        if (string.IsNullOrWhiteSpace(baseName))
        {
            baseName = "column";
        }

        string newName = baseName;
        if (_designerHost.Container.Components[newName] != null)
        {
            int i = 1;
            string tryName = newName;
            while (_designerHost.Container.Components[tryName] != null)
            {
                tryName = newName + i.ToString();
                i++;
            }
            newName = tryName;
        }

        using (var transaction = _designerHost.CreateTransaction("SanitizeColumnName"))
        {
            try
            {
                _changeService?.OnComponentChanging(column, null);

                // Remove from current container then add to root container with the sanitized name
                var currentContainer = column.Site.Container;
                if (currentContainer != null)
                {
                    currentContainer.Remove(column);
                }
                _designerHost.Container.Add(column, newName);

                // Sanitize runtime Name property too
                if (!string.IsNullOrEmpty(column.Name))
                {
                    int nameDot = column.Name.LastIndexOf('.');
                    if (nameDot >= 0)
                    {
                        column.Name = column.Name.Substring(nameDot + 1);
                    }
                }

                _changeService?.OnComponentChanged(column, null, null, null);
                transaction.Commit();
            }
            catch
            {
                try
                {
                    transaction.Cancel();
                }
                catch { }
            }
        }
    }
}