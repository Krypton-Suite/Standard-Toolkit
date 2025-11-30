@echo off
setlocal EnableExtensions
set "SCRIPT_DIR=%~dp0"
pushd "%SCRIPT_DIR%"

if exist "%ProgramFiles%\Microsoft Visual Studio\18\Insiders\MSBuild\Current\Bin" goto vs18prev
if exist "%ProgramFiles%\Microsoft Visual Studio\18\Enterprise\MSBuild\Current\Bin" goto vs18ent
if exist "%ProgramFiles%\Microsoft Visual Studio\18\Professional\MSBuild\Current\Bin" goto vs18pro
if exist "%ProgramFiles%\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin" goto vs18com
if exist "%ProgramFiles%\Microsoft Visual Studio\18\BuildTools\MSBuild\Current\Bin" goto vs18build

echo "Unable to detect suitable environment. Check if VS 2026 is installed."
echo.
pause
goto exitbatch

:vs18prev
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Insiders\MSBuild\Current\Bin"
goto build

:vs18ent
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Enterprise\MSBuild\Current\Bin"
goto build

:vs18pro
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Professional\MSBuild\Current\Bin"
goto build

:vs18com
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin"
goto build

:vs18build
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\BuildTools\MSBuild\Current\Bin"
goto build

:build
for /f "tokens=* usebackq" %%A in (`tzutil /g`) do (
    set "zone=%%A"
)

@echo Started: %date% %time% %zone%
@echo
set "targets=Build"
if not "%~1" == "" set "targets=%~1"
"%msbuildpath%\msbuild.exe" /t:%targets% "%SCRIPT_DIR%..\Project Files\installer.proj" /fl /flp:logfile="%SCRIPT_DIR%..\..\Logs\installer-build-log.log" /bl:"%SCRIPT_DIR%..\..\Logs\installer-build-log.binlog" /clp:Summary;ShowTimestamp /v:quiet

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