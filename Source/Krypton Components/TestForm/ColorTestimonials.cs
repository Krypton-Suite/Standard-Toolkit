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

        private void SetColor()
        {
            // Original colors
            Bitmap b = new Bitmap(pboxOriginal.Size.Width, pboxOriginal.Size.Height);
            Graphics g = Graphics.FromImage(b);

            Color color = cbEnableTransparancy.Checked
                ? Color.FromArgb((byte)tbarAlpha.Value, (byte)tbarRed.Value, (byte)tbarGreen.Value, (byte)tbarBlue.Value)
                : Color.FromArgb((byte)tbarRed.Value, (byte)tbarGreen.Value, (byte)tbarBlue.Value);

            g.FillRectangle(new SolidBrush(color), new RectangleF(0, 0, pboxOriginal.Size.Width, pboxOriginal.Size.Height));

            pboxOriginal.Image = b;

            // Inverted colors
            b = new Bitmap(pboxOriginal.Size.Width, pboxOriginal.Size.Height);
            g = Graphics.FromImage(b);

            color = cbEnableTransparancy.Checked
                ? ColorInverting.InvertARGBFromInt((byte)tbarAlpha.Value, (byte)tbarRed.Value, (byte)tbarGreen.Value, (byte)tbarBlue.Value)
                : ColorInverting.InvertRGBFromInt((byte)tbarRed.Value, (byte)tbarGreen.Value, (byte)tbarBlue.Value);

            g.FillRectangle(new SolidBrush(color), new RectangleF(0, 0, pboxInverted.Size.Width, pboxInverted.Size.Height));

            // Update displayed inverted values
            lblInvertedAlpha.Text = color.A.ToString();
            lblInvertedRed.Text = color.R.ToString();
            lblInvertedGreen.Text = color.G.ToString();
            lblInvertedBlue.Text = color.B.ToString();

            pboxInverted.Image = b;
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

            SetColor();
        }
    }
}
