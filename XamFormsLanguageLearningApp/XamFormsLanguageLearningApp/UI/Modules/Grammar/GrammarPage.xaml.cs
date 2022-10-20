using Xamarin.Forms;

namespace XamFormsLanguageLearningApp
{
    public partial class GrammarPage : ContentPage
    {
        #region Fields

        private GrammarViewModel _viewModel;

        #endregion Fields

        #region Constructors

        public GrammarPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new GrammarViewModel();
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