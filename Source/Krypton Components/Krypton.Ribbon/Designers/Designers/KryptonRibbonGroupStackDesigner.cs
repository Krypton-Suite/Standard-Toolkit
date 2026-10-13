#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *  
 *
 */
#endregion

namespace Krypton.Ribbon;

internal class KryptonRibbonGroupStackDesigner : ComponentDesigner
{
    #region Instance Fields
    private IDesignerHost _designerHost;
    private IComponentChangeService _changeService;
    private KryptonRibbonGroupStack _ribbonStack;
    private DesignerVerbCollection _verbs;
    private DesignerVerb _toggleHelpersVerb;
    private DesignerVerb _moveFirstVerb;
    private DesignerVerb _movePrevVerb;
    private DesignerVerb _moveNextVerb;
    private DesignerVerb _moveLastVerb;
    private DesignerVerb _addButtonVerb;
    private DesignerVerb _addColorButtonVerb;
    private DesignerVerb _addCheckBoxVerb;
    private DesignerVerb _addRadioButtonVerb;
    private DesignerVerb _addLabelVerb;
    private DesignerVerb _addCustomControlVerb;
    private DesignerVerb _addClusterVerb;
    private DesignerVerb _addTextBoxVerb;
    private DesignerVerb _addMaskedTextBoxVerb;
    private DesignerVerb _addRichTextBoxVerb;
    private DesignerVerb _addComboBoxVerb;
    private DesignerVerb _addNumericUpDownVerb;
    private DesignerVerb _addDomainUpDownVerb;
    private DesignerVerb _addDateTimePickerVerb;
    private DesignerVerb _addTrackBarVerb;
    private DesignerVerb _addThemeComboBoxVerb;
    private DesignerVerb _clearItemsVerb;
    private DesignerVerb _deleteStackVerb;
    private ContextMenuStrip? _cms;
    private ToolStripMenuItem _toggleHelpersMenu;
    private ToolStripMenuItem _visibleMenu;
    private ToolStripMenuItem _maximumSizeMenu;
    private ToolStripMenuItem _maximumLMenu;
    private ToolStripMenuItem _maximumMMenu;
    private ToolStripMenuItem _maximumSMenu;
    private ToolStripMenuItem _minimumSizeMenu;
    private ToolStripMenuItem _minimumLMenu;
    private ToolStripMenuItem _minimumMMenu;
    private ToolStripMenuItem _minimumSMenu;
    private ToolStripMenuItem _moveFirstMenu;
    private ToolStripMenuItem _movePreviousMenu;
    private ToolStripMenuItem _moveNextMenu;
    private ToolStripMenuItem _moveLastMenu;
    private ToolStripMenuItem _moveToGroupMenu;
    private ToolStripMenuItem _addButtonMenu;
    private ToolStripMenuItem _addColorButtonMenu;
    private ToolStripMenuItem _addCheckBoxMenu;
    private ToolStripMenuItem _addRadioButtonMenu;
    private ToolStripMenuItem _addLabelMenu;
    private ToolStripMenuItem _addCustomControlMenu;
    private ToolStripMenuItem _addClusterMenu;
    private ToolStripMenuItem _addTextBoxMenu;
    private ToolStripMenuItem _addMaskedTextBoxMenu;
    private ToolStripMenuItem _addRichTextBoxMenu;
    private ToolStripMenuItem _addComboBoxMenu;
    private ToolStripMenuItem _addNumericUpDownMenu;
    private ToolStripMenuItem _addDomainUpDownMenu;
    private ToolStripMenuItem _addDateTimePickerMenu;
    private ToolStripMenuItem _addTrackBarMenu;
    private ToolStripMenuItem _addThemeComboBoxMenu;
    private ToolStripMenuItem _clearItemsMenu;
    private ToolStripMenuItem _deleteStackMenu;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonGroupStackDesigner class.
    /// </summary>
    public KryptonRibbonGroupStackDesigner()
    {
    }
    #endregion

