namespace Krypton.Toolkit;

/// <summary>Access Krypton input box settings.</summary>
[Category(@"Code")]
[Description(@"Access Krypton input box settings.")]
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonInputBoxValues : Storage
{
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => throw new NotImplementedException();
}