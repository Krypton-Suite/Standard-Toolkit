#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

using Krypton.Toolkit;

namespace UpdateManifestEditor
{
    /// <summary>Edits the JSON manifest consumed by KryptonApplicationUpdater.</summary>
    public partial class MainForm : KryptonForm
    {
        private readonly List<ManifestPackage> _packages = new List<ManifestPackage>();
        private int _selected = -1;
        private bool _loading;
        private string? _savePath;

        public MainForm()
        {
            InitializeComponent();
            cmbSeverity.Items.AddRange(new object[] { "optional", "recommended", "important", "critical" });
            cmbKind.Items.AddRange(new object[] { "installer", "archive" });
            cmbAllowPostpone.Items.AddRange(new object[] { "default", "allow", "block" });
            cmbAllowSkip.Items.AddRange(new object[] { "default", "allow", "block" });
            cmbAllowResume.Items.AddRange(new object[] { "default", "allow", "block" });
            cmbAllowRetry.Items.AddRange(new object[] { "default", "allow", "block" });
            AddPresentationEditorRows();
            AddPackage();
        }

        private void AddPresentationEditorRows()
        {
            editor.SuspendLayout();
            editor.Controls.Remove(lblNotes);
            editor.Controls.Remove(txtNotes);
            while (editor.RowStyles.Count > 15)
            {
                editor.RowStyles.RemoveAt(editor.RowStyles.Count - 1);
            }

            for (var index = 0; index < 10; index++)
            {
                editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            }

            editor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            editor.RowCount = 26;

            lblBannerText = CreateEditorLabel("Banner text");
            txtBannerText = CreateEditorTextBox();
            lblBannerBackColor = CreateEditorLabel("Banner back");
            txtBannerBackColor = CreateEditorTextBox();
            lblBannerBackColor2 = CreateEditorLabel("Banner back 2");
            txtBannerBackColor2 = CreateEditorTextBox();
            lblBannerForeColor = CreateEditorLabel("Banner fore");
            txtBannerForeColor = CreateEditorTextBox();
            lblBannerImageUrl = CreateEditorLabel("Banner image");
            txtBannerImageUrl = CreateEditorTextBox();
            lblPulseEnable = CreateEditorLabel("Pulse");
            cmbPulseEnable = CreateEditorCombo(new object[] { "default", "on", "off" });
            lblPulseColor = CreateEditorLabel("Pulse colour");
            txtPulseColor = CreateEditorTextBox();
            lblPulseStyle = CreateEditorLabel("Pulse style");
            cmbPulseStyle = CreateEditorCombo(new object[] { "default", "All", "Bottom" });
            lblPulseSpeed = CreateEditorLabel("Pulse speed");
            txtPulseSpeed = CreateEditorTextBox();
            lblProgressDisplay = CreateEditorLabel("Progress");
            cmbProgressDisplay = CreateEditorCombo(new object[] { "default", "progressBar", "transferSpeedGraph" });

            editor.Controls.Add(lblBannerText, 0, 15);
            editor.Controls.Add(txtBannerText, 1, 15);
            editor.Controls.Add(lblBannerBackColor, 0, 16);
            editor.Controls.Add(txtBannerBackColor, 1, 16);
            editor.Controls.Add(lblBannerBackColor2, 0, 17);
            editor.Controls.Add(txtBannerBackColor2, 1, 17);
            editor.Controls.Add(lblBannerForeColor, 0, 18);
            editor.Controls.Add(txtBannerForeColor, 1, 18);
            editor.Controls.Add(lblBannerImageUrl, 0, 19);
            editor.Controls.Add(txtBannerImageUrl, 1, 19);
            editor.Controls.Add(lblPulseEnable, 0, 20);
            editor.Controls.Add(cmbPulseEnable, 1, 20);
            editor.Controls.Add(lblPulseColor, 0, 21);
            editor.Controls.Add(txtPulseColor, 1, 21);
            editor.Controls.Add(lblPulseStyle, 0, 22);
            editor.Controls.Add(cmbPulseStyle, 1, 22);
            editor.Controls.Add(lblPulseSpeed, 0, 23);
            editor.Controls.Add(txtPulseSpeed, 1, 23);
            editor.Controls.Add(lblProgressDisplay, 0, 24);
            editor.Controls.Add(cmbProgressDisplay, 1, 24);
            editor.Controls.Add(lblNotes, 0, 25);
            editor.Controls.Add(txtNotes, 1, 25);
            editor.ResumeLayout();
        }

