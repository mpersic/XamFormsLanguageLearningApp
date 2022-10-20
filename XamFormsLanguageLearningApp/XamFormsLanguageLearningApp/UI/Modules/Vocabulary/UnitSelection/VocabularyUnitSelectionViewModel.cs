using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamFormsLanguageLearningApp.Models.Units;

namespace XamFormsLanguageLearningApp
{
    [QueryProperty(nameof(Name), nameof(Name))]
    public class VocabularyUnitSelectionViewModel : BaseViewModel
    {
        #region Fields

        private string _description;
        private string _name;
        private string text;

        #endregion Fields

        #region Constructors

        public VocabularyUnitSelectionViewModel()
        {
            ItemTapped = new Command<VocabularyGradedUnit>(OnItemSelected);

            GradedUnits = new ObservableCollection<VocabularyGradedUnit>();
        }

        #endregion Constructors



        #region Properties

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public ObservableCollection<VocabularyGradedUnit> GradedUnits { get; }
        public string Id { get; set; }

        public Command<VocabularyGradedUnit> ItemTapped { get; }

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
                var assembly = typeof(VocabularyUnitSelectionPage).GetTypeInfo().Assembly;
                var grammarUnits = VocabularyService.GetSelectedUnits(assembly, name);
                var gradedUnits = new List<VocabularyGradedUnit>(
                    grammarUnits.Select(unit => new VocabularyGradedUnit(unit)).ToList());
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

        private async void OnItemSelected(VocabularyGradedUnit item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(VocabularyExamPage)}?{nameof(VocabularyExamViewModel.Name)}={item.Lesson}");
        }

        #endregion Methods
    }
}