using Prism.Services.Dialogs;
using System;
using GpsNotebook.ViewModel;
using Prism.Navigation;

namespace GpsNotebook.Dialogs
{
    public class SharePinViaQRDialogViewModel : ViewModelBase, IDialogAware
    {
        public SharePinViaQRDialogViewModel(INavigationService navigationService) :
            base(navigationService)
        {
        }

        #region -- Public Property --

        private string _qrCodeData;
        public string QrCodeData
        {
            get { return _qrCodeData; }
            set { SetProperty(ref _qrCodeData, value); }
        }

        #endregion

        #region -- Private Helpers --


        #endregion

        #region -- IDialogAware implementation --

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            //to constants
            if (parameters.ContainsKey("SharePinCommand"))
            {
                QrCodeData = parameters.GetValue<string>("SharePinCommand");
            }
        }

        #endregion
    }
}
