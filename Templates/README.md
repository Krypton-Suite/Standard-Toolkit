## Visual Studio Templates

This directory contains Visual Studio templates for the Krypton Standard Toolkit.

For maintainer-focused implementation and release details, see:

- `../Documents/Development/Visual-Studio-Templates-Developer-Guide.md`

## Included Templates

- `ItemTemplates/KryptonForm`: Adds a `KryptonForm` item template for `Add > New Item`.
- `ItemTemplates/KryptonRibbonForm`: Adds a `KryptonRibbonForm` item template for `Add > New Item`.
- `ProjectTemplates/KryptonWinFormsApp`: Adds a WinForms project template that starts with a `KryptonForm`.
- `ProjectTemplates/KryptonRibbonWinFormsApp`: Adds a WinForms project template with a `KryptonRibbon` on the main form.

## Install (Visual Studio 2022)

1. Zip each template folder so that the `.vstemplate` file is at the root of the zip.
2. Copy item template zip files to:
   - `%USERPROFILE%\Documents\Visual Studio 2022\Templates\ItemTemplates\Visual C#\`
3. Copy project template zip files to:
   - `%USERPROFILE%\Documents\Visual Studio 2022\Templates\ProjectTemplates\Visual C#\`
4. Restart Visual Studio.

## Notes

- These templates use `Krypton.Toolkit` and `KryptonManager`.
- The ribbon item template also uses `Krypton.Ribbon`.
- The project template references `Krypton.Standard.Toolkit` from NuGet.
