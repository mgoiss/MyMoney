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
        //TODO Verificar a questão do Backup, que não tá sendo sobre posto
        //TODO Corrigir o bug da mascara: quando digito um valor e quero apaga-lo, caso eu vá apagando até o final o app quebra 
        //TODO reestruturar as classes
        //TODO Alterar a forma como é chamada os PopUp
        //TODO fazer as ListViwer Carregar gradativamente
        //TODO Login por Digital
        //TODO Criar um metodo para esqueci a senha
        //TODO fazer efeito de carregamento
		public TelaPrincipal ()
		{
			InitializeComponent ();

            this.Appearing += AtualizarLista; //Faz atualizar os dados da lista
        }


        //Função para atualizar a lista de transações
        private void AtualizarLista(object sender, System.EventArgs e)
        {
            DataBase cont = new DataBase();
            var Contas = cont.ListarContas();
            ListaConta.ItemsSource = Contas;
        }

        /*private void DetalheConta(object sender, EventArgs args)
        {
            Label lblDetalhe = (Label)sender;
            TapGestureRecognizer tapGest = ((TapGestureRecognizer)lblDetalhe.GestureRecognizers[0]);
            Conta conta = tapGest.CommandParameter as Conta;

            Navigation.PushAsync(new TelaDetalheConta(conta));


            //App.Current.MainPage = new NavigationPage(new TelaDetalheConta());
        }*/

        private async void ApagarTodaConta(object sender, EventArgs e)
        {
            bool resultado = await DisplayAlert("Atenção", "Deseja realmente apagar todas as contas?", "Sim", "Não");
            if (resultado == true)
            {
                DataBase data = new DataBase();

                data.ApagarTodaConta();

                App.Current.MainPage = new NavigationPage(new Abas()) { BarBackgroundColor = Color.FromHex("#E02041") }; //Chamando novamente a aba para que seja recarregado o valor total
            }
        }

        private async void CriarConta(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupCriarConta());

            App.Current.MainPage = new NavigationPage(new Abas()) { BarBackgroundColor = Color.FromHex("#E02041") }; //Chamando novamente a aba para que seja recarregado o valor total
        }

        private void ListaContaToque(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                var selection = e.Item as Conta;
                Navigation.PushAsync(new TelaDetalheConta(selection));
            }
        }
    }
}