using MarcTron.Plugin;
using Plugin.LocalNotification;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamFormsLanguageLearningApp.ViewModels;

namespace XamFormsLanguageLearningApp.Views
{
    public partial class InformationPage : ContentPage
    {

        private InformationViewModel _viewModel;


        #region Constructors

        public InformationPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new InformationViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        #endregion Constructors

        private void Button_Clicked(object sender, EventArgs e)
        {

            var notification = new NotificationRequest
            {
                Description = "Practice!",
                Title = "Practice notification!",
                NotificationId = 1337,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(5),
                    NotifyRepeatInterval = TimeSpan.FromDays(1)
                } 
            };
            LocalNotificationCenter.Current.Show(notification);
        }
    }
}