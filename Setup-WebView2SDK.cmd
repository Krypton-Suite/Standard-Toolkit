@echo off
echo Setting up WebView2 SDK for KryptonWebView2 control...
echo.

REM Create WebView2SDK directory if it doesn't exist
if not exist "WebView2SDK" (
    echo Creating WebView2SDK directory...
    mkdir WebView2SDK
)

echo.
echo IMPORTANT: You need to download the WebView2 SDK assemblies manually.
echo.
echo Please follow these steps:
echo 1. Download the WebView2 SDK from: https://developer.microsoft.com/en-us/microsoft-edge/webview2/
echo 2. Extract the following files to the WebView2SDK folder:
echo    - Microsoft.Web.WebView2.Core.dll
echo    - Microsoft.Web.WebView2.WinForms.dll
echo    - WebView2Loader.dll
echo.
echo Alternative: Use NuGet Package Manager to install Microsoft.Web.WebView2
echo and copy the assemblies from the packages folder to WebView2SDK.
echo.
echo After copying the assemblies, rebuild the solution.
echo.
pause
