﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamFormsLanguageLearningApp.Services;
using XamFormsLanguageLearningApp.Services.DataStore;
using XamFormsLanguageLearningApp.Views;

namespace XamFormsLanguageLearningApp
{
    public partial class App : Application
    {
        #region Constructors

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<GrammarService>();
            DependencyService.Register<VocabularyService>();
            MainPage = new AppShell();
        }

        #endregion Constructors



        #region Methods

        protected override void OnResume()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnStart()
        {
        }

        #endregion Methods
    }
}