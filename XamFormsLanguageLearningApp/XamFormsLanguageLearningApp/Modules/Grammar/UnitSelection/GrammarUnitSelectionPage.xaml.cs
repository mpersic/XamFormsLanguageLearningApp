using System.ComponentModel;
using Xamarin.Forms;
using XamFormsLanguageLearningApp.ViewModels;

namespace XamFormsLanguageLearningApp.Views
{
    public partial class GrammarUnitSelectionPage : ContentPage
    {
        #region Constructors

        public GrammarUnitSelectionPage()
        {
            InitializeComponent();
            BindingContext = new GrammarUnitSelectionViewModel();
        }

        #endregion Constructors
    }
}