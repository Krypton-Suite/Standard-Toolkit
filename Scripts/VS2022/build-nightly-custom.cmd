@echo off
setlocal EnableExtensions
set "SCRIPT_DIR=%~dp0"
pushd "%SCRIPT_DIR%"

if exist "%ProgramFiles%\Microsoft Visual Studio\2022\Preview\MSBuild\Current\Bin" goto vs17prev
if exist "%ProgramFiles%\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin" goto vs17ent
if exist "%ProgramFiles%\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin" goto vs17pro
if exist "%ProgramFiles%\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin" goto vs17com
if exist "%ProgramFiles%\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin" goto vs17build

echo "Unable to detect suitable environment. Check if VS 2022 is installed."
echo.
pause
goto exitbatch

:vs17prev
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\2022\Preview\MSBuild\Current\Bin"
goto build

:vs17ent
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin"
goto build

:vs17pro
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin"
goto build

:vs17com
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin"
goto build

:vs17build
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin"
goto build

:build
for /f "tokens=* usebackq" %%A in (`tzutil /g`) do (
    set "zone=%%A"
)

@echo Started: %date% %time% %zone%
@echo
set "targets=Build"
if not "%~1" == "" set "targets=%~1"
"%msbuildpath%\msbuild.exe" /t:%targets% "%SCRIPT_DIR%nightly.proj" /fl /flp:logfile="%SCRIPT_DIR%..\..\build.log"

@echo Build Completed: %date% %time% %zone%

:exitbatch
popd
"%SCRIPT_DIR%..\main-menu.cmd"
exit /b