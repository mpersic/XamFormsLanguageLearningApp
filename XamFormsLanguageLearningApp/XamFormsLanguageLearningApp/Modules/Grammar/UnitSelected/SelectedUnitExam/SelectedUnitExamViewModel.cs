//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Reflection;
//using System.Threading.Tasks;
//using Xamarin.Essentials;
//using Xamarin.Forms;
//using XamFormsLanguageLearningApp.Models;
//using XamFormsLanguageLearningApp.Modules.Grammar.UnitSelected;
//using XamFormsLanguageLearningApp.ViewModels;

//namespace XamFormsLanguageLearningApp.Modules
//{
//    public class SelectedUnitExamViewModel : BaseViewModel
//    {
//        #region Fields

//        private List<string> _correctAnswersCollection;
//        private bool _isReading;

//        private string correctAnswer;
//        private int correctAnswers;
//        private int currentQuestion;
//        private string currentScore;
//        private bool examIsCompleted;
//        private bool examIsVisible;
//        private string examName;
//        private ExamState examState;
//        private string name;
//        private bool promptForExamIsVisible;
//        private bool revisionIsVisible;
//        private bool showFinalScore;
//        private string userAnswer;
//        private string visibleQuestion;

//        #endregion Fields

//        #region Constructors

//        public SelectedUnitExamViewModel()
//        {
//            CheckAnswerCommand = new Command(CheckAnwser);
//            GoToTestCommand = new Command(GoToTest);
//            GoToRevisionCommand = new Command(GoToRevision);
//            GoToHomePageCommand = new Command(GoToHomePage);
//            ShowAdditionalInfoCommand = new Command<GrammarExample>(selectedWord => ShowAdditionalInfo(selectedWord));
//            //ReadTextCommand = new Command(async () => await ReadTextAsync());
//            //_examService = examService;
//            _correctAnswersCollection = new List<string>();
//            ActiveQuestion = new ObservableCollection<GrammarExample>();
//            Questions = new ObservableCollection<GrammarExample>();
//        }

//        #endregion Constructors

//        #region Properties

//        public Command CheckAnswerCommand { get; }

//        public string CorrectAnswer
//        {
//            get => correctAnswer;
//            set => SetProperty(ref correctAnswer, value);
//        }

//        public int CorrectAnswers
//        {
//            get => correctAnswers;
//            set => SetProperty(ref correctAnswers, value);
//        }

//        public int CurrentQuestion
//        {
//            get => currentQuestion;
//            set => SetProperty(ref currentQuestion, value);
//        }

//        public string CurrentScore
//        {
//            get => currentScore;
//            set => SetProperty(ref currentScore, value);
//        }

//        public bool ExamIsCompleted
//        {
//            get => examIsCompleted;
//            set => SetProperty(ref examIsCompleted, value);
//        }

//        public bool ExamIsVisible
//        {
//            get => examIsVisible;
//            set => SetProperty(ref examIsVisible, value);
//        }

//        public string ExamName
//        {
//            get => examName;
//            set => SetProperty(ref examName, value);
//        }

//        public ExamState ExamState
//        {
//            get => examState;
//            set => SetProperty(ref examState, value);
//        }

//        public Command GoToHomePageCommand { get; }

//        public Command GoToRevisionCommand { get; }

//        public Command GoToTestCommand { get; }

//        public bool PromptForExamIsVisible
//        {
//            get => promptForExamIsVisible;
//            set => SetProperty(ref promptForExamIsVisible, value);
//        }

//        public Command ReadTextCommand { get; }

//        public bool RevisionIsVisible
//        {
//            get => revisionIsVisible;
//            set => SetProperty(ref revisionIsVisible, value);
//        }

//        public Command ShowAdditionalInfoCommand { get; }

//        public bool ShowFinalScore
//        {
//            get => showFinalScore;
//            set => SetProperty(ref showFinalScore, value);
//        }

//        public string UserAnswer
//        {
//            get => userAnswer;
//            set => SetProperty(ref userAnswer, value);
//        }

//        public string VisibleQuestion
//        {
//            get => visibleQuestion;
//            set => SetProperty(ref visibleQuestion, value);
//        }

//        private ObservableCollection<GrammarExample> ActiveQuestion { get; }
//        private ObservableCollection<GrammarExample> GrammarExamples { get; }
//        private ObservableCollection<GrammarExample> Questions { get; }

//        #endregion Properties

//        #region Methods

//        public async void ProcessExamState()
//        {
//            switch (ExamState)
//            {
//                case ExamState.Prompt:
//                    PromptForExamIsVisible = true;
//                    ExamIsVisible = false;
//                    RevisionIsVisible = false;
//                    ExamIsCompleted = false;
//                    break;

//                case ExamState.Enter:
//                    ShowFinalScore = true;
//                    PromptForExamIsVisible = false;
//                    ExamIsVisible = true;
//                    RevisionIsVisible = false;
//                    ExamIsCompleted = false;
//                    await LoadAndInitializeExam();
//                    break;

