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
        private bool _showTableTitle;
        private bool _hasFourColumns;

        #endregion Fields

        #region Constructors

        public BindableGrammarExample(GrammarExample domain)
        {
            Intro = domain.Intro;
            Deklinacija = domain.Deklinacija;
            GlagolPoLicima = domain.GlagolPoLicima;
            TableTitle = domain.TablicaNepravilnihGlagola.Ime;
            TableRows = domain.TablicaNepravilnihGlagola.Redovi;
            ShowTableTitle = TableTitle.Length > 0;
            if (TableRows.Count > 0)
            foreach(var row in TableRows)
            {
                if(row.Stupac4.Length > 0)
                {
                    HasFourColumns = true;
                }
            }
            if(TableRows.Count > 0)
            {
                ShowTableTitle = TableTitle.Length > 0;
                ShowTable = true;
                TableHeight = ShowTableTitle ? (TableRows.Count + 1) * 40 : (TableRows.Count) * 40;
                TableHeight = ShowTableTitle ? (TableRows.Count + 1) * 30 : TableRows.Count * 30;
            }
        }

        #endregion Constructors



        #region Properties

        public ObservableCollection<string> Deklinacija { get; set; }

        public ObservableCollection<string> GlagolPoLicima { get; set; }

        public string Intro { get; set; }

        public bool ShowTable
        {
            get => _showTable;
            set => SetProperty(ref _showTable, value);
        }

        public bool ShowTableTitle
        {
            get => _showTableTitle;
            set => SetProperty(ref _showTableTitle, value);
        }

        public bool HasFourColumns
        {
            get => _hasFourColumns;
            set => SetProperty(ref _hasFourColumns, value);
        }


        public int TableHeight
        {
            get => _tableHeight;
            set => SetProperty(ref _tableHeight, value);
        }

        public string TableTitle
        {
            get => _tableTitle;
            set => SetProperty(ref _tableTitle, value);
        }

        public ObservableCollection<Redovi> TableRows { get; set; }


        #endregion Properties
    }
}