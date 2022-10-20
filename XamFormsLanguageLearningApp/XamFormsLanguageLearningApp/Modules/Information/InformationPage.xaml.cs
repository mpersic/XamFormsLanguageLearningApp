﻿using MarcTron.Plugin;
using Plugin.LocalNotification;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamFormsLanguageLearningApp.Effects;
using XamFormsLanguageLearningApp.ViewModels;

namespace XamFormsLanguageLearningApp.Views
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

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (_viewModel.NotificationsToggled)
            {
                var notification = new NotificationRequest
                {
                    Title = "Hej, uživaj u svom danu.",
                    Description = "Ali ne zaboravi učiti Njemački!",
                    NotificationId = 1337,
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(5),
                        NotifyRepeatInterval = TimeSpan.FromDays(1)
                    }
                };
                LocalNotificationCenter.Current.Show(notification);
            }
            else
            {
                LocalNotificationCenter.Current.Cancel(1337);
            }
        }

        #endregion Methods
    }
}