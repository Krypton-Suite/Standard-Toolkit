<#
.SYNOPSIS
    Exercises Krypton file-operation dialogs and writes PrintWindow PNGs.

.DESCRIPTION
    Loads the Debug Krypton.Dialogs build in-process (STA). Confirms copy, replace,
    skip, keep-both, rename, and permanent delete against a temp sandbox, then
    captures the conflict, recycle, permanent-delete, and progress dialogs.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-FileOperationDialogScreenshot.ps1 -TargetFramework net10.0-windows
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net10.0-windows',
    [string]$BinDir,
    [string]$OutputDirectory,
    [switch]$DemoOnly
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $OutputDirectory) {
    $OutputDirectory = Join-Path $repoRoot 'Documents\PR'
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
foreach ($name in @('Krypton.Interop.dll', 'Krypton.Resources.dll', 'Krypton.Toolkit.dll', 'Krypton.Dialogs.dll')) {
    [void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin $name))
}

[System.Windows.Forms.Application]::EnableVisualStyles()

function New-Operation {
    $operation = New-Object Krypton.Dialogs.KryptonFileOperation
    $operation.add_Error({
        param($sender, $e)
        Write-Output ("Error {0}: {1}" -f $e.Path, $e.Message)
    })
    return $operation
}

function Confirm-Delete {
    param($operation, [bool]$Permanent)
    $handler = {
        param($sender, $e)
        $e.Handled = $true
        $e.Confirmed = $true
    }
    if ($Permanent) {
        $operation.add_ConfirmPermanentDelete($handler)
    }
    else {
        $operation.add_ConfirmDelete($handler)
    }
}

$demoPath = Join-Path $OutputDirectory '4672-file-operation-demo.png'
if ($DemoOnly) {
    $testForm = Join-Path $bin 'TestForm.exe'
    [void][System.Reflection.Assembly]::LoadFrom($testForm)
    $demoType = [System.Reflection.Assembly]::LoadFrom($testForm).GetType('TestForm.FileOperationProgressDemo')
    $demo = [System.Activator]::CreateInstance($demoType)
    $demo.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $demo.Location = New-Object System.Drawing.Point 80, 80
    $demo.TopMost = $true
    $demo.Show()
    $demo.Activate()
    [void][UnitTestNative]::SetForegroundWindow($demo.Handle)
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 400
    Save-UnitTestWindowPng -Form $demo -Path $demoPath -SettleMs 150
    $demo.Close()
    $demo.Dispose()
    Write-Output "Wrote $demoPath"
    return
}

$root = Join-Path ([System.IO.Path]::GetTempPath()) ('krypton-file-op-' + [guid]::NewGuid().ToString('N'))
[System.IO.Directory]::CreateDirectory((Join-Path $root 'source')) | Out-Null
[System.IO.Directory]::CreateDirectory((Join-Path $root 'dest')) | Out-Null
$sourceFile = Join-Path $root 'source\notes.txt'
$destFile = Join-Path $root 'dest\notes.txt'
[System.IO.File]::WriteAllText($sourceFile, 'from-source')
[System.IO.File]::WriteAllText($destFile, 'already-there')

$copy = New-Operation
if (-not $copy.Copy($sourceFile, (Join-Path $root 'copied'))) {
    throw 'Copy failed.'
}
$copied = Join-Path $root 'copied\notes.txt'
if ((Get-Content -LiteralPath $copied -Raw) -ne 'from-source') {
    throw 'Copied content did not match.'
}
$copy.Dispose()

$replace = New-Operation
$replace.add_FileConflict({
    param($sender, $e)
    $e.Handled = $true
    $e.Choice = [Krypton.Dialogs.KryptonFileConflictChoice]::Replace
})
if (-not $replace.Copy($sourceFile, (Join-Path $root 'dest'), 'notes.txt')) {
    throw 'Replace failed.'
}
if ((Get-Content -LiteralPath $destFile -Raw) -ne 'from-source') {
    throw 'Replace did not overwrite the destination.'
}
$replace.Dispose()

[System.IO.File]::WriteAllText($destFile, 'keep-me')
$skip = New-Operation
$skip.add_FileConflict({
    param($sender, $e)
    $e.Handled = $true
    $e.Choice = [Krypton.Dialogs.KryptonFileConflictChoice]::Skip
})
if (-not $skip.Copy($sourceFile, (Join-Path $root 'dest'), 'notes.txt')) {
    throw 'Skip failed.'
}
if ((Get-Content -LiteralPath $destFile -Raw) -ne 'keep-me') {
    throw 'Skip overwrote the destination.'
}
$skip.Dispose()

