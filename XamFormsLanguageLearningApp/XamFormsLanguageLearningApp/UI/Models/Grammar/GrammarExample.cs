using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace XamFormsLanguageLearningApp.Models
{
    public class GrammarExample
    {
        #region Properties

        public ObservableCollection<string> Deklinacija { get; set; }
        public ObservableCollection<string> GlagolPoLicima { get; set; }
        public string Intro { get; set; }
        public TablicaNepravilnihGlagola TablicaNepravilnihGlagola { get; set; }


        #endregion Properties
    }
}