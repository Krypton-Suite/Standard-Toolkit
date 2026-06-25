@echo off
setlocal EnableExtensions EnableDelayedExpansion
REM Locate MSBuild for a Visual Studio generation.
REM Usage: call "%~dp0find-msbuild.cmd" <2019|2022|18>
REM Sets msbuildpath (directory containing MSBuild.exe). Exit 0 on success, 1 on failure.
REM Override: set MSBUILDPATH or MSBUILD_PATH to the MSBuild\Current\Bin directory.

set "VS_PROFILE=%~1"
if not defined VS_PROFILE exit /b 1

if defined MSBUILDPATH (
    if exist "!MSBUILDPATH!\MSBuild.exe" (
        set "FOUND_MSBUILD_BIN=!MSBUILDPATH!"
        set "FOUND_VIA=override"
        goto :report_success
    )
)
if defined MSBUILD_PATH (
    if exist "!MSBUILD_PATH!\MSBuild.exe" (
        set "FOUND_MSBUILD_BIN=!MSBUILD_PATH!"
        set "FOUND_VIA=override"
        goto :report_success
    )
)

set "VSWHERE=%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"
if exist "%VSWHERE%" (
    set "VS_VERSION_ARG="
    if /I "!VS_PROFILE!"=="2019" set "VS_VERSION_ARG=-version [16.0,17.0)"
    if /I "!VS_PROFILE!"=="2022" set "VS_VERSION_ARG=-version [17.0,18.0)"
    if /I "!VS_PROFILE!"=="18" set "VS_VERSION_ARG=-version [18.0,19.0)"
    if defined VS_VERSION_ARG (
        for /f "usebackq tokens=* delims=" %%M in (`"!VSWHERE!" !VS_VERSION_ARG! -products * -requires Microsoft.Component.MSBuild -find MSBuild\Current\Bin\MSBuild.exe 2^>nul`) do (
            set "FOUND_MSBUILD_BIN=%%~dpM"
            set "FOUND_VIA=vswhere"
            goto :report_success
        )
    )
)

if /I "!VS_PROFILE!"=="2019" goto :fallback_2019
if /I "!VS_PROFILE!"=="2022" goto :fallback_2022
if /I "!VS_PROFILE!"=="18" goto :fallback_18
exit /b 1

:fallback_2019
set "VS_ROOT=%ProgramFiles(x86)%\Microsoft Visual Studio\2019"
for %%E in (Insiders Preview Enterprise Professional Community BuildTools) do (
    if exist "!VS_ROOT!\%%E\MSBuild\Current\Bin\MSBuild.exe" (
        set "FOUND_MSBUILD_BIN=!VS_ROOT!\%%E\MSBuild\Current\Bin"
        set "FOUND_VIA=fallback"
        goto :report_success
    )
)
exit /b 1

:fallback_2022
set "VS_ROOT=%ProgramFiles%\Microsoft Visual Studio\2022"
for %%E in (Preview Enterprise Professional Community BuildTools) do (
    if exist "!VS_ROOT!\%%E\MSBuild\Current\Bin\MSBuild.exe" (
        set "FOUND_MSBUILD_BIN=!VS_ROOT!\%%E\MSBuild\Current\Bin"
        set "FOUND_VIA=fallback"
        goto :report_success
    )
)
exit /b 1

:fallback_18
set "VS_ROOT=%ProgramFiles%\Microsoft Visual Studio\18"
for %%E in (Insiders Enterprise Professional Community BuildTools) do (
    if exist "!VS_ROOT!\%%E\MSBuild\Current\Bin\MSBuild.exe" (
        set "FOUND_MSBUILD_BIN=!VS_ROOT!\%%E\MSBuild\Current\Bin"
        set "FOUND_VIA=fallback"
        goto :report_success
    )
)
exit /b 1

:report_success
if "!FOUND_MSBUILD_BIN:~-1!"=="\" set "FOUND_MSBUILD_BIN=!FOUND_MSBUILD_BIN:~0,-1!"

set "VS_PRODUCT_LABEL="
if /I "!VS_PROFILE!"=="2019" set "VS_PRODUCT_LABEL=Visual Studio 2019"
if /I "!VS_PROFILE!"=="2022" set "VS_PRODUCT_LABEL=Visual Studio 2022"
if /I "!VS_PROFILE!"=="18" set "VS_PRODUCT_LABEL=Visual Studio 2026"

set "VS_EDITION="
for %%E in (Insiders Preview Enterprise Professional Community BuildTools) do (
    if not "!FOUND_MSBUILD_BIN:\%%E\MSBuild=!"=="!FOUND_MSBUILD_BIN!" set "VS_EDITION=%%E"
)

set "VS_DISPLAY_NAME="
if /I "!FOUND_VIA!"=="vswhere" if exist "!VSWHERE!" if defined VS_VERSION_ARG (
    for /f "usebackq tokens=* delims=" %%D in (`"!VSWHERE!" !VS_VERSION_ARG! -products * -requires Microsoft.Component.MSBuild -property displayName 2^>nul`) do (
        if not defined VS_DISPLAY_NAME set "VS_DISPLAY_NAME=%%D"
    )
)

set "MSBUILD_TOOL_VERSION=unknown"
for /f "usebackq tokens=* delims=" %%V in (`"!FOUND_MSBUILD_BIN!\MSBuild.exe" -version -nologo 2^>nul`) do set "MSBUILD_TOOL_VERSION=%%V"

echo(
echo Using build tools:
if defined VS_DISPLAY_NAME (
    echo   Visual Studio: !VS_DISPLAY_NAME!
) else if defined VS_EDITION (
    echo   Visual Studio: !VS_PRODUCT_LABEL! !VS_EDITION!
) else (
    echo   Visual Studio: !VS_PRODUCT_LABEL!
)
echo   MSBuild path: !FOUND_MSBUILD_BIN!
echo   MSBuild version: !MSBUILD_TOOL_VERSION!
echo(

for %%P in ("!FOUND_MSBUILD_BIN!") do endlocal & set "msbuildpath=%%~P" & exit /b 0
