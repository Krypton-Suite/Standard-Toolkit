namespace TestForm;

public partial class DateTimeExample : Form
{
    public DateTimeExample()
    {
        InitializeComponent();
    }

    private void kryptonColorButton1_SelectedColorChanged(object sender, ColorEventArgs e)
    {
        kryptonDateTimePicker1.StateCommon.Back.Color1 = e.Color;
    }
}