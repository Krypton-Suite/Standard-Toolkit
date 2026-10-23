#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public partial class KryptonTaskDialogElementContent
{
    /// <summary>
    /// Used internally by KryptonTaskDialogElementContent
    /// </summary>
    public class ContentImageStorage :
        IKryptonTaskDialogElementContentImage,
        IKryptonTaskDialogElementPropertyChanged<ContentImageStorageProperties>
    {
        #region Events
        /// <summary>
        /// Subscribe to be notified when one of the properties changes.
        /// </summary>
        public event Action<ContentImageStorageProperties>? PropertyChanged;
        #endregion

        #region Identity
        public ContentImageStorage()
        {
            Visible = false;
            Size = new Size(0, 0);
            Image = null;
        }
        #endregion

        #region Private
        private void OnPropertyChanged(ContentImageStorageProperties property)
        {
            PropertyChanged?.Invoke(property);
        }
        #endregion

        #region Public
        /// <summary>
        /// When true the image display left of the text otherwise on the right side.
        /// </summary>
        public bool PositionedLeft
        {
            get => field;

            set
            {
                if (field != value)
                {
                    field = value;
                    OnPropertyChanged(ContentImageStorageProperties.Position);
                }
            }
        }

        /// <summary>
        /// Desired size of the image.<br/>
        /// If the size is left to zero the image will be display using it's own size.
        /// </summary>
        public Size Size
        {
            get => field;

            set
            {
                if (field != value)
                {
                    field = value;
                    OnPropertyChanged(ContentImageStorageProperties.Size);
                }
            }
        }

        /// <inheritdoc/>
        public bool Visible
        {
            get => field;

            set
            {
                if (field != value)
                {
                    field = value;
                    OnPropertyChanged(ContentImageStorageProperties.Visible);
                }
            }
        }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public Image? Image
        {
            get => field;

            set
            {
                if (field != value)
                {
                    field = value;
                    OnPropertyChanged(ContentImageStorageProperties.Image);
                }
            }
        }
        public void ResetImage() => Image = null;

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <returns>String.Empty</returns>
        public sealed override string ToString()
        {
            return string.Empty;
        }
        #endregion
    }
}

