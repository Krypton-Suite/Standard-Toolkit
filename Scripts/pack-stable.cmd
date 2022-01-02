@REM This command build the toolkit and create nuget packages

@echo off

echo Do you want to pack using Visual Studio 2019 or 2022? (2019/2022)
set INPUT=
set /P INPUT=Type 2019 or 2022: %=%
if /I "%INPUT%"=="2019" goto vs2019pack
if /I "%INPUT%"=="2022" goto vs2022pack

:vs2019pack
build-stable-2019.cmd Pack

@echo Do you want to return to complete another task? (Y/N)
set /p answer="Enter input:"
if %answer%==Y (goto run)
if %answer%==y (goto run)
if %answer%==N exit
if %answer%==n exit

@echo Invalid input, please try again.

:vs2022pack
build-stable-2022.cmd Pack

@echo Do you want to return to complete another task? (Y/N)
set /p answer="Enter input:"
if %answer%==Y (goto run)
if %answer%==y (goto run)
if %answer%==N exit
if %answer%==n exit

@echo Invalid input, please try again.

:run
cd ..

run.cmd