    #region Public
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate the designer with.</param>
    public override void Initialize([DisallowNull] IComponent component)
    {
        // Let base class do standard stuff
        base.Initialize(component);

        Debug.Assert(component is not null);

        // Cast to correct type
        _ribbonStack =component as KryptonRibbonGroupStack ?? ThrowHelper.ThrowArgumentNullException(component as KryptonRibbonGroupStack, nameof(component));
        if (_ribbonStack != null)
        {
            _ribbonStack.DesignTimeAddButton += OnAddButton;
            _ribbonStack.DesignTimeAddColorButton += OnAddColorButton;
            _ribbonStack.DesignTimeAddCheckBox += OnAddCheckBox;
            _ribbonStack.DesignTimeAddRadioButton += OnAddRadioButton;
            _ribbonStack.DesignTimeAddLabel += OnAddLabel;
            _ribbonStack.DesignTimeAddCustomControl += OnAddCustomControl;
            _ribbonStack.DesignTimeAddCluster += OnAddCluster;
            _ribbonStack.DesignTimeAddTextBox += OnAddTextBox;
            _ribbonStack.DesignTimeAddMaskedTextBox += OnAddMaskedTextBox;
            _ribbonStack.DesignTimeAddRichTextBox += OnAddRichTextBox;
            _ribbonStack.DesignTimeAddComboBox += OnAddComboBox;
            _ribbonStack.DesignTimeAddNumericUpDown += OnAddNumericUpDown;
            _ribbonStack.DesignTimeAddDomainUpDown += OnAddDomainUpDown;
            _ribbonStack.DesignTimeAddDateTimePicker += OnAddDateTimePicker;
            _ribbonStack.DesignTimeAddTrackBar += OnAddTrackBar;
            _ribbonStack.DesignTimeAddThemeComboBox += OnAddThemeComboBox;
            _ribbonStack.DesignTimeContextMenu += OnContextMenu;
        }

        // Get access to the services
        _designerHost =(IDesignerHost?)GetService(typeof(IDesignerHost)) ?? ThrowHelper.ThrowNullReferenceException<IDesignerHost>(SharedStaticFunctions.VariableCannotBeNull(nameof(_designerHost)));
        _changeService =(IComponentChangeService?)GetService(typeof(IComponentChangeService)) ?? ThrowHelper.ThrowNullReferenceException<IComponentChangeService>(SharedStaticFunctions.VariableCannotBeNull(nameof(_changeService)));

        // We need to know when we are being removed/changed
        _changeService.ComponentRemoving += OnComponentRemoving;
        _changeService.ComponentChanged += OnComponentChanged;
    }

    /// <summary>
    /// Gets the collection of components associated with the component managed by the designer.
    /// </summary>
#if KRYPTON_WINFORMS_DESIGNER_SDK
    public override IReadOnlyCollection<IComponent> AssociatedComponents
#else
    public override ICollection AssociatedComponents
#endif
    {
        get
        {
            var compound = KryptonDesignerSdkCompat.ToArrayList(base.AssociatedComponents);
                
            if ( _ribbonStack.Items is not null && _ribbonStack.Items.Count > 0)
            {
                compound.AddRange(_ribbonStack.Items);
            }
                
            return KryptonDesignerSdkCompat.Associated(compound);
        }
    }

