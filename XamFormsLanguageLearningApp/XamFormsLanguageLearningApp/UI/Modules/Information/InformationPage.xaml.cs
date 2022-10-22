using Plugin.LocalNotification;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamFormsLanguageLearningApp
{
    public partial class InformationPage : ContentPage
    {
        #region Fields

        private InformationViewModel _viewModel;

        #endregion Fields

        #region Constructors

        public InformationPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new InformationViewModel();
        }

        #endregion Constructors



        #region Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void Notifications_Toggled(object sender, ToggledEventArgs e)
        {
            if (_viewModel.IsBusy)
            {
                return;
            }
            if (_viewModel.NotificationsToggled)
            {
                var notification = new NotificationRequest
                {
                    Title = "Hej, uživaj u svom danu.",
                    Description = "...ali ne zaboravi učiti njemački!",
                    NotificationId = 1337,
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(5),
                        NotifyRepeatInterval = TimeSpan.FromDays(1)
                    }
                };
                LocalNotificationCenter.Current.Show(notification);
                Preferences.Set("notifications_toggled", true);
            }
            else
            {
                LocalNotificationCenter.Current.Cancel(1337);
                Preferences.Set("notifications_toggled", false);
            }
        }

        private void Score_Toggled(object sender, ToggledEventArgs e)
        {
            if (_viewModel.IsBusy)
            {
                return;
            }
            if (_viewModel.ScoreToggled)
            {
                Preferences.Set("score_toggled", true);
            }
            else
            {
                Preferences.Set("score_toggled", false);
            }
        }


        #endregion Methods
    }
}