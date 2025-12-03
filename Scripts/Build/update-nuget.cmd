@echo off

cls

echo 1. Update NuGet client
echo 2. Update NuGet client (use 'Self' switch)
echo 3. Go back to main menu
echo 4. End

set /p answer="Enter number (1 - 4): "
if %answer%==1 (goto updatenuget)
if %answer%==2 (goto updatenugetself)
if %answer%==3 (goto backtorun)
if %answer%==4 (goto end)

@echo Invalid input, please try again.

pause

goto setmenu

:setmenu

cls

echo 1. Update NuGet client
echo 2. Update NuGet client (use 'Self' switch)
echo 3. Go back to main menu
echo 4. End

set /p answer="Enter number (1 - 4): "
if %answer%==1 (goto updatenuget)
if %answer%==2 (goto updatenugetself)
if %answer%==3 (goto backtorun)
if %answer%==4 (goto end)

@echo Invalid input, please try again.

pause

goto setmenu

:backtorun
cd ..

run.cmd

:updatenuget
cls

nuget update

pause

goto setmenu

:updatenugetself
cls

nuget update -Self

pause

goto setmenu

:end
@echo Exiting the build system, have a good day. Bye!

pause

exit