using MyMoney.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyMoney
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = new ModeloVisual();
        }
        /*
        private void Comecar(object sender, EventArgs e)
        {
            App.Current.MainPage = new Abas();
        }*/
    }
}
