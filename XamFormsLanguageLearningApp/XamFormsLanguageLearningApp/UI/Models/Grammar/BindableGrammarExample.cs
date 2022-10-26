using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace XamFormsLanguageLearningApp.Models
{
    public class BindableGrammarExample : BaseViewModel
    {
        #region Fields

        private int _tableHeight;
        private string _tableTitle;
        private bool _showTable;

        #endregion Fields

        #region Constructors

        public BindableGrammarExample(GrammarExample domain)
        {
            Intro = domain.Intro;
            Deklinacija = domain.Deklinacija;
            GlagolPoLicima = domain.GlagolPoLicima;
            TableTitle = domain.TablicaNepravilnihGlagola.Ime;
            TableRows = domain.TablicaNepravilnihGlagola.Redovi;
            if(TableRows.Count > 0)
            {
                ShowTable = true;
                TableHeight = (TableRows.Count + 1) * 30;
            }
        }

        #endregion Constructors



        #region Properties

        public bool ShowTable
        {
            get => _showTable;
            set => SetProperty(ref _showTable, value);
        }

        public string TableTitle
        {
            get => _tableTitle;
            set => SetProperty(ref _tableTitle, value);
        }

        public int TableHeight
        {
            get => _tableHeight;
            set => SetProperty(ref _tableHeight, value);
        }

        public ObservableCollection<string> Deklinacija { get; set; }

        public ObservableCollection<Redovi> TableRows { get; set; }

        public ObservableCollection<string> GlagolPoLicima { get; set; }

        public string Intro { get; set; }

        #endregion Properties
    }
}