@echo off
REM =============================================================================
REM find-msbuild.cmd - Locate MSBuild for a Visual Studio toolset generation
REM =============================================================================
REM
REM PURPOSE
REM   Shared helper for all orchestration scripts under Scripts\Build\,
REM   Scripts\VS2022\, and Scripts\Current\. Resolves the directory that contains
REM   MSBuild.exe and assigns it to the caller's msbuildpath variable.
REM
REM USAGE (must be CALLed, not executed directly)
REM   call "%~dp0find-msbuild.cmd" <profile>
REM
REM PROFILES
REM   2019           Visual Studio 2019 only (Scripts\Build\ legacy toolset)
REM   2022           Visual Studio 2022 only (Scripts\VS2022\)
REM   current        Newest install with MSBuild, major version 18 or later
REM   latest         Alias for current
REM   NN             Pinned major install folder (18, 19, 20, ...) for one yearly
REM                  release; vswhere range is [NN.0,(NN+1).0)
REM
REM DISCOVERY ORDER
REM   1. MSBUILDPATH or MSBUILD_PATH when set (must point at MSBuild\Current\Bin)
REM   2. vswhere.exe under %ProgramFiles(x86)%\Microsoft Visual Studio\Installer\
REM      (finds custom drives and non-default install locations)
REM   3. Standard folder probing under %ProgramFiles% / %ProgramFiles(x86)%
REM
REM EXIT CODES
REM   0  Success; msbuildpath is set in the caller environment
REM   1  Failure; :report_failure prints guidance to stdout
REM
REM BATCH NOTES
REM   - EnableDelayedExpansion is required for !VAR! inside blocks and for /f.
REM   - Success uses "endlocal & set msbuildpath=..." so the path survives CALL.
REM   - 2^>nul on for /f command lines escapes redirection for the child cmd.
REM
REM Related issue: https://github.com/Krypton-Suite/Standard-Toolkit/issues/3788
REM =============================================================================

setlocal EnableExtensions EnableDelayedExpansion

REM -----------------------------------------------------------------------------
REM Argument / override handling
REM -----------------------------------------------------------------------------

set "VS_PROFILE=%~1"
if not defined VS_PROFILE goto :report_failure
if /I "!VS_PROFILE!"=="latest" set "VS_PROFILE=current"

REM Explicit override: honour either env var name used in docs and CI.
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

REM -----------------------------------------------------------------------------
REM Map profile -> vswhere version filter, fallback mode, and display label
REM -----------------------------------------------------------------------------
REM Visual Studio product version mapping (installer major -> marketing name):
REM   16.x = VS 2019    folder: %ProgramFiles(x86)%\...\2019\
REM   17.x = VS 2022    folder: %ProgramFiles%\...\2022\
REM   18.x = VS 2026    folder: %ProgramFiles%\...\18\
REM   19.x = VS 2027    folder: %ProgramFiles%\...\19\  (year = major + 2008)
REM   ... yearly releases continue with numeric major folders under 18+

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
    REM Open upper bound: any major 18+; -latest picks the newest matching install.
    set "VS_VERSION_ARG=-version [18.0,) -latest"
    set "FALLBACK_MODE=current"
    set "VS_PRODUCT_LABEL=Visual Studio (current)"
)

REM Pure numeric profile pins one major folder (18, 19, 20, ...).
if not defined FALLBACK_MODE (
    echo !VS_PROFILE!| findstr /r "^[0-9][0-9]*$" >nul
    if not errorlevel 1 (
        set "VS_MAJOR_FOLDER=!VS_PROFILE!"
        set "FALLBACK_MODE=major"
        set "VS_PRODUCT_LABEL=Visual Studio !VS_PROFILE!"
    )
)

if not defined FALLBACK_MODE goto :report_failure

REM Build vswhere range [N.0,(N+1).0) for pinned major profiles.
if "!FALLBACK_MODE!"=="major" (
    set /a "VS_MAJOR_NEXT=!VS_MAJOR_FOLDER!+1"
    set "VS_VERSION_ARG=-version [!VS_MAJOR_FOLDER!.0,!VS_MAJOR_NEXT!.0) -latest"
)

REM -----------------------------------------------------------------------------
REM Primary discovery: vswhere.exe
REM -----------------------------------------------------------------------------
REM The VS Installer registers every install path; vswhere reads that database so
REM builds work when Visual Studio is not under the default Program Files tree.

set "VSWHERE=%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"
if exist "%VSWHERE%" if defined VS_VERSION_ARG (
    for /f "usebackq tokens=* delims=" %%M in (`"!VSWHERE!" !VS_VERSION_ARG! -products * -requires Microsoft.Component.MSBuild -find MSBuild\Current\Bin\MSBuild.exe 2^>nul`) do (
        REM %%~dpM is the Bin directory; may include a trailing backslash.
        set "FOUND_MSBUILD_BIN=%%~dpM"
        set "FOUND_VIA=vswhere"
        goto :report_success
    )
)

REM -----------------------------------------------------------------------------
REM Fallback discovery: probe well-known install roots on the system drive
REM -----------------------------------------------------------------------------
REM Edition order prefers preview/insiders channels, then paid SKUs, then Build Tools.

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
REM Scan numeric majors 99..18 so future yearly folders (19, 20, ...) need no script change.
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

REM -----------------------------------------------------------------------------
REM Success reporting and return to caller
REM -----------------------------------------------------------------------------

:report_success
if "!FOUND_MSBUILD_BIN:~-1!"=="\" set "FOUND_MSBUILD_BIN=!FOUND_MSBUILD_BIN:~0,-1!"

REM Derive edition from path when vswhere displayName is unavailable.
set "VS_EDITION="
for %%E in (Insiders Preview Enterprise Professional Community BuildTools) do (
    if not "!FOUND_MSBUILD_BIN:\%%E\MSBuild=!"=="!FOUND_MSBUILD_BIN!" set "VS_EDITION=%%E"
)

REM Prefer marketing displayName from vswhere (accurate for any install location).
set "VS_DISPLAY_NAME="
if /I "!FOUND_VIA!"=="vswhere" if exist "!VSWHERE!" if defined VS_VERSION_ARG (
    for /f "usebackq tokens=* delims=" %%D in (`"!VSWHERE!" !VS_VERSION_ARG! -products * -requires Microsoft.Component.MSBuild -property displayName 2^>nul`) do (
        if not defined VS_DISPLAY_NAME set "VS_DISPLAY_NAME=%%D"
    )
)

REM MSBuild tool version (e.g. 18.7.8.x for VS 2026 toolset).
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

REM endlocal restores caller setlocal state but "set msbuildpath=..." runs in caller scope.
for %%P in ("!FOUND_MSBUILD_BIN!") do endlocal & set "msbuildpath=%%~P" & exit /b 0

REM -----------------------------------------------------------------------------
REM Failure reporting
REM -----------------------------------------------------------------------------
REM Messages are centralized here so individual build scripts only pause on error.

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
    REM Marketing year for major >= 18: 18+2008=2026, 19+2008=2027, ...
    set /a "VS_FAIL_YEAR=2008+!VS_MAJOR_FOLDER!"
    echo Check if Visual Studio !VS_FAIL_YEAR! ^(major !VS_MAJOR_FOLDER!^) with MSBuild is installed, or set MSBUILDPATH / MSBUILD_PATH.
) else (
    echo Check that a supported Visual Studio install with MSBuild is available, or set MSBUILDPATH / MSBUILD_PATH.
)
echo(
exit /b 1
