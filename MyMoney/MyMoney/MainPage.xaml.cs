using MyMoney.Modelo;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MyMoney.Telas;
using MyMoney.Banco;

namespace MyMoney
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = new ModeloVisual();
        }
 
        private void Comecar(object sender, EventArgs e)
        {
            //TODO: Colocar um teste, para analisar se é o primeiro acesso ao sistema
            var usuario = new DataBase();

            if(usuario.VerificarExistenciaUsuario() == true)
            {
                App.Current.MainPage = new TelaLogin();
            }
            else
            {
                App.Current.MainPage = new TelaCadastro();
            }
        }
    }
}
