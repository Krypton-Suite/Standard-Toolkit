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

public partial class KryptonBasicRTLTestForm : KryptonForm
{
    public KryptonBasicRTLTestForm()
    {
        InitializeComponent();
    }

    private void kryptonButton1_Click(object sender, EventArgs e)
    {
        new WinFormsBasicRTLTestForm().Show();
    }
}