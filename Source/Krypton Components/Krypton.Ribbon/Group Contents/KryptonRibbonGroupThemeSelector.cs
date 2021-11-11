using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Represents a ribbon group combo box.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonGroupComboBox), "ToolboxBitmaps.KryptonRibbonGroupComboBox.bmp")]
    [Designer("Krypton.Ribbon.KryptonRibbonGroupComboBoxDesigner, Krypton.Ribbon")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultEvent("SelectedTextChanged")]
    [DefaultProperty("Text")]
    public class KryptonRibbonGroupThemeSelector : KryptonRibbonGroupComboBox
    {
        #region Instance Fields

        private string[] _themeArray = new string[]
        {
            "Professional - System",

            "Professional - Office 2003",

            "Office 2007 - Black",

            "Office 2007 - Black (Dark Mode)",

            //"Office 2007 - Black (Light Mode)",

            "Office 2007 - Blue",

            "Office 2007 - Blue (Dark Mode)",

            "Office 2007 - Blue (Light Mode)",

            "Office 2007 - Silver",

            "Office 2007 - Silver (Dark Mode)",

            "Office 2007 - Silver (Light Mode)",

            "Office 2010 - Black",

            "Office 2010 - Black (Dark Mode)",

            //"Office 2010 - Black (Light Mode)",

            "Office 2010 - Blue",

            "Office 2010 - Blue (Dark Mode)",

            "Office 2010 - Blue (Light Mode)",

            "Office 2010 - Silver",

            "Office 2010 - Silver (Dark Mode)",

            "Office 2010 - Silver (Light Mode)",

            "Office 2010 - White",

            "Office 2013",

            "Office 365 - Black",

            "Office 365 - Black (Dark Mode)",

            //"Office 365 - Black (Light Mode)",

            "Office 365 - Blue",

            "Office 365 - Blue (Dark Mode)",

            "Office 365 - Blue (Light Mode)",

            "Office 365 - Silver",

            "Office 365 - Silver (Dark Mode)",

            "Office 365 - Silver (Light Mode)",

            "Office 365 - White",

            "Sparkle - Blue",

            "Sparkle - Blue (Dark Mode)",

            "Sparkle - Blue (Light Mode)",

            "Sparkle - Orange",

            "Sparkle - Orange (Dark Mode)",

            "Sparkle - Orange (Light Mode)",

            "Sparkle - Purple",

            "Sparkle - Purple (Dark Mode)",

            "Sparkle - Purple (Light Mode)",

            "Custom"
        };

        #endregion

        #region Identity

        public KryptonRibbonGroupThemeSelector()
        {
            foreach (string theme in _themeArray)
            {
                Items.Add(theme);
            }
        }
        #endregion
    }
}
