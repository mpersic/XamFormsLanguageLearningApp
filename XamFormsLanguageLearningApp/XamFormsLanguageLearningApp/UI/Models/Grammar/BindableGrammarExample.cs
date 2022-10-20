using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace XamFormsLanguageLearningApp.Models
{
    public class BindableGrammarExample : BaseViewModel
    {
        #region Fields

        private int deklinacijaCount;

        private int glagolPoLicimaCount;

        #endregion Fields

        #region Constructors

        public BindableGrammarExample(GrammarExample domain)
        {
            Intro = domain.Intro;
            Deklinacija = domain.Deklinacija;
            GlagolPoLicima = domain.GlagolPoLicima;
            DeklinacijaCount = Deklinacija.Count * 50;
            GlagolPoLicimaCount = GlagolPoLicima.Count * 50;
        }

        #endregion Constructors



        #region Properties

        public ObservableCollection<string> Deklinacija { get; set; }

        public int DeklinacijaCount
        {
            get => deklinacijaCount;
            set => SetProperty(ref deklinacijaCount, value);
        }

        public ObservableCollection<string> GlagolPoLicima { get; set; }

        public int GlagolPoLicimaCount
        {
            get => glagolPoLicimaCount;
            set => SetProperty(ref glagolPoLicimaCount, value);
        }

        public string Intro { get; set; }

        #endregion Properties
    }
}