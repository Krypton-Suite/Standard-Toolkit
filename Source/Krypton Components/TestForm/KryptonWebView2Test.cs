#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

#if WEBVIEW2_AVAILABLE
using Microsoft.Web.WebView2.Core;
#endif

namespace TestForm;

#if WEBVIEW2_AVAILABLE
/// <summary>
/// Test form demonstrating the KryptonWebView2 control functionality.
/// 
/// <para>
/// This form showcases the key features of the KryptonWebView2 control including:
/// - Control initialization and error handling
/// - Navigation with address bar
/// - Navigation controls (Back, Forward, Refresh)
/// - Integration with Krypton theming
/// - Proper async/await patterns for WebView2 operations
/// </para>
/// 
/// <para>
/// The form includes a toolbar with navigation controls and an address bar,
/// while the main area displays the WebView2 control. Error handling is
/// demonstrated for WebView2 initialization failures.
/// </para>
/// </summary>
public partial class KryptonWebView2Test : KryptonForm
{
    public KryptonWebView2Test()
    {
        InitializeComponent();
        InitializeWebView2();
    }

    /// <summary>
    /// Initializes the WebView2 control asynchronously and navigates to a default page.
    /// </summary>
    /// <remarks>
    /// This method demonstrates proper WebView2 initialization with error handling.
    /// If initialization fails, the WebView2 control is hidden and an error message
    /// is displayed to the user with instructions to install the WebView2 Runtime.
    /// </remarks>
    private async void InitializeWebView2()
    {
        try
        {
            // Initialize the WebView2 control
            await kryptonWebView21.EnsureCoreWebView2Async();
            
            // Navigate to a test page
            kryptonWebView21.CoreWebView2?.Navigate("https://www.microsoft.com");
        }
        catch (Exception ex)
        {
            // If WebView2 fails to initialize, show a message
            kryptonWebView21.Visible = false;
            kryptonLabel1.Text = $"WebView2 initialization failed: {ex.Message}\n\nPlease ensure WebView2 Runtime is installed.";
            kryptonLabel1.Visible = true;
        }
    }

    /// <summary>
    /// Handles the Navigate button click event.
    /// </summary>
    /// <param name="sender">The button that raised the event.</param>
    /// <param name="e">Event arguments.</param>
    /// <remarks>
    /// This method demonstrates navigation with URL validation and protocol handling.
    /// If the entered URL doesn't have a protocol, HTTPS is automatically added.
    /// Navigation errors are caught and displayed to the user.
    /// </remarks>
    private void kbtnNavigate_Click(object sender, EventArgs e)
    {
        try
        {
            var url = kryptonTextBox1.Text.Trim();
            if (!string.IsNullOrEmpty(url))
            {
                // Add protocol if not present
                if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                {
                    url = "https://" + url;
                }
                
                kryptonWebView21.CoreWebView2?.Navigate(url);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Navigation error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Handles the Back button click event.
    /// </summary>
    /// <param name="sender">The button that raised the event.</param>
    /// <param name="e">Event arguments.</param>
    /// <remarks>
    /// Demonstrates navigation history management by going back to the previous page.
    /// </remarks>
    private void kbtnBack_Click(object sender, EventArgs e)
    {
        try
        {
            kryptonWebView21.CoreWebView2?.GoBack();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Navigation error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Handles the Forward button click event.
    /// </summary>
    /// <param name="sender">The button that raised the event.</param>
    /// <param name="e">Event arguments.</param>
    /// <remarks>
    /// Demonstrates navigation history management by going forward to the next page.
    /// </remarks>
    private void kbtnForward_Click(object sender, EventArgs e)
    {
        try
        {
            kryptonWebView21.CoreWebView2?.GoForward();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Navigation error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Handles the Refresh button click event.
    /// </summary>
    /// <param name="sender">The button that raised the event.</param>
    /// <param name="e">Event arguments.</param>
    /// <remarks>
    /// Demonstrates page reload functionality by refreshing the current page.
    /// </remarks>
    private void kbtnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            kryptonWebView21.CoreWebView2?.Reload();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Navigation error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Handles the NavigationCompleted event from the WebView2 control.
    /// </summary>
    /// <param name="sender">The WebView2 control that raised the event.</param>
    /// <param name="e">Navigation completion event arguments.</param>
    /// <remarks>
    /// This method demonstrates how to handle navigation completion events.
    /// On successful navigation, the address bar is updated with the current URL.
    /// This provides visual feedback to the user about the current page location.
    /// </remarks>
    private void kryptonWebView21_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
    {
        // Update the address bar with the current URL
        if (e.IsSuccess && kryptonWebView21.CoreWebView2 != null)
        {
            kryptonTextBox1.Text = kryptonWebView21.CoreWebView2.Source;
        }
    }
}
#endif