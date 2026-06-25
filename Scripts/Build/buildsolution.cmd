echo off
setlocal EnableExtensions
set "SCRIPT_DIR=%~dp0"
pushd "%SCRIPT_DIR%"

echo Do you want to build using Visual Studio 2019 or 2026? (2019/2026)
set INPUT=
set /P INPUT=Type 2019 or 2026: %=%
if /I "%INPUT%"=="2019" goto vs2019build
if /I "%INPUT%"=="2026" goto vs2026build

:vs2019build
call "%SCRIPT_DIR%..\Common\find-msbuild.cmd" 2019
if errorlevel 1 (
echo "Unable to detect suitable environment. Check if VS 2019 is installed."
pause
goto exitbatch
)
goto build2019

:build2019
@echo Started: %date% %time%
@echo
set "targets=Build"
if not "%~1" == "" set "targets=%~1"
REM /m: multi-processor MSBuild (all logical CPUs).
"%msbuildpath%\msbuild.exe" /m /t:%targets% "%SCRIPT_DIR%build.proj" /fl /flp:logfile="%SCRIPT_DIR%..\..\Logs\solution-build-log.log" /bl:"%SCRIPT_DIR%..\..\Logs\solution-build-log.binlog" /clp:Summary;ShowTimestamp /v:quiet

:vs2026build
call "%SCRIPT_DIR%..\Common\find-msbuild.cmd" 18
if errorlevel 1 (
echo "Unable to detect suitable environment. Check if VS 2026 is installed."
goto exitbatch
)
goto build2026

:build2026
set "targets=Build"
if not "%~1" == "" set "targets=%~1"
REM /m: multi-processor MSBuild (all logical CPUs).
"%msbuildpath%\msbuild.exe" /m /t:%targets% "%SCRIPT_DIR%build.proj" /fl /flp:logfile="%SCRIPT_DIR%..\..\Logs\solution-build-log.log" /bl:"%SCRIPT_DIR%..\..\Logs\solution-build-log.binlog" /clp:Summary;ShowTimestamp /v:quiet

echo Do you now want to create NuGet packages? (y/n)
set INPUT=
set /PINPUT=Type input: %=%
if /I "%INPUT%"=="y" goto createpackages
if /I "%INPUT%"=="n" goto break

:createpackages
echo Do you want to pack using Visual Studio 2019 or 2026? (2019/2026)
set INPUT=
set /P INPUT=Type 2019 or 2026: %=%
if /I "%INPUT%"=="2019" goto vs2019pack
if /I "%INPUT%"=="2026" goto vs2026pack

:vs2019pack
build-2019.cmd Pack

@echo Build Completed: %date% %time%

:vs2026pack
build-2026.cmd Pack

@echo Build Completed: %date% %time%

:break
pause
@echo Build Completed: %date% %time%

:exitbatch
popd
exit /b
