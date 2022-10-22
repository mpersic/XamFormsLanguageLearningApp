using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamFormsLanguageLearningApp.Models;
using XamFormsLanguageLearningApp.Models.Units;

namespace XamFormsLanguageLearningApp
{
    public class InformationViewModel : BaseViewModel
    {
        #region Fields

        private bool _notificationsToggled;
        private bool _scoreToggled;

        #endregion Fields

        #region Constructors

        public InformationViewModel()
        {

        }

        #endregion Constructors



        #region Properties

        public bool NotificationsToggled
        {
            get => _notificationsToggled;
            set => SetProperty(ref _notificationsToggled, value);
        }

        public bool ScoreToggled
        {
            get => _scoreToggled;
            set => SetProperty(ref _scoreToggled, value);
        }

        #endregion Properties

        #region Methods

        public void OnAppearing()
        {
            IsBusy = true;
            NotificationsToggled = Preferences.Get("notifications_toggled", false);
            ScoreToggled = Preferences.Get("score_toggled", true);
            IsBusy = false;

        }

        #endregion

    }
}