#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

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
    /// <summary>
    /// Manual repro harness for GitHub issue #3183: a small square or wrong caption glyph next to the
    /// <see cref="KryptonForm"/> Close (and related) buttons when a custom XML palette is applied.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The embedded resource <c>Microsoft365_Super_Pink</c> matches the theme attached to the issue.
    /// Run the form, then click <b>Apply Pink Theme</b> and inspect the top-right caption buttons
    /// (especially when maximized vs normal).
    /// </para>
    /// <para>
    /// The form starts maximized to mirror the original reporter’s sample and to exercise maximize /
    /// restore button-spec state.
    /// </para>
    /// </remarks>
    public partial class Bug3183SmallSquareRenderedNextToClose : KryptonForm
    {
        public Bug3183SmallSquareRenderedNextToClose()
        {
            InitializeComponent();
        }

        /// <summary>
        /// No startup theme load: compare default chrome first, then apply the custom palette from the button.
        /// </summary>
        private void Bug3183SmallSquareRenderedNextToClose_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Loads the embedded pink theme and switches the global palette to Custom so this form’s caption
        /// uses the same path as real apps (<see cref="KryptonManager.GlobalCustomPalette"/>).
        /// </summary>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            // Theme XML is stored as a string resource so the repro has no dependency on disk paths.
            /*byte[] contentFile = Encoding.UTF8.GetBytes(Properties.Resources.Microsoft365_Super_Pink);

            KryptonCustomPaletteBase customPaletteBase = new KryptonCustomPaletteBase();
            customPaletteBase.ImportWithUpgrade(new MemoryStream(contentFile));

            this.kryptonManager1.GlobalCustomPalette = customPaletteBase;
            this.kryptonManager1.GlobalPaletteMode = PaletteMode.Custom;*/
        }
    }
}
