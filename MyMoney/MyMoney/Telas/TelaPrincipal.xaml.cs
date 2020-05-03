using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyMoney.Telas.Popup;
using MyMoney.Banco;
using MyMoney.Modelo;

namespace MyMoney.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TelaPrincipal : ContentPage
	{
		public TelaPrincipal ()
		{
			InitializeComponent ();

            DataBase cont = new DataBase();
            var Contas = cont.ListarContas();


            ListaConta.ItemsSource = Contas;
		}

        private void DetalheConta(object sender, EventArgs args)
        {
            //TODO: Passar o id da conta para puxar os dados na tela de detalhe
            Navigation.PushAsync(new TelaDetalheConta());
            //App.Current.MainPage = new NavigationPage(new TelaDetalheConta());
        }

        private async void CriarConta(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopupCriarConta());

            App.Current.MainPage = new NavigationPage(new Abas()) { BarBackgroundColor = Color.FromHex("#E02041") }; //Chamando novamente a aba para que seja recarregado o valor total
        }

        private void ListaConta_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}