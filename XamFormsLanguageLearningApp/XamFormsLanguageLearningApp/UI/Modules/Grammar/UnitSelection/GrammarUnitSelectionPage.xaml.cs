using System.ComponentModel;
using Xamarin.Forms;

namespace XamFormsLanguageLearningApp
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