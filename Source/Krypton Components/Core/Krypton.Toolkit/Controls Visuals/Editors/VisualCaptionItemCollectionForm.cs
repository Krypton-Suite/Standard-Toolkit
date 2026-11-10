#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Caption-item collection editor. Add lists existing form controls and can create new ones.
/// The property grid edits the hosted control (properties and events); Edge is set above it.
/// </summary>
internal sealed class VisualCaptionItemCollectionForm : VisualStandardCollectionForm
{
    #region Static Fields

    private static readonly Type[] NewControlTypes =
    {
        typeof(KryptonTextBox),
        typeof(KryptonButton),
        typeof(KryptonComboBox),
        typeof(KryptonCheckBox),
        typeof(KryptonLabel),
        typeof(KryptonMaskedTextBox)
    };

    #endregion

    #region Instance Fields

    private readonly KryptonDropButton _addDropButton;
    private readonly KryptonContextMenu _addMenu;
    private readonly KryptonContextMenuItems _addMenuItems;
    private readonly KryptonPanel _edgePanel;
    private readonly KryptonLabel _edgeLabel;
    private readonly KryptonComboBox _edgeChoice;
    private readonly List<Control> _createdThisSession = new List<Control>();
    private bool _syncingEdge;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="VisualCaptionItemCollectionForm"/> class.
    /// </summary>
    /// <param name="editor">Owning collection editor.</param>
    public VisualCaptionItemCollectionForm(KryptonDesignerStandardCollectionEditor editor)
        : base(editor)
    {
        _addMenuItems = new KryptonContextMenuItems();
        _addMenu = new KryptonContextMenu();
        _addMenu.Items.Add(_addMenuItems);

        _addDropButton = new KryptonDropButton
        {
            Name = @"_addDropButton",
            KryptonContextMenu = _addMenu,
            Dock = DockStyle.Fill
        };
        _addDropButton.Values.Text = @"Add";
        _addDropButton.DropDown += OnAddDropDown;

        var buttonSize = KryptonDesignerEditorDpi.Scale(this, new Size(112, 28));
        _addDropButton.AutoSize = false;
        _addDropButton.Size = buttonSize;
        _addDropButton.MinimumSize = buttonSize;

        ButtonPanel.Controls.Remove(AddButton);
        AddButton.Visible = false;
        ButtonPanel.Controls.Add(_addDropButton, 0, 0);

        _edgeLabel = new KryptonLabel
        {
            AutoSize = true,
            Name = @"_edgeLabel"
        };
        _edgeLabel.Values.Text = @"Edge";

        _edgeChoice = new KryptonComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            DropDownWidth = 120,
            Name = @"_edgeChoice",
            Width = 120
        };
        _edgeChoice.Items.AddRange(new object[]
        {
            FormCaptionTextBoxEdge.Left,
            FormCaptionTextBoxEdge.Center,
            FormCaptionTextBoxEdge.Right
        });
        _edgeChoice.SelectedIndexChanged += OnEdgeChoiceChanged;

        _edgePanel = new KryptonPanel
        {
            Dock = DockStyle.Top,
            Height = 32,
            Name = @"_edgePanel",
            Padding = new Padding(0, 4, 0, 4)
        };
        _edgePanel.Controls.Add(_edgeChoice);
        _edgePanel.Controls.Add(_edgeLabel);
        _edgeLabel.Location = new Point(0, 6);
        _edgeChoice.Location = new Point(48, 4);

        // Insert edge row under the properties heading.
        PropertiesPanel.Controls.Add(_edgePanel);
        PropertiesPanel.Controls.SetChildIndex(EditorPropertyGrid, 0);
        PropertiesPanel.Controls.SetChildIndex(_edgePanel, 1);
        PropertiesPanel.Controls.SetChildIndex(PropertiesLabel, 2);

