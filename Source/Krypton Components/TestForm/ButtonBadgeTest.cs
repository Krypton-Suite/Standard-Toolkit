#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive test form demonstrating badge functionality on KryptonButton and KryptonCheckButton.
/// </summary>
public partial class ButtonBadgeTest : KryptonForm
{
    private int _notificationCount = 5;

    public ButtonBadgeTest()
    {
        InitializeComponent();
        InitializeBadges();
    }

    private void InitializeBadges()
    {
        // Setup demo buttons with various badge configurations
        SetupPositionExamples();
        SetupColorExamples();
        SetupNotificationExamples();
        SetupInteractiveExamples();
        SetupShapeExamples();
        SetupAnimationExamples();
        SetupFontExamples();

        // Set property grid to show badge properties
        propertyGrid.SelectedObject = btnInteractive;
    }

    private void SetupPositionExamples()
    {
        // Top Right (default)
        btnTopRight.BadgeValues.Text = "3";
        btnTopRight.BadgeValues.Visible = true;
        btnTopRight.BadgeValues.Position = BadgePosition.TopRight;
        btnTopRight.BadgeValues.BadgeColor = Color.Red;
        btnTopRight.BadgeValues.TextColor = Color.White;

        // Top Left
        btnTopLeft.BadgeValues.Text = "12";
        btnTopLeft.BadgeValues.Visible = true;
        btnTopLeft.BadgeValues.Position = BadgePosition.TopLeft;
        btnTopLeft.BadgeValues.BadgeColor = Color.Blue;
        btnTopLeft.BadgeValues.TextColor = Color.White;

        // Bottom Right
        btnBottomRight.BadgeValues.Text = "99+";
        btnBottomRight.BadgeValues.Visible = true;
        btnBottomRight.BadgeValues.Position = BadgePosition.BottomRight;
        btnBottomRight.BadgeValues.BadgeColor = Color.Green;
        btnBottomRight.BadgeValues.TextColor = Color.White;

        // Bottom Left
        btnBottomLeft.BadgeValues.Text = "!";
        btnBottomLeft.BadgeValues.Visible = true;
        btnBottomLeft.BadgeValues.Position = BadgePosition.BottomLeft;
        btnBottomLeft.BadgeValues.BadgeColor = Color.Orange;
        btnBottomLeft.BadgeValues.TextColor = Color.White;
    }

    private void SetupColorExamples()
    {
        // Red badge with text
        btnRedBadge.BadgeValues.Text = "5";
        btnRedBadge.BadgeValues.Visible = true;
        btnRedBadge.BadgeValues.BadgeColor = Color.Red;
        btnRedBadge.BadgeValues.TextColor = Color.White;

        // Blue badge with text
        btnBlueBadge.BadgeValues.Text = "8";
        btnBlueBadge.BadgeValues.Visible = true;
        btnBlueBadge.BadgeValues.BadgeColor = Color.Blue;
        btnBlueBadge.BadgeValues.TextColor = Color.White;

        // Green badge with text
        btnGreenBadge.BadgeValues.Text = "12";
        btnGreenBadge.BadgeValues.Visible = true;
        btnGreenBadge.BadgeValues.BadgeColor = Color.Green;
        btnGreenBadge.BadgeValues.TextColor = Color.White;

        // Custom color badge with image (using SystemIcons)
        btnCustomBadge.BadgeValues.BadgeImage = SystemIcons.Information.ToBitmap();
        btnCustomBadge.BadgeValues.Visible = true;
        btnCustomBadge.BadgeValues.BadgeColor = Color.Purple;
        btnCustomBadge.BadgeValues.Text = ""; // Clear text when using image
    }

    private void SetupNotificationExamples()
    {
        // Notification counter
        btnNotifications.BadgeValues.Text = _notificationCount.ToString();
        btnNotifications.BadgeValues.Visible = _notificationCount > 0;
        btnNotifications.BadgeValues.BadgeColor = Color.Red;
        btnNotifications.BadgeValues.TextColor = Color.White;

        // Message badge
        btnMessages.BadgeValues.Text = "3";
        btnMessages.BadgeValues.Visible = true;
        btnMessages.BadgeValues.BadgeColor = Color.DarkBlue;
        btnMessages.BadgeValues.TextColor = Color.White;

        // Alert badge with image
        btnAlerts.BadgeValues.BadgeImage = SystemIcons.Warning.ToBitmap();
        btnAlerts.BadgeValues.Visible = true;
        btnAlerts.BadgeValues.BadgeColor = Color.Orange;
        btnAlerts.BadgeValues.Text = ""; // Clear text when using image
    }

