namespace TestForm
{
    partial class Bug3850TooltipHotSpotDemo
    {
        private System.ComponentModel.IContainer components;

        /// <inheritdoc />
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
            this.kryptonToolTip1 = new Krypton.Toolkit.KryptonToolTip(this.components);
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.klblInstructions = new Krypton.Toolkit.KryptonWrapLabel();
            this.chkLegacyPlacement = new Krypton.Toolkit.KryptonCheckBox();
            this.cmbCursor = new Krypton.Toolkit.KryptonComboBox();
            this.klblCursor = new Krypton.Toolkit.KryptonLabel();
            this.pnlCursorRegion = new Krypton.Toolkit.KryptonPanel();
            this.klblCursorRegion = new Krypton.Toolkit.KryptonLabel();
            this.kbtnCenter = new Krypton.Toolkit.KryptonButton();
            this.kbtnRelativePoint = new Krypton.Toolkit.KryptonButton();
            this.kbtnMouse = new Krypton.Toolkit.KryptonButton();
            this.kbtnLegacy = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)this.kpnlMain).BeginInit();
            this.kpnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.pnlCursorRegion).BeginInit();
            this.pnlCursorRegion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.cmbCursor).BeginInit();
            this.SuspendLayout();
            //
            // kpnlMain
            //
            this.kpnlMain.Controls.Add(this.klblInstructions);
            this.kpnlMain.Controls.Add(this.chkLegacyPlacement);
            this.kpnlMain.Controls.Add(this.cmbCursor);
            this.kpnlMain.Controls.Add(this.klblCursor);
            this.kpnlMain.Controls.Add(this.pnlCursorRegion);
            this.kpnlMain.Controls.Add(this.kbtnCenter);
            this.kpnlMain.Controls.Add(this.kbtnRelativePoint);
            this.kpnlMain.Controls.Add(this.kbtnMouse);
            this.kpnlMain.Controls.Add(this.kbtnLegacy);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Padding = new System.Windows.Forms.Padding(14);
            this.kpnlMain.Size = new System.Drawing.Size(584, 420);
            this.kpnlMain.TabIndex = 0;
            //
            // klblInstructions
            //
            this.klblInstructions.Dock = System.Windows.Forms.DockStyle.Top;
            this.klblInstructions.Location = new System.Drawing.Point(14, 14);
            this.klblInstructions.Name = "klblInstructions";
            this.klblInstructions.Size = new System.Drawing.Size(556, 88);
            this.klblInstructions.TabIndex = 0;
            this.klblInstructions.Text =
                "Issue #3850: hover each button and the cursor region. Tooltips must not cover the cursor hotspot. "
                + "Try Mouse, RelativePoint, and Center placement, switch cursors (arrow, I-beam, wait, size-all), "
                + "and enable legacy cursor placement to compare ShowCalculatingSize anchoring.";
            //
            // chkLegacyPlacement
            //
            this.chkLegacyPlacement.Location = new System.Drawing.Point(17, 112);
            this.chkLegacyPlacement.Name = "chkLegacyPlacement";
            this.chkLegacyPlacement.Size = new System.Drawing.Size(320, 24);
            this.chkLegacyPlacement.TabIndex = 1;
            this.chkLegacyPlacement.Values.Text = "Use legacy cursor-anchored placement (KryptonToolTip)";
            //
            // klblCursor
            //
            this.klblCursor.AutoSize = true;
            this.klblCursor.Location = new System.Drawing.Point(17, 146);
            this.klblCursor.Name = "klblCursor";
            this.klblCursor.Size = new System.Drawing.Size(98, 20);
            this.klblCursor.TabIndex = 2;
            this.klblCursor.Values.Text = "Cursor in panel:";
            //
            // cmbCursor
            //
            this.cmbCursor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCursor.DropDownWidth = 200;
            this.cmbCursor.Items.AddRange(new object[]
            {
                "Default (arrow)",
                "I-beam",
                "Wait",
                "Size All"
            });
            this.cmbCursor.Location = new System.Drawing.Point(121, 142);
            this.cmbCursor.Name = "cmbCursor";
            this.cmbCursor.Size = new System.Drawing.Size(200, 26);
            this.cmbCursor.TabIndex = 3;
            //
            // kbtnMouse
            //
            this.kbtnMouse.Location = new System.Drawing.Point(17, 186);
            this.kbtnMouse.Name = "kbtnMouse";
            this.kbtnMouse.Size = new System.Drawing.Size(260, 36);
            this.kbtnMouse.TabIndex = 4;
            this.kbtnMouse.Values.Text = "PlacementMode.Mouse";
            this.kbtnMouse.ToolTipValues.Heading = "Mouse placement";
            this.kbtnMouse.ToolTipValues.Description =
                "Tooltip anchors below the full cursor image. The hotspot should stay visible.";
            //
            // kbtnRelativePoint
            //
            this.kbtnRelativePoint.Location = new System.Drawing.Point(307, 186);
            this.kbtnRelativePoint.Name = "kbtnRelativePoint";
            this.kbtnRelativePoint.Size = new System.Drawing.Size(260, 36);
            this.kbtnRelativePoint.TabIndex = 5;
            this.kbtnRelativePoint.Values.Text = "PlacementMode.RelativePoint";
            this.kbtnRelativePoint.ToolTipValues.Heading = "RelativePoint placement";
            this.kbtnRelativePoint.ToolTipValues.Description =
                "When the control overlaps the cursor, the tooltip shifts to the right of the cursor bounds.";
            //
            // kbtnCenter
            //
            this.kbtnCenter.Location = new System.Drawing.Point(17, 236);
            this.kbtnCenter.Name = "kbtnCenter";
            this.kbtnCenter.Size = new System.Drawing.Size(260, 36);
            this.kbtnCenter.TabIndex = 6;
            this.kbtnCenter.Values.Text = "PlacementMode.Center";
            this.kbtnCenter.ToolTipValues.Heading = "Center placement";
            this.kbtnCenter.ToolTipValues.Description =
                "Centered on the control; shifts clear of the cursor when they intersect.";
            //
            // kbtnLegacy
            //
            this.kbtnLegacy.Location = new System.Drawing.Point(307, 236);
            this.kbtnLegacy.Name = "kbtnLegacy";
            this.kbtnLegacy.Size = new System.Drawing.Size(260, 36);
            this.kbtnLegacy.TabIndex = 7;
            this.kbtnLegacy.Values.Text = "KryptonToolTip (legacy path)";
            //
            // pnlCursorRegion
            //
            this.pnlCursorRegion.Controls.Add(this.klblCursorRegion);
            this.pnlCursorRegion.Location = new System.Drawing.Point(17, 290);
            this.pnlCursorRegion.Name = "pnlCursorRegion";
            this.pnlCursorRegion.Size = new System.Drawing.Size(550, 110);
            this.pnlCursorRegion.StateCommon.Color1 = System.Drawing.Color.FromArgb(240, 240, 240);
            this.pnlCursorRegion.TabIndex = 8;
            //
            // klblCursorRegion
            //
            this.klblCursorRegion.Location = new System.Drawing.Point(12, 12);
            this.klblCursorRegion.Name = "klblCursorRegion";
            this.klblCursorRegion.Size = new System.Drawing.Size(520, 48);
            this.klblCursorRegion.TabIndex = 0;
            this.klblCursorRegion.Values.Text =
                "Move the selected cursor over this panel and hover the buttons above. "
                + "Wide cursors (wait, size-all) make incorrect hotspot handling obvious.";
            this.klblCursorRegion.ToolTipValues.Heading = "Cursor region";
            this.klblCursorRegion.ToolTipValues.Description =
                "Built-in KryptonLabel tooltip with default Bottom placement while using a custom panel cursor.";
            this.klblCursorRegion.ToolTipValues.ToolTipPosition.PlacementMode = Krypton.Toolkit.PlacementMode.Mouse;
            //
            // Bug3850TooltipHotSpotDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 420);
            this.Controls.Add(this.kpnlMain);
            this.Name = "Bug3850TooltipHotSpotDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = @"Bug 3850 Tooltip HotSpot Demo";
            ((System.ComponentModel.ISupportInitialize)this.kpnlMain).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.kpnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.pnlCursorRegion).EndInit();
            this.pnlCursorRegion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.cmbCursor).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonToolTip kryptonToolTip1;

        private Krypton.Toolkit.KryptonPanel kpnlMain;

        private Krypton.Toolkit.KryptonWrapLabel klblInstructions;

        private Krypton.Toolkit.KryptonCheckBox chkLegacyPlacement;

        private Krypton.Toolkit.KryptonComboBox cmbCursor;

        private Krypton.Toolkit.KryptonLabel klblCursor;

        private Krypton.Toolkit.KryptonPanel pnlCursorRegion;

        private Krypton.Toolkit.KryptonLabel klblCursorRegion;

        private Krypton.Toolkit.KryptonButton kbtnMouse;

        private Krypton.Toolkit.KryptonButton kbtnRelativePoint;

        private Krypton.Toolkit.KryptonButton kbtnCenter;

        private Krypton.Toolkit.KryptonButton kbtnLegacy;
    }
}
