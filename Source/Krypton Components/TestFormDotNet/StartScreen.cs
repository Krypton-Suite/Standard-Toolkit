using Krypton.Toolkit;

namespace TestFormDotNet
{
    public partial class StartScreen : KryptonForm
    {
        public StartScreen()
        {
            // Simple test without InitializeComponent
            Text = "KryptonForm .NET Designer Test";
            Size = new System.Drawing.Size(400, 300);
        }
    }
}
