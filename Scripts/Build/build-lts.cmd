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
echo.
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
for /f "tokens=* usebackq" %%A in (`tzutil /g`) do (
    set "zone=%%A"
)

@echo Started to build Stable release
@echo:
@echo Started: %date% %time% %zone%
@echo:
set "targets=Build"
if not "%~1" == "" set "targets=%~1"
"%msbuildpath%\msbuild.exe" /t:%targets% "%SCRIPT_DIR%longtermstable.proj" /fl /flp:logfile="%SCRIPT_DIR%..\..\Logs\long-term-stable-build-log.log" /bl:"%SCRIPT_DIR%..\..\Logs\long-term-stable-build-log.binlog" /clp:Summary;ShowTimestamp /v:quiet
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