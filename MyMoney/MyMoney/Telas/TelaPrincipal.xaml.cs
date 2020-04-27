using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        private void CriarConta(object sender, EventArgs args)
        {
            //TODO: Passar o id da conta para puxar os dados na tela de detalhe
            Navigation.PushModalAsync(new TelaCriarConta());
        }
    }
}