#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonSearchBox), "ToolboxBitmaps.KryptonTextBox.bmp")]
    [DefaultEvent(nameof(SearchChanged))]
    [DefaultProperty(nameof(Text))]
    [DefaultBindingProperty(nameof(Text))]
    [Designer(typeof(KryptonTextBoxDesigner))]
    [DesignerCategory(@"code")]
    [Description(@"Enables the user to enter text, and provides multiline editing and password character masking.")]
    public class KryptonSearchBox : KryptonTextBox
    {
        #region Instance Fields

        private readonly ButtonSpecAny _clearButtonSpec;

        #endregion

        #region Events

        public event EventHandler? SearchChanged;

        #endregion

        #region Identity

        public KryptonSearchBox()
        {
            CueHint.CueHintText = KryptonManager.Strings.SearchBoxStrings.SearchBoxCueText;

            ToolTipValues.Heading = KryptonManager.Strings.SearchBoxStrings.ToolTipHeadingText;

            ToolTipValues.Description = KryptonManager.Strings.SearchBoxStrings.ToolTipBodyText;

            _clearButtonSpec = new ButtonSpecAny
            {
                Type = PaletteButtonSpecStyle.Close,
                Edge = PaletteRelativeEdgeAlign.Far,
                Style = PaletteButtonStyle.Standalone,
                Visible = false,
                UniqueName = "ClearSearchBoxButtonSpec",
                Enabled = ButtonEnabled.True,
                ToolTipTitle = KryptonManager.Strings.SearchBoxStrings.ClearSearchBoxToolTip,
                ToolTipBody = KryptonManager.Strings.SearchBoxStrings.ClearSearchBoxToolTipDescription
            };

            _clearButtonSpec.Click += ClearButtonSpec_Click;

            ButtonSpecs.Add(_clearButtonSpec);

            TextChanged += KryptonSearchBox_TextChanged;

            KeyDown += KryptonSearchBox_KeyDown;
        }

        #endregion

        #region Implementation

        private void KryptonSearchBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(Text))
            {
                SearchChanged?.Invoke(this, EventArgs.Empty);

                e.Handled = true;

                e.SuppressKeyPress = true;
            }
        }

        private void KryptonSearchBox_TextChanged(object? sender, EventArgs e) => _clearButtonSpec.Visible = !string.IsNullOrEmpty(Text);

        private void ClearButtonSpec_Click(object? sender, EventArgs e) => ClearSearch();

        private void ClearSearch()
        {
            Text = string.Empty;

            Focus();
        }

        #endregion

        [AllowNull]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text { get; set; } = GlobalStaticValues.DEFAULT_EMPTY_STRING;
    }
}
