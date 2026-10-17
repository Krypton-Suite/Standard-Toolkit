# KryptonForm caption controls

Use `KryptonCaptionForm` when the title bar hosts controls. Ordinary `KryptonForm` keeps the previous designer surface. Each hosted control keeps its own left, centre, or right edge. `KryptonSearchBox` works because it derives from `KryptonTextBox` and lives in `Krypton.Toolkit.Utilities`. Toolkit does not reference Utilities.

The caption is non-client chrome. Hosted controls are real HWNDs, each parented to its own borderless owned window over a reserved caption slot. `KryptonForm` exposes empty protected chrome hooks (`PrepareCaptionChromeSlots`, `SyncCaptionChromeHosts`, and related members) that `KryptonCaptionForm` overrides.

## Public surface

| Member | Where | Role |
|--------|-------|------|
| `KryptonCaptionForm` | Derived form | Hosts caption controls without growing every `KryptonForm` designer. |
| `CaptionTextBox` / `CaptionTextBoxEdge` | `KryptonCaptionForm` | Simple shortcut for one text box (or search box). |
| `KryptonFormCaptionItems` | Toolbox component | Designer list of caption items for a `KryptonCaptionForm`. |
| `KryptonFormCaptionItem` | Item on that component | One `Control` plus its `FormCaptionTextBoxEdge`. |
| `AddCaptionControl` / `SetCaptionControlEdge` / `RemoveCaptionControl` | `KryptonCaptionForm` | Advanced code API. |

## Designer usage

1. Change the form base type to `KryptonCaptionForm`.
2. Drop `KryptonFormCaptionItems`, set `OwnerForm`, and edit `Items`.
3. Or assign `CaptionTextBox` for a single editor.

```csharp
public partial class MainForm : KryptonCaptionForm
{
    public MainForm()
    {
        InitializeComponent();
        var items = new KryptonFormCaptionItems { OwnerForm = this };
        items.Items.Add(new KryptonFormCaptionItem(button, FormCaptionTextBoxEdge.Left));
        items.Items.Add(new KryptonFormCaptionItem(search, FormCaptionTextBoxEdge.Right));
        items.Apply();
    }
}
```

## Validation

- TestForm: `CaptionTextBoxDemo` inherits `KryptonCaptionForm` and uses `KryptonFormCaptionItems`.
- Consumer example: `KryptonForm Examples` on Standard-Toolkit-Demos branch `alpha-4550-caption-textbox`.