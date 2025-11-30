# Krypton Ribbon Backstage API Reference

## KryptonRibbon Class

### Properties

#### BackstagePages
```csharp
[Category("Ribbon")]
[Description("Collection of backstage pages.")]
[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
[Editor(typeof(KryptonRibbonBackstagePageCollectionEditor), typeof(UITypeEditor))]
public KryptonRibbonBackstagePageCollection BackstagePages { get; }
```
Gets the collection of backstage pages available in the ribbon.

#### SelectedBackstagePage
```csharp
[Category("Ribbon")]
[Description("The currently selected backstage page.")]
[DefaultValue(null)]
public KryptonRibbonBackstagePage? SelectedBackstagePage { get; set; }
```
Gets or sets the currently selected backstage page. Setting this property will fire the `BackstagePageSelected` event.

#### UseBackstageForAppButton
```csharp
[Category("Ribbon")]
[Description("Determines if the backstage view is shown when the application button is clicked.")]
[DefaultValue(true)]
public bool UseBackstageForAppButton { get; set; }
```
Gets or sets whether the backstage view should be displayed when the application button is clicked. When `false`, the traditional application menu is shown instead.

### Methods

#### ShowBackstage()
```csharp
public void ShowBackstage()
public void ShowBackstage(bool keyboardActivated)
```
Shows the backstage view as a full-screen overlay.

**Parameters:**
- `keyboardActivated` (optional): Indicates whether the backstage was activated via keyboard input.

**Exceptions:**
- `InvalidOperationException`: Thrown if no parent form is found or if the backstage is already visible.

#### HideBackstage()
```csharp
public void HideBackstage()
```
Hides the currently visible backstage view.

**Remarks:** 
This method fires the `BackstageClosing` event, which can be cancelled. If not cancelled, the backstage is closed and the `BackstageClosed` event is fired.

#### ToggleBackstage()
```csharp
public void ToggleBackstage()
```
Toggles the backstage view visibility. Shows if hidden, hides if visible.

#### ShouldShowBackstage()
```csharp
internal bool ShouldShowBackstage()
```
Determines whether the backstage should be shown based on current settings and page availability.

**Returns:** `true` if backstage should be shown; otherwise, `false`.

#### AddBackstagePage (Text Only)
```csharp
public KryptonRibbonBackstagePage AddBackstagePage(string text)
```
Adds a new backstage page with the specified navigation text and automatically refreshes the navigation if the backstage is visible.

**Parameters:**
- `text` - The text to display on the navigation button

**Returns:** The created KryptonRibbonBackstagePage

#### AddBackstagePage (UserControl)
```csharp
public KryptonRibbonBackstagePage AddBackstagePage(string text, UserControl userControl)
```
Adds a new backstage page with a UserControl and automatically refreshes the navigation if the backstage is visible.

**Parameters:**
- `text` - The text to display on the navigation button
- `userControl` - The UserControl to display as page content

**Returns:** The created KryptonRibbonBackstagePage

#### AddBackstagePage (Any Control)
```csharp
public KryptonRibbonBackstagePage AddBackstagePage(string text, Control control)
```
Adds a new backstage page with any Control and automatically refreshes the navigation if the backstage is visible.

**Parameters:**
- `text` - The text to display on the navigation button
- `control` - The Control to display as page content

**Returns:** The created KryptonRibbonBackstagePage

#### AddBackstagePage (Text Content)
```csharp
public KryptonRibbonBackstagePage AddBackstagePage(string text, string title, string description)
```
Adds a new backstage page with title and description text content and automatically refreshes the navigation if the backstage is visible.

**Parameters:**
- `text` - The text to display on the navigation button
- `title` - The title to display in the content area
- `description` - The description to display in the content area

**Returns:** The created KryptonRibbonBackstagePage

