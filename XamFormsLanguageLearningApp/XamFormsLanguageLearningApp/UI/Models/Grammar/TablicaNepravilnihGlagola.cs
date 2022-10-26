using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace XamFormsLanguageLearningApp
{
    public class TablicaNepravilnihGlagola
    {
        public string Ime { get; set; }
        public ObservableCollection<Redovi> Redovi { get; set; }
    }
}
