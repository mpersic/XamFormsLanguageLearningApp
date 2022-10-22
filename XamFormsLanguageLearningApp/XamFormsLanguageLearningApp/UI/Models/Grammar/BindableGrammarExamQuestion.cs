using System;
using System.Collections.Generic;
using System.Text;

namespace XamFormsLanguageLearningApp.Models
{
    public class BindableGrammarExamQuestion : BaseViewModel
    {
        #region Fields

        private string _answerPart1;
        private string _answerPart2;
        private string _answerPart3;

        private bool _hasAnswerPart1;

        private bool _hasAnswerPart2;

        private bool _hasAnswerPart3;

        private bool _hasQuestionPart1;

        private bool _hasQuestionPart2;

        private bool _hasQuestionPart3;

        private string _questionPart1;

        private string _questionPart2;

        private string _questionPart3;


        #endregion Fields

        #region Constructors

        public BindableGrammarExamQuestion(GrammarExamQuestion domain)
        {
            AnswerPart1 = domain.AnswerPart1;
            AnswerPart2 = domain.AnswerPart2;
            AnswerPart3 = domain.AnswerPart3;
            QuestionPart1 = domain.QuestionPart1;
            QuestionPart2 = domain.QuestionPart2;
            QuestionPart3 = domain.QuestionPart3;
            if (domain.AnswerPart1 != string.Empty)
            {
                HasAnswerPart1 = true;
            }
            if (domain.AnswerPart2 != string.Empty)
            {
                HasAnswerPart2 = true;
            }
            if (domain.AnswerPart3 != string.Empty)
            {
                HasAnswerPart3 = true;
            }
            if (domain.QuestionPart1 != string.Empty)
            {
                HasQuestionPart1 = true;
            }
            if (domain.QuestionPart2 != string.Empty)
            {
                HasQuestionPart2 = true;
            }
            if (domain.QuestionPart3 != string.Empty)
            {
                HasQuestionPart3 = true;
            }
        }

        #endregion Constructors



        #region Properties

        public string AnswerPart1
        {
            get => _answerPart1;
            set => SetProperty(ref _answerPart1, value);
        }

        public string AnswerPart2
        {
            get => _answerPart2;
            set => SetProperty(ref _answerPart2, value);
        }

        public string AnswerPart3
        {
            get => _answerPart3;
            set => SetProperty(ref _answerPart3, value);
        }

        public bool HasAnswerPart1
        {
            get => _hasAnswerPart1;
            set => SetProperty(ref _hasAnswerPart1, value);
        }

        public bool HasAnswerPart2
        {
            get => _hasAnswerPart2;
            set => SetProperty(ref _hasAnswerPart2, value);
        }

        public bool HasAnswerPart3
        {
            get => _hasAnswerPart3;
            set => SetProperty(ref _hasAnswerPart3, value);
        }

        public bool HasQuestionPart1
        {
            get => _hasQuestionPart1;
            set => SetProperty(ref _hasQuestionPart1, value);
        }

        public bool HasQuestionPart2
        {
            get => _hasAnswerPart2;
            set => SetProperty(ref _hasQuestionPart2, value);
        }

        public bool HasQuestionPart3
        {
            get => _hasQuestionPart3;
            set => SetProperty(ref _hasQuestionPart3, value);
        }

        public string QuestionPart1
        {
            get => _questionPart1;
            set => SetProperty(ref _questionPart1, value);
        }

        public string QuestionPart2
        {
            get => _questionPart2;
            set => SetProperty(ref _questionPart2, value);
        }

        public string QuestionPart3
        {
            get => _questionPart3;
            set => SetProperty(ref _questionPart3, value);
        }

        #endregion Properties
    }
}