    /// <summary>
    /// Gets the design-time verbs supported by the component that is associated with the designer.
    /// </summary>
    public override DesignerVerbCollection Verbs
    {
        get
        {
            UpdateVerbStatus();
            return _verbs;
        }
    }
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
                _ribbonStack.DesignTimeAddButton -= OnAddButton;
                _ribbonStack.DesignTimeAddColorButton -= OnAddColorButton;
                _ribbonStack.DesignTimeAddCheckBox -= OnAddCheckBox;
                _ribbonStack.DesignTimeAddRadioButton -= OnAddRadioButton;
                _ribbonStack.DesignTimeAddLabel -= OnAddLabel;
                _ribbonStack.DesignTimeAddCustomControl -= OnAddCustomControl;
                _ribbonStack.DesignTimeAddCluster -= OnAddCluster;
                _ribbonStack.DesignTimeAddTextBox -= OnAddTextBox;
                _ribbonStack.DesignTimeAddMaskedTextBox -= OnAddMaskedTextBox;
                _ribbonStack.DesignTimeAddRichTextBox -= OnAddRichTextBox;
                _ribbonStack.DesignTimeAddComboBox -= OnAddComboBox;
                _ribbonStack.DesignTimeAddNumericUpDown -= OnAddNumericUpDown;
                _ribbonStack.DesignTimeAddDomainUpDown -= OnAddDomainUpDown;
                _ribbonStack.DesignTimeAddDateTimePicker -= OnAddDateTimePicker;
                _ribbonStack.DesignTimeContextMenu -= OnContextMenu;
                _ribbonStack.DesignTimeAddTrackBar -= OnAddTrackBar;
                _ribbonStack.DesignTimeAddThemeComboBox -= OnAddThemeComboBox;
                _changeService.ComponentRemoving -= OnComponentRemoving;
                _changeService.ComponentChanged -= OnComponentChanged;
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
    private void UpdateVerbStatus()
    {
        // Create verbs first time around
        if (_verbs == null)
        {
            _verbs = [];
            _toggleHelpersVerb = new DesignerVerb(@"Toggle Helpers", OnToggleHelpers);
            _moveFirstVerb = new DesignerVerb(@"Move Stack First", OnMoveFirst);
            _movePrevVerb = new DesignerVerb(@"Move Stack Previous", OnMovePrevious);
            _moveNextVerb = new DesignerVerb(@"Move Stack Next", OnMoveNext);
            _moveLastVerb = new DesignerVerb(@"Move Stack Last", OnMoveLast);
            _addButtonVerb = new DesignerVerb(@"Add Button", OnAddButton);
            _addColorButtonVerb = new DesignerVerb(@"Add Color Button", OnAddColorButton);
            _addCheckBoxVerb = new DesignerVerb(@"Add CheckBox", OnAddCheckBox);
            _addRadioButtonVerb = new DesignerVerb(@"Add RadioButton", OnAddRadioButton);
            _addLabelVerb = new DesignerVerb(@"Add Label", OnAddLabel);
            _addCustomControlVerb = new DesignerVerb(@"Add Custom Control", OnAddCustomControl);
            _addClusterVerb = new DesignerVerb(@"Add Cluster", OnAddCluster);
            _addRichTextBoxVerb = new DesignerVerb(@"Add RichTextBox", OnAddRichTextBox);
            _addTextBoxVerb = new DesignerVerb(@"Add TextBox", OnAddTextBox);
            _addMaskedTextBoxVerb = new DesignerVerb(@"Add MaskedTextBox", OnAddMaskedTextBox);
            _addComboBoxVerb = new DesignerVerb(@"Add ComboBox", OnAddComboBox);
            _addNumericUpDownVerb = new DesignerVerb(@"Add NumericUpDown", OnAddNumericUpDown);
            _addDomainUpDownVerb = new DesignerVerb(@"Add DomainUpDown", OnAddDomainUpDown);
            _addDateTimePickerVerb = new DesignerVerb(@"Add DateTimePicker", OnAddDateTimePicker);
            _addTrackBarVerb = new DesignerVerb(@"Add TrackBar", OnAddTrackBar);
            _addThemeComboBoxVerb = new DesignerVerb("Add Theme ComboBox", OnAddThemeComboBox);
            _clearItemsVerb = new DesignerVerb(@"Clear Items", OnClearItems);
            _deleteStackVerb = new DesignerVerb(@"Delete Stack", OnDeleteStack);
            _verbs.AddRange(new[]
            {
                _toggleHelpersVerb,
                _moveFirstVerb, 
                _movePrevVerb, 
                _moveNextVerb, 
                _moveLastVerb,
                _addButtonVerb,
                _addCheckBoxVerb,
                _addClusterVerb,
                _addColorButtonVerb,
                _addComboBoxVerb,
                _addCustomControlVerb,
                _addDateTimePickerVerb,
                _addDomainUpDownVerb,
                _addLabelVerb,
                _addNumericUpDownVerb,
                _addMaskedTextBoxVerb,
                _addRadioButtonVerb,
                _addRichTextBoxVerb,
                _addTextBoxVerb,
                _addTrackBarVerb,
                _addThemeComboBoxVerb,
                _clearItemsVerb,
                _deleteStackVerb
            });
        }

        var moveFirst = false;
        var movePrev = false;
        var moveNext = false;
        var moveLast = false;
        var clearItems = false;

        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.Items is not null
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            moveFirst = _ribbonStack.RibbonGroup.Items.IndexOf(_ribbonStack) > 0;
            movePrev = _ribbonStack.RibbonGroup.Items.IndexOf(_ribbonStack) > 0;
            moveNext = _ribbonStack.RibbonGroup.Items.IndexOf(_ribbonStack) < (_ribbonStack.RibbonGroup.Items.Count - 1);
            moveLast = _ribbonStack.RibbonGroup.Items.IndexOf(_ribbonStack) < (_ribbonStack.RibbonGroup.Items.Count - 1);
            clearItems = _ribbonStack.Items.Count > 0;
        }

