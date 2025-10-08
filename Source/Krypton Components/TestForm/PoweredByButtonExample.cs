using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm;

public partial class PoweredByButtonExample : KryptonForm
{
    public PoweredByButtonExample()
    {
        InitializeComponent();

        foreach (var value in Enum.GetValues(typeof(ToolkitSupportType)))
        {
            kryptonComboBox1.Items.Add(value);
        }

        kryptonComboBox1.SelectedIndex = 0;
    }

    private void PoweredByButtonExample_Load(object sender, EventArgs e)
    {

    }

    private void kchkShowReadmeButton_CheckedChanged(object sender, EventArgs e)
    {
        kryptonPoweredByButton1.ButtonValues.ShowReadmeButton = kchkShowReadmeButton.Checked;
    }

    private void kchkShowChangelogButton_CheckedChanged(object sender, EventArgs e)
    {
        kryptonPoweredByButton1.ButtonValues.ShowChangeLogButton = kchkShowChangelogButton.Checked;
    }

    private void kryptonComboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ToolkitSupportType support = (ToolkitSupportType)kryptonComboBox1.SelectedItem;

        kryptonPoweredByButton1.ButtonValues.ToolkitSupportType = support;
    }
}