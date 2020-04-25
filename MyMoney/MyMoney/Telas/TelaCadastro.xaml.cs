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
            App.Current.MainPage = new TelaLogin();
        }
	}
}