using MyMoney.Banco;
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
	public partial class TelaLogin : ContentPage
	{
		public TelaLogin ()
		{
			InitializeComponent ();
		}

        private void Acessar(object sender, EventArgs args)
        {
            DataBase data = new DataBase();

            int senha = 0;

            if(txtSenha.Text != null)
            {
                if (Int32.TryParse(txtSenha.Text.ToString(), out senha))
                {
                    if (data.Login(int.Parse(txtSenha.Text)))
                    {
                        App.Current.MainPage = new NavigationPage(new Abas()) { BarBackgroundColor = Color.FromHex("#E02041") };                    
                    }
                    else
                    {
                        DisplayAlert("Erro", "Senha Incorreta", "OK");
                    }
                }
                else
                {
                    DisplayAlert("Erro", "O campo senha deve ser preenchido apenas com numero!", "OK");
                }

            }
            else
            {
                DisplayAlert("Erro", "Informe uma senha!", "OK");
            }
        }
	}
}