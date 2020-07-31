using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using Rg.Plugins.Popup.Services;
using CarouselView.FormsPlugin.Android;

namespace MyMoney.Droid
{
    [Activity(Label = "MyMoney", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, WindowSoftInputMode = SoftInput.AdjustPan, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private Bundle bundle;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            Rg.Plugins.Popup.Popup.Init(this, bundle);

            CarouselViewRenderer.Init();
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // fazer algo se existem algumas páginas no `PopupStack`
                PopupNavigation.Instance.PopAsync();
            }
            else
            {
                // Faça alguma coisa se não houver nenhuma página no `PopupStack` 
            }
        }
    }
}