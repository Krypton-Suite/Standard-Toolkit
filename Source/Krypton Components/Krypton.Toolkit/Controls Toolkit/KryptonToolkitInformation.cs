namespace Krypton.Toolkit
{
    [ToolboxItem(false)]
    [DesignerCategory(@"code")]
    internal class KryptonToolkitInformation
    {
        #region Public

        public ToolkitType ToolkitType { get; set; } = ToolkitType.Stable;

        #endregion

        #region Implementation

        public static void ShowCore(ToolkitType toolkitType)
        {
            VisualToolkitInformationForm toolkitInformationForm = new VisualToolkitInformationForm(toolkitType)
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            toolkitInformationForm.ShowDialog();
        }

        #endregion
    }
}