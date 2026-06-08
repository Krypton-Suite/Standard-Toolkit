#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Krypton.Ribbon;
using Krypton.Toolkit;

namespace RibbonAccessibilityProbe
{
    internal static class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Any(arg => string.Equals(arg, "--show", StringComparison.OrdinalIgnoreCase)))
            {
                Application.Run(new ProbeForm());
                return 0;
            }

            try
            {
                RunAssertions();
                Console.WriteLine(@"Ribbon accessibility probe passed.");
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return 1;
            }
        }

        private static void RunAssertions()
        {
            using (var form = new ProbeForm())
            {
                form.Show();
                Application.DoEvents();
                form.Ribbon.PerformLayout();
                Application.DoEvents();

                AccessibleObject ribbonAccessible = form.Ribbon.AccessibilityObject;

                AccessibleObject homeTab = RequireDescendant(ribbonAccessible, @"Home");
                Require(homeTab.Role == AccessibleRole.PageTab, @"Home tab should expose PageTab role.");

                AccessibleObject group = RequireDescendant(ribbonAccessible, @"Clipboard");
                Require(group.Role == AccessibleRole.Grouping, @"Clipboard group should expose Grouping role.");

                AccessibleObject pasteButton = RequireDescendant(ribbonAccessible, @"Paste Special");
                Require(pasteButton.Role == AccessibleRole.PushButton, @"Paste button should expose PushButton role.");
                pasteButton.DoDefaultAction();
                Require(form.PasteClicks == 1, @"Paste button default action should invoke Click.");

                AccessibleObject boldCheckBox = RequireDescendant(ribbonAccessible, @"Bold");
                Require(boldCheckBox.Role == AccessibleRole.CheckButton, @"Bold checkbox should expose CheckButton role.");
                Require((boldCheckBox.State & AccessibleStates.Checked) == 0, @"Bold checkbox should start unchecked.");
                boldCheckBox.DoDefaultAction();
                Require(form.BoldCheckBox.Checked, @"Bold checkbox default action should toggle Checked.");
                Require((boldCheckBox.State & AccessibleStates.Checked) == AccessibleStates.Checked, @"Bold checkbox state should expose Checked after toggle.");

                AccessibleObject qatButton = RequireDescendant(ribbonAccessible, @"Quick Paste");
                qatButton.DoDefaultAction();
                Require(form.QatClicks == 1, @"QAT default action should invoke Click.");

                AccessibleObject customHost = RequireDescendant(ribbonAccessible, @"Search box");
                Require(!string.IsNullOrWhiteSpace(customHost.Name), @"Custom control host should expose a stable name.");
            }
        }

        private static AccessibleObject RequireDescendant(AccessibleObject root, string name)
        {
            AccessibleObject? found = Descendants(root).FirstOrDefault(child => string.Equals(child.Name, name, StringComparison.Ordinal));

            if (found == null)
            {
                throw new InvalidOperationException($@"Could not find accessible descendant named '{name}'.");
            }

            return found;
        }

        private static IEnumerable<AccessibleObject> Descendants(AccessibleObject root)
        {
            int childCount = root.GetChildCount();

            for (int i = 0; i < childCount; i++)
            {
                AccessibleObject? child = root.GetChild(i);

                if (child == null)
                {
                    continue;
                }

                yield return child;

                foreach (AccessibleObject descendant in Descendants(child))
                {
                    yield return descendant;
                }
            }
        }

        private static void Require(bool condition, string message)
        {
            if (!condition)
            {
                throw new InvalidOperationException(message);
            }
        }
    }

    internal sealed class ProbeForm : Form
    {
        private readonly KryptonRibbonGroupButton _pasteButton;
        private readonly KryptonRibbonQATButton _qatButton;

        public ProbeForm()
        {
            Text = @"Ribbon Accessibility Probe";
            Name = @"RibbonAccessibilityProbeForm";
            Size = new Size(900, 400);

            Ribbon = new KryptonRibbon
            {
                Name = @"probeRibbon",
                Dock = DockStyle.Top
            };

            var homeTab = new KryptonRibbonTab
            {
                Text = @"Home",
                KeyTip = @"H"
            };

            var clipboardGroup = new KryptonRibbonGroup
            {
                TextLine1 = @"Clipboard",
                KeyTipGroup = @"C"
            };

            var triple = new KryptonRibbonGroupTriple();

            _pasteButton = new KryptonRibbonGroupButton
            {
                TextLine1 = @"Paste",
                TextLine2 = @"Special",
                KeyTip = @"P"
            };
            _pasteButton.Click += OnPasteClick;

            BoldCheckBox = new KryptonRibbonGroupCheckBox
            {
                TextLine1 = @"Bold",
                KeyTip = @"B"
            };

            var customTextBox = new TextBox
            {
                Name = @"searchBox",
                AccessibleName = @"Search box",
                Text = @"Search"
            };

            var customHost = new KryptonRibbonGroupCustomControl
            {
                CustomControl = customTextBox,
                KeyTip = @"S"
            };

            triple.Items!.Add(_pasteButton);
            triple.Items.Add(BoldCheckBox);
            triple.Items.Add(customHost);
            clipboardGroup.Items.Add(triple);
            homeTab.Groups.Add(clipboardGroup);
            Ribbon.RibbonTabs.Add(homeTab);
            Ribbon.SelectedTab = homeTab;

            _qatButton = new KryptonRibbonQATButton
            {
                Text = @"Quick Paste"
            };
            _qatButton.Click += OnQatClick;
            Ribbon.QATButtons.Add(_qatButton);

            Controls.Add(Ribbon);
        }

        public KryptonRibbon Ribbon { get; }

        public KryptonRibbonGroupCheckBox BoldCheckBox { get; }

        public int PasteClicks { get; private set; }

        public int QatClicks { get; private set; }

        private void OnPasteClick(object? sender, EventArgs e) => PasteClicks++;

        private void OnQatClick(object? sender, EventArgs e) => QatClicks++;
    }
}