        private static KryptonLabel CreateEditorLabel(string text)
        {
            var label = new KryptonLabel();
            label.Values.Text = text;
            return label;
        }

        private KryptonTextBox CreateEditorTextBox()
        {
            var box = new KryptonTextBox
            {
                Dock = DockStyle.Fill
            };
            box.TextChanged += OnFieldChanged;
            return box;
        }

        private KryptonComboBox CreateEditorCombo(object[] items)
        {
            var combo = new KryptonComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownWidth = 160,
                Width = 160
            };
            combo.Items.AddRange(items);
            combo.SelectedIndexChanged += OnFieldChanged;
            return combo;
        }

        private void OnAdd(object? sender, EventArgs e) => AddPackage();

        private void OnRemove(object? sender, EventArgs e)
        {
            if (_selected < 0 || _selected >= _packages.Count)
            {
                return;
            }

            var index = _selected;
            _packages.RemoveAt(index);
            lstPackages.Items.RemoveAt(index);
            if (_packages.Count == 0)
            {
                _selected = -1;
                ClearFields();
                UpdatePreview();
                return;
            }

            SelectPackage(Math.Min(index, _packages.Count - 1));
        }

        private void OnSelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_loading)
            {
                return;
            }

            CommitSelection();
            _selected = lstPackages.SelectedIndex;
            ShowSelection();
            UpdatePreview();
        }

        private void OnFieldChanged(object? sender, EventArgs e)
        {
            if (_loading)
            {
                return;
            }

            CommitSelection();
            RefreshListItem();
            UpdatePreview();
        }

        private void OnBrowse(object? sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Package file";
                dialog.Filter = "Packages (*.exe;*.msi;*.zip)|*.exe;*.msi;*.zip|All files (*.*)|*.*";
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                txtPackageFile.Text = dialog.FileName;
                if (string.IsNullOrWhiteSpace(txtPackageUrl.Text))
                {
                    txtPackageUrl.Text = dialog.FileName;
                }

                cmbKind.SelectedItem = dialog.FileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
                    ? "archive"
                    : "installer";
                ApplyHash();
                ApplyThumbprint(reportMissing: false);
            }
        }

        private void OnHash(object? sender, EventArgs e) => ApplyHash();

        private void OnSignature(object? sender, EventArgs e) => ApplyThumbprint(reportMissing: true);

        private void OnNow(object? sender, EventArgs e) =>
            txtReleasedUtc.Text = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);

        private void OnOpen(object? sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Open manifest";
                dialog.Filter = "JSON manifest (*.json)|*.json|All files (*.*)|*.*";
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    var packages = ManifestJson.Read(File.ReadAllText(dialog.FileName));
                    ReplacePackages(packages);
                    _savePath = dialog.FileName;
                    SetStatus("Opened " + dialog.FileName);
                }
                catch (Exception exception)
                {
                    SetStatus(exception.Message);
                }
            }
        }

        private void OnSave(object? sender, EventArgs e)
        {
            var path = _savePath;
            if (path == null || string.IsNullOrWhiteSpace(path))
            {
                OnSaveAs(sender, e);
                return;
            }

            SaveTo(path);
        }

        private void OnSaveAs(object? sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Title = "Save manifest";
                dialog.Filter = "JSON manifest (*.json)|*.json|All files (*.*)|*.*";
                dialog.FileName = "krypton-update.json";
                if (!string.IsNullOrWhiteSpace(_savePath))
                {
                    dialog.FileName = Path.GetFileName(_savePath);
                    dialog.InitialDirectory = Path.GetDirectoryName(_savePath);
                }

                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                if (SaveTo(dialog.FileName))
                {
                    _savePath = dialog.FileName;
                }
            }
        }

        private void OnCopy(object? sender, EventArgs e)
        {
            CommitSelection();
            var json = ManifestJson.Write(_packages);
            txtPreview.Text = json;
            Clipboard.SetText(json);
            var error = ManifestJson.Validate(_packages);
            SetStatus(error ?? "Copied the manifest to the clipboard.");
        }

        private void AddPackage()
        {
            CommitSelection();
            var package = new ManifestPackage
            {
                ReleasedUtc = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture)
            };
            _packages.Add(package);
            lstPackages.Items.Add(package);
            SelectPackage(_packages.Count - 1);
        }

        private void ReplacePackages(IReadOnlyList<ManifestPackage> packages)
        {
            _loading = true;
            _packages.Clear();
            lstPackages.Items.Clear();
            foreach (var package in packages)
            {
                _packages.Add(package);
                lstPackages.Items.Add(package);
            }

            _loading = false;
            if (_packages.Count == 0)
            {
                _selected = -1;
                ClearFields();
                UpdatePreview();
                return;
            }

            SelectPackage(0);
        }

        private void SelectPackage(int index)
        {
            _loading = true;
            _selected = index;
            lstPackages.SelectedIndex = index;
            _loading = false;
            ShowSelection();
            UpdatePreview();
        }

        private void ShowSelection()
        {
            if (_selected < 0 || _selected >= _packages.Count)
            {
                ClearFields();
                return;
            }

            var package = _packages[_selected];
            var kind = package.PackageKind;
            if (string.Equals(kind, "zip", StringComparison.OrdinalIgnoreCase))
            {
                kind = "archive";
            }

            _loading = true;
            txtVersion.Text = package.Version;
            SelectCombo(cmbSeverity, package.Severity, "recommended");
            SelectCombo(cmbKind, kind, "installer");
            txtPackageFile.Text = package.PackageFile;
            txtPackageUrl.Text = package.PackageUrl;
            txtSha256.Text = package.Sha256;
            txtThumbprint.Text = package.CertificateThumbprint;
            txtArguments.Text = package.InstallerArguments;
            txtReleasedUtc.Text = package.ReleasedUtc;
            txtMaximumPostponeDays.Text = package.MaximumPostponeDays;
            SelectCombo(cmbAllowPostpone, AllowPostponeLabel(package.AllowPostpone), "default");
            SelectCombo(cmbAllowSkip, AllowPostponeLabel(package.AllowSkip), "default");
            SelectCombo(cmbAllowResume, AllowPostponeLabel(package.AllowResume), "default");
            SelectCombo(cmbAllowRetry, AllowPostponeLabel(package.AllowRetry), "default");
            txtBannerText.Text = package.BannerText;
            txtBannerBackColor.Text = package.BannerBackColor;
            txtBannerBackColor2.Text = package.BannerBackColor2;
            txtBannerForeColor.Text = package.BannerForeColor;
            txtBannerImageUrl.Text = package.BannerImageUrl;
            SelectCombo(cmbPulseEnable, PulseEnableLabel(package.PulseEnable), "default");
            txtPulseColor.Text = package.PulseColor;
            SelectCombo(cmbPulseStyle, string.IsNullOrWhiteSpace(package.PulseStyle) ? "default" : package.PulseStyle, "default");
            txtPulseSpeed.Text = package.PulseSpeed;
            SelectCombo(cmbProgressDisplay, string.IsNullOrWhiteSpace(package.ProgressDisplay) ? "default" : package.ProgressDisplay, "default");
            txtNotes.Text = package.ReleaseNotes;
            _loading = false;
        }

        private void ClearFields()
        {
            _loading = true;
            txtVersion.Text = string.Empty;
            cmbSeverity.SelectedIndex = -1;
            cmbKind.SelectedIndex = -1;
            txtPackageFile.Text = string.Empty;
            txtPackageUrl.Text = string.Empty;
            txtSha256.Text = string.Empty;
            txtThumbprint.Text = string.Empty;
            txtArguments.Text = string.Empty;
            txtReleasedUtc.Text = string.Empty;
            txtMaximumPostponeDays.Text = string.Empty;
            cmbAllowPostpone.SelectedIndex = -1;
            cmbAllowSkip.SelectedIndex = -1;
            cmbAllowResume.SelectedIndex = -1;
            cmbAllowRetry.SelectedIndex = -1;
            txtBannerText.Text = string.Empty;
            txtBannerBackColor.Text = string.Empty;
            txtBannerBackColor2.Text = string.Empty;
            txtBannerForeColor.Text = string.Empty;
            txtBannerImageUrl.Text = string.Empty;
            cmbPulseEnable.SelectedIndex = -1;
            txtPulseColor.Text = string.Empty;
            cmbPulseStyle.SelectedIndex = -1;
            txtPulseSpeed.Text = string.Empty;
            cmbProgressDisplay.SelectedIndex = -1;
            txtNotes.Text = string.Empty;
            _loading = false;
        }

        private void CommitSelection()
        {
            if (_loading || _selected < 0 || _selected >= _packages.Count)
            {
                return;
            }

            var package = _packages[_selected];
            package.Version = txtVersion.Text.Trim();
            package.Severity = cmbSeverity.SelectedItem as string ?? "recommended";
            package.PackageKind = cmbKind.SelectedItem as string ?? "installer";
            package.PackageFile = txtPackageFile.Text.Trim();
            package.PackageUrl = txtPackageUrl.Text.Trim();
            package.Sha256 = txtSha256.Text.Trim();
            package.CertificateThumbprint = txtThumbprint.Text.Trim();
            package.InstallerArguments = txtArguments.Text.Trim();
            package.ReleasedUtc = txtReleasedUtc.Text.Trim();
            package.MaximumPostponeDays = txtMaximumPostponeDays.Text.Trim();
            package.AllowPostpone = AllowPostponeValue(cmbAllowPostpone.SelectedItem as string);
            package.AllowSkip = AllowPostponeValue(cmbAllowSkip.SelectedItem as string);
            package.AllowResume = AllowPostponeValue(cmbAllowResume.SelectedItem as string);
            package.AllowRetry = AllowPostponeValue(cmbAllowRetry.SelectedItem as string);
            package.BannerText = txtBannerText.Text.Trim();
            package.BannerBackColor = txtBannerBackColor.Text.Trim();
            package.BannerBackColor2 = txtBannerBackColor2.Text.Trim();
            package.BannerForeColor = txtBannerForeColor.Text.Trim();
            package.BannerImageUrl = txtBannerImageUrl.Text.Trim();
            package.PulseEnable = PulseEnableValue(cmbPulseEnable.SelectedItem as string);
            package.PulseColor = txtPulseColor.Text.Trim();
            var style = cmbPulseStyle.SelectedItem as string;
            package.PulseStyle = style == null || style == "default" ? string.Empty : style;
            package.PulseSpeed = txtPulseSpeed.Text.Trim();
            var progress = cmbProgressDisplay.SelectedItem as string;
            package.ProgressDisplay = progress == null || progress == "default" ? string.Empty : progress;
            package.ReleaseNotes = txtNotes.Text;
        }

        private void RefreshListItem()
        {
            if (_selected < 0 || _selected >= lstPackages.Items.Count)
            {
                return;
            }

            _loading = true;
            var index = _selected;
            lstPackages.Items[index] = _packages[index];
            lstPackages.SelectedIndex = index;
            _loading = false;
        }

        private void UpdatePreview()
        {
            txtPreview.Text = ManifestJson.Write(_packages);
            var error = ManifestJson.Validate(_packages);
            SetStatus(error ?? "Ready to save.");
        }

        private bool SaveTo(string path)
        {
            CommitSelection();
            var error = ManifestJson.Validate(_packages);
            txtPreview.Text = ManifestJson.Write(_packages);
            if (error != null)
            {
                SetStatus(error);
                return false;
            }

            File.WriteAllText(path, txtPreview.Text);
            SetStatus("Saved " + path);
            return true;
        }

        private void ApplyHash()
        {
            var path = txtPackageFile.Text.Trim();
            if (path.Length == 0 || !File.Exists(path))
            {
                SetStatus("Choose a package file to hash.");
                return;
            }

            using (var stream = File.OpenRead(path))
            using (var algorithm = SHA256.Create())
            {
                var hash = algorithm.ComputeHash(stream);
                txtSha256.Text = BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
            }

            SetStatus("SHA-256 updated.");
        }

        private void ApplyThumbprint(bool reportMissing)
        {
            var path = txtPackageFile.Text.Trim();
            if (path.Length == 0 || !File.Exists(path))
            {
                SetStatus("Choose a package file to read its signature.");
                return;
            }

            var thumbprint = ReadThumbprint(path);
            if (thumbprint == null)
            {
                txtThumbprint.Text = string.Empty;
                if (reportMissing)
                {
                    SetStatus("The file is not Authenticode-signed. The thumbprint was cleared.");
                }

                return;
            }

            txtThumbprint.Text = thumbprint;
            SetStatus("Thumbprint read from the signature.");
        }

        private static string? ReadThumbprint(string path)
        {
            try
            {
                // X509CertificateLoader does not read an Authenticode signature out of a PE file.
#pragma warning disable SYSLIB0057
                var certificate = new X509Certificate2(X509Certificate.CreateFromSignedFile(path));
#pragma warning restore SYSLIB0057
                var thumbprint = certificate.Thumbprint;
                if (thumbprint == null || string.IsNullOrWhiteSpace(thumbprint))
                {
                    return null;
                }

                return thumbprint.Replace(" ", string.Empty);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string AllowPostponeLabel(string value)
        {
            if (bool.TryParse(value, out var allow))
            {
                return allow ? "allow" : "block";
            }

            return "default";
        }

        private static string AllowPostponeValue(string? label)
        {
            if (string.Equals(label, "allow", StringComparison.OrdinalIgnoreCase))
            {
                return "true";
            }

            if (string.Equals(label, "block", StringComparison.OrdinalIgnoreCase))
            {
                return "false";
            }

            return string.Empty;
        }

        private static string PulseEnableLabel(string value)
        {
            if (bool.TryParse(value, out var enable))
            {
                return enable ? "on" : "off";
            }

            return "default";
        }

        private static string PulseEnableValue(string? label)
        {
            if (string.Equals(label, "on", StringComparison.OrdinalIgnoreCase))
            {
                return "true";
            }

            if (string.Equals(label, "off", StringComparison.OrdinalIgnoreCase))
            {
                return "false";
            }

            return string.Empty;
        }

        private static void SelectCombo(KryptonComboBox combo, string value, string fallback)
        {
            var wanted = string.IsNullOrWhiteSpace(value) ? fallback : value.Trim();
            for (var index = 0; index < combo.Items.Count; index++)
            {
                if (string.Equals(combo.Items[index] as string, wanted, StringComparison.OrdinalIgnoreCase))
                {
                    combo.SelectedIndex = index;
                    return;
                }
            }

            combo.SelectedItem = fallback;
        }

        private void SetStatus(string message) => lblStatus.Text = message;

        private KryptonLabel lblBannerText;
        private KryptonTextBox txtBannerText;
        private KryptonLabel lblBannerBackColor;
        private KryptonTextBox txtBannerBackColor;
        private KryptonLabel lblBannerBackColor2;
        private KryptonTextBox txtBannerBackColor2;
        private KryptonLabel lblBannerForeColor;
        private KryptonTextBox txtBannerForeColor;
        private KryptonLabel lblBannerImageUrl;
        private KryptonTextBox txtBannerImageUrl;
        private KryptonLabel lblPulseEnable;
        private KryptonComboBox cmbPulseEnable;
        private KryptonLabel lblPulseColor;
        private KryptonTextBox txtPulseColor;
        private KryptonLabel lblPulseStyle;
        private KryptonComboBox cmbPulseStyle;
        private KryptonLabel lblPulseSpeed;
        private KryptonTextBox txtPulseSpeed;
        private KryptonLabel lblProgressDisplay;
        private KryptonComboBox cmbProgressDisplay;
    }
}
