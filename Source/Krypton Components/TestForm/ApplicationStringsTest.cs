#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

public partial class ApplicationStringsTest : KryptonForm
{
    private const string DemoKey = "SaveDraft";
    private const string DemoStringSetName = "DemoApp";

    private readonly DemoAppStrings _demoAppStrings = new DemoAppStrings();

    public ApplicationStringsTest()
    {
        InitializeComponent();

        KryptonCustomStrings.Set(DemoKey, "S&ave Draft");
        KryptonCustomStrings.RegisterStringSet(DemoStringSetName, _demoAppStrings);

        ApplyDictionaryDemoString();
        ApplyTypedDemoString();
    }

    private void ApplyDictionaryDemoString()
    {
        kbtnDemo.Text = KryptonCustomStrings.Get(DemoKey, DemoKey);
        ktxtValue.Text = KryptonCustomStrings.Get(DemoKey);
    }

    private void ApplyTypedDemoString()
    {
        DemoAppStrings? strings = KryptonCustomStrings.GetStringSet<DemoAppStrings>(DemoStringSetName);
        if (strings != null)
        {
            kbtnTypedDemo.Text = strings.SaveDraft;
            ktxtTypedValue.Text = strings.SaveDraft;
        }
    }

    private void kbtnUpdate_Click(object sender, EventArgs e)
    {
        KryptonCustomStrings.Set(DemoKey, ktxtValue.Text);
        ApplyDictionaryDemoString();
    }

    private void kbtnReset_Click(object sender, EventArgs e)
    {
        KryptonCustomStrings.Values.Remove(DemoKey);
        kbtnDemo.Text = DemoKey;
        ktxtValue.Text = string.Empty;
    }

    private void kbtnUpdateTyped_Click(object sender, EventArgs e)
    {
        DemoAppStrings? strings = KryptonCustomStrings.GetStringSet<DemoAppStrings>(DemoStringSetName);
        if (strings != null)
        {
            strings.SaveDraft = ktxtTypedValue.Text;
            ApplyTypedDemoString();
        }
    }

    private void kbtnResetTyped_Click(object sender, EventArgs e)
    {
        DemoAppStrings? strings = KryptonCustomStrings.GetStringSet<DemoAppStrings>(DemoStringSetName);
        strings?.Reset();
        ApplyTypedDemoString();
    }
}
