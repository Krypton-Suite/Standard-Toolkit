#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public partial class KryptonTaskDialogElementCommandLinkButtons
{
    /// <summary>
    /// Internally used by KryptonTaskDialogElementCommandLinkButtons
    /// </summary>
    internal class ButtonsCollectionEditor : System.ComponentModel.Design.CollectionEditor,
        IDisposable
    {
        #region Fields
        private bool _disposed;
        private KryptonTaskDialogElementCommandLinkButtons? _parentInstance;
        private CollectionForm? _collectionForm;
        #endregion

        #region Identity
        public ButtonsCollectionEditor(Type type) : base(type)
        {
            _parentInstance = null;
            _collectionForm = null;
        }
        #endregion

        #region Overrides
        protected override CollectionForm CreateCollectionForm()
        {
            _parentInstance = this.Context?.Instance as KryptonTaskDialogElementCommandLinkButtons;
            if (_parentInstance is not null)
            {
                _collectionForm = base.CreateCollectionForm();
                _collectionForm.Shown += OnCollectionFormShown;
                _collectionForm.FormClosing += OnCollectionFormClosed;

                return _collectionForm;
            }
            else
            {
                throw new NullReferenceException(nameof(_parentInstance));
            }
        }
        #endregion

        #region Private
        private void OnCollectionFormClosed(object? sender, FormClosingEventArgs e)
        {
            if (_parentInstance is not null)
            {
                _parentInstance.CollectionEditorActive = false;
                _parentInstance.CollectionEditorClosed?.Invoke();

                e.Cancel = false;
            }
            else
            {
                throw new NullReferenceException(nameof(_parentInstance));
            }
        }

        private void OnCollectionFormShown(object? sender, EventArgs e)
        {
            if (_parentInstance is not null)
            {
                _parentInstance?.CollectionEditorActive = true;
            }
            else
            {
                throw new NullReferenceException(nameof(_parentInstance));
            }
        }
        #endregion

        #region IDispose
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if (_collectionForm is not null)
                {
                    _collectionForm.FormClosing -= OnCollectionFormClosed;
                    _collectionForm.Shown -= OnCollectionFormShown;
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
