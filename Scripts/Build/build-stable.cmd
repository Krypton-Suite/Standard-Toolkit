@echo off
setlocal EnableExtensions
set "SCRIPT_DIR=%~dp0"
pushd "%SCRIPT_DIR%"

if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Insiders\MSBuild\Current\Bin" goto vs16prev
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin" goto vs16ent
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin" goto vs16pro
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin" goto vs16com
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin" goto vs16build

echo "Unable to detect suitable environment. Check if VS 2019 is installed."
echo.
pause
goto exitbatch

:vs16prev
set "msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Insiders\MSBuild\Current\Bin"
goto build

:vs16ent
set "msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin"
goto build

:vs16pro
set "msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin"
goto build

:vs16com
set "msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin"
goto build

:vs16build
set "msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin"
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