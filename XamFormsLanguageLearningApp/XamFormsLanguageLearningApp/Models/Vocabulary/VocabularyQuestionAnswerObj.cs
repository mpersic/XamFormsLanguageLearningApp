using System;
using System.Collections.Generic;
using System.Text;

namespace XamFormsLanguageLearningApp.Models
{
    public class VocabularyQuestionAnswerObj
    {
        #region Properties

        public List<string> Answer { get; set; }
        public string Question { get; set; }

        public List<WordExplanation> WordExplanations { get; set; }

        #endregion Properties
    }
}