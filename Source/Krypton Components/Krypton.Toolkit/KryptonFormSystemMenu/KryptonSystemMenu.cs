using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Toolkit
{
    public class KryptonSystemMenu
    {
        private KryptonForm _form;
        private ViewDrawDocker _drawHeading;
        private KryptonSystemMenuListener _kryptonFormSystemMenuListener;

        public KryptonSystemMenu(KryptonForm kryptonForm, ViewDrawDocker drawHeading)
        {
            _form = kryptonForm;
            _drawHeading = drawHeading;
            _kryptonFormSystemMenuListener = new(_form, _drawHeading);
        }
    }
}
