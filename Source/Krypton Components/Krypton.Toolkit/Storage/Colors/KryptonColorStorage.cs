using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Toolkit
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class KryptonColorStorage : Storage
    {
        public override bool IsDefault { get; }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
