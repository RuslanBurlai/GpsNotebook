using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace GpsNotebook.Dialogs
{
    public class TapOnPinViewModel : IDialogAware
    {
        #region --- Implement IDialogAware ---

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
            throw new NotImplementedException();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