#### RemoveBackstagePage (By Reference)
```csharp
public bool RemoveBackstagePage(KryptonRibbonBackstagePage page)
```
Removes a backstage page and automatically refreshes the navigation if the backstage is visible.

**Parameters:**
- `page` - The page to remove

**Returns:** True if the page was found and removed, false otherwise

#### RemoveBackstagePage (By Text)
```csharp
public bool RemoveBackstagePage(string text)
```
Removes a backstage page by its navigation text and automatically refreshes the navigation if the backstage is visible.

**Parameters:**
- `text` - The text of the page to remove

**Returns:** True if the page was found and removed, false otherwise

#### RefreshBackstageNavigation
```csharp
public void RefreshBackstageNavigation()
```
Manually refreshes the backstage navigation when pages are added or removed dynamically. This is automatically called by the AddBackstagePage and RemoveBackstagePage methods.

### Events

#### BackstageOpening
```csharp
[Category("Ribbon")]
[Description("Occurs when the backstage view is about to be shown.")]
public event EventHandler<CancelEventArgs>? BackstageOpening;
```
Occurs before the backstage view is displayed. Can be cancelled by setting `e.Cancel = true`.

#### BackstageOpened
```csharp
[Category("Ribbon")]
[Description("Occurs when the backstage view has been shown.")]
public event EventHandler? BackstageOpened;
```
Occurs after the backstage view has been successfully displayed.

#### BackstageClosing
```csharp
[Category("Ribbon")]
[Description("Occurs when the backstage view is about to be hidden.")]
public event EventHandler<CancelEventArgs>? BackstageClosing;
```
Occurs before the backstage view is hidden. Can be cancelled by setting `e.Cancel = true`.

#### BackstageClosed
```csharp
[Category("Ribbon")]
[Description("Occurs when the backstage view has been hidden.")]
public event EventHandler? BackstageClosed;
```
Occurs after the backstage view has been successfully hidden.

#### BackstagePageSelected
```csharp
[Category("Ribbon")]
[Description("Occurs when the selected backstage page changes.")]
public event EventHandler<BackstagePageEventArgs>? BackstagePageSelected;
```
Occurs when a different backstage page is selected by the user.

---

## KryptonRibbonBackstagePage Class

### Properties

#### Text
```csharp
[Category("Appearance")]
[Description("The text displayed on the navigation button.")]
[DefaultValue("Page")]
public string Text { get; set; }
```
Gets or sets the text displayed on the navigation button for this page.

#### TextTitle
```csharp
[Category("Appearance")]
[Description("The title text displayed in the content area.")]
[DefaultValue("")]
public string TextTitle { get; set; }
```
Gets or sets the title text displayed at the top of the content area when no custom content panel is provided.

#### TextDescription
```csharp
[Category("Appearance")]
[Description("The description text displayed in the content area.")]
[DefaultValue("")]
public string TextDescription { get; set; }
```
Gets or sets the description text displayed below the title in the content area when no custom content panel is provided.

#### ContentPanel
```csharp
[Category("Behavior")]
[Description("Custom control to display in the content area.")]
[DefaultValue(null)]
public Control? ContentPanel { get; set; }
```
Gets or sets a custom control to display in the content area. When set, this takes precedence over `TextTitle` and `TextDescription`.

#### Visible
```csharp
[Category("Behavior")]
[Description("Determines if the page is visible in the navigation.")]
[DefaultValue(true)]
public bool Visible { get; set; }
```
Gets or sets whether the page is visible in the backstage navigation panel.

#### Enabled
```csharp
[Category("Behavior")]
[Description("Determines if the page can be selected.")]
[DefaultValue(true)]
public bool Enabled { get; set; }
```
Gets or sets whether the page can be selected by the user.

#### Tag
```csharp
[Category("Data")]
[Description("User-defined data associated with the page.")]
[DefaultValue(null)]
public object? Tag { get; set; }
```
Gets or sets user-defined data associated with the page.

### Events