$keep = New-Operation
$keep.add_FileConflict({
    param($sender, $e)
    $e.Handled = $true
    $e.Choice = [Krypton.Dialogs.KryptonFileConflictChoice]::KeepBoth
})
if (-not $keep.Copy($sourceFile, (Join-Path $root 'dest'), 'notes.txt')) {
    throw 'Keep both failed.'
}
$kept = Join-Path $root 'dest\notes (2).txt'
if (-not (Test-Path -LiteralPath $kept)) {
    throw 'Keep both did not create notes (2).txt.'
}
$keep.Dispose()

$renameSource = Join-Path $root 'source\rename-me.txt'
[System.IO.File]::WriteAllText($renameSource, 'rename')
$rename = New-Operation
if (-not $rename.Rename($renameSource, 'renamed.txt')) {
    throw 'Rename failed.'
}
if (-not (Test-Path -LiteralPath (Join-Path $root 'source\renamed.txt'))) {
    throw 'Rename did not create renamed.txt.'
}
$rename.Dispose()

$delete = New-Operation
Confirm-Delete -operation $delete -Permanent $true
if (-not $delete.Delete((Join-Path $root 'source\renamed.txt'), $false)) {
    throw 'Permanent delete failed.'
}
if (Test-Path -LiteralPath (Join-Path $root 'source\renamed.txt')) {
    throw 'Permanent delete left the file in place.'
}
$delete.Dispose()
Write-Output 'File operation checks passed.'

function Save-ModalDialog {
    param(
        [scriptblock]$Show,
        [string]$Path
    )
    $script:modalCapturePath = $Path
    $timer = New-Object System.Windows.Forms.Timer
    $timer.Interval = 500
    $timer.Add_Tick({
        param($sender, $eventArgs)
        try {
            if ($null -ne $sender) {
                $sender.Stop()
            }
            $dialog = $null
            foreach ($candidate in [System.Windows.Forms.Application]::OpenForms) {
                if ($null -eq $candidate) {
                    continue
                }
                if ($candidate -is [Krypton.Toolkit.KryptonForm] -and $candidate.Visible) {
                    $dialog = $candidate
                }
            }
            if ($null -eq $dialog) {
                throw "No dialog was visible for $($script:modalCapturePath)"
            }
            $dialog.TopMost = $true
            Save-UnitTestWindowPng -Form $dialog -Path $script:modalCapturePath -SettleMs 150
            $dialog.Close()
        }
        catch {
            $script:modalCaptureError = $_
            if ($null -ne $sender) {
                $sender.Stop()
            }
        }
    })
    $script:modalCaptureError = $null
    $timer.Start()
    & $Show
    $timer.Stop()
    $timer.Dispose()
    if ($null -ne $script:modalCaptureError) {
        throw $script:modalCaptureError
    }
}

$conflictPath = Join-Path $OutputDirectory '4672-file-conflict.png'
$recyclePath = Join-Path $OutputDirectory '4672-delete-confirmation.png'
$permanentPath = Join-Path $OutputDirectory '4672-permanent-delete.png'
$script:progressPath = Join-Path $OutputDirectory '4672-file-operation-progress.png'
$script:progressDetailsPath = Join-Path $OutputDirectory '4672-file-operation-progress-details.png'
$demoPath = Join-Path $OutputDirectory '4672-file-operation-demo.png'

Save-ModalDialog -Path $conflictPath -Show {
    $apply = $false
    [void][Krypton.Dialogs.KryptonFileConflictDialog]::Show(
        $null,
        $sourceFile,
        $destFile,
        $null,
        [ref]$apply)
}.GetNewClosure()

Save-ModalDialog -Path $recyclePath -Show {
    $paths = New-Object 'System.Collections.Generic.List[string]'
    [void]$paths.Add($sourceFile)
    [void]$paths.Add($destFile)
    [void][Krypton.Dialogs.KryptonDeleteConfirmationDialog]::Show($null, $paths, $null)
}.GetNewClosure()

Save-ModalDialog -Path $permanentPath -Show {
    $paths = New-Object 'System.Collections.Generic.List[string]'
    [void]$paths.Add($destFile)
    [void][Krypton.Dialogs.KryptonPermanentDeleteDialog]::Show($null, $paths, $null)
}.GetNewClosure()

