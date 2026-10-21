using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
    public partial class MdiWindow : KryptonForm
    {
        public MdiWindow()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var form = new KryptonForm
            {
                Text = "KFORM",
                MdiParent = this,
                Visible = true
            };
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var form = new Form
            {
                Text = "KFORM",
                MdiParent = this,
                Visible = true
            };
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }
    }
}
