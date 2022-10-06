using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamFormsLanguageLearningApp.Models;
using XamFormsLanguageLearningApp.ViewModels;
using XamFormsLanguageLearningApp.Views;

namespace XamFormsLanguageLearningApp.Views
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