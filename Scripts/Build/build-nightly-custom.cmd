@echo off
REM Custom nightly build using the Scripts/Build/ toolset (Visual Studio 2019, profile "2019").
REM MSBuild discovery: Scripts\Common\find-msbuild.cmd. Failure text comes from the helper; this script only pauses.
setlocal EnableExtensions
set "SCRIPT_DIR=%~dp0"
pushd "%SCRIPT_DIR%"

call "%SCRIPT_DIR%..\Common\find-msbuild.cmd" 2019
if errorlevel 1 (
echo.
pause
goto exitbatch
)
goto build

:build
for /f "tokens=* usebackq" %%A in (`tzutil /g`) do (
    set "zone=%%A"
)

@echo Started: %date% %time% %zone%
@echo
set "targets=Build"
if not "%~1" == "" set "targets=%~1"
"%msbuildpath%\msbuild.exe" /m /t:%targets% "%SCRIPT_DIR%nightly.proj" /fl /flp:logfile="%SCRIPT_DIR%..\..\build.log"

@echo Build Completed: %date% %time% %zone%

:exitbatch
popd
"%SCRIPT_DIR%..\main-menu.cmd"
exit /b