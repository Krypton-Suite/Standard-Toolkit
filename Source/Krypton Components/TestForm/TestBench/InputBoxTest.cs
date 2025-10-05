#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

public partial class InputBoxTest : KryptonForm
{
    private KryptonUseRTLLayout _useRtlLayout = KryptonUseRTLLayout.No;

    public InputBoxTest()
    {
        InitializeComponent();
    }

    private void kbtnTest_Click(object sender, EventArgs e)
    {
        KryptonInputBoxData data = new KryptonInputBoxData()
        {
            Caption = ktxtCaption.Text,
            CueColor = kcbtnCueTextColor.SelectedColor,
            CueText = ktxtCueText.Text,
            CueTypeface = null,
            DefaultResponse = ktxtDefaultResponse.Text,
            Owner = this,
            Prompt = krtxtPrompt.Text,
            UsePasswordOption = kcbUsePasswordOption.Checked,
            UseRTLLayout = _useRtlLayout
        };

        KryptonInputBox.Show(data);
    }

    private void kcbUseRTLOption_CheckedChanged(object sender, EventArgs e)
    {
        _useRtlLayout = kcbUseRTLOption.Checked ? KryptonUseRTLLayout.Yes : KryptonUseRTLLayout.No;
    }
}