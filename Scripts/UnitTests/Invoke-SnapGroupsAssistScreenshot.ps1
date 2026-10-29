<#
.SYNOPSIS
    Captures Snap Groups caption and Assist picker PNGs for #4579.

.DESCRIPTION
    Hosts two KryptonForms, registers a half/half group, opens the toolkit menu,
    and composites PrintWindow captures including the group caption. A second
    run snaps one form with Assist enabled and captures the peer picker.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-SnapGroupsAssistScreenshot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$GroupOutputPath,
    [string]$AssistOutputPath
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $GroupOutputPath) {
    $GroupOutputPath = Join-Path $repoRoot 'Documents\PR\4579-snap-groups-assist-group.png'
}
if (-not $AssistOutputPath) {
    $AssistOutputPath = Join-Path $repoRoot 'Documents\PR\4579-snap-groups-assist-assist.png'
}

$toolkit = Join-Path $bin 'Krypton.Toolkit.dll'
if (-not (Test-Path -LiteralPath $toolkit)) {
    throw "Krypton.Toolkit.dll was not found at $toolkit. Build Debug net472 first."
}

foreach ($path in @($GroupOutputPath, $AssistOutputPath)) {
    $dir = Split-Path -Parent $path
    if ($dir -and -not (Test-Path -LiteralPath $dir)) {
        New-Item -ItemType Directory -Path $dir | Out-Null
    }
}

$hostCs = Join-Path $bin 'SnapGroupsAssistHost.cs'
$hostExe = Join-Path $bin 'SnapGroupsAssistHost.exe'
$hostLog = Join-Path $bin 'SnapGroupsAssistHost.log'
$hostSource = @'
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