//                case ExamState.Revise:
//                    ShowFinalScore = false;
//                    PromptForExamIsVisible = false;
//                    ExamIsVisible = false;
//                    RevisionIsVisible = true;
//                    ExamIsCompleted = false;
//                    await LoadAndInitializeExam();
//                    break;

//                case ExamState.Final:
//                    PromptForExamIsVisible = false;
//                    ExamIsVisible = false;
//                    RevisionIsVisible = false;
//                    ExamIsCompleted = true;
//                    break;

//                default:
//                    PromptForExamIsVisible = false;
//                    ExamIsVisible = false;
//                    ExamIsCompleted = false;
//                    RevisionIsVisible = true;
//                    break;
//            }
//        }

//        private bool CanGoToNextQuestion()
//        {
//            return (CurrentQuestion) < Questions.Count - 1;
//        }

//        private async void CheckAnwser()
//        {
//            if (ExamState.Equals(ExamState.Revise))
//            {
//                NextQuestion();
//                return;
//            }

//            if (UserAnswer.Length == 0)
//            {
//                return;
//            }
//            if (CheckIfAnswerIsValid())
//            {
//                CorrectAnswers++;
//                CurrentScore = $"Score: {CorrectAnswers}/{Questions.Count}";
//                await Shell.Current.DisplayAlert("Bravo", "Točan odgovor!", "OK");
//            }
//            else
//            {
//                await Shell.Current.DisplayAlert("Ups", "Netočan odgovor.", "OK");
//            }
//            UserAnswer = string.Empty;
//            NextQuestion();
//        }

//        private bool ExamNameContainsNonEnglishCharacter()
//        {
//            return ExamName.Contains("č") || ExamName.Contains("ć") || ExamName.Contains("š");
//        }

//        private async void GoToHomePage()
//        {
//            await Shell.Current.GoToAsync("..");
//        }

//        private async Task LoadAndInitializeExam()
//        {
//            try
//            {
//                if (ExamNameContainsNonEnglishCharacter())
//                {
//                    ProcessNonEnglishCharacterRevision();
//                }
//                if (ExamState.Equals(ExamState.Revise))
//                {
//                    ExamName = $"revise-{ExamName}";
//                }
//                ExamName.Replace(" ", "");
//                IsBusy = true;
//                Questions.Clear();
//                var assembly = typeof(SelectedUnitExamPage).GetTypeInfo().Assembly;
//                var items = GrammarService.GetGrammarExamples(assembly, ExamName);
//                foreach (var item in items)
//                {
//                    Questions.Add(item);
//                }

//                if (ExamState.Equals(ExamState.Enter))
//                {
//                    UserAnswer = string.Empty;
//                    CorrectAnswers = 0;
//                    CurrentScore = $"Score: {CorrectAnswers}/{Questions.Count}";
//                }

//                CurrentQuestion = 0;
//                //foreach (var question in Questions[CurrentQuestion].Question)
//                //{
//                //    VisibleQuestion += question;
//                //}
//                //}
//                VisibleQuestion = Questions[CurrentQuestion].Question;
//                //InitializeQuestionEvent?.Invoke();
//                CorrectAnswer = Questions[CurrentQuestion].Answer.First();
//            }
//            catch (Exception ex)
//            {
//                GoToHomePage();
//                IsBusy = false;
//            }
//        }

//        private void NextQuestion()
//        {
//            if (CanGoToNextQuestion())
//            {
//                SetUpQuestion();
//            }
//            else
//            {
//                ShowFinalScreen();
//            }
//        }

//        private void ProcessNonEnglishCharacterRevision()
//        {
//            ExamName = ExamName.Replace("č", "c").Replace("ć", "c").Replace("š", "s");
//        }

//        private void SaveFinalScore()
//        {
//            Preferences.Set(ExamName, $"{CorrectAnswers}/{Questions.Count}");
//        }

//        private void SetUpQuestion()
//        {
//            try
//            {
//                VisibleQuestion = "";
//                CurrentQuestion++;
//                //foreach (var question in Questions[CurrentQuestion].Question)
//                //{
//                //    VisibleQuestion += question;
//                //    //Question.Add(question);
//                //}
//                VisibleQuestion = Questions[CurrentQuestion].Question;
//                //InitializeQuestionEvent?.Invoke();
//                _correctAnswersCollection = Questions[CurrentQuestion].Answer;
//                CorrectAnswer = Questions[CurrentQuestion].Answer.First();
//            }
//            catch (Exception ex)
//            {
//                ShowFinalScreen();
//            }
//        }

//        private void ShowFinalScreen()
//        {
//            if (ExamState.Equals(ExamState.Enter))
//            {
//                SaveFinalScore();
//            }
//            ExamState = ExamState.Final;
//            ProcessExamState();
//        }

//        #endregion Methods
//    }
//}