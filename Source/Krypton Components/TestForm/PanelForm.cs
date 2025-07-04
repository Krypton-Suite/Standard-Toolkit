namespace TestForm;

public partial class PanelForm : KryptonForm
{
    public PanelForm()
    {
        InitializeComponent();
    }

    private void kryptonButton1_Click(object sender, EventArgs e)
    {
        MessageBox.Show(

            $"IsWindowsEleven: {OSUtilities.IsWindowsEleven}" +
            $"PlatformID: {OSUtilities.OsVersionInfo.PlatformId}"
            );
    }


}