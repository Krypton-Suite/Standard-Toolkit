#region Licences

/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */

//--------------------------------------------------------------------------------
// Copyright (C) 2013-2021 JDH Software - <support@jdhsoftware.com>
//
// This program is provided to you under the terms of the Microsoft Public
// License (Ms-PL) as published at https://github.com/Cocotteseb/Krypton-OutlookGrid/blob/master/LICENSE.md
//
// Visit https://www.jdhsoftware.com and follow @jdhsoftware on Twitter
//
//--------------------------------------------------------------------------------

#endregion

namespace Krypton.Toolkit
{
    internal partial class VisualCustomFormatRuleForm : KryptonForm
    {
        #region Instance Fields

        private bool _gradient;

        private Color _minimumColor;

        private Color _maximumColor;

        private Color _intermediateColor;

        private EnumConditionalFormatType _conditionalFormatType;

        #endregion

        #region Public

        public bool Gradient => _gradient;

        public Color MaximumColor => _maximumColor;

        public Color MinimumColor => _minimumColor;

        public Color IntermediateColor => _intermediateColor;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="VisualCustomFormatRuleForm" /> class.</summary>
        /// <param name="conditionalFormatType">Type of the conditional format.</param>
        public VisualCustomFormatRuleForm(EnumConditionalFormatType conditionalFormatType)
        {
            InitializeComponent();

            _conditionalFormatType = conditionalFormatType;

            Initialize();
        }

        #endregion

        #region Implementation

        private void Initialize()
        {
            // Set up text
            Text = KryptonManager.Strings.OutlookGridStrings.CustomFormatWindowTitle;

            klblFill.Text = KryptonManager.Strings.OutlookGridStrings.CustomFormatFillLabelText;

            klblFormat.Text = KryptonManager.Strings.OutlookGridStrings.CustomFormatLabelText;

            klblPreview.Text = KryptonManager.Strings.OutlookGridStrings.CustomFormatPreviewLabelText;

            kcolbtnIntermediateColor.Text = KryptonManager.Strings.OutlookGridStrings
                .CustomFormatIntermediateColorButtonText;

            kcolbtnMaximumColor.Text =
                KryptonManager.Strings.OutlookGridStrings.CustomFormatMaximumColorButtonText;

            kcolbtnMinimumColor.Text =
                KryptonManager.Strings.OutlookGridStrings.CustomFormatMinimumColorButtonText;

            kbtnCancel.Text = KryptonManager.Strings.GeneralStrings.Cancel;

            kbtnOk.Text = KryptonManager.Strings.GeneralStrings.OK;

            kcmbFillMode.SelectedIndex = 0;

            kcmbFormatStyle.SelectedIndex = -1;

            _maximumColor = Color.FromArgb(243, 120, 97);

            _intermediateColor = Color.FromArgb(252, 229, 130);

            _minimumColor = Color.FromArgb(84, 179, 122);
        }

        private void VisualCustomFormatRuleForm_Load(object sender, EventArgs e)
        {
            kcolbtnMinimumColor.SelectedColor = _minimumColor;

            kcolbtnIntermediateColor.SelectedColor = _intermediateColor;

            kcolbtnMaximumColor.SelectedColor = _maximumColor;

            int selected = -1;

            string[] names = Enum.GetNames(typeof(EnumConditionalFormatType));

            for (int i = 0; i < names.Length; i++)
            {
                if (_conditionalFormatType.ToString().Equals(names[i]))
                {
                    selected = i;
                }

                kcmbFormatStyle.Items.Add(new KryptonListItem(OutlookGridLanguageManager.Instance.GetString(names[i])));
            }

            kcmbFormatStyle.SelectedIndex = selected;
        }

