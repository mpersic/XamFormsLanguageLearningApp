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
                var clickableWord = new Label() { Text = item.Word, TextColor = Color.Black };
                clickableWord.BackgroundColor = Color.Transparent;
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += async (s, e) =>
                {
                    if (item.Explanation == string.Empty)
                        return;
                    await Shell.Current.DisplayAlert("Značenje", item.Explanation, "OK");
                };
                //TooltipEffect.SetHasTooltip(clickableWord, false);
                //TooltipEffect.SetBackgroundColor(clickableWord, Color.Blue);
                //if (item.Explanation.Length == 0)
                //{
                //    TooltipEffect.SetHasTooltip(clickableWord, false);
                //}
                //else
                //{
                //    TooltipEffect.SetHasTooltip(clickableWord, true);
                //}
                //TooltipEffect.SetText(clickableWord, item.Explanation);
                //TooltipEffect.SetPosition(clickableWord, TooltipPosition.Bottom);
                clickableWord.GestureRecognizers.Add(tapGestureRecognizer);
                MyWarappicek.Children.Add(clickableWord);
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            //foreach (var c in mainLayout.Children)
            //{
            //    if (TooltipEffect.GetHasTooltip(c))
            //    {
            //        TooltipEffect.SetHasTooltip(c, false);
            //        TooltipEffect.SetHasTooltip(c, true);
            //    }
            //}
        }

        #endregion Methods
    }
}