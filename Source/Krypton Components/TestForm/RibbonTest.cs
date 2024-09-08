#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

using Krypton.Ribbon;

namespace TestForm
{
    public partial class RibbonTest : KryptonForm
    {
        public RibbonTest()
        {
            InitializeComponent();
        }

        private void buttonEditColor_Click(object sender, EventArgs e)
        {
            // Let user change the color definition
            using var kcd = new KryptonColorDialog();

            kcd.AllowFullOpen = true;

            if (kcd.ShowDialog() == DialogResult.OK)
            {
                // Update the Displayed color feedback
                panelContextColor.StateCommon.Color1 = kcd.Color;
                textBoxContextName.Text = kcd.Color.Name;

                textBoxSelectedContexts.Text = $"{textBoxSelectedContexts.Text},{textBoxContextName.Text}";
            }
        }

        private void buttonSelectedApply_Click(object sender, EventArgs e)
        {
            kryptonRibbon.SelectedContext = textBoxSelectedContexts.Text;
        }

        private void textBoxSelectedContexts_KeyDown(object sender, KeyEventArgs e)
        {
            // Pressing enter in text box is same as pressing the apply button
            if (e.KeyCode == Keys.Enter)
            {
                buttonSelectedApply_Click(buttonSelectedApply, EventArgs.Empty);
            }
        }

        private void buttonAddContext_Click(object sender, EventArgs e)
        {
            // Create a new context that uses the information specified
            var newContext = new KryptonRibbonContext
            {
                ContextName = textBoxContextName.Text,
                ContextTitle = textBoxContextTitle.Text,
                ContextColor = panelContextColor.StateCommon.Color1
            };
            kryptonRibbon.RibbonContexts.Add(newContext);

            // Create a new ribbon page that specifies the new context name
            KryptonRibbonTab newTab = new KryptonRibbonTab
            {
                ContextName = newContext.ContextName
            };
            kryptonRibbon.RibbonTabs.Add(newTab);

            // Update the selected context name on the form and control so it shows
            var newSelectedContext = textBoxSelectedContexts.Text;
            if (newSelectedContext.Length > 0)
            {
                newSelectedContext += ",";
            }

            newSelectedContext += newContext.ContextName;
            textBoxSelectedContexts.Text = newSelectedContext;
            kryptonRibbon.SelectedContext = newSelectedContext;
        }

        private void ColorChanged(object sender, EventArgs e)
        {
            if (radioOffice2003.Checked)
            {
                kryptonManager1.GlobalPaletteMode = PaletteMode.ProfessionalOffice2003;
            }
        }

        private void krgbTrigger1715_Click(object sender, EventArgs e)
        {
            kryptonRibbon.SelectedTab!.ContextName = @"Testing";
        }
    }
}