        _moveFirstVerb.Enabled = moveFirst;
        _movePrevVerb.Enabled = movePrev;
        _moveNextVerb.Enabled = moveNext;
        _moveLastVerb.Enabled = moveLast;
        _clearItemsVerb.Enabled = clearItems;
    }

    private void OnToggleHelpers(object? sender, EventArgs e)
    {
        // Invert the current toggle helper mode
        if (_ribbonStack.Ribbon != null)
        {
            _ribbonStack.Ribbon.InDesignHelperMode = !_ribbonStack.Ribbon.InDesignHelperMode;
        }
    }

    private void OnMoveFirst(object? sender, EventArgs e)
    {
        if ((_ribbonStack?.Ribbon is not null)
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack MoveFirst");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack.RibbonGroup)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the stack
                KryptonRibbonGroup ribbonGroup = _ribbonStack.RibbonGroup;
                ribbonGroup.Items.Remove(_ribbonStack);
                ribbonGroup.Items.Insert(0, _ribbonStack);
                UpdateVerbStatus();

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnMovePrevious(object? sender, EventArgs e)
    {
        if (_ribbonStack is { Ribbon: not null, RibbonGroup: not null }
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack MovePrevious");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack.RibbonGroup)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the triple
                KryptonRibbonGroup ribbonGroup = _ribbonStack.RibbonGroup;
                var index = ribbonGroup.Items.IndexOf(_ribbonStack) - 1;
                index = Math.Max(index, 0);
                ribbonGroup.Items.Remove(_ribbonStack);
                ribbonGroup.Items.Insert(index, _ribbonStack);
                UpdateVerbStatus();

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnMoveNext(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack MoveNext");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack.RibbonGroup)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the triple
                KryptonRibbonGroup ribbonGroup = _ribbonStack.RibbonGroup;
                var index = ribbonGroup.Items.IndexOf(_ribbonStack) + 1;
                index = Math.Min(index, ribbonGroup.Items.Count - 1);
                ribbonGroup.Items.Remove(_ribbonStack);
                ribbonGroup.Items.Insert(index, _ribbonStack);
                UpdateVerbStatus();

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnMoveLast(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack MoveLast");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack.RibbonGroup)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the triple
                KryptonRibbonGroup ribbonGroup = _ribbonStack.RibbonGroup;
                ribbonGroup.Items.Remove(_ribbonStack);
                ribbonGroup.Items.Insert(ribbonGroup.Items.Count, _ribbonStack);
                UpdateVerbStatus();

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddButton(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.Items is not null 
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack AddButton");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a button item
                var button = (KryptonRibbonGroupButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupButton));
                _ribbonStack.Items.Add(button);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddColorButton(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.Items is not null
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack AddColorButton");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a button item
                var button = (KryptonRibbonGroupColorButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupColorButton));
                _ribbonStack.Items.Add(button);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddCheckBox(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.Items is not null
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack AddCheckBox");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a check box item
                var checkBox = (KryptonRibbonGroupCheckBox)_designerHost.CreateComponent(typeof(KryptonRibbonGroupCheckBox));
                _ribbonStack.Items.Add(checkBox);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddRadioButton(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.Items is not null
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack AddRadioButton");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a radio button item
                var radioButton = (KryptonRibbonGroupRadioButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupRadioButton));
                _ribbonStack.Items.Add(radioButton);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddLabel(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.Items is not null
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack AddLabel");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a label item
                var label = (KryptonRibbonGroupLabel)_designerHost.CreateComponent(typeof(KryptonRibbonGroupLabel));
                _ribbonStack.Items.Add(label);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddCustomControl(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.Items is not null
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack AddCustomControl");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a custom control item
                var cc = (KryptonRibbonGroupCustomControl)_designerHost.CreateComponent(typeof(KryptonRibbonGroupCustomControl));
                _ribbonStack.Items.Add(cc);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddTextBox(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.Items is not null
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack AddTextBox");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a textbox item
                var tb = (KryptonRibbonGroupTextBox)_designerHost.CreateComponent(typeof(KryptonRibbonGroupTextBox));
                _ribbonStack.Items.Add(tb);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddMaskedTextBox(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.Items is not null
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack AddMaskedTextBox");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a masked textbox item
                var mtb = (KryptonRibbonGroupMaskedTextBox)_designerHost.CreateComponent(typeof(KryptonRibbonGroupMaskedTextBox));
                _ribbonStack.Items.Add(mtb);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddRichTextBox(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            &&_ribbonStack.Items is not null
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack AddRichTextBox");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a richtextbox item
                var rtb = (KryptonRibbonGroupRichTextBox)_designerHost.CreateComponent(typeof(KryptonRibbonGroupRichTextBox));
                _ribbonStack.Items.Add(rtb);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddComboBox(object? sender, EventArgs e)
    {
        if (_ribbonStack is { Ribbon: not null, Items: not null, RibbonGroup: not null }
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack AddComboBox");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a combobox item
                var cb = (KryptonRibbonGroupComboBox)_designerHost.CreateComponent(typeof(KryptonRibbonGroupComboBox));
                _ribbonStack.Items.Add(cb);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddNumericUpDown(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.Items is not null
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack AddNumericUpDown");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a numeric up-down item
                var nud = (KryptonRibbonGroupNumericUpDown)_designerHost.CreateComponent(typeof(KryptonRibbonGroupNumericUpDown));
                _ribbonStack.Items.Add(nud);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddDomainUpDown(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.Items is not null
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack AddDomainUpDown");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a domain up-down item
                var nud = (KryptonRibbonGroupDomainUpDown)_designerHost.CreateComponent(typeof(KryptonRibbonGroupDomainUpDown));
                _ribbonStack.Items.Add(nud);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddDateTimePicker(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.Items is not null
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack AddDateTimePicker");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a date time picker item
                var nud = (KryptonRibbonGroupDateTimePicker)_designerHost.CreateComponent(typeof(KryptonRibbonGroupDateTimePicker));
                _ribbonStack.Items.Add(nud);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddTrackBar(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.Items is not null
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack AddTrackBar");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a trackbar item
                var tb = (KryptonRibbonGroupTrackBar)_designerHost.CreateComponent(typeof(KryptonRibbonGroupTrackBar));
                _ribbonStack.Items.Add(tb);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnAddThemeComboBox(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.Items is not null
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupStack AddThemeComboBox");

            try
            {
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack)[@"Items"];

                RaiseComponentChanging(propertyItems);

                var tcb = (KryptonRibbonGroupThemeComboBox)_designerHost.CreateComponent(
                    typeof(KryptonRibbonGroupThemeComboBox));

                _ribbonStack.Items.Add(tcb);

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                transaction?.Commit();
            }
        }
    }

    private void OnAddCluster(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.Items is not null
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack AddCluster");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a cluster with buttons
                var cluster = (KryptonRibbonGroupCluster)_designerHost.CreateComponent(typeof(KryptonRibbonGroupCluster));
                _ribbonStack.Items.Add(cluster);

                // Get access to the Cluster.Items property
                MemberDescriptor? propertyClusterItems = TypeDescriptor.GetProperties(cluster)[@"Items"];

                RaiseComponentChanging(propertyClusterItems);

                // Get designer to create three new button components
                var button1 = (KryptonRibbonGroupClusterButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupClusterButton));
                var button2 = (KryptonRibbonGroupClusterButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupClusterButton));
                cluster.Items.Add(button1);
                cluster.Items.Add(button2);

                RaiseComponentChanged(propertyClusterItems, null, null);
                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnClearItems(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.Items is not null
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack ClearItems");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Need access to host in order to delete a component
                var host =(IDesignerHost?)GetService(typeof(IDesignerHost)) ?? ThrowHelper.ThrowNullReferenceException<IDesignerHost>(SharedStaticFunctions.VariableCannotBeNull("host"));

                // We need to remove all the items from the stack group
                for (var i = _ribbonStack.Items.Count - 1; i >= 0; i--)
                {
                    KryptonRibbonGroupItem item = _ribbonStack.Items[i];
                    _ribbonStack.Items.Remove(item);
                    host.DestroyComponent(item);
                }

                RaiseComponentChanged(propertyItems, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnDeleteStack(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack DeleteStack");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonStack.RibbonGroup)[@"Items"];

                // Remove the ribbon group from the ribbon tab
                RaiseComponentChanging(null);
                RaiseComponentChanging(propertyItems);

                // Remove the page from the ribbon
                _ribbonStack.RibbonGroup.Items.Remove(_ribbonStack);

                // Get designer to destroy it
                _designerHost.DestroyComponent(_ribbonStack);

                RaiseComponentChanged(propertyItems, null, null);
                RaiseComponentChanged(null, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnVisible(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            _changeService.OnComponentChanged(_ribbonStack, null, _ribbonStack.Visible, !_ribbonStack.Visible);
            _ribbonStack.Visible = !_ribbonStack.Visible;
        }
    }

    private void OnMaxLarge(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            _changeService.OnComponentChanged(_ribbonStack, null, _ribbonStack.MaximumSize, GroupItemSize.Large);
            _ribbonStack.MaximumSize = GroupItemSize.Large;
        }
    }

    private void OnMaxMedium(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            _changeService.OnComponentChanged(_ribbonStack, null, _ribbonStack.MaximumSize, GroupItemSize.Medium);
            _ribbonStack.MaximumSize = GroupItemSize.Medium;
        }
    }

    private void OnMaxSmall(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            _changeService.OnComponentChanged(_ribbonStack, null, _ribbonStack.MaximumSize, GroupItemSize.Small);
            _ribbonStack.MaximumSize = GroupItemSize.Small;
        }
    }

    private void OnMinLarge(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            _changeService.OnComponentChanged(_ribbonStack, null, _ribbonStack.MinimumSize, GroupItemSize.Large);
            _ribbonStack.MinimumSize = GroupItemSize.Large;
        }
    }

    private void OnMinMedium(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            _changeService.OnComponentChanged(_ribbonStack, null, _ribbonStack.MinimumSize, GroupItemSize.Medium);
            _ribbonStack.MinimumSize = GroupItemSize.Medium;
        }
    }

    private void OnMinSmall(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            _changeService.OnComponentChanged(_ribbonStack, null, _ribbonStack.MinimumSize, GroupItemSize.Small);
            _ribbonStack.MinimumSize = GroupItemSize.Small;
        }
    }

    private void OnComponentChanged(object? sender, ComponentChangedEventArgs e) => UpdateVerbStatus();

    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        // If our component is being removed
        if (e.Component == _ribbonStack)
        {
            // Need access to host in order to delete a component
            var host =(IDesignerHost?)GetService(typeof(IDesignerHost)) ?? ThrowHelper.ThrowNullReferenceException<IDesignerHost>(SharedStaticFunctions.VariableCannotBeNull("host"));

            // We need to remove all items from the stack groups
            if (_ribbonStack.Items is not null)
            {
                for (var j = _ribbonStack.Items.Count - 1 ; j >= 0 ; j--)
                {
                    var item = _ribbonStack.Items[j] as KryptonRibbonGroupItem;
                    _ribbonStack.Items.Remove(item);
                    host.DestroyComponent(item);
                }
            }
        }
    }

    private void OnContextMenu(object? sender, MouseEventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Create the menu strip the first time around
            if (_cms == null)
            {
                _cms = new ContextMenuStrip();
                _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, OnToggleHelpers);
                _visibleMenu = new ToolStripMenuItem("Visible", null, OnVisible);
                _maximumLMenu = new ToolStripMenuItem("Large", null, OnMaxLarge);
                _maximumMMenu = new ToolStripMenuItem("Medium", null, OnMaxMedium);
                _maximumSMenu = new ToolStripMenuItem("Small", null, OnMaxSmall);
                _maximumSizeMenu = new ToolStripMenuItem("Maximum Size");
                _maximumSizeMenu.DropDownItems.AddRange(new ToolStripItem[] { _maximumLMenu, _maximumMMenu, _maximumSMenu });
                _minimumLMenu = new ToolStripMenuItem("Large", null, OnMinLarge);
                _minimumMMenu = new ToolStripMenuItem("Medium", null, OnMinMedium);
                _minimumSMenu = new ToolStripMenuItem("Small", null, OnMinSmall);
                _minimumSizeMenu = new ToolStripMenuItem("Minimum Size");
                _minimumSizeMenu.DropDownItems.AddRange(new ToolStripItem[] { _minimumLMenu, _minimumMMenu, _minimumSMenu });
                _moveFirstMenu = new ToolStripMenuItem("Move Stack First", GenericImageResources.MoveFirst, OnMoveFirst);
                _movePreviousMenu = new ToolStripMenuItem("Move Stack Previous", GenericImageResources.MovePrevious, OnMovePrevious);
                _moveNextMenu = new ToolStripMenuItem("Move Stack Next", GenericImageResources.MoveNext, OnMoveNext);
                _moveLastMenu = new ToolStripMenuItem("Move Stack Last", GenericImageResources.MoveLast, OnMoveLast);
                _moveToGroupMenu = new ToolStripMenuItem("Move Stack To Group");
                _addButtonMenu = new ToolStripMenuItem("Add Button", GenericImageResources.KryptonRibbonGroupButton, OnAddButton);
                _addColorButtonMenu = new ToolStripMenuItem("Add Color Button", GenericImageResources.KryptonRibbonGroupColorButton, OnAddColorButton);
                _addCheckBoxMenu = new ToolStripMenuItem("Add CheckBox", GenericImageResources.KryptonRibbonGroupCheckBox, OnAddCheckBox);
                _addComboBoxMenu = new ToolStripMenuItem("Add ComboBox", GenericImageResources.KryptonRibbonGroupComboBox, OnAddComboBox);
                _addCustomControlMenu = new ToolStripMenuItem("Add Custom Control", GenericImageResources.KryptonRibbonGroupCustomControl, OnAddCustomControl);
                _addClusterMenu = new ToolStripMenuItem("Add Cluster", GenericImageResources.KryptonRibbonGroupCluster, OnAddCluster);
                _addDateTimePickerMenu = new ToolStripMenuItem("Add DateTimePicker", GenericImageResources.KryptonRibbonGroupDateTimePicker, OnAddDateTimePicker);
                _addDomainUpDownMenu = new ToolStripMenuItem("Add DomainUpDown", GenericImageResources.KryptonRibbonGroupDomainUpDown, OnAddDomainUpDown);
                _addLabelMenu = new ToolStripMenuItem("Add Label", GenericImageResources.KryptonRibbonGroupLabel, OnAddLabel);
                _addMaskedTextBoxMenu = new ToolStripMenuItem("Add MaskedTextBox", GenericImageResources.KryptonRibbonGroupMaskedTextBox, OnAddMaskedTextBox);
                _addNumericUpDownMenu = new ToolStripMenuItem("Add NumericUpDown", GenericImageResources.KryptonRibbonGroupNumericUpDown, OnAddNumericUpDown);
                _addRadioButtonMenu = new ToolStripMenuItem("Add RadioButton", GenericImageResources.KryptonRibbonGroupRadioButton, OnAddRadioButton);
                _addRichTextBoxMenu = new ToolStripMenuItem("Add RichTextBox", GenericImageResources.KryptonRibbonGroupRichTextBox, OnAddRichTextBox);
                _addTextBoxMenu = new ToolStripMenuItem("Add TextBox", GenericImageResources.KryptonRibbonGroupTextBox, OnAddTextBox);
                _addTrackBarMenu = new ToolStripMenuItem("Add TrackBar", GenericImageResources.KryptonRibbonGroupTrackBar, OnAddTrackBar);
                _addThemeComboBoxMenu = new ToolStripMenuItem("Add ThemeComboBox", GenericImageResources.KryptonRibbonGroupComboBox, OnAddThemeComboBox);
                _clearItemsMenu = new ToolStripMenuItem("Clear Items", null, OnClearItems);
                _deleteStackMenu = new ToolStripMenuItem("Delete Stack", GenericImageResources.Delete, OnDeleteStack);
                _cms.Items.AddRange(new ToolStripItem[]
                {
                    _toggleHelpersMenu, new ToolStripSeparator(),
                    _visibleMenu, 
                    _maximumSizeMenu,
                    _minimumSizeMenu, new ToolStripSeparator(),
                    _moveFirstMenu, 
                    _movePreviousMenu,
                    _moveNextMenu, 
                    _moveLastMenu, new ToolStripSeparator(),
                    _moveToGroupMenu, new ToolStripSeparator(),
                    _addButtonMenu,
                    _addCheckBoxMenu,
                    _addClusterMenu,
                    _addColorButtonMenu,
                    _addComboBoxMenu,
                    _addCustomControlMenu,
                    _addDateTimePickerMenu,
                    _addDomainUpDownMenu,
                    _addLabelMenu,
                    _addMaskedTextBoxMenu,
                    _addNumericUpDownMenu,
                    _addRadioButtonMenu,
                    _addRichTextBoxMenu,
                    _addTextBoxMenu,
                    _addTrackBarMenu,
                    _addThemeComboBoxMenu,
                    new ToolStripSeparator(),
                    _clearItemsMenu, new ToolStripSeparator(),
                    _deleteStackMenu
                });

                // Ensure add images have correct transparent background
                _addButtonMenu.ImageTransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
                _addColorButtonMenu.ImageTransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
                _addCheckBoxMenu.ImageTransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
                _addRadioButtonMenu.ImageTransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
                _addLabelMenu.ImageTransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
                _addCustomControlMenu.ImageTransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
                _addClusterMenu.ImageTransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
                _addTextBoxMenu.ImageTransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
                _addMaskedTextBoxMenu.ImageTransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
                _addRichTextBoxMenu.ImageTransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
                _addComboBoxMenu.ImageTransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
                _addNumericUpDownMenu.ImageTransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
                _addDomainUpDownMenu.ImageTransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
                _addDateTimePickerMenu.ImageTransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
                _addTrackBarMenu.ImageTransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
                _addThemeComboBoxMenu.ImageTransparentColor = SharedStaticVariables.TRANSPARENCY_KEY_COLOR;
            }

            // Update verbs to work out correct enable states
            UpdateVerbStatus();

            // Update sub menu options available for the 'Move To Group'
            UpdateMoveToGroup();

            // Update menu items state from versb
            _toggleHelpersMenu.Checked = _ribbonStack.Ribbon.InDesignHelperMode;
            _visibleMenu.Checked = _ribbonStack.Visible;
            _maximumLMenu.Checked = _ribbonStack.MaximumSize == GroupItemSize.Large;
            _maximumMMenu.Checked = _ribbonStack.MaximumSize == GroupItemSize.Medium;
            _maximumSMenu.Checked = _ribbonStack.MaximumSize == GroupItemSize.Small;
            _minimumLMenu.Checked = _ribbonStack.MinimumSize == GroupItemSize.Large;
            _minimumMMenu.Checked = _ribbonStack.MinimumSize == GroupItemSize.Medium;
            _minimumSMenu.Checked = _ribbonStack.MinimumSize == GroupItemSize.Small;
            _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
            _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
            _moveNextMenu.Enabled = _moveNextVerb.Enabled;
            _moveLastMenu.Enabled = _moveLastVerb.Enabled;
            _moveToGroupMenu.Enabled = _moveToGroupMenu.DropDownItems.Count > 0;
            _clearItemsMenu.Enabled = _clearItemsVerb.Enabled;

            // Show the context menu
            if (CommonHelper.ValidContextMenuStrip(_cms))
            {
                Point screenPt = _ribbonStack.Ribbon.ViewRectangleToPoint(_ribbonStack.StackView!);
                VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
            }
        }
    }

    private void UpdateMoveToGroup()
    {
        // Remove any existing child items
        _moveToGroupMenu.DropDownItems.Clear();

        if (_ribbonStack.Ribbon is not null && _ribbonStack.RibbonTab is not null)
        {
            // Create a new item per group in the same ribbon tab
            foreach (KryptonRibbonGroup group in _ribbonStack.RibbonTab.Groups)
            {
                // Cannot move to ourself, so ignore outself
                if (group != _ribbonStack.RibbonGroup)
                {
                    // Create menu item for the group
                    var groupMenuItem = new ToolStripMenuItem
                    {
                        Text = $"{group.TextLine1} {group.TextLine2}",
                        Tag = group
                    };

                    // Hook into selection of the menu item
                    groupMenuItem.Click += OnMoveToGroup;

                    // Add to end of the list of options
                    _moveToGroupMenu.DropDownItems.Add(groupMenuItem);
                }
            }
        }
    }

    private void OnMoveToGroup(object? sender, EventArgs e)
    {
        if ((_ribbonStack.Ribbon is not null)
            && _ribbonStack.RibbonGroup is not null
            && _ribbonStack.RibbonGroup.Items.Contains(_ribbonStack))
        {
            // Cast to correct type
            var groupMenuItem =sender as ToolStripMenuItem ?? ThrowHelper.ThrowArgumentNullException(sender as ToolStripMenuItem, nameof(sender));

            // Get access to the destination tab
            var destination =groupMenuItem.Tag as KryptonRibbonGroup ?? ThrowHelper.ThrowNullReferenceException<KryptonRibbonGroup>(SharedStaticFunctions.VariableCannotBeNull("destination"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupStack MoveStackToGroup");

            try
            {
                // Get access to the Groups property
                MemberDescriptor? oldItems = TypeDescriptor.GetProperties(_ribbonStack.RibbonGroup)[@"Items"];
                MemberDescriptor? newItems = TypeDescriptor.GetProperties(destination)[@"Items"];

                // Remove the ribbon tab from the ribbon
                RaiseComponentChanging(null);
                RaiseComponentChanging(oldItems);
                RaiseComponentChanging(newItems);

                // Remove group from current group
                _ribbonStack.RibbonGroup.Items.Remove(_ribbonStack);

                // Append to the new destination group
                destination.Items.Add(_ribbonStack);

                RaiseComponentChanged(newItems, null, null);
                RaiseComponentChanged(oldItems, null, null);
                RaiseComponentChanged(null, null, null);
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }
    #endregion
}