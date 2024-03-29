﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamFormsLanguageLearningApp.Models;

namespace XamFormsLanguageLearningApp
{
    [QueryProperty(nameof(Name), nameof(Name))]
    public class GrammarUnitViewModel : BaseViewModel
    {
        #region Fields

        private string _activeQuestionPart1;
        private string _activeQuestionPart2;
        private string _activeQuestionPart3;
        private string _correctAnswer1;
        private string _correctAnswer2;
        private string _correctAnswer3;
        private int _correctAnswers;
        private int _currentQuestion;
        private string _currentScore;
        private bool _examIsCompleted;
        private bool _examIsVisible;
        private string _examName;
        private ExamState _examState;
        private string _finishedStateMessage;
        private string _name;
        private bool _promptForExamIsVisible;
        private bool _questionPart1Visible;
        private bool _questionPart2Visible;
        private bool _questionPart3Visible;
        private bool _revisionIsVisible;
        private bool _showFinalScore;
        private string _unprocessedName;
        private string _userAnswer1;
        private string _userAnswer2;
        private string _userAnswer3;
        private bool _userInput1Visible;
        private bool _userInput2Visible;
        private bool _userInput3Visible;

        #endregion Fields

        #region Constructors

        public GrammarUnitViewModel()
        {
            GoToTestCommand = new Command(GoToTest);
            CheckAnswerCommand = new Command(CheckAnwser);
            GoToRevisionCommand = new Command(GoToRevision);
            ResetExamStateCommand = new Command(ResetExamState);
            FinishRevisionCommand = new Command(FinishRevision);

            BindableGrammarExamQuestions = new ObservableCollection<BindableGrammarExamQuestion>();
            GrammarExamples = new ObservableCollection<BindableGrammarExample>();
        }

        #endregion Constructors



        #region Properties

        public string ActiveQuestionPart1
        {
            get => _activeQuestionPart1;
            set => SetProperty(ref _activeQuestionPart1, value);
        }

        public string ActiveQuestionPart2
        {
            get => _activeQuestionPart2;
            set => SetProperty(ref _activeQuestionPart2, value);
        }

        public string ActiveQuestionPart3
        {
            get => _activeQuestionPart3;
            set => SetProperty(ref _activeQuestionPart3, value);
        }

        public ObservableCollection<BindableGrammarExamQuestion> BindableGrammarExamQuestions { get; }

        public Command CheckAnswerCommand { get; }

        public int CorrectAnswers
        {
            get => _correctAnswers;
            set => SetProperty(ref _correctAnswers, value);
        }

        public int CurrentQuestion
        {
            get => _currentQuestion;
            set => SetProperty(ref _currentQuestion, value);
        }

        public string CurrentScore
        {
            get => _currentScore;
            set => SetProperty(ref _currentScore, value);
        }

        public bool ExamIsCompleted
        {
            get => _examIsCompleted;
            set => SetProperty(ref _examIsCompleted, value);
        }

        public bool ExamIsVisible
        {
            get => _examIsVisible;
            set => SetProperty(ref _examIsVisible, value);
        }

        public string ExamName
        {
            get => _examName;
            set => SetProperty(ref _examName, value);
        }

        public ExamState ExamState
        {
            get => _examState;
            set => SetProperty(ref _examState, value);
        }

        public string FinishedStateMessage
        {
            get => _finishedStateMessage;
            set => SetProperty(ref _finishedStateMessage, value);
        }

        public Command FinishRevisionCommand { get; }
        public Command ResetExamStateCommand { get; }
        public Command GoToRevisionCommand { get; }

        public Command GoToTestCommand { get; }

