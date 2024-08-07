using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Krypton.Toolkit;

namespace TestForm
{
    public partial class ColorTestimonials : KryptonForm
    {
        public ColorTestimonials()
        {
            InitializeComponent();

            cbEnableTransparancy.Checked = false;
            tbarAlpha.Enabled = false;

            tbarAlpha.Value = ColorInverting.ChannelMinValue;
            tbarRed.Value   = ColorInverting.ChannelMinValue;
            tbarGreen.Value = ColorInverting.ChannelMinValue;
            tbarBlue.Value  = ColorInverting.ChannelMinValue;

            cbEnableTransparancy_CheckedChanged(null!, null!);
        }

        private void tbarAlpha_ValueChanged(object sender, EventArgs e)
        {
            nudAlpha.Value = tbarAlpha.Value;
            SetColorFromOriginal();
        }

        private void tbarRed_ValueChanged(object sender, EventArgs e)
        {
            nudRed.Value = tbarRed.Value;
            SetColorFromOriginal();
        }

        private void tbarGreen_ValueChanged(object sender, EventArgs e)
        {
            nudGreen.Value = tbarGreen.Value;
            SetColorFromOriginal();
        }

        private void tbarBlue_ValueChanged(object sender, EventArgs e)
        {
            nudBlue.Value = tbarBlue.Value;
            SetColorFromOriginal();
        }

        private Color UpdateOriginalColor()
        {
            // Original color
            Color color = cbEnableTransparancy.Checked
                ? Color.FromArgb((byte)tbarAlpha.Value, (byte)tbarRed.Value, (byte)tbarGreen.Value, (byte)tbarBlue.Value)
                : Color.FromArgb((byte)tbarRed.Value, (byte)tbarGreen.Value, (byte)tbarBlue.Value);

            SetColorImage(pboxOriginal, color);

            return color;
        }

        private Color UpdateInvertedColor()
        {
            // Inverted color
            Color color = cbEnableTransparancy.Checked
                ? ColorInverting.InvertARGBFromInt((byte)tbarAlpha.Value, (byte)tbarRed.Value, (byte)tbarGreen.Value, (byte)tbarBlue.Value)
                : ColorInverting.InvertRGBFromInt((byte)tbarRed.Value, (byte)tbarGreen.Value, (byte)tbarBlue.Value);

            SetColorImage(pboxInverted, color);

            return color;
        }

        private void SetColorFromOriginal()
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

        private void SetColorFromInverted()
        {
            // Update and get colors
            Color color = UpdateOriginalColor();
            UpdateInvertedColor();

            // Update displayed original values
            if (cbEnableTransparancy.Checked)
            {
                nudAlpha.Value = color.A;
            }
            nudRed.Value = color.R;
            nudGreen.Value = color.G;
            nudBlue.Value = color.B;
        }

        private void SetColorImage(KryptonPictureBox kryptonPictureBox, Color color)
        {
            Bitmap b = new Bitmap(kryptonPictureBox.Size.Width, kryptonPictureBox.Size.Height);
            Graphics g = Graphics.FromImage(b);

            g.FillRectangle(new SolidBrush(color), new RectangleF(0, 0, kryptonPictureBox.Size.Width, kryptonPictureBox.Size.Height));

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

            SetColorFromOriginal();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

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
    }
}
