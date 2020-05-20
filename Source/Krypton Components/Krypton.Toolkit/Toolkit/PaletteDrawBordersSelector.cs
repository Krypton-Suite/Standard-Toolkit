// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006-2020, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    [ToolboxItem(false)]
    internal partial class PaletteDrawBordersSelector : UserControl
    {
        /// <summary>
        /// Initialize a new instance of the PaletteDrawBordersSelector class.
        /// </summary>
        public PaletteDrawBordersSelector()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets and sets the value being edited.
        /// </summary>
        public PaletteDrawBorders Value
        {
            get
            {
                PaletteDrawBorders ret = PaletteDrawBorders.None;

                if (checkBoxInherit.Checked)
                {
                    ret = PaletteDrawBorders.Inherit;
                }
                else
                {
                    if (checkBoxTop.Checked)
                    {
                        ret |= PaletteDrawBorders.Top;
                    }

                    if (checkBoxBottom.Checked)
                    {
                        ret |= PaletteDrawBorders.Bottom;
                    }

                    if (checkBoxLeft.Checked)
                    {
                        ret |= PaletteDrawBorders.Left;
                    }

                    if (checkBoxRight.Checked)
                    {
                        ret |= PaletteDrawBorders.Right;
                    }
                }

                return ret;
            }

            set
            {
                if ((value & PaletteDrawBorders.Inherit) == PaletteDrawBorders.Inherit)
                {
                    checkBoxInherit.Checked = true;
                }
                else
                {
                    if ((value & PaletteDrawBorders.Top) == PaletteDrawBorders.Top)
                    {
                        checkBoxTop.Checked = true;
                    }

                    if ((value & PaletteDrawBorders.Bottom) == PaletteDrawBorders.Bottom)
                    {
                        checkBoxBottom.Checked = true;
                    }

                    if ((value & PaletteDrawBorders.Left) == PaletteDrawBorders.Left)
                    {
                        checkBoxLeft.Checked = true;
                    }

                    if ((value & PaletteDrawBorders.Right) == PaletteDrawBorders.Right)
                    {
                        checkBoxRight.Checked = true;
                    }
                }
            }
        }

        private void checkBoxInherit_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxTop.Enabled = checkBoxBottom.Enabled = !checkBoxInherit.Checked;
            checkBoxLeft.Enabled = checkBoxRight.Enabled = !checkBoxInherit.Checked;
        }
    }
}
