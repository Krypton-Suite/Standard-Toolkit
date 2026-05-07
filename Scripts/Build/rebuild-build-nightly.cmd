@echo off

if exist "%ProgramFiles%\Microsoft Visual Studio\18\Insiders\MSBuild\Current\Bin" goto vscurrentinsiders
if exist "%ProgramFiles%\Microsoft Visual Studio\18\Enterprise\MSBuild\Current\Bin" goto vscurrentent
if exist "%ProgramFiles%\Microsoft Visual Studio\18\Professional\MSBuild\Current\Bin" goto vscurrentpro
if exist "%ProgramFiles%\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin" goto vscurrentcom
if exist "%ProgramFiles%\Microsoft Visual Studio\18\BuildTools\MSBuild\Current\Bin" goto vscurrentbuild

echo "Unable to detect suitable environment. Check if VS 2026 is installed."

pause
goto exitbatch

:vscurrentinsiders
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Insiders\MSBuild\Current\Bin
goto build

:vscurrentent
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Enterprise\MSBuild\Current\Bin
goto build

:vscurrentpro
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Professional\MSBuild\Current\Bin
goto build

:vscurrentcom
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin
goto build

:vscurrentbuild
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\BuildTools\MSBuild\Current\Bin
goto build

:build
for /f "tokens=* usebackq" %%A in (`tzutil /g`) do (
    set "zone=%%A"
)

@echo Rebuild Started: %date% %time% %zone%
@echo
set targets=Rebuild
if not "%~1" == "" set targets=%~1
"%msbuildpath%\msbuild.exe" -t:%targets% nightly.proj /fl /flp:logfile=../Logs/nightly-build-log.log /bl:../Logs/nightly-build-log.binlog  /clp:Summary;ShowTimestamp /v:quiet

:: -t:rebuild

@echo Build Completed: %date% %time% %zone%

pause

@echo Do you want to return to complete another task? (Y/N)
set /p answer="Enter input: "
if %answer%==Y (goto run)
if %answer%==y (goto run)
if %answer%==N exit
if %answer%==n exit

@echo Invalid input, please try again.

:run
cd ../..

run.cmd

:exitbatch