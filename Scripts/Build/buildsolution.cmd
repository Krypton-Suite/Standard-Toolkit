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
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Insiders\MSBuild\Current\Bin" goto vs16prev
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin" goto vs16ent
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin" goto vs16pro
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin" goto vs16com
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin" goto vs16build

echo "Unable to detect suitable environment. Check if VS 2019 is installed."

pause

goto exitbatch

:vs16prev
set "msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Insiders\MSBuild\Current\Bin"
goto build2019

:vs16ent
set "msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin"
goto build2019

:vs16pro
set "msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin"
goto build2019

:vs16com
set "msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin"
goto build2019

:vs16build
set "msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin"
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
goto build2026

:vscurrentent
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Enterprise\MSBuild\Current\Bin"
goto build2026

:vscurrentpro
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Professional\MSBuild\Current\Bin"
goto build2026

:vscurrentcom
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin"
goto build2026

:vscurrentbuild
set "msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\BuildTools\MSBuild\Current\Bin"
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