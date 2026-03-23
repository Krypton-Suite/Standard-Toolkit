#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Displays an OAuth2 sign-in dialog using a Krypton-styled form with WebView2.
/// The API mimics <see cref="Krypton.Toolkit.KryptonMessageBox"/> for consistent usage.
/// </summary>
/// <remarks>
/// <para>
/// Requires WebView2 to be available. Shows a modal dialog that navigates to the authorization URL
/// and intercepts the redirect to extract the authorization code or error.
/// </para>
/// <para>
/// Example:
/// <code>
/// var result = await KryptonOAuth2Login.ShowAsync(authorizationUrl, redirectUri, "Sign in", this);
/// if (result.Success)
///     // Use result.Code for token exchange
/// else
///     // Show result.ErrorMessage to user
/// </code>
/// </para>
/// </remarks>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonOAuth2Login
{
    /// <summary>
    /// Displays the OAuth2 sign-in dialog and returns when the user completes or cancels.
    /// </summary>
    /// <param name="authorizationUrl">The full OAuth2 authorization URL to display.</param>
    /// <param name="redirectUri">The redirect URI. When the provider redirects here, the dialog closes and returns the result.</param>
    /// <returns>The authorization result with code, state, or error message.</returns>
    public static Task<OAuth2AuthorizationResult> ShowAsync(string authorizationUrl, string redirectUri) =>
        ShowCore(null, authorizationUrl, redirectUri, "Sign in");

    /// <summary>
    /// Displays the OAuth2 sign-in dialog and returns when the user completes or cancels.
    /// </summary>
    /// <param name="authorizationUrl">The full OAuth2 authorization URL to display.</param>
    /// <param name="redirectUri">The redirect URI. When the provider redirects here, the dialog closes and returns the result.</param>
    /// <param name="title">The dialog title. Default is "Sign in".</param>
    /// <returns>The authorization result with code, state, or error message.</returns>
    public static Task<OAuth2AuthorizationResult> ShowAsync(string authorizationUrl, string redirectUri, string title) =>
        ShowCore(null, authorizationUrl, redirectUri, title ?? "Sign in");

    /// <summary>
    /// Displays the OAuth2 sign-in dialog in front of the specified owner and returns when the user completes or cancels.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog. Can be null.</param>
    /// <param name="authorizationUrl">The full OAuth2 authorization URL to display.</param>
    /// <param name="redirectUri">The redirect URI. When the provider redirects here, the dialog closes and returns the result.</param>
    /// <returns>The authorization result with code, state, or error message.</returns>
    public static Task<OAuth2AuthorizationResult> ShowAsync(IWin32Window? owner, string authorizationUrl, string redirectUri) =>
        ShowCore(owner, authorizationUrl, redirectUri, "Sign in");

    /// <summary>
    /// Displays the OAuth2 sign-in dialog in front of the specified owner and returns when the user completes or cancels.
    /// </summary>
    /// <param name="owner">Owner of the modal dialog. Can be null.</param>
    /// <param name="authorizationUrl">The full OAuth2 authorization URL to display.</param>
    /// <param name="redirectUri">The redirect URI. When the provider redirects here, the dialog closes and returns the result.</param>
    /// <param name="title">The dialog title. Default is "Sign in".</param>
    /// <returns>The authorization result with code, state, or error message.</returns>
    public static Task<OAuth2AuthorizationResult> ShowAsync(IWin32Window? owner, string authorizationUrl, string redirectUri, string title) =>
        ShowCore(owner, authorizationUrl, redirectUri, title ?? "Sign in");

#if WEBVIEW2_AVAILABLE
    private static async Task<OAuth2AuthorizationResult> ShowCore(IWin32Window? owner, string authorizationUrl, string redirectUri, string title)
    {
        using var form = new VisualOAuth2LoginForm(authorizationUrl, redirectUri, title);
        var resultTask = form.RunAsync(owner);

        if (owner is Form ownerForm)
        {
            form.ShowDialog(ownerForm);
        }
        else
        {
            form.Show();
        }

        return await resultTask.ConfigureAwait(true);
    }
#else
    private static Task<OAuth2AuthorizationResult> ShowCore(IWin32Window? owner, string authorizationUrl, string redirectUri, string title)
    {
        return Task.FromResult(new OAuth2AuthorizationResult(
            false,
            null,
            null,
            "OAuth2 sign-in requires WebView2. Ensure WebView2 Runtime is installed (run Scripts\\WebVew2\\Populate-BundledWebView2.cmd)."));
    }
#endif
}
