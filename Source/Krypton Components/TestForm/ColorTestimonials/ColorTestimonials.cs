﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Krypton.Toolkit;

namespace TestForm
{
    public partial class ColorTestimonials : KryptonForm
    {
        private RectangleF _rectOriginalImage;
        private RectangleF _rectInvertedImage;

        public ColorTestimonials()
        {
            InitializeComponent();

            cbEnableTransparancy.Checked = false;
            tbarAlpha.Enabled = false;

            _rectOriginalImage = new RectangleF(0, 0, pboxOriginal.Size.Width, pboxOriginal.Size.Height);
            _rectInvertedImage = new RectangleF(0, 0, pboxInverted.Size.Width, pboxInverted.Size.Height);

            tbarAlpha.Value = ColorInverting.ChannelMinValue;
            tbarRed.Value = ColorInverting.ChannelMinValue;
            tbarGreen.Value = ColorInverting.ChannelMinValue;
            tbarBlue.Value = ColorInverting.ChannelMinValue;

            cbEnableTransparancy_CheckedChanged(null!, null!);
        }

        private void tbarAlpha_ValueChanged(object sender, EventArgs e)
        {
            nudAlpha.Value = tbarAlpha.Value;
            SetColor();
        }

        private void tbarRed_ValueChanged(object sender, EventArgs e)
        {
            nudRed.Value = tbarRed.Value;
            SetColor();
        }

        private void tbarGreen_ValueChanged(object sender, EventArgs e)
        {
            nudGreen.Value = tbarGreen.Value;
            SetColor();
        }

        private void tbarBlue_ValueChanged(object sender, EventArgs e)
        {
            nudBlue.Value = tbarBlue.Value;
            SetColor();
        }

        private void UpdateOriginalColor()
        {
            // Original color
            Color color = cbEnableTransparancy.Checked
                ? Color.FromArgb((byte)tbarAlpha.Value, (byte)tbarRed.Value, (byte)tbarGreen.Value, (byte)tbarBlue.Value)
                : Color.FromArgb((byte)tbarRed.Value, (byte)tbarGreen.Value, (byte)tbarBlue.Value);

            SetColorImage(pboxOriginal, ref color, ref _rectOriginalImage);
        }

        private Color UpdateInvertedColor()
        {
            // Inverted color
            Color color = cbEnableTransparancy.Checked
                ? ColorInverting.InvertARGBFromInt((byte)tbarAlpha.Value, (byte)tbarRed.Value, (byte)tbarGreen.Value, (byte)tbarBlue.Value)
                : ColorInverting.InvertRGBFromInt((byte)tbarRed.Value, (byte)tbarGreen.Value, (byte)tbarBlue.Value);

            SetColorImage(pboxInverted, ref color, ref _rectInvertedImage);

            return color;
        }

        private void SetColor()
        {
            // Update and get colors
            UpdateOriginalColor();
            Color color = UpdateInvertedColor();

            // Update displayed inverted values
            if (cbEnableTransparancy.Checked)
            {
                nudAlphaInverted.Value = color.A;
            }
            nudRedInverted.Value = color.R;
            nudGreenInverted.Value = color.G;
            nudBlueInverted.Value = color.B;
        }

        private void SetColorImage(KryptonPictureBox kryptonPictureBox, ref Color color, ref RectangleF rect)
        {
            Bitmap b = new Bitmap(kryptonPictureBox.Size.Width, kryptonPictureBox.Size.Height);
            Graphics g = Graphics.FromImage(b);

            g.FillRectangle(new SolidBrush(color), rect);

            kryptonPictureBox.Image = b;
        }

        private void nudAlpha_ValueChanged(object sender, EventArgs e)
        {
            tbarAlpha.Value = (int)nudAlpha.Value;
        }

        private void nudRed_ValueChanged(object sender, EventArgs e)
        {
            tbarRed.Value = (int)nudRed.Value;
        }

        private void nudGreen_ValueChanged(object sender, EventArgs e)
        {
            tbarGreen.Value = (int)nudGreen.Value;
        }

        private void nudBlue_ValueChanged(object sender, EventArgs e)
        {
            tbarBlue.Value = (int)nudBlue.Value;
        }

