<#
.SYNOPSIS
    Opens a KryptonForm, shows the palette snap menu, and captures a PNG.

.DESCRIPTION
    Compiles a small net472 host beside the Debug Krypton.Toolkit build, shows the
    form, calls the private snap-layout popup, and composites PrintWindow captures
    of the form and the popup so the IDE cannot cover either HWND.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-SnapLayoutOverlayScreenshot.ps1

    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-SnapLayoutOverlayScreenshot.ps1 -CustomCard

    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-SnapLayoutOverlayScreenshot.ps1 -Ghost
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$OutputPath,
    [switch]$CustomCard,
    [switch]$Ghost
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $OutputPath) {
    $shotName = if ($Ghost) {
        '4579-snap-preview-ghost.png'
    }
    elseif ($CustomCard) {
        'custom-snap-layouts-menu.png'
    }
    else {
        '4579-snap-layout-overlay.png'
    }
    $OutputPath = Join-Path $repoRoot (Join-Path 'Documents\PR' $shotName)
}

$toolkit = Join-Path $bin 'Krypton.Toolkit.dll'
if (-not (Test-Path -LiteralPath $toolkit)) {
    throw "Krypton.Toolkit.dll was not found at $toolkit. Build Debug net472 first."
}

$outDir = Split-Path -Parent $OutputPath
if ($outDir -and -not (Test-Path -LiteralPath $outDir)) {
    New-Item -ItemType Directory -Path $outDir | Out-Null
}

$hostCs = Join-Path $bin 'SnapShotHost.cs'
$hostExe = Join-Path $bin 'SnapShotHost.exe'
$hostLog = Join-Path $bin 'SnapShotHost.log'
$hostSource = @'
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

internal static class SnapShotHost
{
    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

