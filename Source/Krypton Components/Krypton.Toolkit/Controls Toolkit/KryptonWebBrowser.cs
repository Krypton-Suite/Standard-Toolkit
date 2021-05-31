#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2021. All rights reserved. 
 *  
 */
#endregion

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Provide a WebBrowser control with Krypton styling applied.
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(WebBrowser)/*, "ToolboxBitmaps.KryptonRichTextBox.bmp"*/)]
    [Designer(typeof(KryptonWebBrowserDesigner))]
    [DesignerCategory("code")]
    [Description("Enables the user to browse web page, inside your form. Mainly to be used as a Rich Text Editor")]
    public class KryptonWebBrowser : VisualControlBase,
                                    IContainedInputControl
    {
        public Control ContainedControl { get; }
    }
}
