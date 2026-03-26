using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TestForm.Properties;

namespace TestForm
{
	public partial class Bug3183SmallSquareRenderedNextToClose : KryptonForm
	{
		public Bug3183SmallSquareRenderedNextToClose()
		{
			InitializeComponent();
		}

		private void Bug3183SmallSquareRenderedNextToClose_Load(object sender, EventArgs e)
		{

		}

		private void kryptonButton1_Click(object sender, EventArgs e)
		{
			byte[] contentFile = Encoding.UTF8.GetBytes(Properties.Resources.Microsoft365_Super_Pink);

			KryptonCustomPaletteBase customPaletteBase = new KryptonCustomPaletteBase();
			customPaletteBase.ImportWithUpgrade(new MemoryStream(contentFile));

			this.kryptonManager1.GlobalCustomPalette = customPaletteBase;
			this.kryptonManager1.GlobalPaletteMode = PaletteMode.Custom;
		}
	}
}
