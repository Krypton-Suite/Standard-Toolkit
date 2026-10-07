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
    public partial class Bug3013TestForm : KryptonForm
    {
        public Bug3013TestForm()
        {
            InitializeComponent();
        }

        private void Bug3013TestForm_Resize(object sender, EventArgs e)
        {
            UpdateSizeLabel();
        }

        private void UpdateSizeLabel()
        {
            kwlblFormResizeData.Text = $"Screen size: Height = {Screen.FromControl(this).WorkingArea.Height}, Width = {Screen.FromControl(this).WorkingArea.Width},\nForm Location: Left = {Left}, Top = {Top}, Right = {Right}, Bottom = {Bottom},\nForm size: Height = {Height}, Width = {Width}";
        }
    }
}
