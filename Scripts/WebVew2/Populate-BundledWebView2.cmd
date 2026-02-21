@echo off
REM Populate Source\Krypton Components\Krypton.Utilities\Lib\WebView2 with latest WebView2 DLLs from NuGet.
REM Always uses the latest stable Microsoft.Web.WebView2 version. Run from repository root.

setlocal
cd /d "%~dp0..\.."

set "UTIL_PROJ=Source\Krypton Components\Krypton.Utilities\Krypton.Utilities.csproj"
set "LIB_DIR=Source\Krypton Components\Krypton.Utilities\Lib\WebView2"

echo Populating bundled WebView2 DLLs for Krypton.Utilities...
echo.

if not exist "%LIB_DIR%" mkdir "%LIB_DIR%"

REM Get latest stable WebView2 version (fallback only if API fails)
for /f "delims=" %%i in ('powershell -ExecutionPolicy Bypass -File "%~dp0Get-LatestWebView2Version.ps1"') do set "WEBVIEW2_VERSION=%%i"
if "%WEBVIEW2_VERSION%"=="" set "WEBVIEW2_VERSION=1.0.3595.46"
echo Using WebView2 (latest): %WEBVIEW2_VERSION%
echo.

REM Temporarily add package and restore to get DLLs
echo Adding package and restoring...
dotnet add "%UTIL_PROJ%" package Microsoft.Web.WebView2 --version %WEBVIEW2_VERSION%
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to add package
    exit /b 1
)
dotnet restore "%UTIL_PROJ%"
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to restore
    exit /b 1
)

REM Copy DLLs from NuGet cache to Lib\WebView2
set "NUGET_PATH=%USERPROFILE%\.nuget\packages\microsoft.web.webview2\%WEBVIEW2_VERSION%"
echo Copying from %NUGET_PATH% to %LIB_DIR%...

powershell -Command "& { $src = \"$env:USERPROFILE\.nuget\packages\microsoft.web.webview2\%WEBVIEW2_VERSION%\"; $dst = \"%LIB_DIR%\"; $dlls = @('Microsoft.Web.WebView2.Core.dll','Microsoft.Web.WebView2.WinForms.dll','WebView2Loader.dll'); foreach ($d in $dlls) { $f = Get-ChildItem -Path $src -Recurse -Name $d -ErrorAction SilentlyContinue | Select-Object -First 1; if ($f) { Copy-Item (Join-Path $src $f) $dst -Force; Write-Host \"Copied $d\" } else { Write-Warning \"Not found: $d\" } } }"

REM Remove the package reference so the project uses only bundled DLLs
echo Removing PackageReference so project uses bundled DLLs...
dotnet remove "%UTIL_PROJ%" package Microsoft.Web.WebView2

echo.
echo Done. Bundled WebView2 DLLs are in %LIB_DIR%
echo You can commit these DLLs so CI and other developers don't need to run this script.
echo.
endlocal
