using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XamFormsLanguageLearningApp.Models;
using XamFormsLanguageLearningApp.Models.Units;

namespace XamFormsLanguageLearningApp
{
    public interface IVocabularyService
    {
        #region Methods

        List<Unit> GetSelectedUnits(Assembly assembly, string selectedUnitName);
        List<Unit> GetUnits(Assembly assembly);

        List<VocabularyQuestionAnswerObj> GetQuestions(Assembly assembly, string name);

        #endregion Methods
    }
}
