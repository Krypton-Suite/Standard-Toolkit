using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Toolkit
{
    internal class KryptonIntegratedToolBarManagerActionList : DesignerActionList
    {
        //public KryptonIntegratedToolBarManagerActionList(KryptonIntegratedToolBarManager owner) : base(owner.Component)
        //{
        //}
        public KryptonIntegratedToolBarManagerActionList(IComponent component) : base(component)
        {
        }
    }
}