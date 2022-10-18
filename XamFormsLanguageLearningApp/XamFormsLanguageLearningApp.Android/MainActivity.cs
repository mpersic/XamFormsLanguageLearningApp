using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Gms.Ads;
using Plugin.LocalNotification;
using Android.Views;

namespace XamFormsLanguageLearningApp.Droid
{
    [Activity(Label = "Bruder", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        #region Methods

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            LocalNotificationCenter.CreateNotificationChannel();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            MobileAds.Initialize(ApplicationContext);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Window.AddFlags(WindowManagerFlags.TranslucentNavigation);
            //Window.SetBackgroundDrawableResource(Resource.Drawable.MyBackgroundPNG);
            SetStatusBarColor(Android.Graphics.Color.Transparent);

            LoadApplication(new App());
        }

        #endregion Methods
    }
}