using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamFormsLanguageLearningApp.Models;
using XamFormsLanguageLearningApp.Models.Units;
using XamFormsLanguageLearningApp.Views;

namespace XamFormsLanguageLearningApp.ViewModels
{
    [QueryProperty(nameof(Name), nameof(Name))]
    public class VocabularyExamViewModel : BaseViewModel
    {
        #region Fields

        private List<string> _correctAnswersCollection;
        private bool _isReading;
        private string _name;
        private ExamState _examState;
        private string correctAnswer;
        private int correctAnswers;
        private int currentQuestion;
        private string currentScore;
        private bool examIsCompleted;
        private bool examIsVisible;
        private string examName;
        private ExamState examState;
        private string name;
        private bool promptForExamIsVisible;
        private bool revisionIsVisible;
        private bool showFinalScore;
        private string userAnswer;
        private string visibleQuestion;

        #endregion Fields

        public VocabularyExamViewModel()
        {
            CheckAnswerCommand = new Command(CheckAnwser);
            GoToTestCommand = new Command(GoToTest);
            GoToRevisionCommand = new Command(GoToRevision);
            GoToHomePageCommand = new Command(GoToHomePage);
            _correctAnswersCollection = new List<string>();
            ActiveQuestion = new ObservableCollection<WordExplanation>();
            GrammarExamples = new ObservableCollection<WordExplanation>();
            Questions = new ObservableCollection<VocabularyQuestionAnswerObj>();
        }

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

        #region Properties

        public Command CheckAnswerCommand { get; }

        public string CorrectAnswer
        {
            get => correctAnswer;
            set => SetProperty(ref correctAnswer, value);
        }

        public int CorrectAnswers
        {
            get => correctAnswers;
            set => SetProperty(ref correctAnswers, value);
        }

        public int CurrentQuestion
        {
            get => currentQuestion;
            set => SetProperty(ref currentQuestion, value);
        }

        public string CurrentScore
        {
            get => currentScore;
            set => SetProperty(ref currentScore, value);
        }

        public bool ExamIsCompleted
        {
            get => examIsCompleted;
            set => SetProperty(ref examIsCompleted, value);
        }

        public bool ExamIsVisible
        {
            get => examIsVisible;
            set => SetProperty(ref examIsVisible, value);
        }

        public string ExamName
        {
            get => examName;
            set => SetProperty(ref examName, value);
        }

        public ExamState ExamState
        {
            get => examState;
            set => SetProperty(ref examState, value);
        }

        public Command GoToHomePageCommand { get; }

        public Command GoToRevisionCommand { get; }

        public Command GoToTestCommand { get; }

        public bool PromptForExamIsVisible
        {
            get => promptForExamIsVisible;
            set => SetProperty(ref promptForExamIsVisible, value);
        }

        public Command ReadTextCommand { get; }

        public bool RevisionIsVisible
        {
            get => revisionIsVisible;
            set => SetProperty(ref revisionIsVisible, value);
        }

        public Command ShowAdditionalInfoCommand { get; }

        public bool ShowFinalScore
        {
            get => showFinalScore;
            set => SetProperty(ref showFinalScore, value);
        }

        public string UserAnswer
        {
            get => userAnswer;
            set => SetProperty(ref userAnswer, value);
        }

        public string VisibleQuestion
        {
            get => visibleQuestion;
            set => SetProperty(ref visibleQuestion, value);
        }

        private ObservableCollection<WordExplanation> ActiveQuestion { get; }
        private ObservableCollection<WordExplanation> GrammarExamples { get; }
        private ObservableCollection<VocabularyQuestionAnswerObj> Questions { get; }

        #endregion Properties


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

