#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides a user interface for navigating and manipulating data bound to a data source.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(BindingNavigator))]
[DefaultEvent(nameof(RefreshItems))]
[DefaultProperty(nameof(BindingSource))]
[DesignerCategory(@"code")]
[Description(@"Provides a user interface for navigating and manipulating data bound to a data source.")]
public class KryptonBindingNavigator : UserControl
{
    #region Instance Fields
    
    private BindingSource? _bindingSource;
    private readonly KryptonButton? _moveFirstButton;
    private readonly KryptonButton? _movePreviousButton;
    private readonly KryptonButton? _moveNextButton;
    private readonly KryptonButton? _moveLastButton;
    private readonly KryptonButton? _addNewButton;
    private readonly KryptonButton? _deleteButton;
    private readonly KryptonTextBox? _positionTextBox;
    private readonly KryptonLabel? _countLabel;
    private bool _addNewItemEnabled;
    private bool _deleteItemEnabled;
    private const int ButtonWidth = 30;
    private const int ButtonHeight = 25;
    private const int Spacing = 4;
    
    #endregion

    #region Events
    
    /// <summary>
    /// Occurs when RefreshItems is called to refresh the state of the items.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when RefreshItems is called to refresh the state of the items.")]
    public event EventHandler? RefreshItems;

    /// <summary>
    /// Raises the RefreshItems event.
    /// </summary>
    protected virtual void OnRefreshItems(EventArgs e) => RefreshItems?.Invoke(this, e);
    
    #endregion

    #region Identity
    
