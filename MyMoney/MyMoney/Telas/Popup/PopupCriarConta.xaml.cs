using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyMoney.Banco;
using MyMoney.Modelo;

namespace MyMoney.Telas.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupCriarConta
    {
        Conta ContaAtual;
        
        public PopupCriarConta()
        {
            InitializeComponent();
        }

        public PopupCriarConta(Conta conta, double valorInicial)
        {
            InitializeComponent();

            //Configurando a tela para o modo de edição
            txtNomeConta.Text = conta.NomeConta;
            txtValor.Text = valorInicial.ToString();
            txtValor.IsEnabled = false;
            txtObjetivoConta.Text = conta.Objetivo;

            btncriar.Text = "SALVAR";

            ContaAtual = conta;
        }

        private async void Criar(Object sender, EventArgs args)
        {
            if(verificardados()) //Verificando se todos os dados foram informados
            {
                DataBase data = new DataBase();

                if (btncriar.Text == "SALVAR")
                {
                    //Alterando dados da conta                   
                    ContaAtual.NomeConta = txtNomeConta.Text;
                    ContaAtual.Objetivo = txtObjetivoConta.Text;

                    //Inserindo a conta no banco
                    data.AtualizarConta(ContaAtual);

                    //armengue para fazer o app atualizar os dados
                    Application.Current.MainPage.Navigation.PopAsync(); // Remova a página atualmente no topo.
                    Application.Current.MainPage.Navigation.PopAsync(); // Remova a página atualmente no topo.                
                    await Navigation.PushAsync(new TelaDetalheConta(ContaAtual)); //Chamando novamente a tela detalhe
                    PopupNavigation.Instance.PopAsync(); ; //Por algum motivo precisou dessa parte para voltar a tela inicial
                }
                else
                {                   
                    //Analisando se o valor inicial da conta foi informado
                    double valor = 0.0;
                    if (txtValor.Text != null && txtValor.Text != "")
                    {
                        valor = double.Parse(txtValor.Text.Replace("R", "").Replace("$", "").Replace(" ", "").Replace(".", ""));
                    }

                    //Criando um objeto do tipo conta
                    Conta cont = new Conta(txtNomeConta.Text, txtObjetivoConta.Text, valor);

                    //Inserindo a conta no banco
                    data.InserirConta(cont, 1);

                    //Metodos para voltar a pagina e recarregar o valor total
                    App.Current.MainPage = new NavigationPage(new Abas()) { BarBackgroundColor = Color.FromHex("#E02041") }; //Chamando novamente a aba para que seja recarregado o valor total

                    PopupNavigation.Instance.PopAsync(); //Por algum motivo precisou dessa parte para voltar a tela inicial
                }                                
            }
        }

        private async void Voltar(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private bool verificardados()
        {
            double valor = 0;

            if (txtNomeConta.Text == null || txtObjetivoConta.Text == null)
            {
                DisplayAlert("Atenção", "Preencha todos os dados obrigatorios", "OK");

                return false;
            }
            else if (txtValor.Text != null && txtValor.Text != "")
            {
                if (!(double.TryParse(txtValor.Text.Replace("R", "").Replace("$", "").Replace(" ", "").Replace(".", "").ToString(), out valor)))
                {
                    DisplayAlert("Atenção", "O campo valor deve ser preenchido apenas com numero!", "OK");

                    return false;
                }

                return true;
            }
            else
            {
                return true;
            }

        }
    }
}