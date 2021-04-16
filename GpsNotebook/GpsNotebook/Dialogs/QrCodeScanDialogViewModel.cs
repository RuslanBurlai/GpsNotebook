using GpsNotebook.ViewModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms.GoogleMaps;

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
            _qrScanResalt ?? (_qrScanResalt = new DelegateCommand<object>(OnQrScanResaltCommand));

        #endregion

        #region -- Private Helpers --

        private void OnQrScanResaltCommand(object obj)
        {
            var scanedPin = obj as string;
            //var pin = JsonConvert.DeserializeObject<string>(scanedPin);

            var pinParametrs = new NavigationParameters();
            pinParametrs.Add(nameof(QrCodeScanDialogViewModel), obj);

            NavigationService.GoBackAsync(pinParametrs);
        }

        #endregion
    }

}
