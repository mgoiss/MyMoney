using MyMoney.Banco;
using MyMoney.Modelo;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMoney.Telas.Popup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupSaque: Rg.Plugins.Popup.Pages.PopupPage
    {
        int idConta = 0;
        Conta Conta;

        public PopupSaque (Conta conta)
		{
			InitializeComponent ();
            Conta = conta;
            idConta = conta.Id;
        }

        // // Chamada quando um botão de retorno de hardware é pressionado 
        protected override bool OnBackButtonPressed()
        {
            // Retorna true se você não deseja fechar esta página pop-up quando voltar o botão é pressionado
            return base.OnBackButtonPressed();
        }

        // Chamado quando fundo é clicado
        protected override bool OnBackgroundClicked()
        {
            // retornar falso se você não quer fechar esta página pop-up quando um fundo da página pop-up é clicado
            return base.OnBackgroundClicked();
        }

        private void Cancelar(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private bool ValidarDados()
        {
            //verificação se foi informado dados
            if ((txtValor.Text == "" || txtValor.Text == null) || (txtDescricao.Text == "" || txtDescricao.Text == null))
            {
                if ((txtValor.Text == "" || txtValor.Text == null) && (txtDescricao.Text == "" || txtDescricao.Text == null)) //Todos os campos
                {
                    DisplayAlert("Atenção", "Prencheda todos os Dados!", "OK");

                    return false;
                }
                else if ((txtValor.Text == "" || txtValor.Text == null)) //campo valor
                {
                    DisplayAlert("Atenção", "Prencheda o valor!", "OK");

                    return false;
                }
                else //if(txtDescricao.Text == "" || txtDescricao.Text == null) //campo descrição
                {
                    DisplayAlert("Atenção", "Prencheda a descrição!", "OK");

                    return false;
                }
            }
            else
            {
                double senha;

                if (!(double.TryParse(txtValor.Text.ToString(), out senha))) //VERIFICANDO SE O CAMPO VALOR FOI PREENCHIDO COM NUMERO
                {
                    DisplayAlert("Atenção", "O campo valor deve ser preenchido apenas com numero!", "OK");

                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private async void Depositar(object sender, EventArgs e)
        {
            if (ValidarDados() == true) //Validando dados
            {
                if (double.Parse(txtValor.Text) > Conta.ValorConta) //Verificando se o valor do saque não é maior que o valor total na conta
                {
                    await DisplayAlert("Atenção", "O valor de saque excede o valor total da conta!", "OK");
                }
                else
                {
                    DataBase data = new DataBase();

                    //Criando um objeto do tipo transação
                    Transacao deposito = new Transacao(double.Parse(txtValor.Text), txtDescricao.Text, DateTime.Now, "saque", idConta);

                    //Inserindo a transação no banco
                    data.InserirTransacao(deposito, Conta.ValorConta - deposito.ValorTransacao);

                    //inserindo o novo valor no objeto conta para ser exibido para o usuário
                    Conta.ValorConta -= deposito.ValorTransacao;

                    //Marmengue para fazer o app atualizar os dados
                    Application.Current.MainPage.Navigation.PopAsync(); // Remova a página atualmente no topo.
                    await Navigation.PushAsync(new TelaDetalheConta(Conta)); //Chamando novamente a tela detalhe
                    PopupNavigation.Instance.PopAsync(); ; //Por algum motivo precisou dessa parte para voltar a tela inicial
                }
                
            }                
        }
    }
}