        private async void GoToHomePage()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void CheckAnwser()
        {
            if (ExamState.Equals(ExamState.Revise))
            {
                NextQuestion();
                return;
            }

            if (UserAnswer.Length == 0)
            {
                return;
            }
            if (CheckIfAnswerIsValid())
            {
                CorrectAnswers++;
                CurrentScore = $"Score: {CorrectAnswers}/{Questions.Count}";
                await Shell.Current.DisplayAlert("Bravo", "Točan odgovor!", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Ups", "Netočan odgovor.", "OK");
            }
            UserAnswer = string.Empty;
            NextQuestion();
        }

        private bool CheckIfAnswerIsValid()
        {
            return _correctAnswersCollection.Contains(UserAnswer);
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
        private bool CanGoToNextQuestion()
        {
            return (CurrentQuestion) < Questions.Count - 1;
        }

        private void SetUpQuestion()
        {
            try
            {
                VisibleQuestion = "";
                CurrentQuestion++;
                //foreach (var question in Questions[CurrentQuestion].Question)
                //{
                //    VisibleQuestion += question;
                //    //Question.Add(question);
                //}
                VisibleQuestion = Questions[CurrentQuestion].Question;
                //InitializeQuestionEvent?.Invoke();
                _correctAnswersCollection = Questions[CurrentQuestion].Answer;
                CorrectAnswer = Questions[CurrentQuestion].Answer.First();
            }
            catch (Exception ex)
            {
                ShowFinalScreen();
            }
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

        private void SaveFinalScore()
        {
            Preferences.Set(ExamName, $"{CorrectAnswers}/{Questions.Count}");
        }

        public void LoadName(string name)
        {
            try
            {
                IsBusy = true;
                //GradedUnits.Clear();
                //var assembly = typeof(VocabularyExamPage).GetTypeInfo().Assembly;
                //var questionAnswersObjs = VocabularyService.GetQuestions(assembly, name);
                //var gradedUnits = new List<GradedUnit>(
                //    grammarUnits.Select(unit => new GradedUnit(unit)).ToList());

                var substringAfterNumber = name.Split('.').Last();
                Title = substringAfterNumber.Split('-').First();

                ExamState = ExamState.Prompt;
                ExamName = name.Split(' ').Last();
                ProcessExamState();

                //foreach (var questionAnswerObj in questionAnswersObjs)
                //{
                //    Questions.Add(questionAnswerObj);
                //}
                IsBusy = false;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public async void ProcessExamState()
        {
            switch (ExamState)
            {
                case ExamState.Prompt:
                    PromptForExamIsVisible = true;
                    ExamIsVisible = false;
                    RevisionIsVisible = false;
                    ExamIsCompleted = false;
                    break;

                case ExamState.Enter:
                    ShowFinalScore = true;
                    PromptForExamIsVisible = false;
                    ExamIsVisible = true;
                    RevisionIsVisible = false;
                    ExamIsCompleted = false;
                    await LoadAndInitializeExam();
                    break;

                case ExamState.Revise:
                    ShowFinalScore = false;
                    PromptForExamIsVisible = false;
                    ExamIsVisible = false;
                    RevisionIsVisible = true;
                    ExamIsCompleted = false;
                    await LoadAndInitializeExam();
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

        private async Task LoadAndInitializeExam()
        {
            try
            {
                if (ExamState.Equals(ExamState.Revise))
                {
                    ExamName = $"revise-{ExamName}";
                }
                var assembly = typeof(VocabularyExamPage).GetTypeInfo().Assembly;
                Questions.Clear();
                var questionAnswersObjs = VocabularyService.GetQuestions(assembly, ExamName);
                foreach (var questionAnswerObj in questionAnswersObjs)
                {
                    Questions.Add(questionAnswerObj);
                }

                if (ExamState.Equals(ExamState.Enter))
                {
                    UserAnswer = string.Empty;
                    CorrectAnswers = 0;
                    CurrentScore = $"Score: {CorrectAnswers}/{Questions.Count}";
                }

                CurrentQuestion = 0;
                //foreach (var question in Questions[CurrentQuestion].Question)
                //{
                //    VisibleQuestion += question;
                //}
                //}
                VisibleQuestion = Questions[CurrentQuestion].Question;
                CorrectAnswer = Questions[CurrentQuestion].Answer.First();
            }
            catch (Exception ex)
            {
                GoToHomePage();
            }
        }



    }
}
