using Xamarin.Forms;

namespace XamFormsLanguageLearningApp
{
    public partial class VocabularyPage : ContentPage
    {
        #region Fields

        private VocabularyViewModel _viewModel;

        #endregion Fields

        #region Constructors

        public VocabularyPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new VocabularyViewModel();
        }

        #endregion Constructors



        #region Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        #endregion Methods
    }
}