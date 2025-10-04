@echo off
echo Updating to latest WebView2 SDK version...
echo.

REM Check if WebView2SDK directory exists
if not exist "WebView2SDK" (
    echo WebView2SDK directory not found. Please run Setup-WebView2SDK.cmd first.
    echo.
    pause
    exit /b 1
)

REM Get the latest WebView2 SDK version
echo Detecting latest WebView2 SDK version...
powershell -ExecutionPolicy Bypass -File "%~dp0Get-LatestWebView2Version.ps1" > temp_version.txt 2>&1
set /p WEBVIEW2_VERSION=<temp_version.txt
del temp_version.txt

if "%WEBVIEW2_VERSION%"=="" (
    echo Failed to detect latest version, using fallback version 1.0.2478.35
    set WEBVIEW2_VERSION=1.0.2478.35
) else (
    echo Using WebView2 SDK version: %WEBVIEW2_VERSION%
)

echo.
echo Current WebView2 SDK version: 
if exist "WebView2SDK\Microsoft.Web.WebView2.Core.dll" (
    powershell -Command "Get-ItemProperty 'WebView2SDK\Microsoft.Web.WebView2.Core.dll' | Select-Object -ExpandProperty VersionInfo | Select-Object -ExpandProperty FileVersion"
) else (
    echo Not installed
)
echo.
echo Updating WebView2 SDK to version %WEBVIEW2_VERSION%...

REM Temporarily add the latest version via NuGet
dotnet add "Source/Krypton Components/Krypton.Toolkit/Krypton.Toolkit 2022.csproj" package Microsoft.Web.WebView2 --version %WEBVIEW2_VERSION%

if %ERRORLEVEL% EQU 0 (
    echo.
    echo Copying updated WebView2 assemblies...
    
    REM Copy the assemblies from NuGet cache
    echo Searching for WebView2 assemblies in NuGet cache...
    set "NUGET_PATH=%USERPROFILE%\.nuget\packages\microsoft.web.webview2\%WEBVIEW2_VERSION%"

    REM Find and copy Core DLL
    for /f "delims=" %%i in ('powershell -Command "Get-ChildItem -Path '%NUGET_PATH%' -Recurse -Name 'Microsoft.Web.WebView2.Core.dll' | Select-Object -First 1"') do (
        set "CORE_DLL=%%i"
    )
    if defined CORE_DLL (
        copy "%NUGET_PATH%\%CORE_DLL%" "WebView2SDK\" >nul 2>&1
        echo Copied Microsoft.Web.WebView2.Core.dll
    ) else (
        echo ERROR: Microsoft.Web.WebView2.Core.dll not found in package
        exit /b 1
    )

    REM Find and copy WinForms DLL
    for /f "delims=" %%i in ('powershell -Command "Get-ChildItem -Path '%NUGET_PATH%' -Recurse -Name 'Microsoft.Web.WebView2.WinForms.dll' | Select-Object -First 1"') do (
        set "WINFORMS_DLL=%%i"
    )
    if defined WINFORMS_DLL (
        copy "%NUGET_PATH%\%WINFORMS_DLL%" "WebView2SDK\" >nul 2>&1
        echo Copied Microsoft.Web.WebView2.WinForms.dll
    ) else (
        echo ERROR: Microsoft.Web.WebView2.WinForms.dll not found in package
        exit /b 1
    )

    REM Find and copy WebView2Loader DLL
    for /f "delims=" %%i in ('powershell -Command "Get-ChildItem -Path '%NUGET_PATH%' -Recurse -Name 'WebView2Loader.dll' | Select-Object -First 1"') do (
        set "LOADER_DLL=%%i"
    )
    if defined LOADER_DLL (
        copy "%NUGET_PATH%\%LOADER_DLL%" "WebView2SDK\" >nul 2>&1
        echo Copied WebView2Loader.dll
    ) else (
        echo ERROR: WebView2Loader.dll not found in package
        exit /b 1
    )
    
    REM Remove the NuGet package reference
    dotnet remove "Source/Krypton Components/Krypton.Toolkit/Krypton.Toolkit 2022.csproj" package Microsoft.Web.WebView2
    
    REM Update project file with the latest version
    echo Updating project file with latest version...
    powershell -ExecutionPolicy Bypass -File "%~dp0Update-WebView2ProjectVersion.ps1"
    
    echo.
    echo WebView2 SDK updated successfully!
    echo.
    echo Installed version: %WEBVIEW2_VERSION%
    echo Updated files:
    dir WebView2SDK\*.dll
    echo.
    echo You may need to rebuild the solution for changes to take effect.
) else (
    echo.
    echo Failed to update WebView2 SDK. Please check your internet connection and try again.
)

echo.
pause