    private void SetupInteractiveExamples()
    {
        // Interactive button - properties can be modified in property grid
        btnInteractive.BadgeValues.Text = "42";
        btnInteractive.BadgeValues.Visible = true;
        btnInteractive.BadgeValues.Position = BadgePosition.TopRight;
        btnInteractive.BadgeValues.BadgeColor = Color.Red;
        btnInteractive.BadgeValues.TextColor = Color.White;
    }

    private void BtnIncrementNotifications_Click(object sender, EventArgs e)
    {
        _notificationCount++;
        btnNotifications.BadgeValues.Text = _notificationCount.ToString();
        btnNotifications.BadgeValues.Visible = true;
        UpdateNotificationLabel();
    }

    private void BtnDecrementNotifications_Click(object sender, EventArgs e)
    {
        if (_notificationCount > 0)
        {
            _notificationCount--;
            btnNotifications.BadgeValues.Text = _notificationCount.ToString();
            btnNotifications.BadgeValues.Visible = _notificationCount > 0;
        }
        UpdateNotificationLabel();
    }

    private void BtnClearNotifications_Click(object sender, EventArgs e)
    {
        _notificationCount = 0;
        btnNotifications.BadgeValues.Visible = false;
        UpdateNotificationLabel();
    }

    private void UpdateNotificationLabel()
    {
        lblNotificationCount.Values.Text = $"Notification Count: {_notificationCount}";
    }

    private void BtnToggleBadgeVisibility_Click(object sender, EventArgs e)
    {
        btnToggle.BadgeValues.Visible = !btnToggle.BadgeValues.Visible;
        lblToggleStatus.Values.Text = $"Badge Visible: {btnToggle.BadgeValues.Visible}";
    }

    private void BtnCyclePosition_Click(object sender, EventArgs e)
    {
        BadgePosition currentPosition = btnCyclePosition.BadgeValues.Position;
        BadgePosition newPosition = currentPosition switch
        {
            BadgePosition.TopRight => BadgePosition.TopLeft,
            BadgePosition.TopLeft => BadgePosition.BottomRight,
            BadgePosition.BottomRight => BadgePosition.BottomLeft,
            BadgePosition.BottomLeft => BadgePosition.TopRight,
            _ => BadgePosition.TopRight
        };

        btnCyclePosition.BadgeValues.Position = newPosition;
        lblPositionStatus.Values.Text = $"Position: {newPosition}";
    }

    private void BtnCycleColors_Click(object sender, EventArgs e)
    {
        Color currentColor = btnCycleColors.BadgeValues.BadgeColor;
        (Color newBadgeColor, Color newTextColor) = currentColor switch
        {
            Color c when c == Color.Red => (Color.Blue, Color.White),
            Color c when c == Color.Blue => (Color.Green, Color.White),
            Color c when c == Color.Green => (Color.Orange, Color.White),
            Color c when c == Color.Orange => (Color.Purple, Color.Yellow),
            _ => (Color.Red, Color.White)
        };

        btnCycleColors.BadgeValues.BadgeColor = newBadgeColor;
        btnCycleColors.BadgeValues.TextColor = newTextColor;
        lblColorStatus.Values.Text = $"Badge Color: {newBadgeColor.Name}";
    }

    private void BtnUpdateText_Click(object sender, EventArgs e)
    {
        if (int.TryParse(txtBadgeText.Text, out int value))
        {
            if (value < 0)
            {
                value = 0;
            }
            else if (value > 99)
            {
                value = 99;
                btnUpdateText.BadgeValues.Text = "99+";
            }
            else
            {
                btnUpdateText.BadgeValues.Text = value.ToString();
            }
            btnUpdateText.BadgeValues.Visible = value > 0;
            txtBadgeText.Text = value.ToString();
        }
        else if (!string.IsNullOrWhiteSpace(txtBadgeText.Text))
        {
            btnUpdateText.BadgeValues.Text = txtBadgeText.Text;
            btnUpdateText.BadgeValues.Visible = true;
        }
        else
        {
            btnUpdateText.BadgeValues.Visible = false;
        }
    }

    private void PropertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
    {
        // Refresh the form when properties change
        Refresh();
    }

    private void BtnCheckButton_Click(object sender, EventArgs e)
    {
        // Demonstrate badge on check button - toggle visibility
        checkBtnWithBadge.BadgeValues.Visible = !checkBtnWithBadge.BadgeValues.Visible;
    }

