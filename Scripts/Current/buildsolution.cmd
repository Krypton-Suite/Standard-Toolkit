echo off
setlocal EnableExtensions
set "SCRIPT_DIR=%~dp0"
pushd "%SCRIPT_DIR%"

echo Do you want to build using Visual Studio 2026? (2026)
set INPUT=
set /P "INPUT=Type 2026: "
if /I "%INPUT%"=="2026" goto vs2026build
goto exitbatch

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
set "targets=Build"
if not "%~1" == "" set "targets=%~1"
REM /m: multi-processor MSBuild (all logical CPUs).
"%msbuildpath%\msbuild.exe" /m /t:%targets% "%SCRIPT_DIR%build.proj" /fl /flp:logfile="%SCRIPT_DIR%..\..\Logs\solution-build-log.log" /bl:"%SCRIPT_DIR%..\..\Logs\solution-build-log.binlog" /clp:Summary;ShowTimestamp /v:quiet
goto postbuild

:postbuild
echo Do you now want to create NuGet packages? (y/n)
set INPUT=
set /P "INPUT=Type input: "
if /I "%INPUT%"=="y" goto createpackages
if /I "%INPUT%"=="n" goto break
goto break

:createpackages
echo Do you want to pack using Visual Studio 2026? (2026)
set INPUT=
set /P "INPUT=Type 2026: "
if /I "%INPUT%"=="2026" goto vs2026pack
goto break

:vs2026pack
"%msbuildpath%\msbuild.exe" /m /t:Pack "%SCRIPT_DIR%build.proj" /fl /flp:logfile="%SCRIPT_DIR%..\..\Logs\solution-pack-log.log" /bl:"%SCRIPT_DIR%..\..\Logs\solution-pack-log.binlog" /clp:Summary;ShowTimestamp /v:quiet

@echo Build Completed: %date% %time%
goto break


:break
pause
@echo Build Completed: %date% %time%

:exitbatch
popd
exit /b