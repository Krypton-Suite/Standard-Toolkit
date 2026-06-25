echo off
setlocal EnableExtensions
set "SCRIPT_DIR=%~dp0"
pushd "%SCRIPT_DIR%"

echo Do you want to build using Visual Studio 2019 or 2022? (2019/2022)
set INPUT=
set /P INPUT=Type 2019 or 2022: %=%
if /I "%INPUT%"=="2019" goto vs2019build
if /I "%INPUT%"=="2022" goto vs2022build

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

:vs2022build
call "%SCRIPT_DIR%..\Common\find-msbuild.cmd" 2022
if errorlevel 1 (
echo "Unable to detect suitable environment. Check if VS 2022 is installed."
goto exitbatch
)
goto build2022

:build2022
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
echo Do you want to pack using Visual Studio 2019 or 2022? (2019/2022)
set INPUT=
set /P INPUT=Type 2019 or 2022: %=%
if /I "%INPUT%"=="2019" goto vs2019pack
if /I "%INPUT%"=="2022" goto vs2022pack

:vs2019pack
build-2019.cmd Pack

@echo Build Completed: %date% %time%

:vs2022pack
build-2022.cmd Pack

@echo Build Completed: %date% %time%

:break
pause
@echo Build Completed: %date% %time%

:exitbatch
popd
exit /b
