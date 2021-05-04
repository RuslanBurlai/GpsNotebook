using GpsNotebook.Dialogs;
using GpsNotebook.Services.AppThemeService;
using GpsNotebook.Services.Authorization;
using GpsNotebook.Services.PinLocationRepository;
using GpsNotebook.Services.RepositoryService;
using GpsNotebook.Services.SettingsManager;
using GpsNotebook.Services.UserModelService;
using GpsNotebook.Services.UserRepository;
using GpsNotebook.Services.AppThemeService;
using GpsNotebook.View;
using GpsNotebook.ViewModel;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using GpsNotebook.Styles;
using GpsNotebook.Services.PermissionService;

namespace GpsNotebook
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base (initializer)
        {
        }

        #region -- Overrides --

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LogInOrRegisterView, LogInOrRegisteredViewModel>();
            containerRegistry.RegisterForNavigation<LogInView, LogInViewModel>();
            containerRegistry.RegisterForNavigation<RegisterView, RegisterViewModel>();
            containerRegistry.RegisterForNavigation<RegisterAndPasswordView, RegisterAndPasswordViewModel>();
            containerRegistry.RegisterForNavigation<MapTabbedView>();
            containerRegistry.RegisterForNavigation<MapTabView, MapTabViewModel>();
            containerRegistry.RegisterForNavigation<PinTabView, PinTabViewModel>();
            containerRegistry.RegisterForNavigation<AddPinView, AddPinViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<ScanningQrCodeView, ScanningQrCodeViewModel>();

            //Services
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IRepositoryService>(Container.Resolve<RepositoryService>());
            containerRegistry.RegisterInstance<IUserModelService>(Container.Resolve<UserModelService>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IPinModelService>(Container.Resolve<PinModelService>());
            //containerRegistry.RegisterInstance<IPermissionService>(Container.Resolve<PermissionsService>());
            containerRegistry.RegisterInstance<IAppThemeService>(Container.Resolve<AppThemeService>());

            //Dialogs
            containerRegistry.RegisterDialog<SharePinViaQRDialogView, SharePinViaQRDialogViewModel>();
        }

        protected async override void OnInitialized()
        {
            InitializeComponent();

            if (_appThemeService.IsDarkTheme)
            {
                _appThemeService.SetUIAppTheme(nameof(DarkTheme));
            }
            else
            {
                _appThemeService.SetUIAppTheme(nameof(LightTheme));
            }
            //add IsAuthorized property to AuthService
            if (_authorization.IsAuthorized)
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MapTabbedView)}");
            }
            else
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(LogInOrRegisterView)}");
            }

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #endregion

        #region -- Private Helpers --
        private IAuthorizationService _authorization => Container.Resolve<IAuthorizationService>();
        private IAppThemeService _appThemeService => Container.Resolve<IAppThemeService>();

        #endregion
    }
}
