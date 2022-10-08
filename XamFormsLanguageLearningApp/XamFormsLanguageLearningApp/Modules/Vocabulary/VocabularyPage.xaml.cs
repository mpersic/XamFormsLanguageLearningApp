using MarcTron.Plugin;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamFormsLanguageLearningApp.ViewModels;

namespace XamFormsLanguageLearningApp.Views
{
    public partial class VocabularyPage : ContentPage
    {

        private VocabularyViewModel _viewModel;


        #region Constructors

        public VocabularyPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new VocabularyViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        #endregion Constructors
    }
}