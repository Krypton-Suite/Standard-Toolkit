## Visual Studio Templates

This directory contains Visual Studio templates for the Krypton Standard Toolkit.

For maintainer-focused implementation and release details, see:

- `../Documents/Development/Visual-Studio-Templates-Developer-Guide.md`

## Included Templates

- `ItemTemplates/KryptonForm`: Adds a `KryptonForm` item template for `Add > New Item`.
- `ItemTemplates/KryptonRibbonForm`: Adds a `KryptonRibbonForm` item template for `Add > New Item`.
- `ProjectTemplates/KryptonWinFormsApp`: Adds a WinForms project template that starts with a `KryptonForm`.
- `ProjectTemplates/KryptonRibbonWinFormsApp`: Adds a WinForms project template with a `KryptonRibbon` on the main form.

## Install (recommended — Marketplace)

When the public listing is available:

1. In Visual Studio, open **Extensions > Manage Extensions > Browse**.
2. Search for **Krypton Templates** and install it.
3. Restart Visual Studio.
4. Use **Add > New Item** or **Create a new project** and search for **Krypton**.

Later stable releases are offered as extension updates. Project templates from this listing reference the `Krypton.Standard.Toolkit` NuGet package.

Uninstall an older Krypton Templates package before installing this one. Packages built before Marketplace publishing used the publisher name `Krypton Suite`. This package uses the publisher id `KryptonSuite`. Installing a VSIX from another channel replaces the templates already installed, including this Marketplace extension.

## Install (VSIX)

1. Open the [GitHub Releases](https://github.com/Krypton-Suite/Standard-Toolkit/releases) page for this repository.
2. Open the newest release for your channel. Names look like `Krypton Templates (Stable) 110.26.10.281`. Tags look like `templates-stable-110.26.10.281` (`stable`, `canary`, `alpha`, `rc`, or `current`).
3. Download the `.vsix` on that release and double-click to install (or use **Extensions > Manage Extensions > Install from file**). Each release has one package.
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

## Marketplace publishing

Stable (`master`) releases upload the VSIX from the Release workflow (`.github/workflows/release.yml`). The `publish-templates-marketplace` job runs after a successful stable templates release. It uses the `production` environment secret `VSMARKETPLACE_PAT`. The token needs the **Marketplace (Manage)** scope. Create it from the Microsoft account that owns the publisher.

Before the first publish:

1. Create a publisher at the [Visual Studio Marketplace](https://marketplace.visualstudio.com/manage) whose **ID** is `KryptonSuite`. The display name can be Krypton Suite. The ID must match the VSIX `Publisher` attribute.
2. Store the token as `VSMARKETPLACE_PAT` on the `production` environment.
3. Leave the repository variable `VSMARKETPLACE_PRIVATE` unset to upload a private listing. Install that private extension and check the templates. Set `VSMARKETPLACE_PRIVATE` to `false` to make the listing public.

Canary, alpha, release-candidate, and current builds stay on GitHub Releases. Their project templates reference the Canary or Nightly NuGet packages.

Listing text and the private flag live in `Templates/Marketplace/`.

## Notes

- These templates use `Krypton.Toolkit` and `KryptonManager`.
- The ribbon item template also uses `Krypton.Ribbon`.
- The project template references `Krypton.Standard.Toolkit` from NuGet.
