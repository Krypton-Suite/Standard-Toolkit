using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Toolkit
{
    public class IntegratedToolBarValues : Storage
    {
        #region Instance Fields

        private bool _showIntegratedToolbar;

        private ButtonSpecAny[]? _integratedToolbarButtonCollection;

        private KryptonForm _owner;

        #endregion

        public override bool IsDefault { get; }
    }
}
