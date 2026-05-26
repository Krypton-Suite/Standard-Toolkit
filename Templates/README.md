## Visual Studio Templates

This directory contains Visual Studio templates for the Krypton Standard Toolkit.

For maintainer-focused implementation and release details, see:

- `../Documents/Development/Visual-Studio-Templates-Developer-Guide.md`

## Included Templates

- `ItemTemplates/KryptonForm`: Adds a `KryptonForm` item template for `Add > New Item`.
- `ItemTemplates/KryptonRibbonForm`: Adds a `KryptonRibbonForm` item template for `Add > New Item`.
- `ProjectTemplates/KryptonWinFormsApp`: Adds a WinForms project template that starts with a `KryptonForm`.
- `ProjectTemplates/KryptonRibbonWinFormsApp`: Adds a WinForms project template with a `KryptonRibbon` on the main form.

## Install (recommended — VSIX)

1. Open the [GitHub Releases](https://github.com/Krypton-Suite/Standard-Toolkit/releases) page for this repository.
2. Select the templates release for your channel (`templates-stable`, `templates-canary`, `templates-alpha`, or `templates-current`).
3. Download `krypton-templates-*.vsix` and double-click to install (or use **Extensions > Manage Extensions > Install from file**).
4. Restart Visual Studio.
5. Use **Add > New Item** or **Create a new project** and search for **Krypton**.

## Install (manual zip fallback)

1. Zip each template folder so that the `.vstemplate` file is at the root of the zip.
2. Copy item template zip files to:
   - `%USERPROFILE%\Documents\Visual Studio 2022\Templates\ItemTemplates\Visual C#\`
3. Copy project template zip files to:
   - `%USERPROFILE%\Documents\Visual Studio 2022\Templates\ProjectTemplates\Visual C#\`
4. Restart Visual Studio.

## Local VSIX build

```cmd
dotnet restore "Templates\Vsix\Krypton.Templates.Vsix\Krypton.Templates.Vsix.csproj"
dotnet msbuild "Templates\Vsix\Krypton.Templates.Vsix\Krypton.Templates.Vsix.csproj" /p:Configuration=Release /p:DeployExtension=false
```

Output: `Templates\Vsix\Krypton.Templates.Vsix\bin\Release\net472\Krypton.Templates.Vsix.vsix`

## Notes

- These templates use `Krypton.Toolkit` and `KryptonManager`.
- The ribbon item template also uses `Krypton.Ribbon`.
- The project template references `Krypton.Standard.Toolkit` from NuGet.
