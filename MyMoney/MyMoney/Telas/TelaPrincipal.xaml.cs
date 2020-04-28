using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyMoney.Telas.Popup;

namespace MyMoney.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TelaPrincipal : ContentPage
	{
		public TelaPrincipal ()
		{
			InitializeComponent ();
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
        }
    }
}