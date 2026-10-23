#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

internal class KryptonRibbonGroupTripleDesigner : ComponentDesigner
{
    #region Instance Fields
    private IDesignerHost _designerHost;
    private IComponentChangeService _changeService;
    private KryptonRibbonGroupTriple _ribbonTriple;
    private DesignerVerbCollection? _verbs;
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
    private DesignerVerb _deleteTripleVerb;
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
    private ToolStripMenuItem _deleteTripleMenu;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonGroupTripleDesigner class.
    /// </summary>
    public KryptonRibbonGroupTripleDesigner()
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

        Debug.Assert(component != null);

        // Cast to correct type
        _ribbonTriple = component as KryptonRibbonGroupTriple ?? throw new ArgumentNullException(nameof(component));
        if (_ribbonTriple != null)
        {
            _ribbonTriple.DesignTimeAddButton += OnAddButton;
            _ribbonTriple.DesignTimeAddColorButton += OnAddColorButton;
            _ribbonTriple.DesignTimeAddCheckBox += OnAddCheckBox;
            _ribbonTriple.DesignTimeAddRadioButton += OnAddRadioButton;
            _ribbonTriple.DesignTimeAddLabel += OnAddLabel;
            _ribbonTriple.DesignTimeAddCustomControl += OnAddCustomControl;
            _ribbonTriple.DesignTimeAddTextBox += OnAddTextBox;
            _ribbonTriple.DesignTimeAddMaskedTextBox += OnAddMaskedTextBox;
            _ribbonTriple.DesignTimeAddRichTextBox += OnAddRichTextBox;
            _ribbonTriple.DesignTimeAddComboBox += OnAddComboBox;
            _ribbonTriple.DesignTimeAddNumericUpDown += OnAddNumericUpDown;
            _ribbonTriple.DesignTimeAddDomainUpDown += OnAddDomainUpDown;
            _ribbonTriple.DesignTimeAddDateTimePicker += OnAddDateTimePicker;
            _ribbonTriple.DesignTimeAddTrackBar += OnAddTrackBar;
            _ribbonTriple.DesignTimeAddThemeComboBox += OnAddThemeComboBox;
            _ribbonTriple.DesignTimeContextMenu += OnContextMenu;
        }

