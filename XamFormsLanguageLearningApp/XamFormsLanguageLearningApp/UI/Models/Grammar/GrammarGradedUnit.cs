﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace XamFormsLanguageLearningApp.Models.Units
{
    public class GrammarGradedUnit : BaseViewModel
    {
        #region Fields

        private string _highScore;
        private string _lesson;
        private string _name;
        private bool _scoreIsVisible;

        #endregion Fields

        #region Constructors

        public GrammarGradedUnit(Unit baseUnit)
        {
            Name = baseUnit.Name;
            Lesson = baseUnit.Lesson;
            var formattedName = baseUnit.Lesson.ToLower().Replace(" ","");
            HighScore = Preferences.Get($"{formattedName}", "");
            var scoreIsToggled = Preferences.Get("score_toggled", true);
            if (HighScore.Length > 0 && scoreIsToggled)
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