        private void UpdateFormatType(EnumConditionalFormatType conditionalFormatType)
        {
            switch (conditionalFormatType)
            {
                case EnumConditionalFormatType.TwoColorsRange:
                    klblFill.Visible = false;
                    kcmbFillMode.Visible = false;
                    kcolbtnMinimumColor.Visible = true;
                    kcolbtnIntermediateColor.Visible = false;
                    kcolbtnMaximumColor.Visible = true;
                    break;
                case EnumConditionalFormatType.ThreeColorsRange:
                    klblFill.Visible = false;
                    kcmbFillMode.Visible = false;
                    kcolbtnMinimumColor.Visible = true;
                    kcolbtnIntermediateColor.Visible = true;
                    kcolbtnMaximumColor.Visible = true;
                    break;
                case EnumConditionalFormatType.Bar:
                    klblFill.Visible = true;
                    kcmbFillMode.Visible = true;
                    kcolbtnMinimumColor.Visible = true;
                    kcolbtnIntermediateColor.Visible = false;
                    kcolbtnMaximumColor.Visible = false;
                    break;
            }

            kpbxPreview.Invalidate();
        }

        private void kpbxPreview_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            switch (_conditionalFormatType)
            {
                case EnumConditionalFormatType.Bar:
                    if (_gradient)
                    {
                        using (LinearGradientBrush br = new LinearGradientBrush(e.ClipRectangle, _minimumColor, Color.White, LinearGradientMode.Horizontal))
                        {
                            e.Graphics.FillRectangle(br, e.ClipRectangle);
                        }
                    }
                    else
                    {
                        using (SolidBrush br = new SolidBrush(_minimumColor))
                        {
                            e.Graphics.FillRectangle(br, e.ClipRectangle);
                        }
                    }
                    using (Pen pen = new Pen(_minimumColor)) //Color.FromArgb(255, 140, 197, 66)))
                    {
                        Rectangle rect = e.ClipRectangle;
                        rect.Inflate(-1, -1);
                        e.Graphics.DrawRectangle(pen, rect);
                    }
                    break;
                case EnumConditionalFormatType.TwoColorsRange:
                    // Draw the background gradient.
                    using (LinearGradientBrush br = new LinearGradientBrush(e.ClipRectangle, _minimumColor, _maximumColor, LinearGradientMode.Horizontal))
                    {
                        e.Graphics.FillRectangle(br, e.ClipRectangle);
                    }
                    break;
                case EnumConditionalFormatType.ThreeColorsRange:
                    // Draw the background gradient.              
                    using (LinearGradientBrush br = new LinearGradientBrush(e.ClipRectangle, _minimumColor, _maximumColor, LinearGradientMode.Horizontal))
                    {
                        ColorBlend blend = new ColorBlend();
                        blend.Colors = new[] { _minimumColor, _intermediateColor, _maximumColor };
                        blend.Positions = new[] { 0f, 0.5f, 1.0f };
                        br.InterpolationColors = blend;
                        e.Graphics.FillRectangle(br, e.ClipRectangle);
                    }
                    break;
            }
        }

        private ArrayList GetEnumConditionalFormatTypeList()
        {
            ArrayList values = new ArrayList();

            foreach (var value in Enum.GetValues(typeof(EnumConditionalFormatType)))
            {
                values.Add(value);
            }

            return values;
        }

        private void kbtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void kbtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void kcolbtnMinimumColor_SelectedColorChanged(object sender, ColorEventArgs e)
        {
            _minimumColor = e.Color;

            kpbxPreview.Invalidate();
        }

        private void kcolbtnIntermediateColor_SelectedColorChanged(object sender, ColorEventArgs e)
        {
            _intermediateColor = e.Color;

            kpbxPreview.Invalidate();
        }

        private void kcolbtnMaximumColor_SelectedColorChanged(object sender, ColorEventArgs e)
        {
            _maximumColor = e.Color;

            kpbxPreview.Invalidate();
        }

        private void kcmbFillMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void kcmbFormatStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tag = (kcmbFormatStyle.Items[kcmbFormatStyle.SelectedIndex] as KryptonListItem)?.Tag;

            if (tag is not null)
            {
                _conditionalFormatType = (EnumConditionalFormatType)Enum.Parse(typeof(EnumConditionalFormatType), tag.ToString()!);
            }

            UpdateFormatType(_conditionalFormatType);
        }

        #endregion
    }
}