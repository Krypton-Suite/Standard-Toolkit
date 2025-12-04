@echo off
REM Change to repository root directory (go up two levels from Scripts/WebVew2/)
cd /d "%~dp0..\.."

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

REM Get the latest WebView2 SDK version
echo Detecting latest WebView2 SDK version...
for /f "delims=" %%i in ('powershell -ExecutionPolicy Bypass -File "%~dp0Get-LatestWebView2Version.ps1"') do (
    set "WEBVIEW2_VERSION=%%i"
)

if "%WEBVIEW2_VERSION%"=="" (
    echo Failed to detect latest version, using fallback version 1.0.2478.35
    set WEBVIEW2_VERSION=1.0.2478.35
) else (
    echo Using WebView2 SDK version: %WEBVIEW2_VERSION%
)

REM Try to download and setup WebView2 SDK automatically
echo Attempting to install WebView2 SDK via NuGet...
dotnet add "Source/Krypton Components/Krypton.Toolkit/Krypton.Toolkit 2022.csproj" package Microsoft.Web.WebView2 --version %WEBVIEW2_VERSION%

if %ERRORLEVEL% EQU 0 (
    echo.
    echo Restoring NuGet packages to ensure WebView2 SDK is downloaded...
    dotnet restore "Source/Krypton Components/Krypton.Toolkit/Krypton.Toolkit 2022.csproj"
    
    if %ERRORLEVEL% NEQ 0 (
        echo ERROR: Failed to restore NuGet packages
        exit /b 1
    )
    echo.
    echo Copying WebView2 assemblies to WebView2SDK folder...
    
REM Copy the assemblies from NuGet cache
echo Searching for WebView2 assemblies in NuGet cache...
echo USERPROFILE: %USERPROFILE%
echo WEBVIEW2_VERSION: %WEBVIEW2_VERSION%

REM Use PowerShell to find and copy the assemblies
powershell -Command "& { $nugetPath = \"$env:USERPROFILE\.nuget\packages\microsoft.web.webview2\%WEBVIEW2_VERSION%\"; Write-Host \"NuGet path: $nugetPath\"; if (Test-Path $nugetPath) { Write-Host \"Package directory found\"; $coreDll = Get-ChildItem -Path $nugetPath -Recurse -Name 'Microsoft.Web.WebView2.Core.dll' | Select-Object -First 1; if ($coreDll) { $corePath = Join-Path $nugetPath $coreDll; Copy-Item $corePath 'WebView2SDK\'; Write-Host \"Copied Microsoft.Web.WebView2.Core.dll\" } else { Write-Error \"Core DLL not found\" }; $winFormsDll = Get-ChildItem -Path $nugetPath -Recurse -Name 'Microsoft.Web.WebView2.WinForms.dll' | Select-Object -First 1; if ($winFormsDll) { $winFormsPath = Join-Path $nugetPath $winFormsDll; Copy-Item $winFormsPath 'WebView2SDK\'; Write-Host \"Copied Microsoft.Web.WebView2.WinForms.dll\" } else { Write-Error \"WinForms DLL not found\" }; $loaderDll = Get-ChildItem -Path $nugetPath -Recurse -Name 'WebView2Loader.dll' | Select-Object -First 1; if ($loaderDll) { $loaderPath = Join-Path $nugetPath $loaderDll; Copy-Item $loaderPath 'WebView2SDK\'; Write-Host \"Copied WebView2Loader.dll\" } else { Write-Error \"Loader DLL not found\" } } else { Write-Error \"Package directory not found: $nugetPath\" } }"
    
    REM Remove the NuGet package reference since we're using local assemblies
    dotnet remove "Source/Krypton Components/Krypton.Toolkit/Krypton.Toolkit 2022.csproj" package Microsoft.Web.WebView2
    
    REM Update project file with the latest version
    echo Updating project file with latest version...
    powershell -ExecutionPolicy Bypass -File "%~dp0Update-WebView2ProjectVersion.ps1"
    
    echo.
    echo WebView2 SDK setup completed successfully!
    echo.
    echo Installed version: %WEBVIEW2_VERSION%
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
