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
            //TODO: Verificar a senha
            //App.Current.MainPage = new Abas();
            App.Current.MainPage = new NavigationPage(new Abas()) { BarBackgroundColor = Color.FromHex("#E02041")};
        }
	}
}