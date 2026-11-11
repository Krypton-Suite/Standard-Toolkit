#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of KryptonBindingNavigator control.
/// This example shows:
/// - Basic data binding with BindingSource
/// - Navigation buttons (First, Previous, Next, Last)
/// - Position textbox with validation
/// - Add New and Delete functionality
/// - Enable/Disable Add New and Delete buttons
/// - RefreshItems event handling
/// - Edge cases: empty list, single item, many items
/// - Data binding with detail controls
/// - Integration with KryptonDataGridView
/// </summary>
public partial class BindingNavigatorDemo : KryptonForm
{
    private BindingSource _bindingSource = null!;
    private List<PersonDetail> _dataList = null!;

    public BindingNavigatorDemo()
    {
        InitializeComponent();
        InitializeData();
    }

    /// <summary>
    /// Initializes the data source and binds it to the KryptonBindingNavigator and other controls.
    /// </summary>
    private void InitializeData()
    {
        // Create sample data
        _dataList = new List<PersonDetail>
        {
            new PersonDetail { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Age = 30 },
            new PersonDetail { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Age = 25 },
            new PersonDetail { Id = 3, FirstName = "Bob", LastName = "Johnson", Email = "bob.johnson@example.com", Age = 35 },
            new PersonDetail { Id = 4, FirstName = "Alice", LastName = "Williams", Email = "alice.williams@example.com", Age = 28 },
            new PersonDetail { Id = 5, FirstName = "Charlie", LastName = "Brown", Email = "charlie.brown@example.com", Age = 42 }
        };

        // Create BindingSource with AllowNew enabled
        // AllowRemove is read-only and automatically true for List<T>
        // These properties control whether Add New and Delete buttons are enabled
        _bindingSource = new BindingSource
        {
            DataSource = _dataList,
            AllowNew = true
        };

        // Bind the BindingSource to KryptonBindingNavigator
        // The navigator will automatically handle position changes and update button states
        kryptonBindingNavigator1.BindingSource = _bindingSource;

        // Bind to DataGridView for visual display
        kdgvMain.DataSource = _bindingSource;

        // Bind detail controls to current item properties
        // These will automatically update when navigation occurs
        ktxtFirstName.DataBindings.Add("Text", _bindingSource, "FirstName", false, DataSourceUpdateMode.OnPropertyChanged);
        ktxtLastName.DataBindings.Add("Text", _bindingSource, "LastName", false, DataSourceUpdateMode.OnPropertyChanged);
        ktxtEmail.DataBindings.Add("Text", _bindingSource, "Email", false, DataSourceUpdateMode.OnPropertyChanged);
        knudAge.DataBindings.Add("Value", _bindingSource, "Age", false, DataSourceUpdateMode.OnPropertyChanged);

        // Subscribe to CurrentChanged to update display
        _bindingSource.CurrentChanged += BindingSource_CurrentChanged;
        UpdateCurrentItemDisplay();
    }

    /// <summary>
    /// Handles the CurrentChanged event from BindingSource.
    /// This is called whenever the current item changes (via navigation, add, delete, etc.).
    /// </summary>
    private void BindingSource_CurrentChanged(object? sender, EventArgs e)
    {
        UpdateCurrentItemDisplay();
    }

    /// <summary>
    /// Updates the current item display label to show the currently selected person.
    /// </summary>
    private void UpdateCurrentItemDisplay()
    {
        if (_bindingSource.Current is PersonDetail person)
        {
            klblCurrentItem.Text = $@"Current: {person.FirstName} {person.LastName} (ID: {person.Id})";
        }
        else
        {
            klblCurrentItem.Text = @"Current: None";
        }
    }

    /// <summary>
    /// Demonstrates programmatic addition of a new item.
    /// The KryptonBindingNavigator's Add New button uses BindingSource.AddNew() internally.
    /// </summary>
    private void kbtnAddNew_Click(object sender, EventArgs e)
    {
        int newId = _dataList.Count > 0 ? _dataList.Max(p => p.Id) + 1 : 1;
        PersonDetail newPerson = new PersonDetail
        {
            Id = newId,
            FirstName = @"New",
            LastName = @"Person",
            Email = @"new.person@example.com",
            Age = 0
        };
        _bindingSource.Add(newPerson);
        _bindingSource.MoveLast();
    }

    /// <summary>
    /// Demonstrates programmatic deletion of the current item.
    /// The KryptonBindingNavigator's Delete button uses BindingSource.RemoveCurrent() internally.
    /// </summary>
    private void kbtnDeleteCurrent_Click(object sender, EventArgs e)
    {
        if (_bindingSource.Current != null)
        {
            _bindingSource.RemoveCurrent();
        }
    }

    /// <summary>
    /// Clears all items from the data source.
    /// Demonstrates how the navigator handles an empty list (all buttons disabled except Add New if AllowNew is true).
    /// </summary>
    private void kbtnClearAll_Click(object sender, EventArgs e)
    {
        DialogResult result = KryptonMessageBox.Show(
            this,
            @"Are you sure you want to clear all items?",
            @"Clear All",
            KryptonMessageBoxButtons.YesNo,
            KryptonMessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            _dataList.Clear();
            _bindingSource.DataSource = null;
            _bindingSource.DataSource = _dataList;
            UpdateCurrentItemDisplay();
        }
    }

