echo off
setlocal EnableExtensions
set "SCRIPT_DIR=%~dp0"
pushd "%SCRIPT_DIR%"

echo Do you want to build using Visual Studio 2019 or 2026? (2019/2026)
set INPUT=
set /P "INPUT=Type 2019 or 2026: "
if /I "%INPUT%"=="2019" goto vs2019build
if /I "%INPUT%"=="2026" goto vs2026build
goto exitbatch

:vs2019build
call "%SCRIPT_DIR%..\Common\find-msbuild.cmd" 2019
if errorlevel 1 (
echo.
pause
goto exitbatch
)
goto build2019

:build2019
@echo Started: %date% %time%
@echo
if "%targets%" == "" set "targets=Build"
if not "%~1" == "" set "targets=%~1"
REM /m: multi-processor MSBuild (all logical CPUs).
"%msbuildpath%\msbuild.exe" /m /t:%targets% "%SCRIPT_DIR%build.proj" /fl /flp:logfile="%SCRIPT_DIR%..\..\Logs\solution-build-log.log" /bl:"%SCRIPT_DIR%..\..\Logs\solution-build-log.binlog" /clp:Summary;ShowTimestamp /v:quiet
if "%afterpack%" == "1" goto packdone
goto postbuild

:vs2026build
call "%SCRIPT_DIR%..\Common\find-msbuild.cmd" current
if errorlevel 1 (
echo.
pause
goto exitbatch
)
goto build2026

:build2026
@echo Started: %date% %time%
@echo
if "%targets%" == "" set "targets=Build"
if not "%~1" == "" set "targets=%~1"
REM /m: multi-processor MSBuild (all logical CPUs).
"%msbuildpath%\msbuild.exe" /m /t:%targets% "%SCRIPT_DIR%build.proj" /fl /flp:logfile="%SCRIPT_DIR%..\..\Logs\solution-build-log.log" /bl:"%SCRIPT_DIR%..\..\Logs\solution-build-log.binlog" /clp:Summary;ShowTimestamp /v:quiet
if "%afterpack%" == "1" goto packdone
goto postbuild

:postbuild
echo Do you now want to create NuGet packages? (y/n)
set INPUT=
set /P "INPUT=Type input: "
if /I "%INPUT%"=="y" goto createpackages
if /I "%INPUT%"=="n" goto break
goto break

:createpackages
echo Do you want to pack using Visual Studio 2019 or 2026? (2019/2026)
set INPUT=
set /P "INPUT=Type 2019 or 2026: "
if /I "%INPUT%"=="2019" goto pack2019
if /I "%INPUT%"=="2026" goto pack2026
goto break

:pack2019
set "targets=Pack"
set "afterpack=1"
goto vs2019build

:pack2026
set "targets=Pack"
set "afterpack=1"
goto vs2026build

:packdone
@echo Build Completed: %date% %time%
goto break


:break
pause
@echo Build Completed: %date% %time%

:exitbatch
popd
exit /b