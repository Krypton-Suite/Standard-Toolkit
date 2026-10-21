#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.IO;

namespace TestForm;

/// <summary>
/// Comprehensive test form demonstrating jump list functionality on KryptonForm.
/// </summary>
public partial class JumpListTest : KryptonForm
{
    public JumpListTest()
    {
        InitializeComponent();
        InitializeJumpList();
    }

    private void InitializeJumpList()
    {
        // Set form icon
        Icon = SystemIcons.Application;

        // Set application ID (required for jump lists)
        JumpList.AppId = "KryptonToolkit.JumpListTest";

        // Setup examples
        SetupBasicExamples();
        SetupUserTasksExamples();
        SetupCategoryExamples();
        SetupKnownCategoriesExamples();

        // Setup property grid
        propertyGrid.SelectedObject = this;
    }

    private void SetupBasicExamples()
    {
        // Example 1: Set application ID
        lblExample1.Text = "Example 1: Application ID (Required)";
        btnSetAppId.Text = "Set App ID";
        btnSetAppId.Click += (s, e) =>
        {
            JumpList.AppId = "KryptonToolkit.JumpListTest";
            UpdateStatus("Application ID set: " + JumpList.AppId);
        };

        // Example 2: Clear jump list
        lblExample2.Text = "Example 2: Clear jump list";
        btnClearJumpList.Text = "Clear All";
        btnClearJumpList.Click += (s, e) =>
        {
            JumpList.Reset();
            UpdateStatus("Jump list cleared");
        };
    }

    private void SetupUserTasksExamples()
    {
        // Example 3: Add user tasks
        lblExample3.Text = "Example 3: Add user tasks";
        btnAddUserTask.Text = "Add User Task";
        btnAddUserTask.Click += BtnAddUserTask_Click;

        btnAddMultipleTasks.Text = "Add Multiple Tasks";
        btnAddMultipleTasks.Click += BtnAddMultipleTasks_Click;

        btnClearUserTasks.Text = "Clear User Tasks";
        btnClearUserTasks.Click += (s, e) =>
        {
            JumpList.UserTasks.Clear();
            UpdateStatus("User tasks cleared");
        };
    }

    private void SetupCategoryExamples()
    {
        // Example 4: Add custom categories
        lblExample4.Text = "Example 4: Custom categories";
        btnAddRecentFiles.Text = "Add Recent Files";
        btnAddRecentFiles.Click += BtnAddRecentFiles_Click;

        btnAddTemplates.Text = "Add Templates";
        btnAddTemplates.Click += BtnAddTemplates_Click;

        btnClearCategories.Text = "Clear Categories";
        btnClearCategories.Click += (s, e) =>
        {
            JumpList.ClearCategories();
            UpdateStatus("Categories cleared");
        };
    }

    private void SetupKnownCategoriesExamples()
    {
        // Example 5: Known categories
        lblExample5.Text = "Example 5: Known categories (Windows-managed)";
        btnShowFrequent.Text = "Show Frequent";
        btnShowFrequent.Click += (s, e) =>
        {
            JumpList.ShowFrequentCategory = !JumpList.ShowFrequentCategory;
            UpdateStatus($"Frequent category: {(JumpList.ShowFrequentCategory ? "Enabled" : "Disabled")}");
        };

        btnShowRecent.Text = "Show Recent";
        btnShowRecent.Click += (s, e) =>
        {
            JumpList.ShowRecentCategory = !JumpList.ShowRecentCategory;
            UpdateStatus($"Recent category: {(JumpList.ShowRecentCategory ? "Enabled" : "Disabled")}");
        };
    }

    private void BtnAddUserTask_Click(object? sender, EventArgs e)
    {
        var task = new JumpListItem
        {
            Title = "Open Settings",
            Path = Application.ExecutablePath,
            Arguments = "/settings",
            Description = "Open application settings",
            IconPath = Application.ExecutablePath,
            IconIndex = 0
        };

        JumpList.UserTasks.Add(task);
        UpdateStatus($"Added user task: {task.Title}");
    }

    private void BtnAddMultipleTasks_Click(object? sender, EventArgs e)
    {
        JumpList.UserTasks.Clear();

        // Task 1: New Document
        JumpList.UserTasks.Add(new JumpListItem
        {
            Title = "New Document",
            Path = Application.ExecutablePath,
            Arguments = "/new",
            Description = "Create a new document",
            IconPath = Application.ExecutablePath,
            IconIndex = 0
        });

        // Task 2: Open File
        JumpList.UserTasks.Add(new JumpListItem
        {
            Title = "Open File",
            Path = Application.ExecutablePath,
            Arguments = "/open",
            Description = "Open an existing file",
            IconPath = Application.ExecutablePath,
            IconIndex = 0
        });

        // Task 3: Calculator (using Windows Calculator)
        JumpList.UserTasks.Add(new JumpListItem
        {
            Title = "Open Calculator",
            Path = "calc.exe",
            Description = "Open Windows Calculator",
            IconPath = "shell32.dll",
            IconIndex = 137
        });

        // Task 4: Notepad
        JumpList.UserTasks.Add(new JumpListItem
        {
            Title = "Open Notepad",
            Path = "notepad.exe",
            Description = "Open Windows Notepad"
        });

        UpdateStatus($"Added {JumpList.UserTasks.Count} user tasks");
    }

    private void BtnAddRecentFiles_Click(object? sender, EventArgs e)
    {
        var recentFiles = new List<JumpListItem>();

        // Add some example recent files
        var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // Example file 1
        var file1 = Path.Combine(documentsPath, "Document1.txt");
        recentFiles.Add(new JumpListItem
        {
            Title = "Document1.txt",
            Path = file1,
            Description = file1
        });

        // Example file 2
        var file2 = Path.Combine(documentsPath, "Report.pdf");
        recentFiles.Add(new JumpListItem
        {
            Title = "Report.pdf",
            Path = file2,
            Description = file2
        });

        // Example file 3
        var file3 = Path.Combine(documentsPath, "Spreadsheet.xlsx");
        recentFiles.Add(new JumpListItem
        {
            Title = "Spreadsheet.xlsx",
            Path = file3,
            Description = file3
        });

        JumpList.AddCategory("Recent Files", recentFiles);
        UpdateStatus($"Added Recent Files category with {recentFiles.Count} items");
    }

    private void BtnAddTemplates_Click(object? sender, EventArgs e)
    {
        var templates = new List<JumpListItem>();

        // Template 1: Invoice Template
        templates.Add(new JumpListItem
        {
            Title = "Invoice Template",
            Path = Application.ExecutablePath,
            Arguments = "/template:invoice",
            Description = "Create new invoice from template",
            IconPath = Application.ExecutablePath,
            IconIndex = 0
        });

        // Template 2: Letter Template
        templates.Add(new JumpListItem
        {
            Title = "Letter Template",
            Path = Application.ExecutablePath,
            Arguments = "/template:letter",
            Description = "Create new letter from template",
            IconPath = Application.ExecutablePath,
            IconIndex = 0
        });

        // Template 3: Report Template
        templates.Add(new JumpListItem
        {
            Title = "Report Template",
            Path = Application.ExecutablePath,
            Arguments = "/template:report",
            Description = "Create new report from template",
            IconPath = Application.ExecutablePath,
            IconIndex = 0
        });

        JumpList.AddCategory("Templates", templates);
        UpdateStatus($"Added Templates category with {templates.Count} items");
    }

    private void UpdateStatus(string message)
    {
        lblStatus.Text = $"Status: {message}";
        lblStatus.Refresh();
    }
}
