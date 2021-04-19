using GpsNotebook.ViewModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms.GoogleMaps;
using ZXing;

namespace GpsNotebook.Dialogs
{
    public class QrCodeScanDialogViewModel : ViewModelBase, IDialogAware
    {
        public QrCodeScanDialogViewModel(INavigationService navigationService) :
            base(navigationService)
        {

        }

        #region -- IDialogAware implementation --

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        #endregion

        #region -- Public Properties --

        private ICommand _qrScanResalt;
        public ICommand QrScanResalt =>
            _qrScanResalt ?? (_qrScanResalt = new DelegateCommand<Result>(OnQrScanResaltCommand));

        #endregion

        #region -- Private Helpers --

        private void OnQrScanResaltCommand(Result obj)
        {
            var scanedPin = obj as Result;
            var pin = JsonConvert.DeserializeObject<Pin>(scanedPin.Text);

            var pinParametrs = new NavigationParameters();
            pinParametrs.Add(nameof(QrCodeScanDialogViewModel), obj);

            NavigationService.GoBackAsync(pinParametrs);
        }

        #endregion
    }
}
