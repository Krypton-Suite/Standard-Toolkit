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
	public partial class Bug3203QATLocationHiddenFormTest : KryptonForm
	{
		public Bug3203QATLocationHiddenFormTest()
		{
			InitializeComponent();
		}

		private void Bug3203QATLocationHiddenFormTest_Load(object sender, EventArgs e)
		{
			//this.UpdateLabel();

			this.kryptonRibbon1.QATLocation = Krypton.Ribbon.QATLocation.Hidden;
			this.UpdateLabel();
		}

		private void UpdateLabel()
		{
			this.kryptonLabel1.Text = $"kryptonRibbon1.QATLocation={this.kryptonRibbon1.QATLocation.ToString()}";
		}

		private void btnHidden_Click(object sender, EventArgs e)
		{
			this.kryptonRibbon1.QATLocation = Krypton.Ribbon.QATLocation.Hidden;
			this.UpdateLabel();
		}

		private void btnBelow_Click(object sender, EventArgs e)
		{
			this.kryptonRibbon1.QATLocation = Krypton.Ribbon.QATLocation.Below;
			this.UpdateLabel();
		}

		private void btnAbove_Click(object sender, EventArgs e)
		{
			this.kryptonRibbon1.QATLocation = Krypton.Ribbon.QATLocation.Above;
			this.UpdateLabel();
		}
	}
}