    private void BadgeTest_Load(object sender, EventArgs e)
    {
        // Initialize check button badge with image
        checkBtnWithBadge.BadgeValues.BadgeImage = SystemIcons.Shield.ToBitmap();
        checkBtnWithBadge.BadgeValues.Visible = true;
        checkBtnWithBadge.BadgeValues.BadgeColor = Color.Green;
        checkBtnWithBadge.BadgeValues.Text = ""; // Clear text when using image
        checkBtnWithBadge.BadgeValues.Position = BadgePosition.TopRight;

        // Initialize toggle button badge
        btnToggle.BadgeValues.Text = "ON";
        btnToggle.BadgeValues.Visible = true;
        btnToggle.BadgeValues.BadgeColor = Color.Blue;
        btnToggle.BadgeValues.TextColor = Color.White;

        // Initialize cycle position button
        btnCyclePosition.BadgeValues.Text = "5";
        btnCyclePosition.BadgeValues.Visible = true;
        btnCyclePosition.BadgeValues.Position = BadgePosition.TopRight;
        btnCyclePosition.BadgeValues.BadgeColor = Color.Red;
        btnCyclePosition.BadgeValues.TextColor = Color.White;

        // Initialize cycle colors button
        btnCycleColors.BadgeValues.Text = "8";
        btnCycleColors.BadgeValues.Visible = true;
        btnCycleColors.BadgeValues.BadgeColor = Color.Red;
        btnCycleColors.BadgeValues.TextColor = Color.White;

        // Initialize update text button
        btnUpdateText.BadgeValues.Text = "42";
        btnUpdateText.BadgeValues.Visible = true;
        btnUpdateText.BadgeValues.BadgeColor = Color.Orange;
        btnUpdateText.BadgeValues.TextColor = Color.White;

        UpdateNotificationLabel();
        lblToggleStatus.Values.Text = $"Badge Visible: {btnToggle.BadgeValues.Visible}";
        lblPositionStatus.Values.Text = $"Position: {btnCyclePosition.BadgeValues.Position}";
        lblColorStatus.Values.Text = $"Badge Color: {btnCycleColors.BadgeValues.BadgeColor.Name}";

        // Setup image examples
        SetupImageExamples();
    }

    private void SetupImageExamples()
    {
        // Error icon badge
        btnImageError.BadgeValues.BadgeImage = SystemIcons.Error.ToBitmap();
        btnImageError.BadgeValues.Visible = true;
        btnImageError.BadgeValues.BadgeColor = Color.Red;
        btnImageError.BadgeValues.Text = "";

        // Warning icon badge
        btnImageWarning.BadgeValues.BadgeImage = SystemIcons.Warning.ToBitmap();
        btnImageWarning.BadgeValues.Visible = true;
        btnImageWarning.BadgeValues.BadgeColor = Color.Orange;
        btnImageWarning.BadgeValues.Text = "";

        // Info icon badge
        btnImageInfo.BadgeValues.BadgeImage = SystemIcons.Information.ToBitmap();
        btnImageInfo.BadgeValues.Visible = true;
        btnImageInfo.BadgeValues.BadgeColor = Color.Blue;
        btnImageInfo.BadgeValues.Text = "";

        // Question icon badge
        btnImageQuestion.BadgeValues.BadgeImage = SystemIcons.Question.ToBitmap();
        btnImageQuestion.BadgeValues.Visible = true;
        btnImageQuestion.BadgeValues.BadgeColor = Color.Green;
        btnImageQuestion.BadgeValues.Text = "";
    }

    private void SetupShapeExamples()
    {
        // Demonstrate different badge shapes on existing buttons
        // Circle (default) - already shown in other examples

        // Square shape example on blue badge
        btnBlueBadge.BadgeValues.Shape = BadgeShape.Square;

        // Rounded rectangle shape example on green badge
        btnGreenBadge.BadgeValues.Shape = BadgeShape.RoundedRectangle;
    }

    private void SetupAnimationExamples()
    {
        // Demonstrate fade in/out animation on alerts button
        btnAlerts.BadgeValues.Animation = BadgeAnimation.FadeInOut;

        // Demonstrate pulse animation on messages button
        btnMessages.BadgeValues.Animation = BadgeAnimation.Pulse;

        // Custom badge with pulse animation
        btnCustomBadge.BadgeValues.Animation = BadgeAnimation.Pulse;
    }

    private void SetupFontExamples()
    {
        // Demonstrate custom font on red badge
        btnRedBadge.BadgeValues.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Point);

        // Demonstrate larger font on notification button
        btnNotifications.BadgeValues.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold, GraphicsUnit.Point);

        // Custom font on bottom right button
        btnBottomRight.BadgeValues.Font = new Font("Consolas", 7f, FontStyle.Bold, GraphicsUnit.Point);
    }
}