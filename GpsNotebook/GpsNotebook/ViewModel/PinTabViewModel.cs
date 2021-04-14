using GpsNotebook.Extensions;
using GpsNotebook.Models;
using GpsNotebook.Services.PinLocationRepository;
using GpsNotebook.View;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNotebook.ViewModel
{
    public class PinTabViewModel : ViewModelBase
    {
        private IPinModelService _pinModelService;

        public PinTabViewModel(
            INavigationService navigationPage,
            IPinModelService pinModelService) :
            base(navigationPage)
        {
            //to resources
            Title = "List pins";

            _pinModelService = pinModelService;
        }

        #region -- Public properties --

        private ObservableCollection<PinModel> _pins;
        public ObservableCollection<PinModel> Pins
        {
            get { return _pins; }
            set { SetProperty(ref _pins, value); }
        }

        private ICommand _navigateToAddPin;
        public ICommand NavigateToAddPin =>
            _navigateToAddPin ?? (_navigateToAddPin = new DelegateCommand(OnNavigateToAddPin));

        private PinModel _selectedItemInListView;
        public PinModel SelectedItemInListView
        {
            get { return _selectedItemInListView; }
            set { SetProperty(ref _selectedItemInListView, value); }
        }

        private ICommand _editPinInListCommand;
        public ICommand EditPinInListCommand =>
            _editPinInListCommand ?? (_editPinInListCommand = new DelegateCommand<PinModel>(OnEditPinInListCommand));

        private ICommand _deletePinFromListCommand;
        public ICommand DeletePinFromListCommand =>
            _deletePinFromListCommand ?? (_deletePinFromListCommand = new DelegateCommand<PinModel>(OnDeletePinFromListCommand));

        private ICommand _searchPinsCommand;
        public ICommand SearchPinsCommand =>
            _searchPinsCommand ?? (_searchPinsCommand = new Command<string>(OnSearchPinsCommand));

        private bool _visibleDropDown;
        public bool VisibleDropDown
        {
            get { return _visibleDropDown; }
            set { SetProperty(ref _visibleDropDown, value); }
        }

        private ICommand _selectCategoryCommand;
        public ICommand SelectCategoryCommand =>
            _selectCategoryCommand ?? (_selectCategoryCommand = new DelegateCommand(OnSelectCategoryCommand));

        private bool _isVisibleCategorySelector;
        public bool IsVisibleCategorySelector
        {
            get { return _isVisibleCategorySelector; }
            set { SetProperty(ref _isVisibleCategorySelector, value); }
        }

        private object _sotrValue;
        public object SotrValue
        {
            get { return _sotrValue; }
            set { SetProperty(ref _sotrValue, value); }
        }

        #endregion

        #region -- Private Helpers --

        private async void OnNavigateToAddPin()
        {
            await NavigationService.NavigateAsync($"{nameof(AddPinView)}");
        }

        private void OnEditPinInListCommand(PinModel pin)
        {
        }

        private void OnDeletePinFromListCommand(PinModel pin)
        {
            _pinModelService.DeletePinLocation(pin);
            var myObservableCollection = new ObservableCollection<PinModel>(_pinModelService.GetAllPins());
            Pins = myObservableCollection;
        }

        private void OnSearchPinsCommand(string searchQuery)
        {
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                VisibleDropDown = true;
                var searchPins = _pinModelService.SearchPins(searchQuery);
                Pins = new ObservableCollection<PinModel>(searchPins);
            }
            else
            {
                VisibleDropDown = false;
                var pins = _pinModelService.GetAllPins();
                Pins = new ObservableCollection<PinModel>(pins);
            }
        }

        #endregion

        #region -- Overrides --

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {

            if(parameters.TryGetValue(nameof(PinModel),out PinModel newPin))
            {
                Pins = new ObservableCollection<PinModel>(_pinModelService.GetAllPins());
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add(nameof(Pins), this.Pins);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            Pins = new ObservableCollection<PinModel>(_pinModelService.GetAllPins());
        }

        protected async override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(SelectedItemInListView):
                    {
                        NavigationParameters p = new NavigationParameters();
                        p.Add("SelectedItemFromPinTab", SelectedItemInListView);
                        await NavigationService.NavigateAsync(nameof(MapTabbedView), p);
                        break;
                    }

                case nameof(SotrValue):
                    {
                        var sortMethod = SotrValue as string;
                        var sortedPins = _pinModelService.GetAllPins().Where((pin) => pin.Categories == sortMethod);
                        Pins = new ObservableCollection<PinModel>(sortedPins);
                        //IsVisibleCategorySelector = false;
                        break;
                    }
            }
        }

        private void OnSelectCategoryCommand()
        {
            if (IsVisibleCategorySelector == true)
            {
                IsVisibleCategorySelector = false;
            }
            else
            {
                IsVisibleCategorySelector = true;
            }
        }

        #endregion
    }
}
