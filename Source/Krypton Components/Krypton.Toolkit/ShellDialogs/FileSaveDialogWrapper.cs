#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using MsdnMag;

namespace Krypton.Toolkit;

/// <summary>
///  Displays a dialog window from which the user can select a file.
/// </summary>
[DefaultEvent(nameof(FileOk))]
[DefaultProperty(nameof(FileName))]
public abstract class FileSaveDialogWrapper : FileDialogWrapper
{
    #region Do_CBT

    private protected override void WndMessage(object sender, CWPRETSTRUCT e, out bool actioned)
    {
        base.WndMessage(sender, e, out actioned);

        if (e.message == PI.WM_.INITDIALOG)
        {
            // Hide the expand / collapse in the Save dialog
            var expander = _commonDialogHandler.Controls
                .FirstOrDefault(ctl => ctl.ClassName == @"toolbarwindow32");
            if (expander != null)
            {
                PI.ShowWindow(expander.hWnd, PI.ShowWindowCommands.SW_HIDE);
            }

            // Now make the Background Panel only occupy the bottom (Due to messed up transparency)
            var pnl = new KryptonPanel
            {
                Dock = DockStyle.Bottom,
                Height = (int)(56 * _scaleFactor),
                Name = "kryptonPanel2",
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            _commonDialogHandler._wrapperForm?.Controls.Add(pnl);
            foreach (KryptonPanel? parent in _commonDialogHandler.Controls.Where(static ctl => ctl.Button != null)
                         .Select(static ctl => ctl.Button)
                         .Select(ctl => ctl?.Parent as KryptonPanel)
                    )
            {
                parent!.Top = (int)(16 * _scaleFactor);
                parent.Anchor = AnchorStyles.Right;
                pnl.Controls.Add(parent);
            }
        }
    }

    private protected override void FormResize(object? sender, EventArgs e)
    {
        // Panel controls button placement now (Due to messed up transparency)
        //return;
    }

    private protected override bool WndActivated(object sender, CbtEventArgs e)
    {
        if (!base.WndActivated(sender, e))
        {
            // Not handled
            return false;
        }

        // Modify the FileDialog window
        // When it is the SaveDlg, the Backgrounds of the list's etc are all messed up if made transparent
        PI.SetWindowLong(_handle, PI.GWL_.EXSTYLE,
            PI.GetWindowLong(_handle, PI.GWL_.EXSTYLE) ^ PI.WS_EX_.TRANSPARENT);
        return true;
    }

    #endregion Do_CBT

}