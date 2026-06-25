@echo off
REM Nightly release build using the Scripts/Build/ toolset (Visual Studio 2019, profile "2019").
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

REM Phased Krypton.* build + /m (see Scripts\Build\Krypton.Orchestration.targets).
@echo Started to build Nightly release
@echo:
@echo Started: %date% %time% %zone%
@echo:
set "targets=Build"
if not "%~1" == "" set "targets=%~1"
"%msbuildpath%\msbuild.exe" /m -t:%targets% "%SCRIPT_DIR%nightly.proj" /fl /flp:logfile="%SCRIPT_DIR%..\..\Logs\nightly-build-log.log" /bl:"%SCRIPT_DIR%..\..\Logs\nightly-build-log.binlog"  /clp:Summary;ShowTimestamp /v:quiet
@echo:
:: -t:rebuild

::-graphBuild:True

@echo Nightly release build completed: %date% %time% %zone%
@echo:
@echo You can find the build Logs in ..\..\Logs
@echo:
pause

:prompt
@echo Do you want to return to complete another task? (Y/N)
@echo:
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