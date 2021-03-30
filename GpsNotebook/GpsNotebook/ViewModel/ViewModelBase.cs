using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GpsNotebook.ViewModel
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible, IInitialize
    {
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        protected INavigationService NavigationService { get; private set; }

        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void Destroy()
        {
        }
    }
}