        // Get access to the services
        _designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_designerHost)));
        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(_changeService)));

        // We need to know when we are being removed/changed
        _changeService.ComponentRemoving += OnComponentRemoving;
        _changeService.ComponentChanged += OnComponentChanged;
    }

    /// <summary>
    /// Gets the collection of components associated with the component managed by the designer.
    /// </summary>
    public override ICollection AssociatedComponents
    {
        get
        {
            var compound = new ArrayList(base.AssociatedComponents);

            if (_ribbonTriple.Items is not null)
            {
                compound.AddRange(_ribbonTriple.Items);
            }

            return compound;
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
            return _verbs!;
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
                _ribbonTriple.DesignTimeAddButton -= OnAddButton;
                _ribbonTriple.DesignTimeAddColorButton -= OnAddColorButton;
                _ribbonTriple.DesignTimeAddCheckBox -= OnAddCheckBox;
                _ribbonTriple.DesignTimeAddRadioButton -= OnAddRadioButton;
                _ribbonTriple.DesignTimeAddLabel -= OnAddLabel;
                _ribbonTriple.DesignTimeAddCustomControl -= OnAddCustomControl;
                _ribbonTriple.DesignTimeAddTextBox -= OnAddTextBox;
                _ribbonTriple.DesignTimeAddMaskedTextBox -= OnAddMaskedTextBox;
                _ribbonTriple.DesignTimeAddRichTextBox -= OnAddRichTextBox;
                _ribbonTriple.DesignTimeAddComboBox -= OnAddComboBox;
                _ribbonTriple.DesignTimeAddNumericUpDown -= OnAddNumericUpDown;
                _ribbonTriple.DesignTimeAddDomainUpDown -= OnAddDomainUpDown;
                _ribbonTriple.DesignTimeAddDateTimePicker -= OnAddDateTimePicker;
                _ribbonTriple.DesignTimeAddTrackBar -= OnAddTrackBar;
                _ribbonTriple.DesignTimeAddThemeComboBox -= OnAddThemeComboBox;
                _ribbonTriple.DesignTimeContextMenu -= OnContextMenu;
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
        if (_verbs is null)
        {
            _verbs = [];
            _toggleHelpersVerb = new DesignerVerb(@"Toggle Helpers", OnToggleHelpers);
            _moveFirstVerb = new DesignerVerb(@"Move Triple First", OnMoveFirst);
            _movePrevVerb = new DesignerVerb(@"Move Triple Previous", OnMovePrevious);
            _moveNextVerb = new DesignerVerb(@"Move Triple Next", OnMoveNext);
            _moveLastVerb = new DesignerVerb(@"Move Triple Last", OnMoveLast);
            _addButtonVerb = new DesignerVerb(@"Add Button", OnAddButton);
            _addColorButtonVerb = new DesignerVerb(@"Add Color Button", OnAddColorButton);
            _addCheckBoxVerb = new DesignerVerb(@"Add CheckBox", OnAddCheckBox);
            _addRadioButtonVerb = new DesignerVerb(@"Add RadioButton", OnAddRadioButton);
            _addLabelVerb = new DesignerVerb(@"Add Label", OnAddLabel);
            _addCustomControlVerb = new DesignerVerb(@"Add Custom Control", OnAddCustomControl);
            _addTextBoxVerb = new DesignerVerb(@"Add TextBox", OnAddTextBox);
            _addMaskedTextBoxVerb = new DesignerVerb(@"Add MaskedTextBox", OnAddMaskedTextBox);
            _addRichTextBoxVerb = new DesignerVerb(@"Add RichTextBox", OnAddRichTextBox);
            _addComboBoxVerb = new DesignerVerb(@"Add ComboBox", OnAddComboBox);
            _addNumericUpDownVerb = new DesignerVerb(@"Add NumericUpDown", OnAddNumericUpDown);
            _addDomainUpDownVerb = new DesignerVerb(@"Add DomainUpDown", OnAddDomainUpDown);
            _addDateTimePickerVerb = new DesignerVerb(@"Add DateTimePicker", OnAddDateTimePicker);
            _addTrackBarVerb = new DesignerVerb(@"Add TrackBar", OnAddTrackBar);
            _addThemeComboBoxVerb = new DesignerVerb(@"Add ThemeComboBox", OnAddThemeComboBox);
            _clearItemsVerb = new DesignerVerb(@"Clear Items", OnClearItems);
            _deleteTripleVerb = new DesignerVerb(@"Delete Triple", OnDeleteTriple);
            _verbs.AddRange(new[]
            {
                _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb, _moveNextVerb, _moveLastVerb,
                _addButtonVerb,
                _addCheckBoxVerb,
                _addColorButtonVerb,
                _addComboBoxVerb,
                _addCustomControlVerb,
                _addDateTimePickerVerb,
                _addDomainUpDownVerb,
                _addLabelVerb,
                _addMaskedTextBoxVerb,
                _addNumericUpDownVerb,
                _addRadioButtonVerb,
                _addRichTextBoxVerb,
                _addTextBoxVerb,
                _addTrackBarVerb,
                _addThemeComboBoxVerb,
                _clearItemsVerb, _deleteTripleVerb
            });
        }

        var moveFirst = false;
        var movePrev = false;
        var moveNext = false;
        var moveLast = false;
        var add = false;
        var clearItems = false;

        if ((_ribbonTriple.RibbonGroup is not null) 
            && _ribbonTriple.Items is not null
            && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            moveFirst = _ribbonTriple.RibbonGroup.Items.IndexOf(_ribbonTriple) > 0;
            movePrev = _ribbonTriple.RibbonGroup.Items.IndexOf(_ribbonTriple) > 0;
            moveNext = _ribbonTriple.RibbonGroup.Items.IndexOf(_ribbonTriple) < (_ribbonTriple.RibbonGroup.Items.Count - 1);
            moveLast = _ribbonTriple.RibbonGroup.Items.IndexOf(_ribbonTriple) < (_ribbonTriple.RibbonGroup.Items.Count - 1);
            add = _ribbonTriple.Items.Count < 3;
            clearItems = _ribbonTriple.Items.Count > 0;
        }

        _moveFirstVerb.Enabled = moveFirst;
        _movePrevVerb.Enabled = movePrev;
        _moveNextVerb.Enabled = moveNext;
        _moveLastVerb.Enabled = moveLast;
        _addButtonVerb.Enabled = add;
        _addColorButtonVerb.Enabled = add;
        _addCheckBoxVerb.Enabled = add;
        _addRadioButtonVerb.Enabled = add;
        _addLabelVerb.Enabled = add;
        _addCustomControlVerb.Enabled = add;
        _addTextBoxVerb.Enabled = add;
        _addMaskedTextBoxVerb.Enabled = add;
        _addRichTextBoxVerb.Enabled = add;
        _addComboBoxVerb.Enabled = add;
        _addNumericUpDownVerb.Enabled = add;
        _addDomainUpDownVerb.Enabled = add;
        _addDateTimePickerVerb.Enabled = add;
        _addTrackBarVerb.Enabled = add;
        _addTextBoxVerb.Enabled = add;
        _clearItemsVerb.Enabled = clearItems;
    }

    private void OnToggleHelpers(object? sender, EventArgs e)
    {
        // Invert the current toggle helper mode
        if (_ribbonTriple.Ribbon is not null)
        {
            _ribbonTriple.Ribbon.InDesignHelperMode = !_ribbonTriple.Ribbon.InDesignHelperMode;
        }
    }

    private void OnMoveFirst(object? sender, EventArgs e)
    {
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple MoveFirst");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple.RibbonGroup)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the triple
                KryptonRibbonGroup ribbonGroup = _ribbonTriple.RibbonGroup;
                ribbonGroup.Items.Remove(_ribbonTriple);
                ribbonGroup.Items.Insert(0, _ribbonTriple);
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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple MovePrevious");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple.RibbonGroup)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the triple
                KryptonRibbonGroup ribbonGroup = _ribbonTriple.RibbonGroup;
                var index = ribbonGroup.Items.IndexOf(_ribbonTriple) - 1;
                index = Math.Max(index, 0);
                ribbonGroup.Items.Remove(_ribbonTriple);
                ribbonGroup.Items.Insert(index, _ribbonTriple);
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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple MoveNext");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple.RibbonGroup)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the triple
                KryptonRibbonGroup ribbonGroup = _ribbonTriple.RibbonGroup;
                var index = ribbonGroup.Items.IndexOf(_ribbonTriple) + 1;
                index = Math.Min(index, ribbonGroup.Items.Count - 1);
                ribbonGroup.Items.Remove(_ribbonTriple);
                ribbonGroup.Items.Insert(index, _ribbonTriple);
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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple MoveLast");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple.RibbonGroup)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Move position of the triple
                KryptonRibbonGroup ribbonGroup = _ribbonTriple.RibbonGroup;
                ribbonGroup.Items.Remove(_ribbonTriple);
                ribbonGroup.Items.Insert(ribbonGroup.Items.Count, _ribbonTriple);
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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple AddButton");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a button item
                var button = (KryptonRibbonGroupButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupButton));
                _ribbonTriple.Items!.Add(button);

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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple AddColorButton");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a button item
                var button = (KryptonRibbonGroupColorButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupColorButton));
                _ribbonTriple.Items!.Add(button);

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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple AddCheckBox");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a check box item.
                var checkBox = (KryptonRibbonGroupCheckBox)_designerHost.CreateComponent(typeof(KryptonRibbonGroupCheckBox));
                _ribbonTriple.Items!.Add(checkBox);

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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple AddRadioButton");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a radio button item.
                var radioButton = (KryptonRibbonGroupRadioButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupRadioButton));
                _ribbonTriple.Items!.Add(radioButton);

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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple AddLabel");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a label item
                var label = (KryptonRibbonGroupLabel)_designerHost.CreateComponent(typeof(KryptonRibbonGroupLabel));
                _ribbonTriple.Items!.Add(label);

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
        if ((_ribbonTriple.RibbonGroup != null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple AddCustomControl");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a custom control item
                var cc = (KryptonRibbonGroupCustomControl)_designerHost.CreateComponent(typeof(KryptonRibbonGroupCustomControl));
                _ribbonTriple.Items!.Add(cc);

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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple AddTextBox");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a textbox item
                var tb = (KryptonRibbonGroupTextBox)_designerHost.CreateComponent(typeof(KryptonRibbonGroupTextBox));
                _ribbonTriple.Items!.Add(tb);

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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple AddTrackBar");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a trackbar item
                var tb = (KryptonRibbonGroupTrackBar)_designerHost.CreateComponent(typeof(KryptonRibbonGroupTrackBar));
                _ribbonTriple.Items!.Add(tb);

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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple AddThemeComboBox");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a themecombobox item
                var tcb = (KryptonRibbonGroupThemeComboBox)_designerHost.CreateComponent(typeof(KryptonRibbonGroupThemeComboBox));
                _ribbonTriple.Items!.Add(tcb);

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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple AddMaskedTextBox");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a textbox item
                var mtb = (KryptonRibbonGroupMaskedTextBox)_designerHost.CreateComponent(typeof(KryptonRibbonGroupMaskedTextBox));
                _ribbonTriple.Items!.Add(mtb);

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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple AddRichTextBox");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a richtextbox item
                var rtb = (KryptonRibbonGroupRichTextBox)_designerHost.CreateComponent(typeof(KryptonRibbonGroupRichTextBox));
                _ribbonTriple.Items!.Add(rtb);

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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple AddComboBox");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a combobox item
                var cc = (KryptonRibbonGroupComboBox)_designerHost.CreateComponent(typeof(KryptonRibbonGroupComboBox));
                _ribbonTriple.Items!.Add(cc);

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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple AddNumericUpDown");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a numeric up-down item
                var cc = (KryptonRibbonGroupNumericUpDown)_designerHost.CreateComponent(typeof(KryptonRibbonGroupNumericUpDown));
                _ribbonTriple.Items!.Add(cc);

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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple AddDomainUpDown");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a domain up-down item
                var cc = (KryptonRibbonGroupDomainUpDown)_designerHost.CreateComponent(typeof(KryptonRibbonGroupDomainUpDown));
                _ribbonTriple.Items!.Add(cc);

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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple AddDateTimePicker");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)[@"Items"];

                RaiseComponentChanging(propertyItems);

                // Get designer to create a domain up-down item
                var cc = (KryptonRibbonGroupDateTimePicker)_designerHost.CreateComponent(typeof(KryptonRibbonGroupDateTimePicker));
                _ribbonTriple.Items!.Add(cc);

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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple ClearItems");

            try
            {
                if (_ribbonTriple.Items is not null)
                {
                    // Get access to the Items property
                    MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)[@"Items"];

                    RaiseComponentChanging(propertyItems);

                    // Need access to host in order to delete a component
                    var host = (IDesignerHost?)GetService(typeof(IDesignerHost)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("host"));

                    // We need to remove all the items from the triple group
                    for (var i = _ribbonTriple.Items.Count - 1 ; i >= 0 ; i--)
                    {
                        KryptonRibbonGroupItem item = _ribbonTriple.Items[i];
                        _ribbonTriple.Items.Remove(item);
                        host.DestroyComponent(item);
                    }

                    RaiseComponentChanged(propertyItems, null, null);
                }
            }
            finally
            {
                // If we managed to create the transaction, then do it
                transaction?.Commit();
            }
        }
    }

    private void OnDeleteTriple(object? sender, EventArgs e)
    {
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple DeleteTriple");

            try
            {
                // Get access to the Items property
                MemberDescriptor? propertyItems = TypeDescriptor.GetProperties(_ribbonTriple.RibbonGroup)[@"Items"];

                // Remove the ribbon group from the ribbon tab
                RaiseComponentChanging(null);
                RaiseComponentChanging(propertyItems);

                // Remove the triple from the group
                _ribbonTriple.RibbonGroup.Items.Remove(_ribbonTriple);

                // Get designer to destroy it
                _designerHost.DestroyComponent(_ribbonTriple);

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
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            _changeService.OnComponentChanged(_ribbonTriple, null, _ribbonTriple.Visible, !_ribbonTriple.Visible);
            _ribbonTriple.Visible = !_ribbonTriple.Visible;
        }
    }

    private void OnMaxLarge(object? sender, EventArgs e)
    {
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            _changeService.OnComponentChanged(_ribbonTriple, null, _ribbonTriple.MaximumSize, GroupItemSize.Large);
            _ribbonTriple.MaximumSize = GroupItemSize.Large;
        }
    }

    private void OnMaxMedium(object? sender, EventArgs e)
    {
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            _changeService.OnComponentChanged(_ribbonTriple, null, _ribbonTriple.MaximumSize, GroupItemSize.Medium);
            _ribbonTriple.MaximumSize = GroupItemSize.Medium;
        }
    }

    private void OnMaxSmall(object? sender, EventArgs e)
    {
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            _changeService.OnComponentChanged(_ribbonTriple, null, _ribbonTriple.MaximumSize, GroupItemSize.Small);
            _ribbonTriple.MaximumSize = GroupItemSize.Small;
        }
    }

    private void OnMinLarge(object? sender, EventArgs e)
    {
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            _changeService.OnComponentChanged(_ribbonTriple, null, _ribbonTriple.MinimumSize, GroupItemSize.Large);
            _ribbonTriple.MinimumSize = GroupItemSize.Large;
        }
    }

    private void OnMinMedium(object? sender, EventArgs e)
    {
        if ((_ribbonTriple.RibbonGroup is not null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            _changeService.OnComponentChanged(_ribbonTriple, null, _ribbonTriple.MinimumSize, GroupItemSize.Medium);
            _ribbonTriple.MinimumSize = GroupItemSize.Medium;
        }
    }

    private void OnMinSmall(object? sender, EventArgs e)
    {
        if ((_ribbonTriple.RibbonGroup != null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            _changeService.OnComponentChanged(_ribbonTriple, null, _ribbonTriple.MinimumSize, GroupItemSize.Small);
            _ribbonTriple.MinimumSize = GroupItemSize.Small;
        }
    }

    private void OnComponentChanged(object? sender, ComponentChangedEventArgs e) => UpdateVerbStatus();

    private void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        // If our triple is being removed
        if (e.Component == _ribbonTriple && _ribbonTriple.Items is not null)
        {
            // Need access to host in order to delete a component
            var host = (IDesignerHost?)GetService(typeof(IDesignerHost)) ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("host"));

            // We need to remove all items from the triple group
            for (var j = _ribbonTriple.Items.Count - 1; j >= 0; j--)
            {
                var item = _ribbonTriple.Items[j] as KryptonRibbonGroupItem;
                _ribbonTriple.Items.Remove(item);
                host.DestroyComponent(item);
            }
        }
    }

    private void OnContextMenu(object? sender, MouseEventArgs e)
    {
        if ((_ribbonTriple.RibbonGroup != null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
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
                _moveFirstMenu = new ToolStripMenuItem("Move Triple First", GenericImageResources.MoveFirst, OnMoveFirst);
                _movePreviousMenu = new ToolStripMenuItem("Move Triple Previous", GenericImageResources.MovePrevious, OnMovePrevious);
                _moveNextMenu = new ToolStripMenuItem("Move Triple Next", GenericImageResources.MoveNext, OnMoveNext);
                _moveLastMenu = new ToolStripMenuItem("Move Triple Last", GenericImageResources.MoveLast, OnMoveLast);
                _moveToGroupMenu = new ToolStripMenuItem("Move Triple To Group");
                _addButtonMenu = new ToolStripMenuItem("Add Button", GenericImageResources.KryptonRibbonGroupButton, OnAddButton);
                _addColorButtonMenu = new ToolStripMenuItem("Add Color Button", GenericImageResources.KryptonRibbonGroupColorButton, OnAddColorButton);
                _addCheckBoxMenu = new ToolStripMenuItem("Add CheckBox", GenericImageResources.KryptonRibbonGroupCheckBox, OnAddCheckBox);
                _addComboBoxMenu = new ToolStripMenuItem("Add ComboBox", GenericImageResources.KryptonRibbonGroupComboBox, OnAddComboBox);
                _addCustomControlMenu = new ToolStripMenuItem("Add Custom Control", GenericImageResources.KryptonRibbonGroupCustomControl, OnAddCustomControl);
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
                _deleteTripleMenu = new ToolStripMenuItem("Delete Triple", GenericImageResources.Delete, OnDeleteTriple);
                _cms.Items.AddRange(new ToolStripItem[]
                {
                    _toggleHelpersMenu, new ToolStripSeparator(),
                    _visibleMenu, _maximumSizeMenu, _minimumSizeMenu, new ToolStripSeparator(),
                    _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                    _moveToGroupMenu, new ToolStripSeparator(),
                    _addButtonMenu,
                    _addCheckBoxMenu,
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
                    _deleteTripleMenu
                });

                // Ensure add images have correct transparent background
                _addButtonMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addColorButtonMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addCheckBoxMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addRadioButtonMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addLabelMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addCustomControlMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addTextBoxMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addMaskedTextBoxMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addRichTextBoxMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addComboBoxMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addNumericUpDownMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addDomainUpDownMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addDateTimePickerMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addTrackBarMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
                _addThemeComboBoxMenu.ImageTransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR;
            }

            // Update verbs to work out correct enable states
            UpdateVerbStatus();

            // Update sub menu options available for the 'Move To Group'
            UpdateMoveToGroup();

            // Update menu items state from verb
            _toggleHelpersMenu.Checked = _ribbonTriple.Ribbon!.InDesignHelperMode;
            _visibleMenu.Checked = _ribbonTriple.Visible;
            _maximumLMenu.Checked = _ribbonTriple.MaximumSize == GroupItemSize.Large;
            _maximumMMenu.Checked = _ribbonTriple.MaximumSize == GroupItemSize.Medium;
            _maximumSMenu.Checked = _ribbonTriple.MaximumSize == GroupItemSize.Small;
            _minimumLMenu.Checked = _ribbonTriple.MinimumSize == GroupItemSize.Large;
            _minimumMMenu.Checked = _ribbonTriple.MinimumSize == GroupItemSize.Medium;
            _minimumSMenu.Checked = _ribbonTriple.MinimumSize == GroupItemSize.Small;
            _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
            _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
            _moveNextMenu.Enabled = _moveNextVerb.Enabled;
            _moveLastMenu.Enabled = _moveLastVerb.Enabled;
            _moveToGroupMenu.Enabled = _moveToGroupMenu.DropDownItems.Count > 0;
            _addButtonMenu.Enabled = _addButtonVerb.Enabled;
            _addColorButtonMenu.Enabled = _addColorButtonVerb.Enabled;
            _addCheckBoxMenu.Enabled = _addCheckBoxVerb.Enabled;
            _addRadioButtonMenu.Enabled = _addRadioButtonVerb.Enabled;
            _addLabelMenu.Enabled = _addLabelVerb.Enabled;
            _addCustomControlMenu.Enabled = _addCustomControlVerb.Enabled;
            _addTextBoxMenu.Enabled = _addTextBoxVerb.Enabled;
            _addMaskedTextBoxMenu.Enabled = _addMaskedTextBoxVerb.Enabled;
            _addRichTextBoxMenu.Enabled = _addRichTextBoxVerb.Enabled;
            _addComboBoxMenu.Enabled = _addComboBoxVerb.Enabled;
            _addNumericUpDownMenu.Enabled = _addNumericUpDownVerb.Enabled;
            _addDomainUpDownMenu.Enabled = _addDomainUpDownVerb.Enabled;
            _addDateTimePickerMenu.Enabled = _addDateTimePickerVerb.Enabled;
            _addTrackBarMenu.Enabled = _addTrackBarVerb.Enabled;
            _addThemeComboBoxMenu.Enabled = _addThemeComboBoxVerb.Enabled;
            _clearItemsMenu.Enabled = _clearItemsVerb.Enabled;

            // Show the context menu
            if (CommonHelper.ValidContextMenuStrip(_cms))
            {
                Point screenPt = _ribbonTriple.Ribbon.ViewRectangleToPoint(_ribbonTriple.TripleView);
                VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
            }
        }
    }

    private void UpdateMoveToGroup()
    {
        // Remove any existing child items
        _moveToGroupMenu.DropDownItems.Clear();

        if (_ribbonTriple.Ribbon is not null && _ribbonTriple.RibbonTab is not null)
        {
            // Create a new item per group in the same ribbon tab
            foreach (KryptonRibbonGroup group in _ribbonTriple.RibbonTab.Groups)
            {
                // Cannot move to ourself, so ignore outself
                if (group != _ribbonTriple.RibbonGroup)
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
        if ((_ribbonTriple.RibbonGroup != null) && _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
        {
            // Cast to correct type
            var groupMenuItem = sender as ToolStripMenuItem ?? throw new ArgumentNullException(nameof(sender));

            // Get access to the destination tab
            var destination = groupMenuItem.Tag as KryptonRibbonGroup ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("destination"));

            // Use a transaction to support undo/redo actions
            DesignerTransaction transaction = _designerHost.CreateTransaction(@"KryptonRibbonGroupTriple MoveTripleToGroup");

            try
            {
                // Get access to the Groups property
                MemberDescriptor? oldItems = TypeDescriptor.GetProperties(_ribbonTriple.RibbonGroup)[@"Items"];
                MemberDescriptor? newItems = TypeDescriptor.GetProperties(destination)[@"Items"];

                // Remove the ribbon tab from the ribbon
                RaiseComponentChanging(null);
                RaiseComponentChanging(oldItems);
                RaiseComponentChanging(newItems);

                // Remove group from current group
                _ribbonTriple.RibbonGroup.Items.Remove(_ribbonTriple);

                // Append to the new destination group
                destination.Items.Add(_ribbonTriple);

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