#### PropertyChanged
```csharp
public event PropertyChangedEventHandler? PropertyChanged;
```
Occurs when a property value changes.

---

## KryptonRibbonBackstagePageCollection Class

### Properties

#### Count
```csharp
public int Count { get; }
```
Gets the number of pages in the collection.

#### this[int index]
```csharp
public KryptonRibbonBackstagePage this[int index] { get; }
```
Gets the page at the specified index.

### Methods

#### Add(KryptonRibbonBackstagePage)
```csharp
public void Add(KryptonRibbonBackstagePage page)
```
Adds a page to the collection.

**Parameters:**
- `page`: The page to add.

**Exceptions:**
- `ArgumentNullException`: Thrown if `page` is null.

#### Insert(int, KryptonRibbonBackstagePage)
```csharp
public void Insert(int index, KryptonRibbonBackstagePage page)
```
Inserts a page at the specified index.

**Parameters:**
- `index`: The zero-based index at which to insert the page.
- `page`: The page to insert.

#### Remove(KryptonRibbonBackstagePage)
```csharp
public bool Remove(KryptonRibbonBackstagePage page)
```
Removes a page from the collection.

**Parameters:**
- `page`: The page to remove.

**Returns:** `true` if the page was removed; otherwise, `false`.

#### RemoveAt(int)
```csharp
public void RemoveAt(int index)
```
Removes the page at the specified index.

**Parameters:**
- `index`: The zero-based index of the page to remove.

#### Clear()
```csharp
public void Clear()
```
Removes all pages from the collection.

#### Contains(KryptonRibbonBackstagePage)
```csharp
public bool Contains(KryptonRibbonBackstagePage page)
```
Determines whether the collection contains a specific page.

**Parameters:**
- `page`: The page to locate.

**Returns:** `true` if the page is found; otherwise, `false`.

#### IndexOf(KryptonRibbonBackstagePage)
```csharp
public int IndexOf(KryptonRibbonBackstagePage page)
```
Determines the index of a specific page in the collection.

**Parameters:**
- `page`: The page to locate.

**Returns:** The zero-based index of the page, or -1 if not found.

---

## BackstagePageEventArgs Class

### Properties

#### Page
```csharp
public KryptonRibbonBackstagePage? Page { get; }
```
Gets the backstage page associated with the event.

### Constructor

#### BackstagePageEventArgs(KryptonRibbonBackstagePage?)
```csharp
public BackstagePageEventArgs(KryptonRibbonBackstagePage? page)
```
Initializes a new instance of the BackstagePageEventArgs class.

**Parameters:**
- `page`: The backstage page associated with the event.

---

## Design-Time Support

### KryptonRibbonBackstagePageCollectionEditor
Custom collection editor for managing backstage pages in the Visual Studio designer.

### KryptonRibbonBackstagePageDesigner
Component designer for individual backstage pages.

### KryptonRibbonBackstagePageActionList
Smart tag action list providing quick access to common backstage page properties.

---

## Usage Examples

### Basic Setup
```csharp
// Enable backstage
ribbon.UseBackstageForAppButton = true;

// Add pages
ribbon.BackstagePages.Add(new KryptonRibbonBackstagePage 
{ 
    Text = "Open", 
    TextTitle = "Open Files" 
});

// Handle events
ribbon.BackstagePageSelected += (s, e) => 
{
    MessageBox.Show($"Selected: {e.Page?.Text}");
};
```

### Custom Content
```csharp
var settingsPage = new KryptonRibbonBackstagePage
{
    Text = "Settings",
    ContentPanel = new MySettingsControl()
};
ribbon.BackstagePages.Add(settingsPage);
```

### Programmatic Control
```csharp
// Show backstage programmatically
ribbon.ShowBackstage();

// Select specific page
ribbon.SelectedBackstagePage = ribbon.BackstagePages[0];

// Hide backstage
ribbon.HideBackstage();
```
