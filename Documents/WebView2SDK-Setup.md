# WebView2 SDK Setup for KryptonWebView2

## Overview

The KryptonWebView2 control requires the Microsoft WebView2 SDK assemblies to be available for compilation. This document explains how to set up the WebView2 SDK without using NuGet dependencies.

**Quick Start**: Run `run.cmd` and select option 7 "WebView2 SDK Tools" → option 1 "Setup WebView2 SDK" for automated setup.

**Latest Version**: The setup automatically detects and uses the latest stable WebView2 SDK version from NuGet.

## Setup Options

### Option 1: Manual Download (Recommended)

1. **Download WebView2 SDK**
   - Visit: https://developer.microsoft.com/en-us/microsoft-edge/webview2/
   - Download the latest WebView2 SDK
   - Extract the downloaded package

2. **Copy Required Assemblies**
   - Create a `WebView2SDK` folder at the repository root (same level as `Source` folder)
   - Copy the following files from the extracted SDK to `WebView2SDK`:
     ```
     WebView2SDK/
     ├── Microsoft.Web.WebView2.Core.dll
     ├── Microsoft.Web.WebView2.WinForms.dll
     └── WebView2Loader.dll
     ```

3. **Build the Solution**
   - Rebuild the entire solution
   - The KryptonWebView2 control should now compile successfully

### Option 2: Using NuGet (Temporary)

If you want to quickly get the assemblies without manual download:

1. **Install NuGet Package**
   ```cmd
   dotnet add package Microsoft.Web.WebView2
   ```

2. **Copy Assemblies**
   - Find the NuGet packages folder (usually `%USERPROFILE%\.nuget\packages\`)
   - Navigate to the latest version folder (e.g., `microsoft.web.webview2\1.0.3485.44\lib\net45\`)
   - Copy the required DLLs to the `WebView2SDK` folder
   - Remove the NuGet package reference if desired

3. **Clean Up**
   ```cmd
   dotnet remove package Microsoft.Web.WebView2
   ```

### Option 3: Using Setup Script (Recommended)

#### Method A: Through Build System Menu
1. **Run the Build System**
   ```cmd
   run.cmd
   ```
2. **Select Option 7**: "WebView2 SDK Tools"
3. **Select Option 1**: "Setup WebView2 SDK"
4. **Follow the automated process**

#### Method B: Direct Script Execution
Run the provided setup script directly:
```cmd
Setup-WebView2SDK.cmd
```

Both methods will:
- Check if WebView2 SDK is already installed
- Automatically detect the latest stable WebView2 SDK version
- Download and install WebView2 SDK via NuGet (if needed)
- Copy required assemblies to the WebView2SDK directory
- Update project file with the latest version information
- Clean up temporary NuGet references
- Provide clear feedback on the setup status

### Option 4: Update to Latest Version

If you already have WebView2 SDK installed and want to update to the latest version:

#### Method A: Through Build System Menu
1. **Run the Build System**
   ```cmd
   run.cmd
   ```
2. **Select Option 7**: "WebView2 SDK Tools"
3. **Select Option 2**: "Update WebView2 SDK"
4. **Follow the automated update process**

#### Method B: Direct Script Execution
Run the update script directly:
```cmd
Scripts\Update-WebView2SDK.cmd
```

Both methods will:
- Detect the latest stable WebView2 SDK version
- Download the latest assemblies
- Update the WebView2SDK directory
- Update project file references
- Preserve your existing setup

## File Structure After Setup

Your repository should look like this:
```
Standard-Toolkit/
├── Source/
│   └── Krypton Components/
│       └── Krypton.Toolkit/
│           └── Controls Toolkit/
│               └── KryptonWebView2.cs
├── Scripts/
│   ├── Setup-WebView2SDK.cmd
│   ├── Update-WebView2SDK.cmd
│   ├── Get-LatestWebView2Version.ps1
│   └── Update-WebView2ProjectVersion.ps1
├── WebView2SDK/                    ← Created by setup script
│   ├── Microsoft.Web.WebView2.Core.dll
│   ├── Microsoft.Web.WebView2.WinForms.dll
│   └── WebView2Loader.dll
└── run.cmd                         ← Updated with WebView2 SDK Tools submenu
```

## Verification

After setup, verify that the KryptonWebView2 control compiles:

1. **Build the Solution**
   ```cmd
   dotnet build "Source/Krypton Components/Krypton Toolkit Suite 2022 - VS2022.sln"
   ```

2. **Check for Errors**
   - No compilation errors related to WebView2 types
   - KryptonWebView2 appears in the toolbox
   - Test form compiles successfully

## Troubleshooting

### "WebView2 types not found" Error
- Ensure the WebView2SDK folder exists at the repository root
- Verify all three DLL files are present in WebView2SDK
- Check that the project file references point to the correct paths
- Rebuild the solution completely

### Assembly Loading Errors
- Ensure the DLLs are the correct architecture (AnyCPU or x64)
- Check that the DLLs are not corrupted
- Verify the file permissions allow reading

### Designer Issues
- Reset the Visual Studio toolbox
- Close and reopen Visual Studio
- Ensure the control compiles without errors first

## Runtime Requirements

Remember that end users need the WebView2 Runtime installed:

- **Evergreen Distribution**: Users install from Microsoft's website
- **Fixed Version**: Bundle with your application
- **Download**: https://developer.microsoft.com/en-us/microsoft-edge/webview2/

## Support

If you encounter issues:

1. Check the WebView2 documentation: https://docs.microsoft.com/en-us/microsoft-edge/webview2/
2. Verify your setup matches the requirements
3. Test with the provided KryptonWebView2Test form
4. Report issues to the Krypton Toolkit repository

## Notes

- The WebView2SDK folder should be added to `.gitignore` if you don't want to commit the assemblies
- The assemblies are referenced with `Private=False` so they won't be copied to output
- This setup allows the control to compile without NuGet dependencies
- Runtime WebView2 functionality requires the WebView2 Runtime to be installed on target systems
