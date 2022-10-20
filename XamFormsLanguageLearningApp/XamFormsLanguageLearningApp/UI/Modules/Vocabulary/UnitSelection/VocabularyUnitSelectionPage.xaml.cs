using System.ComponentModel;
using Xamarin.Forms;

namespace XamFormsLanguageLearningApp
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