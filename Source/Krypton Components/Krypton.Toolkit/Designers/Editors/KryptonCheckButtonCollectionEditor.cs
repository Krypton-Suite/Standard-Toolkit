﻿// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace Krypton.Toolkit
{
    internal class KryptonCheckButtonCollectionEditor : UITypeEditor
    {
        /// <summary>
        /// Gets the editor style used by the EditValue method.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
        /// <returns>A UITypeEditorEditStyle enumeration value that indicates the style of editor.</returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) =>
            context?.Instance != null ? UITypeEditorEditStyle.Modal : base.GetEditStyle(context);

        /// <summary>
        /// Edits the specified object's value using the editor style indicated by GetEditStyle.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
        /// <param name="provider">An IServiceProvider that this editor can use to obtain services.</param>
        /// <param name="value">The object to edit.</param>
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if ((context?.Instance != null) && (provider != null))
            {
                // Must use the editor service for showing dialogs
                IWindowsFormsEditorService editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

                if (editorService != null)
                {
                    // Cast the value to the correct type
                    KryptonCheckSet checkSet = (KryptonCheckSet)context.Instance;

                    // Create the dialog used to edit the set of KryptonCheckButtons
                    KryptonCheckButtonCollectionForm dialog = new KryptonCheckButtonCollectionForm(checkSet);

                    if (editorService.ShowDialog(dialog) == DialogResult.OK)
                    {
                        // Notify container that value has been changed
                        context.OnComponentChanged();
                    }
                }
            }

            // Return the original value
            return value;
        }
    }
}
