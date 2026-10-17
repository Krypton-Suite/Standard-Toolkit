#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// One control hosted in the <see cref="KryptonForm"/> caption, with its own edge.
/// </summary>
/// <remarks>
/// Stored on <see cref="KryptonFormCaptionItems"/>. Applied at runtime through
/// <see cref="KryptonForm.AddCaptionControl"/>.
/// </remarks>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonFormCaptionItem
{
    #region Instance Fields

    private Control? _control;
    private FormCaptionTextBoxEdge _edge = FormCaptionTextBoxEdge.Right;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonFormCaptionItem"/> class.
    /// </summary>
    public KryptonFormCaptionItem()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonFormCaptionItem"/> class.
    /// </summary>
    /// <param name="control">Control to place in the caption.</param>
    /// <param name="edge">Caption edge for this control.</param>
    public KryptonFormCaptionItem(Control control, FormCaptionTextBoxEdge edge)
    {
        _control = control;
        _edge = edge;
    }

    /// <summary>
    /// Returns a short description for the property grid.
    /// </summary>
    /// <returns>Control name and edge.</returns>
    public override string ToString()
    {
        var name = _control == null
            ? @"(none)"
            : string.IsNullOrEmpty(_control.Name) ? _control.GetType().Name : _control.Name;
        return $"{name}, {_edge}";
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the control shown in the caption.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Control placed in the form caption.")]
    [DefaultValue(null)]
    [NotifyParentProperty(true)]
    public Control? Control
    {
        get => _control;

        set
        {
            if (ReferenceEquals(_control, value))
            {
                return;
            }

            _control = value;
            Owner?.NotifyItemChanged(this);
        }
    }

    /// <summary>
    /// Gets or sets which caption edge hosts <see cref="Control"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Caption edge for this control. Other caption items keep their own edges.")]
    [DefaultValue(FormCaptionTextBoxEdge.Right)]
    [NotifyParentProperty(true)]
    public FormCaptionTextBoxEdge Edge
    {
        get => _edge;

        set
        {
            if (_edge == value)
            {
                return;
            }

            _edge = value;
            Owner?.NotifyItemChanged(this);
        }
    }

    #endregion

    #region Internal

    /// <summary>
    /// Owning component that applies this item to the form.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal KryptonFormCaptionItems? Owner { get; set; }

    #endregion
}
