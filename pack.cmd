@REM This command build the toolkit and create nuget packages

echo Do you want to pack using Visual Studio 2019 or 2022? (2019/2022)
set INPUT=
set /P INPUT=Type 2019 or 2022: %=%
if /I "%INPUT%"=="2019" goto vs2019pack
if /I "%INPUT%"=="2022" goto vs2022pack

:vs2019pack
build-2019.cmd Pack

:vs2022pack
build-2022.cmd Pack