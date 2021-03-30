using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNotebook.ViewModel
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible, IInitialize
    {

        #region -- Public Properties --

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #endregion

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        #region -- Public Properties --
        protected INavigationService NavigationService { get; private set; }

        #endregion

        #region -- Initialize implementation --

        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        #endregion

        #region -- INavigationAware implementation --

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        #endregion

        #region -- IDestructible implementation --

        public virtual void Destroy()
        {
        }

        #endregion
    }
}
