using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestForm
{
    internal static class Extensions
    {
        public static void SetDoubleBuffered(this Control control, bool enableDoubleBuffering)
        {
            PropertyInfo? propertyInfo = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            if (propertyInfo is not null)
            {
                propertyInfo.SetValue(control, enableDoubleBuffering);
            }
            else
            {
                ThrowHelper.ThrowNullReferenceException(nameof(propertyInfo));
            }
        }
    }
}
