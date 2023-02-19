#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Docking
{
    /// <summary>
    /// Acts as a proxy for a KryptonPage inside a auto hidden group.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonAutoHiddenProxyPage : KryptonPage
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonAutoHiddenProxyPage class.
        /// </summary>
        public KryptonAutoHiddenProxyPage(KryptonPage page)
        {

            // We are a proxy for this cached page reference
            Page = page ?? throw new ArgumentNullException(nameof(page));

            // Text property was updated by the base class constructor, so now we update the actual referenced class
            Page.Text = Text;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            Page?.Dispose();

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the page for which this is a proxy.
        /// </summary>
        public KryptonPage? Page { get; }

        /// <summary>
        /// Gets and sets the page text.
        /// </summary>
        [AllowNull]
        public override string Text
        {
            get => Page != null ? Page.Text : base.Text;

            set
            {
                base.Text = value;
                if (Page != null)
                {
                    Page.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the title text for the page.
        /// </summary>
        [AllowNull]
        public override string TextTitle
        {
            get => Page?.TextTitle ?? string.Empty;
            set
            {
                if (Page != null)
                {
                    Page.TextTitle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the description text for the page.
        /// </summary>
        [AllowNull]
        public override string TextDescription
        {
            get => Page?.TextDescription ?? string.Empty;
            set
            {
                if (Page != null)
                {
                    Page.TextDescription = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the small image for the page.
        /// </summary>
        public override Bitmap? ImageSmall
        {
            get => Page?.ImageSmall;
            set
            {
                if (Page != null) Page.ImageSmall = value;
            }
        }

        /// <summary>
        /// Gets and sets the medium image for the page.
        /// </summary>
        public override Bitmap? ImageMedium
        {
            get => Page?.ImageMedium;
            set
            {
                if (Page != null) Page.ImageMedium = value;
            }
        }

        /// <summary>
        /// Gets and sets the large image for the page.
        /// </summary>
        public override Bitmap? ImageLarge
        {
            get => Page?.ImageLarge;
            set
            {
                if (Page != null) Page.ImageLarge = value;
            }
        }

        /// <summary>
        /// Gets and sets the page tooltip image.
        /// </summary>
        public override Bitmap? ToolTipImage
        {
            get => Page?.ToolTipImage;
            set
            {
                if (Page != null) Page.ToolTipImage = value;
            }
        }

        /// <summary>
        /// Gets and sets the tooltip image transparent color.
        /// </summary>
        public override Color ToolTipImageTransparentColor
        {
            get => Page?.ToolTipImageTransparentColor ?? Color.Empty;
            set
            {
                if (Page != null) 
                    Page.ToolTipImageTransparentColor = value;
            }
        }

        /// <summary>
        /// Gets and sets the page tooltip title text.
        /// </summary>
        public override string ToolTipTitle
        {
            get => Page?.ToolTipTitle ?? string.Empty;
            set
            {
                if (Page != null) 
                    Page.ToolTipTitle = value;
            }
        }

        /// <summary>
        /// Gets and sets the page tooltip body text.
        /// </summary>
        public override string ToolTipBody
        {
            get => Page?.ToolTipBody ?? string.Empty;
            set
            {
                if (Page != null) 
                    Page.ToolTipBody = value;
            }
        }

        /// <summary>
        /// Gets and sets the tooltip label style.
        /// </summary>
        public override LabelStyle ToolTipStyle
        {
            get => Page?.ToolTipStyle ?? default;
            set
            {
                if (Page != null) 
                    Page.ToolTipStyle = value;
            }
        }

        /// <summary>
        /// Gets and sets the KryptonContextMenu to show when right clicked.
        /// </summary>
        public override KryptonContextMenu? KryptonContextMenu
        {
            get => Page?.KryptonContextMenu;
            set
            {
                if (Page != null) 
                    Page.KryptonContextMenu = value;
            }
        }

        /// <summary>
        /// Gets and sets the unique name of the page.
        /// </summary>
        public override string UniqueName
        {
            get => Page?.UniqueName ?? string.Empty;
            set
            {
                if (Page != null) 
                    Page.UniqueName = value;
            }
        }

        /// <summary>
        /// Gets the string that matches the mapping request.
        /// </summary>
        /// <param name="mapping">Text mapping.</param>
        /// <returns>Matching string.</returns>
        public override string GetTextMapping(MapKryptonPageText mapping) => Page?.GetTextMapping(mapping) ?? string.Empty;

        /// <summary>
        /// Gets the image that matches the mapping request.
        /// </summary>
        /// <param name="mapping">Image mapping.</param>
        /// <returns>Image reference.</returns>
        public override Image? GetImageMapping(MapKryptonPageImage mapping) => Page?.GetImageMapping(mapping);

        /// <summary>
        /// Gets and sets the set of page flags.
        /// </summary>
        public override int Flags
        {
            get => Page?.Flags ?? 0;
            set
            {
                if (Page != null) 
                    Page.Flags = value;
            }
        }

        /// <summary>
        /// Set all the provided flags to true.
        /// </summary>
        /// <param name="flags">Flags to set.</param>
        public override void SetFlags(KryptonPageFlags flags) => Page?.SetFlags(flags);

        /// <summary>
        /// Sets all the provided flags to false.
        /// </summary>
        /// <param name="flags">Flags to set.</param>
        public override void ClearFlags(KryptonPageFlags flags) => Page?.ClearFlags(flags);

        /// <summary>
        /// Are all the provided flags set to true.
        /// </summary>
        /// <param name="flags">Flags to test.</param>
        /// <returns>True if all provided flags are defined as true; otherwise false.</returns>
        public override bool AreFlagsSet(KryptonPageFlags flags) => Page != null && Page.AreFlagsSet(flags);

        /// <summary>
        /// Gets the last value set to the Visible property.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override bool LastVisibleSet
        {
            get => Page is { LastVisibleSet: true };
            set
            {
                if (Page != null) 
                    Page.LastVisibleSet = value;
            }
        }

        /// <summary>Occurs when an appearance specific page property has changed.</summary>
        public override event PropertyChangedEventHandler AppearancePropertyChanged
        {
            add
            {
                if (Page != null) 
                    Page.AppearancePropertyChanged += value;
            }

            remove
            {
                if (Page != null) 
                    Page.AppearancePropertyChanged -= value;
            }
        }
        #endregion
    }
}
