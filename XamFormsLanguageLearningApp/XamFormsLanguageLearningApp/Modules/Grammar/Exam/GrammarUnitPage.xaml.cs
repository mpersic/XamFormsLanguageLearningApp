using System.ComponentModel;
using Xamarin.Forms;
using XamFormsLanguageLearningApp.ViewModels;

namespace XamFormsLanguageLearningApp
{
    public partial class GrammarUnitPage : ContentPage
    {
        #region Fields

        private GrammarUnitViewModel _viewModel;

        #endregion Fields

        #region Constructors

        public GrammarUnitPage()
        {
            InitializeComponent();
            BindingContext = new GrammarUnitViewModel();
        }

        #endregion Constructors
    }
}