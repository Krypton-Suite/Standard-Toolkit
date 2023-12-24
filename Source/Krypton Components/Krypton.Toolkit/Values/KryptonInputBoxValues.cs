using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Toolkit
{
    /// <summary>Access Krypton input box settings.</summary>
    [Category(@"Code")]
    [Description(@"Access Krypton input box settings.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class KryptonInputBoxValues : Storage
    {
        public override bool IsDefault => throw new NotImplementedException();
    }
}