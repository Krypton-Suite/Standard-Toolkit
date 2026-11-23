#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class Bug3381KryptonButtonRoundedTextCenteringDemo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support — do not modify the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            _lblIntro = new KryptonWrapLabel();
            _root = new TableLayoutPanel();
            SuspendLayout();
            _root.SuspendLayout();
            //
            // _lblIntro
            //
            _lblIntro.Dock = DockStyle.Top;
            _lblIntro.AutoSize = false;
            _lblIntro.Padding = new Padding(0, 0, 0, 8);
            _lblIntro.Height = 120;
            _lblIntro.Text =
                @"GitHub issue #3381: With large StateCommon.Border.Rounding compared to button height, short text should stay visually centered (horizontal and vertical) inside the pill shape." + Environment.NewLine +
                @"What to check: Cyrillic + large font on a wide short bar; tall narrow capsule; low rounding baseline; then use the live controls to sweep rounding, vertical alignment, font size, and button height. Resize the form — layout should track the border path, not the uncapped palette rounding value.";
            //
            // _root
            //
            _root.Dock = DockStyle.Fill;
            _root.Padding = new Padding(12, 0, 12, 12);
            _root.AutoSize = false;
            _root.ColumnCount = 1;
            _root.Margin = new Padding(0);
            _root.Name = "tableLayoutRoot";
            //
            // Bug3381KryptonButtonRoundedTextCenteringDemo
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 760);
            MinimumSize = new Size(720, 620);
            Name = "Bug3381KryptonButtonRoundedTextCenteringDemo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = @"Bug #3381 — KryptonButton rounded corners + text centering";
            _root.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private KryptonWrapLabel _lblIntro;
        private TableLayoutPanel _root;
        private KryptonButton _btnLive;
        private TrackBar _trackRounding;
        private Label _lblRoundingValue;
        private ComboBox _cmbTextV;
        private NumericUpDown _nudFontSize;
        private NumericUpDown _nudLiveHeight;
        private Panel _pLiveButtonHost;
    }
}
