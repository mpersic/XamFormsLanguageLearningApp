using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamFormsLanguageLearningApp.Models;
using XamFormsLanguageLearningApp.Models.Units;

namespace XamFormsLanguageLearningApp
{
    [QueryProperty(nameof(Name), nameof(Name))]
    public class VocabularyExamViewModel : BaseViewModel
    {
        #region Fields

        private string _correctAnswer;
        private int _correctAnswers;
        private List<string> _correctAnswersCollection;
        private CancellationTokenSource _cts;
        private int _currentQuestion;
        private string _currentScore;
        private bool _examIsCompleted;
        private bool _examIsVisible;
        private string _examName;
        private ExamState _examState;
        private string _finishedStateMessage;
        private bool _isReading;
        private string _name;
        private bool _promptForExamIsVisible;
        private bool _revisionIsVisible;
        private bool _showFinalScore;
        private string _userAnswer;
        private string _visibleQuestion;

        #endregion Fields

        #region Constructors

        public VocabularyExamViewModel()
        {
            CheckAnswerCommand = new Command(CheckAnwser);
            GoToTestCommand = new Command(GoToTest);
            GoToRevisionCommand = new Command(GoToRevision);
            GoToHomePageCommand = new Command(GoToHomePage);
            ReadTextCommand = new Command(async () => await ReadTextAsync());

            ActiveQuestion = new ObservableCollection<WordExplanation>();
            GrammarExamples = new ObservableCollection<WordExplanation>();
            Questions = new ObservableCollection<VocabularyQuestionAnswerObj>();
            WordExplanations = new ObservableCollection<WordExplanation>();

            _correctAnswersCollection = new List<string>();
        }

        #endregion Constructors



        #region Delegates

        public delegate void InitializeQuestion();

        #endregion Delegates

        #region Events

        public event InitializeQuestion InitializeQuestionEvent;

        #endregion Events



        #region Properties

        public Command CheckAnswerCommand { get; }

        public string CorrectAnswer
        {
            get => _correctAnswer;
            set => SetProperty(ref _correctAnswer, value);
        }

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

        public Command GoToHomePageCommand { get; }

        public Command GoToRevisionCommand { get; }

        public Command GoToTestCommand { get; }

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

        public Command ReadTextCommand { get; }

        public bool RevisionIsVisible
        {
            get => _revisionIsVisible;
            set => SetProperty(ref _revisionIsVisible, value);
        }

        public Command ShowAdditionalInfoCommand { get; }

        public bool ShowFinalScore
        {
            get => _showFinalScore;
            set => SetProperty(ref _showFinalScore, value);
        }

        public string UserAnswer
        {
            get => _userAnswer;
            set => SetProperty(ref _userAnswer, value);
        }

        public string VisibleQuestion
        {
            get => _visibleQuestion;
            set => SetProperty(ref _visibleQuestion, value);
        }

        public ObservableCollection<WordExplanation> WordExplanations { get; }
        private ObservableCollection<WordExplanation> ActiveQuestion { get; }
        private ObservableCollection<WordExplanation> GrammarExamples { get; }

        private ObservableCollection<VocabularyQuestionAnswerObj> Questions { get; }

        #endregion Properties



        #region Methods

        public void LoadName(string name)
        {
            try
            {
                IsBusy = true;
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
                    ExamIsCompleted = false;
                    RevisionIsVisible = true;
                    FinishedStateMessage = Strings.LessonComplete;
                    LoadAndInitializeExam();
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

        public async Task ReadTextAsync()
        {
            try
            {
                if (_isReading)
                {
                    return;
                }
                _isReading = true;
                IEnumerable<Locale> locales = await TextToSpeech.GetLocalesAsync();
                var german = locales.FirstOrDefault(l => l.Country == "de_DE");
                _cts = new CancellationTokenSource();
                await TextToSpeech.SpeakAsync(
                    VisibleQuestion,
                    new SpeechOptions
                    {
                        Locale = locales.FirstOrDefault(l => l.Country == "DEU")
                    }
                    , cancelToken: _cts.Token);

                // This method will block until utterance finishes.
            }
            catch (Exception ex)
            {
            }
            finally
            {
                _isReading = false;
            }
        }

        private bool CanGoToNextQuestion()
        {
            return (CurrentQuestion) < Questions.Count - 1;
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
                CurrentScore = $"{Strings.Score}: {CorrectAnswers}/{Questions.Count}";
                await Shell.Current.DisplayAlert(Strings.Bravo, Strings.CorrectAnswer, Strings.OK);
            }
            else
            {
                await Shell.Current.DisplayAlert(Strings.Oops, Strings.IncorrectAnswer, Strings.OK);
            }
            UserAnswer = string.Empty;
            NextQuestion();
        }

        private bool CheckIfAnswerIsValid()
        {
            return _correctAnswersCollection.Contains(UserAnswer);
        }

        private async void GoToHomePage()
        {
            await Shell.Current.GoToAsync("..");
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
                    CurrentScore = $"{Strings.Score}: {CorrectAnswers}/{Questions.Count}";
                }

                CurrentQuestion = 0;
                VisibleQuestion = Questions[CurrentQuestion].Question;
                WordExplanations.Clear();
                foreach (var wordExplanation in Questions[CurrentQuestion].WordExplanations)
                {
                    WordExplanations.Add(wordExplanation);
                }
                InitializeQuestionEvent?.Invoke();
                CorrectAnswer = Questions[CurrentQuestion].Answer.First();
            }
            catch (Exception ex)
            {
                GoToHomePage();
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
            Preferences.Set(ExamName, $"{CorrectAnswers}/{Questions.Count}");
        }

        private void SetUpQuestion()
        {
            try
            {
                VisibleQuestion = "";
                CurrentQuestion++;
                VisibleQuestion = Questions[CurrentQuestion].Question;
                WordExplanations.Clear();
                foreach (var wordExplanation in Questions[CurrentQuestion].WordExplanations)
                {
                    WordExplanations.Add(wordExplanation);
                }
                InitializeQuestionEvent?.Invoke();
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

        #endregion Methods
    }
}