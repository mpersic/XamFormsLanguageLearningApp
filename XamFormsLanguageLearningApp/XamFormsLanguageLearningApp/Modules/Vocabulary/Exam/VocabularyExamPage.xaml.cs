using System.ComponentModel;
using Xamarin.Forms;
using XamFormsLanguageLearningApp.ViewModels;

namespace XamFormsLanguageLearningApp.Views
{
    public partial class VocabularyExamPage : ContentPage
    {
        #region Constructors

        public VocabularyExamPage()
        {
            InitializeComponent();
            BindingContext = new VocabularyExamViewModel();
        }

        #endregion Constructors
    }
}