using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms.GoogleMaps;
using ZXing;
using Prism.Mvvm;

namespace GpsNotebook.Dialogs
{
    public class QrCodeScanDialogViewModel : BindableBase, IDialogAware
    {

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
            var pin = JsonConvert.DeserializeObject<Pin>(obj.Text);

            RequestClose(new DialogParameters 
            { 
                { nameof(QrCodeScanDialogViewModel), pin } 
            });
        }

        #endregion
    }
}
