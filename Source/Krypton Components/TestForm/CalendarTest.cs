#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved. 
 *  
 */
#endregion

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForm
{
    public partial class CalendarTest : KryptonForm
    {
        public CalendarTest()
        {
            InitializeComponent();
        }

        private void kryptonMonthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            KryptonMessageBox.Show($"{kryptonDateTimePicker1.Value}");
        }
    }
}
