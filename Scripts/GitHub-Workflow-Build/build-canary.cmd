@echo off

if exist "%ProgramFiles%\Microsoft Visual Studio\18\Insiders\MSBuild\Current\Bin" goto vs18prev
if exist "%ProgramFiles%\Microsoft Visual Studio\18\Enterprise\MSBuild\Current\Bin" goto vs18ent
if exist "%ProgramFiles%\Microsoft Visual Studio\18\Professional\MSBuild\Current\Bin" goto vs18pro
if exist "%ProgramFiles%\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin" goto vs18com
if exist "%ProgramFiles%\Microsoft Visual Studio\18\BuildTools\MSBuild\Current\Bin" goto vs18build

echo "Unable to detect suitable environment. Check if VS 2026 is installed."

pause
goto exitbatch

:vs18prev
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Insiders\MSBuild\Current\Bin
goto build

:vs18ent
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Enterprise\MSBuild\Current\Bin
goto build

:vs18pro
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Professional\MSBuild\Current\Bin
goto build

:vs18com
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin
goto build

:vs18build
set msbuildpath=%ProgramFiles%\Microsoft Visual Studio\18\BuildTools\MSBuild\Current\Bin
goto build

:build
for /f "tokens=* usebackq" %%A in (`tzutil /g`) do (
    set "zone=%%A"
)

@echo Started to build Canary release
@echo:
@echo Started: %date% %time% %zone%
@echo:
set targets=Build
if not "%~1" == "" set targets=%~1
"%msbuildpath%\msbuild.exe" /t:%targets% canary.proj /fl /flp:logfile=../Logs/canary-build-log.log /bl:../Logs/canary-build-log.binlog /clp:Summary;ShowTimestamp /v:quiet
@echo:
@echo Canary release build completed: %date% %time% %zone%
@echo:
@echo You can find the build Logs in ../Logs
@echo:
pause

@echo Do you want to return to complete another task? (Y/N)
@echo:
set /p answer="Enter input: "
if %answer%==Y (goto run)
if %answer%==y (goto run)
if %answer%==N exit
if %answer%==n exit

@echo Invalid input, please try again.

:run
cd ..

main-menu.cmd