using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Windows.Forms.Design;
using Krypton.Toolkit;

namespace Net8DesignerTest;

/// <summary>
/// Test control to isolate .NET 8+ designer issues.
/// </summary>
[Designer(typeof(TestControlDesigner))]
[ToolboxItem(true)]
public class TestControl : Control
{
    public TestControl()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer, true);
        Size = new Size(100, 30);
        BackColor = Color.LightBlue;
        Text = "Test Control";
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        
        using var brush = new SolidBrush(ForeColor);
        var rect = ClientRectangle;
        rect.Inflate(-2, -2);
        
        e.Graphics.DrawString(Text, Font, brush, rect);
    }
}

/// <summary>
/// Designer for TestControl to troubleshoot .NET 8+ issues.
/// </summary>
public class TestControlDesigner : ControlDesigner
{
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            Debug.WriteLine("[TestControlDesigner] ActionLists property called");
            
            var actionLists = new DesignerActionListCollection();
            actionLists.Add(new TestControlActionList(this));
            
            Debug.WriteLine($"[TestControlDesigner] Returning {actionLists.Count} action lists");
            return actionLists;
        }
    }

    public override void Initialize(IComponent component)
    {
        Debug.WriteLine($"[TestControlDesigner] Initialize called with {component?.GetType().Name}");
        base.Initialize(component);
    }
}

/// <summary>
/// Action list for TestControl.
/// </summary>
public class TestControlActionList : DesignerActionList
{
    private readonly TestControl _control;

    public TestControlActionList(ComponentDesigner designer) : base(designer.Component)
    {
        Debug.WriteLine("[TestControlActionList] Constructor called");
        _control = (TestControl)designer.Component;
    }

    public string Text
    {
        get 
        {
            Debug.WriteLine($"[TestControlActionList] Text getter: {_control.Text}");
            return _control.Text;
        }
        set 
        {
            Debug.WriteLine($"[TestControlActionList] Text setter: {value}");
            _control.Text = value;
            _control.Invalidate();
        }
    }

    public Color BackColor
    {
        get 
        {
            Debug.WriteLine($"[TestControlActionList] BackColor getter: {_control.BackColor}");
            return _control.BackColor;
        }
        set 
        {
            Debug.WriteLine($"[TestControlActionList] BackColor setter: {value}");
            _control.BackColor = value;
        }
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
        Debug.WriteLine("[TestControlActionList] GetSortedActionItems called");
        
        var items = new DesignerActionItemCollection();
        
        items.Add(new DesignerActionHeaderItem("Appearance"));
        items.Add(new DesignerActionPropertyItem(nameof(Text), "Text", "Appearance", "Control text"));
        items.Add(new DesignerActionPropertyItem(nameof(BackColor), "Back Color", "Appearance", "Background color"));
        
        Debug.WriteLine($"[TestControlActionList] Returning {items.Count} action items");
        return items;
    }
}
