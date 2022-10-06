using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using XamFormsLanguageLearningApp.Models;
using XamFormsLanguageLearningApp.ViewModels;

namespace XamFormsLanguageLearningApp.Modules.Grammar.UnitSelected.SelectedUnitExam
{
    [QueryProperty(nameof(Name), nameof(Name))]
    public class GrammarUnitViewModel : BaseViewModel
    {
        #region Fields

        private string _name;

        #endregion Fields

        #region Constructors

        public GrammarUnitViewModel()
        {
            GrammarExamples = new ObservableCollection<BindableGrammarExample>();
        }

        #endregion Constructors



        #region Properties

        public ObservableCollection<BindableGrammarExample> GrammarExamples { get; }

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

        #endregion Properties



        #region Methods

        public void LoadName(string name)
        {
            try
            {
                IsBusy = true;
                GrammarExamples.Clear();
                var assembly = typeof(GrammarUnitPage).GetTypeInfo().Assembly;
                var grammarExamples = GrammarService.GetGrammarExamples(assembly, name);
                var substringAfterNumber = name.Split('.').Last();
                Title = substringAfterNumber.Split('-').First();
                foreach (var grammarExample in grammarExamples)
                {
                    GrammarExamples.Add(new BindableGrammarExample(grammarExample));
                }
                IsBusy = false;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        #endregion Methods
    }
}