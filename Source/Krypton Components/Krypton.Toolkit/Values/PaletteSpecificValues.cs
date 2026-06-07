using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class PaletteSpecificValues : Storage
{
    #region Instance Fields

    private readonly VisualForm _owner;

    private bool _useWindowsControlBoxLayout;

    #endregion

    #region Idenity

    public PaletteSpecificValues(VisualForm owner)
    {
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));

        Reset();
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets a value indicating whether [use windows control box layout].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [use windows control box layout]; otherwise, <c>false</c>.
    /// </value>
    [Description("Should the control box buttons be laid out in the same way as Windows does it.")]
    [Category("Visuals")]
    [DefaultValue(true)]
    public bool UseWindowsControlBoxLayout
    {
        get => _useWindowsControlBoxLayout;
        set
        {
            if (_useWindowsControlBoxLayout != value)
            {
                _useWindowsControlBoxLayout = value;
            }
        }
    }

    private bool ShouldSerializeUseWindowsControlBoxLayout() => !_useWindowsControlBoxLayout;

    public void ResetUseWindowsControlBoxLayout() => UseWindowsControlBoxLayout = true;

    #endregion

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => UseWindowsControlBoxLayout.Equals(true);

    #region Reset

    public void Reset()
    {
        ResetUseWindowsControlBoxLayout();
    }

    #endregion
}