        EditorPropertyGrid.PropertyValueChanged += OnControlPropertyValueChanged;
    }

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override void OnEditValueChanged()
    {
        base.OnEditValueChanged();
        RebuildAddMenu();
        UpdateButtons();
    }

    /// <inheritdoc />
    protected override void UpdateButtons()
    {
        base.UpdateButtons();
        _addDropButton.Enabled = Context?.GetService(typeof(IDesignerHost)) is IDesignerHost
            || GetAvailableControls().Count > 0;
    }

    /// <inheritdoc />
    protected override void UpdatePropertyGrid()
    {
        if (SelectedCollectionItem is KryptonFormCaptionItem item)
        {
            SyncEdgeChoice(item);
            if (item.Control != null && !item.Control.IsDisposed)
            {
                PropertiesLabel.Values.Text = string.Format(
                    CultureInfo.CurrentCulture,
                    @"{0} properties",
                    GetControlDisplayName(item.Control));
                AttachPropertyGridDesignerSite(EditorPropertyGrid, item.Control);
                EditorPropertyGrid.SelectedObject = item.Control;
                EditorPropertyGrid.RefreshTabs(PropertyTabScope.Component);
                // PropertyGrid only shows the lightning bolt when the object is a sited IComponent
                // and the Events tab exists; force the button visible for designer-sited controls.
                EditorPropertyGrid.SetEventsButtonVisible(item.Control.Site != null);
                _edgePanel.Enabled = true;
                return;
            }

            PropertiesLabel.Values.Text = @"Caption item properties";
            AttachPropertyGridDesignerSite(EditorPropertyGrid);
            EditorPropertyGrid.SelectedObject = item;
            EditorPropertyGrid.RefreshTabs(PropertyTabScope.Component);
            EditorPropertyGrid.SetEventsButtonVisible(false);
            _edgePanel.Enabled = true;
            return;
        }

        PropertiesLabel.Values.Text = @"Properties:";
        AttachPropertyGridDesignerSite(EditorPropertyGrid);
        EditorPropertyGrid.SelectedObject = null;
        EditorPropertyGrid.RefreshTabs(PropertyTabScope.Component);
        EditorPropertyGrid.SetEventsButtonVisible(false);
        _edgePanel.Enabled = false;
    }

    /// <inheritdoc />
    protected override void OnAddClick(object? sender, EventArgs e)
    {
        // Add is driven by the drop-down menu.
    }

    /// <inheritdoc />
    protected override void OnRemoveClick(object? sender, EventArgs e)
    {
        if (SelectedCollectionItem is KryptonFormCaptionItem item
            && item.Control != null
            && ContainsControl(_createdThisSession, item.Control))
        {
            DestroyCreatedControl(item.Control);
        }

        base.OnRemoveClick(sender, e);
        RebuildAddMenu();
        UpdateButtons();
    }

    /// <inheritdoc />
    protected override void OnOkClick(object? sender, EventArgs e)
    {
        _createdThisSession.Clear();
        base.OnOkClick(sender, e);
    }

    /// <inheritdoc />
    protected override void OnCancelClick(object? sender, EventArgs e)
    {
        foreach (Control control in _createdThisSession.ToArray())
        {
            DestroyCreatedControl(control);
        }

        _createdThisSession.Clear();
        base.OnCancelClick(sender, e);
    }

    #endregion

    #region Implementation

    private void OnAddDropDown(object? sender, ContextPositionMenuArgs e)
    {
        RebuildAddMenu();
        if (_addMenuItems.Items.Count == 0)
        {
            e.Cancel = true;
        }
    }

    private void OnEdgeChoiceChanged(object? sender, EventArgs e)
    {
        if (_syncingEdge || SelectedCollectionItem is not KryptonFormCaptionItem item)
        {
            return;
        }

        if (_edgeChoice.SelectedItem is FormCaptionTextBoxEdge edge)
        {
            item.Edge = edge;
            RefreshSelectedMemberText();
        }
    }

    private void OnControlPropertyValueChanged(object? sender, PropertyValueChangedEventArgs e) =>
        RefreshSelectedMemberText();

    private void SyncEdgeChoice(KryptonFormCaptionItem item)
    {
        _syncingEdge = true;
        try
        {
            _edgeChoice.SelectedItem = item.Edge;
        }
        finally
        {
            _syncingEdge = false;
        }
    }

    private void RefreshSelectedMemberText() => RefreshMembersListPreservingSelection();

    private void RebuildAddMenu()
    {
        _addMenuItems.Items.Clear();

        var existingHeading = new KryptonContextMenuHeading
        {
            Text = @"Existing controls"
        };
        _addMenuItems.Items.Add(existingHeading);

        var available = GetAvailableControls();
        if (available.Count > 0)
        {
            foreach (Control control in available)
            {
                var menuItem = new KryptonContextMenuItem
                {
                    Text = GetControlDisplayName(control)
                };
                Control captured = control;
                menuItem.Click += (_, __) => AddCaptionItemForControl(captured);
                _addMenuItems.Items.Add(menuItem);
            }

            _addMenuItems.Items.Add(new KryptonContextMenuSeparator());
        }

        var newHeading = new KryptonContextMenuHeading
        {
            Text = @"New control"
        };
        _addMenuItems.Items.Add(newHeading);

        foreach (Type controlType in NewControlTypes)
        {
            var menuItem = new KryptonContextMenuItem
            {
                Text = controlType.Name
            };
            Type capturedType = controlType;
            menuItem.Click += (_, __) => AddNewControlOfType(capturedType);
            _addMenuItems.Items.Add(menuItem);
        }
    }

    private void AddCaptionItemForControl(Control control)
    {
        var item = (KryptonFormCaptionItem)CreateInstance(typeof(KryptonFormCaptionItem));
        item.Control = control;
        item.Edge = FormCaptionTextBoxEdge.Right;
        AddWorkingItem(item);
        RebuildAddMenu();
        UpdateButtons();
    }

    private void AddNewControlOfType(Type controlType)
    {
        if (Context?.GetService(typeof(IDesignerHost)) is not IDesignerHost host)
        {
            return;
        }

        DesignerTransaction? transaction = null;
        try
        {
            transaction = host.CreateTransaction(@"Add caption " + controlType.Name);
            var control = (Control)CommonHelper.CreateInstance(controlType, host);
            if (control.Width <= 0 || control.Height <= 0)
            {
                control.Size = new Size(160, 23);
            }

            if (host.RootComponent is Control root && control.Parent == null && !ReferenceEquals(control, root))
            {
                root.Controls.Add(control);
            }

            _createdThisSession.Add(control);
            AddCaptionItemForControl(control);
            transaction.Commit();
            transaction = null;
        }
        finally
        {
            transaction?.Cancel();
        }
    }

    private void DestroyCreatedControl(Control control)
    {
        _createdThisSession.Remove(control);
        if (Context?.GetService(typeof(IDesignerHost)) is IDesignerHost host)
        {
            CommonHelper.DestroyInstance(control, host);
        }
        else if (!control.IsDisposed)
        {
            control.Dispose();
        }
    }

    private List<Control> GetAvailableControls()
    {
        var result = new List<Control>();
        var used = GetUsedControls();

        foreach (Control control in EnumerateFormControls())
        {
            if (control is Form || control.IsDisposed || used.Contains(control))
            {
                continue;
            }

            result.Add(control);
        }

        result.Sort((left, right) => string.Compare(GetControlDisplayName(left), GetControlDisplayName(right), StringComparison.CurrentCultureIgnoreCase));
        return result;
    }

    private HashSet<Control> GetUsedControls()
    {
        var used = new HashSet<Control>();
        if (WorkingItems == null)
        {
            return used;
        }

        foreach (object entry in WorkingItems)
        {
            if (entry is KryptonFormCaptionItem item && item.Control != null)
            {
                used.Add(item.Control);
            }
        }

        return used;
    }

    private IEnumerable<Control> EnumerateFormControls()
    {
        if (Context?.GetService(typeof(IReferenceService)) is IReferenceService references)
        {
            foreach (object reference in references.GetReferences(typeof(Control)))
            {
                if (reference is Control control)
                {
                    yield return control;
                }
            }

            yield break;
        }

        if (Context?.GetService(typeof(IDesignerHost)) is IDesignerHost host)
        {
            foreach (IComponent component in host.Container.Components)
            {
                if (component is Control control)
                {
                    yield return control;
                }
            }
        }
    }

    private static string GetControlDisplayName(Control control)
    {
        if (!string.IsNullOrEmpty(control.Name))
        {
            return control.Name;
        }

        if (!string.IsNullOrEmpty(control.Site?.Name))
        {
            return control.Site!.Name!;
        }

        return control.GetType().Name;
    }

    private static bool ContainsControl(List<Control> list, Control control)
    {
        foreach (Control item in list)
        {
            if (ReferenceEquals(item, control))
            {
                return true;
            }
        }

        return false;
    }

    #endregion
}
