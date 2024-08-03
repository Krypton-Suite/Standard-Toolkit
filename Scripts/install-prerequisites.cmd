@echo off

goto mainmenu

:visualstudiomainmenu
cls

echo 1. Install Microsoft Visual Studio 2022
echo 2. Go back to main menu

set /p answer="Enter choice (1 - 2): "
if %answer%==1 (goto visualstudioflavourmenu)
if %answer%==2 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto visualstudiomainmenu

:visualstudioflavourmenu
cls

echo 1. Install preview versions
echo 2. Install stable versions
echo 3. Go back to main menu

set /p answer="Enter choice (1 - 3): "
if %answer%==1 (goto visualstudiopreviewinstallmenu)
if %answer%==2 (goto visualstudiostableinstallmenu)
if %answer%==3 (goto visualstudiomainmenu)

@echo Invalid input, please try again.

pause

goto visualstudioflavourmenu

:visualstudiopreviewinstallmenu
cls

echo 1. Install Community 2022 (Preview)
echo 2. Install Enterprise 2022 (Preview)
echo 3. Install Professional 2022 (Preview)
echo 4. Go back

set /p answer="Enter choice (1 - 4): "
if %answer%==1 (goto installvisualstudiocommunitypreview)
if %answer%==2 (goto installvisualstudioenterprisepreview)
if %answer%==3 (goto installvisualstudioprofessionalpreview)
if %answer%==4 (goto visualstudioflavourmenu)

@echo Invalid input, please try again.

pause

goto visualstudiopreviewinstallmenu

:visualstudiostableinstallmenu
cls

echo 1. Install Community 2022
echo 2. Install Enterprise 2022
echo 3. Install Professional 2022
echo 4. Go back

set /p answer="Enter choice (1 - 4): "
if %answer%==1 (goto installvisualstudiocommunity)
if %answer%==2 (goto installvisualstudioenterprise)
if %answer%==3 (goto installvisualstudioprofessional)
if %answer%==4 (goto visualstudioflavourmenu)

@echo Invalid input, please try again.

pause

goto visualstudiostableinstallmenu

:installdotnetmenu
cls

echo 1. Install .NET 9 (Preview)
echo 2. Install .NET 8
echo 3. Install .NET 6
echo 4. Install all supported .NET versions
echo 5. Go back to main menu

set /p answer="Enter choice (1 - 5): "
if %answer%==1 (goto installdotnetnine)
if %answer%==2 (goto installdotneteight)
if %answer%==3 (goto installdotnetsix)
if %answer%==4 (goto installdotnet)
if %answer%==5 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto installdotnetmenu

:upgradetoolsmenu
cls

echo 1. Upgrade Microsoft Build Log Viewer
echo 2. Upgrade Microsoft Build Tools
echo 3. Upgrade .NET
echo     a. .NET 9
echo     b. .NET 8
echo     c. .NET 6
echo     d. All supported .NET versions
echo 4. Upgrade all tools
echo 5. Go to main menu

set /p answer="Enter choice (1 - 5) or (a - d): "
if %answer%==1 (goto upgrademicrosoftbuildlogviewer)
if %answer%==2 (goto upgrademicrosoftbuildtools)
if %answer%==3 (goto upgradedotnetmenu)
if %answer%==a (goto upgradedotnetnine)
if %answer%==b (goto upgradedotneteight)
if %answer%==c (goto upgradedotnetsix)
if %answer%==d (goto upgradedotnet)
if %answer%==4 (goto upgradealltools)
if %answer%==5 (goto mainmenu)

@echo Invalid input, please try again.

pause

goto upgradetoolsmenu

::=====================================================================

:mainmenu

cls

echo 1. Install Microsoft Build Log Viewer
echo 2. Install Microsoft Build Tools
echo 3. Install Microsoft Visual Studio
echo 4. Install .NET
echo 5. Upgrade Tools
echo 6. Go Back

set /p answer="Enter choice (1 - 6): "
if %answer%==1 (goto installbuildlogviewer)
if %answer%==2 (goto installmicrosoftbuildtools)
if %answer%==3 (goto visualstudiomainmenu)
if %answer%==4 (goto installdotnetmenu)
if %answer%==5 (goto upgradetoolsmenu)
if %answer%==6 (goto goback)

@echo Invalid input, please try again.

pause

goto mainmenu

:installbuildlogviewer

winget install KirillOsenkov.MSBuildStructuredLogViewer

:installmicrosoftbuildtools

winget install Microsoft.VisualStudio.2022.BuildTools

:installdotnet

winget install Microsoft.DotNet.SDK.Preview

winget install Microsoft.DotNet.SDK.8

winget install Microsoft.DotNet.SDK.6

:upgradedotnet
cls 

winget upgrade Microsoft.DotNet.SDK.Preview

winget upgrade Microsoft.DotNet.SDK.8

winget upgrade Microsoft.DotNet.SDK.6

goto installdotnetmenu

:upgradealltools

winget upgrade KirillOsenkov.MSBuildStructuredLogViewer

winget upgrade Microsoft.VisualStudio.2022.BuildTools

winget upgrade Microsoft.DotNet.SDK.Preview

winget upgrade Microsoft.DotNet.SDK.8

winget upgrade Microsoft.DotNet.SDK.6

:installdotnetnine
cls

winget install Microsoft.DotNet.SDK.Preview

goto installdotnetmenu

:installdotneteight
cls

winget install Microsoft.DotNet.SDK.8

goto installdotnetmenu

:installdotnetsix
cls 

winget install Microsoft.DotNet.SDK.6

goto installdotnetmenu

:installvisualstudiocommunity
cls

winget install Microsoft.VisualStudio.2022.Community

:installvisualstudiocommunitypreview
cls

winget install Microsoft.VisualStudio.2022.Community.Preview

:installvisualstudioenterprise
cls

winget install Microsoft.VisualStudio.2022.Enterprise

:installvisualstudioenterprisepreview
cls

winget install Microsoft.VisualStudio.2022.Enterprise.Preview

:installvisualstudioprofessional
cls

winget install Microsoft.VisualStudio.2022.Professional

:installvisualstudioprofessionalpreview
cls

winget install Microsoft.VisualStudio.2022.Professional.Preview

:goback

main-menu.cmd