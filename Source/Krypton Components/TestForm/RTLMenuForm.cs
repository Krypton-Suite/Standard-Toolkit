using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Krypton.Toolkit.Suite.Core.Standard.Toolkit.TestForm;

namespace TestForm
{
    public partial class RTLMenuForm : KryptonForm
    {
        public RTLMenuForm()
        {
            InitializeComponent();
        }

        private void kbtnCheckBox_Click(object sender, EventArgs e)
        {
            new CheckBoxTest().Show();
        }

        private void kbtnDataGridView_Click(object sender, EventArgs e)
        {
            new DataGridViewTest().Show();
        }

        private void kbtnDateTimePicker_Click(object sender, EventArgs e)
        {
            new DateTimePickerTest().Show();
        }

        private void kbtnDomainUpDown_Click(object sender, EventArgs e)
        {
            new DomainUpDownTest().Show();
        }

        private void kbtnListView_Click(object sender, EventArgs e)
        {
            new ListViewTest().Show();
        }

        private void kbtnNumericUpDown_Click(object sender, EventArgs e)
        {
            new NumericUpDownTest().Show();
        }

        private void kbtnRadioButton_Click(object sender, EventArgs e)
        {
            new RadioButtonTest().Show();
        }

        private void kbtnRTL_Click(object sender, EventArgs e)
        {
            new RTLTestForm().Show();
        }

        private void kbtnSplitContainer_Click(object sender, EventArgs e)
        {
            new SplitContainerTest().Show();
        }

        private void kbtnTrackBar_Click(object sender, EventArgs e)
        {
            new TrackBarTest().Show();
        }

        private void kbtnTreeView_Click(object sender, EventArgs e)
        {
            new TreeViewTest().Show();
        }

        private void kbtnWorkspace_Click(object sender, EventArgs e)
        {
            new WorkspaceTest().Show();
        }
    }
}
