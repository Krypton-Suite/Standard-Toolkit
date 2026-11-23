@echo off
REM Report installed WebView2 SDK assemblies under the repo WebView2SDK folder.
setlocal EnableExtensions
cd /d "%~dp0..\.."

echo Checking WebView2 SDK version...
echo.

if not exist "WebView2SDK\" (
    echo WebView2SDK directory not found.
    echo Run Setup-WebView2SDK.cmd first.
    echo.
    exit /b 1
)

set "MISSING=0"
if not exist "WebView2SDK\Microsoft.Web.WebView2.Core.dll" (
    echo Missing: Microsoft.Web.WebView2.Core.dll
    set "MISSING=1"
)
if not exist "WebView2SDK\Microsoft.Web.WebView2.WinForms.dll" (
    echo Missing: Microsoft.Web.WebView2.WinForms.dll
    set "MISSING=1"
)
if not exist "WebView2SDK\WebView2Loader.dll" (
    echo Missing: WebView2Loader.dll
    set "MISSING=1"
)

if "%MISSING%"=="1" (
    echo.
    echo One or more WebView2 SDK assemblies are missing.
    echo Run Setup-WebView2SDK.cmd to install them.
    echo.
    exit /b 1
)

echo Installed WebView2 SDK assemblies:
dir WebView2SDK\*.dll
echo.

echo Microsoft.Web.WebView2.Core.dll file version:
powershell -NoProfile -Command "([System.Diagnostics.FileVersionInfo]::GetVersionInfo((Resolve-Path 'WebView2SDK\Microsoft.Web.WebView2.Core.dll')).FileVersion)"
echo.

echo Microsoft.Web.WebView2.WinForms.dll file version:
powershell -NoProfile -Command "([System.Diagnostics.FileVersionInfo]::GetVersionInfo((Resolve-Path 'WebView2SDK\Microsoft.Web.WebView2.WinForms.dll')).FileVersion)"
echo.

echo WebView2Loader.dll file version:
powershell -NoProfile -Command "([System.Diagnostics.FileVersionInfo]::GetVersionInfo((Resolve-Path 'WebView2SDK\WebView2Loader.dll')).FileVersion)"
echo.

if exist "%~dp0Get-LatestWebView2Version.ps1" (
    echo Latest WebView2 SDK version available from NuGet:
    powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0Get-LatestWebView2Version.ps1"
    echo.
)

exit /b 0
