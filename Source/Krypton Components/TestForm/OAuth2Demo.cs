#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.Windows.Forms;

using Krypton.Toolkit;
using Krypton.Utilities;

namespace TestForm;

/// <summary>
/// Comprehensive OAuth2 PKCE demonstration. Configure provider (Azure AD, Google, GitHub),
/// client credentials, and sign in with embedded WebView2 or system browser.
/// Requires an app registration and redirect URI configured with your OAuth2 provider.
/// </summary>
public partial class OAuth2Demo : KryptonForm
{
    private OAuth2TokenResponse? _lastTokens;

    public OAuth2Demo()
    {
        InitializeComponent();
    }

    private void OAuth2Demo_Load(object? sender, EventArgs e)
    {
        _cmbProvider.SelectedIndex = 0;
        OnProviderChanged(null, EventArgs.Empty);
    }

    private void OnProviderChanged(object? sender, EventArgs e)
    {
        var isAzure = _cmbProvider.SelectedIndex == 0;
        _panelTenant.Visible = isAzure;

        if (isAzure)
        {
            _txtScopes.Text = "openid profile";
            _txtRedirectUri.Text = "com.krypton.oauth2.demo://callback";
        }
        else if (_cmbProvider.SelectedIndex == 1)
        {
            _txtScopes.Text = "openid email profile";
            _txtRedirectUri.Text = "com.krypton.oauth2.demo://callback";
        }
        else
        {
            _txtScopes.Text = "read:user";
            _txtRedirectUri.Text = "http://localhost:8400/callback";
        }
    }

    private OAuth2PkceOptions GetOptions()
    {
        var clientId = _txtClientId.Text.Trim();
        var redirectUri = _txtRedirectUri.Text.Trim();
        var scopes = _txtScopes.Text.Trim();

        return _cmbProvider.SelectedIndex switch
        {
            0 => OAuth2ProviderPresets.AzureAd(clientId, redirectUri, _txtTenantId.Text.Trim(), scopes),
            1 => OAuth2ProviderPresets.Google(clientId, redirectUri, scopes),
            2 => OAuth2ProviderPresets.GitHub(clientId, redirectUri, scopes),
            _ => OAuth2ProviderPresets.AzureAd(clientId, redirectUri, "common", scopes)
        };
    }

    private IOAuth2BrowserHost? GetBrowserHost()
    {
        if (!_chkSystemBrowser.Checked)
        {
            return null;
        }

        var redirectUri = _txtRedirectUri.Text.Trim();
        if (!redirectUri.StartsWith("http://localhost", StringComparison.OrdinalIgnoreCase) &&
            !redirectUri.StartsWith("http://127.0.0.1", StringComparison.OrdinalIgnoreCase))
        {
            KryptonMessageBox.Show(
                "System browser mode requires redirect URI to be http://localhost or http://127.0.0.1.",
                "Configuration",
                KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Warning);
            return null;
        }

        return new OAuth2SystemBrowserHost(new OAuth2HttpListenerRedirectHandler());
    }

    private async void OnSignInClick(object? sender, EventArgs e)
    {
        var clientId = _txtClientId.Text.Trim();
        var redirectUri = _txtRedirectUri.Text.Trim();

        if (string.IsNullOrEmpty(clientId))
        {
            KryptonMessageBox.Show("Please enter a Client ID.", "Configuration", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrEmpty(redirectUri))
        {
            KryptonMessageBox.Show("Please enter a Redirect URI.", "Configuration", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
            return;
        }

        _btnSignIn.Enabled = false;
        _lblResult.Text = "Signing in...";
        Application.DoEvents();

        try
        {
            var options = GetOptions();
            var browserHost = GetBrowserHost();
            var client = new OAuth2PkceClient(options, browserHost);

            var tokens = await client.AuthorizeWithBrowserAsync(this, "Sign in to OAuth2 Demo");

            _lastTokens = tokens;
            _btnRefresh.Enabled = tokens.HasRefreshToken;

            _lblResult.Text = "Sign-in successful!\n\n" +
                              $"Access token: {(string.IsNullOrEmpty(tokens.AccessToken) ? "(none)" : tokens.AccessToken.Length > 20 ? tokens.AccessToken.Substring(20) + "..." : tokens.AccessToken)}\n" +
                              $"Token type: {tokens.TokenType ?? "N/A"}\n" +
                              $"Expires in: {tokens.ExpiresIn} seconds\n" +
                              $"Refresh token: {(tokens.HasRefreshToken ? "Yes" : "No")}";
        }
        catch (OAuth2Exception ex)
        {
            _lblResult.Text = $"Sign-in failed: {ex.Message}";
            if (!string.IsNullOrEmpty(ex.Error))
            {
                _lblResult.Text += $"\nError code: {ex.Error}";
            }
        }
        catch (Exception ex)
        {
            _lblResult.Text = $"Error: {ex.Message}";
        }
        finally
        {
            _btnSignIn.Enabled = true;
        }
    }

    private async void OnRefreshClick(object? sender, EventArgs e)
    {
        if (_lastTokens == null || !_lastTokens.HasRefreshToken)
        {
            return;
        }

        _btnRefresh.Enabled = false;
        _lblResult.Text = "Refreshing token...";
        Application.DoEvents();

        try
        {
            var options = GetOptions();
            var client = new OAuth2PkceClient(options);
            var tokens = await client.RefreshTokenAsync(_lastTokens.RefreshToken!);

            _lastTokens = tokens;
            _btnRefresh.Enabled = tokens.HasRefreshToken;

            _lblResult.Text = "Token refreshed successfully!\n\n" +
                              $"Access token: {(string.IsNullOrEmpty(tokens.AccessToken) ? "(none)" : tokens.AccessToken!.Length > 20 ? tokens.AccessToken.Substring(20) + "..." : tokens.AccessToken)}\n" +
                              $"Expires in: {tokens.ExpiresIn} seconds";
        }
        catch (OAuth2Exception ex)
        {
            _lblResult.Text = $"Refresh failed: {ex.Message}";
        }
        catch (Exception ex)
        {
            _lblResult.Text = $"Error: {ex.Message}";
        }
        finally
        {
            _btnRefresh.Enabled = _lastTokens?.HasRefreshToken ?? false;
        }
    }
}
