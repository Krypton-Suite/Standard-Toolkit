#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

using System.Diagnostics.CodeAnalysis;

namespace Krypton.Toolkit;

/// <summary>
/// Concrete AbstractBaseColorScheme implementation backed by a Color[] whose indices match SchemeBaseColors enumeration order.
/// </summary>
public sealed class ArrayBaseColorScheme : AbstractBaseColorScheme
{
    private readonly Color[] _colors;

    public ArrayBaseColorScheme([DisallowNull] Color[] colors)
    {
        if (colors == null)
        {
            throw new ArgumentNullException(nameof(colors));
        }

        int requiredLength = Enum.GetValues(typeof(SchemeBaseColors)).Length;

        // If the provided array is shorter than required, pad with EMPTY_COLOR for safety.
        if (colors.Length < requiredLength)
        {
            var padded = new Color[requiredLength];
            Array.Copy(colors, padded, colors.Length);

            for (int i = colors.Length; i < requiredLength; i++)
            {
                padded[i] = GlobalStaticValues.EMPTY_COLOR;
            }

            _colors = padded;
        }
        else
        {
            _colors = colors;
        }
    }

    /// <summary>
    /// Gets the underlying color array. Returned for compatibility with legacy APIs.
    /// </summary>
    public Color[] Colors => _colors;

    private Color GetColor(SchemeBaseColors index) => _colors[(int)index];