    /// <summary>
    /// Reloads the original sample data.
    /// Useful for resetting the demo to its initial state.
    /// </summary>
    private void kbtnLoadSampleData_Click(object sender, EventArgs e)
    {
        _dataList.Clear();
        _dataList.AddRange(new List<PersonDetail>
        {
            new PersonDetail { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Age = 30 },
            new PersonDetail { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Age = 25 },
            new PersonDetail { Id = 3, FirstName = "Bob", LastName = "Johnson", Email = "bob.johnson@example.com", Age = 35 },
            new PersonDetail { Id = 4, FirstName = "Alice", LastName = "Williams", Email = "alice.williams@example.com", Age = 28 },
            new PersonDetail { Id = 5, FirstName = "Charlie", LastName = "Brown", Email = "charlie.brown@example.com", Age = 42 }
        });

        _bindingSource.DataSource = null;
        _bindingSource.DataSource = _dataList;
        _bindingSource.MoveFirst();
        UpdateCurrentItemDisplay();
    }

    /// <summary>
    /// Toggles the AddNewItemEnabled property.
    /// When false, the Add New button is disabled even if AllowNew is true.
    /// </summary>
    private void kchkAddNewEnabled_CheckedChanged(object sender, EventArgs e)
    {
        kryptonBindingNavigator1.AddNewItemEnabled = kchkAddNewEnabled.Checked;
    }

    /// <summary>
    /// Toggles the DeleteItemEnabled property.
    /// When false, the Delete button is disabled even if AllowRemove is true.
    /// </summary>
    private void kchkDeleteEnabled_CheckedChanged(object sender, EventArgs e)
    {
        kryptonBindingNavigator1.DeleteItemEnabled = kchkDeleteEnabled.Checked;
    }

    /// <summary>
    /// Manually refreshes the navigator's item states.
    /// This is useful when you need to force an update without changing the BindingSource.
    /// </summary>
    private void kbtnRefreshItems_Click(object sender, EventArgs e)
    {
        kryptonBindingNavigator1.RefreshItemsInternal();
    }

    /// <summary>
    /// Handles the RefreshItems event from KryptonBindingNavigator.
    /// This event is raised whenever RefreshItemsInternal() is called or when the navigator
    /// automatically refreshes due to BindingSource changes.
    /// </summary>
    private void kryptonBindingNavigator1_RefreshItems(object? sender, EventArgs e)
    {
        UpdateCurrentItemDisplay();
    }

    /// <summary>
    /// Tests the navigator with an empty list.
    /// Demonstrates that navigation buttons are disabled when there are no items.
    /// </summary>
    private void kbtnTestEmptyList_Click(object sender, EventArgs e)
    {
        _dataList.Clear();
        _bindingSource.DataSource = null;
        _bindingSource.DataSource = _dataList;
        UpdateCurrentItemDisplay();
    }

    /// <summary>
    /// Tests the navigator with a single item.
    /// Demonstrates that First/Previous and Next/Last buttons are disabled appropriately.
    /// </summary>
    private void kbtnTestSingleItem_Click(object sender, EventArgs e)
    {
        _dataList.Clear();
        _dataList.Add(new PersonDetail { Id = 1, FirstName = "Single", LastName = "Item", Email = "single@example.com", Age = 20 });
        _bindingSource.DataSource = null;
        _bindingSource.DataSource = _dataList;
        _bindingSource.MoveFirst();
        UpdateCurrentItemDisplay();
    }

    /// <summary>
    /// Tests the navigator with many items (50).
    /// Demonstrates navigation through a larger dataset and position textbox functionality.
    /// Try typing a number in the position textbox and pressing Enter to jump to that position.
    /// </summary>
    private void kbtnTestManyItems_Click(object sender, EventArgs e)
    {
        _dataList.Clear();
        for (int i = 1; i <= 50; i++)
        {
            _dataList.Add(new PersonDetail
            {
                Id = i,
                FirstName = $"First{i}",
                LastName = $"Last{i}",
                Email = $"person{i}@example.com",
                Age = 20 + (i % 40)
            });
        }
        _bindingSource.DataSource = null;
        _bindingSource.DataSource = _dataList;
        _bindingSource.MoveFirst();
        UpdateCurrentItemDisplay();
    }

    /// <summary>
    /// Demonstrates how to access individual navigator items programmatically.
    /// Shows all the exposed properties and methods available on the navigator.
    /// </summary>
    private void DemonstrateNavigatorItems()
    {
        // Access individual buttons
        KryptonButton? moveFirst = kryptonBindingNavigator1.MoveFirstItem;
        KryptonButton? movePrevious = kryptonBindingNavigator1.MovePreviousItem;
        KryptonButton? moveNext = kryptonBindingNavigator1.MoveNextItem;
        KryptonButton? moveLast = kryptonBindingNavigator1.MoveLastItem;
        KryptonButton? addNew = kryptonBindingNavigator1.AddNewItem;
        KryptonButton? delete = kryptonBindingNavigator1.DeleteItem;

        // Access position textbox and count label
        KryptonTextBox? positionBox = kryptonBindingNavigator1.PositionItem;
        KryptonLabel? countLabel = kryptonBindingNavigator1.CountItem;

        // You can customize these controls if needed
        // For example, change button text or styles
        if (moveFirst != null)
        {
            // Customize button properties here if needed
        }
    }
}

/// <summary>
/// PersonDetail class used as a data model for the KryptonBindingNavigator demo.
/// This class provides detailed person information for demonstrating data binding.
/// </summary>
public class PersonDetail
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Age { get; set; }
}

