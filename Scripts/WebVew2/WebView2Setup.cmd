:: 

@echo off

cls

goto webview2menu

:webview2menu

cls

echo WebView2 SDK Tools
echo.
echo 1. Setup WebView2 SDK
echo 2. Update WebView2 SDK
echo 3. Check WebView2 Version
echo 4. Go back to main menu
echo:
set /p answer="Enter number (1 - 4): "
if %answer%==1 (goto setupwebview2sdk)
if %answer%==2 (goto updatewebview2sdk)
if %answer%==3 (goto checkwebview2version)
if %answer%==4 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto webview2tmenu

:: ===================================================================================================

:setupwebview2sdk
cls

echo Setting up WebView2 SDK for KryptonWebView2 control...
echo This will install the latest stable WebView2 SDK version.
echo.

Setup-WebView2SDK.cmd

cd ..

pause

goto webview2menu

:: ===================================================================================================

:updatewebview2sdk
cls

echo Updating WebView2 SDK to latest version...
echo This will check for updates and install the newest stable version.
echo.

Update-WebView2SDK.cmd

cd ..

pause

goto webview2menu

:: ===================================================================================================

:checkwebview2version
cls

echo Checking WebView2 SDK version...
echo.

Check-WebView2Version.cmd

cd ..

pause

goto webview2menu

:: ===================================================================================================

:mainmenu

cls

cd ..

main-menu.cmd