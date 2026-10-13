# Feature: Add a ribbon group stack for more than three items (#4560)

## Summary

`KryptonRibbonGroupTriple` stays limited to three items. `KryptonRibbonGroupStack` is a new group container that holds any number of the same children as a lines container, inside the ribbon’s three-line height. `Columns` fills down columns of three. `Adaptive` shows large items in one row and wraps smaller items into columns of three.

## Related issues

- Closes #4560

## Type of change

- [x] Feature / enhancement (`Implemented`)

## Changes

- `Krypton.Ribbon`: `KryptonRibbonGroupStack`, `KryptonRibbonGroupStackCollection`, and `RibbonGroupStackLayout`.
- Layout view places children column-major (at most three per column). Adaptive large size uses one horizontal row of full-height children.
- Group designer, design-time New menu, and collection editor can add a stack. A new stack is seeded with six buttons. Leaf item designers and the cluster designer accept a stack parent.
- TestForm `RibbonGroupStackDemo` compares a triple, a columns stack, and an adaptive stack.

## Affected packages & target frameworks

- Packages: `Krypton.Ribbon`, `Krypton.Ribbon.Design`
- TFMs verified: full suite Debug build (`net472`, `net48`, `net481`, `net8.0-windows`, `net9.0-windows`, `net10.0-windows`, `net11.0-windows`), 0 warnings, 0 errors.

## Validation

- TestForm demo: `RibbonGroupStackDemo` (registered in `StartScreen.AddButtons()`).
- Standard-Toolkit-Demos: not updated. The existing clone is on `270-feature-request-add-compact-ribbon-demo` with unrelated uncommitted changes, so a new `alpha-4560-ribbon-group-stack` branch was not created.
- Manual steps:
  1. Open Ribbon group stack (#4560).
  2. Triple shows three large buttons. Columns shows six medium buttons in two columns, plus a text box and a cluster.
  3. Adaptive shows four large buttons in one row (`Adaptive size: Large`).
  4. Click Narrow (or shrink the window). Adaptive wraps into a column of three plus a second column. Columns switches to small icons.
- Build: `dotnet build ".\Source\Krypton Components\Krypton Toolkit Suite 2022 - VS2022.sln" -c Debug`

## Screenshots / GIFs


## Changelog

- Entry added to `Documents/Changelog/Changelog.md`:

```markdown
- Implemented [#4560](https://github.com/Krypton-Suite/Standard-Toolkit/issues/4560), Add a further ribbon group item that allows a bigger number of buttons
   - `KryptonRibbonGroupStack` holds any number of ribbon group items inside the three-line group height. `StackLayout.Columns` fills columns of three. 
   - `StackLayout.Adaptive` shows large items in one row and wraps medium and small items into columns of three. 
   - `KryptonRibbonGroupTriple` stays limited to three items.
```

## Build log

## Breaking changes & migration

None. Existing triples and lines are unchanged.

## Developer documentation

- `Documents/Development/KryptonRibbonGroupStack.md`
