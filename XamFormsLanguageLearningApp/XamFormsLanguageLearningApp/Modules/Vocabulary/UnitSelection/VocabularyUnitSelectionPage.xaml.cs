using System.ComponentModel;
using Xamarin.Forms;
using XamFormsLanguageLearningApp.ViewModels;

namespace XamFormsLanguageLearningApp.Views
{
    public partial class VocabularyUnitSelectionPage : ContentPage
    {
        #region Constructors

        public VocabularyUnitSelectionPage()
        {
            InitializeComponent();
            BindingContext = new VocabularyUnitSelectionViewModel();
        }

        #endregion Constructors
    }
}