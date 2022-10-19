using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamFormsLanguageLearningApp.Models.Units;
using XamFormsLanguageLearningApp.Views;

namespace XamFormsLanguageLearningApp.ViewModels
{
    [QueryProperty(nameof(Name), nameof(Name))]
    public class GrammarUnitSelectionViewModel : BaseViewModel
    {
        #region Fields

        private string _description;
        private string _name;
        private string text;

        #endregion Fields

        #region Constructors

        public GrammarUnitSelectionViewModel()
        {
            ItemTapped = new Command<GrammarGradedUnit>(OnItemSelected);

            GradedUnits = new ObservableCollection<GrammarGradedUnit>();
        }

        #endregion Constructors



        #region Properties

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public ObservableCollection<GrammarGradedUnit> GradedUnits { get; }
        public string Id { get; set; }

        public Command<GrammarGradedUnit> ItemTapped { get; }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                LoadName(value);
            }
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        #endregion Properties



        #region Methods

        public void LoadName(string name)
        {
            try
            {
                IsBusy = true;
                GradedUnits.Clear();
                var assembly = typeof(GrammarUnitSelectionPage).GetTypeInfo().Assembly;
                var grammarUnits = GrammarService.GetSelectedUnits(assembly, name);
                var gradedUnits = new List<GrammarGradedUnit>(
                    grammarUnits.Select(unit => new GrammarGradedUnit(unit)).ToList());
                var substringAfterNumber = name.Split('.').Last();
                Title = substringAfterNumber.Split('-').First();
                foreach (var gradedUnit in gradedUnits)
                {
                    GradedUnits.Add(gradedUnit);
                }
                IsBusy = false;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnItemSelected(GrammarGradedUnit item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(GrammarUnitPage)}?{nameof(GrammarUnitViewModel.Name)}={item.Lesson}");
        }

        #endregion Methods
    }
}