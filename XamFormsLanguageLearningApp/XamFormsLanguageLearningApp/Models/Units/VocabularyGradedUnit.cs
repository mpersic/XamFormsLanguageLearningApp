using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using XamFormsLanguageLearningApp.ViewModels;

namespace XamFormsLanguageLearningApp.Models.Units
{
    public class VocabularyGradedUnit : BaseViewModel
    {
        #region Fields

        private string _highScore;
        private string _lesson;
        private string _name;
        private bool _scoreIsVisible;

        #endregion Fields

        #region Constructors

        public VocabularyGradedUnit(Unit baseUnit)
        {
            Name = baseUnit.Name;
            Lesson = baseUnit.Lesson;
            var splitter = baseUnit.Lesson.Split(' ')[3];
            HighScore = Preferences.Get($"{splitter}", "");
            if (HighScore.Length > 0)
            {
                ScoreIsVisible = true;
            }
        }

        #endregion Constructors



        #region Properties

        public string HighScore
        {
            get => _highScore;
            set => SetProperty(ref _highScore, value);
        }

        public string Lesson
        {
            get => _lesson;
            set => SetProperty(ref _lesson, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public bool ScoreIsVisible
        {
            get => _scoreIsVisible;
            set => SetProperty(ref _scoreIsVisible, value);
        }

        #endregion Properties
    }
}