    [DllImport("user32.dll")]
    private static extern bool PrintWindow(IntPtr hWnd, IntPtr hdc, uint flags);

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    private const uint PW_RENDERFULLCONTENT = 2;

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [STAThread]
    private static int Main(string[] args)
    {
        string pngPath = args.Length > 0 ? args[0] : "snapshot.png";
        string logPath = args.Length > 1 ? args[1] : "snapshot-log.txt";
        try
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form = new Krypton.Toolkit.KryptonForm
            {
                Text = "4579 Snap Layout Overlay",
                StartPosition = FormStartPosition.Manual,
                Location = new Point(80, 80),
                ClientSize = new Size(720, 420),
                TopMost = true,
                PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue,
                Icon = (System.Drawing.Icon)System.Drawing.SystemIcons.Application.Clone()
            };
            var label = new Krypton.Toolkit.KryptonLabel
            {
                AutoSize = false,
                Location = new Point(16, 16),
                Size = new Size(680, 140)
            };
            label.Values.Text = "Issue #4579. Palette snap-layout menu under the maximize button.";
            form.Controls.Add(label);
            /*CUSTOM_SETUP*/
            form.Show();
            form.Activate();
            SetForegroundWindow(form.Handle);
            Application.DoEvents();
            Thread.Sleep(300);
            Application.DoEvents();

            MethodInfo show = null;
            for (Type owner = form.GetType(); owner != null && show == null; owner = owner.BaseType)
            {
                show = owner.GetMethod("ShowSnapLayoutPopup", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            }

            if (show == null)
            {
                File.WriteAllText(logPath, "ShowSnapLayoutPopup was not found.");
                return 2;
            }

            show.Invoke(form, null);
            Application.DoEvents();
            Thread.Sleep(200);
            Application.DoEvents();

            FieldInfo popupField = null;
            for (Type owner = form.GetType(); owner != null && popupField == null; owner = owner.BaseType)
            {
                popupField = owner.GetField("_snapLayoutPopup", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            }

            var popup = popupField == null ? null : popupField.GetValue(form) as Control;
            if (popup == null || !popup.IsHandleCreated)
            {
                File.WriteAllText(logPath, "Snap layout popup did not open.");
                return 3;
            }

            MethodInfo move = popup.GetType().GetMethod("OnMouseMove", BindingFlags.Instance | BindingFlags.NonPublic);
            if (move != null)
            {
                move.Invoke(popup, new object[] { new MouseEventArgs(MouseButtons.None, 0, 30, 30, 0) });
            }

            Application.DoEvents();
            Thread.Sleep(150);
            Application.DoEvents();

            FieldInfo previewField = popup.GetType().GetField("_preview", BindingFlags.Instance | BindingFlags.NonPublic);
            var preview = previewField == null ? null : previewField.GetValue(popup) as Control;
            bool hasPreview = preview != null && preview.IsHandleCreated && preview.Visible;
            Bitmap previewBmp = null;
            RECT previewRect = new RECT();
            if (hasPreview)
            {
                previewBmp = Capture(preview.Handle, out previewRect);
            }

            try
            {
                RECT formRect;
                RECT popupRect;
                using (Bitmap formBmp = Capture(form.Handle, out formRect))
                using (Bitmap popupBmp = Capture(popup.Handle, out popupRect))
                {
                    int left = Math.Min(formRect.Left, popupRect.Left);
                    int top = Math.Min(formRect.Top, popupRect.Top);
                    int right = Math.Max(formRect.Right, popupRect.Right);
                    int bottom = Math.Max(formRect.Bottom, popupRect.Bottom);
                    if (hasPreview)
                    {
                        left = Math.Min(left, previewRect.Left);
                        top = Math.Min(top, previewRect.Top);
                        right = Math.Max(right, previewRect.Right);
                        bottom = Math.Max(bottom, previewRect.Bottom);
                    }

                    using (var canvas = new Bitmap(right - left, bottom - top))
                    using (Graphics g = Graphics.FromImage(canvas))
                    {
                        g.Clear(Color.FromArgb(32, 32, 32));
                        if (hasPreview)
                        {
                            g.DrawImage(previewBmp, previewRect.Left - left, previewRect.Top - top);
                        }

                        g.DrawImage(formBmp, formRect.Left - left, formRect.Top - top);
                        g.DrawImage(popupBmp, popupRect.Left - left, popupRect.Top - top);
                        canvas.Save(pngPath, ImageFormat.Png);
                    }
                }
            }
            finally
            {
                if (previewBmp != null)
                {
                    previewBmp.Dispose();
                }
            }

            form.Close();
            File.WriteAllText(logPath, "saved " + pngPath);
            return 0;
        }
        catch (Exception ex)
        {
            Exception inner = ex;
            while (inner.InnerException != null)
            {
                inner = inner.InnerException;
            }

            File.WriteAllText(logPath, inner.ToString());
            return 1;
        }
    }

    private static Bitmap Capture(IntPtr hwnd, out RECT rect)
    {
        if (!GetWindowRect(hwnd, out rect))
        {
            throw new InvalidOperationException("GetWindowRect failed.");
        }

        int width = rect.Right - rect.Left;
        int height = rect.Bottom - rect.Top;
        var bmp = new Bitmap(width, height);
        using (Graphics g = Graphics.FromImage(bmp))
        {
            IntPtr hdc = g.GetHdc();
            try
            {
                PrintWindow(hwnd, hdc, PW_RENDERFULLCONTENT);
            }
            finally
            {
                g.ReleaseHdc(hdc);
            }
        }

        return bmp;
    }
}
'@
$customSetup = ''
if ($Ghost) {
    $customSetup = @'
            Krypton.Toolkit.KryptonManager.SnapLayoutValues.Mode = Krypton.Toolkit.KryptonSnapLayoutMode.Toolkit;
            Krypton.Toolkit.KryptonManager.SnapLayoutValues.ShowPreview = true;
            Krypton.Toolkit.KryptonManager.SnapLayoutValues.ShowPreviewGhost = true;
            Krypton.Toolkit.KryptonManager.SnapLayoutValues.PreviewOpacity = 0.55f;
            label.Values.Text = "ShowPreviewGhost stretches this window into the hot zone.";
'@
}
elseif ($CustomCard) {
    $customSetup = @'
            Krypton.Toolkit.KryptonManager.SnapLayoutValues.Mode = Krypton.Toolkit.KryptonSnapLayoutMode.Toolkit;
            Krypton.Toolkit.KryptonManager.SnapLayoutValues.CustomLayouts.Clear();
            Krypton.Toolkit.KryptonManager.SnapLayoutValues.CustomLayouts.Add(new Krypton.Toolkit.KryptonSnapLayoutCard(
                new Krypton.Toolkit.KryptonSnapLayoutZone(0f, 0f, 0.7f, 1f),
                new Krypton.Toolkit.KryptonSnapLayoutZone(0.7f, 0f, 0.3f, 1f)));
            label.Values.Text = "Custom 70/30 card appended after the builtin snap layouts.";
'@
}

$hostSource = $hostSource.Replace('/*CUSTOM_SETUP*/', $customSetup)
Set-Content -LiteralPath $hostCs -Value $hostSource -Encoding UTF8

$csc = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
& $csc /nologo /t:winexe /r:System.Windows.Forms.dll /r:System.Drawing.dll /r:$toolkit /out:$hostExe $hostCs
if ($LASTEXITCODE -ne 0) {
    throw "csc failed with exit code $LASTEXITCODE."
}

$p = Start-Process -FilePath $hostExe -ArgumentList $OutputPath, $hostLog -Wait -PassThru
if ($p.ExitCode -ne 0) {
    $detail = if (Test-Path -LiteralPath $hostLog) { Get-Content -LiteralPath $hostLog -Raw } else { 'No host log.' }
    throw "Snap layout screenshot host failed ($($p.ExitCode)). $detail"
}

Remove-Item -LiteralPath $hostCs, $hostExe, $hostLog -ErrorAction SilentlyContinue
Write-Output $OutputPath
