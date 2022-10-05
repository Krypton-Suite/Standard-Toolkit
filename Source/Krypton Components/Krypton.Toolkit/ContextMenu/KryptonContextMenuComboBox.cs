namespace Krypton.Toolkit
{
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonContextMenuComboBox), "ToolboxBitmaps.KryptonComboBox.bmp")]
    [DesignerCategory(@"code")]
    [DesignTimeVisible(false)]
    [DefaultProperty("Text")]
    [DefaultEvent("SelectedIndexChanged")]
    public class KryptonContextMenuComboBox : KryptonContextMenuItemBase
    {
        public override int ItemChildCount { get; }

        public override KryptonContextMenuItemBase this[int index] => throw new NotImplementedException();

        public override bool ProcessShortcut(Keys keyData)
        {
            throw new NotImplementedException();
        }

        public override ViewBase GenerateView(IContextMenuProvider provider, object parent, ViewLayoutStack columns, bool standardStyle,
            bool imageColumn)
        {
            throw new NotImplementedException();
        }
    }
}