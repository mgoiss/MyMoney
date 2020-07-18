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
	public partial class PopupDetalhe
	{
        Conta ContaAtual;
        Transacao TransacaoAtual;

		public PopupDetalhe (Transacao transacao, Conta conta)
		{
			InitializeComponent ();

            BindingContext = transacao;

            ContaAtual = conta;
            TransacaoAtual = transacao;

            txtValor.Text = transacao.ValorTransacao.ToString();
            txtData.Text = transacao.DataTransacao.ToString();
            txtDescricao.Text = transacao.DescTransacao;            
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

        private bool AnaliseTransação()
        {                  
            if (TransacaoAtual.SimboloTransacao == '+') //Verificando o tipo de transação
            {
                //Analisando se aquela transação pode ser apagada tendo em vista que a conta não pode ficar com valor negativo
                if (TransacaoAtual.ValorTransacao > ContaAtual.ValorConta)
                {
                    DisplayAlert("Atenção", "Transação não pode ser apagada, tendo em vista que a conta ficará com o saldo negativo", "OK");

                    return false;
                }
                else
                {
                    ContaAtual.ValorConta -= TransacaoAtual.ValorTransacao;

                    return true;
                }
            }
            else
            {
                ContaAtual.ValorConta += TransacaoAtual.ValorTransacao;

                return true;
            }
        }

        private async void Apagar(object sender, EventArgs e)
        {
            if (txtDescricao.Text == "Valor Inicial da conta") //Verificando se a transa~ção não representa o valor inicial
            {
                await DisplayAlert("Atenção", "Transação não pode ser apagada, tendo em vista que ela representa o valor inicial da conta", "OK");
            }
            else
            {
                bool result = await DisplayAlert("Atenção", "Deseja realmente apagar essa transação?", "Sim", "Não");
                if (result == true)
                {
                    if (AnaliseTransação())
                    {
                        DataBase data = new DataBase();

                        data.ApagarTransacao(TransacaoAtual, ContaAtual.ValorConta);

                        //armengue para fazer o app atualizar os dados
                        Application.Current.MainPage.Navigation.PopAsync(); // Remova a página atualmente no topo.
                        Application.Current.MainPage.Navigation.PopAsync(); // Remova a página atualmente no topo.                
                        await Navigation.PushAsync(new TelaDetalheConta(ContaAtual)); //Chamando novamente a tela detalhe
                        PopupNavigation.Instance.PopAsync(); ; //Por algum motivo precisou dessa parte para voltar a tela inicial
                    }
                }
            }
            
        }
    }
}