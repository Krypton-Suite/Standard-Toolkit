#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class KryptonColorPickerDemo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.kryptonColorPicker1 = new Krypton.Toolkit.Utilities.KryptonColorPicker(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.klblInstructions = new Krypton.Toolkit.KryptonWrapLabel();
            this.tlpCompare = new System.Windows.Forms.TableLayoutPanel();
            this.kgbKrypton = new Krypton.Toolkit.KryptonGroupBox();
            this.tlpKrypton = new System.Windows.Forms.TableLayoutPanel();
            this.klblFlyout = new Krypton.Toolkit.KryptonLabel();
            this.kcmbFlyout = new Krypton.Toolkit.KryptonComboBox();
            this.klblMagnifier = new Krypton.Toolkit.KryptonLabel();
            this.knudMagnifier = new Krypton.Toolkit.KryptonNumericUpDown();
            this.klblZoom = new Krypton.Toolkit.KryptonLabel();
            this.knudZoom = new Krypton.Toolkit.KryptonNumericUpDown();
            this.klblFormats = new Krypton.Toolkit.KryptonLabel();
            this.kclbFormats = new Krypton.Toolkit.KryptonCheckedListBox();
            this.kchkUseSitedComponent = new Krypton.Toolkit.KryptonCheckBox();
            this.kbtnPickKrypton = new Krypton.Toolkit.KryptonButton();
            this.pnlKryptonSwatch = new System.Windows.Forms.Panel();
            this.klblKryptonResult = new Krypton.Toolkit.KryptonLabel();
            this.kgbNative = new Krypton.Toolkit.KryptonGroupBox();
            this.tlpNative = new System.Windows.Forms.TableLayoutPanel();
            this.klblNativeHint = new Krypton.Toolkit.KryptonWrapLabel();
            this.kbtnPickNative = new Krypton.Toolkit.KryptonButton();
            this.pnlNativeSwatch = new System.Windows.Forms.Panel();
            this.klblNativeResult = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            this.tlpCompare.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kgbKrypton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgbKrypton.Panel)).BeginInit();
            this.kgbKrypton.Panel.SuspendLayout();
            this.kgbKrypton.SuspendLayout();
            this.tlpKrypton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbFlyout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgbNative)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgbNative.Panel)).BeginInit();
            this.kgbNative.Panel.SuspendLayout();
            this.kgbNative.SuspendLayout();
            this.tlpNative.SuspendLayout();
            this.SuspendLayout();
            //
            // kpnlMain
            //
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Padding = new System.Windows.Forms.Padding(12);
            this.kpnlMain.Size = new System.Drawing.Size(784, 561);
            this.kpnlMain.TabIndex = 0;
            this.kpnlMain.Controls.Add(this.tlpCompare);
            this.kpnlMain.Controls.Add(this.klblInstructions);
            //
            // klblInstructions
            //
            this.klblInstructions.Dock = System.Windows.Forms.DockStyle.Top;
            this.klblInstructions.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.klblInstructions.Location = new System.Drawing.Point(12, 12);
            this.klblInstructions.Name = "klblInstructions";
            this.klblInstructions.Size = new System.Drawing.Size(760, 72);
            this.klblInstructions.TabIndex = 0;
            this.klblInstructions.Text = "Drop KryptonColorPicker from the toolbox (it sits in the component tray, like ColorDialog). Set flyout, magnifier, zoom, and formats, then Pick from screen. While picking, +/- or Page Up/Down zooms and [ ] resizes the magnifier. The native ColorDialog on the right is a palette window, not a screen dropper — that difference is intentional.";
            //
            // tlpCompare
            //
            this.tlpCompare.ColumnCount = 2;
            this.tlpCompare.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpCompare.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpCompare.Controls.Add(this.kgbKrypton, 0, 0);
            this.tlpCompare.Controls.Add(this.kgbNative, 1, 0);
            this.tlpCompare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCompare.Location = new System.Drawing.Point(12, 84);
            this.tlpCompare.Name = "tlpCompare";
            this.tlpCompare.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.tlpCompare.RowCount = 1;
            this.tlpCompare.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCompare.Size = new System.Drawing.Size(760, 465);
            this.tlpCompare.TabIndex = 1;
            //
            // kgbKrypton
            //
            this.kgbKrypton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kgbKrypton.Location = new System.Drawing.Point(3, 11);
            this.kgbKrypton.Name = "kgbKrypton";
            this.kgbKrypton.Size = new System.Drawing.Size(412, 451);
            this.kgbKrypton.TabIndex = 0;
            this.kgbKrypton.Values.Heading = "KryptonColorPicker (screen dropper)";
            this.kgbKrypton.Panel.Controls.Add(this.tlpKrypton);
            //
            // tlpKrypton
            //
            this.tlpKrypton.ColumnCount = 2;
            this.tlpKrypton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpKrypton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpKrypton.Controls.Add(this.klblFlyout, 0, 0);
            this.tlpKrypton.Controls.Add(this.kcmbFlyout, 1, 0);
            this.tlpKrypton.Controls.Add(this.klblMagnifier, 0, 1);
            this.tlpKrypton.Controls.Add(this.knudMagnifier, 1, 1);
            this.tlpKrypton.Controls.Add(this.klblZoom, 0, 2);
            this.tlpKrypton.Controls.Add(this.knudZoom, 1, 2);
            this.tlpKrypton.Controls.Add(this.klblFormats, 0, 3);
            this.tlpKrypton.Controls.Add(this.kclbFormats, 1, 3);
            this.tlpKrypton.Controls.Add(this.kchkUseSitedComponent, 0, 4);
            this.tlpKrypton.Controls.Add(this.kbtnPickKrypton, 0, 5);
            this.tlpKrypton.Controls.Add(this.pnlKryptonSwatch, 0, 6);
            this.tlpKrypton.Controls.Add(this.klblKryptonResult, 1, 6);
            this.tlpKrypton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpKrypton.Location = new System.Drawing.Point(0, 0);
            this.tlpKrypton.Name = "tlpKrypton";
            this.tlpKrypton.Padding = new System.Windows.Forms.Padding(8);
            this.tlpKrypton.RowCount = 7;
            this.tlpKrypton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpKrypton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpKrypton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpKrypton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpKrypton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpKrypton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpKrypton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tlpKrypton.Size = new System.Drawing.Size(400, 415);
            this.tlpKrypton.TabIndex = 0;
            this.tlpKrypton.SetColumnSpan(this.kchkUseSitedComponent, 2);
            this.tlpKrypton.SetColumnSpan(this.kbtnPickKrypton, 2);
            //
            // klblFlyout
            //
            this.klblFlyout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblFlyout.Location = new System.Drawing.Point(11, 11);
            this.klblFlyout.Name = "klblFlyout";
            this.klblFlyout.Size = new System.Drawing.Size(104, 26);
            this.klblFlyout.TabIndex = 0;
            this.klblFlyout.Values.Text = "Flyout";
            //
            // kcmbFlyout
            //
            this.kcmbFlyout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kcmbFlyout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbFlyout.Location = new System.Drawing.Point(121, 11);
            this.kcmbFlyout.Name = "kcmbFlyout";
            this.kcmbFlyout.Size = new System.Drawing.Size(268, 21);
            this.kcmbFlyout.TabIndex = 1;
            //
            // klblMagnifier
            //
            this.klblMagnifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblMagnifier.Location = new System.Drawing.Point(11, 43);
            this.klblMagnifier.Name = "klblMagnifier";
            this.klblMagnifier.Size = new System.Drawing.Size(104, 26);
            this.klblMagnifier.TabIndex = 2;
            this.klblMagnifier.Values.Text = "Magnifier";
            //
            // knudMagnifier
            //
            this.knudMagnifier.Dock = System.Windows.Forms.DockStyle.Left;
            this.knudMagnifier.Increment = new decimal(new int[] { 2, 0, 0, 0 });
            this.knudMagnifier.Location = new System.Drawing.Point(121, 43);
            this.knudMagnifier.Maximum = new decimal(new int[] { 21, 0, 0, 0 });
            this.knudMagnifier.Minimum = new decimal(new int[] { 7, 0, 0, 0 });
            this.knudMagnifier.Name = "knudMagnifier";
            this.knudMagnifier.Size = new System.Drawing.Size(80, 22);
            this.knudMagnifier.TabIndex = 3;
            this.knudMagnifier.Value = new decimal(new int[] { 11, 0, 0, 0 });
            //
            // klblZoom
            //
            this.klblZoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblZoom.Location = new System.Drawing.Point(11, 75);
            this.klblZoom.Name = "klblZoom";
            this.klblZoom.Size = new System.Drawing.Size(104, 26);
            this.klblZoom.TabIndex = 4;
            this.klblZoom.Values.Text = "Zoom";
            //
            // knudZoom
            //
            this.knudZoom.Dock = System.Windows.Forms.DockStyle.Left;
            this.knudZoom.Increment = new decimal(new int[] { 2, 0, 0, 0 });
            this.knudZoom.Location = new System.Drawing.Point(121, 75);
            this.knudZoom.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            this.knudZoom.Minimum = new decimal(new int[] { 6, 0, 0, 0 });
            this.knudZoom.Name = "knudZoom";
            this.knudZoom.Size = new System.Drawing.Size(80, 22);
            this.knudZoom.TabIndex = 5;
            this.knudZoom.Value = new decimal(new int[] { 12, 0, 0, 0 });
            //
            // klblFormats
            //
            this.klblFormats.Dock = System.Windows.Forms.DockStyle.Top;
            this.klblFormats.Location = new System.Drawing.Point(11, 107);
            this.klblFormats.Name = "klblFormats";
            this.klblFormats.Size = new System.Drawing.Size(104, 20);
            this.klblFormats.TabIndex = 6;
            this.klblFormats.Values.Text = "Formats";
            //
            // kclbFormats
            //
            this.kclbFormats.CheckOnClick = true;
            this.kclbFormats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kclbFormats.Location = new System.Drawing.Point(121, 107);
            this.kclbFormats.Name = "kclbFormats";
            this.kclbFormats.Size = new System.Drawing.Size(268, 160);
            this.kclbFormats.TabIndex = 7;
            //
            // kchkUseSitedComponent
            //
            this.kchkUseSitedComponent.Checked = true;
            this.kchkUseSitedComponent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kchkUseSitedComponent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kchkUseSitedComponent.Location = new System.Drawing.Point(11, 311);
            this.kchkUseSitedComponent.Name = "kchkUseSitedComponent";
            this.kchkUseSitedComponent.Size = new System.Drawing.Size(378, 26);
            this.kchkUseSitedComponent.TabIndex = 8;
            this.kchkUseSitedComponent.Values.Text = "Use the form-sited KryptonColorPicker (tray component)";
            //
            // kbtnPickKrypton
            //
            this.kbtnPickKrypton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnPickKrypton.Location = new System.Drawing.Point(11, 343);
            this.kbtnPickKrypton.Name = "kbtnPickKrypton";
            this.kbtnPickKrypton.Size = new System.Drawing.Size(378, 34);
            this.kbtnPickKrypton.TabIndex = 9;
            this.kbtnPickKrypton.Values.Text = "Pick from screen";
            this.kbtnPickKrypton.Click += new System.EventHandler(this.kbtnPickKrypton_Click);
            //
            // pnlKryptonSwatch
            //
            this.pnlKryptonSwatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKryptonSwatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKryptonSwatch.Location = new System.Drawing.Point(11, 383);
            this.pnlKryptonSwatch.Name = "pnlKryptonSwatch";
            this.pnlKryptonSwatch.Size = new System.Drawing.Size(104, 58);
            this.pnlKryptonSwatch.TabIndex = 10;
            //
            // klblKryptonResult
            //
            this.klblKryptonResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblKryptonResult.Location = new System.Drawing.Point(121, 383);
            this.klblKryptonResult.Name = "klblKryptonResult";
            this.klblKryptonResult.Size = new System.Drawing.Size(268, 58);
            this.klblKryptonResult.TabIndex = 11;
            this.klblKryptonResult.Values.Text = "No colour sampled yet.";
            //
            // kgbNative
            //
            this.kgbNative.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kgbNative.Location = new System.Drawing.Point(421, 11);
            this.kgbNative.Name = "kgbNative";
            this.kgbNative.Size = new System.Drawing.Size(336, 451);
            this.kgbNative.TabIndex = 1;
            this.kgbNative.Values.Heading = "Native ColorDialog";
            this.kgbNative.Panel.Controls.Add(this.tlpNative);
            //
            // tlpNative
            //
            this.tlpNative.ColumnCount = 1;
            this.tlpNative.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpNative.Controls.Add(this.klblNativeHint, 0, 0);
            this.tlpNative.Controls.Add(this.kbtnPickNative, 0, 1);
            this.tlpNative.Controls.Add(this.pnlNativeSwatch, 0, 2);
            this.tlpNative.Controls.Add(this.klblNativeResult, 0, 3);
            this.tlpNative.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpNative.Location = new System.Drawing.Point(0, 0);
            this.tlpNative.Name = "tlpNative";
            this.tlpNative.Padding = new System.Windows.Forms.Padding(8);
            this.tlpNative.RowCount = 4;
            this.tlpNative.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpNative.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpNative.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpNative.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tlpNative.Size = new System.Drawing.Size(324, 415);
            this.tlpNative.TabIndex = 0;
            //
            // klblNativeHint
            //
            this.klblNativeHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblNativeHint.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            this.klblNativeHint.Location = new System.Drawing.Point(11, 11);
            this.klblNativeHint.Name = "klblNativeHint";
            this.klblNativeHint.Size = new System.Drawing.Size(302, 204);
            this.klblNativeHint.TabIndex = 0;
            this.klblNativeHint.Text = "WinForms ColorDialog is a tray component that opens a palette. It does not sample pixels from the screen. Use it here only to compare designer usage (both sit in the component tray) and dialog results.";
            //
            // kbtnPickNative
            //
            this.kbtnPickNative.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnPickNative.Location = new System.Drawing.Point(11, 221);
            this.kbtnPickNative.Name = "kbtnPickNative";
            this.kbtnPickNative.Size = new System.Drawing.Size(302, 34);
            this.kbtnPickNative.TabIndex = 1;
            this.kbtnPickNative.Values.Text = "Show ColorDialog";
            this.kbtnPickNative.Click += new System.EventHandler(this.kbtnPickNative_Click);
            //
            // pnlNativeSwatch
            //
            this.pnlNativeSwatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNativeSwatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNativeSwatch.Location = new System.Drawing.Point(11, 261);
            this.pnlNativeSwatch.Name = "pnlNativeSwatch";
            this.pnlNativeSwatch.Size = new System.Drawing.Size(302, 74);
            this.pnlNativeSwatch.TabIndex = 2;
            //
            // klblNativeResult
            //
            this.klblNativeResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.klblNativeResult.Location = new System.Drawing.Point(11, 341);
            this.klblNativeResult.Name = "klblNativeResult";
            this.klblNativeResult.Size = new System.Drawing.Size(302, 66);
            this.klblNativeResult.TabIndex = 3;
            this.klblNativeResult.Values.Text = "No colour chosen yet.";
            //
            // KryptonColorPickerDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.kpnlMain);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "KryptonColorPickerDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KryptonColorPicker";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.tlpCompare.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kgbKrypton.Panel)).EndInit();
            this.kgbKrypton.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kgbKrypton)).EndInit();
            this.kgbKrypton.ResumeLayout(false);
            this.tlpKrypton.ResumeLayout(false);
            this.tlpKrypton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbFlyout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kgbNative.Panel)).EndInit();
            this.kgbNative.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kgbNative)).EndInit();
            this.kgbNative.ResumeLayout(false);
            this.tlpNative.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.Utilities.KryptonColorPicker kryptonColorPicker1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private Krypton.Toolkit.KryptonPanel kpnlMain;
        private Krypton.Toolkit.KryptonWrapLabel klblInstructions;
        private System.Windows.Forms.TableLayoutPanel tlpCompare;
        private Krypton.Toolkit.KryptonGroupBox kgbKrypton;
        private System.Windows.Forms.TableLayoutPanel tlpKrypton;
        private Krypton.Toolkit.KryptonLabel klblFlyout;
        private Krypton.Toolkit.KryptonComboBox kcmbFlyout;
        private Krypton.Toolkit.KryptonLabel klblMagnifier;
        private Krypton.Toolkit.KryptonNumericUpDown knudMagnifier;
        private Krypton.Toolkit.KryptonLabel klblZoom;
        private Krypton.Toolkit.KryptonNumericUpDown knudZoom;
        private Krypton.Toolkit.KryptonLabel klblFormats;
        private Krypton.Toolkit.KryptonCheckedListBox kclbFormats;
        private Krypton.Toolkit.KryptonCheckBox kchkUseSitedComponent;
        private Krypton.Toolkit.KryptonButton kbtnPickKrypton;
        private System.Windows.Forms.Panel pnlKryptonSwatch;
        private Krypton.Toolkit.KryptonLabel klblKryptonResult;
        private Krypton.Toolkit.KryptonGroupBox kgbNative;
        private System.Windows.Forms.TableLayoutPanel tlpNative;
        private Krypton.Toolkit.KryptonWrapLabel klblNativeHint;
        private Krypton.Toolkit.KryptonButton kbtnPickNative;
        private System.Windows.Forms.Panel pnlNativeSwatch;
        private Krypton.Toolkit.KryptonLabel klblNativeResult;
    }
}
