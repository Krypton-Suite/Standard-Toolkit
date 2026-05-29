namespace TestForm
{
    partial class OAuth2Demo
    {
        private System.ComponentModel.IContainer components;

        private KryptonPanel kryptonPanel1 = null!;
        private TableLayoutPanel tableLayoutPanel1 = null!;
        private FlowLayoutPanel flowLayoutPanel1 = null!;
        private KryptonLabel klblProvider = null!;
        private KryptonLabel klblTenantId = null!;
        private KryptonLabel klblClientId = null!;
        private KryptonLabel klblRedirectUri = null!;
        private KryptonLabel klblScopes = null!;
        private KryptonLabel klblSpacer = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.kryptonPanel1 = new KryptonPanel();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.klblProvider = new KryptonLabel();
            this._cmbProvider = new KryptonComboBox();
            this.klblTenantId = new KryptonLabel();
            this._panelTenant = new KryptonPanel();
            this._txtTenantId = new KryptonTextBox();
            this.klblClientId = new KryptonLabel();
            this._txtClientId = new KryptonTextBox();
            this.klblRedirectUri = new KryptonLabel();
            this._txtRedirectUri = new KryptonTextBox();
            this.klblScopes = new KryptonLabel();
            this._txtScopes = new KryptonTextBox();
            this.klblSpacer = new KryptonLabel();
            this._chkSystemBrowser = new KryptonCheckBox();
            this.flowLayoutPanel1 = new FlowLayoutPanel();
            this._btnSignIn = new KryptonButton();
            this._btnRefresh = new KryptonButton();
            this._lblResult = new KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._panelTenant)).BeginInit();
            this._panelTenant.SuspendLayout();
            this.SuspendLayout();
            //
            // kryptonPanel1
            //
            this.kryptonPanel1.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel1.Dock = DockStyle.Fill;
            this.kryptonPanel1.Location = new Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Padding = new Padding(12);
            this.kryptonPanel1.PanelBackStyle = PaletteBackStyle.ControlClient;
            this.kryptonPanel1.Size = new Size(640, 480);
            this.kryptonPanel1.TabIndex = 0;
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.klblProvider, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._cmbProvider, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.klblTenantId, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._panelTenant, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.klblClientId, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this._txtClientId, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.klblRedirectUri, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._txtRedirectUri, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.klblScopes, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this._txtScopes, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.klblSpacer, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this._chkSystemBrowser, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this._lblResult, 0, 7);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new Size(616, 456);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // klblProvider
            //
            this.klblProvider.Anchor = AnchorStyles.Left;
            this.klblProvider.Location = new Point(3, 6);
            this.klblProvider.Name = "klblProvider";
            this.klblProvider.Size = new Size(50, 20);
            this.klblProvider.TabIndex = 0;
            this.klblProvider.Values.Text = "Provider";
            //
            // _cmbProvider
            //
            this._cmbProvider.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this._cmbProvider.DropDownStyle = ComboBoxStyle.DropDownList;
            this._cmbProvider.Items.AddRange(new object[] { "Azure AD", "Google", "GitHub" });
            this._cmbProvider.Location = new Point(123, 3);
            this._cmbProvider.Name = "_cmbProvider";
            this._cmbProvider.Size = new Size(490, 25);
            this._cmbProvider.TabIndex = 1;
            this._cmbProvider.SelectedIndexChanged += this.OnProviderChanged;
            //
            // klblTenantId
            //
            this.klblTenantId.Anchor = AnchorStyles.Left;
            this.klblTenantId.Location = new Point(3, 38);
            this.klblTenantId.Name = "klblTenantId";
            this.klblTenantId.Size = new Size(60, 20);
            this.klblTenantId.TabIndex = 2;
            this.klblTenantId.Values.Text = "Tenant ID";
            //
            // _panelTenant
            //
            this._panelTenant.Controls.Add(this._txtTenantId);
            this._panelTenant.Dock = DockStyle.Fill;
            this._panelTenant.Location = new Point(123, 35);
            this._panelTenant.Name = "_panelTenant";
            this._panelTenant.PanelBackStyle = PaletteBackStyle.ControlClient;
            this._panelTenant.Size = new Size(490, 26);
            this._panelTenant.TabIndex = 3;
            //
            // _txtTenantId
            //
            this._txtTenantId.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this._txtTenantId.Location = new Point(0, 1);
            this._txtTenantId.Name = "_txtTenantId";
            this._txtTenantId.Size = new Size(490, 23);
            this._txtTenantId.TabIndex = 0;
            this._txtTenantId.Text = "common";
            //
            // klblClientId
            //
            this.klblClientId.Anchor = AnchorStyles.Left;
            this.klblClientId.Location = new Point(3, 70);
            this.klblClientId.Name = "klblClientId";
            this.klblClientId.Size = new Size(55, 20);
            this.klblClientId.TabIndex = 4;
            this.klblClientId.Values.Text = "Client ID";
            //
            // _txtClientId
            //
            this._txtClientId.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this._txtClientId.Location = new Point(123, 67);
            this._txtClientId.Name = "_txtClientId";
            this._txtClientId.Size = new Size(490, 23);
            this._txtClientId.TabIndex = 5;
            //
            // klblRedirectUri
            //
            this.klblRedirectUri.Anchor = AnchorStyles.Left;
            this.klblRedirectUri.Location = new Point(3, 102);
            this.klblRedirectUri.Name = "klblRedirectUri";
            this.klblRedirectUri.Size = new Size(75, 20);
            this.klblRedirectUri.TabIndex = 6;
            this.klblRedirectUri.Values.Text = "Redirect URI";
            //
            // _txtRedirectUri
            //
            this._txtRedirectUri.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this._txtRedirectUri.Location = new Point(123, 99);
            this._txtRedirectUri.Name = "_txtRedirectUri";
            this._txtRedirectUri.Size = new Size(490, 23);
            this._txtRedirectUri.TabIndex = 7;
            this._txtRedirectUri.Text = "com.krypton.oauth2.demo://callback";
            //
            // klblScopes
            //
            this.klblScopes.Anchor = AnchorStyles.Left;
            this.klblScopes.Location = new Point(3, 134);
            this.klblScopes.Name = "klblScopes";
            this.klblScopes.Size = new Size(45, 20);
            this.klblScopes.TabIndex = 8;
            this.klblScopes.Values.Text = "Scopes";
            //
            // _txtScopes
            //
            this._txtScopes.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this._txtScopes.Location = new Point(123, 131);
            this._txtScopes.Name = "_txtScopes";
            this._txtScopes.Size = new Size(490, 23);
            this._txtScopes.TabIndex = 9;
            this._txtScopes.Text = "openid profile";
            //
            // klblSpacer
            //
            this.klblSpacer.Location = new Point(3, 163);
            this.klblSpacer.Name = "klblSpacer";
            this.klblSpacer.Size = new Size(50, 20);
            this.klblSpacer.TabIndex = 10;
            //
            // _chkSystemBrowser
            //
            this._chkSystemBrowser.Anchor = AnchorStyles.Left;
            this._chkSystemBrowser.Location = new Point(123, 163);
            this._chkSystemBrowser.Name = "_chkSystemBrowser";
            this._chkSystemBrowser.Size = new Size(400, 20);
            this._chkSystemBrowser.TabIndex = 11;
            this._chkSystemBrowser.Values.Text = "Use system browser (HttpListener) instead of embedded WebView2";
            //
            // flowLayoutPanel1
            //
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this._btnSignIn);
            this.flowLayoutPanel1.Controls.Add(this._btnRefresh);
            this.flowLayoutPanel1.Dock = DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            this.flowLayoutPanel1.Location = new Point(3, 203);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new Size(610, 34);
            this.flowLayoutPanel1.TabIndex = 12;
            //
            // _btnSignIn
            //
            this._btnSignIn.Location = new Point(3, 3);
            this._btnSignIn.Name = "_btnSignIn";
            this._btnSignIn.Size = new Size(120, 28);
            this._btnSignIn.TabIndex = 0;
            this._btnSignIn.Values.Text = "Sign in";
            this._btnSignIn.Click += this.OnSignInClick;
            //
            // _btnRefresh
            //
            this._btnRefresh.Enabled = false;
            this._btnRefresh.Location = new Point(129, 3);
            this._btnRefresh.Name = "_btnRefresh";
            this._btnRefresh.Size = new Size(120, 28);
            this._btnRefresh.TabIndex = 1;
            this._btnRefresh.Values.Text = "Refresh token";
            this._btnRefresh.Click += this.OnRefreshClick;
            //
            // _lblResult
            //
            this.tableLayoutPanel1.SetColumnSpan(this._lblResult, 2);
            this._lblResult.AutoSize = false;
            this._lblResult.Dock = DockStyle.Fill;
            this._lblResult.LabelStyle = LabelStyle.NormalControl;
            this._lblResult.Location = new Point(3, 243);
            this._lblResult.Name = "_lblResult";
            this._lblResult.Padding = new Padding(0, 8, 0, 0);
            this._lblResult.Size = new Size(610, 210);
            this._lblResult.TabIndex = 13;
            this._lblResult.Values.Text = "Enter your Client ID and Redirect URI (must be registered with the provider), then click Sign in.\n\nFor Azure AD: Register an app at https://portal.azure.com → Azure Active Directory → App registrations.\nFor Google: https://console.cloud.google.com/apis/credentials\nFor GitHub: https://github.com/settings/developers";
            //
            // OAuth2Demo
            //
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(640, 480);
            this.Controls.Add(this.kryptonPanel1);
            this.MinimumSize = new Size(500, 400);
            this.Name = "OAuth2Demo";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "OAuth2 PKCE Demo";
            this.Load += this.OAuth2Demo_Load;
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._panelTenant)).EndInit();
            this._panelTenant.ResumeLayout(false);
            this._panelTenant.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private KryptonComboBox _cmbProvider;
        private KryptonPanel _panelTenant;
        private KryptonTextBox _txtTenantId;
        private KryptonTextBox _txtClientId;
        private KryptonTextBox _txtRedirectUri;
        private KryptonTextBox _txtScopes;
        private KryptonCheckBox _chkSystemBrowser;
        private KryptonButton _btnSignIn;
        private KryptonButton _btnRefresh;
        private KryptonLabel _lblResult;
    }
}