@echo off
REM Change to repository root directory (go up two levels from Scripts/WebVew2/)
cd /d "%~dp0..\.."

echo Checking WebView2 SDK version...
echo.

if exist "WebView2SDK\Microsoft.Web.WebView2.Core.dll" (
    echo Current installed version:
    powershell -Command "Get-ItemProperty 'WebView2SDK\Microsoft.Web.WebView2.Core.dll' | Select-Object -ExpandProperty VersionInfo | Select-Object -ExpandProperty FileVersion"
    echo.
    echo Latest available version:
    powershell -ExecutionPolicy Bypass -File "%~dp0Get-LatestWebView2Version.ps1"
) else (
    echo WebView2 SDK is not installed.
    echo.
    echo Latest available version:
    powershell -ExecutionPolicy Bypass -File "%~dp0Get-LatestWebView2Version.ps1"
)

echo.
pause
