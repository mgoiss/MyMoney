using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using MyMoney.Telas.Popup;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyMoney.Modelo;
using MyMoney.Banco;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Pages;
using System.Collections.Generic;

namespace MyMoney.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TelaDetalheConta : ContentPage
    {
        Conta ContaAtual = new Conta();

        ObservableCollection<Transacao> transacaos = new ObservableCollection<Transacao>(); //Criando uma coleção dp tipo Transação para exibir na lista

        public TelaDetalheConta(Conta conta)
        {
            InitializeComponent();

            BindingContext = conta;

            ContaAtual = conta;

            //AtualizarLista();

            this.Appearing += AtualizarLista; //Faz atualizar os dados da lista AtualizarLista(ContaAtual.Id);
        }

        //Função para atualizar a lista de transações
        private void AtualizarLista(object sender, System.EventArgs e)
        {
            DataBase cont = new DataBase();
            var Transacaos = cont.ListarTransacaoConta(ContaAtual.Id);

            ListaConta.ItemsSource = Transacaos;

            ListaConta.EndRefresh();

            /*transacaos.Clear();
            ListaConta.ItemsSource = transacaos;
            foreach (var valor in Transacaos)
            {
                transacaos.Add(new Transacao { CodTransacao = valor.CodTransacao, ValorTransacao = valor.ValorTransacao, DescTransacao = valor.DescTransacao, DataTransacao = valor.DataTransacao, TipoTransacao = valor.TipoTransacao, SimboloTransacao = valor.SimboloTransacao, ContaId = valor.ContaId });
            } */
        }

        private void Depositar(object sender, EventArgs e)
        {
            var page = new PopupDeposito(ContaAtual); //Instanciando um objeto do tipo POPUPDeposito
            PopupNavigation.Instance.PushAsync(page); //Abrindo o POPUP           
        }

        private void Sacar(object sender, EventArgs e)
        {
            var page = new PopupSaque(ContaAtual); //Instanciando um objeto do tipo POPUPSaque
            PopupNavigation.Instance.PushAsync(page); //Abrindo o POPUP
        }

        //Finalizar a função Detalhe
        private async void VerDetalhe(object sender, EventArgs e)
        {
            Label lblDetalhe = (Label)sender;
            TapGestureRecognizer tapGest = ((TapGestureRecognizer)lblDetalhe.GestureRecognizers[0]);
            Transacao trans = tapGest.CommandParameter as Transacao;

            await PopupNavigation.Instance.PushAsync(new PopupDetalhe(trans, ContaAtual));
        }

        private async void ApagarConta(object sender, EventArgs e)
        {
            Button btnDetalhe = (Button)sender;
            TapGestureRecognizer tapGest = ((TapGestureRecognizer)btnDetalhe.GestureRecognizers[0]);
            Conta conta = tapGest.CommandParameter as Conta;

            //Analisando se o usuário deseja apagar a conta
            bool resultado = await DisplayAlert(conta.NomeConta, "Deseja apagar a conta?", "Sim", "Não");
            if (resultado == true)
            {
                DataBase data = new DataBase();

                data.ApagarConta(conta);

                App.Current.MainPage = new NavigationPage(new Abas()) { BarBackgroundColor = Color.FromHex("#E02041") }; //Chamando novamente a aba para que seja recarregado o valor total
            }
        }

        private async void EditarConta(object sender, EventArgs e)
        {

            Button btnDetalhe = (Button)sender;
            TapGestureRecognizer tapGest = ((TapGestureRecognizer)btnDetalhe.GestureRecognizers[0]);
            Conta conta = tapGest.CommandParameter as Conta;

            DataBase data = new DataBase();
            List<Transacao> ValorInicial = data.ListarTransacaoConta(conta.Id);
            double valor = 0.0;

            //Verificando se foi informado um valor inicial, para passa-lo para a tela de edição
            if (ValorInicial[ValorInicial.Count - 1].DescTransacao == "Valor Inicial da conta")
            {
                valor = ValorInicial[ValorInicial.Count - 1].ValorTransacao;
            }

            await PopupNavigation.Instance.PushAsync(new PopupCriarConta(conta, valor));
        }
    }
}