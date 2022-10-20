using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamFormsLanguageLearningApp.Models.Units;

namespace XamFormsLanguageLearningApp
{
    public class GrammarViewModel : BaseViewModel
    {
        #region Fields

        private Unit _selectedItem;

        #endregion Fields

        #region Constructors

        public GrammarViewModel()
        {
            Title = "Gramatika";
            Items = new ObservableCollection<Unit>();
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);

            ItemTapped = new Command<Unit>(OnItemSelected);
        }

        #endregion Constructors



        #region Properties

        public ObservableCollection<Unit> Items { get; }
        public Command<Unit> ItemTapped { get; }
        public Command LoadItemsCommand { get; }

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
                var assembly = typeof(GrammarPage).GetTypeInfo().Assembly;
                var items = GrammarService.GetUnits(assembly);
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
            await Shell.Current.GoToAsync($"{nameof(GrammarUnitSelectionPage)}?{nameof(GrammarUnitSelectionViewModel.Name)}={item.Name}");
        }

        #endregion Methods
    }
}