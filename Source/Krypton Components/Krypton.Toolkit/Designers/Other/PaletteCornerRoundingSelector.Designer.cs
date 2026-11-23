#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    partial class PaletteCornerRoundingSelector
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            checkBoxInherit = new CheckBox();
            labelOr = new Label();
            checkBoxTopLeft = new CheckBox();
            checkBoxTopRight = new CheckBox();
            checkBoxBottomRight = new CheckBox();
            checkBoxBottomLeft = new CheckBox();
            numericUpDownTopLeft = new NumericUpDown();
            numericUpDownTopRight = new NumericUpDown();
            numericUpDownBottomRight = new NumericUpDown();
            numericUpDownBottomLeft = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTopLeft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTopRight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownBottomRight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownBottomLeft).BeginInit();
            SuspendLayout();
            //
            // checkBoxInherit
            //
            checkBoxInherit.AutoSize = true;
            checkBoxInherit.Location = new Point(12, 12);
            checkBoxInherit.Name = "checkBoxInherit";
            checkBoxInherit.Size = new Size(55, 19);
            checkBoxInherit.TabIndex = 0;
            checkBoxInherit.Text = "Inherit";
            checkBoxInherit.UseVisualStyleBackColor = true;
            checkBoxInherit.CheckedChanged += checkBoxInherit_CheckedChanged;
            //
            // labelOr
            //
            labelOr.AutoSize = true;
            labelOr.Location = new Point(9, 35);
            labelOr.Name = "labelOr";
            labelOr.Size = new Size(53, 15);
            labelOr.TabIndex = 1;
            labelOr.Text = "---- OR ----";
            //
            // checkBoxTopLeft
            //
            checkBoxTopLeft.AutoSize = true;
            checkBoxTopLeft.Location = new Point(12, 60);
            checkBoxTopLeft.Name = "checkBoxTopLeft";
            checkBoxTopLeft.Size = new Size(69, 19);
            checkBoxTopLeft.TabIndex = 2;
            checkBoxTopLeft.Text = "Top Left";
            checkBoxTopLeft.UseVisualStyleBackColor = true;
            checkBoxTopLeft.CheckedChanged += CornerOverride_CheckedChanged;
            //
            // checkBoxTopRight
            //
            checkBoxTopRight.AutoSize = true;
            checkBoxTopRight.Location = new Point(12, 88);
            checkBoxTopRight.Name = "checkBoxTopRight";
            checkBoxTopRight.Size = new Size(77, 19);
            checkBoxTopRight.TabIndex = 4;
            checkBoxTopRight.Text = "Top Right";
            checkBoxTopRight.UseVisualStyleBackColor = true;
            checkBoxTopRight.CheckedChanged += CornerOverride_CheckedChanged;
            //
            // checkBoxBottomRight
            //
            checkBoxBottomRight.AutoSize = true;
            checkBoxBottomRight.Location = new Point(12, 116);
            checkBoxBottomRight.Name = "checkBoxBottomRight";
            checkBoxBottomRight.Size = new Size(97, 19);
            checkBoxBottomRight.TabIndex = 6;
            checkBoxBottomRight.Text = "Bottom Right";
            checkBoxBottomRight.UseVisualStyleBackColor = true;
            checkBoxBottomRight.CheckedChanged += CornerOverride_CheckedChanged;
            //
            // checkBoxBottomLeft
            //
            checkBoxBottomLeft.AutoSize = true;
            checkBoxBottomLeft.Location = new Point(12, 144);
            checkBoxBottomLeft.Name = "checkBoxBottomLeft";
            checkBoxBottomLeft.Size = new Size(89, 19);
            checkBoxBottomLeft.TabIndex = 8;
            checkBoxBottomLeft.Text = "Bottom Left";
            checkBoxBottomLeft.UseVisualStyleBackColor = true;
            checkBoxBottomLeft.CheckedChanged += CornerOverride_CheckedChanged;
            //
            // numericUpDownTopLeft
            //
            numericUpDownTopLeft.DecimalPlaces = 1;
            numericUpDownTopLeft.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            numericUpDownTopLeft.Location = new Point(118, 58);
            numericUpDownTopLeft.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numericUpDownTopLeft.Name = "numericUpDownTopLeft";
            numericUpDownTopLeft.Size = new Size(58, 23);
            numericUpDownTopLeft.TabIndex = 3;
            //
            // numericUpDownTopRight
            //
            numericUpDownTopRight.DecimalPlaces = 1;
            numericUpDownTopRight.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            numericUpDownTopRight.Location = new Point(118, 86);
            numericUpDownTopRight.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numericUpDownTopRight.Name = "numericUpDownTopRight";
            numericUpDownTopRight.Size = new Size(58, 23);
            numericUpDownTopRight.TabIndex = 5;
            //
            // numericUpDownBottomRight
            //
            numericUpDownBottomRight.DecimalPlaces = 1;
            numericUpDownBottomRight.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            numericUpDownBottomRight.Location = new Point(118, 114);
            numericUpDownBottomRight.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numericUpDownBottomRight.Name = "numericUpDownBottomRight";
            numericUpDownBottomRight.Size = new Size(58, 23);
            numericUpDownBottomRight.TabIndex = 7;
            //
            // numericUpDownBottomLeft
            //
            numericUpDownBottomLeft.DecimalPlaces = 1;
            numericUpDownBottomLeft.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            numericUpDownBottomLeft.Location = new Point(118, 142);
            numericUpDownBottomLeft.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numericUpDownBottomLeft.Name = "numericUpDownBottomLeft";
            numericUpDownBottomLeft.Size = new Size(58, 23);
            numericUpDownBottomLeft.TabIndex = 9;
            //
            // PaletteCornerRoundingSelector
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(numericUpDownBottomLeft);
            Controls.Add(numericUpDownBottomRight);
            Controls.Add(numericUpDownTopRight);
            Controls.Add(numericUpDownTopLeft);
            Controls.Add(checkBoxBottomLeft);
            Controls.Add(checkBoxBottomRight);
            Controls.Add(checkBoxTopRight);
            Controls.Add(checkBoxTopLeft);
            Controls.Add(labelOr);
            Controls.Add(checkBoxInherit);
            Name = "PaletteCornerRoundingSelector";
            Size = new Size(188, 178);
            ((System.ComponentModel.ISupportInitialize)numericUpDownTopLeft).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTopRight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownBottomRight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownBottomLeft).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private CheckBox checkBoxInherit;
        private Label labelOr;
        private CheckBox checkBoxTopLeft;
        private CheckBox checkBoxTopRight;
        private CheckBox checkBoxBottomRight;
        private CheckBox checkBoxBottomLeft;
        private NumericUpDown numericUpDownTopLeft;
        private NumericUpDown numericUpDownTopRight;
        private NumericUpDown numericUpDownBottomRight;
        private NumericUpDown numericUpDownBottomLeft;
    }
}
