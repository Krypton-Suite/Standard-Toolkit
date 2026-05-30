@echo off
setlocal EnableExtensions
set "SCRIPT_DIR=%~dp0"
pushd "%SCRIPT_DIR%"

if exist "%ProgramFiles%\Microsoft Visual Studio\18\Insiders\MSBuild\Current\Bin" goto vscurrentinsiders
if exist "%ProgramFiles%\Microsoft Visual Studio\18\Enterprise\MSBuild\Current\Bin" goto vscurrentent
if exist "%ProgramFiles%\Microsoft Visual Studio\18\Professional\MSBuild\Current\Bin" goto vscurrentpro
if exist "%ProgramFiles%\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin" goto vscurrentcom
if exist "%ProgramFiles%\Microsoft Visual Studio\18\BuildTools\MSBuild\Current\Bin" goto vscurrentbuild

echo "Unable to detect suitable environment. Check if VS 2026 is installed."
pause
goto exitbatch

:vscurrentinsiders
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Insiders\MSBuild\Current\Bin"
goto build

:vscurrentent
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Enterprise\MSBuild\Current\Bin"
goto build

:vscurrentpro
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Professional\MSBuild\Current\Bin"
goto build

:vscurrentcom
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin"
goto build

:vscurrentbuild
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\BuildTools\MSBuild\Current\Bin"
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
