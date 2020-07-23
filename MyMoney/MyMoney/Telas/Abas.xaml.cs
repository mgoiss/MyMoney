using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyMoney.Banco;
using System.Globalization;

namespace MyMoney.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Abas : TabbedPage
	{
		public Abas ()
		{           
			InitializeComponent ();

            this.Appearing += AtualizarValor; //Faz atualizar o valor total            
		}

        //Função para atualizar a lista de transações
        private void AtualizarValor(object sender, System.EventArgs e)
        {
            DataBase data = new DataBase();
            double total = data.TotalDinheiro(); //Pegando o valor total de todas as contas
            //Total.Text = string.Format("{0:C}", total.ToString()); //Exibindo o valor total das contas    
            Total.Text = total.ToString("C2", CultureInfo.CurrentCulture); //Exibindo o valor total das contas    
            //Total.Text = total.ToString(); //Exibindo o valor total das contas    

        }
    }
}