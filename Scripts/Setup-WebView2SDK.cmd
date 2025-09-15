@echo off
echo Setting up WebView2 SDK for KryptonWebView2 control...
echo.

REM Check if WebView2SDK directory exists and has the required files
if exist "WebView2SDK\Microsoft.Web.WebView2.Core.dll" (
    if exist "WebView2SDK\Microsoft.Web.WebView2.WinForms.dll" (
        if exist "WebView2SDK\WebView2Loader.dll" (
            echo WebView2 SDK assemblies are already present!
            echo.
            echo Files found:
            dir WebView2SDK\*.dll
            echo.
            echo The KryptonWebView2 control should compile successfully.
            echo You can now build the solution and use the control.
            echo.
            pause
            exit /b 0
        )
    )
)

REM Create WebView2SDK directory if it doesn't exist
if not exist "WebView2SDK" (
    echo Creating WebView2SDK directory...
    mkdir WebView2SDK
)

echo.
echo WebView2 SDK assemblies are missing. Setting up automatically...
echo.

REM Try to download and setup WebView2 SDK automatically
echo Attempting to install WebView2 SDK via NuGet...
dotnet add "Source/Krypton Components/Krypton.Toolkit/Krypton.Toolkit 2022.csproj" package Microsoft.Web.WebView2 --version 1.0.2478.35

if %ERRORLEVEL% EQU 0 (
    echo.
    echo Copying WebView2 assemblies to WebView2SDK folder...
    
    REM Copy the assemblies from NuGet cache
    copy "%USERPROFILE%\.nuget\packages\microsoft.web.webview2\1.0.2478.35\lib\net45\Microsoft.Web.WebView2.Core.dll" "WebView2SDK\" >nul 2>&1
    copy "%USERPROFILE%\.nuget\packages\microsoft.web.webview2\1.0.2478.35\lib\net45\Microsoft.Web.WebView2.WinForms.dll" "WebView2SDK\" >nul 2>&1
    copy "%USERPROFILE%\.nuget\packages\microsoft.web.webview2\1.0.2478.35\runtimes\win-x64\native\WebView2Loader.dll" "WebView2SDK\" >nul 2>&1
    
    REM Remove the NuGet package reference since we're using local assemblies
    dotnet remove "Source/Krypton Components/Krypton.Toolkit/Krypton.Toolkit 2022.csproj" package Microsoft.Web.WebView2
    
    echo.
    echo WebView2 SDK setup completed successfully!
    echo.
    echo Files copied:
    dir WebView2SDK\*.dll
    echo.
    echo You can now build the solution and use the KryptonWebView2 control.
) else (
    echo.
    echo Automatic setup failed. Please follow manual setup instructions:
    echo.
    echo MANUAL SETUP INSTRUCTIONS:
    echo 1. Download the WebView2 SDK from: https://developer.microsoft.com/en-us/microsoft-edge/webview2/
    echo 2. Extract the following files to the WebView2SDK folder:
    echo    - Microsoft.Web.WebView2.Core.dll
    echo    - Microsoft.Web.WebView2.WinForms.dll
    echo    - WebView2Loader.dll
    echo.
    echo Alternative: Use NuGet Package Manager to install Microsoft.Web.WebView2
    echo and copy the assemblies from the packages folder to WebView2SDK.
)

echo.
pause
