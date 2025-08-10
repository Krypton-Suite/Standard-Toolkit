#region Licences
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    internal class CustomFormatRule
    {
        #region Public

        /// <summary>Shows the specified conditional format.</summary>
        /// <param name="conditionalFormat">The conditional format.</param>
        /// <param name="layout">The layout.</param>
        public static DialogResult Show(EnumConditionalFormatType conditionalFormat,
            RightToLeftLayout layout = RightToLeftLayout.LeftToRight) => ShowCore(conditionalFormat, layout);

        #endregion

        #region Implementation

        private static DialogResult ShowCore(EnumConditionalFormatType conditionalFormat, RightToLeftLayout layout)
        {
            if (layout == RightToLeftLayout.LeftToRight)
            {
                using var cfr = new VisualCustomFormatRuleForm(conditionalFormat);

                return cfr.ShowDialog();
            }
            else
            {
                using var cfrRTL = new VisualCustomFormatRuleRtlAwareForm(conditionalFormat);

                return cfrRTL.ShowDialog();
            }
        }

        #endregion
    }
}