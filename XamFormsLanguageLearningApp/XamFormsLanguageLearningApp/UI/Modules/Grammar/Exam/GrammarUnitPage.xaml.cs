using System.ComponentModel;
using Xamarin.Forms;

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
            BindingContext = _viewModel = new GrammarUnitViewModel();
        }

        #endregion Constructors
    }
}