﻿#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2024 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit
{

    internal class KryptonDataGridViewUtilities
    {
        internal static class TextFormatFlagsCellStyleAlignments
        {
            // Calculate these once for use in ComputeTextFormatFlagsForCellStyleAlignment()
            private const TextFormatFlags baseMask = TextFormatFlags.NoPrefix | TextFormatFlags.PreserveGraphicsClipping;

            internal const TextFormatFlags TopLeft_RightToLeft = baseMask | TextFormatFlags.Top | TextFormatFlags.Right | TextFormatFlags.RightToLeft;
            internal const TextFormatFlags TopLeft_LeftToRight = baseMask | TextFormatFlags.Top | TextFormatFlags.Left;
            internal const TextFormatFlags TopCenter = baseMask | TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
            internal const TextFormatFlags TopRight_RightToLeft = baseMask | TextFormatFlags.Top | TextFormatFlags.Left | TextFormatFlags.RightToLeft;
            internal const TextFormatFlags TopRight_LeftToRight = baseMask | TextFormatFlags.Top | TextFormatFlags.Right;

            internal const TextFormatFlags MiddleLeft_RightToLeft = baseMask | TextFormatFlags.VerticalCenter | TextFormatFlags.Right | TextFormatFlags.RightToLeft;
            internal const TextFormatFlags MiddleLeft_LeftToRight = baseMask | TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
            internal const TextFormatFlags MiddleCenter = baseMask | TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
            internal const TextFormatFlags MiddleRight_RightToLeft = baseMask | TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.RightToLeft;
            internal const TextFormatFlags MiddleRight_LeftToRight = baseMask | TextFormatFlags.VerticalCenter | TextFormatFlags.Right;

            internal const TextFormatFlags BottomLeft_RightToLeft = baseMask | TextFormatFlags.Bottom | TextFormatFlags.Right | TextFormatFlags.RightToLeft;
            internal const TextFormatFlags BottomLeft_LeftToRight = baseMask | TextFormatFlags.Bottom | TextFormatFlags.Left;
            internal const TextFormatFlags BottomCenter = baseMask | TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
            internal const TextFormatFlags BottomRight_RightToLeft = baseMask | TextFormatFlags.Bottom | TextFormatFlags.Left | TextFormatFlags.RightToLeft;
            internal const TextFormatFlags BottomRight_Lefttoright = baseMask | TextFormatFlags.Bottom | TextFormatFlags.Right;

            internal const TextFormatFlags DefaultAlignment = baseMask | TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
        }

        internal static TextFormatFlags ComputeTextFormatFlagsForCellStyleAlignment(
            bool rightToLeft,
            DataGridViewContentAlignment alignment,
            DataGridViewTriState wrapMode)
        {
            // This routine has been copied from the dotnet winforms project and slightly rewritten to reduce computing masks
            // Licensed to the .NET Foundation under one or more agreements.
            // The .NET Foundation licenses this file to you under the MIT license.
            TextFormatFlags tff = alignment switch
            {
                DataGridViewContentAlignment.TopLeft when rightToLeft => TextFormatFlagsCellStyleAlignments.TopLeft_RightToLeft,
                DataGridViewContentAlignment.TopLeft when !rightToLeft => TextFormatFlagsCellStyleAlignments.TopLeft_LeftToRight,
                DataGridViewContentAlignment.TopCenter => TextFormatFlagsCellStyleAlignments.TopCenter,
                DataGridViewContentAlignment.TopRight when rightToLeft => TextFormatFlagsCellStyleAlignments.TopRight_RightToLeft,
                DataGridViewContentAlignment.TopRight when !rightToLeft => TextFormatFlagsCellStyleAlignments.TopRight_LeftToRight,

                DataGridViewContentAlignment.MiddleLeft when rightToLeft => TextFormatFlagsCellStyleAlignments.MiddleLeft_RightToLeft,
                DataGridViewContentAlignment.MiddleLeft when !rightToLeft => TextFormatFlagsCellStyleAlignments.MiddleLeft_LeftToRight,
                DataGridViewContentAlignment.MiddleCenter => TextFormatFlagsCellStyleAlignments.MiddleCenter,
                DataGridViewContentAlignment.MiddleRight when rightToLeft => TextFormatFlagsCellStyleAlignments.MiddleRight_RightToLeft,
                DataGridViewContentAlignment.MiddleRight when !rightToLeft => TextFormatFlagsCellStyleAlignments.MiddleRight_LeftToRight,

                DataGridViewContentAlignment.BottomLeft when rightToLeft => TextFormatFlagsCellStyleAlignments.BottomLeft_RightToLeft,
                DataGridViewContentAlignment.BottomLeft when !rightToLeft => TextFormatFlagsCellStyleAlignments.BottomLeft_LeftToRight,
                DataGridViewContentAlignment.BottomCenter => TextFormatFlagsCellStyleAlignments.BottomCenter,
                DataGridViewContentAlignment.BottomRight when rightToLeft => TextFormatFlagsCellStyleAlignments.BottomRight_RightToLeft,
                DataGridViewContentAlignment.BottomRight when !rightToLeft => TextFormatFlagsCellStyleAlignments.BottomRight_Lefttoright,
                _ => TextFormatFlagsCellStyleAlignments.DefaultAlignment
            };

            return  tff | (wrapMode == DataGridViewTriState.False
                ? TextFormatFlags.SingleLine
                : TextFormatFlags.WordBreak);

        }
    }
}
