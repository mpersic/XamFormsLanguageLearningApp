using System.ComponentModel;
using Xamarin.Forms;
using XamFormsLanguageLearningApp.Effects;
using XamFormsLanguageLearningApp.ViewModels;

namespace XamFormsLanguageLearningApp.Views
{
    public partial class VocabularyExamPage : ContentPage
    {
        #region Fields

        private VocabularyExamViewModel _viewModel;

        #endregion Fields

        #region Constructors

        public VocabularyExamPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new VocabularyExamViewModel();
            _viewModel.InitializeQuestionEvent += InitializeQuestion;
        }

        #endregion Constructors



        #region Methods

        private void InitializeQuestion()
        {
            MyWarappicek.Children.Clear();
            foreach (var item in _viewModel.WordExplanations)
            {
                var clickableWord = new Label()
                {
                    Text = item.Word.Trim(),
                    TextColor = Color.Black,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                };
                clickableWord.BackgroundColor = Color.Transparent;
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += async (s, e) =>
                {
                    if (item.Explanation == string.Empty)
                        return;
                    await Shell.Current.DisplayAlert("Značenje", item.Explanation, "OK");
                };
                clickableWord.GestureRecognizers.Add(tapGestureRecognizer);
                MyWarappicek.Children.Add(clickableWord);
            }
        }

        #endregion Methods
    }
}