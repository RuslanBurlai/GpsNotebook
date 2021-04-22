﻿using GpsNotebook.Dialogs;
using GpsNotebook.Services.Authorization;
using GpsNotebook.Services.Permission;
using GpsNotebook.Services.PinLocationRepository;
using GpsNotebook.Services.RepositoryService;
using GpsNotebook.Services.SettingsManager;
using GpsNotebook.Services.UserModelService;
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
        private IAuthorizationService _authorization => Container.Resolve<IAuthorizationService>();
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


            //Services
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IRepositoryService>(Container.Resolve<RepositoryService>());
            containerRegistry.RegisterInstance<IUserModelService>(Container.Resolve<UserModelService>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IPinModelService>(Container.Resolve<PinModelService>());
            containerRegistry.RegisterInstance<IPermissionService>(Container.Resolve<PermissionsService>());


            //Dialogs
            containerRegistry.RegisterDialog<TapOnPin, TapOnPinViewModel>();
            containerRegistry.RegisterDialog<QrCodeScanDialogView, QrCodeScanDialogViewModel>();
        }

        protected async override void OnInitialized()
        {
            InitializeComponent();

            //add IsAuthorized property to AuthService
            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(LogInOrRegisterView)}");
            //if (_authorization.GetUserId != 0)
            //{
            //    await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MapTabbedView)}");
            //}
            //else
            //{
            //    await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignInView)}");
            //}

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
