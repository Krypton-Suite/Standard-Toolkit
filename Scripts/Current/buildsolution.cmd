echo off

echo Do you want to build using Visual Studio 2019 or 2026? (2019/2026)
set INPUT=
set /P INPUT=Type 2019 or 2026: %=%
if /I "%INPUT%"=="2019" goto vs2019build
if /I "%INPUT%"=="2026" goto vs2026build

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
set msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Insiders\MSBuild\Current\Bin
goto build2019

:vs16ent
set msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin
goto build2019

:vs16pro
set msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin
goto build2019

:vs16com
set msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin
goto build2019

:vs16build
set msbuildpath=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin
goto build2019

:build2019
@echo Started: %date% %time%
@echo
set targets=Build
if not "%~1" == "" set targets=%~1
"%msbuildpath%\msbuild.exe" /t:%targets% build.proj /fl /flp:logfile=../Logs/solution-build-log.log /bl:solution-build-log.binlog /clp:Summary;ShowTimestamp /v:quiet

:vs2026build
if exist "%ProgramFiles%\Microsoft Visual Studio\18\Insiders\MSBuild\Current\Bin" goto vscurrentinsiders
if exist "%ProgramFiles%\Microsoft Visual Studio\18\Enterprise\MSBuild\Current\Bin" goto vscurrentent
if exist "%ProgramFiles%\Microsoft Visual Studio\18\Professional\MSBuild\Current\Bin" goto vscurrentpro
if exist "%ProgramFiles%\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin" goto vscurrentcom
if exist "%ProgramFiles%\Microsoft Visual Studio\18\BuildTools\MSBuild\Current\Bin" goto vscurrentbuild

echo "Unable to detect suitable environment. Check if VS 2026 is installed."
goto exitbatch
pause

:vscurrentinsiders
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Insiders\MSBuild\Current\Bin
goto build2026

:vscurrentent
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Enterprise\MSBuild\Current\Bin
goto build2026

:vscurrentpro
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Professional\MSBuild\Current\Bin
goto build2026

:vscurrentcom
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin
goto build2026

:vscurrentbuild
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\BuildTools\MSBuild\Current\Bin
goto build2026

:build2026
set targets=Build
if not "%~1" == "" set targets=%~1
"%msbuildpath%\msbuild.exe" /t:%targets% build.proj /fl /flp:logfile=build.log

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