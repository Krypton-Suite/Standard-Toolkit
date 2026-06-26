@echo off
REM Stable release build using the Scripts/VS2022/ toolset (Visual Studio 2022, profile "2022").
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

@echo Started to build Stable release
@echo:
@echo Started: %date% %time% %zone%
@echo:
set "targets=Build"
if not "%~1" == "" set "targets=%~1"
REM Phased Krypton.* build + /m (see Scripts\Build\Krypton.Orchestration.targets).
"%msbuildpath%\msbuild.exe" /m /t:%targets% "%SCRIPT_DIR%build.proj" /fl /flp:logfile="%SCRIPT_DIR%..\..\Logs\stable-build-log.log" /bl:"%SCRIPT_DIR%..\..\Logs\stable-build-log.binlog" /clp:Summary;ShowTimestamp /v:quiet
@echo:
@echo Stable release build completed: %date% %time% %zone%
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