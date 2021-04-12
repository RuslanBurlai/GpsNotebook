using GpsNotebook.Dialogs;
using GpsNotebook.Repository;
using GpsNotebook.Services.Authentication;
using GpsNotebook.Services.Authorization;
using GpsNotebook.Services.PinLocationRepository;
using GpsNotebook.Services.SettingsManager;
using GpsNotebook.Services.UserRepository;
using GpsNotebook.View;
using GpsNotebook.ViewModel;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;

namespace GpsNotebook
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base (initializer)
        {
        }

        #region -- Ovverides --

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();

            containerRegistry.RegisterForNavigation<MapTabbedView>();
            containerRegistry.RegisterForNavigation<MapTabView, MapTabViewModel>();
            containerRegistry.RegisterForNavigation<PinTabView, PinTabViewModel>();

            containerRegistry.RegisterForNavigation<AddPinView, AddPinViewModel>();

            //Services
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository.Repository>());
            containerRegistry.RegisterInstance<IUserRepository>(Container.Resolve<UserRepository>());
            containerRegistry.RegisterInstance<IAuthentication>(Container.Resolve<Authentication>());
            containerRegistry.RegisterInstance<IAuthorization>(Container.Resolve<Authorization>());
            containerRegistry.RegisterInstance<IPinLocationRepository>(Container.Resolve<PinLocationRepository>());

            //Dialog
            containerRegistry.RegisterDialog<TapOnPin, TapOnPinViewModel>();
        }

        protected async override void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignInView)}");
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
    }
}
