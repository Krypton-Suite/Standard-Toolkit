@echo off
REM Installer package build using the Scripts/VS2022/ toolset (Visual Studio 2022, profile "2022").
REM MSBuild discovery: Scripts\Common\find-msbuild.cmd. Failure text comes from the helper; this script only pauses.
setlocal EnableExtensions
set "SCRIPT_DIR=%~dp0"
pushd "%SCRIPT_DIR%"

call "%SCRIPT_DIR%..\Common\find-msbuild.cmd" 2022
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
REM /m: multi-processor MSBuild (all logical CPUs).
"%msbuildpath%\msbuild.exe" /m /t:%targets% "%SCRIPT_DIR%..\Project Files\installer.proj" /fl /flp:logfile="%SCRIPT_DIR%..\..\Logs\installer-build-log.log" /bl:"%SCRIPT_DIR%..\..\Logs\installer-build-log.binlog" /clp:Summary;ShowTimestamp /v:quiet

@echo Build Completed: %date% %time% %zone%

pause

:prompt
@echo Do you want to return to complete another task? (Y/N)
set /p answer="Enter input: "
if /i "%answer%"=="Y" goto run
if /i "%answer%"=="N" goto exitbatch
@echo Invalid input, please try again.
goto prompt

:run
popd
"%SCRIPT_DIR%..\main-menu.cmd"
exit /b

:exitbatch
popd
exit /b