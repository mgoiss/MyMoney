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

        //Função para atualizar a lista de transações
        private void FiltrarLista(object sender, System.EventArgs e)
        {
            var itemSelecionado = pcFiltro.Items[pcFiltro.SelectedIndex]; //Pegando o item selecionado

            if (pcFiltro.Items[pcFiltro.SelectedIndex] == "Depósito") //DEFININDO O TEM DA FORMA QUE TÁ SALVO NO BANCO
            {
                itemSelecionado = "deposito";
            }
            else
            {
                itemSelecionado = "saque";
            }            

            DataBase trans = new DataBase();

            var Transacaos = trans.FiltrarTransacaoConta(ContaAtual.Id, itemSelecionado); //FILTRANDO

            if (Transacaos.Count == 0) //Analisando se foi encontrado alguma transação daquele item
            {
                DisplayAlert("Atenção", "Não foi encontrado nenhuma: " + pcFiltro.Items[pcFiltro.SelectedIndex], "OK");
            }
            else
            {
                //Exibindo os dados
                ListaConta.ItemsSource = Transacaos;
                ListaConta.EndRefresh();
            }
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

        //Função para v o detalhe da transação por meio do click na Label
        /*private async void VerDetalhe(object sender, EventArgs e)
        {
            Label lblDetalhe = (Label)sender;
            TapGestureRecognizer tapGest = ((TapGestureRecognizer)lblDetalhe.GestureRecognizers[0]);
            Transacao trans = tapGest.CommandParameter as Transacao;

            await PopupNavigation.Instance.PushAsync(new PopupDetalhe(trans, ContaAtual));
        }*/

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

        //Metodo para filtar por data
        private void PcData_DateSelected(object sender, DateChangedEventArgs e)
        {
            var dataSelecionado = pcData.Date.Date.ToString("dd/MM/yyyy"); //Pegando o item selecionado            

            if (pcData.Date.Date > DateTime.Now) //Analisando se a data informada não é maior do que a do dia atual
            {
                DisplayAlert("Atenção", "A data selecionada é maior que a atual", "OK");
            }
            else
            {
                DataBase trans = new DataBase();

                //var Transacaos = trans.FiltrarDataTransacaoConta(ContaAtual.Id, dataSelecionado); //FILTRANDO

                var Transacaos = trans.ListarTransacaoConta(ContaAtual.Id); //Pegando todos os itens da lista

                List<Transacao> valoresFiltrado = new List<Transacao>();

                foreach (var item in Transacaos) //Filtrando a lista a partir da data
                {
                    if (item.DataTransacao.ToString("dd/MM/yyyy") == dataSelecionado)
                    {
                        valoresFiltrado.Add(item);
                    }
                }

                if (valoresFiltrado.Count == 0) //Analisando se foi encontrado alguma transação daquele item
                {
                    DisplayAlert("Atenção", "Não foi encontrado nenhuma transação na data: " + dataSelecionado, "OK");
                }
                else
                {
                    //Exibindo os dados
                    ListaConta.ItemsSource = valoresFiltrado;
                    ListaConta.EndRefresh();
                }
            }
                        
        }

        private async void ListaContaToque(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                var selection = e.Item as Transacao;
                await PopupNavigation.Instance.PushAsync(new PopupDetalhe(selection, ContaAtual));
            }
        }
    }
}