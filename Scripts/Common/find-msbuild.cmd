@echo off
REM =============================================================================
REM find-msbuild.cmd - Locate MSBuild for a Visual Studio toolset generation
REM =============================================================================
REM Usage: call "%~dp0find-msbuild.cmd" <2019|2022|current|latest|NN>
REM   2019 / 2022     - pinned legacy generations
REM   current / latest - newest install with MSBuild, major version 18 or later
REM   NN              - pinned major folder (18, 19, 20, ...) for one yearly release
REM Sets msbuildpath (MSBuild\Current\Bin). Exit 0 on success, 1 on failure.
REM Override: MSBUILDPATH or MSBUILD_PATH -> MSBuild\Current\Bin directory.
REM Issue: https://github.com/Krypton-Suite/Standard-Toolkit/issues/3788

setlocal EnableExtensions EnableDelayedExpansion

set "VS_PROFILE=%~1"
if not defined VS_PROFILE goto :report_failure
if /I "!VS_PROFILE!"=="latest" set "VS_PROFILE=current"

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

set "VS_VERSION_ARG="
set "FALLBACK_MODE="
set "VS_PRODUCT_LABEL="
set "VS_MAJOR_FOLDER="

if /I "!VS_PROFILE!"=="2019" (
    set "VS_VERSION_ARG=-version [16.0,17.0)"
    set "FALLBACK_MODE=2019"
    set "VS_PRODUCT_LABEL=Visual Studio 2019"
)
if /I "!VS_PROFILE!"=="2022" (
    set "VS_VERSION_ARG=-version [17.0,18.0)"
    set "FALLBACK_MODE=2022"
    set "VS_PRODUCT_LABEL=Visual Studio 2022"
)
if /I "!VS_PROFILE!"=="current" (
    set "VS_VERSION_ARG=-version [18.0,) -latest"
    set "FALLBACK_MODE=current"
    set "VS_PRODUCT_LABEL=Visual Studio (current)"
)

if not defined FALLBACK_MODE (
    echo !VS_PROFILE!| findstr /r "^[0-9][0-9]*$" >nul
    if not errorlevel 1 (
        set "VS_MAJOR_FOLDER=!VS_PROFILE!"
        set "FALLBACK_MODE=major"
        set "VS_PRODUCT_LABEL=Visual Studio !VS_PROFILE!"
    )
)

if not defined FALLBACK_MODE goto :report_failure

if "!FALLBACK_MODE!"=="major" (
    set /a "VS_MAJOR_NEXT=!VS_MAJOR_FOLDER!+1"
    set "VS_VERSION_ARG=-version [!VS_MAJOR_FOLDER!.0,!VS_MAJOR_NEXT!.0) -latest"
)

set "VSWHERE=%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"
if exist "%VSWHERE%" if defined VS_VERSION_ARG (
    for /f "usebackq tokens=* delims=" %%M in (`"!VSWHERE!" !VS_VERSION_ARG! -products * -requires Microsoft.Component.MSBuild -find MSBuild\Current\Bin\MSBuild.exe 2^>nul`) do (
        set "FOUND_MSBUILD_BIN=%%~dpM"
        set "FOUND_VIA=vswhere"
        goto :report_success
    )
)

if "!FALLBACK_MODE!"=="2019" goto :fallback_2019
if "!FALLBACK_MODE!"=="2022" goto :fallback_2022
if "!FALLBACK_MODE!"=="current" goto :fallback_current
if "!FALLBACK_MODE!"=="major" goto :fallback_major
goto :report_failure

:fallback_2019
set "VS_ROOT=%ProgramFiles(x86)%\Microsoft Visual Studio\2019"
for %%E in (Insiders Preview Enterprise Professional Community BuildTools) do (
    if exist "!VS_ROOT!\%%E\MSBuild\Current\Bin\MSBuild.exe" (
        set "FOUND_MSBUILD_BIN=!VS_ROOT!\%%E\MSBuild\Current\Bin"
        set "FOUND_VIA=fallback"
        goto :report_success
    )
)
goto :report_failure

:fallback_2022
set "VS_ROOT=%ProgramFiles%\Microsoft Visual Studio\2022"
for %%E in (Preview Enterprise Professional Community BuildTools) do (
    if exist "!VS_ROOT!\%%E\MSBuild\Current\Bin\MSBuild.exe" (
        set "FOUND_MSBUILD_BIN=!VS_ROOT!\%%E\MSBuild\Current\Bin"
        set "FOUND_VIA=fallback"
        goto :report_success
    )
)
goto :report_failure

:fallback_current
for /L %%N in (99,-1,18) do (
    set "VS_ROOT=%ProgramFiles%\Microsoft Visual Studio\%%N"
    for %%E in (Insiders Enterprise Professional Community BuildTools Preview) do (
        if exist "!VS_ROOT!\%%E\MSBuild\Current\Bin\MSBuild.exe" (
            set "FOUND_MSBUILD_BIN=!VS_ROOT!\%%E\MSBuild\Current\Bin"
            set "FOUND_VIA=fallback"
            goto :report_success
        )
    )
)
goto :report_failure

:fallback_major
set "VS_ROOT=%ProgramFiles%\Microsoft Visual Studio\!VS_MAJOR_FOLDER!"
for %%E in (Insiders Enterprise Professional Community BuildTools Preview) do (
    if exist "!VS_ROOT!\%%E\MSBuild\Current\Bin\MSBuild.exe" (
        set "FOUND_MSBUILD_BIN=!VS_ROOT!\%%E\MSBuild\Current\Bin"
        set "FOUND_VIA=fallback"
        goto :report_success
    )
)
goto :report_failure

:report_success
if "!FOUND_MSBUILD_BIN:~-1!"=="\" set "FOUND_MSBUILD_BIN=!FOUND_MSBUILD_BIN:~0,-1!"

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

:report_failure
echo(
echo Unable to detect suitable environment.
if /I "!VS_PROFILE!"=="2019" (
    echo Check if Visual Studio 2019 with MSBuild is installed, or set MSBUILDPATH / MSBUILD_PATH.
) else if /I "!VS_PROFILE!"=="2022" (
    echo Check if Visual Studio 2022 with MSBuild is installed, or set MSBUILDPATH / MSBUILD_PATH.
) else if /I "!VS_PROFILE!"=="current" (
    echo Check if Visual Studio 2026 or later with MSBuild is installed, or set MSBUILDPATH / MSBUILD_PATH.
) else if defined VS_MAJOR_FOLDER (
    set /a "VS_FAIL_YEAR=2008+!VS_MAJOR_FOLDER!"
    echo Check if Visual Studio !VS_FAIL_YEAR! ^(major !VS_MAJOR_FOLDER!^) with MSBuild is installed, or set MSBUILDPATH / MSBUILD_PATH.
) else (
    echo Check that a supported Visual Studio install with MSBuild is available, or set MSBUILDPATH / MSBUILD_PATH.
)
echo(
exit /b 1