    public override Color TextLabelControl => GetColor(SchemeBaseColors.TextLabelControl);
    public override Color TextButtonNormal => GetColor(SchemeBaseColors.TextButtonNormal);
    public override Color TextButtonChecked => GetColor(SchemeBaseColors.TextButtonChecked);
    public override Color ButtonNormalBorder => GetColor(SchemeBaseColors.ButtonNormalBorder);
    public override Color ButtonNormalDefaultBorder => GetColor(SchemeBaseColors.ButtonNormalDefaultBorder);
    public override Color ButtonNormalBack1 => GetColor(SchemeBaseColors.ButtonNormalBack1);
    public override Color ButtonNormalBack2 => GetColor(SchemeBaseColors.ButtonNormalBack2);
    public override Color ButtonNormalDefaultBack1 => GetColor(SchemeBaseColors.ButtonNormalDefaultBack1);
    public override Color ButtonNormalDefaultBack2 => GetColor(SchemeBaseColors.ButtonNormalDefaultBack2);
    public override Color ButtonNormalNavigatorBack1 => GetColor(SchemeBaseColors.ButtonNormalNavigatorBack1);
    public override Color ButtonNormalNavigatorBack2 => GetColor(SchemeBaseColors.ButtonNormalNavigatorBack2);
    public override Color PanelClient => GetColor(SchemeBaseColors.PanelClient);
    public override Color PanelAlternative => GetColor(SchemeBaseColors.PanelAlternative);
    public override Color ControlBorder => GetColor(SchemeBaseColors.ControlBorder);
    public override Color SeparatorHighBorder1 => GetColor(SchemeBaseColors.SeparatorHighBorder1);
    public override Color SeparatorHighBorder2 => GetColor(SchemeBaseColors.SeparatorHighBorder2);
    public override Color HeaderPrimaryBack1 => GetColor(SchemeBaseColors.HeaderPrimaryBack1);
    public override Color HeaderPrimaryBack2 => GetColor(SchemeBaseColors.HeaderPrimaryBack2);
    public override Color HeaderSecondaryBack1 => GetColor(SchemeBaseColors.HeaderSecondaryBack1);
    public override Color HeaderSecondaryBack2 => GetColor(SchemeBaseColors.HeaderSecondaryBack2);
    public override Color HeaderText => GetColor(SchemeBaseColors.HeaderText);
    public override Color StatusStripText => GetColor(SchemeBaseColors.StatusStripText);
    public override Color ButtonBorder => GetColor(SchemeBaseColors.ButtonBorder);
    public override Color SeparatorLight => GetColor(SchemeBaseColors.SeparatorLight);
    public override Color SeparatorDark => GetColor(SchemeBaseColors.SeparatorDark);
    public override Color GripLight => GetColor(SchemeBaseColors.GripLight);
    public override Color GripDark => GetColor(SchemeBaseColors.GripDark);
    public override Color ToolStripBack => GetColor(SchemeBaseColors.ToolStripBack);
    public override Color StatusStripLight => GetColor(SchemeBaseColors.StatusStripLight);
    public override Color StatusStripDark => GetColor(SchemeBaseColors.StatusStripDark);
    public override Color ImageMargin => GetColor(SchemeBaseColors.ImageMargin);
    public override Color ToolStripBegin => GetColor(SchemeBaseColors.ToolStripBegin);
    public override Color ToolStripMiddle => GetColor(SchemeBaseColors.ToolStripMiddle);
    public override Color ToolStripEnd => GetColor(SchemeBaseColors.ToolStripEnd);
    public override Color OverflowBegin => GetColor(SchemeBaseColors.OverflowBegin);
    public override Color OverflowMiddle => GetColor(SchemeBaseColors.OverflowMiddle);
    public override Color OverflowEnd => GetColor(SchemeBaseColors.OverflowEnd);
    public override Color ToolStripBorder => GetColor(SchemeBaseColors.ToolStripBorder);
    public override Color FormBorderActive => GetColor(SchemeBaseColors.FormBorderActive);
    public override Color FormBorderInactive => GetColor(SchemeBaseColors.FormBorderInactive);
    public override Color FormBorderActiveLight => GetColor(SchemeBaseColors.FormBorderActiveLight);
    public override Color FormBorderActiveDark => GetColor(SchemeBaseColors.FormBorderActiveDark);
    public override Color FormBorderInactiveLight => GetColor(SchemeBaseColors.FormBorderInactiveLight);
    public override Color FormBorderInactiveDark => GetColor(SchemeBaseColors.FormBorderInactiveDark);
    public override Color FormBorderHeaderActive => GetColor(SchemeBaseColors.FormBorderHeaderActive);
    public override Color FormBorderHeaderInactive => GetColor(SchemeBaseColors.FormBorderHeaderInactive);
    public override Color FormBorderHeaderActive1 => GetColor(SchemeBaseColors.FormBorderHeaderActive1);
    public override Color FormBorderHeaderActive2 => GetColor(SchemeBaseColors.FormBorderHeaderActive2);
    public override Color FormBorderHeaderInactive1 => GetColor(SchemeBaseColors.FormBorderHeaderInactive1);
    public override Color FormBorderHeaderInactive2 => GetColor(SchemeBaseColors.FormBorderHeaderInactive2);
    public override Color FormHeaderShortActive => GetColor(SchemeBaseColors.FormHeaderShortActive);
    public override Color FormHeaderShortInactive => GetColor(SchemeBaseColors.FormHeaderShortInactive);
    public override Color FormHeaderLongActive => GetColor(SchemeBaseColors.FormHeaderLongActive);
    public override Color FormHeaderLongInactive => GetColor(SchemeBaseColors.FormHeaderLongInactive);
    public override Color FormButtonBorderTrack => GetColor(SchemeBaseColors.FormButtonBorderTrack);
    public override Color FormButtonBack1Track => GetColor(SchemeBaseColors.FormButtonBack1Track);
    public override Color FormButtonBack2Track => GetColor(SchemeBaseColors.FormButtonBack2Track);
    public override Color FormButtonBorderPressed => GetColor(SchemeBaseColors.FormButtonBorderPressed);
    public override Color FormButtonBack1Pressed => GetColor(SchemeBaseColors.FormButtonBack1Pressed);
    public override Color FormButtonBack2Pressed => GetColor(SchemeBaseColors.FormButtonBack2Pressed);
    public override Color TextButtonFormNormal => GetColor(SchemeBaseColors.TextButtonFormNormal);
    public override Color TextButtonFormTracking => GetColor(SchemeBaseColors.TextButtonFormTracking);
    public override Color TextButtonFormPressed => GetColor(SchemeBaseColors.TextButtonFormPressed);
    public override Color LinkNotVisitedOverrideControl => GetColor(SchemeBaseColors.LinkNotVisitedOverrideControl);
    public override Color LinkVisitedOverrideControl => GetColor(SchemeBaseColors.LinkVisitedOverrideControl);
    public override Color LinkPressedOverrideControl => GetColor(SchemeBaseColors.LinkPressedOverrideControl);
    public override Color LinkNotVisitedOverridePanel => GetColor(SchemeBaseColors.LinkNotVisitedOverridePanel);
    public override Color LinkVisitedOverridePanel => GetColor(SchemeBaseColors.LinkVisitedOverridePanel);
    public override Color LinkPressedOverridePanel => GetColor(SchemeBaseColors.LinkPressedOverridePanel);
    public override Color TextLabelPanel => GetColor(SchemeBaseColors.TextLabelPanel);
    public override Color RibbonTabTextNormal => GetColor(SchemeBaseColors.RibbonTabTextNormal);
    public override Color RibbonTabTextChecked => GetColor(SchemeBaseColors.RibbonTabTextChecked);
    public override Color RibbonTabSelected1 => GetColor(SchemeBaseColors.RibbonTabSelected1);
    public override Color RibbonTabSelected2 => GetColor(SchemeBaseColors.RibbonTabSelected2);
    public override Color RibbonTabSelected3 => GetColor(SchemeBaseColors.RibbonTabSelected3);
    public override Color RibbonTabSelected4 => GetColor(SchemeBaseColors.RibbonTabSelected4);
    public override Color RibbonTabSelected5 => GetColor(SchemeBaseColors.RibbonTabSelected5);
    public override Color RibbonTabTracking1 => GetColor(SchemeBaseColors.RibbonTabTracking1);
    public override Color RibbonTabTracking2 => GetColor(SchemeBaseColors.RibbonTabTracking2);
    public override Color RibbonTabHighlight1 => GetColor(SchemeBaseColors.RibbonTabHighlight1);
    public override Color RibbonTabHighlight2 => GetColor(SchemeBaseColors.RibbonTabHighlight2);
    public override Color RibbonTabHighlight3 => GetColor(SchemeBaseColors.RibbonTabHighlight3);
    public override Color RibbonTabHighlight4 => GetColor(SchemeBaseColors.RibbonTabHighlight4);
    public override Color RibbonTabHighlight5 => GetColor(SchemeBaseColors.RibbonTabHighlight5);
    public override Color RibbonTabSeparatorColor => GetColor(SchemeBaseColors.RibbonTabSeparatorColor);
    public override Color RibbonGroupsArea1 => GetColor(SchemeBaseColors.RibbonGroupsArea1);
    public override Color RibbonGroupsArea2 => GetColor(SchemeBaseColors.RibbonGroupsArea2);
    public override Color RibbonGroupsArea3 => GetColor(SchemeBaseColors.RibbonGroupsArea3);
    public override Color RibbonGroupsArea4 => GetColor(SchemeBaseColors.RibbonGroupsArea4);
    public override Color RibbonGroupsArea5 => GetColor(SchemeBaseColors.RibbonGroupsArea5);
    public override Color RibbonGroupBorder1 => GetColor(SchemeBaseColors.RibbonGroupBorder1);
    public override Color RibbonGroupBorder2 => GetColor(SchemeBaseColors.RibbonGroupBorder2);
    public override Color RibbonGroupTitle1 => GetColor(SchemeBaseColors.RibbonGroupTitle1);
    public override Color RibbonGroupTitle2 => GetColor(SchemeBaseColors.RibbonGroupTitle2);
    public override Color RibbonGroupBorderContext1 => GetColor(SchemeBaseColors.RibbonGroupBorderContext1);
    public override Color RibbonGroupBorderContext2 => GetColor(SchemeBaseColors.RibbonGroupBorderContext2);
    public override Color RibbonGroupTitleContext1 => GetColor(SchemeBaseColors.RibbonGroupTitleContext1);
    public override Color RibbonGroupTitleContext2 => GetColor(SchemeBaseColors.RibbonGroupTitleContext2);
    public override Color RibbonGroupDialogDark => GetColor(SchemeBaseColors.RibbonGroupDialogDark);
    public override Color RibbonGroupDialogLight => GetColor(SchemeBaseColors.RibbonGroupDialogLight);
    public override Color RibbonGroupTitleTracking1 => GetColor(SchemeBaseColors.RibbonGroupTitleTracking1);
    public override Color RibbonGroupTitleTracking2 => GetColor(SchemeBaseColors.RibbonGroupTitleTracking2);
    public override Color RibbonMinimizeBarDark => GetColor(SchemeBaseColors.RibbonMinimizeBarDark);
    public override Color RibbonMinimizeBarLight => GetColor(SchemeBaseColors.RibbonMinimizeBarLight);
    public override Color RibbonGroupCollapsedBorder1 => GetColor(SchemeBaseColors.RibbonGroupCollapsedBorder1);
    public override Color RibbonGroupCollapsedBorder2 => GetColor(SchemeBaseColors.RibbonGroupCollapsedBorder2);
    public override Color RibbonGroupCollapsedBorder3 => GetColor(SchemeBaseColors.RibbonGroupCollapsedBorder3);
    public override Color RibbonGroupCollapsedBorder4 => GetColor(SchemeBaseColors.RibbonGroupCollapsedBorder4);
    public override Color RibbonGroupCollapsedBack1 => GetColor(SchemeBaseColors.RibbonGroupCollapsedBack1);
    public override Color RibbonGroupCollapsedBack2 => GetColor(SchemeBaseColors.RibbonGroupCollapsedBack2);
    public override Color RibbonGroupCollapsedBack3 => GetColor(SchemeBaseColors.RibbonGroupCollapsedBack3);
    public override Color RibbonGroupCollapsedBack4 => GetColor(SchemeBaseColors.RibbonGroupCollapsedBack4);
    public override Color RibbonGroupCollapsedBorderT1 => GetColor(SchemeBaseColors.RibbonGroupCollapsedBorderT1);
    public override Color RibbonGroupCollapsedBorderT2 => GetColor(SchemeBaseColors.RibbonGroupCollapsedBorderT2);
    public override Color RibbonGroupCollapsedBorderT3 => GetColor(SchemeBaseColors.RibbonGroupCollapsedBorderT3);
    public override Color RibbonGroupCollapsedBorderT4 => GetColor(SchemeBaseColors.RibbonGroupCollapsedBorderT4);
    public override Color RibbonGroupCollapsedBackT1 => GetColor(SchemeBaseColors.RibbonGroupCollapsedBackT1);
    public override Color RibbonGroupCollapsedBackT2 => GetColor(SchemeBaseColors.RibbonGroupCollapsedBackT2);
    public override Color RibbonGroupCollapsedBackT3 => GetColor(SchemeBaseColors.RibbonGroupCollapsedBackT3);
    public override Color RibbonGroupCollapsedBackT4 => GetColor(SchemeBaseColors.RibbonGroupCollapsedBackT4);
    public override Color RibbonGroupFrameBorder1 => GetColor(SchemeBaseColors.RibbonGroupFrameBorder1);
    public override Color RibbonGroupFrameBorder2 => GetColor(SchemeBaseColors.RibbonGroupFrameBorder2);
    public override Color RibbonGroupFrameInside1 => GetColor(SchemeBaseColors.RibbonGroupFrameInside1);
    public override Color RibbonGroupFrameInside2 => GetColor(SchemeBaseColors.RibbonGroupFrameInside2);
    public override Color RibbonGroupFrameInside3 => GetColor(SchemeBaseColors.RibbonGroupFrameInside3);
    public override Color RibbonGroupFrameInside4 => GetColor(SchemeBaseColors.RibbonGroupFrameInside4);
    public override Color RibbonGroupCollapsedText => GetColor(SchemeBaseColors.RibbonGroupCollapsedText);
    public override Color RibbonGroupButtonText => GetColor(SchemeBaseColors.RibbonGroupButtonText);
    public override Color AlternatePressedBack1 => GetColor(SchemeBaseColors.AlternatePressedBack1);
    public override Color AlternatePressedBack2 => GetColor(SchemeBaseColors.AlternatePressedBack2);
    public override Color AlternatePressedBorder1 => GetColor(SchemeBaseColors.AlternatePressedBorder1);
    public override Color AlternatePressedBorder2 => GetColor(SchemeBaseColors.AlternatePressedBorder2);
    public override Color FormButtonBack1Checked => GetColor(SchemeBaseColors.FormButtonBack1Checked);
    public override Color FormButtonBack2Checked => GetColor(SchemeBaseColors.FormButtonBack2Checked);
    public override Color FormButtonBorderCheck => GetColor(SchemeBaseColors.FormButtonBorderCheck);
    public override Color FormButtonBack1CheckTrack => GetColor(SchemeBaseColors.FormButtonBack1CheckTrack);
    public override Color FormButtonBack2CheckTrack => GetColor(SchemeBaseColors.FormButtonBack2CheckTrack);
    public override Color RibbonQATMini1 => GetColor(SchemeBaseColors.RibbonQATMini1);
    public override Color RibbonQATMini2 => GetColor(SchemeBaseColors.RibbonQATMini2);
    public override Color RibbonQATMini3 => GetColor(SchemeBaseColors.RibbonQATMini3);
    public override Color RibbonQATMini4 => GetColor(SchemeBaseColors.RibbonQATMini4);
    public override Color RibbonQATMini5 => GetColor(SchemeBaseColors.RibbonQATMini5);
    public override Color RibbonQATMini1I => GetColor(SchemeBaseColors.RibbonQATMini1I);
    public override Color RibbonQATMini2I => GetColor(SchemeBaseColors.RibbonQATMini2I);
    public override Color RibbonQATMini3I => GetColor(SchemeBaseColors.RibbonQATMini3I);
    public override Color RibbonQATMini4I => GetColor(SchemeBaseColors.RibbonQATMini4I);
    public override Color RibbonQATMini5I => GetColor(SchemeBaseColors.RibbonQATMini5I);
    public override Color RibbonQATFullbar1 => GetColor(SchemeBaseColors.RibbonQATFullbar1);
    public override Color RibbonQATFullbar2 => GetColor(SchemeBaseColors.RibbonQATFullbar2);
    public override Color RibbonQATFullbar3 => GetColor(SchemeBaseColors.RibbonQATFullbar3);
    public override Color RibbonQATButtonDark => GetColor(SchemeBaseColors.RibbonQATButtonDark);
    public override Color RibbonQATButtonLight => GetColor(SchemeBaseColors.RibbonQATButtonLight);
    public override Color RibbonQATOverflow1 => GetColor(SchemeBaseColors.RibbonQATOverflow1);
    public override Color RibbonQATOverflow2 => GetColor(SchemeBaseColors.RibbonQATOverflow2);
    public override Color RibbonGroupSeparatorDark => GetColor(SchemeBaseColors.RibbonGroupSeparatorDark);
    public override Color RibbonGroupSeparatorLight => GetColor(SchemeBaseColors.RibbonGroupSeparatorLight);
    public override Color ButtonClusterButtonBack1 => GetColor(SchemeBaseColors.ButtonClusterButtonBack1);
    public override Color ButtonClusterButtonBack2 => GetColor(SchemeBaseColors.ButtonClusterButtonBack2);
    public override Color ButtonClusterButtonBorder1 => GetColor(SchemeBaseColors.ButtonClusterButtonBorder1);
    public override Color ButtonClusterButtonBorder2 => GetColor(SchemeBaseColors.ButtonClusterButtonBorder2);
    public override Color NavigatorMiniBackColor => GetColor(SchemeBaseColors.NavigatorMiniBackColor);
    public override Color GridListNormal1 => GetColor(SchemeBaseColors.GridListNormal1);
    public override Color GridListNormal2 => GetColor(SchemeBaseColors.GridListNormal2);
    public override Color GridListPressed1 => GetColor(SchemeBaseColors.GridListPressed1);
    public override Color GridListPressed2 => GetColor(SchemeBaseColors.GridListPressed2);
    public override Color GridListSelected => GetColor(SchemeBaseColors.GridListSelected);
    public override Color GridSheetColNormal1 => GetColor(SchemeBaseColors.GridSheetColNormal1);
    public override Color GridSheetColNormal2 => GetColor(SchemeBaseColors.GridSheetColNormal2);
    public override Color GridSheetColPressed1 => GetColor(SchemeBaseColors.GridSheetColPressed1);
    public override Color GridSheetColPressed2 => GetColor(SchemeBaseColors.GridSheetColPressed2);
    public override Color GridSheetColSelected1 => GetColor(SchemeBaseColors.GridSheetColSelected1);
    public override Color GridSheetColSelected2 => GetColor(SchemeBaseColors.GridSheetColSelected2);
    public override Color GridSheetRowNormal => GetColor(SchemeBaseColors.GridSheetRowNormal);
    public override Color GridSheetRowPressed => GetColor(SchemeBaseColors.GridSheetRowPressed);
    public override Color GridSheetRowSelected => GetColor(SchemeBaseColors.GridSheetRowSelected);
    public override Color GridDataCellBorder => GetColor(SchemeBaseColors.GridDataCellBorder);
    public override Color GridDataCellSelected => GetColor(SchemeBaseColors.GridDataCellSelected);
    public override Color InputControlTextNormal => GetColor(SchemeBaseColors.InputControlTextNormal);
    public override Color InputControlTextDisabled => GetColor(SchemeBaseColors.InputControlTextDisabled);
    public override Color InputControlBorderNormal => GetColor(SchemeBaseColors.InputControlBorderNormal);
    public override Color InputControlBorderDisabled => GetColor(SchemeBaseColors.InputControlBorderDisabled);
    public override Color InputControlBackNormal => GetColor(SchemeBaseColors.InputControlBackNormal);
    public override Color InputControlBackDisabled => GetColor(SchemeBaseColors.InputControlBackDisabled);
    public override Color InputControlBackInactive => GetColor(SchemeBaseColors.InputControlBackInactive);
    public override Color InputDropDownNormal1 => GetColor(SchemeBaseColors.InputDropDownNormal1);
    public override Color InputDropDownNormal2 => GetColor(SchemeBaseColors.InputDropDownNormal2);
    public override Color InputDropDownDisabled1 => GetColor(SchemeBaseColors.InputDropDownDisabled1);
    public override Color InputDropDownDisabled2 => GetColor(SchemeBaseColors.InputDropDownDisabled2);
    public override Color ContextMenuHeadingBack => GetColor(SchemeBaseColors.ContextMenuHeadingBack);
    public override Color ContextMenuHeadingText => GetColor(SchemeBaseColors.ContextMenuHeadingText);
    public override Color ContextMenuImageColumn => GetColor(SchemeBaseColors.ContextMenuImageColumn);
    public override Color AppButtonBack1 => GetColor(SchemeBaseColors.AppButtonBack1);
    public override Color AppButtonBack2 => GetColor(SchemeBaseColors.AppButtonBack2);
    public override Color AppButtonBorder => GetColor(SchemeBaseColors.AppButtonBorder);
    public override Color AppButtonOuter1 => GetColor(SchemeBaseColors.AppButtonOuter1);
    public override Color AppButtonOuter2 => GetColor(SchemeBaseColors.AppButtonOuter2);
    public override Color AppButtonOuter3 => GetColor(SchemeBaseColors.AppButtonOuter3);
    public override Color AppButtonInner1 => GetColor(SchemeBaseColors.AppButtonInner1);
    public override Color AppButtonInner2 => GetColor(SchemeBaseColors.AppButtonInner2);
    public override Color AppButtonMenuDocsBack => GetColor(SchemeBaseColors.AppButtonMenuDocsBack);
    public override Color AppButtonMenuDocsText => GetColor(SchemeBaseColors.AppButtonMenuDocsText);
    public override Color SeparatorHighInternalBorder1 => GetColor(SchemeBaseColors.SeparatorHighInternalBorder1);
    public override Color SeparatorHighInternalBorder2 => GetColor(SchemeBaseColors.SeparatorHighInternalBorder2);
    public override Color RibbonGalleryBorder => GetColor(SchemeBaseColors.RibbonGalleryBorder);
    public override Color RibbonGalleryBackNormal => GetColor(SchemeBaseColors.RibbonGalleryBackNormal);
    public override Color RibbonGalleryBackTracking => GetColor(SchemeBaseColors.RibbonGalleryBackTracking);
    public override Color RibbonGalleryBack1 => GetColor(SchemeBaseColors.RibbonGalleryBack1);
    public override Color RibbonGalleryBack2 => GetColor(SchemeBaseColors.RibbonGalleryBack2);
    public override Color RibbonTabTracking3 => GetColor(SchemeBaseColors.RibbonTabTracking3);
    public override Color RibbonTabTracking4 => GetColor(SchemeBaseColors.RibbonTabTracking4);
    public override Color RibbonGroupBorder3 => GetColor(SchemeBaseColors.RibbonGroupBorder3);
    public override Color RibbonGroupBorder4 => GetColor(SchemeBaseColors.RibbonGroupBorder4);
    public override Color RibbonGroupBorder5 => GetColor(SchemeBaseColors.RibbonGroupBorder5);
    public override Color RibbonGroupTitleText => GetColor(SchemeBaseColors.RibbonGroupTitleText);
    public override Color RibbonDropArrowLight => GetColor(SchemeBaseColors.RibbonDropArrowLight);
    public override Color RibbonDropArrowDark => GetColor(SchemeBaseColors.RibbonDropArrowDark);
    public override Color HeaderDockInactiveBack1 => GetColor(SchemeBaseColors.HeaderDockInactiveBack1);
    public override Color HeaderDockInactiveBack2 => GetColor(SchemeBaseColors.HeaderDockInactiveBack2);
    public override Color ButtonNavigatorBorder => GetColor(SchemeBaseColors.ButtonNavigatorBorder);
    public override Color ButtonNavigatorText => GetColor(SchemeBaseColors.ButtonNavigatorText);
    public override Color ButtonNavigatorTrack1 => GetColor(SchemeBaseColors.ButtonNavigatorTrack1);
    public override Color ButtonNavigatorTrack2 => GetColor(SchemeBaseColors.ButtonNavigatorTrack2);
    public override Color ButtonNavigatorPressed1 => GetColor(SchemeBaseColors.ButtonNavigatorPressed1);
    public override Color ButtonNavigatorPressed2 => GetColor(SchemeBaseColors.ButtonNavigatorPressed2);
    public override Color ButtonNavigatorChecked1 => GetColor(SchemeBaseColors.ButtonNavigatorChecked1);
    public override Color ButtonNavigatorChecked2 => GetColor(SchemeBaseColors.ButtonNavigatorChecked2);
    public override Color ToolTipBottom => GetColor(SchemeBaseColors.ToolTipBottom);
    public override Color MenuItemText => GetColor(SchemeBaseColors.MenuItemText);
    public override Color MenuMarginGradientStart => GetColor(SchemeBaseColors.MenuMarginGradientStart);
    public override Color MenuMarginGradientMiddle => GetColor(SchemeBaseColors.MenuMarginGradientMiddle);
    public override Color MenuMarginGradientEnd => GetColor(SchemeBaseColors.MenuMarginGradientEnd);
    public override Color DisabledMenuItemText => GetColor(SchemeBaseColors.DisabledMenuItemText);
    public override Color MenuStripText => GetColor(SchemeBaseColors.MenuStripText);
}
