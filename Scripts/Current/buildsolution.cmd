@echo off
setlocal EnableExtensions
set "SCRIPT_DIR=%~dp0"
pushd "%SCRIPT_DIR%"

call "%SCRIPT_DIR%..\Common\find-msbuild.cmd" current
if errorlevel 1 (
pause
goto exitbatch
)
goto build

:build
@echo Started: %date% %time%
@echo:
set "targets=Build"
if not "%~1" == "" set "targets=%~1"
REM /m: multi-processor MSBuild (all logical CPUs).
setlocal
call "%SCRIPT_DIR%setup-dotnet11-sdk.cmd" || (endlocal & goto exitbatch)
"%msbuildpath%\msbuild.exe" /m /t:%targets% "%SCRIPT_DIR%build.proj" /fl /flp:logfile="%SCRIPT_DIR%..\..\Logs\solution-build-log.log" /bl:"%SCRIPT_DIR%..\..\Logs\solution-build-log.binlog" /clp:Summary;ShowTimestamp /v:quiet
endlocal

@echo Build Completed: %date% %time%
@echo:
pause

:exitbatch
popd
exit /b