internal static class SnapGroupsAssistHost
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
        string mode = args.Length > 0 ? args[0] : "group";
        string pngPath = args.Length > 1 ? args[1] : "snapshot.png";
        string logPath = args.Length > 2 ? args[2] : "snapshot-log.txt";
        try
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Krypton.Toolkit.KryptonManager.SnapLayoutValues.Mode = Krypton.Toolkit.KryptonSnapLayoutMode.Toolkit;
            Krypton.Toolkit.KryptonManager.SnapLayoutValues.RememberGroups = true;
            Krypton.Toolkit.KryptonManager.SnapLayoutValues.ShowAssist = true;
            Krypton.Toolkit.KryptonManager.SnapLayoutValues.ShowPreview = false;

            if (string.Equals(mode, "assist", StringComparison.OrdinalIgnoreCase))
            {
                return CaptureAssist(pngPath, logPath);
            }

            return CaptureGroup(pngPath, logPath);
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

    private static int CaptureGroup(string pngPath, string logPath)
    {
        var left = CreateForm("4579 Group Left", new Point(60, 60));
        var right = CreateForm("4579 Group Right", new Point(500, 80));
        left.Show();
        right.Show();
        left.Activate();
        SetForegroundWindow(left.Handle);
        Application.DoEvents();
        Thread.Sleep(200);

        Rectangle work = Screen.FromHandle(left.Handle).WorkingArea;
        var zones = new Krypton.Toolkit.KryptonSnapLayoutZone[]
        {
            new Krypton.Toolkit.KryptonSnapLayoutZone(0f, 0f, 0.5f, 1f),
            new Krypton.Toolkit.KryptonSnapLayoutZone(0.5f, 0f, 0.5f, 1f)
        };
        InvokeApply(left, zones, 0, work, false);
        InvokeApply(right, zones, 1, work, false);
        Application.DoEvents();
        Thread.Sleep(150);

        MethodInfo show = FindMethod(left, "ShowSnapLayoutPopup");
        if (show == null)
        {
            File.WriteAllText(logPath, "ShowSnapLayoutPopup was not found.");
            return 2;
        }

        show.Invoke(left, null);
        Application.DoEvents();
        Thread.Sleep(250);

        Control popup = GetFieldControl(left, "_snapLayoutPopup");
        if (popup == null || !popup.IsHandleCreated)
        {
            File.WriteAllText(logPath, "Snap layout popup did not open.");
            return 3;
        }

        MethodInfo move = popup.GetType().GetMethod("OnMouseMove", BindingFlags.Instance | BindingFlags.NonPublic);
        if (move != null)
        {
            // First builtin card is usually side-by-side halves near the top-left of the popup.
            move.Invoke(popup, new object[] { new MouseEventArgs(MouseButtons.None, 0, 36, 36, 0) });
        }

        Application.DoEvents();
        Thread.Sleep(200);
        Control caption = GetFieldControl(popup, "_groupCaption");
        Composite(pngPath, left, popup, caption);
        left.Close();
        right.Close();
        File.WriteAllText(logPath, "saved " + pngPath);
        return 0;
    }

    private static int CaptureAssist(string pngPath, string logPath)
    {
        var host = CreateForm("4579 Assist Host", new Point(80, 80));
        var peer = CreateForm("4579 Assist Peer", new Point(520, 120));
        peer.ShowInTaskbar = true;
        host.Show();
        peer.Show();
        host.Activate();
        SetForegroundWindow(host.Handle);
        Application.DoEvents();
        Thread.Sleep(200);

        Rectangle work = Screen.FromHandle(host.Handle).WorkingArea;
        var zones = new Krypton.Toolkit.KryptonSnapLayoutZone[]
        {
            new Krypton.Toolkit.KryptonSnapLayoutZone(0f, 0f, 0.5f, 1f),
            new Krypton.Toolkit.KryptonSnapLayoutZone(0.5f, 0f, 0.5f, 1f)
        };
        InvokeApply(host, zones, 0, work, true);
        Application.DoEvents();
        Thread.Sleep(300);

        Control assist = GetFieldControl(host, "_snapAssistPopup");
        if (assist == null || !assist.IsHandleCreated)
        {
            File.WriteAllText(logPath, "Assist popup did not open.");
            return 4;
        }

        Composite(pngPath, host, assist, null);
        host.Close();
        peer.Close();
        File.WriteAllText(logPath, "saved " + pngPath);
        return 0;
    }

    private static Krypton.Toolkit.KryptonForm CreateForm(string title, Point location)
    {
        var form = new Krypton.Toolkit.KryptonForm
        {
            Text = title,
            StartPosition = FormStartPosition.Manual,
            Location = location,
            ClientSize = new Size(360, 220),
            TopMost = true,
            PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue,
            Icon = (Icon)SystemIcons.Application.Clone(),
            ShowInTaskbar = true
        };
        var label = new Krypton.Toolkit.KryptonLabel
        {
            AutoSize = false,
            Dock = DockStyle.Fill
        };
        label.Values.Text = title;
        form.Controls.Add(label);
        return form;
    }

    private static void InvokeApply(Krypton.Toolkit.KryptonForm form, Krypton.Toolkit.KryptonSnapLayoutZone[] zones, int zoneIndex, Rectangle work, bool offerAssist)
    {
        MethodInfo apply = FindMethod(form, "ApplyToolkitSnap");
        if (apply == null)
        {
            throw new InvalidOperationException("ApplyToolkitSnap was not found.");
        }

        apply.Invoke(form, new object[] { zones, zoneIndex, work, offerAssist });
    }

    private static MethodInfo FindMethod(object instance, string name)
    {
        for (Type owner = instance.GetType(); owner != null; owner = owner.BaseType)
        {
            MethodInfo method = owner.GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            if (method != null)
            {
                return method;
            }
        }

        return null;
    }

    private static Control GetFieldControl(object instance, string name)
    {
        for (Type owner = instance.GetType(); owner != null; owner = owner.BaseType)
        {
            FieldInfo field = owner.GetField(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            if (field != null)
            {
                return field.GetValue(instance) as Control;
            }
        }

        return null;
    }

    private static void Composite(string pngPath, Control primary, Control secondary, Control tertiary)
    {
        RECT a;
        RECT b;
        using (Bitmap bmpA = Capture(primary.Handle, out a))
        using (Bitmap bmpB = Capture(secondary.Handle, out b))
        {
            int left = Math.Min(a.Left, b.Left);
            int top = Math.Min(a.Top, b.Top);
            int right = Math.Max(a.Right, b.Right);
            int bottom = Math.Max(a.Bottom, b.Bottom);
            Bitmap bmpC = null;
            RECT c = new RECT();
            if (tertiary != null && tertiary.IsHandleCreated && tertiary.Visible)
            {
                bmpC = Capture(tertiary.Handle, out c);
                left = Math.Min(left, c.Left);
                top = Math.Min(top, c.Top);
                right = Math.Max(right, c.Right);
                bottom = Math.Max(bottom, c.Bottom);
            }

            try
            {
                using (var canvas = new Bitmap(Math.Max(1, right - left), Math.Max(1, bottom - top)))
                using (Graphics g = Graphics.FromImage(canvas))
                {
                    g.Clear(Color.FromArgb(32, 32, 32));
                    g.DrawImage(bmpA, a.Left - left, a.Top - top);
                    g.DrawImage(bmpB, b.Left - left, b.Top - top);
                    if (bmpC != null)
                    {
                        g.DrawImage(bmpC, c.Left - left, c.Top - top);
                    }

                    canvas.Save(pngPath, ImageFormat.Png);
                }
            }
            finally
            {
                if (bmpC != null)
                {
                    bmpC.Dispose();
                }
            }
        }
    }

    private static Bitmap Capture(IntPtr hwnd, out RECT rect)
    {
        if (!GetWindowRect(hwnd, out rect))
        {
            throw new InvalidOperationException("GetWindowRect failed.");
        }

        int width = Math.Max(1, rect.Right - rect.Left);
        int height = Math.Max(1, rect.Bottom - rect.Top);
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

Set-Content -LiteralPath $hostCs -Value $hostSource -Encoding UTF8
$csc = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
& $csc /nologo /t:winexe /r:System.Windows.Forms.dll /r:System.Drawing.dll /r:$toolkit /out:$hostExe $hostCs
if ($LASTEXITCODE -ne 0) {
    throw "csc failed with exit code $LASTEXITCODE."
}

$pGroup = Start-Process -FilePath $hostExe -ArgumentList @('group', $GroupOutputPath, $hostLog) -Wait -PassThru
if ($pGroup.ExitCode -ne 0) {
    $detail = if (Test-Path -LiteralPath $hostLog) { Get-Content -LiteralPath $hostLog -Raw } else { 'No host log.' }
    throw "Group screenshot host failed ($($pGroup.ExitCode)). $detail"
}

$pAssist = Start-Process -FilePath $hostExe -ArgumentList @('assist', $AssistOutputPath, $hostLog) -Wait -PassThru
if ($pAssist.ExitCode -ne 0) {
    $detail = if (Test-Path -LiteralPath $hostLog) { Get-Content -LiteralPath $hostLog -Raw } else { 'No host log.' }
    throw "Assist screenshot host failed ($($pAssist.ExitCode)). $detail"
}

Remove-Item -LiteralPath $hostCs, $hostExe, $hostLog -ErrorAction SilentlyContinue
Write-Output $GroupOutputPath
Write-Output $AssistOutputPath