        public ObservableCollection<BindableGrammarExample> GrammarExamples { get; }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                LoadName(value);
            }
        }

        public bool PromptForExamIsVisible
        {
            get => _promptForExamIsVisible;
            set => SetProperty(ref _promptForExamIsVisible, value);
        }

        public bool QuestionPart1Visible
        {
            get => _questionPart1Visible;
            set => SetProperty(ref _questionPart1Visible, value);
        }

        public bool QuestionPart2Visible
        {
            get => _questionPart2Visible;
            set => SetProperty(ref _questionPart2Visible, value);
        }

        public bool QuestionPart3Visible
        {
            get => _questionPart3Visible;
            set => SetProperty(ref _questionPart3Visible, value);
        }

        public bool RevisionIsVisible
        {
            get => _revisionIsVisible;
            set => SetProperty(ref _revisionIsVisible, value);
        }

        public bool ShowFinalScore
        {
            get => _showFinalScore;
            set => SetProperty(ref _showFinalScore, value);
        }

        public string UserAnswer1
        {
            get => _userAnswer1;
            set => SetProperty(ref _userAnswer1, value);
        }

        public string UserAnswer2
        {
            get => _userAnswer2;
            set => SetProperty(ref _userAnswer2, value);
        }

        public string UserAnswer3
        {
            get => _userAnswer3;
            set => SetProperty(ref _userAnswer3, value);
        }

        public bool UserInput1Visible
        {
            get => _userInput1Visible;
            set => SetProperty(ref _userInput1Visible, value);
        }

        public bool UserInput2Visible
        {
            get => _userInput2Visible;
            set => SetProperty(ref _userInput2Visible, value);
        }

        public bool UserInput3Visible
        {
            get => _userInput3Visible;
            set => SetProperty(ref _userInput3Visible, value);
        }

        #endregion Properties



        #region Methods

        public void LoadName(string name)
        {
            try
            {
                IsBusy = true;
                _unprocessedName = name;
                BindableGrammarExamQuestions.Clear();
                GrammarExamples.Clear();
                var substringAfterNumber = name.Split('.').Last();
                Title = substringAfterNumber.Split('-').First();

                ExamState = ExamState.Prompt;
                ExamName = name.Split(' ').Last();
                ProcessExamState();
                IsBusy = false;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public void ProcessExamState()
        {
            switch (ExamState)
            {
                case ExamState.Prompt:
                    ExamIsVisible = false;
                    RevisionIsVisible = false;
                    ExamIsCompleted = false;
                    PromptForExamIsVisible = true;
                    break;

                case ExamState.Enter:
                    ShowFinalScore = false;
                    PromptForExamIsVisible = false;
                    RevisionIsVisible = false;
                    ExamIsCompleted = false;
                    ExamIsVisible = true;
                    FinishedStateMessage = Strings.TestComplete;
                    LoadAndInitializeExam();
                    break;

                case ExamState.Revise:
                    ShowFinalScore = false;
                    PromptForExamIsVisible = false;
                    ExamIsVisible = false;
                    RevisionIsVisible = true;
                    ExamIsCompleted = false;
                    FinishedStateMessage = Strings.LessonComplete;
                    LoadAndInitializeRevision();
                    break;

                case ExamState.Final:
                    PromptForExamIsVisible = false;
                    ExamIsVisible = false;
                    RevisionIsVisible = false;
                    ExamIsCompleted = true;
                    break;

                default:
                    PromptForExamIsVisible = false;
                    ExamIsVisible = false;
                    ExamIsCompleted = false;
                    RevisionIsVisible = true;
                    break;
            }
        }

        private bool CanGoToNextQuestion()
        {
            return (CurrentQuestion) < BindableGrammarExamQuestions.Count - 1;
        }

        private async void CheckAnwser()
        {
            if (UserAnswer1.Length == 0 && UserAnswer2.Length == 0 && UserAnswer3.Length == 0)
            {
                return;
            }
            if (CheckIfAnswerIsValid())
            {
                CorrectAnswers++;
                CurrentScore = $"{Strings.Score}: {CorrectAnswers}/{BindableGrammarExamQuestions.Count}";
                await Shell.Current.DisplayAlert(Strings.Bravo, Strings.CorrectAnswer, Strings.OK);
            }
            else
            {
                await Shell.Current.DisplayAlert(Strings.Oops, Strings.IncorrectAnswer, Strings.OK);
            }
            CleanUserInputs();
            NextQuestion();
        }

        private bool CheckIfAnswerIsValid()
        {
            return _correctAnswer1.Equals(UserAnswer1) && _correctAnswer2.Equals(UserAnswer2) && _correctAnswer3.Equals(UserAnswer3);
        }

        private void CleanUserInputs()
        {
            UserAnswer1 = string.Empty;
            UserAnswer2 = string.Empty;
            UserAnswer3 = string.Empty;
        }

        private void FinishRevision()
        {
            ExamState = ExamState.Final;
            ProcessExamState();
        }

        private void ResetExamState()
        {
            ExamState = ExamState.Prompt;
            ProcessExamState();
        }

        private void GoToRevision()
        {
            ExamState = ExamState.Revise;
            ProcessExamState();
        }

        private void GoToTest()
        {
            ExamState = ExamState.Enter;
            ProcessExamState();
        }

        private void LoadAndInitializeExam()
        {
            try
            {
                ExamName = $"{_unprocessedName}";
                var assembly = typeof(GrammarUnitPage).GetTypeInfo().Assembly;
                var grammarExamQuestions = GrammarService.GetGrammarExamQuestions(assembly, ExamName);
                foreach (var examQuestion in grammarExamQuestions)
                {
                    BindableGrammarExamQuestions.Add(new BindableGrammarExamQuestion(examQuestion));
                }

                CleanUserInputs();

                CurrentQuestion = 0;
                CorrectAnswers = 0;
                CurrentScore = $"{Strings.Score}: {CorrectAnswers}/{BindableGrammarExamQuestions.Count}";

                SetupQuestionAndAnswer();
            }
            catch (Exception ex)
            {
                ResetExamState();
            }
        }

        private void LoadAndInitializeRevision()
        {
            try
            {
                IsBusy = true;
                GrammarExamples.Clear();
                var assembly = typeof(GrammarUnitPage).GetTypeInfo().Assembly;
                var grammarExamples = GrammarService.GetGrammarExamples(assembly, _unprocessedName);
                foreach (var grammarExample in grammarExamples)
                {
                    GrammarExamples.Add(new BindableGrammarExample(grammarExample));
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void NextQuestion()
        {
            if (CanGoToNextQuestion())
            {
                SetUpQuestion();
            }
            else
            {
                ShowFinalScreen();
            }
        }

        private void SaveFinalScore()
        {
            Preferences.Set(ExamName.ToLower().Replace(" ",""), $"{CorrectAnswers}/{BindableGrammarExamQuestions.Count}");
        }

        private void SetQuestionVisibility()
        {
            QuestionPart1Visible = ActiveQuestionPart1 != string.Empty ? true : false;
            QuestionPart2Visible = ActiveQuestionPart2 != string.Empty ? true : false;
            QuestionPart3Visible = ActiveQuestionPart3 != string.Empty ? true : false;
            UserInput1Visible = _correctAnswer1 != string.Empty ? true : false;
            UserInput2Visible = _correctAnswer2 != string.Empty ? true : false;
            UserInput3Visible = _correctAnswer3 != string.Empty ? true : false;
        }

        private void SetUpQuestion()
        {
            try
            {
                CleanUserInputs();

                CurrentQuestion++;
                CurrentScore = $"{Strings.Score}: {CorrectAnswers}/{BindableGrammarExamQuestions.Count}";

                SetupQuestionAndAnswer();
            }
            catch (Exception ex)
            {
                ShowFinalScreen();
            }
        }

        private void SetupQuestionAndAnswer()
        {
            ActiveQuestionPart1 = BindableGrammarExamQuestions[CurrentQuestion].QuestionPart1;
            ActiveQuestionPart2 = BindableGrammarExamQuestions[CurrentQuestion].QuestionPart2;
            ActiveQuestionPart3 = BindableGrammarExamQuestions[CurrentQuestion].QuestionPart3;
            _correctAnswer1 = BindableGrammarExamQuestions[CurrentQuestion].AnswerPart1;
            _correctAnswer2 = BindableGrammarExamQuestions[CurrentQuestion].AnswerPart2;
            _correctAnswer3 = BindableGrammarExamQuestions[CurrentQuestion].AnswerPart3;
            SetQuestionVisibility();
        }

        private void ShowFinalScreen()
        {
            if (ExamState.Equals(ExamState.Enter))
            {
                SaveFinalScore();
            }
            ExamState = ExamState.Final;
            ProcessExamState();
        }

        #endregion Methods
    }
}