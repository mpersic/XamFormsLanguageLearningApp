using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XamFormsLanguageLearningApp.Models;
using XamFormsLanguageLearningApp.Models.Units;

namespace LanguageLearningApp.Services
{
    public interface IGrammarService
    {
        #region Methods

        List<GrammarExample> GetGrammarExamples(Assembly assembly, string name);

        List<Unit> GetSelectedUnits(Assembly assembly, string selectedUnitName);

        List<Unit> GetUnits(Assembly assembly);

        #endregion Methods
    }
}