    /// <summary>
    /// Initialize a new instance of the KryptonBindingNavigator class.
    /// </summary>
    public KryptonBindingNavigator()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw, true);

        _addNewItemEnabled = true;
        _deleteItemEnabled = true;

        // Set default size
        Height = ButtonHeight + 8;

        // Create navigation buttons
        _moveFirstButton = new KryptonButton
        {
            Text = @"|<",
            AutoSize = false,
            Size = new Size(ButtonWidth, ButtonHeight)
        };
        _moveFirstButton.Click += OnMoveFirst;

        _movePreviousButton = new KryptonButton
        {
            Text = @"<",
            AutoSize = false,
            Size = new Size(ButtonWidth, ButtonHeight)
        };
        _movePreviousButton.Click += OnMovePrevious;

        _moveNextButton = new KryptonButton
        {
            Text = @">",
            AutoSize = false,
            Size = new Size(ButtonWidth, ButtonHeight)
        };
        _moveNextButton.Click += OnMoveNext;

        _moveLastButton = new KryptonButton
        {
            Text = @">|",
            AutoSize = false,
            Size = new Size(ButtonWidth, ButtonHeight)
        };
        _moveLastButton.Click += OnMoveLast;

        _addNewButton = new KryptonButton
        {
            Text = @"+",
            AutoSize = false,
            Size = new Size(ButtonWidth, ButtonHeight)
        };
        _addNewButton.Click += OnAddNew;

        _deleteButton = new KryptonButton
        {
            Text = @"×",
            AutoSize = false,
            Size = new Size(ButtonWidth, ButtonHeight)
        };
        _deleteButton.Click += OnDelete;

        // Create position textbox
        _positionTextBox = new KryptonTextBox
        {
            Width = 50,
            Height = ButtonHeight
        };
        _positionTextBox.KeyDown += OnPositionKeyDown;
        _positionTextBox.Leave += OnPositionLeave;

        // Create count label
        _countLabel = new KryptonLabel
        {
            Text = @"of 0",
            AutoSize = true,
            LabelStyle = LabelStyle.NormalControl
        };

        // Add controls
        Controls.Add(_moveFirstButton);
        Controls.Add(_movePreviousButton);
        Controls.Add(_positionTextBox);
        Controls.Add(_countLabel);
        Controls.Add(_moveNextButton);
        Controls.Add(_moveLastButton);
        Controls.Add(_addNewButton);
        Controls.Add(_deleteButton);

        // Layout controls
        PerformLayout();

        // Refresh items state
        RefreshItemsInternal();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_bindingSource != null)
            {
                _bindingSource.PositionChanged -= OnBindingSourcePositionChanged;
                _bindingSource.CurrentChanged -= OnBindingSourceCurrentChanged;
                _bindingSource.ListChanged -= OnBindingSourceListChanged;
            }

            if (_moveFirstButton != null)
            {
                _moveFirstButton.Click -= OnMoveFirst;
            }

            if (_movePreviousButton != null)
            {
                _movePreviousButton.Click -= OnMovePrevious;
            }

            if (_moveNextButton != null)
            {
                _moveNextButton.Click -= OnMoveNext;
            }

            if (_moveLastButton != null)
            {
                _moveLastButton.Click -= OnMoveLast;
            }

            if (_addNewButton != null)
            {
                _addNewButton.Click -= OnAddNew;
            }

            if (_deleteButton != null)
            {
                _deleteButton.Click -= OnDelete;
            }

            if (_positionTextBox != null)
            {
                _positionTextBox.KeyDown -= OnPositionKeyDown;
                _positionTextBox.Leave -= OnPositionLeave;
            }
        }

        base.Dispose(disposing);
    }
    
    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the BindingSource component that is the source of data.
    /// </summary>
    [Category(@"Data")]
    [Description(@"The BindingSource component that is the source of data.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public BindingSource? BindingSource
    {
        get => _bindingSource;
        set
        {
            if (_bindingSource != value)
            {
                // Unhook from old binding source
                if (_bindingSource != null)
                {
                    _bindingSource.PositionChanged -= OnBindingSourcePositionChanged;
                    _bindingSource.CurrentChanged -= OnBindingSourceCurrentChanged;
                    _bindingSource.ListChanged -= OnBindingSourceListChanged;
                }

                _bindingSource = value;

                // Hook into new binding source
                if (_bindingSource != null)
                {
                    _bindingSource.PositionChanged += OnBindingSourcePositionChanged;
                    _bindingSource.CurrentChanged += OnBindingSourceCurrentChanged;
                    _bindingSource.ListChanged += OnBindingSourceListChanged;
                }

                RefreshItemsInternal();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the Add New button is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the Add New button is enabled.")]
    [DefaultValue(true)]
    public bool AddNewItemEnabled
    {
        get => _addNewItemEnabled;
        set
        {
            if (_addNewItemEnabled != value)
            {
                _addNewItemEnabled = value;
                _addNewButton?.Enabled = value && CanAddNew();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the Delete button is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the Delete button is enabled.")]
    [DefaultValue(true)]
    public bool DeleteItemEnabled
    {
        get => _deleteItemEnabled;
        set
        {
            if (_deleteItemEnabled != value)
            {
                _deleteItemEnabled = value;
                _deleteButton?.Enabled = value && CanDelete();
            }
        }
    }

    /// <summary>
    /// Gets the Move First button.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonButton? MoveFirstItem => _moveFirstButton;

    /// <summary>
    /// Gets the Move Previous button.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonButton? MovePreviousItem => _movePreviousButton;

    /// <summary>
    /// Gets the Move Next button.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonButton? MoveNextItem => _moveNextButton;

    /// <summary>
    /// Gets the Move Last button.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonButton? MoveLastItem => _moveLastButton;

    /// <summary>
    /// Gets the Add New button.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonButton? AddNewItem => _addNewButton;

    /// <summary>
    /// Gets the Delete button.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonButton? DeleteItem => _deleteButton;

    /// <summary>
    /// Gets the position textbox.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonTextBox? PositionItem => _positionTextBox;

    /// <summary>
    /// Gets the count label.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonLabel? CountItem => _countLabel;

    /// <summary>
    /// Refreshes the state of the standard items to reflect the current state of the data.
    /// </summary>
    public void RefreshItemsInternal()
    {
        OnRefreshItems(EventArgs.Empty);

        if (_bindingSource == null)
        {
            // Disable all buttons
            _moveFirstButton?.Enabled = false;

            _movePreviousButton?.Enabled = false;

            _moveNextButton?.Enabled = false;

            _moveLastButton?.Enabled = false;

            _addNewButton?.Enabled = false;

            _deleteButton?.Enabled = false;

            if (_positionTextBox != null)
            {
                _positionTextBox.Text = string.Empty;
                _positionTextBox.Enabled = false;
            }

            _countLabel?.Text = @"of 0";

            return;
        }

        int count = _bindingSource.Count;
        int position = _bindingSource.Position + 1; // 1-based for display

        // Update position textbox
        if (_positionTextBox != null)
        {
            _positionTextBox.Text = position.ToString();
            _positionTextBox.Enabled = true;
        }

        // Update count label
        _countLabel?.Text = $@"of {count}";
        
        // Trigger layout to reposition controls after count label width changes
        PerformLayout();

        // Update button states
        bool canMovePrevious = _bindingSource.Position > 0;
        bool canMoveNext = _bindingSource.Position < count - 1;

        _moveFirstButton?.Enabled = canMovePrevious;

        _movePreviousButton?.Enabled = canMovePrevious;

        _moveNextButton?.Enabled = canMoveNext;

        _moveLastButton?.Enabled = canMoveNext;

        _addNewButton?.Enabled = _addNewItemEnabled && CanAddNew();

        _deleteButton?.Enabled = _deleteItemEnabled && CanDelete();
    }
    
    #endregion

    #region Protected Overrides

    /// <summary>
    /// Raises the Layout event.
    /// </summary>
    protected override void OnLayout(LayoutEventArgs levent)
    {
        base.OnLayout(levent);

        if (_moveFirstButton == null || _movePreviousButton == null || _positionTextBox == null ||
            _countLabel == null || _moveNextButton == null || _moveLastButton == null ||
            _addNewButton == null || _deleteButton == null)
        {
            return;
        }

        int x = Padding.Left;
        int y = (Height - ButtonHeight) / 2;

        // First button
        _moveFirstButton.Location = new Point(x, y);
        x += ButtonWidth + Spacing;

        // Previous button
        _movePreviousButton.Location = new Point(x, y);
        x += ButtonWidth + Spacing * 2; // Extra spacing before position

        // Position textbox
        _positionTextBox.Location = new Point(x, y);
        x += _positionTextBox.Width + Spacing;

        // Count label
        _countLabel.Location = new Point(x, y + (ButtonHeight - _countLabel.Height) / 2);
        x += _countLabel.Width + Spacing * 2; // Extra spacing after position

        // Next button
        _moveNextButton.Location = new Point(x, y);
        x += ButtonWidth + Spacing;

        // Last button
        _moveLastButton.Location = new Point(x, y);
        x += ButtonWidth + Spacing * 2; // Extra spacing before add/delete

        // Add New button
        _addNewButton.Location = new Point(x, y);
        x += ButtonWidth + Spacing;

        // Delete button
        _deleteButton.Location = new Point(x, y);
    }
    #endregion

    #region Implementation
    private void OnMoveFirst(object? sender, EventArgs e)
    {
        _bindingSource?.MoveFirst();
    }

    private void OnMovePrevious(object? sender, EventArgs e)
    {
        _bindingSource?.MovePrevious();
    }

    private void OnMoveNext(object? sender, EventArgs e)
    {
        _bindingSource?.MoveNext();
    }

    private void OnMoveLast(object? sender, EventArgs e)
    {
        _bindingSource?.MoveLast();
    }

    private void OnAddNew(object? sender, EventArgs e)
    {
        if (_bindingSource != null && CanAddNew())
        {
            _bindingSource.AddNew();
        }
    }

    private void OnDelete(object? sender, EventArgs e)
    {
        if (_bindingSource != null && CanDelete())
        {
            _bindingSource.RemoveCurrent();
        }
    }

    private void OnPositionKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && _positionTextBox != null && _bindingSource != null)
        {
            if (int.TryParse(_positionTextBox.Text, out int position))
            {
                // Convert from 1-based to 0-based
                position = Math.Max(1, Math.Min(position, _bindingSource.Count));
                _bindingSource.Position = position - 1;
            }
            else
            {
                // Restore current position
                _positionTextBox.Text = (_bindingSource.Position + 1).ToString();
            }

            e.Handled = true;
        }
    }

    private void OnPositionLeave(object? sender, EventArgs e)
    {
        if (_positionTextBox != null && _bindingSource != null)
        {
            // Restore current position since navigation only occurs on Enter key
            // This ensures the textbox always reflects the actual binding source position
            _positionTextBox.Text = (_bindingSource.Position + 1).ToString();
        }
    }

    private void OnBindingSourcePositionChanged(object? sender, EventArgs e)
    {
        RefreshItemsInternal();
    }

    private void OnBindingSourceCurrentChanged(object? sender, EventArgs e)
    {
        RefreshItemsInternal();
    }

    private void OnBindingSourceListChanged(object? sender, ListChangedEventArgs e)
    {
        RefreshItemsInternal();
    }

    private bool CanAddNew() => _bindingSource is { AllowNew: true };

    private bool CanDelete() => _bindingSource is { Count: > 0, AllowRemove: true };

    #endregion
}
