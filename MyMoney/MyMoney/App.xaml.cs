using MyMoney.Banco;
using MyMoney.Telas;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyMoney
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new Telas.TelaPrincipal());
            //MainPage = new NavigationPage(new Telas.Abas()); 
            //MainPage = new NavigationPage(new MainPage());
            //MainPage = new Telas.Abas();
            //App.Current.MainPage = new NavigationPage(new Telas.Abas()) { BarBackgroundColor = Color.FromHex("#E02041") };

            var usuario = new DataBase();

            if (usuario.VerificarExistenciaUsuario() == true)
            {
                App.Current.MainPage = new TelaLogin();
            }
            else
            {
                App.Current.MainPage = new MainPage();
            }

            //MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