$large = Join-Path $root 'source\many'
[System.IO.Directory]::CreateDirectory($large) | Out-Null
$payload = New-Object byte[] 4096
for ($i = 0; $i -lt 2500; $i++) {
    [System.IO.File]::WriteAllBytes((Join-Path $large ('f{0:D4}.bin' -f $i)), $payload)
}

$script:progressShot = $false
$script:progressDetailsShot = $false
$script:progressSeen = $null
$script:progressExpandedAt = $null
$script:progressCaptureError = $null
$progressTimer = New-Object System.Windows.Forms.Timer
$progressTimer.Interval = 200
$progressTimer.Add_Tick({
    param($sender, $eventArgs)
    try {
        if ($script:progressDetailsShot) {
            if ($null -ne $sender) {
                $sender.Stop()
            }
            return
        }
        foreach ($candidate in @([System.Windows.Forms.Application]::OpenForms)) {
            if ($null -eq $candidate) {
                continue
            }
            if ($candidate.GetType().Name -ne 'VisualFileOperationProgressForm' -or -not $candidate.Visible) {
                continue
            }
            if ($null -eq $script:progressSeen) {
                $script:progressSeen = [datetime]::UtcNow
                return
            }
            if (-not $script:progressShot) {
                if (([datetime]::UtcNow - $script:progressSeen).TotalMilliseconds -lt 600) {
                    return
                }
                $script:progressShot = $true
                $candidate.TopMost = $true
                Save-UnitTestWindowPng -Form $candidate -Path $script:progressPath -SettleMs 100
                [void]$candidate.GetType().InvokeMember(
                    'SetDetailsExpanded',
                    [System.Reflection.BindingFlags]::InvokeMethod -bor
                    [System.Reflection.BindingFlags]::Instance -bor
                    [System.Reflection.BindingFlags]::NonPublic -bor
                    [System.Reflection.BindingFlags]::Public,
                    $null,
                    $candidate,
                    @($true))
                $script:progressExpandedAt = [datetime]::UtcNow
                return
            }
            if (-not $script:progressDetailsShot) {
                # Wait long enough for a visible left-growing series after expand.
                if ($null -eq $script:progressExpandedAt -or (([datetime]::UtcNow - $script:progressExpandedAt).TotalMilliseconds -lt 2500)) {
                    return
                }
                $script:progressDetailsShot = $true
                $candidate.TopMost = $true
                Save-UnitTestWindowPng -Form $candidate -Path $script:progressDetailsPath -SettleMs 150
                if ($null -ne $sender) {
                    $sender.Stop()
                }
            }
            return
        }
    }
    catch {
        $script:progressCaptureError = $_
        if ($null -ne $sender) {
            $sender.Stop()
        }
    }
})
$progressTimer.Start()
$largeOp = New-Operation
$largeOp.QueueCopy($large, (Join-Path $root 'dest'), 'many-copy')
if (-not $largeOp.Perform()) {
    throw 'Large copy failed.'
}
$progressTimer.Stop()
$progressTimer.Dispose()
$largeOp.Dispose()
if ($null -ne $script:progressCaptureError) {
    throw $script:progressCaptureError
}
if (-not $script:progressShot) {
    throw 'The progress dialog was not shown.'
}
if (-not $script:progressDetailsShot) {
    throw 'The expanded progress dialog was not captured.'
}

$testForm = Join-Path $bin 'TestForm.exe'
if (Test-Path -LiteralPath $testForm) {
    $demoAsm = [System.Reflection.Assembly]::LoadFrom($testForm)
    $demoType = $demoAsm.GetType('TestForm.FileOperationProgressDemo')
    if ($null -eq $demoType) {
        throw 'TestForm.FileOperationProgressDemo was not found.'
    }
    $demo = [System.Activator]::CreateInstance($demoType)
    $demo.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $demo.Location = New-Object System.Drawing.Point 80, 80
    $demo.TopMost = $true
    $demo.Show()
    $demo.Activate()
    [void][UnitTestNative]::SetForegroundWindow($demo.Handle)
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 400
    Save-UnitTestWindowPng -Form $demo -Path $demoPath -SettleMs 150
    $demo.Close()
    $demo.Dispose()
}

[System.IO.Directory]::Delete($root, $true)
Write-Output "Wrote $conflictPath"
Write-Output "Wrote $recyclePath"
Write-Output "Wrote $permanentPath"
Write-Output "Wrote $script:progressPath"
Write-Output "Wrote $script:progressDetailsPath"
Write-Output "Wrote $demoPath"
