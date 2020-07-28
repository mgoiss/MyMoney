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
        }

        private void Comecar(object sender, EventArgs e)
        {
            App.Current.MainPage = new TelaCadastro();
        }
    }
}
