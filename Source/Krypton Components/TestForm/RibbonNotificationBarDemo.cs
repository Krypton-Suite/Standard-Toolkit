#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Ribbon;
using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of the Krypton Ribbon Notification Bar feature.
/// </summary>
public partial class RibbonNotificationBarDemo : KryptonForm
{
    private int _progressValue;

    public RibbonNotificationBarDemo()
    {
        InitializeComponent();
        InitializeDemo();
    }

    private void InitializeDemo()
    {
        // Setup event handler for notification bar button clicks
        kryptonRibbon.NotificationBarButtonClick += OnNotificationBarButtonClick;

        // Setup ribbon tabs
        SetupRibbonTabs();

        // Setup demo buttons
        SetupDemoButtons();
    }

    private void SetupRibbonTabs()
    {
        // Home tab with notification controls
        var homeTab = new KryptonRibbonTab { Text = "Home" };
        var notificationGroup = new KryptonRibbonGroup { Tag = "Notifications" };

        var showInfoButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Show",
            TextLine2 = "Information",
            ImageLarge = SystemIcons.Information.ToBitmap()
        };
        showInfoButton.Click += (s, e) => ShowInformationNotification();

        var showWarningButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Show",
            TextLine2 = "Warning",
            ImageLarge = SystemIcons.Warning.ToBitmap()
        };
        showWarningButton.Click += (s, e) => ShowWarningNotification();

        var showErrorButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Show",
            TextLine2 = "Error",
            ImageLarge = SystemIcons.Error.ToBitmap()
        };
        showErrorButton.Click += (s, e) => ShowErrorNotification();

        var showSuccessButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Show",
            TextLine2 = "Success",
            ImageLarge = SystemIcons.Shield.ToBitmap()
        };
        showSuccessButton.Click += (s, e) => ShowSuccessNotification();

        var lines = new KryptonRibbonGroupLines();
        lines.Items.Add(showInfoButton);
        lines.Items.Add(showWarningButton);
        lines.Items.Add(showErrorButton);
        lines.Items.Add(showSuccessButton);
        notificationGroup.Items.Add(lines);

        homeTab.Groups.Add(notificationGroup);
        kryptonRibbon.RibbonTabs.Add(homeTab);
    }

    private void SetupDemoButtons()
    {
        // Information notification examples
        btnInfoBasic.Click += (s, e) => ShowInformationNotification();
        btnInfoWithTitle.Click += (s, e) => ShowInformationWithCommands();

        // Warning notification examples
        btnWarningOfficeStyle.Click += (s, e) => ShowWarningNotification();
        btnWarningAutoDismiss.Click += (s, e) =>
        {
            kryptonRibbon.NotificationBar.Type = RibbonNotificationBarType.Warning;
            kryptonRibbon.NotificationBar.Text = "This notification will auto-dismiss in 5 seconds.";
            kryptonRibbon.NotificationBar.ShowActionButtons = false;
            kryptonRibbon.NotificationBar.AutoDismissSeconds = 5;
            kryptonRibbon.NotificationBar.Visible = true;
        };

        // Error notification examples
        btnErrorBasic.Click += (s, e) => ShowErrorNotification();
        btnErrorMultipleActions.Click += (s, e) => ShowErrorWithCommands();

        // Success notification examples
        btnSuccessBasic.Click += (s, e) => ShowSuccessNotification();
        btnSuccessAutoDismiss.Click += (s, e) =>
        {
            kryptonRibbon.NotificationBar.Type = RibbonNotificationBarType.Success;
            kryptonRibbon.NotificationBar.Text = "Document saved successfully!";
            kryptonRibbon.NotificationBar.ShowActionButtons = false;
            kryptonRibbon.NotificationBar.AutoDismissSeconds = 3;
            kryptonRibbon.NotificationBar.Visible = true;
        };

        // Custom notification examples
        btnCustomColors.Click += (s, e) =>
        {
            kryptonRibbon.NotificationBar.Type = RibbonNotificationBarType.Custom;
            kryptonRibbon.NotificationBar.CustomBackColor = Color.FromArgb(240, 248, 255); // Alice Blue
            kryptonRibbon.NotificationBar.CustomForeColor = Color.FromArgb(25, 25, 112); // Midnight Blue
            kryptonRibbon.NotificationBar.CustomBorderColor = Color.FromArgb(70, 130, 180); // Steel Blue
            kryptonRibbon.NotificationBar.Text = "This is a custom-styled notification with brand colors.";
            kryptonRibbon.NotificationBar.Visible = true;
        };

        btnCustomNoButtons.Click += (s, e) =>
        {
            kryptonRibbon.NotificationBar.Type = RibbonNotificationBarType.Information;
            kryptonRibbon.NotificationBar.Text = "Notification without action buttons or close button.";
            kryptonRibbon.NotificationBar.ShowActionButtons = false;
            kryptonRibbon.NotificationBar.ShowCloseButton = false;
            kryptonRibbon.NotificationBar.AutoDismissSeconds = 5;
            kryptonRibbon.NotificationBar.Visible = true;
        };

        // Advanced examples
        btnProgressNotification.Click += (s, e) => StartProgressNotification();
        btnDynamicUpdate.Click += (s, e) => StartDynamicUpdate();
        btnNotificationQueue.Click += (s, e) => ShowNotificationQueue();

        // Customization controls
        btnApplyCustomization.Click += (s, e) => ApplyCustomization();
        btnHideNotification.Click += (s, e) => kryptonRibbon.NotificationBar.Visible = false;
    }

    private void ShowInformationNotification()
    {
        kryptonRibbon.NotificationBar.Type = RibbonNotificationBarType.Information;
        kryptonRibbon.NotificationBar.Text = "This is an informational notification. Use this type for general information, tips, or non-critical updates.";
        kryptonRibbon.NotificationBar.ActionButtonTexts = new[] { "OK" };
        kryptonRibbon.NotificationBar.Icon = SystemIcons.Information.ToBitmap();
        kryptonRibbon.NotificationBar.Visible = true;
    }

    private void ShowInformationWithCommands()
    {
        // Create KryptonCommand objects for action buttons
        var learnMoreCommand = new KryptonCommand
        {
            Text = "Learn more",
            ImageSmall = SystemIcons.Question.ToBitmap()
        };
        learnMoreCommand.Execute += (s, e) =>
        {
            MessageBox.Show("Learn more clicked! This demonstrates KryptonCommand.Execute event.", 
                "KryptonCommand Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        };

        var dismissCommand = new KryptonCommand
        {
            Text = "Dismiss",
            ImageSmall = SystemIcons.Application.ToBitmap()
        };
        dismissCommand.Execute += (s, e) =>
        {
            kryptonRibbon.NotificationBar.Visible = false;
            MessageBox.Show("Dismiss clicked! Notification hidden via KryptonCommand.Execute event.", 
                "KryptonCommand Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        };

        // Use KryptonCommand array instead of ActionButtonTexts
        kryptonRibbon.NotificationBar.Type = RibbonNotificationBarType.Information;
        kryptonRibbon.NotificationBar.Title = "NEW FEATURES";
        kryptonRibbon.NotificationBar.Text = "Check out the latest features in this update! This example uses KryptonCommand objects for buttons.";
        kryptonRibbon.NotificationBar.ActionButtonCommands = new[] { learnMoreCommand, dismissCommand };
        kryptonRibbon.NotificationBar.Icon = SystemIcons.Information.ToBitmap();
        kryptonRibbon.NotificationBar.Visible = true;
    }

    private void ShowWarningNotification()
    {
        kryptonRibbon.NotificationBar.Type = RibbonNotificationBarType.Warning;
        kryptonRibbon.NotificationBar.Title = "UPDATES AVAILABLE";
        kryptonRibbon.NotificationBar.Text = "Updates for Office are ready to be applied, but are blocked by one or more apps.";
        kryptonRibbon.NotificationBar.ActionButtonTexts = new[] { "Update now" };
        kryptonRibbon.NotificationBar.Icon = SystemIcons.Warning.ToBitmap();
        kryptonRibbon.NotificationBar.Visible = true;
    }

    private void ShowErrorNotification()
    {
        kryptonRibbon.NotificationBar.Type = RibbonNotificationBarType.Error;
        kryptonRibbon.NotificationBar.Text = "An error occurred while saving the document. Please try again.";
        kryptonRibbon.NotificationBar.ActionButtonTexts = new[] { "Retry", "Cancel" };
        kryptonRibbon.NotificationBar.Icon = SystemIcons.Error.ToBitmap();
        kryptonRibbon.NotificationBar.Visible = true;
    }

    private void ShowErrorWithCommands()
    {
        // Create KryptonCommand objects with different enabled states
        var retryCommand = new KryptonCommand
        {
            Text = "Retry",
            ImageSmall = SystemIcons.Shield.ToBitmap(),
            Enabled = true
        };
        retryCommand.Execute += (s, e) =>
        {
            MessageBox.Show("Retry clicked! Attempting to save again...", 
                "KryptonCommand Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        };

        var saveAsCommand = new KryptonCommand
        {
            Text = "Save As",
            ImageSmall = SystemIcons.Application.ToBitmap(),
            Enabled = true
        };
        saveAsCommand.Execute += (s, e) =>
        {
            MessageBox.Show("Save As clicked! Opening save dialog...", 
                "KryptonCommand Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        };

        var discardCommand = new KryptonCommand
        {
            Text = "Discard",
            ImageSmall = SystemIcons.Error.ToBitmap(),
            Enabled = true
        };
        discardCommand.Execute += (s, e) =>
        {
            var result = MessageBox.Show("Are you sure you want to discard changes?", 
                "Confirm Discard", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                kryptonRibbon.NotificationBar.Visible = false;
            }
        };

        kryptonRibbon.NotificationBar.Type = RibbonNotificationBarType.Error;
        kryptonRibbon.NotificationBar.Text = "Failed to save document. What would you like to do? (Using KryptonCommand)";
        kryptonRibbon.NotificationBar.ActionButtonCommands = new[] { retryCommand, saveAsCommand, discardCommand };
        kryptonRibbon.NotificationBar.Icon = SystemIcons.Error.ToBitmap();
        kryptonRibbon.NotificationBar.Visible = true;
    }

    private void ShowSuccessNotification()
    {
        kryptonRibbon.NotificationBar.Type = RibbonNotificationBarType.Success;
        kryptonRibbon.NotificationBar.Text = "Your changes have been saved successfully!";
        kryptonRibbon.NotificationBar.ActionButtonTexts = new[] { "OK" };
        kryptonRibbon.NotificationBar.Icon = SystemIcons.Shield.ToBitmap();
        kryptonRibbon.NotificationBar.Visible = true;
    }

    private void StartProgressNotification()
    {
        _progressValue = 0;
        kryptonRibbon.NotificationBar.Type = RibbonNotificationBarType.Information;
        kryptonRibbon.NotificationBar.Text = "Processing... 0%";
        kryptonRibbon.NotificationBar.ShowActionButtons = false;
        kryptonRibbon.NotificationBar.AutoDismissSeconds = 0;
        kryptonRibbon.NotificationBar.Visible = true;

        var timer = new Timer { Interval = 200 };
        timer.Tick += (s, e) =>
        {
            _progressValue += 5;
            if (_progressValue <= 100)
            {
                kryptonRibbon.NotificationBar.Text = $"Processing... {_progressValue}%";
                if (_progressValue == 100)
                {
                    timer.Stop();
                    timer.Dispose();
                    kryptonRibbon.NotificationBar.Type = RibbonNotificationBarType.Success;
                    kryptonRibbon.NotificationBar.Text = "Processing complete!";
                    kryptonRibbon.NotificationBar.AutoDismissSeconds = 3;
                }
            }
        };
        timer.Start();
    }

    private void StartDynamicUpdate()
    {
        kryptonRibbon.NotificationBar.Type = RibbonNotificationBarType.Information;
        kryptonRibbon.NotificationBar.Text = "Initializing...";
        kryptonRibbon.NotificationBar.ShowActionButtons = false;
        kryptonRibbon.NotificationBar.Visible = true;

        var messages = new[] { "Loading...", "Processing data...", "Almost done...", "Complete!" };
        int index = 0;

        var timer = new Timer { Interval = 1000 };
        timer.Tick += (s, e) =>
        {
            if (index < messages.Length)
            {
                kryptonRibbon.NotificationBar.Text = messages[index];
                index++;
            }
            else
            {
                timer.Stop();
                timer.Dispose();
                kryptonRibbon.NotificationBar.Type = RibbonNotificationBarType.Success;
                kryptonRibbon.NotificationBar.AutoDismissSeconds = 2;
            }
        };
        timer.Start();
    }

    private void ShowNotificationQueue()
    {
        var queue = new Queue<string>();
        queue.Enqueue("First notification");
        queue.Enqueue("Second notification");
        queue.Enqueue("Third notification");

        ShowNextInQueue(queue);
    }

    private void ShowNextInQueue(Queue<string> queue)
    {
        if (queue.Count == 0)
        {
            return;
        }

        string message = queue.Dequeue();
        kryptonRibbon.NotificationBar.Type = RibbonNotificationBarType.Information;
        kryptonRibbon.NotificationBar.Text = message;
        kryptonRibbon.NotificationBar.ActionButtonTexts = new[] { "Next", "Dismiss" };
        kryptonRibbon.NotificationBar.Visible = true;

        // Store queue in tag for button click handler
        kryptonRibbon.Tag = queue;
    }

    private void ApplyCustomization()
    {
        // Apply settings from customization panel
        if (cmbNotificationType.SelectedItem != null)
        {
            kryptonRibbon.NotificationBar.Type = (RibbonNotificationBarType)cmbNotificationType.SelectedItem;
        }

        kryptonRibbon.NotificationBar.Text = txtMessageText.Text;
        kryptonRibbon.NotificationBar.Title = txtTitleText.Text;
        kryptonRibbon.NotificationBar.ShowIcon = chkShowIcon.Checked;
        kryptonRibbon.NotificationBar.ShowCloseButton = chkShowCloseButton.Checked;
        kryptonRibbon.NotificationBar.ShowActionButtons = chkShowActionButtons.Checked;

        if (chkShowActionButtons.Checked && !string.IsNullOrEmpty(txtButtonTexts.Text))
        {
            var buttonTexts = txtButtonTexts.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .ToArray();
            kryptonRibbon.NotificationBar.ActionButtonTexts = buttonTexts;
        }

        if (numAutoDismiss.Value > 0)
        {
            kryptonRibbon.NotificationBar.AutoDismissSeconds = (int)numAutoDismiss.Value;
        }
        else
        {
            kryptonRibbon.NotificationBar.AutoDismissSeconds = 0;
        }

        if (numHeight.Value > 0)
        {
            kryptonRibbon.NotificationBar.Height = (int)numHeight.Value;
        }
        else
        {
            kryptonRibbon.NotificationBar.Height = 0;
        }

        kryptonRibbon.NotificationBar.Visible = true;
    }

    private void OnNotificationBarButtonClick(object? sender, RibbonNotificationBarEventArgs e)
    {
        string message;
        if (e.ActionButtonIndex == -1)
        {
            message = "Close button was clicked.";
        }
        else
        {
            // Check if using KryptonCommand or ActionButtonTexts
            string buttonText;
            if (kryptonRibbon.NotificationBar.ActionButtonCommands != null && 
                e.ActionButtonIndex < kryptonRibbon.NotificationBar.ActionButtonCommands.Length)
            {
                var command = kryptonRibbon.NotificationBar.ActionButtonCommands[e.ActionButtonIndex];
                buttonText = command?.Text ?? "Unknown";
                message = $"Action button '{buttonText}' (index {e.ActionButtonIndex}) was clicked via NotificationBarButtonClick event. " +
                         $"Note: KryptonCommand.Execute event also fires.";
            }
            else
            {
                buttonText = kryptonRibbon.NotificationBar.ActionButtonTexts?[e.ActionButtonIndex] ?? "Unknown";
                message = $"Action button '{buttonText}' (index {e.ActionButtonIndex}) was clicked.";
            }

            // Handle queue navigation
            if (kryptonRibbon.Tag is Queue<string> queue)
            {
                if (e.ActionButtonIndex == 0 && buttonText == "Next")
                {
                    ShowNextInQueue(queue);
                    return;
                }
                kryptonRibbon.Tag = null;
            }
        }

        // Show result in status label
        lblStatus.Text = $"Event: {message}";

        // For demo purposes, show message box
        MessageBox.Show(message, "Notification Bar Event", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        // Clean up
        kryptonRibbon.NotificationBar.Visible = false;
        base.OnFormClosing(e);
    }
}