using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Toolkit
{
    internal class KryptonSystemMenuListener : NativeWindow
    {
        /*
         * What needs to be intercepted
         *      Right Mouse click in the title bar (the menu shows on the position of the mouse cursor)
         *      Left Mouse click on the ControlBox (the menu shows on the position of the ControlBox)
         *      ALT + Space (the menu shows on the position of the ControlBox)
         */

        public event Action? KeyAltSpaceDown;
        public event Action? NCRightMouseButtonDown;
        public event Action? NCLeftMouseButtonDown;


        private readonly KryptonForm _form;
        private ViewDrawDocker _drawHeading;

        public KryptonSystemMenuListener(KryptonForm kryptonForm, ViewDrawDocker drawHeading)
        {
            _form = kryptonForm;
            _drawHeading = drawHeading;

            AssignHandle(_form.Handle);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == PI.WM_.NCRBUTTONDOWN)
            {
                // Intercept Non Client Area Right Mouse Down
                // But it needs to know if the click happens on the title bar, otherwise
                // the message must be forwarded.
                //if (some where in the title bar)
                {

                    OnNCRightMouseButtonDown();

                    // eat the message....????
                    return;
                }
            }
            else if (m.Msg == PI.WM_.NCLBUTTONDOWN)
            {
                // Intercept Non Client Area Left Mouse Down.
                // But it needs to know if the ControlBox is Clicked
                if (_form.ControlBox)
                {
                    OnNCLeftMouseButtonDown();
                }
            }
            else if ((m.Msg & PI.WM_.SYSKEYDOWN) == PI.WM_.SYSKEYDOWN || (m.WParam.ToInt32() & PI.WM_.KEYDOWN) == PI.WM_.KEYDOWN)
            {
                if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
                {
                    if (m.WParam.ToInt32() == (int)Keys.Space)
                    {
                        // Intercept ALT + SPACE
                        OnKeyAltSpaceDown();

                        // eat the message
                        return;
                    }
                }
            }

            base.WndProc(ref m);
        }

        private void OnNCLeftMouseButtonDown()
        {
            Debug.Print($"OnNCLeftMouseButtonDown");
            NCLeftMouseButtonDown?.Invoke();
        }

        private void OnNCRightMouseButtonDown()
        {
            Debug.Print($"OnNCRightMouseButtonDown");
            NCRightMouseButtonDown?.Invoke();
        }

        private void OnKeyAltSpaceDown()
        {
            Debug.Print($"OnKeyAltSpaceDown");
            KeyAltSpaceDown?.Invoke();
        }
    }
}