        private void cbEnableTransparancy_CheckedChanged(object sender, EventArgs e)
        {
            tbarAlpha.Enabled = cbEnableTransparancy.Checked;
            nudAlpha.Enabled = cbEnableTransparancy.Checked;
            nudAlphaInverted.Enabled = cbEnableTransparancy.Checked;

            SetColor();
        }

        private void nudAlphaInverted_ValueChanged(object sender, EventArgs e)
        {
            nudAlpha.Value = ColorInverting.Invert((byte)nudAlphaInverted.Value);
        }

        private void nudRedInverted_ValueChanged(object sender, EventArgs e)
        {
            nudRed.Value = ColorInverting.Invert((byte)nudRedInverted.Value);
        }

        private void nudGreenInverted_ValueChanged(object sender, EventArgs e)
        {
            nudGreen.Value = ColorInverting.Invert((byte)nudGreenInverted.Value);
        }

        private void nudBlueInverted_ValueChanged(object sender, EventArgs e)
        {
            nudBlue.Value = ColorInverting.Invert((byte)nudBlueInverted.Value);
        }
        private void btnColorFromHexColor_Click(object sender, EventArgs e)
        {
            CheckHexColorInput();
        }

        private bool CheckHexColorInput()
        {
            bool result = false;

            if (ColorInverting.IsHexColor(tbHexColor.Text))
            {
                if (cbEnableTransparancy.Checked)
                {
                    nudAlpha.Value = ColorInverting.ChannelMaxValue;
                }

                nudRed.Value = Convert.ToInt32(tbHexColor.Text.Substring(1, 2), 16);
                nudGreen.Value = Convert.ToInt32(tbHexColor.Text.Substring(3, 2), 16);
                nudBlue.Value = Convert.ToInt32(tbHexColor.Text.Substring(5, 2), 16);

                result = true;
            }
            else
            {
                KryptonMessageBox.Show(
                    "Incorrect hexadecimal input string.\n" +
                    "Use the format \"#FFFFFF\"",
                    this.Text,
                    icon: KryptonMessageBoxIcon.Exclamation,
                    buttons: KryptonMessageBoxButtons.OK);
            }

            return result;
        }

        private void tbHexColor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                CheckHexColorInput();
            }
        }

        private void btnCopyOriginal_Click(object sender, EventArgs e)
        {
            if (cbEnableTransparancy.Checked)
            {
                Clipboard.SetText(
                    $"Color.FromArgb({((int)nudAlpha.Value)}, {((int)nudRed.Value)}, {((int)nudGreen.Value)}, {((int)nudBlue.Value)});");
            }
            else
            {
                Clipboard.SetText(
                    $"Color.FromArgb({((int)nudRed.Value)}, {((int)nudGreen.Value)}, {((int)nudBlue.Value)});");
            }
        }

        private void btnCopyInverted_Click(object sender, EventArgs e)
        {
            if (cbEnableTransparancy.Checked)
            {
                Clipboard.SetText(
                    $"Color.FromArgb({((int)nudAlphaInverted.Value)}, {((int)nudRedInverted.Value)}, {((int)nudGreenInverted.Value)}, {((int)nudBlueInverted.Value)});");
            }
            else
            {
                Clipboard.SetText(
                    $"Color.FromArgb({((int)nudRedInverted.Value)}, {((int)nudGreenInverted.Value)}, {((int)nudBlueInverted.Value)});");
            }
        }

        private void btnLighter_Click(object sender, EventArgs e)
        {
            Color color = ControlPaint.Light(Color.FromArgb(((int)nudRed.Value), ((int)nudGreen.Value), ((int)nudBlue.Value)));

            nudRed.Value   = color.R;
            nudGreen.Value = color.G;
            nudBlue.Value  = color.B;
        }

        private void btnDarker_Click(object sender, EventArgs e)
        {
            Color color = ControlPaint.Dark(Color.FromArgb(((int)nudRed.Value), ((int)nudGreen.Value), ((int)nudBlue.Value)));

            nudRed.Value   = color.R;
            nudGreen.Value = color.G;
            nudBlue.Value  = color.B;
        }
    }
}
