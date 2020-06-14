using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMoney.Telas.Popup;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyMoney.Modelo;

namespace MyMoney.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TelaDetalheConta : ContentPage
	{
		public TelaDetalheConta ( Conta conta)
		{
			InitializeComponent ();

            BindingContext = conta;
		}

        private async void Depositar(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopupDeposito());
        }

        private async void Sacar(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopupSaque());
        }

        private async void VerDetalhe(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopupDetalhe());
        }
    }
}