namespace Krypton.Toolkit
{
    internal class OverrideComboBoxStyleDropDownStyle : UITypeEditor
    {
        /// <summary>
        /// Gets the editor style used by the EditValue method.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
        /// <returns>UITypeEditorEditStyle value.</returns>
        /// <remarks>
        /// We show a drop down for editing the PaletteDrawBorders value.
        /// </remarks>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) => context?.Instance != null
            ? UITypeEditorEditStyle.DropDown
            : base.GetEditStyle(context);


        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider?.GetService(typeof(IWindowsFormsEditorService));
            if (svc != null)
            {
                UserControl ctrl = new();
                ListBox clb = new() { Dock = DockStyle.Fill };
                clb.Items.Add(ComboBoxStyle.DropDown);
                clb.Items.Add(ComboBoxStyle.DropDownList);
                clb.SelectedIndexChanged += delegate
                    {
                        value = Enum.Parse(typeof(ComboBoxStyle), clb.SelectedItem.ToString());
                        svc.CloseDropDown();
                    };
                ctrl.Controls.Add(clb);
                svc.DropDownControl(ctrl);
            }

            return value;
        }
    }
}
