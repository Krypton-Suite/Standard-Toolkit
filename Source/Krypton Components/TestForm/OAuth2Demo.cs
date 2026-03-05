#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.Drawing;
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
    private KryptonComboBox _cmbProvider = null!;
    private KryptonTextBox _txtClientId = null!;
    private KryptonTextBox _txtRedirectUri = null!;
    private KryptonTextBox _txtScopes = null!;
    private KryptonTextBox _txtTenantId = null!;
    private KryptonCheckBox _chkSystemBrowser = null!;
    private KryptonButton _btnSignIn = null!;
    private KryptonButton _btnRefresh = null!;
    private KryptonLabel _lblResult = null!;
    private KryptonPanel _panelTenant = null!;
    private OAuth2TokenResponse? _lastTokens;

    public OAuth2Demo()
    {
        InitializeComponent();
        BuildLayout();
    }

    private void OAuth2Demo_Load(object? sender, EventArgs e)
    {
        _cmbProvider.SelectedIndex = 0;
        OnProviderChanged(null, EventArgs.Empty);
    }

    private void BuildLayout()
    {
        var mainPanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            PanelBackStyle = PaletteBackStyle.ControlClient
        };

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 0,
            Padding = Padding.Empty
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        int row = 0;

        // Provider
        layout.RowCount = row + 1;
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        layout.Controls.Add(new KryptonLabel { Text = "Provider", Anchor = AnchorStyles.Left }, 0, row);
        _cmbProvider = new KryptonComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Anchor = AnchorStyles.Left | AnchorStyles.Right
        };
        _cmbProvider.Items.AddRange(["Azure AD", "Google", "GitHub"]);
        _cmbProvider.SelectedIndexChanged += OnProviderChanged;
        layout.Controls.Add(_cmbProvider, 1, row);
        row++;

        // Tenant (Azure only)
        layout.RowCount = row + 1;
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        layout.Controls.Add(new KryptonLabel { Text = "Tenant ID", Anchor = AnchorStyles.Left }, 0, row);
        _panelTenant = new KryptonPanel { Dock = DockStyle.Fill, PanelBackStyle = PaletteBackStyle.ControlClient };
        _txtTenantId = new KryptonTextBox
        {
            Text = "common",
            Anchor = AnchorStyles.Left | AnchorStyles.Right,
            Margin = Padding.Empty
        };
        _panelTenant.Controls.Add(_txtTenantId);
        layout.Controls.Add(_panelTenant, 1, row);
        row++;

        // Client ID
        layout.RowCount = row + 1;
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        layout.Controls.Add(new KryptonLabel { Text = "Client ID", Anchor = AnchorStyles.Left }, 0, row);
        _txtClientId = new KryptonTextBox
        {
            Anchor = AnchorStyles.Left | AnchorStyles.Right,
            CueHint = { 
                CueHintText = "Application (client) ID from app registration"
                },
        };
        layout.Controls.Add(_txtClientId, 1, row);
        row++;

        // Redirect URI
        layout.RowCount = row + 1;
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        layout.Controls.Add(new KryptonLabel { Text = "Redirect URI", Anchor = AnchorStyles.Left }, 0, row);
        _txtRedirectUri = new KryptonTextBox
        {
            Text = "com.krypton.oauth2.demo://callback",
            Anchor = AnchorStyles.Left | AnchorStyles.Right,
            CueHint = { 
                CueHintText = "e.g. myapp://callback or http://localhost:8400/callback"
                },
        };
        layout.Controls.Add(_txtRedirectUri, 1, row);
        row++;

        // Scopes
        layout.RowCount = row + 1;
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        layout.Controls.Add(new KryptonLabel { Text = "Scopes", Anchor = AnchorStyles.Left }, 0, row);
        _txtScopes = new KryptonTextBox
        {
            Text = "openid profile",
            Anchor = AnchorStyles.Left | AnchorStyles.Right,
            CueHint = {
                CueHintText = "Space-separated scopes"
                },
        };
        layout.Controls.Add(_txtScopes, 1, row);
        row++;

        // System browser
        layout.RowCount = row + 1;
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        layout.Controls.Add(new KryptonLabel { Text = string.Empty }, 0, row);
        _chkSystemBrowser = new KryptonCheckBox
        {
            Text = "Use system browser (HttpListener) instead of embedded WebView2",
            Checked = false,
            Anchor = AnchorStyles.Left
        };
        layout.Controls.Add(_chkSystemBrowser, 1, row);
        row++;

        // Buttons
        layout.RowCount = row + 1;
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        var btnPanel = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, Dock = DockStyle.Fill, Margin = Padding.Empty };
        _btnSignIn = new KryptonButton { Text = "Sign in", Size = new Size(120, 32) };
        _btnSignIn.Click += OnSignInClick;
        _btnRefresh = new KryptonButton { Text = "Refresh token", Size = new Size(120, 32), Enabled = false };
        _btnRefresh.Click += OnRefreshClick;
        btnPanel.Controls.Add(_btnSignIn);
        btnPanel.Controls.Add(_btnRefresh);
        layout.Controls.Add(btnPanel, 0, row);
        layout.SetColumnSpan(btnPanel, 2);
        row++;

        // Result
        layout.RowCount = row + 1;
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        _lblResult = new KryptonLabel
        {
            Text = "Enter your Client ID and Redirect URI (must be registered with the provider), then click Sign in.\n\n" +
                   "For Azure AD: Register an app at https://portal.azure.com → Azure Active Directory → App registrations.\n" +
                   "For Google: https://console.cloud.google.com/apis/credentials\n" +
                   "For GitHub: https://github.com/settings/developers",
            AutoSize = false,
            Dock = DockStyle.Fill,
            Padding = new Padding(0, 8, 0, 0),
            LabelStyle = LabelStyle.NormalControl
        };
        layout.Controls.Add(_lblResult, 0, row);
        layout.SetColumnSpan(_lblResult, 2);

        mainPanel.Controls.Add(layout);
        Controls.Add(mainPanel);
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
