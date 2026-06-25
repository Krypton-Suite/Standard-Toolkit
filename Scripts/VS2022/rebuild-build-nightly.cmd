@echo off
REM Rebuild then build nightly using the Scripts/VS2022/ toolset (Visual Studio 2022, profile "2022").
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

@echo Rebuild Started: %date% %time% %zone%
@echo
set "targets=Rebuild"
if not "%~1" == "" set "targets=%~1"
"%msbuildpath%\msbuild.exe" /m -t:%targets% "%SCRIPT_DIR%nightly.proj" /fl /flp:logfile="%SCRIPT_DIR%..\..\Logs\nightly-build-log.log" /bl:"%SCRIPT_DIR%..\..\Logs\nightly-build-log.binlog"  /clp:Summary;ShowTimestamp /v:quiet

:: -t:rebuild

@echo Build Completed: %date% %time% %zone%

pause

@echo Do you want to return to complete another task? (Y/N)
set /p answer="Enter input: "
if %answer%==Y (goto run)
if %answer%==y (goto run)
if %answer%==N exit
if %answer%==n exit

@echo Invalid input, please try again.

:run
popd

"%SCRIPT_DIR%..\..\run.cmd"
exit /b

:exitbatch
popd
exit /b