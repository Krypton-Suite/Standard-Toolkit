namespace TestForm;

public partial class CheckBoxExtendedDemo : KryptonForm
{
    public CheckBoxExtendedDemo()
    {
        InitializeComponent();
    }

    private void CheckBoxExtendedDemo_Load(object sender, EventArgs e)
    {
        kcbxAgreement.Values.SubtextFont = new Font(Font.FontFamily, Font.SizeInPoints - 1F);
        kcbxAgreement.SubtextLinkValues.LinkArea = new LinkArea(18, 14);
        kcbxAgreement.SubtextLinkClicked += kcbxAgreement_SubtextLinkClicked;
        UpdateStatus();
    }

    private void kcbxAgreement_SubtextLinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
    {
        kwlblStatus.Text = @"Agreement link clicked. Open your terms document here.";
    }

    private void kcbxAgreement_CheckedChanged(object sender, EventArgs e) => UpdateStatus();

    private void UpdateStatus()
    {
        kwlblStatus.Text = $@"Agreement accepted: {kcbxAgreement.Checked}";
    }
}
