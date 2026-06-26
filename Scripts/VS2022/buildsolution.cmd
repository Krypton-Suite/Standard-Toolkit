echo off
setlocal EnableExtensions
set "SCRIPT_DIR=%~dp0"
pushd "%SCRIPT_DIR%"

echo Do you want to build using Visual Studio 2022? (2022)
set INPUT=
set /P "INPUT=Type 2022: "
if /I "%INPUT%"=="2022" goto vs2022build
goto exitbatch

:vs2022build
call "%SCRIPT_DIR%..\Common\find-msbuild.cmd" 2022
if errorlevel 1 (
echo.
pause
goto exitbatch
)
goto build2022

:build2022
@echo Started: %date% %time%
@echo
set "targets=Build"
if not "%~1" == "" set "targets=%~1"
REM /m: multi-processor MSBuild (all logical CPUs).
"%msbuildpath%\msbuild.exe" /m /t:%targets% "%SCRIPT_DIR%build.proj" /fl /flp:logfile="%SCRIPT_DIR%..\..\Logs\solution-build-log.log" /bl:"%SCRIPT_DIR%..\..\Logs\solution-build-log.binlog" /clp:Summary;ShowTimestamp /v:quiet

:postbuild
echo Do you now want to create NuGet packages? (y/n)
set INPUT=
set /P "INPUT=Type input: "
if /I "%INPUT%"=="y" goto createpackages
if /I "%INPUT%"=="n" goto break
goto break

:createpackages
echo Do you want to pack using Visual Studio 2022? (2022)
set INPUT=
set /P "INPUT=Type 2022: "
if /I "%INPUT%"=="2022" goto vs2022pack
goto break

:vs2022pack
"%msbuildpath%\msbuild.exe" /m /t:Pack "%SCRIPT_DIR%build.proj" /fl /flp:logfile="%SCRIPT_DIR%..\..\Logs\solution-pack-log.log" /bl:"%SCRIPT_DIR%..\..\Logs\solution-pack-log.binlog" /clp:Summary;ShowTimestamp /v:quiet

@echo Build Completed: %date% %time%
goto break

:break
pause
@echo Build Completed: %date% %time%

:exitbatch
popd
exit /b