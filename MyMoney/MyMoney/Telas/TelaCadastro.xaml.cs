using MyMoney.Banco;
using MyMoney.Modelo;
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
	public partial class TelaCadastro : ContentPage
	{
		public TelaCadastro ()
		{
			InitializeComponent ();
        }

        private void VamosLa(object sender, EventArgs args)
        {
            //TODO: Verificar se os dados obrigatorios foram preenchidos e se foi adicionado ao banco
            if(verificardados())
            {
                DataBase data = new DataBase();

                Usuario use = new Usuario(txtNome.Text, int.Parse(txtSenha.Text));

                double valor = 0.0;
                if(txtValor.Text != null)
                {
                    valor = double.Parse(txtValor.Text);
                }

                Conta cont = new Conta(txtNomeConta.Text, txtObjetivoConta.Text, valor);

                data.InserirUsuario(use, cont);

                App.Current.MainPage = new TelaLogin();
            }            
        }

        private bool verificardados()
        {
            int senha = 0;
            int valor = 0;

            if (txtNome.Text == null || txtSenha.Text == null || txtNomeConta.Text == null || txtObjetivoConta.Text == null)
            {
                DisplayAlert("Erro", "Preencha todos os dados obrigatorios", "OK");

                return false;
            }
            else if (!(Int32.TryParse(txtSenha.Text.ToString(), out senha)))
            {
                DisplayAlert("Erro", "O campo senha deve ser preenchido apenas com numero!", "OK");

                return false;
            }
            else if (txtValor.Text != null)
            {
                if(!(Int32.TryParse(txtValor.Text.ToString(), out valor)))
                {
                    DisplayAlert("Erro", "O campo valor deve ser preenchido apenas com numero!", "OK");

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