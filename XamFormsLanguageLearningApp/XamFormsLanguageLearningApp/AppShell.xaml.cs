using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamFormsLanguageLearningApp.ViewModels;
using XamFormsLanguageLearningApp.Views;

namespace XamFormsLanguageLearningApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        #region Constructors

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(GrammarUnitSelectionPage), typeof(GrammarUnitSelectionPage));
            Routing.RegisterRoute(nameof(GrammarUnitPage), typeof(GrammarUnitPage));
            Routing.RegisterRoute(nameof(VocabularyUnitSelectionPage), typeof(VocabularyUnitSelectionPage));
            Routing.RegisterRoute(nameof(VocabularyExamPage), typeof(VocabularyExamPage));
        }

        #endregion Constructors
    }
}