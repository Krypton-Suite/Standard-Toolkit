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
        btnTopRight.BadgeValues.BadgeContentValues.Text = "3";
        btnTopRight.BadgeValues.BadgeContentValues.Visible = true;
        btnTopRight.BadgeValues.BadgeContentValues.Position = BadgePosition.TopRight;
        btnTopRight.BadgeValues.BadgeColorValues.BadgeColor = Color.Red;
        btnTopRight.BadgeValues.BadgeColorValues.TextColor = Color.White;

        // Top Left
        btnTopLeft.BadgeValues.BadgeContentValues.Text = "12";
        btnTopLeft.BadgeValues.BadgeContentValues.Visible = true;
        btnTopLeft.BadgeValues.BadgeContentValues.Position = BadgePosition.TopLeft;
        btnTopLeft.BadgeValues.BadgeColorValues.BadgeColor = Color.Blue;
        btnTopLeft.BadgeValues.BadgeColorValues.TextColor = Color.White;

        // Bottom Right
        btnBottomRight.BadgeValues.BadgeContentValues.Text = "99+";
        btnBottomRight.BadgeValues.BadgeContentValues.Visible = true;
        btnBottomRight.BadgeValues.BadgeContentValues.Position = BadgePosition.BottomRight;
        btnBottomRight.BadgeValues.BadgeColorValues.BadgeColor = Color.Green;
        btnBottomRight.BadgeValues.BadgeColorValues.TextColor = Color.White;

        // Bottom Left
        btnBottomLeft.BadgeValues.BadgeContentValues.Text = "!";
        btnBottomLeft.BadgeValues.BadgeContentValues.Visible = true;
        btnBottomLeft.BadgeValues.BadgeContentValues.Position = BadgePosition.BottomLeft;
        btnBottomLeft.BadgeValues.BadgeColorValues.BadgeColor = Color.Orange;
        btnBottomLeft.BadgeValues.BadgeColorValues.TextColor = Color.White;
    }

    private void SetupColorExamples()
    {
        // Red badge with text
        btnRedBadge.BadgeValues.BadgeContentValues.Text = "5";
        btnRedBadge.BadgeValues.BadgeContentValues.Visible = true;
        btnRedBadge.BadgeValues.BadgeColorValues.BadgeColor = Color.Red;
        btnRedBadge.BadgeValues.BadgeColorValues.TextColor = Color.White;

        // Blue badge with text
        btnBlueBadge.BadgeValues.BadgeContentValues.Text = "8";
        btnBlueBadge.BadgeValues.BadgeContentValues.Visible = true;
        btnBlueBadge.BadgeValues.BadgeColorValues.BadgeColor = Color.Blue;
        btnBlueBadge.BadgeValues.BadgeColorValues.TextColor = Color.White;

        // Green badge with text
        btnGreenBadge.BadgeValues.BadgeContentValues.Text = "12";
        btnGreenBadge.BadgeValues.BadgeContentValues.Visible = true;
        btnGreenBadge.BadgeValues.BadgeColorValues.BadgeColor = Color.Green;
        btnGreenBadge.BadgeValues.BadgeColorValues.TextColor = Color.White;

        // Custom color badge with image (using SystemIcons)
        btnCustomBadge.BadgeValues.BadgeContentValues.BadgeImage = SystemIcons.Information.ToBitmap();
        btnCustomBadge.BadgeValues.BadgeContentValues.Visible = true;
        btnCustomBadge.BadgeValues.BadgeColorValues.BadgeColor = Color.Purple;
        btnCustomBadge.BadgeValues.BadgeContentValues.Text = ""; // Clear text when using image
    }

    private void SetupNotificationExamples()
    {
        // Notification counter
        btnNotifications.BadgeValues.BadgeContentValues.Text = _notificationCount.ToString();
        btnNotifications.BadgeValues.BadgeContentValues.Visible = _notificationCount > 0;
        btnNotifications.BadgeValues.BadgeColorValues.BadgeColor = Color.Red;
        btnNotifications.BadgeValues.BadgeColorValues.TextColor = Color.White;

        // Message badge
        btnMessages.BadgeValues.BadgeContentValues.Text = "3";
        btnMessages.BadgeValues.BadgeContentValues.Visible = true;
        btnMessages.BadgeValues.BadgeColorValues.BadgeColor = Color.DarkBlue;
        btnMessages.BadgeValues.BadgeColorValues.TextColor = Color.White;

        // Alert badge with image
        btnAlerts.BadgeValues.BadgeContentValues.BadgeImage = SystemIcons.Warning.ToBitmap();
        btnAlerts.BadgeValues.BadgeContentValues.Visible = true;
        btnAlerts.BadgeValues.BadgeColorValues.BadgeColor = Color.Orange;
        btnAlerts.BadgeValues.BadgeContentValues.Text = ""; // Clear text when using image
    }

    private void SetupInteractiveExamples()
    {
        // Interactive button - properties can be modified in property grid
        btnInteractive.BadgeValues.BadgeContentValues.Text = "42";
        btnInteractive.BadgeValues.BadgeContentValues.Visible = true;
        btnInteractive.BadgeValues.BadgeContentValues.Position = BadgePosition.TopRight;
        btnInteractive.BadgeValues.BadgeColorValues.BadgeColor = Color.Red;
        btnInteractive.BadgeValues.BadgeColorValues.TextColor = Color.White;
    }

    private void BtnIncrementNotifications_Click(object sender, EventArgs e)
    {
        _notificationCount++;
        btnNotifications.BadgeValues.BadgeContentValues.Text = _notificationCount.ToString();
        btnNotifications.BadgeValues.BadgeContentValues.Visible = true;
        UpdateNotificationLabel();
    }

    private void BtnDecrementNotifications_Click(object sender, EventArgs e)
    {
        if (_notificationCount > 0)
        {
            _notificationCount--;
            btnNotifications.BadgeValues.BadgeContentValues.Text = _notificationCount.ToString();
            btnNotifications.BadgeValues.BadgeContentValues.Visible = _notificationCount > 0;
        }
        UpdateNotificationLabel();
    }

    private void BtnClearNotifications_Click(object sender, EventArgs e)
    {
        _notificationCount = 0;
        btnNotifications.BadgeValues.BadgeContentValues.Visible = false;
        UpdateNotificationLabel();
    }

    private void UpdateNotificationLabel()
    {
        lblNotificationCount.Values.Text = $"Notification Count: {_notificationCount}";
    }

    private void BtnToggleBadgeVisibility_Click(object sender, EventArgs e)
    {
        btnToggle.BadgeValues.BadgeContentValues.Visible = !btnToggle.BadgeValues.BadgeContentValues.Visible;
        lblToggleStatus.Values.Text = $"Badge Visible: {btnToggle.BadgeValues.BadgeContentValues.Visible}";
    }

    private void BtnCyclePosition_Click(object sender, EventArgs e)
    {
        BadgePosition currentPosition = btnCyclePosition.BadgeValues.BadgeContentValues.Position;
        BadgePosition newPosition = currentPosition switch
        {
            BadgePosition.TopRight => BadgePosition.TopLeft,
            BadgePosition.TopLeft => BadgePosition.BottomRight,
            BadgePosition.BottomRight => BadgePosition.BottomLeft,
            BadgePosition.BottomLeft => BadgePosition.TopRight,
            _ => BadgePosition.TopRight
        };

        btnCyclePosition.BadgeValues.BadgeContentValues.Position = newPosition;
        lblPositionStatus.Values.Text = $"Position: {newPosition}";
    }

    private void BtnCycleColors_Click(object sender, EventArgs e)
    {
        Color currentColor = btnCycleColors.BadgeValues.BadgeColorValues.BadgeColor;
        (Color newBadgeColor, Color newTextColor) = currentColor switch
        {
            Color c when c == Color.Red => (Color.Blue, Color.White),
            Color c when c == Color.Blue => (Color.Green, Color.White),
            Color c when c == Color.Green => (Color.Orange, Color.White),
            Color c when c == Color.Orange => (Color.Purple, Color.Yellow),
            _ => (Color.Red, Color.White)
        };

        btnCycleColors.BadgeValues.BadgeColorValues.BadgeColor = newBadgeColor;
        btnCycleColors.BadgeValues.BadgeColorValues.TextColor = newTextColor;
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
                btnUpdateText.BadgeValues.BadgeContentValues.Text = "99+";
            }
            else
            {
                btnUpdateText.BadgeValues.BadgeContentValues.Text = value.ToString();
            }
            btnUpdateText.BadgeValues.BadgeContentValues.Visible = value > 0;
            txtBadgeText.Text = value.ToString();
        }
        else if (!string.IsNullOrWhiteSpace(txtBadgeText.Text))
        {
            btnUpdateText.BadgeValues.BadgeContentValues.Text = txtBadgeText.Text;
            btnUpdateText.BadgeValues.BadgeContentValues.Visible = true;
        }
        else
        {
            btnUpdateText.BadgeValues.BadgeContentValues.Visible = false;
        }
    }

    private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
        // Refresh the form when properties change
        Refresh();
    }

    private void BtnCheckButton_Click(object sender, EventArgs e)
    {
        // Demonstrate badge on check button - toggle visibility
        checkBtnWithBadge.BadgeValues.BadgeContentValues.Visible = !checkBtnWithBadge.BadgeValues.BadgeContentValues.Visible;
    }

    private void BadgeTest_Load(object sender, EventArgs e)
    {
        // Initialize check button badge with image
        checkBtnWithBadge.BadgeValues.BadgeContentValues.BadgeImage = SystemIcons.Shield.ToBitmap();
        checkBtnWithBadge.BadgeValues.BadgeContentValues.Visible = true;
        checkBtnWithBadge.BadgeValues.BadgeColorValues.BadgeColor = Color.Green;
        checkBtnWithBadge.BadgeValues.BadgeContentValues.Text = ""; // Clear text when using image
        checkBtnWithBadge.BadgeValues.BadgeContentValues.Position = BadgePosition.TopRight;

        // Initialize toggle button badge
        btnToggle.BadgeValues.BadgeContentValues.Text = "ON";
        btnToggle.BadgeValues.BadgeContentValues.Visible = true;
        btnToggle.BadgeValues.BadgeColorValues.BadgeColor = Color.Blue;
        btnToggle.BadgeValues.BadgeColorValues.TextColor = Color.White;

        // Initialize cycle position button
        btnCyclePosition.BadgeValues.BadgeContentValues.Text = "5";
        btnCyclePosition.BadgeValues.BadgeContentValues.Visible = true;
        btnCyclePosition.BadgeValues.BadgeContentValues.Position = BadgePosition.TopRight;
        btnCyclePosition.BadgeValues.BadgeColorValues.BadgeColor = Color.Red;
        btnCyclePosition.BadgeValues.BadgeColorValues.TextColor = Color.White;

        // Initialize cycle colors button
        btnCycleColors.BadgeValues.BadgeContentValues.Text = "8";
        btnCycleColors.BadgeValues.BadgeContentValues.Visible = true;
        btnCycleColors.BadgeValues.BadgeColorValues.BadgeColor = Color.Red;
        btnCycleColors.BadgeValues.BadgeColorValues.TextColor = Color.White;

        // Initialize update text button
        btnUpdateText.BadgeValues.BadgeContentValues.Text = "42";
        btnUpdateText.BadgeValues.BadgeContentValues.Visible = true;
        btnUpdateText.BadgeValues.BadgeColorValues.BadgeColor = Color.Orange;
        btnUpdateText.BadgeValues.BadgeColorValues.TextColor = Color.White;

        UpdateNotificationLabel();
        lblToggleStatus.Values.Text = $"Badge Visible: {btnToggle.BadgeValues.BadgeContentValues.Visible}";
        lblPositionStatus.Values.Text = $"Position: {btnCyclePosition.BadgeValues.BadgeContentValues.Position}";
        lblColorStatus.Values.Text = $"Badge Color: {btnCycleColors.BadgeValues.BadgeColorValues.BadgeColor.Name}";

        // Setup image examples
        SetupImageExamples();
    }

    private void SetupImageExamples()
    {
        // Error icon badge
        btnImageError.BadgeValues.BadgeContentValues.BadgeImage = SystemIcons.Error.ToBitmap();
        btnImageError.BadgeValues.BadgeContentValues.Visible = true;
        btnImageError.BadgeValues.BadgeColorValues.BadgeColor = Color.Red;
        btnImageError.BadgeValues.BadgeContentValues.Text = "";

        // Warning icon badge
        btnImageWarning.BadgeValues.BadgeContentValues.BadgeImage = SystemIcons.Warning.ToBitmap();
        btnImageWarning.BadgeValues.BadgeContentValues.Visible = true;
        btnImageWarning.BadgeValues.BadgeColorValues.BadgeColor = Color.Orange;
        btnImageWarning.BadgeValues.BadgeContentValues.Text = "";

        // Info icon badge
        btnImageInfo.BadgeValues.BadgeContentValues.BadgeImage = SystemIcons.Information.ToBitmap();
        btnImageInfo.BadgeValues.BadgeContentValues.Visible = true;
        btnImageInfo.BadgeValues.BadgeColorValues.BadgeColor = Color.Blue;
        btnImageInfo.BadgeValues.BadgeContentValues.Text = "";

        // Question icon badge
        btnImageQuestion.BadgeValues.BadgeContentValues.BadgeImage = SystemIcons.Question.ToBitmap();
        btnImageQuestion.BadgeValues.BadgeContentValues.Visible = true;
        btnImageQuestion.BadgeValues.BadgeColorValues.BadgeColor = Color.Green;
        btnImageQuestion.BadgeValues.BadgeContentValues.Text = "";
    }

    private void SetupShapeExamples()
    {
        // Demonstrate different badge shapes on existing buttons
        // Circle (default) - already shown in other examples

        // Square shape example on blue badge
        btnBlueBadge.BadgeValues.BadgeContentValues.Shape = BadgeShape.Square;

        // Rounded rectangle shape example on green badge
        btnGreenBadge.BadgeValues.BadgeContentValues.Shape = BadgeShape.RoundedRectangle;
    }

    private void SetupAnimationExamples()
    {
        // Demonstrate fade in/out animation on alerts button
        btnAlerts.BadgeValues.BadgeContentValues.Animation = BadgeAnimation.FadeInOut;

        // Demonstrate pulse animation on messages button
        btnMessages.BadgeValues.BadgeContentValues.Animation = BadgeAnimation.Pulse;

        // Custom badge with pulse animation
        btnCustomBadge.BadgeValues.BadgeContentValues.Animation = BadgeAnimation.Pulse;
    }

    private void SetupFontExamples()
    {
        // Demonstrate custom font on red badge
        btnRedBadge.BadgeValues.BadgeContentValues.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Point);

        // Demonstrate larger font on notification button
        btnNotifications.BadgeValues.BadgeContentValues.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold, GraphicsUnit.Point);

        // Custom font on bottom right button
        btnBottomRight.BadgeValues.BadgeContentValues.Font = new Font("Consolas", 7f, FontStyle.Bold, GraphicsUnit.Point);
    }
}