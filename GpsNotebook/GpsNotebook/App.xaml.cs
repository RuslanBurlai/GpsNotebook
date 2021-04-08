using GpsNotebook.Repository;
using GpsNotebook.Services.Authentication;
using GpsNotebook.Services.PinLocationRepository;
using GpsNotebook.Services.UserRepository;
using GpsNotebook.View;
using GpsNotebook.ViewModel;
using Prism;using Prism.Ioc;
using Prism.Unity;
using System;
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
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository.Repository>());
            containerRegistry.RegisterInstance<IUserRepository>(Container.Resolve<UserRepository>());
            containerRegistry.RegisterInstance<IAuthentication>(Container.Resolve<Authentication>());

            containerRegistry.RegisterInstance<IPinLocationRepository>(Container.Resolve<PinLocationRepository>());
        }

        protected async override void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MapTabbedView)}");
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
