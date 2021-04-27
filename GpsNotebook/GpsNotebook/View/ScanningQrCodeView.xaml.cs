using ZXing.Net.Mobile.Forms;

namespace GpsNotebook.View
{
    public partial class ScanningQrCodeView :ZXingScannerPage
    {
        public ScanningQrCodeView()
        {
            InitializeComponent();
        }

        private void ZXingScannerPage_OnScanResult(ZXing.Result result)
        {
            Navigation.PopAsync();
        }
    }
}