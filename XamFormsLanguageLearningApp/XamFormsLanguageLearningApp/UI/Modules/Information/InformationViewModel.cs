using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamFormsLanguageLearningApp.Models;
using XamFormsLanguageLearningApp.Models.Units;

namespace XamFormsLanguageLearningApp
{
    public class InformationViewModel : BaseViewModel
    {
        #region Fields

        private bool _notificationsToggled;

        private Unit _selectedItem;

        #endregion Fields

        #region Constructors

        public InformationViewModel()
        {
            Items = new ObservableCollection<Unit>();
            ItemTapped = new Command<Unit>(OnItemSelected);
        }

        #endregion Constructors



        #region Properties

        public ObservableCollection<Unit> Items { get; }

        public Command<Unit> ItemTapped { get; }

        public bool NotificationsToggled
        {
            get => _notificationsToggled;
            set => SetProperty(ref _notificationsToggled, value);
        }

        public Unit SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        #endregion Properties



        #region Methods

        public void OnAppearing()
        {
            IsBusy = true;
            ExecuteLoadItemsCommand();
            SelectedItem = null;
        }

        private void ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var assembly = typeof(VocabularyPage).GetTypeInfo().Assembly;
                var items = VocabularyService.GetUnits(assembly);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnItemSelected(Unit item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(VocabularyUnitSelectionPage)}?{nameof(VocabularyUnitSelectionViewModel.Name)}={item.Name}");
        }

        #endregion Methods
    }
}