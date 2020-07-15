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
using System.Diagnostics;

namespace MyMoney.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TelaPrincipal : ContentPage
	{
		public TelaPrincipal ()
		{
			InitializeComponent ();

            this.Appearing += AtualizarLista; //Faz atualizar os dados da lista
        }


        //Função para atualizar a lista de transações
        private void AtualizarLista(object sender, System.EventArgs e)
        {
            //TODO criar o metodo para atualizar
            DataBase cont = new DataBase();
            var Contas = cont.ListarContas();
            ListaConta.ItemsSource = Contas;
        }

        private void DetalheConta(object sender, EventArgs args)
        {
            Label lblDetalhe = (Label)sender;
            TapGestureRecognizer tapGest = ((TapGestureRecognizer)lblDetalhe.GestureRecognizers[0]);
            Conta conta = tapGest.CommandParameter as Conta;

            Navigation.PushAsync(new TelaDetalheConta(conta));


            //App.Current.MainPage = new NavigationPage(new TelaDetalheConta());
        }

        private async void ApagarConta(object sender, EventArgs e)
        {
            Button btnDetalhe = (Button)sender;
            TapGestureRecognizer tapGest = ((TapGestureRecognizer)btnDetalhe.GestureRecognizers[0]);
            Conta conta = tapGest.CommandParameter as Conta;

            bool resultado = await DisplayAlert(conta.NomeConta, "Deseja apagar a conta?", "Sim", "Não");
            if (resultado == true)
            {
                DataBase data = new DataBase();

                data.ApagarConta(conta);

                App.Current.MainPage = new NavigationPage(new Abas()) { BarBackgroundColor = Color.FromHex("#E02041") }; //Chamando novamente a aba para que seja recarregado o valor total
            }
        }

        private async void CriarConta(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopupCriarConta());

            App.Current.MainPage = new NavigationPage(new Abas()) { BarBackgroundColor = Color.FromHex("#E02041") }; //Chamando novamente a aba para que seja recarregado o valor total
        }
    }
}