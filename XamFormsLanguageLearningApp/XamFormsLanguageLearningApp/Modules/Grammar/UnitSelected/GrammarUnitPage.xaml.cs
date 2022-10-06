using System.ComponentModel;
using Xamarin.Forms;
using XamFormsLanguageLearningApp.Modules.Grammar.UnitSelected.SelectedUnitExam;
using XamFormsLanguageLearningApp.ViewModels;

namespace XamFormsLanguageLearningApp
{
    public partial class GrammarUnitPage : ContentPage
    {
        #region Constructors

        public GrammarUnitPage()
        {
            InitializeComponent();
            BindingContext = new GrammarUnitViewModel();
        }

        #endregion Constructors
    }
}