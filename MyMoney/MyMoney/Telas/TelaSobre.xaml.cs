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
    public partial class TelaSobre : ContentPage
    {
        public TelaSobre()
        {
            InitializeComponent();
        }

        private async void ApagarTudo(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Atenção", "Deseja realmente apagar todos os dados?", "Sim", "Não");
            if (result == true)
            {

                DataBase data = new DataBase();

                data.ApagarDados();

                //Chamando a tela cadastro
                App.Current.MainPage = new TelaCadastro();

            }
        }

    }
}