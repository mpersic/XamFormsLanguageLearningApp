using MarcTron.Plugin;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamFormsLanguageLearningApp.Views
{
    public partial class AboutPage : ContentPage
    {
        #region Constructors

        public AboutPage()
        {
            InitializeComponent();

            CrossMTAdmob.Current.AdsId = "ca-app-pub-9059808668630923/8030440475";
            CrossMTAdmob.Current.OnRewardedVideoAdLoaded += (s, args) =>
            {
                CrossMTAdmob.Current.ShowRewardedVideo();
            };
        }

        #endregion Constructors



        #region Methods

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                CrossMTAdmob.Current.OnRewardedVideoAdLoaded += (s, args) =>
                {
                    CrossMTAdmob.Current.ShowRewardedVideo();
                };
            }
            catch (Exception ex)
            {
            }
        }

        private void Button1_Clicked(object sender, EventArgs e)
        {
            try
            {
                CrossMTAdmob.Current.LoadRewardedVideo("ca-app-pub-9059808668630923/8030440475");
            }
            catch (Exception ex)
            {
            }
        }

        #endregion Methods
    }
}